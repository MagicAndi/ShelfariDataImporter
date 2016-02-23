using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NLog;
using BizArk.Core.CmdLine;
using System.IO;

namespace ShelfariDataImporter
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string ApplicationTitle
        {
            get { return AppScope.Configuration.ApplicationTitle; }
        }

        public static string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public static string ApplicationTitleAndVersion
        {
            get { return string.Format("{0} - Version {1}", ApplicationTitle, Version); }
        }

        /// <summary>
        /// Main entry point for console application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            // Global exception handler
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            ConfigureConsoleForDisplay();

            var cmdLineArgs = new CommandLineArgs();
            cmdLineArgs.Initialize();

            // Validate only after checking to see if they requested help
            // in order to prevent displaying errors when they request help.
            if (cmdLineArgs.Help || !cmdLineArgs.IsValid())
            {
                Console.WriteLine(cmdLineArgs.GetHelpText(Console.WindowWidth));
                return;
            }

            // Process command line arguments
            var importer = new DataImporter(cmdLineArgs.InputFile, string.Empty);
            importer.Import();
            
            // Clean-up           

            // Exit
            DisplayExitPrompt();
        }

        private static void ConfigureConsoleForDisplay()
        {
            Console.WriteLine(ApplicationTitleAndVersion);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void DisplayExitPrompt()
        {
            if (AppScope.Configuration.DisplayExitPrompt)
            {
                Console.WriteLine("Press Enter to exit application.");
                Console.ReadLine();
            }
        }

        private static void DisplayApplicationTitle()
        {
            Console.WriteLine(ApplicationTitleAndVersion);
        }

        /// <summary>
        /// Method to trap unhandled exceptions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Error((Exception)e.ExceptionObject, "An unhandled exception has occurred");
            DisplayExitPrompt();
            Environment.Exit(1);
        }
    }
}
