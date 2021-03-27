using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_Encryption
{
    public class UTF8
    {
        public List<string> EncodeUTF8(string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            int[] encbytes = bytes.Select(i => (int)i).ToArray();
            List<string> encoded = new List<string>();
            for(int i=0;i<encbytes.Length;i++)
            encoded.Add(Convert.ToString(encbytes[i]));
            return encoded;
        }

        public string DecodeUTF8(List<string> encbytes)
        {
           int[] b = encbytes.Select(x =>Int32.Parse(x)).ToArray();
            byte[] bytes=b.Select(x => (byte)x).ToArray();
            string decstr = Encoding.UTF8.GetString(bytes);
            return decstr;
        }
    }
}
