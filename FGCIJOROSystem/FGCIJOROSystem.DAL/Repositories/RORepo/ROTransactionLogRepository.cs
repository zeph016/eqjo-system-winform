using FGCIJOROSystem.Domain.RescueOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace FGCIJOROSystem.DAL.Repositories.RORepo
{
    public class ROTransactionLogRepository : IRepository<clsROTransactionLogs>
    {

        public void Add(clsROTransactionLogs obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ROTransactionLogs]
                                       ([ROId]
                                       ,[RONo]
                                       ,[BranchId]
                                       ,[CustomerCategory]
                                       ,[CustomerType]
                                       ,[CustomerId]
                                       ,[Location]
                                       ,[CustomerRemarks]
                                       ,[ItemType]
                                       ,[EquipmentId]
                                       ,[EquipmentRemarks]
                                       ,[RODate]
                                       ,[DriverId]
                                       ,[ContractorCategory]
                                       ,[ContractorType]
                                       ,[ContractorId]
                                       ,[ContractorSection]
                                       ,[SectionHead]
                                       ,[Status]
                                       ,[IsJobOut]
                                       ,[ServiceVehicleId]
                                       ,[ServiceDriverId]
                                       ,[ApproveId]
                                       ,[ApprovedDate]
                                       ,[DateEncoded]
                                       ,[RefYear]
                                       ,[Mileage])
                                 VALUES
                                       (@ROId
                                       ,@RONo
                                       ,@BranchId
                                       ,@CustomerCategory
                                       ,@CustomerType
                                       ,@CustomerId
                                       ,@Location
                                       ,@CustomerRemarks
                                       ,@ItemType
                                       ,@EquipmentId
                                       ,@EquipmentRemarks
                                       ,@RODate
                                       ,@DriverId
                                       ,@ContractorCategory
                                       ,@ContractorType
                                       ,@ContractorId
                                       ,''
                                       ,''
                                       ,@Status
                                       ,@IsJobOut
                                       ,@ServiceVehicleId
                                       ,@ServiceDriverId
                                       ,@ApproverId
                                       ,@ApprovedDate
                                       ,@DateEncoded
                                       ,@RefYear
                                       ,@Mileage)
		                         SELECT SCOPE_IDENTITY() As Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                connection.Close();
                foreach (var x in obj.ListOfRODetailTransactionLogs)
                {
                    x.ROTransLogId = returnId;
                    if (x.Id == 0)
                    {
                        new RODetailTransactionLogRepository().Add(x);
                    }
                }
                foreach (var x in obj.ListOfROMechanicsTransactionLogs)
                {
                    x.ROTransLogId = returnId;
                    if (x.Id == 0)
                    {
                        new ROMechTransactionLogRepository().Add(x);
                    }
                }
            }
        }

        public void Update(clsROTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsROTransactionLogs obj)
        {
            throw new NotImplementedException();
        }

        public List<clsROTransactionLogs> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsROTransactionLogs FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public List<clsROTransactionLogs> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsROTransactionLogs> List = new List<clsROTransactionLogs>();
                String query = @"SELECT RO.[Id]
                                  ,RO.[BranchId]
                                  ,RO.[RONo]
                                  ,RO.[RefYear]
                                  ,RO.[CustomerCategory]
                                  ,RO.[CustomerType]
                                  ,RO.[CustomerId]
                                  ,RO.[Location]
                                  ,RO.[CustomerRemarks]
                                  ,RO.[ItemType]
                                  ,RO.[EquipmentId]
                                  ,RO.[EquipmentRemarks]
                                  ,CONVERT(DATETIME ,RO.[RODate]) As [RODate]
                                  ,RO.[DriverId]
                                  ,RO.[ContractorCategory]
                                  ,RO.[ContractorType]
                                  ,RO.ContractorId
								  ,RO.IsJobOut
                                  ,RO.[Status]	 
								  ,RO.ServiceDriverId
								  ,RO.ServiceVehicleId
	                              ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN RO.ItemType = 1 THEN SD.ToolName
							                             WHEN RO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	                              ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							                             WHEN RO.ItemType = 1 THEN 'STE' 
							                             WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							                             ELSE  '' END)

		                            ,EquipmentLocation = (CASE WHEN RO.ItemType = 0 THEN EQ.ActualLocation 
							                             WHEN RO.ItemType = 1 THEN '' 
							                             WHEN RO.ItemType = 2 THEN ''
							                             ELSE  '' END)
		                            ,ContractorName = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							                             WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							                             WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							                             WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							                             ELSE  '' END)

		                            ,ContractorSectionHead = RO.SectionHead

		                            ,CustomerName = (CASE WHEN RO.CustomerType = 0 THEN PCust.Name
							                              WHEN RO.CustomerType = 1 THEN CustCom.CompanyName 
							                              WHEN RO.CustomerType = 2 THEN CustDep.DepartmentName
							                              WHEN RO.CustomerType = 3 THEN CustSec.SectionName
							                              ELSE  '' END)

		                            ,CustomerAddress = (CASE WHEN RO.CustomerType = 0 THEN PCust.Address
		                            WHEN RO.CustomerType = 3 THEN CustSec.Location
		                            ELSE  '' END)

		                            ,DriverName = CONCAT(DGI.FirstName, ' ', DGI.MiddleName, ' ', DGI.LastName, ' ', DGI.NameExtension)
									,ServiceDriverName = CONCAT(SDGI.FirstName, ' ', SDGI.MiddleName, ' ', SDGI.LastName, ' ', SDGI.NameExtension)
									,SVET.PPETypeName + ' ' + SVEQ.PPEName As ServiceVehicleName
									,PrintCount	

	                                ,RO.ApproveId As ApproverId
	                                ,ApproverName = CONCAT(AGI.FirstName,' ', AGI.LastName,' ', AGI.NameExtension)
                                    ,ApproverPosition = AP.PositionName

									,ApproverId = RO.ApproveId
	                                ,ApproverName = CONCAT(AGI.FirstName,' ', AGI.LastName,' ', AGI.NameExtension)
                                    ,ApproverPosition = AP.PositionName

									,RO.UserId
	                                ,UserName = CONCAT(UGI.FirstName,' ', UGI.LastName,' ', UGI.NameExtension)
                                    ,UserPosition = UP.PositionName
							  FROM [dbo].[ROTransactionLogs] AS RO
                              LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
	                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
	                            LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

	                            LEFT JOIN Contractors C ON C.Id = RO.ContractorId
	                            LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

	                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = RO.CustomerId
	
                               LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON RO.DriverId = DGI.Id 
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SDGI ON RO.ServiceDriverId = SDGI.Id
							   
							   LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS SVEQ ON SVEQ.Id = RO.ServiceVehicleId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS SVET ON SVEQ.PPETypeId = SVET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS SVEC ON SVEQ.PPEClassId = SVEC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS SVER ON SVEQ.Id =  SVER.DescriptionAndStatusId
								
								LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations AEI ON RO.DriverId = AEI.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] AGI ON AEI.GeneralInformationsId = AGI.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions AP ON AEI.PositionsId = AP.Id

							   LEFT JOIN Users U ON RO.UserId = U.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations UEI ON U.MLEmployeeId = UEI.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] UGI ON UEI.GeneralInformationsId = UGI.Id
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions UP ON UEI.PositionsId = UP.Id    " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsROTransactionLogs>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }
}
