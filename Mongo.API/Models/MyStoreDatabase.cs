using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDb.Api.Models
{
    public class MyStoreDatabase
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProductsCollectionName { get; set; }
    }
}
