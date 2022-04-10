using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDES
{
    public class FeistelNetwork:ICrypto
    {
 
        private IExpandKey _key;
        private IFeistelFunction _function;
        private byte[][] _keys;

        public FeistelNetwork(IExpandKey k,IFeistelFunction f,byte[] key)
        {
            _key = k;
            _function = f;
            _keys = _key.GetKeys(key);
        }

        public byte[] Encrypt(byte[] data) 
        {
            data = AuxiliaryFunctions.Permutation(data, Constants.IP);
            var R =new byte[data.Length/2];
            
            for (int i = 4; i < 8; i++)
            {
                R[i-4]=data[i];
            }
            var L = data;
            for(int i = 0; i < 16; i++)
            {
                var extra=AuxiliaryFunctions.XOR(_function.FeistelFunction(R, _keys[i]),L);
                L = R;
                R = extra;
            }
            byte[] result = new byte[8];
            for(int i = 0; i < 8; i++)
            {
                if (i >= 4)
                {
                    result[i] = L[i];
                }
                else
                {
                    result[i] = R[i];
                }
            }
            result = AuxiliaryFunctions.Permutation(result, Constants.IPReverse);
            return result;
        }

        public byte[] Decrypt(byte[] data) 
        {
            data = AuxiliaryFunctions.Permutation(data, Constants.IP);
            var R = new byte[data.Length / 2];
            data.CopyTo(R, 4);
            var L = data;
            for (int i = 0; i < 16; i++)
            {
                var extra = AuxiliaryFunctions.XOR(_function.FeistelFunction(L, _keys[15-i]), R);
                R = L;
                L = extra;
            }
            byte[] result = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                if (i >= 4)
                {
                    result[i] = L[i];
                }
                else
                {
                    result[i] = R[i];
                }
            }
            result = AuxiliaryFunctions.Permutation(result, Constants.IPReverse);
            return result;
        }

    }
}
