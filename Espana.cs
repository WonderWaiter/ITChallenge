using System;

namespace ITCLibrary
{
    public static class Espana
    {
        public static int CalcPages()
        {
            int content = 171024;
            int title = 12825;
            int maxPageSize = 14359;

            for (int pageDigits = 1; pageDigits < 100; pageDigits++)
            {
                int leftSpace = maxPageSize - (2 * pageDigits + 1) - title;
                int pages = (int)Math.Ceiling((double)content / leftSpace);
                int digits = pages.ToString().Length;
                if (digits == pageDigits)
                {
                    return pages;
                }
            }
            throw new Exception();
        }
    }
}
