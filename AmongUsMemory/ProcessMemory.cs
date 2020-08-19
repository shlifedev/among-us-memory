using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace ProcessUtil
{
    [Flags()]
    public enum ProcessAccess : int
    {
        AllAccess = CreateThread | DuplicateHandle | QueryInformation | SetInformation | Terminate | VMOperation | VMRead | VMWrite | Synchronize,
        CreateThread = 0x2,
        DuplicateHandle = 0x40,
        QueryInformation = 0x400,
        SetInformation = 0x200,
        Terminate = 0x1,
        VMOperation = 0x8,
        VMRead = 0x10,
        VMWrite = 0x20,
        Synchronize = 0x100000
    }

    public static class CurrentUser
    {
        public static WindowsIdentity Identity = WindowsIdentity.GetCurrent();

        public static bool IsAdministrator()
        {
            bool isAdmin = false;

            WindowsPrincipal wp = new WindowsPrincipal(CurrentUser.Identity);
            isAdmin = wp.IsInRole(WindowsBuiltInRole.Administrator);

            return isAdmin;
        }
    }

    public class ProcessMemory
    {
        #region WINAPI definitions

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public int State;
            public int Protect;
            public int Type;
        }
        #endregion

        #region WINAPI DLL-IMPORTS
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);
        [DllImport("kernel32.dll")]
        static extern bool GetExitCodeThread(IntPtr hThread, out uint lpExitCode);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, AllocationType dwFreeType);

        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(IntPtr hProcess, uint lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, int dwLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        #endregion

        #region CONSTRUCTOR
        public ProcessMemory()
        {
            this.m_Process = new Process();
            this.m_ProcessAccess = 0;
        }
        public ProcessMemory(string processName) : this()
        {
            this.m_Process = Process.GetProcessesByName(processName)[0];
        }
        public ProcessMemory(int processId)
            : this()
        {
            this.m_Process = Process.GetProcessById(processId);
        }
        public ProcessMemory(Process process)
            : this()
        {
            this.m_Process = process;
        }

        ~ProcessMemory()
        {
            this.Close();
            this.m_Process = null;
        }
        #endregion

        #region Get/Set
        public int Id { get { return this.m_Process.Id; } }
        public ProcessAccess AccessFlags { get { return this.m_ProcessAccess; } }
        public bool Opened { get { return (this.m_Handle != IntPtr.Zero); } }
        #endregion

        public IntPtr MemAlloc(uint size)
        {
            return MemAlloc(IntPtr.Zero, size, AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
        }
        public IntPtr MemAlloc(IntPtr baseAddress, uint size, AllocationType allocationType, MemoryProtection memoryProtection)
        {
            return VirtualAllocEx(this.m_Handle, baseAddress, size, allocationType, memoryProtection);
        }

        public bool MemFree(IntPtr baseAddress)
        {
            return VirtualFreeEx(this.m_Handle, baseAddress, 0, AllocationType.Release);
        }

        public int CallFunction(IntPtr functionPtr, IntPtr param)
        {
            uint threadID;
            uint returnValue = 0;

            IntPtr hThread = CreateRemoteThread(this.m_Handle, IntPtr.Zero, 0, functionPtr, param, 0, out threadID);
            WaitForSingleObject(hThread, 0xFFFFFFFF);
            GetExitCodeThread(hThread, out returnValue);

            return (int)returnValue;
        }
        /// <summary>
        /// Test
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RemoteThreadParams
        {
            [MarshalAs(UnmanagedType.I4)]
            public int Param1;
            [MarshalAs(UnmanagedType.I4)]
            public int Param2;
        }

        public int CallFunction2(IntPtr functionPtr, IntPtr param)
        {
            uint threadID;
            uint returnValue = 0;
            IntPtr hThread = CreateRemoteThread(this.m_Handle, IntPtr.Zero, 0, functionPtr, param, 0, out threadID);
            WaitForSingleObject(hThread, 0xFFFFFFFF);
            GetExitCodeThread(hThread, out returnValue);
            return (int)returnValue;
        }
        public int SecureCallFunction(IntPtr functionPtr, IntPtr param)
        {
            byte[] SEHBegin = {
                0x68, 0, 0, 0, 0,               // PUSH [ERRORHANDLER]- 
                0x64, 0xFF, 0x35, 0, 0, 0 ,0,   // PUSH DWORD PTR FS:[0]   
                0x64, 0x89, 0x25, 0, 0, 0, 0    // MOV DWORD PTR FS:[0],ESP 
            };

            byte[] inline = {
                0x68, 0, 0, 0, 0,           // PUSH [param]
                0xFF, 0x15, 0, 0, 0, 0,     // CALL DWORD PTR DS:[0] -
                0x59,                       // POP ECX -
                0xEB, 0x09                  // JUMP SHORT +9 - 
            };

            byte[] SEHEnd = {
                0x8B, 0x64, 0xE4, 0x08,         // MOV ESP,DWORD PTR SS:[ESP+8]  
                0x31, 0xC0,                     // XOR EAX,EAX  - set eax
                0x83, 0xE8, 0x01,               // SUB EAX,1      to -1
                0x64, 0x8F, 0x5, 0, 0, 0, 0,    // POP DWORD PTR FS:[0 
                0x83, 0xC4, 0x4,                // ADD ESP,4
                0xC3                            // RETN
            };

            int codeLen = SEHBegin.Length + inline.Length + SEHEnd.Length;
            IntPtr destination = this.MemAlloc((uint) codeLen + 4);
            Buffer.BlockCopy(BitConverter.GetBytes((int)param), 0, inline, 1, 4);
            int SEHandler = (int)destination + SEHBegin.Length + inline.Length; //ㅇ
            Buffer.BlockCopy(BitConverter.GetBytes(SEHandler), 0, SEHBegin, 1, 4);
            this.Write((IntPtr)((int)destination + codeLen), BitConverter.GetBytes((int)functionPtr));
            Buffer.BlockCopy(BitConverter.GetBytes((int)destination + codeLen), 0, inline, 7, 4);
            this.Write(destination, SEHBegin);
            this.Write((IntPtr)((int)destination + SEHBegin.Length), inline);
            this.Write((IntPtr)((int)destination + SEHBegin.Length + inline.Length), SEHEnd);
            int ret = this.CallFunction(destination, IntPtr.Zero);
            bool success = this.MemFree(destination);

            return ret;
        }


        public float CallFunctionFloat(IntPtr functionPtr, IntPtr param)
        {
            float ret = 0;

            byte[] inline = { 0x68,0,0,0,0,0xFF, 0x15, 0, 0, 0, 0, 0xD9, 0x1D, 0, 0, 0, 0, 0xA1, 0, 0, 0, 0,0x59, 0xC3,0,0,0,0,0,0,0,0 };

            IntPtr destination = this.MemAlloc((uint)inline.Length);

            Buffer.BlockCopy(BitConverter.GetBytes((int)param), 0, inline, 1, 4);

            Buffer.BlockCopy(BitConverter.GetBytes((int)functionPtr), 0, inline, 24, 4);
            Buffer.BlockCopy(BitConverter.GetBytes((int)destination + 24), 0, inline, 7, 4);

            byte[] pReturnValue = BitConverter.GetBytes((int)destination + 28);
            Buffer.BlockCopy(pReturnValue, 0, inline, 13, 4);
            Buffer.BlockCopy(pReturnValue, 0, inline, 18, 4);

            this.Write(destination, inline);
            int littleEndian = this.CallFunction(destination, IntPtr.Zero);
            ret = BitConverter.ToSingle(BitConverter.GetBytes(littleEndian).ToArray(), 0);
            bool success = this.MemFree(destination);

            return ret;
        }

        public bool Open(ProcessAccess desiredAccess)
        {
            bool ret = false;

            this.m_ProcessAccess = desiredAccess;

            this.m_Handle = OpenProcess(desiredAccess, false, this.m_Process.Id);
            if (this.m_Handle != null) ret = true;

            return ret;
        }
        public bool Open()
        {
            return this.Open(ProcessAccess.AllAccess);
        }

        public bool Close()
        {
            bool success = false;

            if (this.m_Handle != IntPtr.Zero)
            {
                success = CloseHandle(this.m_Handle);
                if (success)
                    this.m_Handle = IntPtr.Zero;
            }

            return success;
        }


        #region Read/Write
        public byte[] Read(int BaseAddress, int size)
        {
            return this.Read((IntPtr)BaseAddress, size);
        }
        public byte[] Read(IntPtr BaseAddress, int size)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[size];

            bool success = ReadProcessMemory(this.m_Handle, BaseAddress, buffer, (UIntPtr) size, out bytesRead);

            if (!success)
                buffer = null;

            return buffer;
        }

        public IntPtr ReadPointer(IntPtr address)
        {
            return this.ReadPointer(address, 0);
        }
        public IntPtr ReadPointer(IntPtr address, int offset)
        {
            IntPtr ret = IntPtr.Zero;

            if (offset > 0)
            {
                address = (IntPtr)((int)address + offset);
            }

            byte[] ptr = this.Read(address, 4);
            if (ptr != null)
            {
                ret = (IntPtr)BitConverter.ToInt32(ptr, 0);
            }

            return ret;
        }
        public bool Write(IntPtr BaseAddress, byte[] data)
        {
            int bytesWritten = 0;
            bool success = WriteProcessMemory(this.m_Handle, BaseAddress, data, (uint)data.Length, out bytesWritten);
            return success;
        }


        #endregion



        #region DECLARE MEMBER-VARS
        protected Process m_Process;
        protected IntPtr m_Handle;
        protected ProcessAccess m_ProcessAccess;
        #endregion
    }
}
