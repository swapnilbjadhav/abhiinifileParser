using System;
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
            Section section = new Section("");
            List<string> sectionValue = new List<string>();
            string sectionData = "";
            string sectionKey = "";

            List<string> lines = File.ReadAllLines(Path).ToList();
            foreach (var line in lines)
            {
                if ((line.TrimStart().TrimEnd().StartsWith("#")) || (line.Trim() == ""))
                {
                    //do nothing
                }
                else
                {
                    if (line.StartsWith("[") || line.EndsWith("]"))
                    {
                        //put section here
                        sectionData = line.Substring(0, line.Length);
                        section = new Section(sectionData);
                    }
                    else
                    {
                        //key values
                        string[] keyValue = null;
                        keyValue = line.Split(new char[] { '=' }, 2);

                        if (sectionData == null)
                        {
                            sectionData = "ROOT";
                        }
                        sectionKey = keyValue[0];
                        sectionValue.Add(keyValue[1]);

                        if (!section.KeyValuePairs.ContainsKey(sectionKey))
                        {
                            sectionValue.Clear();
                            sectionValue.Add(keyValue[1]);
                            section.KeyValuePairs.Add(sectionKey, sectionValue.ToList());
                        }
                        else
                        {
                            section.KeyValuePairs.Remove(sectionKey);
                            section.KeyValuePairs.Add(sectionKey, sectionValue.ToList());
                        }
                    }
                }
                if (!this.Sections.Contains(section))
                {
                    this.Sections.Add(section);
                }
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
