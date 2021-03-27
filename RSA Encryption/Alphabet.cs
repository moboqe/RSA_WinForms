using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_Encryption
{
   class AlphabetEnc
    {
       public static char[] Alphabet()
        {

            string russym = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string latsym = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            string othersym = " .,<>?/|{}[]()-_=+*!@#$%^&*;№:'\"";
            string all = russym + latsym + numbers + othersym;
            char[] dict = all.ToCharArray();
            //  int len = russym.Length + latsym.Length + numbers.Length + othersym.Length;
            return dict;
        }
        public List<string> Encode(string text)
        {
            List<string> outmas = new List<string>();

            char[] codes = Alphabet();
            for (int i = 0; i < text.Length; i++)
            {
                outmas.Add(Convert.ToString(Array.IndexOf(codes, text[i])));
            }
            return outmas;
        }
        public string Decode(List<string> decryptedstring)
        {
            string text = "";
            char[] codes = Alphabet();
            for (int i = 0; i < decryptedstring.Count; i++)
            {
                text += codes[Convert.ToInt32(Convert.ToString(decryptedstring[i]))];
            }
            return text;
        }        
    }
}
