using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDES
{

    public interface IExpandKey
    {
        public byte[][] GetKeys(byte[] key);
    }

    public interface IFeistelFunction
    {
        public byte[] FeistelFunction(byte[] block, byte[] keyi);
    }

    public interface ICrypto
    {
        public byte[] Encrypt(byte[] data);

        public byte[] Decrypt(byte[] data);
    }

}