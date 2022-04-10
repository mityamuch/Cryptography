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
        private byte[] _key;
        private Mode _mode;

        public DesСontext(byte[] key,Mode mode, params object[] options) 
        { 
            _key = key;
            _mode = mode;
        }

        public byte[] Encrypt(byte[] Indata, byte[] Outdata) 
        {
            if (Indata.Length % 8 != 0)
            {
                Indata.Append((byte)0x80);
                while (Indata.Length % 8 != 0)
                {
                    Indata.Append((byte)0);
                }
            }
            else
            {
                Indata.Append((byte)0x80);
                for(int i=0;i<7;i++)
                {
                    Indata.Append((byte)0);
                }
            }
            FeistelNetwork network = new FeistelNetwork(new KeyGenerator(), new FeistelFunc(), _key);
            byte[] block=new byte[8];
            int posinblock=0;
            int posinOutdata=0;
            for(int i = 0; i < Indata.Length; i++)
            {
                block[posinblock] = Indata[i];
                posinblock++;
                if (posinblock == 8)
                {
                    posinblock = 0;
                    block=network.Encrypt(block);
                    foreach(byte b in block)
                    {
                        Outdata[posinOutdata] = b;
                        posinOutdata++;
                    }
                }
            }







            return null;
        }

        public byte[] Decrypt(byte[] Indata,byte[] Outdata) 
        {
            return null;
        }
    }
}
