using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompresJSON
{
    public partial class NORTHWNDEntities
    {
        public static NORTHWNDEntities JsonDB()
        {
            var db = new NORTHWNDEntities();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return db;
        }
    }
}