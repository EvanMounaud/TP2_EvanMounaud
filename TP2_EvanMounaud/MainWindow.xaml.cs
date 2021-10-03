using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TP2_EvanMounaud
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private static int Mod(int a, int b)
        {
            return ((a % b) + b) % b;
        }

        private static string Cipher(string input, string key, bool encipher)
        {
            bool error = false;
            bool error2 = false;
            for (int i = 0; i < key.Length; ++i)
            {
                if (!char.IsLetter(key[i]))
                {
                    error = true;
                }
            }

            if (error)
            {
                string message = "Your key can contain only letters";
                string title = "Error";
                _ = MessageBox.Show(message, title);
                return null;
            }

            string output = string.Empty;
            int nonAlphaCharCount = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (char.IsLetter(input[i]))
                {
                    bool cIsUpper = char.IsUpper(input[i]);
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;
                    int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                    k = encipher ? k : -k;
                    char ch = (char)(Mod(input[i] + k - offset, 26) + offset);
                    output += ch;
                }
                else
                {
                    output += input[i];
                    ++nonAlphaCharCount;
                    error2 = true;
                }
            }
            if (error2)
            {
                string message = "The Vigenere Cipher is not changing different characters from letters";
                string title = "Error";
                _ = MessageBox.Show(message, title);
            }

            return output;
        }

        public static string Encipher(string input, string key)
        {
            return Cipher(input, key, true);
        }

        public static string Decipher(string input, string key)
        {
            return Cipher(input, key, false);
        }

        private void Encrypt_OnClick (object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder(EncryptionTextBox.Text);
            string key = KeyTextBox.Text;
            string r = s.ToString();
            ResultEncryptionTextBox.Text = Convert.ToString(Encipher(r, key));


        }
        private void Decrypt_OnClick(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder(DecryptionTextBox.Text);
            string key = KeyTextBox.Text;
            string r = s.ToString();
            ResultDecryptionTextBox.Text = Convert.ToString(Decipher(r, key));


        }

    }
}
