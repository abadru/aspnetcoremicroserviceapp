using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Settings
{
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string CollectionName { get ; set ; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
