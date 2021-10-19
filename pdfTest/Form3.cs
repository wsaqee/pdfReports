using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pdfTest
{
    public partial class Form3 : Form
    {
        public string GetUsername()
        {
            return textBoxUsername.Text;
        }
        public string GetPassword()
        {
            return textBoxPass.Text;
        }
        public Form3()
        {
            InitializeComponent();
            // In your constructor or somewhere more suitable:
            Program.SendMessage(textBoxUsername.Handle, 0x1501, 1, "Korisnicko ime");
            Program.SendMessage(textBoxPass.Handle, 0x1501, 1, "Lozinka");
        }
    }
}
