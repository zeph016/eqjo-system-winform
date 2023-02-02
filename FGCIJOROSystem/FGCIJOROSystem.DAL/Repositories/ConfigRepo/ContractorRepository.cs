using FGCIJOROSystem.Domain.Configurations.Contractors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class ContractorRepository : IRepository<clsContractor>
    {
        public void Add(clsContractor obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[Contractors]
                                                   ([Type]
                                                   ,[Firstname]
                                                   ,[Middlename]
                                                   ,[Lastname]
                                                   ,[NameExtension]
                                                   ,[CompanyName]
                                                   ,[Address]
                                                   ,[ContactNos]
                                                   ,[Active])
                                             VALUES
                                                   (@ContractorCategory
                                                   ,@Firstname
                                                   ,@Middlename
                                                   ,@Lastname
                                                   ,@NameExtension
                                                   ,@CompanyName
                                                   ,@Address
                                                   ,@ContactNos
                                                   ,@Active)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsContractor obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[Contractors]
                                   SET [Type] = @ContractorCategory
                                      ,[Firstname] = @Firstname
                                      ,[Middlename] = @Middlename
                                      ,[Lastname] = @Lastname
                                      ,[NameExtension] = @NameExtension
                                      ,[CompanyName] = @CompanyName
                                      ,[Address] = @Address
                                      ,[ContactNos] = @ContactNos
                                      ,[Active] = @Active
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Delete(clsContractor obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[Contractors]
                                   SET [Active] = '0'
                                    WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsContractor> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsContractor> Lists = new List<clsContractor>();
                String query = @"SELECT [Id]
                                  ,[Type] as ContractorCategory
                                  ,[Firstname]
                                  ,[Middlename]
                                  ,[Lastname]
                                  ,[NameExtension]
                                  ,[CompanyName]
                                  ,[Address]
                                  ,[ContactNos]
                                  ,[Active]
                              FROM [dbo].[Contractors]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsContractor>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsContractor FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsContractor Item = new clsContractor();
                String query = @"SELECT [Id]
                                  ,[Type] = 0
                                  ,[Firstname]
                                  ,[Middlename]
                                  ,[Lastname]
                                  ,[NameExtension]
                                  ,[CompanyName]
                                  ,[Address]
                                  ,[ContactNos]
                                  ,[Active]
                              FROM [dbo].[Contractors] 
                                WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Item = connection.Query<clsContractor>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return Item;
            }
        }

        public List<clsContractor> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsContractor> Lists = new List<clsContractor>();
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsContractor>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}
