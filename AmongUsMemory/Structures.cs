using System;
using System.Runtime.InteropServices;

namespace HamsterCheese.AmongUsMemory
{
    [System.Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerControll
    {
   
        [FieldOffset(0x08)]
        public UIntPtr m_cachedPtr;
        [FieldOffset(0x0c)]
        public uint spawnId;
        [FieldOffset(0x10)]
        public uint netId;
        [FieldOffset(0x14)]
        public uint DirtyBits;
        [FieldOffset(0x18)]
        public int SpawnFlags;
        [FieldOffset(0x19)]
        public byte sendMode;
        [FieldOffset(0x1c)]
        public uint OwnerId;
        [FieldOffset(0x20)]
        public byte DespawnOnDestroy;
        [FieldOffset(0x24)]
        public Int32 LastStartCounter;
        [FieldOffset(0x28)]
        public byte PlayerId;
        [FieldOffset(0x2c)]
        public Single MaxReportDistance;
        [FieldOffset(0x30)]
        public bool moveable;
        [FieldOffset(0x31)]
        public byte inVent;
        [FieldOffset(0x34)]
        public UIntPtr _cachedData;
        [FieldOffset(0x38)]
        public UIntPtr FootSteps;
        [FieldOffset(0x3c)]
        public UIntPtr KillSfx;
        [FieldOffset(0x40)]
        public UIntPtr KillAnimations;
        [FieldOffset(0x44)]
        public Single killTimer;
        [FieldOffset(0x48)]
        public Int32 RemainingEmergencies;
        [FieldOffset(0x4c)]
        public UIntPtr nameText;
        [FieldOffset(0x50)]
        public UIntPtr LightPrefab;
        [FieldOffset(0x54)]
        public UIntPtr myLight;
        [FieldOffset(0x58)]
        public UIntPtr Collider;
        [FieldOffset(0x5c)]
        public UIntPtr MyPhysics;
        [FieldOffset(0x60)]
        public UIntPtr NetTransform;
        [FieldOffset(0x64)]
        public UIntPtr CurrentPet;
        [FieldOffset(0x68)]
        public UIntPtr HatRenderer;
        [FieldOffset(0x6c)]
        public UIntPtr myRend;
        [FieldOffset(0x70)]
        public UIntPtr hitBuffer;
        [FieldOffset(0x74)]
        public UIntPtr myTasks;
        [FieldOffset(0x78)]
        public UIntPtr ScannerAnims;
        [FieldOffset(0x7c)]
        public UIntPtr ScannersImages;
        [FieldOffset(0x80)]
        public UIntPtr closest;
        [FieldOffset(0x84)]
        public Boolean isNew;
        [FieldOffset(0x88)]
        public UIntPtr cache;
        [FieldOffset(0x8c)]
        public UIntPtr itemsInRange;
        [FieldOffset(0x90)]
        public UIntPtr newItemsInRange;
        [FieldOffset(0x94)]
        public Byte scannerCount;
        [FieldOffset(0x95)]
        public Boolean infectedSet;
    }
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
    public struct PlayerInfo
    {
        [System.Runtime.InteropServices.FieldOffset(8)]         public byte PlayerId;
        [System.Runtime.InteropServices.FieldOffset(12)]        public IntPtr PlayerName;
        [System.Runtime.InteropServices.FieldOffset(16)]        public byte ColorId;
        [System.Runtime.InteropServices.FieldOffset(20)]        public uint HatId;
        [System.Runtime.InteropServices.FieldOffset(24)]        public uint PetId;
        [System.Runtime.InteropServices.FieldOffset(28)]        public uint SkinId;
        [System.Runtime.InteropServices.FieldOffset(32)]        public byte Disconnected;
        [System.Runtime.InteropServices.FieldOffset(36)]        public IntPtr Tasks;
        [System.Runtime.InteropServices.FieldOffset(40)]        public byte IsImpostor;
        [System.Runtime.InteropServices.FieldOffset(41)]        public byte IsDead;
        [System.Runtime.InteropServices.FieldOffset(44)]        public IntPtr _object;
    }



        [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2
    {
        public float x,y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsZero()
        {
            if (this.x == 0 && this.y == 0) return true;
            else return false;
        }
        public bool IsGarbage()
        {
            if (this.x == float.MaxValue && this.y == float.MaxValue) return true;
            else return false;
        }
        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0, 0);
            }
        }
    }
}