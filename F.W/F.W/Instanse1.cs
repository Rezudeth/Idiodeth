using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoCarsAllowed.Model;
using NoCarsAllowed.View;

namespace NoCarsAllowed
{
    class Instanse
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Console.WriteLine("Console is to check number of iterations of list function on Form2");
            Application.Run(new Form1());
            /*
            TCatalog catalog = new TCatalog();
            Console.WriteLine(catalog.ShowCatalog());
            Console.ReadLine();
            */
        }
    }
}
