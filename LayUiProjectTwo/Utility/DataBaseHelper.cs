using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class DataBaseHelper
    {
        /// <summary>
        /// 获取数据库链接
        /// </summary>
        /// <returns></returns>
        public static Database CreateDataBase()
        {
            var dataBase = XmlHepler.xmlload("DataBase");
            var dburl = XmlHepler.xmlload(dataBase);
            Database dbbase=null;
            Database db = DatabaseFactory.CreateDatabase();
            DbConnectionStringBuilder dbsb = db.DbProviderFactory.CreateConnectionStringBuilder();
            dbsb.ConnectionString = dburl;
            GenericDatabase loadDb = new GenericDatabase(dbsb.ToString(), db.DbProviderFactory);
            dbbase = loadDb;
            return dbbase;
        }
     }
}
