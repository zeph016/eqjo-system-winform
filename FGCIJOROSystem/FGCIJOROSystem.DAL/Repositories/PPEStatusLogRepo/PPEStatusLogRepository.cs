using FGCIJOROSystem.Domain.PPEStatusLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.PPEStatusLogRepo
{
    public class PPEStatusLogRepository// : IRepository<clsPPEStatusLog>
    {
        public void Add(clsPPEStatusLog obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[PPEStatusLog]
                                               ([EquipmentId]
                                               ,[PPETypeId]
                                               ,[PPEClassId]
                                               ,[EquipmentStatusId]
                                               ,[EmployeeId]
                                               ,[SystemName]
                                               ,[DateUpdate]
                                               ,[TimeUpdate])
                                         VALUES
                                               (@EquipmentId
                                               ,@PPETypeId
                                               ,@PPEClassId
                                               ,@EquipmentStatusId
                                               ,@EmployeeId
                                               ,@SystemName
                                               ,GETDATE()
                                               ,@TimeUpdate)";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }
        public void Update(clsPPEStatusLog obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[PPEStatusLog]
                                           SET [EquipmentId] = @EquipmentId
                                              ,[PPETypeId] = @PPETypeId
                                              ,[PPEClassId] = @PPEClassId
                                              ,[EquipmentStatusId] = @EquipmentStatusId
                                              ,[EmployeeId] = @EmployeeId
                                              ,[SystemName] = @SystemName
                                              ,[DateUpdate] = @DateUpdate
                                              ,[TimeUpdate] = @TimeUpdate
                                         WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void UpdateToPPELog(clsPPEStatusLog obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[PPEStatusLog]
                                           SET [EquipmentId] = @EquipmentId
                                              ,[PPETypeId] = @PPETypeId
                                              ,[PPEClassId] = @PPEClassId
                                              ,[EquipmentStatusId] = @EquipmentStatusId
                                              ,[EmployeeId] = @EmployeeId
                                              ,[SystemName] = @SystemName
                                              ,[DateUpdate] = @DateUpdate
                                              ,[TimeUpdate] = @TimeUpdate
                                         WHERE Id=@Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }
        public void Delete(clsPPEStatusLog obj)
        {
            throw new NotImplementedException();
        }

        public List<clsPPEStatusLog> GetAllFiltered(bool isEquipment, String equipmentIds, 
                                                    bool isEquipmentStatus, Int64 equipmentStatusId, 
                                                    bool IsDate,DateTime startDate, DateTime endDate)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsPPEStatusLog> Lists = new List<clsPPEStatusLog>();
                String query = @"SELECT ET.Id AS PPEStatusId
                                          ,EquipmentId
										  ,DS.PPEName AS EquipmentCode
										  ,PT.PPETypeName AS EquipmentName
										  ,DS.ActualLocation AS Location
                                          ,ET.EquipmentStatusId AS EquipmentStatusId
										  ,ES.StatusDescription AS EquipmentStatusName
                                          ,EmployeeId
										  ,CONCAT(GI.LastName, + ', ' + GI.FirstName, + ' ' + GI.MiddleName) AS FullName
                                          ,SystemName
                                          ,DateUpdate
                                          ,Convert(nvarchar(8), TimeUpdate)
                                      FROM PPEStatusLog AS ET
											LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.DescriptionAndStatus DS ON DS.Id = ET.EquipmentId
											LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.PPETypes PT ON PT.Id = DS.PPETypeId
												LEFT JOIN FGCIAccountingPPEMonitoringDB.dbo.EquipmentStatus ES ON ES.EquipmentStatusId = ET.EquipmentStatusId
													LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.EmployeesInformations EI ON EI.Id = ET.EmployeeId
													LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI ON GI.Id = EI.GeneralInformationsId 
													WHERE ET.Id <> 0 ";
                if (isEquipment)
                {
                    query += " AND EquipmentId IN ( " + equipmentIds + " ) ";
                }
                if (isEquipmentStatus)
                {
                    query += " AND ET.EquipmentStatusId IN ( " + equipmentStatusId + " ) ";
                }
                if (IsDate)
                {
                    query += " AND DateUpdate BETWEEN @startDate AND @endDate ";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsPPEStatusLog>(query, new { startDate = startDate, endDate = endDate}).ToList();
                connection.Close();
                return Lists;
            }
        }
        public List<clsPPEStatusLog> GetAllEquipment(bool isEquipment, String equipmentIds,
                                                    bool isEquipmentStatus, Int64 equipmentStatusId,
                                                    bool IsDate, DateTime startDate, DateTime endDate)
        {
            using (IDbConnection connection = DbConnection.PPEConnection)
            {
                List<clsPPEStatusLog> Lists = new List<clsPPEStatusLog>();
                String query = @"SELECT ppe.Id as EquipmentId, 
		                                    ppe.PPEName AS EquipmentCode, ppeType.PPETypeName AS EquipmentName,
		                                    ppe.PlateNo,sta.StatusDescription AS EquipmentStatusName, ppe.EquipmentStatusId
                                    FROM DescriptionAndStatus ppe
                                    LEFT JOIN PPETypes ppeType ON ppe.PPETypeId = ppeType.Id
                                    LEFT JOIN EquipmentStatus sta ON ppe.EquipmentStatusId = sta.EquipmentStatusId WHERE ppe.Id <> 0  ";
                if (isEquipment)
                {
                    query += " AND ppe.Id IN ( " + equipmentIds + " ) ";
                }
                if (isEquipmentStatus)
                {
                    query += " AND ppe.EquipmentStatusId IN ( " + equipmentStatusId + " ) ";
                }
               
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsPPEStatusLog>(query, new { startDate = startDate, endDate = endDate }).ToList();
                connection.Close();
                return Lists;
            }
        }

            public clsPPEStatusLog FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsPPEStatusLog> SearchBy(string whereQuery)
        {
            throw new NotImplementedException();
        }
    }
}
