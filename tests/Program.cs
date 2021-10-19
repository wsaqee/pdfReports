using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string usrEmail = Console.ReadLine();
            if (usrEmail == null || usrEmail.Length == 0) return;
            string[] mailDivAt = usrEmail.Split('@');
            string[] mailNameDivDot = mailDivAt[0].Split('.');

            List<string> lines = new List<string>();
            foreach (string s in mailNameDivDot)
            {
                if (lines.Count == 0)
                {
                    lines.Add(s);
                    continue;
                }
                if (lines.Last().Length + s.Length > 30)
                    lines.Add("." + s);
                else
                    lines[lines.Count - 1] += "." + s;
            }

            if (lines.Last().Length + mailDivAt[1].Length > 30)
                lines.Add("@" + mailDivAt[1]);
            else
                lines[lines.Count - 1] += "@" + mailDivAt[1];

            foreach (string l in lines) Console.WriteLine(l);
            Console.ReadLine();
        }
    }
}
