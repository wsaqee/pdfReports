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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string[] attributes_lines = System.IO.File.ReadAllLines(@"AdObjectAttributes.txt");

            foreach (string line in attributes_lines)
            {
                if (line != null && line.Length > 0)
                {
                    string [] args = line.Split('\t');
                    if (args != null && args.Count() == 2)
                    {
                        string name = args[0];
                        bool _checked = false;
                        if (args[1].ToLower().Equals("true")) _checked = true;

                        checkedListAttributes.Items.Add(name, _checked);
                    }
                    

                    
                }
                    

                 
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            StringBuilder str = new StringBuilder();
            for (int i=0; i < checkedListAttributes.Items.Count; i++)
            {
                str.AppendLine(checkedListAttributes.Items[i].ToString() + '\t' + checkedListAttributes.GetItemChecked(i).ToString());
            }
            System.IO.File.WriteAllText(@"AdObjectAttributes.txt", str.ToString());

            this.DialogResult = DialogResult.OK;
        }

        public List<string> getAttributes()
        {
            return checkedListAttributes.CheckedItems.OfType<string>().ToList();
        }
    }
}
