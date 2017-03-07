using QualityControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //String thisprocessname = Process.GetCurrentProcess().ProcessName;

            //if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
            //{
            //    MessageBox.Show("Программа уже запущена", "Оповещение");
            //    return;
            //}


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
