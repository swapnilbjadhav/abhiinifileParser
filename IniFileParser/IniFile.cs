﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IniFileParser
{
    public class IniFile
    {
        public List<Section> Sections { get; }

        public  string Path { get; set; }

        public IniFile(string filePath)
        {
            Path = filePath;
            Sections = new List<Section>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var section in Sections)
            {
                sb.AppendLine(section.ToString());
            }

            return sb.ToString();
        }

        public void Parse()
        {
            List<string> lines = File.ReadAllLines(Path).ToList();
            foreach (var line in lines)
            {

            }

        }
    }

    public class Section
    {
        public string Name { get; set; }

        public Dictionary<string, List<string>> KeyValuePairs { get; set; }

        public Section(string name)
        {
            Name = name;
            KeyValuePairs = new Dictionary<string, List<string>>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Section Name:- "+Name);
            foreach (var keyValuePair in KeyValuePairs)
            {
                sb.Append(keyValuePair.Key+" = ");
                foreach (string s in keyValuePair.Value)
                {
                    sb.Append(s + ",");
                }

                sb = new StringBuilder(sb.ToString().TrimEnd(','));
                sb.AppendLine();
            }

            return sb.ToString().TrimEnd(',');
        }
    }
}
