using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyDES.CipherСontext;

namespace MyDES
{

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class DES : ICrypto
    {
        private readonly FeistelNetwork _network;

        public DES(byte[] key, params object[] options)
        {
            _network = new FeistelNetwork(new KeyGenerator(), new FeistelFunc(), key);
        }

       
        public byte[] Encrypt(byte[] data)
        {
            var permdata = AuxiliaryFunctions.Permutation(data, Constants.IP);
            var result = _network.Encrypt(permdata);
            result = AuxiliaryFunctions.Permutation(result, Constants.IPReverse);
            return result;
        }
        public byte[] Decrypt(byte[] data)
        {
            var permdata = AuxiliaryFunctions.Permutation(data, Constants.IP);
            var result= _network.Decrypt(permdata);
            result = AuxiliaryFunctions.Permutation(result, Constants.IPReverse);
            return result;
        }
    }
}
