using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace TestKernel {
    public class Kernel : Sys.Kernel {
        /*macro Echo
         var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);
         */
        protected override void BeforeRun() {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
        }

        protected override void Run() {
            Console.Write("Input: ");
            Echo();
        }

        #region MacroDummies
        // Dummy method required for IDE to not complain about missing method
        static void Echo() { }
        #endregion
    }
}
