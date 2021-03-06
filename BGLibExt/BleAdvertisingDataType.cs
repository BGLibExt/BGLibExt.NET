﻿namespace BGLibExt
{
    public enum BleAdvertisingDataType
    {
        Flags = 0x01,
        IncompleteListof16BitServiceClassUUIDs = 0x02,
        CompleteListof16BitServiceClassUUIDs = 0x03,
        IncompleteListof32BitServiceClassUUIDs = 0x04,
        CompleteListof32BitServiceClassUUIDs = 0x05,
        IncompleteListof128BitServiceClassUUIDs = 0x06,
        CompleteListof128BitServiceClassUUIDs = 0x07,
        ShortenedLocalName = 0x08,
        CompleteLocalName = 0x09,
        TxPowerLevel = 0x0A,
        ClassofDevice = 0x0D,
        SimplePairingHashC = 0x0E,
        SimplePairingHashC192 = 0x0E,
        SimplePairingRandomizerR = 0x0F,
        SimplePairingRandomizerR192 = 0x0F,
        DeviceID = 0x10,
        SecurityManagerTKValue = 0x10,
        SecurityManagerOutofBandFlags = 0x11,
        SlaveConnectionIntervalRange = 0x12,
        Listof16BitServiceSolicitationUUIDs = 0x14,
        Listof128BitServiceSolicitationUUIDs = 0x15,
        ServiceData = 0x16,
        ServiceData16BitUUID = 0x16,
        PublicTargetAddress = 0x17,
        RandomTargetAddress = 0x18,
        Appearance = 0x19,
        AdvertisingInterval = 0x1A,
        LEBluetoothDeviceAddress = 0x1B,
        LERole = 0x1C,
        SimplePairingHashC256 = 0x1D,
        SimplePairingRandomizerR256 = 0x1E,
        Listof32BitServiceSolicitationUUIDs = 0x1F,
        ServiceData32BitUUID = 0x20,
        ServiceData128BitUUID = 0x21,
        LESecureConnectionsConfirmationValue = 0x22,
        LESecureConnectionsRandomValue = 0x23,
        URI = 0x24,
        IndoorPositioning = 0x25,
        TransportDiscoveryData = 0x26,
        LESupportedFeatures = 0x27,
        ChannelMapUpdateIndication = 0x28,
        PBADV = 0x29,
        MeshMessage = 0x2A,
        MeshBeacon = 0x2B,
        ThreeDimensionalInformationData = 0x3D,
        ManufacturerSpecificData = 0xFF
    }
}
