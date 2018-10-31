using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace ITCLibrary
{
    public static class Rumania
    {
        //-----BEGIN RSA PUBLIC KEY-----
        //MCgCIQCKkJ7C1rP6I1dyoIO/Hm3Tob+rgB6oMVMGVsRpFmQ7lQIDAQAB
        //-----END RSA PUBLIC KEY-----

        // (https://8gwifi.org/PemParserFunctions.jsp)

        //Algo RSA
        //Format X.509
        // ASN1 Dump
        //RSA Public Key[40:dc:a8:a1:af:2b:d1:c2:d6:f0:c3:a0:25:94:bc:3a:c6:97:b1:a8]

        //    modulus: 8a909ec2d6b3fa235772a083bf1e6dd3a1bfab801ea831530656c46916643b95
        //    public exponent: 10001 	(65537)		

        public static void Test1()
        {
            BigInteger n = 3233;
            BigInteger e = 17;
            BigInteger p = 61;

            string stringInput = "hola";
            byte[] bb = Encoding.UTF8.GetBytes(stringInput);
            List<byte> bl = new List<byte>();
            for (int i = 0; i < bb.Length; i++)
            {
                bl.AddRange(CopyAndReverse(Encrypt(new BigInteger(CopyAndReverse(bb[i])), n, e).ToByteArray()));
            }
            File.WriteAllBytes("hola.txt", bl.ToArray());


            Decode(n, e, p, "hola.txt", 2, 2);
        }

        public static void Work()
        {
            BigInteger e = 0X10001;
            BigInteger p = BigInteger.Parse("1094782941871623486260250734009229761");
            BigInteger p16 = 16;
            for (int i = 0; i < 15; i++)
            {
                p16 = p16 * 16;
            }
            BigInteger n =
                (ulong)0X8a909ec2d6b3fa23 * p16 * p16 * p16 +
                (ulong)0X5772a083bf1e6dd3 * p16 * p16 +
                (ulong)0Xa1bfab801ea83153 * p16 +
                (ulong)0X0656c46916643b95;


            Decode(n, e, p, "data", 32, 32);
        }

        public static void Decode(BigInteger n, BigInteger e, BigInteger p, string inputPath, int range1, int range2)
        {
            BigInteger q = n / p;
            BigInteger totient = (p - 1) * (q - 1);
            BigInteger d = 0;
            for (long k = 0; k < long.MaxValue; k++)
            {
                BigInteger div = 1 + k * totient;
                if (div % e == 0)
                {
                    d = div / e;
                    break;
                }
            }
            byte[] bList = File.ReadAllBytes(inputPath);
            Directory.CreateDirectory("RSA");
            for (int lt = range1; lt <= range2; lt++)
            {
                List<byte> bytes = new List<byte>();
                for (int i = 0; i < bList.Length - (lt - 1); i = i + lt)
                {
                    List<byte> sub = new List<byte>();
                    for (int j = 0; j < lt; j++)
                    {
                        sub.Add(bList[i + j]);
                    }
                    bytes.AddRange(CopyAndReverse(Decrypt(new BigInteger(CopyAndReverse(sub.ToArray())), d, n).ToByteArray()));
                }
                File.WriteAllBytes(string.Format(@"RSA\RSAdecrypt_{0}.txt", lt), bytes.ToArray());
            }
        }

        private static BigInteger Encrypt(BigInteger m, BigInteger n, BigInteger e)
        {
            return BigInteger.ModPow(m, e, n);
        }

        private static BigInteger Decrypt(BigInteger mEnc, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(mEnc, d, n);
        }

        private static byte[] CopyAndReverse(params byte[] data)
        {
            byte[] reversed = new byte[data.Length];
            Array.Copy(data, 0, reversed, 0, data.Length);
            Array.Reverse(reversed);
            return reversed;
        }
    }
}
