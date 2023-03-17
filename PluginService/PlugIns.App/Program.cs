using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlugIns.App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PlugIns.Contract.MyPlugin> myPlugins = new List<Contract.MyPlugin>();

            const string path = @"C:\Users\Riesenhuhn\Desktop\VerteilteSysteme\VerteilteSystemeSS12023\PluginService\PlugIns.PlugIn\bin\Debug";
            var dlls = Directory.GetFiles(path, "*.dll");
            foreach (var dll in dlls)
            {
                var ass = Assembly.LoadFrom(dll);
                var plugins = ass.GetTypes().Where(w => typeof(PlugIns.Contract.MyPlugin).IsAssignableFrom(w));
                foreach (var plugin in plugins)
                {
                    Console.WriteLine(plugin);
                    myPlugins.Add(Activator.CreateInstance(plugin) as Contract.MyPlugin);
                }
                
            }

            Console.WriteLine("Text eingeben:");
            var txt = Console.ReadLine();


            foreach (var item in myPlugins)
            {
                Console.WriteLine(item.Do(txt));
            }

            Console.WriteLine("Enter to exit");
            Console.ReadLine();
        }
    }
}
