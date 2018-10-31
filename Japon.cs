using System;
using System.Collections.Generic;
using System.Numerics;

namespace ITCLibrary
{
    public static class Japon
    {
        public static List<Tuple<BigInteger, BigInteger>> PredictCases(int k, int n)
        {
            List<Tuple<BigInteger, BigInteger>> res = new List<Tuple<BigInteger, BigInteger>>();
            int max = Math.Min(n, k);
            BigInteger last = 1;
            BigInteger lastPower = 1;
            for (int i = 1; i <= max; i++)
            {
                //last = last * ( k - (i - 1) / (k + 2 - i) );
                lastPower = lastPower * k;
                last = ( (last * k) - ((last * (i - 1)) / (k + 2 - i)) );                
                res.Add(new Tuple<BigInteger, BigInteger>(lastPower - last, lastPower));
            }
            return res;
        }

        public static Tuple<BigInteger, BigInteger> ReduceFraction (Tuple<BigInteger, BigInteger> fraction)
        {
            Tuple<BigInteger, BigInteger> res = new Tuple<BigInteger, BigInteger>(fraction.Item1, fraction.Item2);
            List<long> prime = new List<long>() { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
            foreach (long p in prime)
            {
                while (true)
                {
                    if (res.Item1 % p == 0 && res.Item2 % p == 0)
                    {
                        res = new Tuple<BigInteger, BigInteger>(res.Item1 / p, res.Item2 / p);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return res;
        }

        private static long FailedCases = 0;

        public static Tuple<long, long> CalcCases(int k, int n)
        {
            long totalCases = (long)Math.Pow(k, n);
            FailedCases = 0;
            Calc(k, n, new List<int>());
            return new Tuple<long, long>(FailedCases, totalCases);
        }

        private static void Calc(int k, int n, List<int> cases)
        {
            if (n > 0)
            {                
                for (int i = 0; i < k; i++)
                {
                    List<int> cas = new List<int>(cases);
                    cas.Add(i);
                    Calc(k, n - 1, cas);
                }
            }
            else
            {
                if (!TestHashTable(k, cases))
                {
                    FailedCases++;
                }                
            }
        }

        private static bool TestHashTable(int k, List<int> input)
        {            
            int n = input.Count;
            Dictionary<int,int> hash = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                int inp = input[i];
                bool isOk = false;
                for (int j = inp; j < k; j++)
                {
                    if (!hash.ContainsKey(j))
                    {
                        hash.Add(j, inp);
                        isOk = true;
                        break;
                    }
                }
                if (!isOk)
                {
                    return false;
                }
            }
            return true;
        }

        // 0 / 1
        // 0 / 5                //5		    5		    0
        // 1 / 25               //24		4.8		    0.2
        // 17 / 125             //108		4.5		    0.5
        // 193 / 625            //432		4		    1
        // 1829 / 3125          //1296		3		    2
        // 15625 / 15625        //0		    0		    5		(n-1)/(k+2-n)

        // 0 / 1
        // 0 / 6                //6         6           0
        // 1 / 36               //35		5.83333333	0.16666666
        // 20 / 216             //196		5.6		    0.4
        // 267 / 1296           //1029		5.25		0.75
        // 2974 / 7776          //4802		4.66666666	1.33333333
        // 29849 / 46656        //16807		3.5		    2.5
        // 279936 / 279936      //0		    0		    6		

        // 0 / 1
        // 0 / 7                //7	        7           0
        // 1 / 49               //48		6.85714285	0.14285715
        // 23 / 343             //320		6.66666666	0.33333333
        // 353 / 2401           //2048		6,4		    0.6
        // 4519 / 16807         //12288		6		    1
        // 52113 / 117649       //65536		5.33333333	1.66666666
        // 561399 / 823543      //262144	4		    3
        // 5764801 / 5764801    //0		    0		    7


        //Usando el metodo completo CalcCases obtube las fracciones sin reducir para 5, 6, 7
        //La primer columna es la resta entre el divisor - dividendo  (si f = (a/b)  => col1 = b-a)
        //La segunda columna es la division de col1(n)/col1(n-1)
        //La tercer columna es k-col2(n)
        //Encontre que la tercer columna puede ser calculada como (n-1)/(k+2-n)
        //De ahi fui calculando hacia atras
    }
}
