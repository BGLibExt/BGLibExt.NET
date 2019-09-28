﻿using Bluegiga.BLE.Events.ATTClient;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BGLibExt.BleCommands
{
    internal class BleFindServicesCommand : BleCommand
    {
        private const ushort GattMaxHandle = 0xFFFF;
        private const ushort GattMinHandle = 0x0001;
        private static readonly byte[] GattServiceTypePrimary = new byte[] { 0x00, 0x28 };

        public BleFindServicesCommand()
            : base(BleModuleConnection.Instance.BleProtocol, BleModuleConnection.Instance.SerialPort)
        {
        }

        public BleFindServicesCommand(BleProtocol ble, SerialPort port)
            : base(ble, port)
        {
        }

        public async Task<List<BleService>> ExecuteAsync(byte connection, int timeout = DefaultTimeout)
        {
            return await ExecuteAsync(connection, CancellationToken.None, timeout);
        }

        public async Task<List<BleService>> ExecuteAsync(byte connection, CancellationToken cancellationToken, int timeout = DefaultTimeout)
        {
            var taskCompletionSource = new TaskCompletionSource<List<BleService>>();
            using (var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cancellationTokenSource.CancelAfter(timeout);

                var services = new List<BleService>();

                void OnGroupFound(object sender, GroupFoundEventArgs e)
                {
                    if (e.connection == connection)
                    {
                        services.Add(new BleService(e.uuid, e.start, e.end));
                    }
                }

                void OnProcedureCompleted(object sender, ProcedureCompletedEventArgs e)
                {
                    if (e.connection == connection)
                    {
                        if (services.Any())
                        {
                            taskCompletionSource.SetResult(services);
                        }
                        else
                        {
                            taskCompletionSource.SetException(new Exception("Couldn't find any servies"));
                        }
                    }
                }

                try
                {
                    Ble.Lib.BLEEventATTClientGroupFound += OnGroupFound;
                    Ble.Lib.BLEEventATTClientProcedureCompleted += OnProcedureCompleted;

                    using (cancellationTokenSource.Token.Register(() => taskCompletionSource.SetCanceled(), useSynchronizationContext: false))
                    {
                        Ble.SendCommand(Port, Ble.Lib.BLECommandATTClientReadByGroupType(connection, GattMinHandle, GattMaxHandle, (byte[])GattServiceTypePrimary));

                        return await taskCompletionSource.Task.ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
                finally
                {
                    Ble.Lib.BLEEventATTClientGroupFound -= OnGroupFound;
                    Ble.Lib.BLEEventATTClientProcedureCompleted -= OnProcedureCompleted;
                }
            }
        }
    }
}
