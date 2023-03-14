using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIns.PlugIn
{
    class GematriaCalc : PlugIns.Contract.MyPlugin
    {
        public string Do(string input)
        {
            Dictionary<char, int> cipher = new Dictionary<char, int> {
    {'a', 1}, {'b', 2}, {'c', 3}, {'d', 4},
    {'e', 5}, {'f', 6}, {'g', 7}, {'h', 8},
    {'i', 9}, {'j', 10}, {'k', 11}, {'l', 12},
    {'m', 13}, {'n', 14}, {'o', 15}, {'p', 16},
    {'q', 17}, {'r', 18}, {'s', 19}, {'t', 20},
    {'u', 21}, {'v', 22}, {'w', 23}, {'x', 24},
    {'y', 25}, {'z', 26}
};
            int result = 0;
            foreach (char c in input)
            {
                if (cipher.ContainsKey(c))
                {
                    result += cipher[c];
                }
            }
            return result.ToString();
        }
    }
}
