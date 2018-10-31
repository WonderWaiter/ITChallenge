using System;
using System.Collections.Generic;

namespace ITCLibrary
{
    public static class USA
    {
        public static void Work()
        {
            List<string> input = new List<string>()
            {
                "edbbaacdcaacacbababaabadeeeaaddecaaeceeecbdcdaeacaccccaddeaaddecdcdcdccadcacceeecdcbceecebde",
                "dadbccabcdeccbcdbedaaabbdccdddcbdbebdeca",
                "aeaeddabaacbdcecacccbbacededbecbaccdccccebacdbbaedecbaeadaebedeccbaedcdcdabdcedbddabaeeaadcbdd",
                "abbdaedeeeedeaeeabcabbadbebedcedaadabbbddbbebdabecdcbdcc",
                "cddddabbaeaccaabedebbaaeabccecddcdbaaecbeeadeaeadabeddadaccbcdeebcacceaddabccdccaaddddd",
                "bbeeabcadeecbcadae",
                "dcbaceaadbdeceaaccaaeecadeedabeaecadbbebeecbdcddaadbbdbeecaaebcadddbb",
                "adcdeaccccaaeabaaeaaabeaecdbadbabdecadeeacebcdcceceebeecdeaeebbbccaeacedeaeddbd",
                "ed",
                "ebeecaddbbceecebdeadedecddddecddecebeabbbecabdbeddeceabc"
            };            
            foreach (string s in input)
            {
                Console.Write(string.Format("{0} ", Count(s)));
            }
        }

        public static long Count(string s)
        {
            long count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                string ss = s.Substring(i);
                for (int j = 0; j < ss.Length; j++)
                {
                    if (ss[j] == s[j])
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return count;
        }
    }
}
