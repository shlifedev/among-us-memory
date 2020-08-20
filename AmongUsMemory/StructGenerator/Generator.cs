using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AmongUsMemory.StructGenerator
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
        public static void Generate(string path)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);

            foreach(var file in di.GetFiles())
            {
                XDocument  xml =XDocument.Load(file.FullName); //"D:\\test\\config.xml" == @"D:\test\config.xml" 
                var list = xml.Root.Elements().Reverse();
                List< Element > datas = new List<Element>();
                foreach(var m in list)
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

                Gen(file.Name.Split('.')[0], datas);
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
        static void Gen(string className, List<Element> data)
        {
            string fields = null;
            foreach(var em in data)
            {
                var field = $"[System.Runtime.InteropServices.FieldOffset({em.offset})]\tpublic {GetType(em.varType, em.displayMethod)} {em.desc};\n";
                fields += field;
            }


            string m = $"[System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]\npublic struct {className}{{\n{fields}}}";

            Console.WriteLine(m);
        }
    }
}
