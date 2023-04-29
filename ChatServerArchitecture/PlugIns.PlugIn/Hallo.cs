using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIns.PlugIn
{
    public class Hallo : PlugIns.Contract.MyPlugin
    {
        public string Do(string s)
        {
            return $"Hallo {s}!";
        }
    }
}
