using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITCLibrary
{
    public static class Alemania
    {
        //1,20,449,6792,62606,331447,1003694,1736807

        private static List<string> Resolved = new List<string>();

        private static object LockObject = new object();

        public static long CountAll()
        {
            long bigCount = 0;
            for (int i = 0; i <= 7; i++)
            {
                bigCount += CountValidCombos(i);
            }
            bigCount = bigCount * 2;
            return bigCount;
        }

        public static long CountValidCombos(int sum)
        {
            Resolved = new List<string>();
            long totalCount = 0;
            List<byte[]> combos = GetLineCombos(sum);
            int comboCount = combos.Count;
            for (byte i0 = 0; i0 < comboCount; i0++)
            {
                Console.WriteLine("{0}/{1}", i0 + 1, comboCount);
                Parallel.For(0, comboCount,
                    i1 =>
                    {
                        List<byte[]> pcombos;
                        byte pi0;
                        lock (LockObject)
                        {
                            pcombos = new List<byte[]>(combos);
                            pi0 = i0;
                        }
                        int subTotalCount = 0;
                        for (byte i2 = 0; i2 < comboCount; i2++)
                        {
                            for (byte i3 = 0; i3 < comboCount; i3++)
                            {
                                for (byte i4 = 0; i4 < comboCount; i4++)
                                {
                                    byte[,] matrix = new byte[5, 5];
                                    for (int i = 0; i < 5; i++)
                                    {
                                        matrix[i, 0] = pcombos[pi0][i];
                                        matrix[i, 1] = pcombos[i1][i];
                                        matrix[i, 2] = pcombos[i2][i];
                                        matrix[i, 3] = pcombos[i3][i];
                                        matrix[i, 4] = pcombos[i4][i];
                                    }
                                    if (ValidateCombo(matrix, sum))
                                    {
                                        subTotalCount++;
                                    }
                                }
                            }
                        }
                        lock (LockObject)
                        {
                            totalCount += subTotalCount;
                        }
                    }
                );
            }
            return totalCount;
        }

        private static bool ValidateCombo(byte[,] matrix, int sum)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!(matrix[i, 0] + matrix[i, 1] + matrix[i, 2] + matrix[i, 3] + matrix[i, 4] == sum))
                {
                    return false;
                }
            }
            if (!(matrix[0, 0] + matrix[1, 1] + matrix[2, 2] + matrix[3, 3] + matrix[4, 4] == sum))
            {
                return false;
            }
            if (!(matrix[4, 0] + matrix[3, 1] + matrix[2, 2] + matrix[1, 3] + matrix[0, 4] == sum))
            {
                return false;
            }
            return true;   
        }

        private static string Show(byte[,] matrix)
        {
            return string.Format(
@"
{0}{1}{2}{3}{4}
{5}{6}{7}{8}{9}
{10}{11}{12}{13}{14}
{15}{16}{17}{18}{19}
{20}{21}{22}{23}{24}
",
matrix[0, 0],
matrix[0, 1],
matrix[0, 2],
matrix[0, 3],
matrix[0, 4],
matrix[1, 0],
matrix[1, 1],
matrix[1, 2],
matrix[1, 3],
matrix[1, 4],
matrix[2, 0],
matrix[2, 1],
matrix[2, 2],
matrix[2, 3],
matrix[2, 4],
matrix[3, 0],
matrix[3, 1],
matrix[3, 2],
matrix[3, 3],
matrix[3, 4],
matrix[4, 0],
matrix[4, 1],
matrix[4, 2],
matrix[4, 3],
matrix[4, 4]
                );
        }

        private static List<byte[]> GetLineCombos(int sum)
        {
            List<byte[]> trueCombos = new List<byte[]>();
            List<string> combos = new List<string>();
            for (byte i0 = 0; i0 < 4; i0++)
            {
                for (byte i1 = 0; i1 < 4; i1++)
                {
                    for (byte i2 = 0; i2 < 4; i2++)
                    {
                        for (byte i3 = 0; i3 < 4; i3++)
                        {
                            for (byte i4 = 0; i4 < 4; i4++)
                            {
                                string combo = string.Format("{0}{1}{2}{3}{4}", i0, i1, i2, i3, i4);
                                if (!combos.Contains(combo))
                                {
                                    if (i0 + i1 + i2 + i3 + i4 == sum)
                                    {
                                        combos.Add(combo);
                                        trueCombos.Add(new byte[] { i0, i1, i2, i3, i4 });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return trueCombos;
        }
    }
}
