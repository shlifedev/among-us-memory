using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoUtil
{
    class Program
    {
        static void Main(string[] args)
        { 
            if(args.Length != 0)
            {
                Console.WriteLine(args[0]);
                HamsterCheese.StructGenerator.Generator.Generate(args[0], args[1]);
                return;
            }
            Console.WriteLine(args[0]);
                HamsterCheese.StructGenerator.Generator.Generate(@"C:\Users\shlif\OneDrive\Documents\GitHub\AmongUsMemory\AmongUsMemory\XmlStructs", null); 

                System.Threading.Thread.Sleep(99999);
        }
    }
}
