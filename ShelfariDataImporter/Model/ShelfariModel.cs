using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace ShelfariDataImporter.Model
{
    public class ShelfariModel : DbContext
    {
        #region Properties

        // Entity types to be included in model
        public virtual DbSet<ShelfariRecord> Books { get; set; }

        #endregion
        
        #region Constructor

        // Your context has been configured to use a 'AssignmentsModel' connection string from your application's
        // configuration file (App.config or Web.config). By default, this connection string targets the
        // 'QOL.Azure.DataLoader.AssignmentsModel' database on your LocalDb instance.
        //
        // If you wish to target a different database and/or database provider, modify the 'AssignmentsModel'
        // connection string in the application configuration file.
        public ShelfariModel()
            : base("name=ShelfariModel")
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}