
using System.Security.Cryptography.X509Certificates;

namespace DigitalPersona_app
{
    internal static class Program
    {
        public static Capabilites_form capabilites_Form = new Capabilites_form();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new DP_Enterance());

        }
    }
}