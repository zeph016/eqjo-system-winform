using FGCIJOROSystem.Domain.Configurations.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
	public class CustomerRepository : IRepository<clsCustomer>
    {
        #region AddCustomer
        public void Add(clsCustomer obj)
        {
            using (IDbConnection connection = DbConnection.ProjectEFileConnection)
            {
                String query = @"INSERT INTO PrivateCustomer([Name]
                                            ,[Address]
                                            ,[Phone]
                                            ,[Active]) 
                                            VALUES (@CustomerName
                                            ,@CustomerAddress
                                            ,@ContactNo
                                            ,@Active)";
               
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }
        #endregion
      
        #region UpdateCustomer
        public void Update(clsCustomer obj)
        {
            using (IDbConnection connection = DbConnection.ProjectEFileConnection)
            {
                String query = @"UPDATE [dbo].[PrivateCustomer]
                                SET [Name] = @CustomerName
                                  ,[Address] = @CustomerAddress
                                  ,[Phone] =@ContactNo
                                  ,[Active] =@Active
                                WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }
        #endregion

        #region DeleteCustomer

        public void Delete(clsCustomer obj)
		{
			using (IDbConnection connection = DbConnection.ProjectEFileConnection)
			{
                string query = @"UPDATE [dbo].[PrivateCustomer]
                               SET Active ='0'
                             WHERE Id=@Id";
                if (connection.State==ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
			}
		}
        #endregion
        #region GetAll
        public List<clsCustomer> GetAll()
        {
            using (IDbConnection connection = DbConnection.ProjectEFileConnection)
            {
                List<clsCustomer> Lists = new List<clsCustomer>();
                String query = @"SELECT [Id]
                              ,[Name] As CustomerName
                              ,[Address] As CustomerAddress
                              ,[Phone] As ContactNo
                              ,[Active] As Active
                          FROM [dbo].[PrivateCustomer]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsCustomer>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        #endregion

        #region FindByID
        public clsCustomer FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.ProjectEFileConnection)
            {
                clsCustomer List = new clsCustomer();
                String query = "";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsCustomer>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        #endregion
        #region SearchBy
        public List<clsCustomer> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.ProjectEFileConnection)
            {
                List<clsCustomer> Lists = new List<clsCustomer>();
                String query = @"SELECT [Id]
                              ,[Name] As CustomerName
                              ,[Address] As CustomerAddress
                              ,[Phone] As ContactNo
                              ,[Active] As Active
                          FROM [dbo].[PrivateCustomer]
                          WHERE Name like '%' + @whereQuery + '%'";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsCustomer>(query, new { whereQuery = whereQuery }).ToList();
                connection.Close();
                return Lists;
            }

        }
        
        #endregion
    }
}
