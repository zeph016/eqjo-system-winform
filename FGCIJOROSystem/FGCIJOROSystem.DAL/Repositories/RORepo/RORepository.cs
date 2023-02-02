using FGCIJOROSystem.Domain.RescueOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.Domain.Enums;

namespace FGCIJOROSystem.DAL.Repositories.RORepo
{
    public class RORepository : IRepository<clsRescueOrder>
    {
        public Int64 ROId;
        public void Add(clsRescueOrder obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[ROs]
                                       ([RONo]
                                       ,RefYear
                                       ,[BranchId]
                                       ,[CustomerType],[CustomerId],[CustomerRemarks]
                                       ,[ItemType],[EquipmentId],[EquipmentRemarks],Location
		                               ,[RODate]
                                       ,[DriverId]
                                       ,[ContractorCategory],[ContractorType],[ContractorId],SectionHead
                                       ,[Status]
                                       ,[ChecklistNo]
                                       ,[IsJobOut],[ServiceVehicleId],[ServiceDriverId],[Mileage])
                                 VALUES
                                       ((SELECT CASE WHEN EXISTS(SELECT * FROM [dbo].[ROs] As RO WHERE RO.BranchId = @BranchId) THEN(CASE WHEN (SELECT ISNULL(MAX(RO.RefYear),YEAR(GETDATE())) FROM [dbo].[ROs] As RO) = YEAR(GETDATE()) THEN 
                                        (CASE WHEN (SELECT ISNULL(MAX(RO.RONo),0) FROM [dbo].[ROs] As RO) = 0 THEN 1 ELSE (SELECT MAX(RO.RONo) + 1 FROM [dbo].[ROs] As RO) END)
                                        ELSE 1 END) ELSE 1 END)
                                       ,YEAR(GETDATE())
                                       ,@BranchId
                                       ,@CustomerType,@CustomerId,@CustomerRemarks
                                       ,@ItemType,@EquipmentId,@EquipmentRemarks,@EquipmentLocation
                                       ,@RODate
                                       ,@DriverId
                                       ,@ContractorCategory,@ContractorType,@ContractorId,@ContractorSectionHead
                                       ,@Status
                                       ,@ChecklistNo
                                       ,@IsJobOut,@ServiceVehicleId,@ServiceDriverId,@Mileage);
		                         SELECT SCOPE_IDENTITY() As Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                ROId = returnId;
                connection.Close();
                foreach (var x in obj.ListOfRODetails)
                {
                    x.ROId = returnId;
                    if (x.Id == 0)
                    {
                        new RODetailRepository().Add(x);
                    }
                    else
                    {
                        new RODetailRepository().Update(x);
                    }
                }
                foreach (var x in obj.ListOfROMechanics)
                {
                    x.ROId = returnId;
                    if (x.Id == 0)
                    {
                        new ROMechanicRepository().Add(x);
                    }
                    else
                    {
                        new ROMechanicRepository().Update(x);
                    }
                }
            }
        }

        public void Update(clsRescueOrder obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"
                            UPDATE [dbo].[ROs]
                               SET --[RONo] = @RONo
                                   [BranchId] = @BranchId
                                  ,[ChecklistNo] = @ChecklistNo
                                  ,[CustomerType] = @CustomerType
                                  ,[CustomerId] = @CustomerId
                                  ,[Location] = @EquipmentLocation
                                  ,[CustomerRemarks] = @CustomerRemarks
                                  ,[ItemType] = @ItemType
                                  ,[EquipmentId] = @EquipmentId
                                  ,[EquipmentRemarks] = @EquipmentRemarks
                                  ,[RODate] = @RODate
                                  ,[DriverId] = @DriverId
                                  ,[ContractorCategory] = @ContractorCategory
                                  ,[ContractorType] = @ContractorType
                                  ,[ContractorId] = @ContractorId
                                  ,[SectionHead] = @ContractorSectionHead
                                  --,[SectionHead] = @SectionHead
                                  --,[Status] = @Status
                                  ,[IsJobOut] = @IsJobOut
                                  ,[ServiceVehicleId] = @ServiceVehicleId
                                  ,[ServiceDriverId] = @ServiceDriverId
								  ,[Mileage] = @Mileage
                             WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                connection.Close();
                foreach (var x in obj.ListOfRODetails)
                {
                    x.ROId = obj.Id;
                    if (x.Id == 0)
                    {
                        new RODetailRepository().Add(x);
                    }
                    else
                    {
                        new RODetailRepository().Update(x);
                    }
                }
                foreach (var x in obj.ListOfROMechanics)
                {
                    x.ROId = obj.Id;
                    if (x.Id == 0)
                    {
                        new ROMechanicRepository().Add(x);
                    }
                    else
                    {
                        new ROMechanicRepository().Update(x);
                    }
                }
            }
        }

        public void Delete(clsRescueOrder obj)
        {
            throw new NotImplementedException();
        }

        public List<clsRescueOrder> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsRescueOrder> List = new List<clsRescueOrder>();
                String query = @"SELECT RO.[Id]
                                  ,RO.[BranchId]
                                  ,RO.[RONo]
                                  ,RO.[RefYear]
                                  ,RO.[ChecklistNo]
                                  ,RO.[CustomerCategory]
                                  ,RO.[CustomerType]
                                  ,RO.[CustomerId]
                                  ,RO.[Location] As EquipmentLocation
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

		                            /**,EquipmentLocation = (CASE WHEN RO.ItemType = 0 THEN EQ.ActualLocation 
							                             WHEN RO.ItemType = 1 THEN '' 
							                             WHEN RO.ItemType = 2 THEN ''
							                             ELSE  '' END)**/
		                            ,ContractorName = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							                             WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							                             WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							                             WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							                             ELSE  '' END)

		                            ,ContractorSectionHead = SectionHead

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
									,(SVET.PPETypeName + ' ' + SVEQ.PPEName) As ServiceVehicleName
									,PrintCount = (SELECT MAX(RT.PrintCount) FROM ROTransactionLogs as RT WHERE RT.ROId = RO.Id)	

									,ApproverId = (SELECT MAX(RT.ApproveId) FROM ROTransactionLogs as RT WHERE RT.ROId = RO.Id)		
									,ApproverName = (SELECT MAX(CONCAT(RGI.FirstName,' ', RGI.LastName,' ', RGI.NameExtension)) FROM ROTransactionLogs as RT 
											LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON RT.ApproveId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] RGI ON REI.GeneralInformationsId = RGI.Id WHERE RT.ROId =RO.Id )
									,ApproverPosition = (SELECT MAX(RP.PositionName) FROM ROTransactionLogs as RT 
											LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON RT.ApproveId = REI.Id 	
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As RP ON RP.Id = REI.PositionsId 
												WHERE RT.ROId = RO.Id)

									,EncoderName = (SELECT MAX(CONCAT(RGI.FirstName,' ', RGI.LastName,' ', RGI.NameExtension)) FROM ROTransactionLogs as RT 
											LEFT JOIN Users As U ON U.Id = RT.UserId
												LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON U.MLEmployeeId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] RGI ON REI.GeneralInformationsId = RGI.Id WHERE RT.ROId = RO.Id )
									,EncoderPosition = (SELECT MAX(RP.PositionName) FROM ROTransactionLogs as RT 
											LEFT JOIN Users As U ON U.Id = RT.UserId
												LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON U.MLEmployeeId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As RP ON RP.Id = REI.PositionsId 
												WHERE RT.ROId = RO.Id)
							  FROM [dbo].[ROs] AS RO
                              LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
	                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
	                            LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

	                            LEFT JOIN Contractors C ON C.Id = RO.ContractorId
	                            LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId
                                /*LEFT JOIN Personnels P ON Sec.Id = P.SectionId */
                                /*LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id */
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

	                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = RO.CustomerId
	
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations DEI ON RO.DriverId = DEI.Id 
                               LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON DEI.GeneralInformationsId = DGI.Id 

							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations SDEI ON RO.ServiceDriverId = SDEI.Id 
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SDGI ON SDEI.GeneralInformationsId = SDGI.Id
							   
							   
							   LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS SVEQ ON SVEQ.Id = RO.ServiceVehicleId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS SVET ON SVEQ.PPETypeId = SVET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS SVEC ON SVEQ.PPEClassId = SVEC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS SVER ON SVEQ.Id =  SVER.DescriptionAndStatusId ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsRescueOrder>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public clsRescueOrder FindByID(long id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsRescueOrder CurrentRow = new clsRescueOrder();
                String query = @"SELECT RO.[Id]
                                  ,RO.[BranchId]
                                  ,RO.[RONo]
                                  ,RO.[RefYear]
                                  ,RO.[ChecklistNo]
                                  ,RO.[CustomerCategory]
                                  ,RO.[CustomerType]
                                  ,RO.[CustomerId]
                                  ,RO.[Location] As EquipmentLocation
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

		                            /**,EquipmentLocation = (CASE WHEN RO.ItemType = 0 THEN EQ.ActualLocation 
							                             WHEN RO.ItemType = 1 THEN '' 
							                             WHEN RO.ItemType = 2 THEN ''
							                             ELSE  '' END)**/
		                            ,ContractorName = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							                             WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							                             WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							                             WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							                             ELSE  '' END)

		                            ,ContractorSectionHead = SectionHead

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
									,(SVET.PPETypeName + ' ' + SVEQ.PPEName) As ServiceVehicleName
									,PrintCount = (SELECT MAX(RT.PrintCount) FROM ROTransactionLogs as RT WHERE RT.ROId = RO.Id)	

									,ApproverId = (SELECT MAX(RT.ApproveId) FROM ROTransactionLogs as RT WHERE RT.ROId = RO.Id)		
									,ApproverName = (SELECT MAX(CONCAT(RGI.FirstName,' ', RGI.LastName,' ', RGI.NameExtension)) FROM ROTransactionLogs as RT 
											LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON RT.ApproveId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] RGI ON REI.GeneralInformationsId = RGI.Id WHERE RT.ROId =RO.Id )
									,ApproverPosition = (SELECT MAX(RP.PositionName) FROM ROTransactionLogs as RT 
											LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON RT.ApproveId = REI.Id 	
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As RP ON RP.Id = REI.PositionsId 
												WHERE RT.ROId = RO.Id)

									,EncoderName = (SELECT MAX(CONCAT(RGI.FirstName,' ', RGI.LastName,' ', RGI.NameExtension)) FROM ROTransactionLogs as RT 
											LEFT JOIN Users As U ON U.Id = RT.UserId
												LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON U.MLEmployeeId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] RGI ON REI.GeneralInformationsId = RGI.Id WHERE RT.ROId = RO.Id )
									,EncoderPosition = (SELECT MAX(RP.PositionName) FROM ROTransactionLogs as RT 
											LEFT JOIN Users As U ON U.Id = RT.UserId
												LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON U.MLEmployeeId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As RP ON RP.Id = REI.PositionsId 
												WHERE RT.ROId = RO.Id)
							  FROM [dbo].[ROs] AS RO
                              LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
	                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
	                            LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

	                            LEFT JOIN Contractors C ON C.Id = RO.ContractorId
	                            LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId
                                /*LEFT JOIN Personnels P ON Sec.Id = P.SectionId */
                                /*LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id */
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

	                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = RO.CustomerId
	
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations DEI ON RO.DriverId = DEI.Id 
                               LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON DEI.GeneralInformationsId = DGI.Id 

							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations SDEI ON RO.ServiceDriverId = SDEI.Id 
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SDGI ON SDEI.GeneralInformationsId = SDGI.Id
							   
							   
							   LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS SVEQ ON SVEQ.Id = RO.ServiceVehicleId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS SVET ON SVEQ.PPETypeId = SVET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS SVEC ON SVEQ.PPEClassId = SVEC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS SVER ON SVEQ.Id =  SVER.DescriptionAndStatusId WHERE RO.Id = @Id;";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                CurrentRow = connection.Query<clsRescueOrder>(query, new { Id = id}).FirstOrDefault();
                connection.Close();
                return CurrentRow;
            }
        }

        public List<clsRescueOrder> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsRescueOrder> Lists = new List<clsRescueOrder>();
                String query = @"SELECT RO.[Id]
                                  ,RO.[BranchId]
                                  ,RO.[RONo]
                                  ,RO.[RefYear]
                                  ,RO.[ChecklistNo]
                                  ,RO.[CustomerCategory]
                                  ,RO.[CustomerType]
                                  ,RO.[CustomerId]
                                  ,RO.[Location] As EquipmentLocation
                                  ,RO.[CustomerRemarks]
                                  ,RO.[ItemType]
                                  ,RO.[EquipmentId]
                                  ,RO.[EquipmentRemarks]
                                  ,CONVERT(DATETIME ,RO.[RODate]) As [RODate]
                                  ,RO.[DriverId]
                                  ,RO.[ContractorCategory]
                                  ,RO.[ContractorType]
                                  ,RO.[ContractorId]
								  ,RO.[IsJobOut]
                                  ,RO.[Status]	 
								  ,RO.[ServiceDriverId]
								  ,RO.[ServiceVehicleId]
								  ,RO.[Mileage]
	                              ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN RO.ItemType = 1 THEN SD.ToolName
							                             WHEN RO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	                              ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							                             WHEN RO.ItemType = 1 THEN 'STE' 
							                             WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							                             ELSE  '' END)

		                            /**,EquipmentLocation = (CASE WHEN RO.ItemType = 0 THEN EQ.ActualLocation 
							                             WHEN RO.ItemType = 1 THEN '' 
							                             WHEN RO.ItemType = 2 THEN ''
							                             ELSE  '' END)**/
		                            ,ContractorName = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							                             WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							                             WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							                             WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							                             ELSE  '' END)

		                            ,ContractorSectionHead = SectionHead

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
									,(SVET.PPETypeName + ' ' + SVEQ.PPEName) As ServiceVehicleName
									,PrintCount = (SELECT MAX(RT.PrintCount) FROM ROTransactionLogs as RT WHERE RT.ROId = RO.Id)	

									,ApproverId = (SELECT MAX(RT.ApproveId) FROM ROTransactionLogs as RT WHERE RT.ROId = RO.Id)		
									,ApproverName = (SELECT MAX(CONCAT(RGI.FirstName,' ', RGI.LastName,' ', RGI.NameExtension)) FROM ROTransactionLogs as RT 
											LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON RT.ApproveId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] RGI ON REI.GeneralInformationsId = RGI.Id WHERE RT.ROId =RO.Id )
									,ApproverPosition = (SELECT MAX(RP.PositionName) FROM ROTransactionLogs as RT 
											LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON RT.ApproveId = REI.Id 	
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As RP ON RP.Id = REI.PositionsId 
												WHERE RT.ROId = RO.Id)

									,EncoderName = (SELECT MAX(CONCAT(RGI.FirstName,' ', RGI.LastName,' ', RGI.NameExtension)) FROM ROTransactionLogs as RT 
											LEFT JOIN Users As U ON U.Id = RT.UserId
												LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON U.MLEmployeeId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] RGI ON REI.GeneralInformationsId = RGI.Id WHERE RT.ROId = RO.Id )
									,EncoderPosition = (SELECT MAX(RP.PositionName) FROM ROTransactionLogs as RT 
											LEFT JOIN Users As U ON U.Id = RT.UserId
												LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON U.MLEmployeeId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As RP ON RP.Id = REI.PositionsId 
												WHERE RT.ROId = RO.Id)
							  FROM [dbo].[ROs] AS RO
                              LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
	                            LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
	                            LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId

	                            LEFT JOIN Contractors C ON C.Id = RO.ContractorId
	                            LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId
                                /*LEFT JOIN Personnels P ON Sec.Id = P.SectionId */
                                /*LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id */
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

	                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = RO.CustomerId
	
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations DEI ON RO.DriverId = DEI.Id 
                               LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON DEI.GeneralInformationsId = DGI.Id 

							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations SDEI ON RO.ServiceDriverId = SDEI.Id 
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SDGI ON SDEI.GeneralInformationsId = SDGI.Id
							   
							   LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS SVEQ ON SVEQ.Id = RO.ServiceVehicleId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS SVET ON SVEQ.PPETypeId = SVET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS SVEC ON SVEQ.PPEClassId = SVEC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS SVER ON SVEQ.Id =  SVER.DescriptionAndStatusId " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Lists = connection.Query<clsRescueOrder>(query).ToList();
                connection.Close();
                return Lists;
            }
        }
        public void UpdateROStatus(clsEnums.JOROStatus Status, Int64 ROId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[ROs]
                                   SET [Status] = @Status
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, new { Status = (Int64)Status, Id = ROId });
                connection.Close();
            }
        }
        public List<clsRescueOrder> GetAllDriversAttendance(DateTime dtFrom, DateTime dtTo)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsRescueOrder> List = new List<clsRescueOrder>();
                String query = @"SELECT RO.[Id]
                                  ,RO.[BranchId]
                                  ,RO.[RONo]
                                  ,RO.[RefYear]
                                  ,RO.[ChecklistNo]
                                  ,RO.[CustomerCategory]
                                  ,RO.[CustomerType]
                                  ,RO.[CustomerId]
                                  ,RO.[Location] As EquipmentLocation
                                  ,RO.[CustomerRemarks]
                                  ,RO.[ItemType]
                                  ,RO.[EquipmentId]
                                  ,RO.[EquipmentRemarks]
                                  ,CONVERT(DateTime ,RO.[RODate], 101) As [RODate]
                                  ,RO.[DriverId]
		                          ,CONCAT(DGI.FirstName, ' ', DGI.MiddleName, ' ', DGI.LastName, ' ', DGI.NameExtension) AS DriverName
                                  ,RO.[ContractorCategory]
                                  ,RO.[ContractorType]
                                  ,RO.ContractorId
								  ,RO.IsJobOut
                                  ,RO.[Status]	 
								  ,RO.ServiceDriverId
								  ,CONCAT(SDGI.FirstName, ' ', SDGI.MiddleName, ' ', SDGI.LastName, ' ', SDGI.NameExtension) AS ServiceDriverName
								  ,RO.ServiceVehicleId
								  ,(SVET.PPETypeName + ' ' + SVEQ.PPEName) As ServiceVehicleName
	                              ,EquipmentName = (CASE WHEN RO.ItemType = 0 THEN ET.PPETypeName 
							                             WHEN RO.ItemType = 1 THEN SD.ToolName
							                             WHEN RO.ItemType = 2 THEN OT.[Name]  
							                             ELSE  '' END)
	                              ,EquipmentCode = (CASE WHEN RO.ItemType = 0 THEN EQ.PPEName 
							                             WHEN RO.ItemType = 1 THEN 'STE' 
							                             WHEN RO.ItemType = 2 THEN 'SPARE PARTS'
							                             ELSE  '' END)

		                            /**,EquipmentLocation = (CASE WHEN RO.ItemType = 0 THEN EQ.ActualLocation 
							                             WHEN RO.ItemType = 1 THEN '' 
							                             WHEN RO.ItemType = 2 THEN ''
							                             ELSE  '' END)**/
		                            ,ContractorName = (CASE WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 0 THEN C.Firstname + ' ' + C.Middlename + ' ' + C.Lastname + ' ' + C.NameExtension
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 0 THEN C.CompanyName 
							                             WHEN RO.ContractorCategory = 0 AND RO.ContractorType = 1 THEN Sec.GroupDescription
							                             WHEN RO.ContractorCategory = 1 AND RO.ContractorType = 1 THEN SC.CompanyName
							                             WHEN RO.ContractorCategory = 2 AND RO.ContractorType = 1 THEN SDp.DepartmentName
							                             WHEN RO.ContractorCategory = 3 AND RO.ContractorType = 1 THEN SS.SectionName
							                             ELSE  '' END)

		                            ,ContractorSectionHead = SectionHead

		                            ,CustomerName = (CASE WHEN RO.CustomerType = 0 THEN PCust.Name
							                              WHEN RO.CustomerType = 1 THEN CustCom.CompanyName 
							                              WHEN RO.CustomerType = 2 THEN CustDep.DepartmentName
							                              WHEN RO.CustomerType = 3 THEN CustSec.SectionName
							                              ELSE  '' END)

		                            ,CustomerAddress = (CASE WHEN RO.CustomerType = 0 THEN PCust.Address
		                            WHEN RO.CustomerType = 3 THEN CustSec.Location
		                            ELSE  '' END)

									,PrintCount = (SELECT MAX(RT.PrintCount) FROM ROTransactionLogs as RT WHERE RT.ROId = RO.Id)	

									,ApproverId = (SELECT MAX(RT.ApproveId) FROM ROTransactionLogs as RT WHERE RT.ROId = RO.Id)		
									,ApproverName = (SELECT MAX(CONCAT(RGI.FirstName,' ', RGI.LastName,' ', RGI.NameExtension)) FROM ROTransactionLogs as RT 
											LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON RT.ApproveId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] RGI ON REI.GeneralInformationsId = RGI.Id WHERE RT.ROId =RO.Id )
									,ApproverPosition = (SELECT MAX(RP.PositionName) FROM ROTransactionLogs as RT 
											LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON RT.ApproveId = REI.Id 	
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As RP ON RP.Id = REI.PositionsId 
												WHERE RT.ROId = RO.Id)

									,EncoderName = (SELECT MAX(CONCAT(RGI.FirstName,' ', RGI.LastName,' ', RGI.NameExtension)) FROM ROTransactionLogs as RT 
											LEFT JOIN Users As U ON U.Id = RT.UserId
												LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON U.MLEmployeeId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] RGI ON REI.GeneralInformationsId = RGI.Id WHERE RT.ROId = RO.Id )
									,EncoderPosition = (SELECT MAX(RP.PositionName) FROM ROTransactionLogs as RT 
											LEFT JOIN Users As U ON U.Id = RT.UserId
												LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] REI ON U.MLEmployeeId = REI.Id 
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As RP ON RP.Id = REI.PositionsId 
												WHERE RT.ROId = RO.Id)
									 ,CAST(Case WHEN  AR.JODetailId != 0 THEN (CASE WHEN AR.ReferenceType = 0 THEN '#RO' + SUBSTRING(RO.RefYear, 3, 3) + FORMAT(RO.RONo,'0000') END)
                                                                ELSE '' END as varchar) 
                                            + ' ' + AR.Activity As Activity
							  FROM [dbo].[ROs] AS RO
                              LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ ON EQ.Id = RO.EquipmentId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
											LEFT JOIN FGCILInventoryDB.dbo.StockDetails SD  ON SD.Id = RO.EquipmentId
												LEFT JOIN OtherEquipments OT ON OT.Id = RO.EquipmentId
													LEFT JOIN RODetails RD ON RD.ROId = RO.Id
														LEFT JOIN ActualAdvanceRefNos AR ON AR.Id = RD.Id

	                            LEFT JOIN Contractors C ON C.Id = RO.ContractorId
	                            LEFT JOIN Sections Sec ON Sec.Id = RO.ContractorId
                                /*LEFT JOIN Personnels P ON Sec.Id = P.SectionId */
                                /*LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EI ON P.EmployeeId = EI.Id 
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SGI ON EI.GeneralInformationsId = SGI.Id */
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Companies] As SC ON SC.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Sections As SS ON SS.Id = RO.ContractorId
	                            LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Departments As SDp ON SDp.Id = RO.ContractorId

	                            LEFT JOIN FGCIProjectEFileCabinetDB.dbo.PrivateCustomer AS PCust ON PCust.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Companies AS CustCom ON CustCom.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments AS CustDep ON CustDep.Id = RO.CustomerId
	                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Sections AS CustSec ON CustSec.Id = RO.CustomerId
	
                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations DEI ON RO.DriverId = DEI.Id 
                               LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON DEI.GeneralInformationsId = DGI.Id
								LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Positions POs ON POs.Id = DEI.PositionsId

							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].EmployeesInformations SDEI ON RO.ServiceDriverId = SDEI.Id 
							   LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] SDGI ON SDEI.GeneralInformationsId = SDGI.Id
							   
							   LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS SVEQ ON SVEQ.Id = RO.ServiceVehicleId
	                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS SVET ON SVEQ.PPETypeId = SVET.Id  
		                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS SVEC ON SVEQ.PPEClassId = SVEC.Id 
			                            LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS SVER ON SVEQ.Id =  SVER.DescriptionAndStatusId 
										WHERE RODate BETWEEN @dtFrom AND @dtTo";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsRescueOrder>(query, new { dtFrom = dtFrom, dtTo = dtTo}).ToList();
                connection.Close();
                return List;
            }
        }
    }
}