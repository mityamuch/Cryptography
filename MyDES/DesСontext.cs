using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDES
{
    
    public enum Mode{
        ECB,
        CBC,
        CFB,
        OFB,
        CTR,
        RD,
        RDH
    };
    
    public class DesСontext
    {
        private byte[] _key;
        private Mode _mode;
        private FeistelNetwork _network;

        public DesСontext(byte[] key,Mode mode, params object[] options) 
        { 
            _key = key;
            _mode = mode;
            _network = new FeistelNetwork(new KeyGenerator(), new FeistelFunc(), _key);
        }

        public byte[] Encrypt(byte[] Indata) 
        {


            var eIndata = new byte[Indata.Length + 8- (Indata.Length % 8)];
            byte[] Outdata = new byte[eIndata.Length];
            int lastind=0;
            for(lastind = 0; lastind < Indata.Length; lastind++)
            {
                eIndata[lastind] = Indata[lastind]; 
            }
            eIndata[lastind]=(byte)0x80;

            switch (_mode)
            {
                case Mode.ECB:
                    {
                        byte[] block = new byte[8];
                        int posinblock = 0;
                        int posinOutData = 0;
                        for (int i = 0; i < eIndata.Length; i++)
                        {
                            block[posinblock] = eIndata[i];
                            posinblock++;
                            if (posinblock == 8)
                            {
                                posinblock = 0;
                                block = _network.Encrypt(block);
                                foreach (byte b in block)
                                {
                                    Outdata[posinOutData]=b;
                                    posinOutData++;

                                }
                            }
                        }
                    }
                    break;
                case Mode.CBC:
                    break;
                case Mode.CFB:
                    break;
                case Mode.OFB:
                    break;
                case Mode.CTR:
                    break;
                case Mode.RD:
                    break;
                case Mode.RDH:
                    break;
                default:
                    break;
            }

           
            







            return Outdata;
        }

        public byte[] Decrypt(byte[] Indata) 
        {
            byte[] Outdata = new byte[Indata.Length];
            switch (_mode)
            {
                case Mode.ECB:
                    {
                        byte[] block = new byte[8];
                        int posinblock = 0;
                        int posinoutdata = 0;

                        for (int i = 0; i < Indata.Length; i++)
                        {
                            block[posinblock] = Indata[i];
                            posinblock++;
                            if (posinblock == 8)
                            {
                                posinblock = 0;
                                block = _network.Decrypt(block);
                                foreach (byte b in block)
                                {
                                    Outdata[posinoutdata++] = b;
                                }

                            }
                            
                        }
                    }
                    break;
                case Mode.CBC:
                    break;
                case Mode.CFB:
                    break;
                case Mode.OFB:
                    break;
                case Mode.CTR:
                    break;
                case Mode.RD:
                    break;
                case Mode.RDH:
                    break;
                default:
                    break;
            }


            return CutTail(Outdata);
        }

        public byte[] CutTail(byte[] data)
        {
           
            int i;
            for ( i= data.Length-1; i >=0; i--)
            {
                if(data[i] == (byte)0x80)
                {
                    
                    break;
                }
            }
            byte[] outdata = new byte[i];
            for (int j = 0; j < i;j++)
                outdata[j] = data[j];
            return outdata;
        }

    }
}
