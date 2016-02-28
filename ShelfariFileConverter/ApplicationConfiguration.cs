using Westwind.Utilities.Configuration;

namespace ShelfariFileConverter
{
    public class ApplicationConfiguration : AppConfiguration
    {
        #region Public Properties

        public string ApplicationTitle { get; set; }
        public bool DisplayExitPrompt { get; set; }  

        #endregion

        #region Constructor

        public ApplicationConfiguration()
        {
        }

        #endregion        
    }
}