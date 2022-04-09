using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDES
{
    enum Mode{
        ECB,
        CBC,
        CFB,
        OFB,
        CTR,
        RD,
        RDH
    };

    internal class DesСontext
    {
        public DesСontext(byte[] key,Mode mode, params object[] options) { }

        public byte[] Encrypt(byte[] Indata, byte[] Outdata) 
        {
            return null;
        }

        public byte[] Decrypt(byte[] Indata,byte[] Outdata) 
        {
            return null;
        }
    }
}
