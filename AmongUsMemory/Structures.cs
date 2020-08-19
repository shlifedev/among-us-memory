using System;
using System.Runtime.InteropServices;

namespace HamsterCheese.AmongUsMemory
{
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PlayerControll
    {
        public UIntPtr m_cachedPtr;
        public uint spawnId;
        public uint netId;
        public uint DirtyBits;
        public byte SpawnFlags;
        public bool sendMode;
        public uint OwnerId;
        public byte DespawnOnDestroy;
        public Int32 LastStartCounter;
        public byte PlayerId;
        public Single MaxReportDistance;
        public bool moveable;
        public byte inVent;
        public UIntPtr _cachedData;
        public UIntPtr FootSteps;
        public UIntPtr KillSfx;
        public UIntPtr KillAnimations;
        public Single killTimer;
        public Int32 RemainingEmergencies;
        public UIntPtr nameText;
        public UIntPtr LightPrefab;
        public UIntPtr myLight;
        public UIntPtr Collider;
        public UIntPtr MyPhysics;
        public UIntPtr NetTransform;
        public UIntPtr CurrentPet;
        public UIntPtr HatRenderer;
        public UIntPtr myRend;
        public UIntPtr hitBuffer;
        public UIntPtr myTasks;
        public UIntPtr ScannerAnims;
        public UIntPtr ScannersImages;
        public UIntPtr closest;
        public Boolean isNew;
        public UIntPtr cache;
        public UIntPtr itemsInRange;
        public UIntPtr newItemsInRange;
        public Byte scannerCount;
        public Boolean infectedSet;
    }
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PlayerInfo
    {
        public IntPtr test,test2;
        public byte PlayerId;
        public UIntPtr PlayerName;
        public byte ColorId;
        public uint HatId;
        public uint PetId;
        public uint SkinId;
        public byte Disconnected;
        public UIntPtr Tasks;
        public byte IsImpostor;
        public byte IsDead;
        private UIntPtr _object;
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