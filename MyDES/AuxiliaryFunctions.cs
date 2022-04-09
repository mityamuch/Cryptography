﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDES
{
    internal sealed class AuxiliaryFunctions
    {
        public static byte[] Permutation(byte[] block, int[] Pblock)
        {
            byte[] result = new byte[8];
            int curind = 0;
            foreach (int i in Pblock)
            {
                SetBit(result, GetBit(block, i), curind);
                curind++;
            }

            return result;
        }


        public static byte[] Substitution( byte[] block)//48bit on enter
        {
            byte[] result = new byte[4] {0,0,0,0};

            for(int i = 0; i < 8; i++) 
            {
                var bit1 = GetBit(block, i + 1) ? 10 : 0;
                var bit6 = GetBit(block, i + 6) ? 1 : 0;
                var row = bit1 | bit6;

                var bit2 = GetBit(block, i + 2) ? 1000 : 0;
                var bit3=GetBit(block, i + 3) ? 100 : 0;
                var bit4 = GetBit(block, i + 4) ? 10 : 0;
                var bit5= GetBit(block, i + 5) ? 1 : 0;
                var col= bit2 | bit3 | bit4 | bit5;

                int value = Constants.S[i,16*row+col];
                if (i % 2 == 0)
                {
                    result[i / 2] |=(byte) (value<<4);
                }
                else
                {
                    result[i / 2] |= (byte)(value);
                }
            }


            return result;
        }

        public static void SetBit(byte[] block,bool value,int pos) 
        {
            if(value)
                block[(pos-1)/8] |= (byte)(1 << (7- (pos-1)%8));
            else
                block[(pos-1/8)] &= (byte)(~(1 << (7- (pos-1)%8)));
        }

        public static bool GetBit(byte[] block,int pos)
        {
            return (block[(pos - 1) / 8] & (1 << (7 - (pos - 1) % 8))) != 0;
        }

    }
}
