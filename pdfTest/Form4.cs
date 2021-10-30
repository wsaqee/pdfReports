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
        private AdAttributeFileRW AttributeCtrl;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //load attributes from path
            AttributeCtrl = new AdAttributeFileRW(Properties.Resources.adAttributesPath);

            //populate checkbox list from attribute list
            foreach (AdAttribute item in AttributeCtrl.AdAttributes)
            {
                checkedListAttributes.Items.Add(item.Name, item.Enabled);
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            //sync attribute list with checkbox list
            for (int i=0; i<checkedListAttributes.Items.Count; i++)
            {
                int indx = AttributeCtrl.AdAttributes.FindIndex(x => x.Name == checkedListAttributes.Items[i].ToString());
                if (indx != -1) AttributeCtrl.AdAttributes[i].Enabled = checkedListAttributes.GetItemChecked(i);
            }
            //write attributes to file
            AttributeCtrl.WriteFile();
            //close dialog
            this.DialogResult = DialogResult.OK;
        }
    }
}
