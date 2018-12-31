using EncryptedNotePad.Serialization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace EncryptedNotePad
{
    internal class FileManager
    {




        public string LoadFile(string path, string password, ref System.Drawing.Color TextColor, ref System.Drawing.Font TextFont)
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);


            LPF obj = (LPF)formatter.Deserialize(compressionStream);

            compressionStream.Close();
            fileStream.Close();

            //    decompressionStream.Close();
            TextColor = obj.Color;
            TextFont = obj.Font;
            return Encoding.UTF8.GetString(AESThenHMAC.SimpleDecryptWithPassword(obj.Content, password, 16));
        }


        private static string LoadCompressedFile(string filename)
        {
            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
            StreamReader reader = new StreamReader(compressionStream);
            string data = reader.ReadToEnd();
            reader.Close();
            return data;
        }


        public bool SaveFile(string path, string password, string text, System.Drawing.Color TextColor, System.Drawing.Font TextFont)
        {
            LPF obj = new LPF
            {
                Color = TextColor,
                Font = TextFont
            };
            obj.Content = AESThenHMAC.SimpleEncryptWithPassword(Encoding.UTF8.GetBytes(text), password, obj.verificationString);
                                          
            IFormatter formatter = new BinaryFormatter();

            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            GZipStream compressionStream = new GZipStream(fileStream,CompressionMode.Compress);
            formatter.Serialize(compressionStream, obj);

            compressionStream.Close();
            fileStream.Close();
            
            //   compressionStream.Close();

            return true;
        }

        private static void SaveCompressedFile(string filename, string data)
        {
            FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Compress);
            StreamWriter writer = new StreamWriter(compressionStream);
            writer.Write(data);
            writer.Close();
        }

       




    }
}
