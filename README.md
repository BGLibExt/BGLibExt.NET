# BGLibExt

A higher abstraction level .NET library for Silicon Labs/Bluegiga BLE112 modules.

This library is based on [jedinja/monomyo](https://github.com/jedinja/monomyo) and [jrowberg/bglib](https://github.com/jrowberg/bglib). The BGLib was left as it was and the higher level abstraction code from [jedinja/monomyo](https://github.com/jedinja/monomyo) was refactored to be used as a library and some BLE advertisement related features have been added. For version 2.0 BGLibExt has been almost completely rewritten, because there were a lot of things that required a clean up.

## Installation

Install nuget package [BGLibExt](https://www.nuget.org/packages/BGLibExt/)

## Usage

### Discover devices

```c#
var bgLib = new BGLib();
var bleModuleConnection = new BleModuleConnection(bgLib);
bleModuleConnection.Start("COM1");

var bleDeviceDiscovery = new BleDeviceDiscovery(bgLib, bleModuleConnection);
bleDeviceDiscovery.ScanResponse += (sender, args) =>
{
};
bleDeviceDiscovery.StartDeviceDiscovery();
await Task.Delay(10000);
bleDeviceDiscovery.StopDeviceDiscovery();

bleModuleConnection.Stop();
```

### Connect to device

```c#
var bgLib = new BGLib();
var bleModuleConnection = new BleModuleConnection(bgLib);
bleModuleConnection.Start("COM1");

var bleDeviceManager = new BleDeviceManager(bgLib, bleModuleConnection);
var bleDevice = await bleDeviceManager.ConnectAsync(address, addressType);

bleModuleConnection.Stop();
```

### Disconnect from device

```c#
var bgLib = new BGLib();
var bleModuleConnection = new BleModuleConnection(bgLib);
bleModuleConnection.Start("COM1");

var bleDeviceManager = new BleDeviceManager(bgLib, bleModuleConnection);
var bleDevice = await bleDeviceManager.ConnectAsync(address, addressType);
await bleDevice.DisconnectAsync();

bleModuleConnection.Stop();
```

### Read characteristic

```c#
var bgLib = new BGLib();
var bleModuleConnection = new BleModuleConnection(bgLib);
bleModuleConnection.Start("COM1");

var bleDeviceManager = new BleDeviceManager(bgLib, bleModuleConnection);
var bleDevice = await bleDeviceManager.ConnectAsync(address, addressType);
var data = await bleDevice.CharacteristicsByUuid[characteristicId].ReadValueAsync();
await bleDevice.DisconnectAsync();

bleModuleConnection.Stop();
```

### Write characteristic

```c#
var bgLib = new BGLib();
var bleModuleConnection = new BleModuleConnection(bgLib);
bleModuleConnection.Start("COM1");

var bleDeviceManager = new BleDeviceManager(bgLib, bleModuleConnection);
var bleDevice = await bleDeviceManager.ConnectAsync(address, addressType);
var data = new byte[] { 1, 2, 3, 4 };
await bleDevice.CharacteristicsByUuid[characteristicId].WriteValueAsync(data);
await bleDevice.DisconnectAsync();

bleModuleConnection.Stop();
```

### Characteristic value changed notifications

```c#
var bgLib = new BGLib();
var bleModuleConnection = new BleModuleConnection(bgLib);
bleModuleConnection.Start("COM1");

var bleDeviceManager = new BleDeviceManager(bgLib, bleModuleConnection);
var bleDevice = await bleDeviceManager.ConnectAsync(address, addressType);
bleDevice.CharacteristicsByUuid[characteristicId].ValueChanged += (sender, args) =>
{
}
await bleDevice.CharacteristicsByUuid[characteristicId].WriteCccAsync(BleCccValue.NotificationsEnabled);
await Task.Delay(10000);
await bleDevice.DisconnectAsync();

bleModuleConnection.Stop();
```
