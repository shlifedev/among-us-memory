using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;

namespace HamsterCheese.AmongUsMemory
{
    public static class Utils
    {
        static Dictionary<(Type, string), int> _offsetMap = new Dictionary<(Type, string), int>();

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

        public static IntPtr GetMemberPointer(IntPtr basePtr, Type type, string fieldName)
        {
            var offset = GetOffset(type, fieldName); 
            return basePtr.Sum(offset);
        }
        public static int GetOffset(Type type, string fieldName)
        {
            if (_offsetMap.ContainsKey((type, fieldName)))
            {
                return _offsetMap[(type, fieldName)];
            }
            var field = type.GetField(fieldName);
            var atts = field.GetCustomAttributes(true);
            foreach (var att in atts)
            {
                if (att.GetType() == typeof(FieldOffsetAttribute))
                {
                    _offsetMap.Add((type, fieldName), (att as FieldOffsetAttribute).Value);
                    return (att as FieldOffsetAttribute).Value;
                }
            }

            return -1;
        }

    }
}