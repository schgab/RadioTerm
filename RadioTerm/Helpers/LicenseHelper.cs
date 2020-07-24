using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RadioTerm.Helpers
{
    internal static class LicenseHelper
    {
        private static readonly string LicenseFilePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "LICENSE.md");

        public static void ShowLicensesIfNeed(Action<string> outputAction)
        {
            if (outputAction is null)
                throw new ApplicationException("License output action was not provided.");

            if (!EnsureHasLicenseFile())
            {
                outputAction("License file not found.");
                return;
            }

            outputAction(File.ReadAllText(LicenseFilePath));
        }

        private static bool EnsureHasLicenseFile()
            => File.Exists(LicenseFilePath);

        public static bool HasShowLicenseOption(string[] args)
            => args?.Any(a => a == "show-license") ?? false;
    }
}