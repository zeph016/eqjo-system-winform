using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.DAL
{
    public class DbConnection
    {//comment
        public static IDbConnection JOROConnection
        {
            get
            {
                //IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["JOROConnection"].ToString());
                IDbConnection conn = new SqlConnection(DAL.Properties.Settings.Default.JOROSystemDB);
                return conn;
            }
        }

        public static IDbConnection MasterlistConnection
        {
            get
            {
                //IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterlistConnection"].ToString());
                IDbConnection conn = new SqlConnection(DAL.Properties.Settings.Default.MasterlistConnection);
                return conn;
            }
        }
        public static IDbConnection PPEConnection
        {
            get
            {
                //IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PPEConnection"].ToString());
                IDbConnection conn = new SqlConnection(DAL.Properties.Settings.Default.PPEConnection);
                return conn;
            }
        }
        public static IDbConnection ProjectEFileConnection
        {
            get
            {
                //IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectEFileConnection"].ToString());
                IDbConnection conn = new SqlConnection(DAL.Properties.Settings.Default.ProjectEFileConnection);
                return conn;
            }
        }
        public static IDbConnection ProductMasterlistConnection
        {
            get
            {
                //IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ProductMasterlistConnection"].ToString());
                IDbConnection conn = new SqlConnection(DAL.Properties.Settings.Default.ProductMasterlistConnection);
                return conn;
            }
        }
       public static IDbConnection InventoryConnection
        {
            get
            {
                //IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InventoryConnection"].ToString());
                IDbConnection conn = new SqlConnection(DAL.Properties.Settings.Default.InventoryConnection);
                return conn;
            }
        }

        public static String GetDBNameMasterlist()
        {
            return MasterlistConnection.Database;
        }
        public static String GetDBNamePPE()
        {
            return PPEConnection.Database;
        }
        public static String GetDBNameProjectEFile()
        {
            return ProjectEFileConnection.Database;
        }
        public static String GetDBNameProductMasterlist()
        {
            return ProductMasterlistConnection.Database;
        }
        public static String GetDBNameInventory()
        {
            return InventoryConnection.Database;
        }
    }
}
