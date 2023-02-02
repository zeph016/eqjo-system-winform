using FGCIJOROSystem.Domain.JobOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using FGCIJOROSystem.Domain.Enums;
using System.Data.SqlClient;

namespace FGCIJOROSystem.DAL.Repositories.JORepo
{
    public class JORepository : IRepository<clsJobOrder>
    {
        public Int64 JOId { get; set; }
        public void Add(clsJobOrder obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"INSERT INTO [dbo].[JOs]
                                    ([BranchId]
                                    ,[SectionHead]
                                    ,[JONo]
                                    ,[JODate]
                                    ,[RefYear]
                                    ,[CustomerCategory],[CustomerType],[CustomerId],[Location],[CustomerRemarks]
                                    ,[EquipmentOnBranch],[ItemType],[EquipmentId],[EquipmentRemarks]                                
                                    ,[DriverId]
                                    ,[ContractorCategory],[ContractorType],[ContractorId]
                                    ,[PartsRequest],[ChecklistNo],[Status],[Mileage])
                                VALUES
                                    (@BranchId
                                    ,@ContractorSectionHead
                                    ,(SELECT CASE WHEN EXISTS(SELECT * FROM [dbo].[JOs] As JO WHERE JO.BranchId = @BranchId) THEN CASE WHEN (SELECT ISNULL(MAX(JO.RefYear),YEAR(GETDATE())) FROM [dbo].[JOs] As JO) = YEAR(GETDATE()) THEN 
                                    (CASE WHEN (SELECT ISNULL(MAX(JO.JONo),0) FROM [dbo].[JOs] As JO) = 0 THEN 1 ELSE (SELECT MAX(JO.JONo) + 1 FROM [dbo].[JOs] As JO) END)
                                    ELSE 1 END ELSE 1 END)
                                    ,@JODate
                                    ,YEAR(GETDATE())
                                    ,@CustomerType,@CustomerType,@CustomerId,@Location,@CustomerRemarks
                                    ,@EquipmentOnBranch
                                    ,@ItemType,@EquipmentId,@EquipmentRemarks                                
                                    ,@DriverId
                                    ,@ContractorCategory,@ContractorType,@ContractorId
                                    ,@PartsRequest,@ChecklistNo,@Status,@Mileage);
                                    SELECT SCOPE_IDENTITY() As Id;
                                IF @ItemType = 1  
                                BEGIN
                                    UPDATE [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] 
                                    SET EquipmentStatusId = 3
                                    WHERE Id = @EquipmentId;
                                END";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                Int64 returnId = connection.Query<Int64>(query, obj).FirstOrDefault();
                obj.Id = returnId;
                JOId = returnId;
                connection.Close();
                foreach(var x in obj.ListOfJODetails)
                {
                    x.JOId = returnId;
                    if (x.Id == 0)
                    {
                        new JODetailRepository().Add(x);
                    }
                    else
                    {
                        new JODetailRepository().Update(x);
                    }
                }
                foreach (var x in obj.ListOfMechanics)
                {
                    x.JOId = returnId;
                    if (x.Id == 0)
                    {
                        new JOMechanicRepository().Add(x);
                    }
                    else
                    {
                        new JOMechanicRepository().Update(x);
                    }
                }
                //new JOTransactionLogRepository().Add();
            }
        }

        public void Update(clsJobOrder obj)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JOs]
                                   SET [BranchId] = @BranchId
                                      ,[SectionHead] = @ContractorSectionHead
                                      ,[JONo] = @JONo
                                      ,[ChecklistNo] = @ChecklistNo
                                      ,[CustomerCategory] = @CustomerType
                                      ,[CustomerType] = @CustomerType
                                      ,[CustomerId] = @CustomerId
                                      ,[Location] = @Location
                                      ,[CustomerRemarks] = @CustomerRemarks
                                      ,[EquipmentOnBranch] = @EquipmentOnBranch
                                      ,[ItemType] = @ItemType
                                      ,[EquipmentId] = @EquipmentId
                                      ,[EquipmentRemarks] = @EquipmentRemarks
                                      ,[JODate] = @JODate
                                      ,[DriverId] = @DriverId
                                      ,[ContractorCategory] = @ContractorCategory
                                      ,[ContractorType] = @ContractorType
                                      ,[ContractorId] = @ContractorId
                                      ,[PartsRequest] = @PartsRequest
                                      --,[Status] = @Status
                                      ,[Mileage] = @Mileage
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, obj);
                JOId = obj.Id;
                connection.Close();
                foreach (var x in obj.ListOfJODetails)
                {
                    x.JOId = obj.Id;
                    if (x.Id == 0)
                    {
                        new JODetailRepository().Add(x);
                    }
                    else
                    {
                        new JODetailRepository().Update(x);
                    }
                }
                foreach (var x in obj.ListOfMechanics)
                {
                    x.JOId = obj.Id;
                    if (x.Id == 0)
                    {
                        new JOMechanicRepository().Add(x);
                    }
                    else
                    {
                        new JOMechanicRepository().Update(x);
                    }
                }
            }
        }
        
        public void Delete(clsJobOrder obj)
        {
            throw new NotImplementedException();
        }

        public List<clsJobOrder> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJobOrder> List = new List<clsJobOrder>();
                String query = @"SELECT JO.[Id]
                                        ,JO.[BranchId]
                                        ,JO.[ChecklistNo]
                                        ,JO.[JONo]
                                        ,JO.[RefYear]
                                        ,JO.[CustomerCategory]
                                        ,JO.[CustomerType]
                                        ,JO.[CustomerId]
                                        ,JO.[Location] As EquipmentLocation
                                        ,JO.[CustomerRemarks]
                                        ,JO.[EquipmentOnBranch]
                                        ,JO.[ItemType]
                                        ,JO.[EquipmentId]
                                        ,JO.[EquipmentRemarks]
                                        ,CONVERT(DATETIME ,JO.[JODate]) As [JODate]
                                        ,JO.[DriverId]
                                        ,JO.[ContractorCategory]
                                        ,JO.[ContractorType]
                                        ,JO.[ContractorId]
                                        ,JO.[PartsRequest]
                                        ,JO.[Status]	 
	                                    ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN JO.ItemType = 1 THEN SD.ToolName
							                                    WHEN JO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN JO.ItemType = 1 THEN 'STE' 
							                                    WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)

	                                    /**,EquipmentLocation = (CASE WHEN JO.ItemType = 0 THEN EQ.ActualLocation 
							                                    WHEN JO.ItemType = 1 THEN '' 
							                                    WHEN JO.ItemType = 2 THEN ''
							                                    ELSE  '' END)**/
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
                                        ,PrintCount = (SELECT MAX(JT.PrintCount) FROM JOTransactionLogs as JT WHERE JT.JOId = JO.Id)	

	                                    ,ApproverId = (SELECT MAX(JT.ApproveId) FROM JOTransactionLogs as JT WHERE JT.JOId = JO.Id)		
	                                    ,ApproverName = (SELECT MAX(CONCAT(JGI.FirstName,' ', JGI.LastName,' ', JGI.NameExtension)) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON JT.ApproveId = JEI.Id 
				                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] JGI ON JEI.GeneralInformationsId = JGI.Id WHERE JT.JOId = JO.Id )
                                        ,ApproverPosition = (SELECT MAX(JP.PositionName) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON JT.ApproveId = JEI.Id 	
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As JP ON JP.Id = JEI.PositionsId 
                                                    WHERE JT.JOId = JO.Id)

	                                    ,EncoderName = (SELECT MAX(CONCAT(JGI.FirstName,' ', JGI.LastName,' ', JGI.NameExtension)) FROM JOTransactionLogs as JT 
												LEFT JOIN Users As U ON U.Id = JT.UserId
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON U.MLEmployeeId = JEI.Id 
				                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] JGI ON JEI.GeneralInformationsId = JGI.Id WHERE JT.JOId = JO.Id )
                                        ,EncoderPosition = (SELECT MAX(JP.PositionName) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN Users As U ON U.Id = JT.UserId
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON U.MLEmployeeId = JEI.Id 
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As JP ON JP.Id = JEI.PositionsId 
                                                    WHERE JT.JOId = JO.Id)
                                    FROM [dbo].[JOs] AS JO
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
	
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON JO.DriverId = DGI.Id ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJobOrder>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public clsJobOrder FindByID(long id)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                clsJobOrder currentRow = new clsJobOrder();
                String query = @"SELECT JO.[Id]
                                        ,JO.[BranchId]
                                        ,JO.[ChecklistNo]
                                        ,JO.[JONo]
                                        ,JO.[RefYear]
                                        ,JO.[CustomerCategory]
                                        ,JO.[CustomerType]
                                        ,JO.[CustomerId]
                                        ,JO.[Location] As EquipmentLocation
                                        ,JO.[CustomerRemarks]
                                        ,JO.[EquipmentOnBranch]
                                        ,JO.[ItemType]
                                        ,JO.[EquipmentId]
                                        ,JO.[EquipmentRemarks]
                                        ,CONVERT(DATETIME ,JO.[JODate]) As [JODate]
                                        ,JO.[DriverId]
                                        ,JO.[ContractorCategory]
                                        ,JO.[ContractorType]
                                        ,JO.[ContractorId]
                                        ,JO.[PartsRequest]
                                        ,JO.[Status]	 
	                                    ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN JO.ItemType = 1 THEN SD.ToolName
							                                    WHEN JO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN JO.ItemType = 1 THEN 'STE' 
							                                    WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)

	                                    /**,EquipmentLocation = (CASE WHEN JO.ItemType = 0 THEN EQ.ActualLocation 
							                                    WHEN JO.ItemType = 1 THEN '' 
							                                    WHEN JO.ItemType = 2 THEN ''
							                                    ELSE  '' END)**/
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
                                        ,PrintCount = (SELECT MAX(JT.PrintCount) FROM JOTransactionLogs as JT WHERE JT.JOId = JO.Id)	

	                                    ,ApproverId = (SELECT MAX(JT.ApproveId) FROM JOTransactionLogs as JT WHERE JT.JOId = JO.Id)		
	                                    ,ApproverName = (SELECT MAX(CONCAT(JGI.FirstName,' ', JGI.LastName,' ', JGI.NameExtension)) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON JT.ApproveId = JEI.Id 
				                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] JGI ON JEI.GeneralInformationsId = JGI.Id WHERE JT.JOId = JO.Id )
                                        ,ApproverPosition = (SELECT MAX(JP.PositionName) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON JT.ApproveId = JEI.Id 	
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As JP ON JP.Id = JEI.PositionsId 
                                                    WHERE JT.JOId = JO.Id)

	                                    ,EncoderName = (SELECT MAX(CONCAT(JGI.FirstName,' ', JGI.LastName,' ', JGI.NameExtension)) FROM JOTransactionLogs as JT 
												LEFT JOIN Users As U ON U.Id = JT.UserId
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON U.MLEmployeeId = JEI.Id 
				                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] JGI ON JEI.GeneralInformationsId = JGI.Id WHERE JT.JOId = JO.Id )
                                        ,EncoderPosition = (SELECT MAX(JP.PositionName) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN Users As U ON U.Id = JT.UserId
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON U.MLEmployeeId = JEI.Id 
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As JP ON JP.Id = JEI.PositionsId 
                                                    WHERE JT.JOId = JO.Id)
                                    FROM [dbo].[JOs] AS JO
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
	
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON JO.DriverId = DGI.Id WHERE JO.Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                currentRow = connection.Query<clsJobOrder>(query, new { Id = id }).FirstOrDefault();
                connection.Close();
                return currentRow;
            }
        }

        public List<clsJobOrder> SearchBy(string whereQuery)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJobOrder> Lists = new List<clsJobOrder>();
                String query = @"SELECT JO.[Id]
                                        ,JO.[BranchId]
                                        ,JO.[ChecklistNo]
                                        ,JO.[JONo]
                                        ,JO.[RefYear]
                                        ,JO.[CustomerCategory]
                                        ,JO.[CustomerType]
                                        ,JO.[CustomerId]
                                        ,JO.[Location] As EquipmentLocation
                                        ,JO.[CustomerRemarks]
                                        ,JO.[EquipmentOnBranch]
                                        ,JO.[ItemType]
                                        ,JO.[EquipmentId]
                                        ,JO.[EquipmentRemarks]
                                        ,CONVERT(DATETIME ,JO.[JODate]) As [JODate]
                                        ,JO.[DriverId]
                                        ,JO.[ContractorCategory]
                                        ,JO.[ContractorType]
                                        ,JO.[ContractorId]
                                        ,JO.[PartsRequest]
                                        ,JO.[Status]
                                        ,JO.[Mileage]
	                                    ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN JO.ItemType = 1 THEN SD.ToolName
							                                    WHEN JO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN JO.ItemType = 1 THEN 'STE' 
							                                    WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)

	                                    /**,EquipmentLocation = (CASE WHEN JO.ItemType = 0 THEN EQ.ActualLocation 
							                                    WHEN JO.ItemType = 1 THEN '' 
							                                    WHEN JO.ItemType = 2 THEN ''
							                                    ELSE  '' END)**/
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
                                        ,PrintCount = (SELECT MAX(JT.PrintCount) FROM JOTransactionLogs as JT WHERE JT.JOId = JO.Id)	

	                                    ,ApproverId = (SELECT MAX(JT.ApproveId) FROM JOTransactionLogs as JT WHERE JT.JOId = JO.Id)		
	                                    ,ApproverName = (SELECT MAX(CONCAT(JGI.FirstName,' ', JGI.LastName,' ', JGI.NameExtension)) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON JT.ApproveId = JEI.Id 
				                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] JGI ON JEI.GeneralInformationsId = JGI.Id WHERE JT.JOId = JO.Id )
                                        ,ApproverPosition = (SELECT MAX(JP.PositionName) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON JT.ApproveId = JEI.Id 	
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As JP ON JP.Id = JEI.PositionsId 
                                                    WHERE JT.JOId = JO.Id)

	                                    ,EncoderName = (SELECT MAX(CONCAT(JGI.FirstName,' ', JGI.LastName,' ', JGI.NameExtension)) FROM JOTransactionLogs as JT 
												LEFT JOIN Users As U ON U.Id = JT.UserId
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON U.MLEmployeeId = JEI.Id 
				                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] JGI ON JEI.GeneralInformationsId = JGI.Id WHERE JT.JOId = JO.Id )
                                        ,EncoderPosition = (SELECT MAX(JP.PositionName) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN Users As U ON U.Id = JT.UserId
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON U.MLEmployeeId = JEI.Id 
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As JP ON JP.Id = JEI.PositionsId 
                                                    WHERE JT.JOId = JO.Id)
                                    FROM [dbo].[JOs] AS JO
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
	
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON JO.DriverId = DGI.Id " + whereQuery;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int timeOutInSeconds = 120;
                Lists = connection.Query<clsJobOrder>(query, new { whereQuery },null,true,timeOutInSeconds).ToList();//
                connection.Close();
                return Lists;
            }
        }

        public void UpdateJOStatus(clsEnums.JOROStatus Status, Int64 JOId)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"UPDATE [dbo].[JOs]
                                   SET [Status] = @Status
                                 WHERE Id = @Id";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                connection.Execute(query, new { Status = (Int64)Status, Id = JOId});
                connection.Close();
            }
        }

        public List<clsJobOrder> GetAllFilteredEquipment(string equipmentIds)
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsJobOrder> List = new List<clsJobOrder>();
                String query = @"SELECT JO.[Id]
                                        ,JO.[BranchId]
                                        ,JO.[ChecklistNo]
                                        ,JO.[JONo]
                                        ,JO.[RefYear]
                                        ,JO.[CustomerCategory]
                                        ,JO.[CustomerType]
                                        ,JO.[CustomerId]
                                        ,JO.[Location] As EquipmentLocation
                                        ,JO.[CustomerRemarks]
                                        ,JO.[EquipmentOnBranch]
                                        ,JO.[ItemType]
                                        ,JO.[EquipmentId]
                                        ,JO.[EquipmentRemarks]
                                        ,CONVERT(DATETIME ,JO.[JODate]) As [JODate]
                                        ,JO.[DriverId]
                                        ,JO.[ContractorCategory]
                                        ,JO.[ContractorType]
                                        ,JO.[ContractorId]
                                        ,JO.[PartsRequest]
                                        ,JO.[Status]	 
	                                    ,EquipmentName = (CASE WHEN JO.ItemType = 0 THEN ET.PPETypeName 
							                                    WHEN JO.ItemType = 1 THEN SD.ToolName
							                                    WHEN JO.ItemType = 2 THEN OT.[Name]  
							                                    ELSE  '' END)
	                                    ,EquipmentCode = (CASE WHEN JO.ItemType = 0 THEN EQ.PPEName 
							                                    WHEN JO.ItemType = 1 THEN 'STE' 
							                                    WHEN JO.ItemType = 2 THEN 'SPARE PARTS'
							                                    ELSE  '' END)

	                                    /**,EquipmentLocation = (CASE WHEN JO.ItemType = 0 THEN EQ.ActualLocation 
							                                    WHEN JO.ItemType = 1 THEN '' 
							                                    WHEN JO.ItemType = 2 THEN ''
							                                    ELSE  '' END)**/
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
                                        ,PrintCount = (SELECT MAX(JT.PrintCount) FROM JOTransactionLogs as JT WHERE JT.JOId = JO.Id)	

	                                    ,ApproverId = (SELECT MAX(JT.ApproveId) FROM JOTransactionLogs as JT WHERE JT.JOId = JO.Id)		
	                                    ,ApproverName = (SELECT MAX(CONCAT(JGI.FirstName,' ', JGI.LastName,' ', JGI.NameExtension)) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON JT.ApproveId = JEI.Id 
				                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] JGI ON JEI.GeneralInformationsId = JGI.Id WHERE JT.JOId = JO.Id )
                                        ,ApproverPosition = (SELECT MAX(JP.PositionName) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON JT.ApproveId = JEI.Id 	
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As JP ON JP.Id = JEI.PositionsId 
                                                    WHERE JT.JOId = JO.Id)

	                                    ,EncoderName = (SELECT MAX(CONCAT(JGI.FirstName,' ', JGI.LastName,' ', JGI.NameExtension)) FROM JOTransactionLogs as JT 
												LEFT JOIN Users As U ON U.Id = JT.UserId
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON U.MLEmployeeId = JEI.Id 
				                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] JGI ON JEI.GeneralInformationsId = JGI.Id WHERE JT.JOId = JO.Id )
                                        ,EncoderPosition = (SELECT MAX(JP.PositionName) FROM JOTransactionLogs as JT 
		                                        LEFT JOIN Users As U ON U.Id = JT.UserId
													LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] JEI ON U.MLEmployeeId = JEI.Id 
                                                        LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].Positions As JP ON JP.Id = JEI.PositionsId 
                                                    WHERE JT.JOId = JO.Id)
                                    FROM [dbo].[JOs] AS JO
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
	
                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[GeneralInformations] DGI ON JO.DriverId = DGI.Id WHERE EquipmentId IN ("+ equipmentIds + " )";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJobOrder>(query).ToList();
                connection.Close();
                return List;
            }
        }

        //public void UpdateJODateApproved(DateTime DateApproved, Int64 JOId)
        //{
        //    using (IDbConnection connection = DbConnection.JOROConnection)
        //    {
        //        String query = @"";
        //        if (connection.State == ConnectionState.Closed)
        //        {
        //            connection.Open();
        //        }
        //        connection.Execute(query, new { joid = JOId, date = DateApproved });
        //        connection.Close();
        //    }
        //}
    }
}
