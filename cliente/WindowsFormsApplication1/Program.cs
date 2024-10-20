using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClientApplication;

namespace WindowsFormsApplication1  // Cambié a WindowsFormsApplication1 para que coincida con Form1.cs
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
