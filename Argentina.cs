using System;
using System.Collections.Generic;
using System.Numerics;

namespace ITCLibrary
{
    public static class Argentina
    {
        public static long PreSequence(long m, long n)
        {
            List<long> preSerie = new List<long>() { 0, 1 };
            long preMod = 1;
            for (long k = 2; k < long.MaxValue; k++)
            {
                preMod = (preMod * 2) + (k % 2 == 0 ? -1 : 1);
                if (preMod >= m)
                {
                    preMod = preMod - m;
                }
                if (preMod == 0)
                {
                    break;
                }
                else
                {
                    preSerie.Add(preMod);
                }
            }
            long sequenceCount = preSerie.Count;
            long repeats = n / sequenceCount;
            int index = (int)(n - (sequenceCount * repeats));
            long result = preSerie[index - 1];
            return result;
        }

        public static long Sequence(long m, long n)
        {
            List<long> serie = new List<long>();
            BigInteger po = 1;
            long mod = 0;
            for (long k = 0; k < n; k++)
            {
                BigInteger ff = (po + (k % 2 == 0 ? -1 : 1)) / 3;
                mod = (long)(ff % m);
                serie.Add(mod);
                po = po * 2;
                Console.Write(string.Format("{0} ", mod));
            }
            return mod;
        }

        //Descubri que el algoritmo de las alpacas es el numero de Jacobsthal
        //https://en.wikipedia.org/wiki/Jacobsthal_number
        //Con esto solo no alcanzaba porque los numeros son gigantes (m y n)
        //El metodo Sequence(m,n) calcula el modulo del numero de jacobsthal, usa BigInteger para no hacer overflow
        //Pude notar que el modulo cumple con la siguiente ecuacion 
        // M(k,m) = 2M(k-1,m) - (-1^k)
        //Tuve que imprimir por consola la secuencia del modulo Sequence(m,n) para darme cuenta
        //cuando el m(k) = 0 la secuencia se repite, siempre la secuencia comienza en k=0
        //Sabiendo esto una vez que encuentro la longitud de la secuencia, el resultado es
        // jacobsthal(n) = M( mod(n, M(-,m).lenght), m ) 
    }
}
