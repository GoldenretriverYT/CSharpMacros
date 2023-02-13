namespace TestProject {
    internal class Program {
        /*macro Plus x y
        (x+y).ToString()
            */

        /*macro GetNumberString aValue
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
            string xResult; // dummy variable to prevent IDE from complaining about missing variable
            string[] xChars = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            xResult = string.Empty;

            if (123 == 0) {
                xResult = "0";
            } else {
                int xValue = 123;

                if (123 < 0) {
                    xValue *= -1;
                }

                while (xValue > 0) {
                    int xValue2 = xValue % 10;
                    xResult = string.Concat(xChars[xValue2], xResult);
                    xValue /= 10;
                }
            }

            if (123 < 0) {
                xResult = string.Concat("-", xResult);
            }
;

            Console.WriteLine(xResult);
            // as our macro returns a string, this will be a string concatenation and not a numeric addition
            Console.WriteLine((1 + 2).ToString()
 + (3 + 4).ToString()
);
        }


    }
}