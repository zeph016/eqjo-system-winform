using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.GlobalRepo
{
    public class ProductUnitRepository
    {        
        public List<clsProductUnit> GetAll()
        {
            using (IDbConnection connection = DbConnection.ProductMasterlistConnection)
            {
                List<clsProductUnit> Lists = new List<clsProductUnit>();
                String query = @"SELECT [Id]
                                      ,[UnitName]
                                      ,[Remarks]
                                      ,[UnitShortName]
                                      ,[IsActive]
                                  FROM [dbo].[Units]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsProductUnit>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsProductUnit FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.ProductMasterlistConnection)
            {
                clsProductUnit Item = new clsProductUnit();
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Item = connection.Query<clsProductUnit>(query).FirstOrDefault();
                connection.Close();
                return Item;
            }
        }

        public List<clsProductUnit> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.ProductMasterlistConnection)
            {
                List<clsProductUnit> Lists = new List<clsProductUnit>();
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsProductUnit>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}
