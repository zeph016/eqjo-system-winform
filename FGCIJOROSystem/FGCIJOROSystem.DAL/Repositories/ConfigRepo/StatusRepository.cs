using FGCIJOROSystem.Domain.Configurations.Status;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.ConfigRepo
{
    public class StatusRepository : IRepository<clsStatus>
    {
        public void Add(clsStatus obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[Status]
                                ([Name]
                                ,[Description]
                                ,[IsClosure]
                                ,[IsAlert]
                                ,[NeedsApproval]
                                ,[SequenceOrder]
                                ,[Printable]
                                ,[IsApproval]
                                ,[IsBilled]
                                ,[EquipmentStatusSymbol]
                                ,[Active])
                            VALUES
                                (@StatusName
                                ,@StatusDescription
                                ,@IsClosure
                                ,@IsAlert
                                ,@NeedsApproval
                                ,@SequenceOrder
                                ,@Printable
                                ,@IsApproval
                                ,@IsBilled
                                ,@EquipmentStatusSymbol
                                ,@Active)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsStatus obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[Status]
                               SET [Name] = @StatusName
                                  ,[Description] = @StatusDescription
                                  ,[IsClosure] = @IsClosure
                                  ,[IsAlert] = @IsAlert
                                  ,[NeedsApproval] = @NeedsApproval
                                  ,[SequenceOrder] = @SequenceOrder
                                  ,[Printable] = @Printable
                                  ,[IsApproval] = @IsApproval
                                  ,[IsBilled] = @IsBilled
                                  ,[EquipmentStatusSymbol] = @EquipmentStatusSymbol
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

        public void Delete(clsStatus obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[Status]
                                   SET Active ='0'
                                 WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public List<clsStatus> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsStatus> Lists = new List<clsStatus>();
                String query = "Select *, Name As StatusName, Description As StatusDescription from Status Where Active = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsStatus>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsStatus> GetAllStatus()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsStatus> Lists = new List<clsStatus>();
                String query = "Select *, Name As StatusName, Description As StatusDescription from Status";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsStatus>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public clsStatus FindByID(Int64 id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsStatus List = new clsStatus();
                String query = "Select *, Name As StatusName, Description As StatusDescription from Status Where Id = Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsStatus>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return List;
            }
        }

        public List<clsStatus> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsStatus> Lists = new List<clsStatus>();
                String query = "Select *, Name As StatusName, Description As StatusDescription from Status WHERE " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsStatus>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}
