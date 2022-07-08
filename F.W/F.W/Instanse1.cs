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
            Application.Run(new Form1());
            /*
            TCatalog catalog = new TCatalog();
            Console.WriteLine(catalog.ShowCatalog());
            Console.ReadLine();
            */
        }
    }
}
