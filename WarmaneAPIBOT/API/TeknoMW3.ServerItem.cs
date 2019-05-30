using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TeknoMW3_ServerList
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MW3ServerQuery
    {
        public uint Magic4CC;
        public int TimeStamp;

        public byte[] ToBytes()
        {
            int size = Marshal.SizeOf(this);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            byte[] bytes = new byte[size];
            try
            {
                Marshal.StructureToPtr(this, buffer, false);
                Marshal.Copy(buffer, bytes, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return bytes;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    unsafe struct MW3ServerInfo
    {
        public uint Magic4CC;
        public uint TimeStamp;
        public int Players;
        public int MaxPlayers;
        [MarshalAs(UnmanagedType.U1)]
        public bool bPasswordProtected;
        public uint bDedicated;
        public int ServerVersion;
        public ulong SteamId;
        public uint GameIP_int;
        public uint GameIP_ext;
        public ushort GamePort;
        public ushort QueryPort;
        public ushort NetPort;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string SecID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string SecKey;

        public ushort MapName_ptr;
        public ushort ServerName_ptr;
        public ushort ServerTags_ptr;
        public ushort ServerInfos_ptr;

        public ushort RawDataSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2048, ArraySubType = UnmanagedType.U1)]
        public byte[] RawData;

        public static MW3ServerInfo FromBytes(byte[] bytes)
        {
            int size = Marshal.SizeOf(typeof(MW3ServerInfo));
            IntPtr buffer = Marshal.AllocHGlobal(bytes.Length);
            try
            {
                Marshal.Copy(bytes, 0, buffer, bytes.Length);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return (MW3ServerInfo)Marshal.PtrToStructure(buffer, typeof(MW3ServerInfo));
        }

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MW3MasterServerEntry
    {
        public uint IpAddress;
        public ushort QPort;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MW3MasterServerRequest
    {
        public uint Magic4CC;
        public uint Version;
        public ushort QPort;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MW3MasterClientRequest
    {
        public uint Magic4CC;
        public uint Version;

        public byte[] ToBytes()
        {
            int size = Marshal.SizeOf(this);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            byte[] bytes = new byte[size];
            try
            {
                Marshal.StructureToPtr(this, buffer, false);
                Marshal.Copy(buffer, bytes, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return bytes;
        }

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct MW3MasterClientResponse
    {
        public uint NumberOfEntries;
        public MW3MasterServerEntry[] Entries;

        public static MW3MasterClientResponse FromBytes(byte[] bytes)
        {
            IntPtr buffer = Marshal.AllocHGlobal(4);
            var info = new MW3MasterClientResponse();
            try
            {
                Marshal.Copy(bytes, 0, buffer, 4);
                info.NumberOfEntries = (uint)Marshal.PtrToStructure(buffer, typeof(uint));

                info.Entries = new MW3MasterServerEntry[info.NumberOfEntries];

                for (var i = 0; i < info.NumberOfEntries; i++)
                {
                    buffer = Marshal.AllocHGlobal(6);
                    Marshal.Copy(bytes, 4 + i * 6, buffer, 6);
                    info.Entries[i] = (MW3MasterServerEntry)Marshal.PtrToStructure(buffer, typeof(MW3MasterServerEntry));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

            return info;
        }
    }
}
