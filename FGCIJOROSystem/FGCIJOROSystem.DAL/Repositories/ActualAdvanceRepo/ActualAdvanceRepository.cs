using FGCIJOROSystem.Domain.ActualAdvance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.ActualAdvanceRepo
{
    public class ActualAdvanceRepository : IRepository<clsActualAdvanceDetails>
    {

        public void Add(clsActualAdvanceDetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ActualAdvanceDetails]
                                       ([EmployeeId],[EncoderId]
                                       ,[DateOfUpdate]
                                       ,[DateEncoded]
                                       ,[AMStatus],[PMStatus],[AttendanceRemarks])
                                 VALUES
                                       (@EmployeeId,@EncoderId
                                       ,@DateOfUpdate
                                       ,GETDATE()
                                       ,@AMStatus,@PMStatus,@AttendanceRemarks);
                                 SELECT SCOPE_IDENTITY() AS Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 Id = connection.Query<Int64>(query,obj).FirstOrDefault();
                connection.Close();
                foreach (var item in obj.ListOfActualReference)
                {
                    item.ActualAdvanceId = Id;
                    if (item.Id == 0)
                    {
                        new ActualAdvanceReferenceRepository().Add(item);
                    }
                    else
                    {
                        new ActualAdvanceReferenceRepository().Update(item);
                    }
                    
                }
                foreach (var item in obj.ListOfAdvanceReference)
                {
                    item.ActualAdvanceId = Id;
                    if (item.Id == 0)
                    {
                        new ActualAdvanceReferenceRepository().Add(item);
                    }
                    else
                    {
                        new ActualAdvanceReferenceRepository().Update(item);
                    }
                }                
            }
        }

        public void Update(clsActualAdvanceDetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ActualAdvanceDetails]
                                   SET [EmployeeId] = @EmployeeId                                      
                                      ,[EncoderId] = @EncoderId
                                      ,[DateOfUpdate] = @DateOfUpdate
                                      ,[DateEncoded] = @DateEncoded
                                      ,[AMStatus] = @AMStatus
                                      ,[PMStatus] = @PMStatus
                                      ,[AttendanceRemarks] = @AttendanceRemarks
                                 WHERE Id = @Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Query<Int64>(query, obj).FirstOrDefault();
                connection.Close();
                foreach (var item in obj.ListOfActualReference)
                {
                    item.ActualAdvanceId = obj.Id;
                    if (item.Id == 0)
                    {
                        new ActualAdvanceReferenceRepository().Add(item);
                    }
                    else
                    {
                        new ActualAdvanceReferenceRepository().Update(item);
                    }

                }
                foreach (var item in obj.ListOfAdvanceReference)
                {
                    item.ActualAdvanceId = obj.Id;
                    if (item.Id == 0)
                    {
                        new ActualAdvanceReferenceRepository().Add(item);
                    }
                    else
                    {
                        new ActualAdvanceReferenceRepository().Update(item);
                    }
                }
            }
        }

        public void Delete(clsActualAdvanceDetails obj)
        {
            throw new NotImplementedException();
        }

        public List<clsActualAdvanceDetails> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsActualAdvanceDetails FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsActualAdvanceDetails> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsActualAdvanceDetails> List = new List<clsActualAdvanceDetails>();
                String query = @"SELECT * FROM [dbo].[ActualAdvanceDetails] " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsActualAdvanceDetails>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}
