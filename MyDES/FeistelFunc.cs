using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDES
{
    public class FeistelFunc:IFeistelFunction
    {

        public byte[] FeistelFunction(byte[] block,byte[] keyi)
        {
            var permutedBlock = AuxiliaryFunctions.Permutation(block,Constants.E);
            var xoredBlock = AuxiliaryFunctions.XOR(permutedBlock, keyi);
            var stransformedBlock = AuxiliaryFunctions.Substitution(xoredBlock);
            return AuxiliaryFunctions.Permutation(stransformedBlock, Constants.P);
        }
    }
}
