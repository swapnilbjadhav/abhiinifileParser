using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IniFileParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "./Content/SampleIniFile.ini";
            if (!File.Exists(filePath))
            {
                throw  new Exception("ini file doesn't exist");
            }
            IniFile iniFile = new IniFile(filePath);
            iniFile.Parse();
            Console.WriteLine(iniFile.ToString());
            Console.ReadLine();

        }
    }
}
