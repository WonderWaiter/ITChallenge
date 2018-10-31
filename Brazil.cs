using System.Collections.Specialized;

namespace ITCLibrary
{
    public class Brazil
    {
        public static long CalcCombinations()
        {
            long count = 0;
            for (int i = int.MinValue; i < int.MaxValue; i++)
            {
                if (CheckIfValid(i))
                {
                    count++;
                }
            }
            return count;
        }

        private static bool CheckIfValid(int bv)
        {
            BitVector32 b = new BitVector32(bv);
            for (int i = 0; i < 32; i++)
            {
                if (GetBit(b, i) == true)
                {
                    if (i < 30 && GetBit(b, i + 1) == true && GetBit(b, i + 2) == true)
                    {
                        i = i + 2;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool GetBit(BitVector32 b, int pos)
        {
            return b[1 << pos];
        }

        //Calcule todas las combinaciones posibles para meter cubos de 3x3 en la pared
        //Esto da el resultado final porque los paneles no se pueden encimar ni nada raro
    }
}
