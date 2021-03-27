using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

namespace RSA_Encryption
{
   
    static class StringExtensions
    {
        public static string Reverse(this string input)
        {
            return new string(input.ToCharArray().Reverse().ToArray());
        }
    }
   public class RSA_Algorithm
    {       
         public static int Mul(int x, int e, int n)
        {
            string BINnumber = Convert.ToString(e, 2);
            int length = BINnumber.Length;
            string reversebits = BINnumber.Reverse();

            int[] binnumber = new int[length];

            for (int i = 0; i < length; i++)
            {
                binnumber[i] = Convert.ToInt32(reversebits.Substring(i, 1));
            }


            int[] array = new int[length];
            array[0] = x;
            for (int i = 1; i < length; i++)
            {
                array[i] = array[i - 1] * array[i - 1] % n;

            }

            int p = 1;
            for (int i = 0; i < length; i++)
            {
                if (binnumber[i] > 0)
                    p *= Convert.ToInt32(Math.Pow(array[i], binnumber[i]));
            }

            int Result = p % n;
           
            return Result;
        }
        static int EulerFunc(int p, int q)
        {
            
            return (p - 1) * (q - 1);
        }
       public static int NumberN(int p, int q)
        {
            return p * q;
        }
        private static int Gcd(int a, int b, out int x, out int y)
        {
            if (b < a)
            {
                var t = a;
                a = b;
                b = t;
            }

            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            int gcd = Gcd(b % a, a, out x, out y);

            int newY = x;
            int newX = y - (b / a) * x;

            x = newX;
            y = newY;
            return gcd;
        }
        private static bool IsCoprime(int a, int b)
        {
            return a == b ? a == 1 : a > b ? IsCoprime(a - b, b) : IsCoprime(b - a, a);
        }
    internal   static int NumberE(int p, int q)
        {
            int phi = EulerFunc(p,q);
            int e = 0;
            for (int i = 10; i < 31; i++)
            {
                if (IsCoprime(phi, i))
                {
                    e = i;
                    break;
                }
            }
            return e;
        }

        internal static int NumberD(int p, int q) //
        {


            int phi = EulerFunc(p,  q);
            int e = NumberE(p,  q);
            int d;
            int k;

            int nod = Gcd(e, phi, out  d, out  k);


            return (d + phi);
        }


        string InputText()
        {
            Console.WriteLine("Введите текст");
            return Console.ReadLine();
        }


        public static List<string> Encrypt(List<string> encodedstring, int p, int q)
        {
            int n = NumberN( p,  q);
            int e = NumberE( p,  q);
            List<string> encryptedstr = new List<string>();
            BigInteger tmp = new BigInteger();
            for (int i = 0; i < encodedstring.Count; i++)
            {
               tmp = new BigInteger(Convert.ToUInt64(encodedstring[i]));
                tmp = BigInteger.Pow(tmp, e);
                BigInteger n_ = new BigInteger(n);
                tmp = tmp % n_;
                encryptedstr.Add(tmp.ToString());
            }
            return encryptedstr;
        }

        public static List<string> Decrypt(List<string> encryptedstring, int p, int q)
        {
            int n = NumberN( p,  q);
            int d = NumberD(p,  q);
            BigInteger tmp ;
            List<string> decryptedstr = new List<string>();
            for (int i = 0; i < encryptedstring.Count; i++)
            {
                tmp = new BigInteger(Convert.ToUInt64(encryptedstring[i]));
                tmp = BigInteger.Pow(tmp, d);
                BigInteger n_ = new BigInteger(n);
                tmp = tmp % n_;
                decryptedstr.Add(tmp.ToString());
            }
            return decryptedstr;
        }
    }
}