using System;
using System.IO;
using System.Text;

namespace ITCLibrary
{
    public static class Guyana
    {
        public static void DoWork()
        {
            Dowload();
            byte[] file = File.ReadAllBytes("input.pdf");
            string body = Encoding.UTF7.GetString(file);
            string head = body.Substring(0, 5);
            for (int i = 0; i < 256; i++)
            {
                string convertedHead = XorText(head, i);
                if (convertedHead.StartsWith(@"%PDF"))
                {
                    byte[] cry = Crypt(file, (uint)i);
                    File.WriteAllBytes(string.Format("output_{0}.pdf", i), cry);
                }
            }
        }

        private static string XorText(string text, int key)
        {
            string newText = "";
            for (int i = 0; i < text.Length; i++)
            {
                int charValue = Convert.ToInt32(text[i]);
                charValue ^= key;
                newText += char.ConvertFromUtf32(charValue);
            }
            return newText;
        }

        private static byte[] Crypt(byte[] data, uint k)
        {
            byte[] result = new byte[data.Length];
            for (int c = 0; c < data.Length; c++)
            {
                result[c] = (byte)((uint)data[c] ^ k);
            }
            return result;
        }

        private static void Dowload()
        {
            byte[] response = new System.Net.WebClient().DownloadData(@"https://s3.amazonaws.com/it.challenge.18/secreto.pdf");
            File.WriteAllBytes("input.pdf", response);
        }

        //Es una encriptacion XOR por el nombre de los personajes del problema Ximena, Oscar, Raul
        //Use una desencriptacion de texto (XorText) para ver si con alguna key la cabecera del documento es "%PDF"
        //Luego desencripte los bytes directamente con (Crypt) porque guardar el archivo de texto desencriptado
    }
}
