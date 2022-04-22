using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDES
{
    
    public partial class CipherСontext
    {

        #region Nested

        public enum Mode
        {
            ECB,
            CBC,
            CFB,
            OFB,
            CTR,
            RD,
            RDH
        };

        #endregion

        private readonly ICrypto _crypto;
        private readonly Mode _mode;
        public CipherСontext(ICrypto crypto, Mode mode = Mode.ECB) 
        {
            _crypto = crypto ?? throw new ArgumentNullException(nameof(crypto));
            _mode = mode;
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

            // TODO: Parallel!!!, ThreadPool, Thread

            // TODO: почитай про Task.WhenAll
            // TODO: Task.Run, Task.Factory.StartNew

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

                                block = _crypto.Encrypt(block);

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
                                block = _crypto.Decrypt(block);
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
            for (i = data.Length - 1; i >= 0; i--)
            {
                if (data[i] == (byte)0x80)
                {
                    break;
                }
            }

            if (i == -1)
            {
                return data;
            }
            
            byte[] outdata = new byte[i];
            for (int j = 0; j < i; j++)
            {
                outdata[j] = data[j];
            }

            return outdata;
        }

    }
}
