using System.Runtime.InteropServices;
using System.Timers;
namespace Schedule
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            //bool createdNew;
            //using (var mutex = new System.Threading.Mutex(true, "MyUniqueMutexName", out createdNew))
            //{
            //    if (createdNew)
            //    {
                    Application.Run(new Form1());
            //    }
            //    else
            //    {
            //        // ѕриложение уже запущено, активируем его окно
            //        ProcessCurrentInstance();
            //    }
            //}
        }

        private static void ProcessCurrentInstance()
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var processes = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (var process in processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    SetForegroundWindow(process.MainWindowHandle);
                    break;
                }
            }
        }
    }
}