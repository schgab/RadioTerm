using System;
using RadioTerm.Helpers;
using RadioTerm.IO;
using RadioTerm.Renderer;

namespace RadioTerm
{
    public static class Program
    {
        #region Unhandled exception handling
        static Program() => AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e) => Console.WriteLine(e);
        #endregion

        public static void Main(string[] args)
        {
            if (LicenseHelper.HasShowLicenseOption(args))
            {
                LicenseHelper.ShowLicensesIfNeed(Console.WriteLine);
                return;
            }

            var rendererEngine = new ConsoleRendererEngine();
            new Player.Player(rendererEngine, new ConsolePlayerIOEngine(rendererEngine)).Run();
        }
    }
}
