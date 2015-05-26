using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompresJSON
{
    public partial class AlexDbEntities
    {
        public static AlexDbEntities JsonDB()
        {
            var db = new AlexDbEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return db;
        }
    }
}