using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfTest
{
    class AdAttributeFileRW
    {
        private List<AdAttribute> adAttributes;
        private string path;
        public List<AdAttribute> AdAttributes { get { return adAttributes; } }
        public AdAttributeFileRW(string path)
        {
            this.path = path;
            adAttributes = new List<AdAttribute>();
            ReadFile();
        }

        public void ReadFile()
        {
            adAttributes.Clear();
            string[] attributes_lines = System.IO.File.ReadAllLines(path);
            foreach (string line in attributes_lines)
            {
                if (line != null && line.Length > 0)
                {
                    string[] args = line.Split('\t');
                    if (args != null && args.Count() == 2)
                    {
                        string name = args[0];
                        bool enabled = false;
                        if (args[1].ToLower().Equals("true")) enabled = true;

                        adAttributes.Add(new AdAttribute(name, enabled));
                    }
                }
            }
        }

        public void WriteFile()
        {
            StringBuilder str = new StringBuilder();
            foreach (AdAttribute item in AdAttributes)
            {
                str.AppendLine(item.Name + '\t' + item.Enabled.ToString());
            }
            System.IO.File.WriteAllText(path, str.ToString());

            //reload list from file
            ReadFile();
        }
    }
}
