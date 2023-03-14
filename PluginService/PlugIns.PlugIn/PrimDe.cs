using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIns.PlugIn
{
    class PrimDe
    {
        public Dictionary<int, int> PrimeFactors(int number)
        {
            var primeFactors = new Dictionary<int, int>();
            int factor = 2;

            while (number > 1)
            {
                if (number % factor == 0)
                {
                    number /= factor;
                    if (primeFactors.ContainsKey(factor))
                    {
                        primeFactors[factor]++;
                    }
                    else
                    {
                        primeFactors.Add(factor, 1);
                    }
                }
                else
                {
                    factor++;
                }
            }

            return primeFactors;
        }
    }
}
