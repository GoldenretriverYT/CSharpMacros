using System;

namespace Test {
    internal class Program {
        /*macro Plus x y
        (x+y).ToString()
            */

        /*macro GetNumberString aValue xResult
        string[] xChars = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        xResult = string.Empty;

        if (aValue == 0)
        {
            xResult = "0";
        }
        else
        {
            int xValue = aValue;

            if (aValue < 0)
            {
                xValue *= -1;
            }

            while (xValue > 0)
            {
                int xValue2 = xValue % 10;
                xResult = string.Concat(xChars[xValue2], xResult);
                xValue /= 10;
            }
        }

        if (aValue < 0)
        {
            xResult = string.Concat("-", xResult);
        }
        */
        static void Main(string[] args) {
            string myStr; // dummy variable to prevent IDE from complaining about missing variable
            GetNumberString(123, myStr);

            Console.WriteLine(myStr);
            // as our macro returns a string, this will be a string concatenation and not a numeric addition
            Console.WriteLine(Plus(1, 2) + Plus(3, 4));
        }

        #region MacroDummies
        // Dummy method required for IDE to not complain about missing method
        static string Plus(int x, int y) => {return "";}
    static string GetNumberString(int x, string arg) => {return "";}
        #endregion
    }
}