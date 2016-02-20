using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BizArk.Core;
using BizArk.Core.CmdLine;
using System.IO;

namespace ShelfariDataImporter
{
    public class CommandLineArgs : CmdLineObject
    {
        #region Public Properties

        [CmdLineArg(Alias = "f", Required = true)]
        [Description("Absolute path of the input file to be processed.")]
        public string InputFile { get; set; }
                
        [CmdLineArg(Alias = "v", Required = false)]
        [Description("Prints all messages to standard output.")]
        public bool Verbose { get; set; }
                        
        #endregion

        #region Constructor

        public CommandLineArgs()
        {
            Verbose = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Override the base Validate() method.
        /// Useful when unable to add a custom validator to the Validators property
        /// for a command line property (CmdLineProperty in Bizark library).
        /// </summary>
        /// <returns>String array containing any validation error messages.</returns>
        protected override string[] Validate()
        {
            var errors = new List<string>();
            errors.AddRange(base.Validate());

            if(!File.Exists(this.InputFile))            
            {
                errors.Add(string.Format("InputFile has an error: file '{0}' does not exist.", this.InputFile));
            }
            
            return errors.ToArray();
        }

        #endregion
    }
}