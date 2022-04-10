using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDES
{
    public class KeyGenerator:IExpandKey
    {
        public byte[][] GetKeys(byte[] key)
        {
            var result = new byte[16][];
            var expandedKey = ExpandStartKey(key);
            var permutedKey = AuxiliaryFunctions.Permutation(expandedKey, Constants.KeyReplace);//C0
            var C = permutedKey;
            byte[] D = new byte[4];
            int j = 29;
            for (int i = 0; i < 28; i++)
            {
                AuxiliaryFunctions.SetBit(D, AuxiliaryFunctions.GetBit(permutedKey, j), i + 1);
                j++;
            }
            for (int i = 0; i < 16; i++)
            {
                C = Rotate(C, Constants.Shifts[i]);
                D = Rotate(D, Constants.Shifts[i]);
                var gluedkey=Glue(C, D);
                result[i] = AuxiliaryFunctions.Permutation(gluedkey,Constants.KeySelect);
            }
            return result;
        }
        private byte[] ExpandStartKey(byte[] key)
        {
            byte[] result=new byte[8];
            for(int i=0;i<8; i++)
            {
                int countone=0;
                for(int j = 0; j < 7; j++)
                {
                    var bit=AuxiliaryFunctions.GetBit(key, j+i*7+1);
                    if (bit)
                        countone++;
                    AuxiliaryFunctions.SetBit(result,bit,j+i*8+1);
                }
                if (countone % 2 == 1)
                {
                    AuxiliaryFunctions.SetBit(result, false,8*(i+1));
                }

                else
                {
                    AuxiliaryFunctions.SetBit(result, true, 8 * (i + 1));
                }
            }
            return result;
        }

        private byte[] Rotate(byte[] partkey,int rotatecount)
        {
            var result = new byte[4];
            for(int i = 0; i < 28; i++)
            {
                AuxiliaryFunctions.SetBit(result, AuxiliaryFunctions.GetBit(partkey, (i + rotatecount) % 28 + 1), i + 1);
            }
            return result;
        }

        private byte[] Glue(byte[] part1,byte[] part2)
        {
            var result=new byte[7];
            for(int i = 0; i < 56; i++)
            {
                if (i >= 28)
                {
                    AuxiliaryFunctions.SetBit(result, AuxiliaryFunctions.GetBit(part2, i-28+1), i + 1);
                }
                else
                {
                    AuxiliaryFunctions.SetBit(result , AuxiliaryFunctions.GetBit(part1, i+1), i + 1);
                }
            }
            return result;

        }
    }
}
