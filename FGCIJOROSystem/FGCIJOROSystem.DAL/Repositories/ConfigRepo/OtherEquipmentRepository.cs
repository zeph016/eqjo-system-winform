using FGCIJOROSystem.Domain.Configurations.OtherEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class OtherEquipmentRepository : IRepository<clsOtherEquipment>
    {

        public void Add(clsOtherEquipment obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[OtherEquipments]
                                                           ([Name]
                                                           ,[Active])
                                                     VALUES
                                                           (@Name
                                                           ,@Active)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsOtherEquipment obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[OtherEquipments]
                                           SET [Name] = @Name
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

        public void Delete(clsOtherEquipment obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[OtherEquipments]
                                       SET [Active] = '0'
                                     WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsOtherEquipment> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsOtherEquipment> Lists = new List<clsOtherEquipment>();
                String query = @"SELECT [Id]
                                      ,[Name]
                                      ,[Active]
                                  FROM [dbo].[OtherEquipments]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsOtherEquipment>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsOtherEquipment FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsOtherEquipment List = new clsOtherEquipment();
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsOtherEquipment>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsOtherEquipment> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsOtherEquipment> Lists = new List<clsOtherEquipment>();
                String query = @"";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsOtherEquipment>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}
