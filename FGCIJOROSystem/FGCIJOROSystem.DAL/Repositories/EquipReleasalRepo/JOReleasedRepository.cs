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
    public class JOReleasedRepository : IRepository<clsJOReleased>
    {
        public void Add(clsJOReleased obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JOReleased]
                                   ([JOId]
                                   ,[JOReleasalTransactionId]
                                   ,[Active]
                                   ,[Status])
                             VALUES
                                   (@JOId
                                   ,@JOTransReleasalId
                                   ,1
                                   ,0);
                                IF @ItemType = 1  
                                BEGIN
                                    UPDATE [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] 
                                    SET EquipmentStatusId = 1
                                    WHERE Id = @EquipmentId;
                                END";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
            }
        }

        public void Update(clsJOReleased obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JOReleased]
                                   SET [Active] = @Active
                                      ,[Status] = @Status
                                 WHERE Id = @Id;
                                IF @ItemType = 1  
                                BEGIN
                                    UPDATE [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] 
                                    SET EquipmentStatusId = 1
                                    WHERE Id = @EquipmentId;
                                END";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                connection.Close();
            }
        }

        public void Delete(clsJOReleased obj)
        {
            throw new NotImplementedException();
        }

        public List<clsJOReleased> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsJOReleased FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsJOReleased> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOReleased> Lists = new List<clsJOReleased>();
                String query = @"SELECT JR.[Id]
                                      ,JR.[JOId]
                                      ,JR.[JOReleasalTransactionId]
                                      ,JR.[Active]
                                      ,JR.[Status]
	                                  ,JO.JONo
                                      ,JO.RefYear
	                                  ,JO.ItemType
	                                  ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                                WHEN JO.ItemType = 1 THEN SD.ToolName
							                                WHEN JO.ItemType = 2 THEN OT.[Name]  
							                                ELSE  '' END)
	                                ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                                WHEN JO.ItemType = 1 THEN 'STE' 
							                                WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                                ELSE  '' END)
                                  FROM [FGCIJOROSystemDB].[dbo].[JOReleased] AS JR
                                  LEFT JOIN [dbo].[JOs] AS JO ON JO.Id = JR.JOId
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
	                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
		                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
                                LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId" + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsJOReleased>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
    }
}
