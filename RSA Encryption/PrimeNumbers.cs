using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_Encryption
{
    public static class PrimeNumbers
    {
        public static int P { get; set; }
        public static int Q { get; set; }
       public static bool IsPrime(int n)
        {
            bool prime = true;
            if (n <= 2)
                prime = false;
            else
            {
                for (int i = 2; i <= Math.Sqrt(n); i++)
                {
                    if (n % i == 0)
                    {
                        prime = false;
                        break;
                    }
                }
            }
            return prime;
        }
    }
}
