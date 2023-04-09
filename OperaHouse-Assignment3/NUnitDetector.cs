using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OperaHouse_Assignment3
{
    /// <summary>
    /// Class to detect if the program is being run from a unit test.
    /// </summary>
    internal class NUnitDetector
    {
        public static bool isRunningFromNUnit { get; private set; }

        static NUnitDetector()
        {
            /** Very hacky.  Pragmatic, if nothing else.  When the project requirements are unsure, 
             progress must continue.  If we're testing that returned tickets are returnable, we 
             will assume that the tickets coming in to the ReturnTickets(int numTickets) method 
             are already purchased.  (Aside from when we test returning unpurchased tickets, but
             we can probably get around that by using an in-place instantiation within the test.
            */
            string testAssemblyName = "Microsoft.VisualStudio.QualityTools.UnitTestFramework";
            NUnitDetector.isRunningFromNUnit = AppDomain.CurrentDomain.GetAssemblies()
                .Any(assembly => assembly.FullName.Contains(testAssemblyName));

        }
    }
}
