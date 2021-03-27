using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA_Encryption
{
    public partial class Form1 : Form
    {
        /*       
              byte=0 - not chosen
              byte=1 - UTF8
              byte=2 - Alphabet         
         */
        private static byte flag;
        private static string savedText;
        private RSA_Algorithm rsa;
        private UTF8 utf8;
        private AlphabetEnc alph;
        private List<string> enc1 = null;
        private List<string> enc2 =null;
        string []outUTF8;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            utf8 = new UTF8();
            alph = new AlphabetEnc();
            rsa = new RSA_Algorithm();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    flag = 1;
                    break;
                case 1:
                    flag = 2;
                    break;
                default:
                    Task.Run(() => { MessageBox.Show("Произошла ошибка!"); });
                    break;
            }
            /*if (comboBox1.SelectedIndex == 0)
            {
                flag = 1;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                flag = 2;
            }*/
        }


        private void richTextBox1_Leave(object sender, EventArgs e)
        {

            savedText = richTextBox1.Text;
            // MessageBox.Show(savedText);
        }

        private void LastText_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = savedText;
        }

        private void GetValues()
        {
            int p = 0;
            int q = 0;
            p = int.Parse(textBox1.Text);
            q = int.Parse(textBox2.Text);
            PrimeNumbers.P = (int)p;
            PrimeNumbers.Q = (int)q;
        }
        private List<string> EncryptionMethod(List<string> bytes)
        {
            List<string> encrypted = null;
            
            if (PrimeNumbers.IsPrime(int.Parse(textBox1.Text)) && PrimeNumbers.IsPrime(int.Parse(textBox2.Text)))
            {
                //MessageBox.Show("true");
                GetValues();
                textBox4.Text = Convert.ToString(RSA_Algorithm.NumberE(PrimeNumbers.P, PrimeNumbers.Q));
                textBox4.Visible = true;
                label5.Text = RSA_Algorithm.NumberN(PrimeNumbers.P, PrimeNumbers.Q).ToString();
                label5.Visible = true;
                label6.Text = RSA_Algorithm.NumberN(PrimeNumbers.P, PrimeNumbers.Q).ToString();
                label6.Visible = true;
                encrypted = RSA_Algorithm.Encrypt(bytes, PrimeNumbers.P, PrimeNumbers.Q);
            }
            else MessageBox.Show("Числа не являются простыми");
            return encrypted;
        }
        private List<string> DecryptionMethod(List<string> bytes)
        {
            GetValues();
            textBox3.Text = Convert.ToString(RSA_Algorithm.NumberD(PrimeNumbers.P, PrimeNumbers.Q));
            textBox3.Visible = true;
            List<string> decrypted = null;
            decrypted = RSA_Algorithm.Decrypt(bytes, PrimeNumbers.P, PrimeNumbers.Q);

            return decrypted;
        }
        private void Encrypt_Click(object sender, EventArgs e)
        {

            List<string> bytes;
            string codes;
            string text;
            switch (flag)
            {
                case 1:
                    text = "Hello, world";
                    text = richTextBox1.Text;
                    bytes = utf8.EncodeUTF8(text);
                   // outUTF8 = Array.ConvertAll(bytes, x => x.ToString());
                    codes = string.Join(Environment.NewLine, bytes);
                    //    Task.Run(() => { MessageBox.Show(text + "   \n" + codes); });                  
                    List<string> encodeddataUTF8 =EncryptionMethod(bytes);
                    enc1 = encodeddataUTF8;
                 //   Task.Run(() => { MessageBox.Show(PrimeNumbers.P + "   \n" + PrimeNumbers.Q); });
                    richTextBox2.Text = "";
                for (int i=0;i<encodeddataUTF8.Count;i++)
                    richTextBox2.Text = richTextBox2.Text+(Convert.ToString(encodeddataUTF8[i])); 
                    break;
                case 2:
                    text = "Привет, мир";
                    text = richTextBox1.Text;
                    bytes = alph.Encode(text);
                    codes = string.Join(Environment.NewLine, bytes);
                    //  Task.Run(() => { MessageBox.Show(text + "   \n" + codes); });
                    List<string> encodeddataalph = EncryptionMethod(bytes);
                    enc2 = encodeddataalph;
                    //Task.Run(() => { MessageBox.Show(PrimeNumbers.P + "   \n" + PrimeNumbers.Q); });
                    richTextBox2.Text = "";
                    for (int i = 0; i < encodeddataalph.Count; i++)
                        richTextBox2.Text = richTextBox2.Text + (Convert.ToString(encodeddataalph[i]));
                    break;
                default:
                    Task.Run(() => { MessageBox.Show("Произошла ошибка!"); });
                    break;
            }
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
           List<string> decodeddataUTF8=null;
            List<string> decodeddataalph = null;
            switch (flag)
            {
                case 1:
                    if (enc1.Count > 0)
                    {
                        decodeddataUTF8 = DecryptionMethod(enc1);
                        richTextBox2.Text = "";
                        richTextBox2.Text = utf8.DecodeUTF8(decodeddataUTF8);
                    }
                    else Task.Run(() => { MessageBox.Show("Произошла ошибка!"); });
                    break;
                case 2:
                    if (enc2.Count > 0)
                    {
                        decodeddataalph = DecryptionMethod(enc2);
                        richTextBox2.Text = "";
                        richTextBox2.Text = alph.Decode(decodeddataalph);
                    }
                    else Task.Run(() => { MessageBox.Show("Произошла ошибка!"); });
                    break;
                default:
                    Task.Run(() => { MessageBox.Show("Произошла ошибка!"); });
                    break;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
    }
}
