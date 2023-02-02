using FGCIJOROSystem.Domain.EquipmentHistory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.Domain.Reports;
namespace FGCIJOROSystem.DAL.Repositories.Reports
{
    public class EquipmentHistoryRepository : IRepository<clsEquipmentHistoryReport>
    {
        public void Add(clsEquipmentHistoryReport obj)
        {
            throw new NotImplementedException();
        }

        public void Update(clsEquipmentHistoryReport obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(clsEquipmentHistoryReport obj)
        {
            throw new NotImplementedException();
        }

        public List<clsEquipmentHistoryReport> GetAll()
        {
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                List<clsEquipmentHistoryReport> List = new List<clsEquipmentHistoryReport>();
                String query = @"SELECT 
                                    '0' AS EquipmentType,
                                    EQ.Id AS EquipmentId,
                                    EQ.Id As EquipmentMasterlistId,
                                    EQ.PPEName As EquipmentCode,                                        
                                    ET.PPETypeName AS EquipmentName,                                        
                                    EQ.ActualLocation As Location, 
                                    GID.FirstName AS DFirstname, GID.MiddleName AS DMiddlename, GID.LastName AS DLastName, GID.NameExtension AS DNameExtension                                        
                                FROM [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ 
                                    INNER JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id  
                                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS ER ON EQ.Id =  ER.DescriptionAndStatusId 
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GID ON ER.MLDriversNameId = GID.Id 
                                                LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[EmployeesInformations] EID ON GID.Id = EID.GeneralInformationsId 
                                                    LEFT JOIN [FGCIHRDMasterlistSystemDB].[dbo].[Positions] PD ON EID.PositionsId = PD.Id ";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsEquipmentHistoryReport>(query).ToList();
                connection.Close();
                return List;
            }
        }
        //, bool isDate, DateTime DateStart, DateTime DateEnd
        public List<clsEquipmentHistoryReport> GetEquipmentHistory(bool isEquipment, string equipmentIds, bool isDate, DateTime DateStart, DateTime DateEnd)
        {
            using (IDbConnection connection = DbConnection.InventoryConnection)
            {
                List<clsEquipmentHistoryReport> List = new List<clsEquipmentHistoryReport>();
                String query = @"SELECT InventoryDetailWithdrawalId,
									Convert(varchar(10),WithdrawalDate,101) WithdrawalDate,
									ProductName,
									WithdrawalQuantity,
									UnitName,
									BranchId,
									BranchName,
									ProjectId,
									Project,
									EmployeeName,
									PONumber,
									PRSNo,
									WithdrawalSlipNumber,
									MDRSNo,
									WasteStatusId,
									WasteStatus,
									Remarks
									--SupplierName, 
									--MRAR,
									--WasteCategoryId,
									--Price,
									--Amount,
									--IsCancelled,
									--ProjectLocation
									
                               FROM (SELECT withDet.Id AS InventoryDetailWithdrawalId,           
									CONVERT(varchar(10),mat.WithdrawalDate, 101) AS WithdrawalDate,
									mat.IsCancelled,          
									withDet.WithdrawalQuantity,           
									mat.WithdrawalSlipNumber,           
									ws.WasteStatusName AS WasteStatus,           
									withDet.WasteStatusId AS WasteCategoryId,
									withDet.WithdrawalPrice as Price,
									withDet.WithdrawalAmount as Amount,
									withDet.PRSNumber AS PRSNo,
									withDet.WithdrawalRemarks AS Remarks,
									mat.FGCIEquipmentId AS ProjectId,
									ISNULL(CASE WHEN withDet.PODetailsId = 0 THEN  prod.ProductName       
									ELSE CASE WHEN withDet.IsOldPO = 1 THEN  POProd.ProductName       
									ELSE (SELECT prodDet.ProductDetailName FROM FGCIEquipmentPOSystemDB.dbo.PODetails pod       
									LEFT JOIN FGCIProductMasterlistDB.dbo.ProductDetails prodDet ON pod.ProductId = prodDet.Id WHERE pod.Id = withDet.PODetailsId)  END END, '') COLLATE Latin1_General_CI_AS AS ProductName,       
									un.UnitName,       
									CASE WHEN mat.EmployeeId <> 0 AND mat.NonEmployeeId = 0 THEN CONCAT(genInfo.FirstName, ' ', genInfo.LastName) ELSE CASE WHEN mat.EmployeeId = 0 AND mat.NonEmployeeId <> 0 THEN CONCAT(nonEmp.FirstName, ' ', nonEmp.LastName) END END AS EmployeeName,           
									CASE WHEN withDet.IsOldPO = 1 THEN ISNULL(POdet.POIdNo, '')       
									ELSE (SELECT po.PONumber FROM FGCIEquipmentPOSystemDB.dbo.PODetails podets     
									LEFT JOIN FGCIEquipmentPOSystemDB.dbo.POs po ON podets.POId = po.Id WHERE podets.Id = withDet.PODetailsId) COLLATE Latin1_General_CI_AS END AS PONumber,           
									ISNULL(CASE WHEN mat.ProjectId = 0 AND mat.EFileOthersId = 0 AND mat.EFileDepartmentsId <> 0 THEN ISNULL(masDepartment.DepartmentName, '') ELSE CASE WHEN mat.ProjectId = 0 AND mat.EFileDepartmentsId = 0 AND mat.EFileOthersId <> 0 THEN ISNULL(others.ProjectName, '') ELSE CASE WHEN mat.EFileDepartmentsId = 0 AND mat.EFileOthersId = 0 AND mat.ProjectId <> 0 THEN ISNULL(project.ProjectShortcutName, '')      
									ELSE CASE WHEN mat.EFileDepartmentsId = 0 AND mat.EFileOthersId = 0 AND mat.ProjectId = 0 AND mat.FGCIEquipmentId = 0 AND mat.OthersEquipmentId = 0 AND mat.SupplierId = 0 AND       
									mat.STEId = 0 THEN BranchName ELSE CASE WHEN mat.FGCIEquipmentId <> 0 THEN CASE WHEN FGEquipment.PlateNo <> '' THEN CONCAT(FGEquipment.PPEName,'(', FGEquipment.PlateNo,')') ELSE FGEquipment.PPEName END  ELSE CASE WHEN mat.OthersEquipmentId <> 0 THEN CASE WHEN OtherEquipment.PlateNumber <> '' THEN CONCAT(OtherEquipment.PPEName,'(', OtherEquipment.PlateNumber,')') ELSE OtherEquipment.PPEName END      ELSE CASE WHEN mat.SupplierId       
									<> 0 THEN(SELECT supProduct.SupplierName          
                            FROM FGCIProductMasterlistDB.dbo.Suppliers supProduct          
									WHERE mat.SupplierId = supProduct.Id) COLLATE Latin1_General_CI_AS ELSE CASE WHEN mat.STEId <> 0 THEN          
									(SELECT stocDet.ToolName      
                            FROM StockDetails stocDet         
									WHERE mat.STEId = stocDet.Id) END END END END END END END END, '') AS Project,    
									ISNULL(CASE WHEN mat.POId = 0 AND withDet.PODetailsId <> 0 AND mat.IsOldPO = 1 THEN sup.SupplierName             
									ELSE CASE WHEN mat.POId = 0 AND withDet.PODetailsId <> 0 AND withDet.IsOldPO = 0 THEN             
									(SELECT CASE WHEN pos.SupplierCategoryId = 0 OR pos.SupplierCategoryId IS NULL THEN (SELECT sup.SupplierName FROM FGCIProductMasterlistDB.dbo.Suppliers sup WHERE pos.SupplierId = sup.Id)       
									ELSE CASE WHEN pos.SupplierCategoryId = 1 THEN (SELECT project.ProjectName FROM FGCIProjectEFileCabinetDB.dbo.ProjectDetails project WHERE pos.SupplierId = project.Id)       
									ELSE CASE WHEN pos.SupplierCategoryId = 2 THEN (SELECT masterlistDepartment.DepartmentName FROM FGCIProjectEFileCabinetDB.dbo.AdditionalProjectsDepartment projectDepartment        
																				LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments masterlistDepartment ON projectDepartment.MasterlistDepartmentId = masterlistDepartment.Id WHERE pos.SupplierId = projectDepartment.Id)       
									ELSE CASE WHEN pos.SupplierCategoryId = 3 THEN  (SELECT sections.SectionName FROM FGCIHRDMasterlistSystemDB.dbo.Sections sections WHERE pos.SupplierId = sections.Id)       
									ELSE CASE WHEN pos.SupplierCategoryId = 4 THEN (SELECT otherProjects.ProjectName FROM FGCIProjectEFileCabinetDB.dbo.AdditionalProjectsOthers otherProjects WHERE pos.SupplierId = otherProjects.Id)       
									END END END END END  FROM FGCIEquipmentPOSystemDB.dbo.PODetails podets          
									LEFT JOIN FGCIEquipmentPOSystemDB.dbo.POs pos ON podets.POId = pos.Id        
									WHERE podets.Id = withDet.PODetailsId)            
									ELSE CASE WHEN mat.POId <> 0  AND mat.IsOldPO = 1 AND withDet.PODetailsId = 0  THEN Sup.SupplierName        
									ELSE CASE WHEN mat.POId <> 0  AND mat.IsOldPO = 0 AND withDet.PODetailsId = 0 THEN             
									(SELECT CASE WHEN pos.SupplierCategoryId = 0 OR pos.SupplierCategoryId IS NULL THEN (SELECT sup.SupplierName FROM FGCIProductMasterlistDB.dbo.Suppliers sup WHERE pos.SupplierId = sup.Id)       
									ELSE CASE WHEN pos.SupplierCategoryId = 1 THEN (SELECT project.ProjectName FROM FGCIProjectEFileCabinetDB.dbo.ProjectDetails project WHERE pos.SupplierId = project.Id)       
									ELSE CASE WHEN pos.SupplierCategoryId = 2 THEN (SELECT masterlistDepartment.DepartmentName FROM FGCIProjectEFileCabinetDB.dbo.AdditionalProjectsDepartment projectDepartment        
																				LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments masterlistDepartment ON projectDepartment.MasterlistDepartmentId = masterlistDepartment.Id WHERE pos.SupplierId = projectDepartment.Id)       
									ELSE CASE WHEN pos.SupplierCategoryId = 3 THEN  (SELECT sections.SectionName FROM FGCIHRDMasterlistSystemDB.dbo.Sections sections WHERE pos.SupplierId = sections.Id)       
									ELSE CASE WHEN pos.SupplierCategoryId = 4 THEN (SELECT otherProjects.ProjectName FROM FGCIProjectEFileCabinetDB.dbo.AdditionalProjectsOthers otherProjects WHERE pos.SupplierId = otherProjects.Id)       
									END END END END END       
                            FROM  FGCIEquipmentPOSystemDB.dbo.POs pos             
									LEFT JOIN FGCIProductMasterlistDB.dbo.Suppliers newPOSup on pos.SupplierId = newPOSup.Id        
									WHERE pos.Id = mat.POId)             
									ELSE CASE WHEN mat.POId <> 0 AND withDet.PODetailsId <> 0 AND mat.IsOldPO = 1 THEN sup.SupplierName          
									ELSE CASE WHEN mat.POId <> 0 AND withDet.PODetailsId <> 0 AND mat.IsOldPO = 0 THEN             
									(SELECT CASE WHEN pos.SupplierCategoryId = 0 OR pos.SupplierCategoryId IS NULL THEN (SELECT sup.SupplierName FROM FGCIProductMasterlistDB.dbo.Suppliers sup WHERE pos.SupplierId = sup.Id)       
									WHEN pos.SupplierCategoryId = 1 THEN (SELECT project.ProjectName FROM FGCIProjectEFileCabinetDB.dbo.ProjectDetails project WHERE pos.SupplierId = project.Id)       
									WHEN pos.SupplierCategoryId = 2 THEN (SELECT masterlistDepartment.DepartmentName FROM FGCIProjectEFileCabinetDB.dbo.AdditionalProjectsDepartment projectDepartment        
																				LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.Departments masterlistDepartment ON projectDepartment.MasterlistDepartmentId = masterlistDepartment.Id WHERE pos.SupplierId = projectDepartment.Id)       
									WHEN pos.SupplierCategoryId = 3 THEN  (SELECT sections.SectionName FROM FGCIHRDMasterlistSystemDB.dbo.Sections sections WHERE pos.SupplierId = sections.Id)       
									WHEN pos.SupplierCategoryId = 4 THEN (SELECT otherProjects.ProjectName FROM FGCIProjectEFileCabinetDB.dbo.AdditionalProjectsOthers otherProjects WHERE pos.SupplierId = otherProjects.Id)       
									END FROM  FGCIEquipmentPOSystemDB.dbo.POs pos WHERE pos.Id = mat.POId)        
									ELSE branch.BranchName END END END END END END,branch.BranchName) COLLATE SQL_Latin1_General_CP1_CI_AS as SupplierName,    
									mat.MRAR, withDet.WasteStatusId, branch.BranchName, mat.BranchId,ISNULL(withDet.MDRSNo,'') as MDRSNo, ISNULL(mat.Location,'') as ProjectLocation         
                            FROM  MaterialWithdrawalDetailsInventory AS withDet LEFT OUTER JOIN          
									MaterialWithdrawalsInventory AS mat ON withDet.InventoryWithdrawalId = mat.Id LEFT OUTER JOIN          
									WasteStatus AS ws ON withDet.WasteStatusId = ws.Id LEFT OUTER JOIN          
									InventoryProducts AS prod ON withDet.InventoryProductsId = prod.Id LEFT OUTER JOIN          
									FGCIPOSystemDB.dbo.PODetails AS POdet ON withDet.PODetailsId = POdet.Id LEFT OUTER JOIN          
									FGCIPOSystemDB.dbo.Products AS POProd ON POdet.ProductId = POProd.Id LEFT OUTER JOIN          
									FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AS genInfo ON genInfo.Id = mat.EmployeeId LEFT OUTER JOIN       
									FGCIHRDMasterlistSystemDB.dbo.NonEmployeeMasterlist AS nonEmp ON nonEmp.Id = mat.NonEmployeeId LEFT OUTER JOIN       
									Units AS un ON withDet.UnitId = un.Id LEFT OUTER JOIN          
									FGCIProjectEFileCabinetDB.dbo.ProjectDetails AS project ON mat.ProjectId = project.Id LEFT OUTER JOIN          
									FGCIProjectEFileCabinetDB.dbo.AdditionalProjectsDepartment AS department ON mat.EFileDepartmentsId = department.Id LEFT OUTER JOIN       
									FGCIProjectEFileCabinetDB.dbo.AdditionalProjectsOthers AS others ON mat.EFileOthersID = others.Id LEFT OUTER JOIN          
									FGCIHRDMasterlistSystemDB.dbo.Departments AS masDepartment ON department.MasterlistDepartmentId = masDepartment.Id LEFT OUTER JOIN       
									FGCIPOSystemDB.dbo.POes AS PO ON mat.POId = PO.Id LEFT OUTER JOIN          
									FGCIPOSystemDB.dbo.Suppliers AS Sup ON PO.SupplierId = Sup.Id LEFT OUTER JOIN          
									FGCIAccountingPPEMonitoringDB.dbo.DescriptionAndStatus AS FGEquipment ON mat.FGCIEquipmentId = FGEquipment.Id LEFT OUTER JOIN       
									FGCIAccountingPPEMonitoringDB.dbo.Others AS OtherEquipment ON mat.OthersEquipmentId = OtherEquipment.Id LEFT OUTER JOIN          
									StockBranch AS branch ON branch.Id = mat.BranchId
									WHERE  (mat.FGCIEquipmentId <> 0 OR mat.OthersEquipmentId <> 0) AND BranchId = 4
									) AS trans
									WHERE        (InventoryDetailWithdrawalId <> 0) AND IsCancelled = 0 AND WithdrawalDate BETWEEN @DateStart AND @DateEnd";
                if (isEquipment)
                {
                    query += " AND ProjectId IN ( " + equipmentIds + " )";
                }
                //if (isDate)
                //{
                //    query += " AND CONVERT(varchar(10),WithdrawalDate,101) BETWEEN CONVERT(varchar(10),'" + DateStart.ToString() + "',101) AND CONVERT(varchar(10),'" + DateStart.ToString() + "',101)";
                //}
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsEquipmentHistoryReport>(query, new {DateStart = DateStart, DateEnd = DateEnd}).ToList();
                connection.Close();
                return List;
            }
        }
        clsEquipmentHistoryReport IRepository<clsEquipmentHistoryReport>.FindByID(long id)
        {
            throw new NotImplementedException();
        }

        List<clsEquipmentHistoryReport> IRepository<clsEquipmentHistoryReport>.SearchBy(string whereQuery)
        {
            throw new NotImplementedException();
        }

    }
}
//SELECT EQ.Id as EquipmentId
//                                          ,EQ.PPEName AS EquipmentCode
//                                          ,ET.PPETypeName AS EquipmentName
//                                          ,EC.PPEClassName AS EquipmentClass
//                                      FROM [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS EQ
//                                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS ET ON EQ.PPETypeId = ET.Id
//                                        LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS EC ON EQ.PPEClassId = EC.Id
//                                            group by eq.Id, PPEName, et.PPETypeName, ec.PPEClassName";

//AND (WithdrawalDate BETWEEN (convert(datetime, @DateStart,101)) AND (convert(datetime,@DateEnd,101)))