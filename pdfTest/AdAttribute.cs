using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfTest
{
    class AdAttribute
    {
        public string Name { get; }
        public bool Enabled { get; set; }
        public AdAttribute(string name, bool enabled) 
        {
            this.Name = name;
            this.Enabled = enabled;
        }
    }
}
