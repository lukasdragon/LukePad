using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedNotePad.Serialization
{
    [Serializable]
    class LPF
    {
        public System.Drawing.Font Font;
        public System.Drawing.Color Color;
        public Byte[] Content;
        public Byte[] verificationString = Guid.NewGuid().ToByteArray();    
    }
}
