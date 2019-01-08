using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ConnectSQLServer
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            //
            string datasource = @"TUANDOAN-PC\SQLEXPRESS";
            string database = "QLDT_V1";
            
            return DBSQLServerUtils.GetDBConnection(datasource, database);
        }
    }
}
