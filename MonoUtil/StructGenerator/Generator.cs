using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace HamsterCheese.StructGenerator
{
    public class Element
    {
        public int offset;
        public string varType;
        public int sizeOf;
        public string desc;
        public string displayMethod;
    }
    public static class Generator
    {
        public static void Generate(string path, string output)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
            if (di.Exists)
            {
                foreach (var file in di.GetFiles())
                {
                    XDocument  xml =XDocument.Load(file.FullName);  
                    var list = xml.Root.Elements().Reverse();
                    List< Element > datas = new List<Element>();
                    foreach (var m in list)
                    {
                        Element em = new Element();
                        em.offset = int.Parse(m.Attribute("Offset").Value);
                        em.varType = m.Attribute("Vartype").Value;
                        em.sizeOf = int.Parse(m.Attribute("Bytesize").Value);
                        em.desc = m.Attribute("Description").Value;
                        em.displayMethod = m.Attribute("DisplayMethod").Value;
                        datas.Add(em);
                    }

                    datas = datas.OrderBy(x => x.offset).ToList();

                    Gen(file.Name.Split('.')[0], datas, output);
                }
            }
        }

        static string GetType(string varType, string dm)
        {
            if (varType == "Pointer") return "IntPtr";
            if (varType == "4 Bytes")
            {
                if (dm == "Unsigned Integer") return "uint";
                if (dm == "Integer") return "int";
            }
            if (varType == "Float")
            {
                if (dm == "Unsigned Integer") return "float";
            }
            if (varType == "Byte")
            {
                if (dm == "Unsigned Integer") return "byte";
            }
            return varType.ToLower();
        }

        static string ValidFieldName(string n)
        {
            if(n.Contains("<") || n.Contains(">"))
            {
                n = n.Replace("<", null);
                n = n.Replace(">", null);
                n = n.Replace("k__BackingField", null);
            }
      
            return n;
        }
        static void Gen(string className, List<Element> data, string output = null)
        {
            string fields = "";
            foreach(var em in data)
            {
                var field = $"[System.Runtime.InteropServices.FieldOffset({em.offset})]\tpublic {GetType(em.varType, em.displayMethod)} {ValidFieldName(em.desc)};\n";
                fields += field;
            }


            string m = $"using System; \n using System.Runtime.InteropServices;\n\n [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]\npublic struct {className}{{\n{fields}}}";
            if(output != null)
            {
                System.IO.File.WriteAllText(output +$@"\{className}.cs", m);
            }
            Console.WriteLine(m);
        }
    }
}
