using FGCIJOROSystem.Domain.WorkAssignment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.WorkAssignment
{
    public class ROWorkAssignmentRepository : IRepository<clsWorkAssignments>
    {

        public void Add(clsWorkAssignments obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ROWorkAssignment]
                                   ([RODetailId]
                                   ,[EmployeeId]
                                   ,[DateEncoded]
                                   ,[IsActive])
                             VALUES
                                   (@JODetailId
                                   ,@EmployeeId
                                   ,GETDATE()
                                   ,1);";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Query<Int64>(query, obj).FirstOrDefault();
                connection.Close();
            }
        }

        public void Update(clsWorkAssignments obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ROWorkAssignment]
                                   SET [RODetailId] = @JODetailId
                                   ,[EmployeeId] = @EmployeeId
                                   ,[IsActive] = @IsActive
                             WHERE Id = @Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Query<Int64>(query, obj).FirstOrDefault();
                connection.Close();
            }
        }

        public void Delete(clsWorkAssignments obj)
        {
            throw new NotImplementedException();
        }

        public List<clsWorkAssignments> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsWorkAssignments FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsWorkAssignments> SearchBy(string whereQuery)
        {
            throw new NotImplementedException();
        }
    }
}
