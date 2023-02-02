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
    public class ChecklistGeneratorDetailRepository: IRepository<clsChecklistGeneratorDetails>
    {

        public void Add(clsChecklistGeneratorDetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ChecklistGeneratorDetails]
                                                   ([ChecklistGeneratorId]
                                                   ,[ChecklistDetailId]
                                                   ,[IncomingStatus]
                                                   ,[IncomingRemarks]
                                                   ,[OutgoingStatus]
                                                   ,[OutgoingRemarks]
                                                   ,[EquipmentMasterlistId])
                                             VALUES
                                                   (@ChecklistGeneratorId
                                                   ,@ChecklistDetailId
                                                   ,@IncomingStatus
                                                   ,@IncomingRemarks
                                                   ,@OutgoingStatus
                                                   ,@OutgoingRemarks
                                                   ,@EquipmentMasterlistId";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            
           
        }

        public void Update(clsChecklistGeneratorDetails obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ChecklistGeneratorDetails]
                                        SET [ChecklistGeneratorId] = @ChecklistGeneratorId
                                            ,[ChecklistDetailId] = @ChecklistDetailId
                                            ,[IncomingStatus] = @IncomingStatus
                                            ,[IncomingRemarks] = @IncomingRemarks
                                            ,[OutgoingStatus] = @OutgoingStatus
                                            ,[OutgoingRemarks] = @OutgoingRemarks
                                            ,[EquipmentMasterlistId] = @EquipmentMasterlistId
                                        WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }            
        }

        public void Delete(clsChecklistGeneratorDetails obj)
        {
            throw new NotImplementedException();
        }

        public List<clsChecklistGeneratorDetails> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsChecklistGeneratorDetails> Lists = new List<clsChecklistGeneratorDetails>();
                String query = @"SELECT [Id]
                                      ,[ChecklistGeneratorId]
                                      ,[ChecklistDetailId]
                                      ,[IncomingStatus]
                                      ,[IncomingRemarks]
                                      ,[OutgoingStatus]
                                      ,[OutgoingRemarks]
                                      ,[EquipmentMasterlistId]
                                  FROM [dbo].[ChecklistGeneratorDetails]";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsChecklistGeneratorDetails>(query).ToList();
                connection.Close();
                return Lists;
            }
        }

        public clsChecklistGeneratorDetails FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsChecklistGeneratorDetails> SearchBy(string whereQuery)
        {
            throw new NotImplementedException();
        }
    }
}
