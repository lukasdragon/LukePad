using EncryptedNotePad.Serialization;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace EncryptedNotePad
{
    internal class FileManager
    {

        private static readonly byte[] Salt = new byte[] { 15, 30, 35, 40, 55, 68, 69, 80 };


        public string LoadFile(string path, string password, ref System.Drawing.Color TextColor, ref System.Drawing.Font TextFont)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            LPF obj = (LPF)formatter.Deserialize(stream);
            stream.Close();
            TextColor = obj.Color;
            TextFont = obj.Font;
            return DecryptString(obj.Content, CreateKey(password, Salt, 32));
        }


        public bool SaveFile(string path, string password, string text, System.Drawing.Color TextColor, System.Drawing.Font TextFont)
        {
            LPF obj = new LPF
            {
                Color = TextColor,
                Font = TextFont,
                Content = EncryptString(text, CreateKey(password, Salt, 32))
            };

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, obj);
            stream.Close();

            return false;
        }



        public static string EncryptString(string message, byte[] key)
        {
            var aes = new AesCryptoServiceProvider();
            var iv = aes.IV;
            using (var memStream = new System.IO.MemoryStream())
            {
                memStream.Write(iv, 0, iv.Length);  // Add the IV to the first 16 bytes of the encrypted value
                using (var cryptStream = new CryptoStream(memStream, aes.CreateEncryptor(key, aes.IV), CryptoStreamMode.Write))
                {
                    using (var writer = new System.IO.StreamWriter(cryptStream))
                    {
                        writer.Write(message);
                    }
                }
                var buf = memStream.ToArray();
                return Convert.ToBase64String(buf, 0, buf.Length);
            }
        }

        public static string DecryptString(string encryptedValue, byte[] key)
        {
            var bytes = Convert.FromBase64String(encryptedValue);
            var aes = new AesCryptoServiceProvider();
            using (var memStream = new System.IO.MemoryStream(bytes))
            {
                var iv = new byte[16];
                memStream.Read(iv, 0, 16);  // Pull the IV from the first 16 bytes of the encrypted value
                using (var cryptStream = new CryptoStream(memStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using (var reader = new System.IO.StreamReader(cryptStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        private static byte[] CreateKey(string password, byte[] salt, int keyBytes = 32)
        {
            const int Iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password, Salt, Iterations);
            return keyGenerator.GetBytes(keyBytes);
        }



    }
}
