using FGCIJOROSystem.Domain.Checklist;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.Checklist
{
    public class ChecklistPrintLogsRepository : IRepository<clsChecklistPrintLogs>
    {
        public void Add(clsChecklistPrintLogs obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ChecklistPrintLogs]
                                               ([Id]
                                               ,[ChecklistNo]
                                               ,[DateEncoded]
                                               ,[Username])
                                         VALUES
                                               (@Id
                                               ,@ChecklistNo
                                               ,@DateEncoded
                                               ,@Username)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();

            }
        }

        public void Update(clsChecklistPrintLogs obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsChecklistPrintLogs obj)
        {
            throw new NotImplementedException();
        }

        public List<clsChecklistPrintLogs> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistPrintLogs> Lists = new List<clsChecklistPrintLogs>();
                String query = @"SELECT [Id]
                                          ,[ChecklistNo]
                                          ,[DateEncoded]
                                          ,[Username]
                                      FROM [dbo].[ChecklistPrintLogs]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistPrintLogs>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsChecklistPrintLogs FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsChecklistPrintLogs> SearchBy(string whereQuery)
        {
            throw new NotImplementedException();
        }
    }
}
