namespace TestProject {
    internal class Program {



        static void Main(string[] args) {
            string myStr; // dummy variable to prevent IDE from complaining about missing variable
            string[] xChars = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            myStr = string.Empty;

            if (123 == 0) {
                myStr = "0";
            } else {
                int xValue = 123;

                if (123 < 0) {
                    xValue *= -1;
                }

                while (xValue > 0) {
                    int xValue2 = xValue % 10;
                    myStr = string.Concat(xChars[xValue2], myStr);
                    xValue /= 10;
                }
            }

            if (123 < 0) {
                myStr = string.Concat("-", myStr);
            }
;

            Console.WriteLine(myStr);
            // as our macro returns a string, this will be a string concatenation and not a numeric addition
            Console.WriteLine((1 + 2).ToString()
 + (3 + 4).ToString()
);
        }


    }
}