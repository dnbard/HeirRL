using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace HeirRL.Data
{
    internal class SQLiteConnector
    {
        internal const string DBName = "heirrl.sqlite";
        internal static readonly SQLiteFactory Factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
        internal const string ConnectionString = "Data Source = " + DBName; 
    }
}
