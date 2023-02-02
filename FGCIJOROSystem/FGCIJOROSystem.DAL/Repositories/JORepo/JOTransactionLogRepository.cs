using FGCIJOROSystem.Domain.JobOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
namespace FGCIJOROSystem.DAL.Repositories.JORepo
{
    public class JOTransactionLogRepository : IRepository<clsJOTransactionLogs>
    {

        public void Add(clsJOTransactionLogs obj)
        {
            Int64 returnId = 0;
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JOTransactionLogs]
                                   ([BranchId],[JONo]
                                    ,[CustomerCategory],[CustomerType],[CustomerId],[Location],[CustomerRemarks]
                                   ,[EquipmentOnBranch]
                                   ,[ItemType],[EquipmentId],[EquipmentRemarks],[JODate]
                                   ,[DriverId],[ContractorCategory],[ContractorType],[ContractorId]
                                   ,[PartsRequest]
                                   ,[Status]
                                   ,[PrintCount],[UserId],[JOId]
                                   ,[ApproveId],[ApprovedDate],[DateEncoded],[RefYear],[Mileage])
                             VALUES
                                   (@BranchId,@JONo
                                   ,@CustomerType,@CustomerType,@CustomerId,@Location,@CustomerRemarks
                                   ,@EquipmentOnBranch
                                   ,@ItemType,@EquipmentId,@EquipmentRemarks,@JODate
                                   ,@DriverId
                                   ,@ContractorCategory,@ContractorType,@ContractorId
                                   ,@PartsRequest
                                   ,@Status
                                   ,@PrintCount,@UserId,@JOId
                                   ,@ApproveId,@ApprovedDate,GETDATE(),@RefYear,@Mileage);
                                SELECT SCOPE_IDENTITY() As Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                connection.Close();

            }
            foreach (var x in obj.ListOfJODetailTransactionLogs)
            {
                x.JOTransLogId = returnId;
                new JODetailTransactionLogRepository().Add(x);
            }
            foreach (var x in obj.ListOfMechanicsTransactionLogs)
            {
                x.JOTransLogId = returnId;
                new JOMechTransactionLogRepository().Add(x);
            }
        }

        public void Update(clsJOTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsJOTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public List<clsJOTransactionLogs> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsJOTransactionLogs FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsJOTransactionLogs> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJOTransactionLogs> List = new List<clsJOTransactionLogs>();
                String query = @"SELECT JO.[Id]
                                  ,JO.[BranchId]
                                  ,JO.[JONo]
                                  ,JO.[RefYear]
                                  ,JO.[CustomerCategory]
                                  ,JO.[CustomerType]
                                  ,JO.[CustomerId]
                                  ,JO.[Location]
                                  ,JO.[CustomerRemarks]
                                  ,JO.[ItemType]
                                  ,JO.[EquipmentId]
                                  ,JO.[EquipmentRemarks]
                                  ,CONVERT(DATETIME ,JO.[JODate]) As [JODate]
                                  ,JO.[DriverId]
                                  ,JO.[ContractorCategory]
                                  ,JO.[ContractorType]
                                  ,JO.ContractorId
                                  ,JO.[Status]	 
	                              ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN JO.ItemType = 1 THEN SD.ToolName
							                             WHEN JO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	                              ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                             WHEN JO.ItemType = 1 THEN 'STE' 
							                             WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                             ELSE  '' END)

		                            ,EquipmentLocation = (CASE WHEN JO.ItemType = 0 THEN EQ.ActualLocation 
							                             WHEN JO.ItemType = 1 THEN '' 
							                             WHEN JO.ItemType = 2 THEN ''
							                             ELSE  '' END)
		                            ,ContractorName = (CASE WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							                             WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 0 THEN C.CompanyName 
							                             WHEN JO.ContractorCategory = 0 AND JO.ContractorType = 1 THEN Sec.GroupDescription
							                             WHEN JO.ContractorCategory = 1 AND JO.ContractorType = 1 THEN SC.CompanyName
							                             WHEN JO.ContractorCategory = 2 AND JO.ContractorType = 1 THEN SDp.DepartmentName
							                             WHEN JO.ContractorCategory = 3 AND JO.ContractorType = 1 THEN SS.SectionName
							                             ELSE  '' END)

		                            ,ContractorSectionHead = JO.SectionHead

		                            ,CustomerName = (CASE WHEN JO.CustomerType = 0 THEN PCust.Name
							                              WHEN JO.CustomerType = 1 THEN CustCom.CompanyName 
							                              WHEN JO.CustomerType = 2 THEN CustDep.DepartmentName
							                              WHEN JO.CustomerType = 3 THEN CustSec.SectionName
							                              ELSE  '' END)

		                            ,CustomerAddress = (CASE WHEN JO.CustomerType = 0 THEN PCust.Address
		                            WHEN JO.CustomerType = 3 THEN CustSec.Location
		                            ELSE  '' END)

		                            ,DriverName = CONCAT(DGI.FirstName, ' ', DGI.MiddleName, ' ', DGI.LastName, ' ', DGI.NameExtension)
									
									,PrintCount	

	                                ,JO.ApproveId As ApproverId
	                                ,ApproverName = CONCAT(AGI.FirstName,' ', AGI.LastName,' ', AGI.NameExtension)
                                    ,ApproverPosition = AP.PositionName

									,ApproverId = JO.ApproveId
	                                ,ApproverName = CONCAT(AGI.FirstName,' ', AGI.LastName,' ', AGI.NameExtension)
                                    ,ApproverPosition = AP.PositionName

									,JO.UserId
	                                ,UserName = CONCAT(UGI.FirstName,' ', UGI.LastName,' ', UGI.NameExtension)
                                    ,UserPosition = UP.PositionName
							  FROM [dbo].[JOTransactionLogs] AS JO
                              LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = JO.EquipmentId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
	                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = JO.EquipmentId
	                            LEFT JOIN OtherEquipments OT ON OT.Id = JO.EquipmentId

	                            LEFT JOIN Contractors C ON C.Id = JO.ContractorId
	                            LEFT JOIN Sections Sec ON Sec.Id = JO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = JO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = JO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = JO.ContractorId

	                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = JO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = JO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = JO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = JO.CustomerId
	
                               LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON JO.DriverId = DGI.Id 
							   
								
								LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations AEI ON JO.DriverId = AEI.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] AGI ON AEI.GeneralInformationsId = AGI.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions AP ON AEI.PositionsId = AP.Id

							   							    LEFT JOIN Users U ON JO.UserId = U.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations UEI ON U.MLEmployeeId = UEI.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] UGI ON UEI.GeneralInformationsId = UGI.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions UP ON UEI.PositionsId = UP.Id 
							    " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOTransactionLogs>(query).ToList();
                connection.Close();
                return List;
            }
        }
        
    }
}
