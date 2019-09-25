using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using ClipBoardFrameWork;
using Models;

namespace ClipBoardFrameWork
{
   public class ClipBoardFrame : IDisposable
    {
        public event EventHandler<ClipBoardModel> ClipboardTextChanged;

        HwndSource Win32InteropSource;
        IntPtr WindowInteropHandle;
        private bool disposed = false;


        public ClipBoardFrame(Window clipboardWindow)
        {
            InitializeInteropSource(clipboardWindow);
            InitializeWindowInteropHandle(clipboardWindow);

            StartHandlingWin32Messages();
            AddListenerForClipboardWin32Messages();

        }
   
        
        private void InitializeInteropSource(Window clipboardWindow)
        {
            var presentationSource = PresentationSource.FromVisual(clipboardWindow);
            Win32InteropSource = presentationSource as HwndSource;

            if (Win32InteropSource == null)
            {
               /* throw new ArgumentException(
                    $"Window must be initialized before using the {nameof(WindowClipboardMonitor)}. Use the window's OnSourceInitialized() handler if possible, or a later point in the window lifecycle."
                    , nameof(clipboardWindow));
                    */
            }
        }

        private void InitializeWindowInteropHandle(Window clipboardWindow)
        {
            WindowInteropHandle = new WindowInteropHelper(clipboardWindow).Handle;
            if (WindowInteropHandle == null)
            {
               /* throw new ArgumentException(
                    $"{nameof(clipboardWindow)} must be initialized before using the {nameof(WindowClipboardMonitor)}. Use the Window's OnSourceInitialized() handler if possible, or a later point in the window lifecycle."
                    , nameof(clipboardWindow));
                    */
            }
        }
        private void StartHandlingWin32Messages()
        {
            Win32InteropSource.AddHook(Win32InteropMessageHandler);
        }

        private void StopHandlingWin32Messages()
        {
            Win32InteropSource.RemoveHook(Win32InteropMessageHandler);
        }

        private void AddListenerForClipboardWin32Messages()
        {
            depends.AddClipboardFormatListener(WindowInteropHandle);
        }

        private void RemoveListenerForClipboardWin32Messages()
        {
            depends.RemoveClipboardFormatListener(WindowInteropHandle);
        }
        private IntPtr Win32InteropMessageHandler(IntPtr windowHandle, int messageCode, IntPtr wParam, IntPtr lParam, ref bool messageHandled)
        {
            if (messageCode == depends.ClipboardUpdateWindowMessageCode)
            {
                OnClipboardChanged();

                messageHandled = true;
                return depends.HandledClipboardUpdateReturnCode;
            }

            return depends.NoMessageHandledReturnCode;
        }
        private void OnClipboardChanged()
        {
            ProcessClipboardTextWithRetry();
        }

        private void ProcessClipboardTextWithRetry()
        {
            const int maxAttempts = 10;
            int currentAttemptNumber = 1;

            while (currentAttemptNumber <= maxAttempts)
            {
                try
                {
                    ProcessClipboardText();
                    return;
                }
                catch (COMException ex) when (ex.ErrorCode == depends.UnableToOpenClipboardComErrorCode)
                {
                    SleepUntilNextRetry(currentAttemptNumber);
                }
                currentAttemptNumber++;
            }
        }

        private void SleepUntilNextRetry(int currentAttemptNumber)
        {
            const int sleepDurationMilliseconds = 50;
            var timeUntilNextRetry = TimeSpan.FromMilliseconds(sleepDurationMilliseconds);
            Thread.Sleep(timeUntilNextRetry);
        }
        private void ProcessClipboardText()
        {


            ClipBoardModel result = new ClipBoardModel();
           
            if(Clipboard.ContainsFileDropList())
                {
                foreach(string item in Clipboard.GetFileDropList())
                {
                    result.filepath = item;
                    result.filetype = Formats.file;
                }
            }
            else if(Clipboard.ContainsText())
            {
                result.filepath = Clipboard.GetText();
                result.filetype = Formats.text;
            }
            else
            {
                string temp2;
                IDataObject temp4 = Clipboard.GetDataObject();
                var temp1 = temp4.GetFormats();
                foreach (var x in temp1)
                {
                    temp2 = x;
                }
                // var x = Clipboard.GetImage();
                result.filepath = "Invalid";
                result.filetype = "Invalid";
            }
     
          
            //  ClipboardFormat? format = null;

            //string xx = iData.GetDataPresent(DataFormats.);
            ClipboardTextChanged?.Invoke(this, result);
            //}
        }

        public void Dispose()
        {
            ReleaseResources();
            GC.SuppressFinalize(this);
        }

        protected virtual void ReleaseResources()
        {
            if (disposed)
            {
                return;
            }
            else
            {
                disposed = true;
            }

            RemoveListenerForClipboardWin32Messages();
            StopHandlingWin32Messages();

            Win32InteropSource = null;
            WindowInteropHandle = IntPtr.Zero;
        }

        ~ClipBoardFrame()
        {
            ReleaseResources();
        }
    }
}
