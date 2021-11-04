using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfTest
{
    class ReportUser
    {
        public string DisplayName { get; set; }
        public string SAMAccountName { get; set; }
        public string Mail { get; set; }
        public bool Enabled { get; set; }
        public string Department { get; set; }

        public ReportUser()
        {

        }
    }
}
