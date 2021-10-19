using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pdfTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            // In your constructor or somewhere more suitable:
            Program.SendMessage(textBoxReportName.Handle, 0x1501, 1, "Unesi ime izvještaja.");
        }


        public string getText()
        {
            return textBoxReportName.Text;
        }
    }
}
