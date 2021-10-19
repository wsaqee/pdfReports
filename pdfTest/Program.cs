using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace pdfTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        // Within your class or scoped in a more appropriate location:
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        static void Main()
        {

            //ReportGenerator.NewReport("Izvještaj o nekim grupama");
            //for (int i=1; i<15; i++)
            //{
            //    ReportGenerator.CreateNewTable("Grupa" + i, "Opis" + i);
            //    Random r = new Random(i);
            //    int ran = r.Next(2, 20);
            //    for (int j=1; j<ran; j++)
            //    {
            //        ReportGenerator.AddMember("Grupa" + i, "Sys Generik" + j, "usrname" + j, "peroperic@think.com", "Aktiviran");
            //    }
                
            //}
            //ReportGenerator.GenerateReport();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
