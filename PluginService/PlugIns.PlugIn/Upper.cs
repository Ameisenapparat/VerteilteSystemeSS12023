using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIns.PlugIn
{
    public class Upper : PlugIns.Contract.MyPlugin
    {
        public string Do(string s)
        {
            return s.ToUpper();
        }
    }
}
