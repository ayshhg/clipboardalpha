using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClipBoardFrameWork
{
    internal class depends
    {
      
        public const int ClipboardUpdateWindowMessageCode = 0x031D;
        public static readonly IntPtr HandledClipboardUpdateReturnCode = IntPtr.Zero;
        public static readonly IntPtr NoMessageHandledReturnCode = IntPtr.Zero;

       
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr windowHandle);

       
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveClipboardFormatListener(IntPtr windowHandle);

       
        private static uint CLIPBRD_E_CANT_OPEN = 0x800401D0;
        public static int UnableToOpenClipboardComErrorCode = (int)CLIPBRD_E_CANT_OPEN;
    }
}
