using System;
using System.Runtime.InteropServices;

namespace HamsterCheese.AmongUsMemory
{
    public static class Utils
    {
        public static T FromBytes<T>(byte[] bytes)
        {
            GCHandle gcHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var data = (T)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(T));
            gcHandle.Free();
            return data;
        }

        public static int SizeOf<T>()
        {
            var size = Marshal.SizeOf(typeof(T));
            return size;
        }


        public static string GetAddress(this long value) { return value.ToString("X"); }
        public static string GetAddress(this int value) { return value.ToString("X"); }
        public static string GetAddress(this uint value) { return value.ToString("X"); }
        public static string GetAddress(this IntPtr value) { return value.ToInt32().GetAddress(); }
        public static IntPtr Sum(this IntPtr ptr, IntPtr ptr2) { return (IntPtr)(ptr.ToInt32() + ptr2.ToInt32()); }
        public static IntPtr Sum(this IntPtr ptr, UIntPtr ptr2) { return (IntPtr)(ptr.ToInt32() + (int)ptr2.ToUInt32()); }
        public static IntPtr Sum(this UIntPtr ptr, IntPtr ptr2) { return (IntPtr)(ptr.ToUInt32() + ptr2.ToInt32()); }
        public static IntPtr Sum(this int ptr, IntPtr ptr2) { return (IntPtr)(ptr + ptr2.ToInt32()); }
        public static IntPtr Sum(this IntPtr ptr, int ptr2) { return (IntPtr)(ptr.ToInt32() + ptr2); }

    }
}