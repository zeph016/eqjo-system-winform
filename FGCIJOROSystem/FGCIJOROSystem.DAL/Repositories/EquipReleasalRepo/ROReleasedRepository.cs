using FGCIJOROSystem.Domain.EquipmentReleasal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo
{
    public class ROReleasedRepository : IRepository<clsROReleased>
    {

        public void Add(clsROReleased obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ROReleased]
                                   ([ROId]
                                   ,[ROReleasalTransactionId]
                                   ,[Active]
                                   ,[Status])
                             VALUES
                                   (@ROId
                                   ,@ROTransReleasalId
                                   ,@Active
                                   ,0);";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsROReleased obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ROReleased]
                                   SET [Active] = @Active
                                      ,[Status] = @Status
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                connection.Close();
            }
        }

        public void Delete(clsROReleased obj)
        {
            throw new NotImplementedException();
        }

        public List<clsROReleased> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsROReleased FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsROReleased> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsROReleased> Lists = new List<clsROReleased>();
                String query = @"SELECT RR.[Id]
                                      ,RR.[ROId]
                                      ,RR.[ROReleasalTransactionId]
                                      ,RR.[Active]
                                      ,RR.[Status]
	                                  ,RO.RONo
                                      ,RO.RefYear
	                                  ,RO.ItemType
	                                  ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                                WHEN RO.ItemType = 1 THEN SD.ToolName
							                                WHEN RO.ItemType = 2 THEN OT.[Name]  
							                                ELSE  '' END)
	                                ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							                                WHEN RO.ItemType = 1 THEN 'STE' 
							                                WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							                                ELSE  '' END)
                                  FROM [FGCIJOROSystemDB].[dbo].[ROReleased] AS RR
                                  LEFT JOIN [dbo].[ROs] AS RO ON RO.Id = RR.ROId
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
                                LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId" + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsROReleased>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}
