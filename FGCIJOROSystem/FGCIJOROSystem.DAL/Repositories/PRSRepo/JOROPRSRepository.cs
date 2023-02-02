using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.PRS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FGCIJOROSystem.Domain.RescueOrder;
namespace FGCIJOROSystem.DAL.Repositories.PRSRepo
{
    public class JOROPRSRepository
    {
        public List<clsJOROPRS> GetJOPRS(clsJobOrder jo)
        {
            List<clsJOROPRS> List = new List<clsJOROPRS>();
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"SELECT 
                                        P.Id, P.DateRequested, P.DateReceived, P.Location, P.PRSStatus, P.Remarks, P.PRSCategory, P.PRSCount, P.PRSControlNumber, 
                                        CONCAT(RQB.FirstName, ' ', RQB.MiddleName, ' ', RQB.LastName, ' ', RQB.NameExtension) AS RequestedBy, 
                                        CONCAT(CB.FirstName, ' ', CB.MiddleName, ' ', CB.LastName, ' ', CB.NameExtension) AS CheckedBy, 
                                        CONCAT(VB.FirstName, ' ', VB.MiddleName, ' ', VB.LastName, ' ', VB.NameExtension) AS VerifiedBy, 
                                        CONCAT(AB.FirstName, ' ', AB.MiddleName, ' ', AB.LastName, ' ', AB.NameExtension) AS ApprovedBy, 
                                        CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ', GI.LastName, ' ', GI.NameExtension) AS PreparedBy, 
                                        PD.Id AS PRSDetailId, PD.Specification, PD.Quantity, PD.IPOWQuantity, PD.RemBalQuantity, PD.Remarks AS DetailRemarks, 
                                        U.Unitname, 
                                        PR.ProductDetailName As Product,
										PD.ProductId,
										PD.DefaultProductId
                                    FROM 
                                        FGCIPRSSystem.dbo.PRSJO JP 
                                            INNER JOIN FGCIPRSSystem.dbo.PRS P ON JP.PRSId = P.Id 
                                                LEFT JOIN FGCIPRSSystem.dbo.UserAccounts UA ON P.PreparedById = UA.MasterlistId
                                                        LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI ON GI.Id = UA.MasterlistId
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations RQB ON RQB.Id = P.RequestedById
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations CB ON CB.Id = P.CheckedById
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations VB ON VB.Id = P.VerifiedById
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AB ON AB.Id = P.ApprovedById 
                                                LEFT JOIN FGCIPRSSystem.dbo.PRSDetails PD ON P.Id = PD.PRSId 
                                                    LEFT JOIN FGCIProductMasterlistDB.dbo.Units U ON PD.UnitId = U.Id 
                                                    LEFT JOIN FGCIProductMasterlistDB.dbo.ProductDetails PR ON PD.ProductId = PR.Id 
                                    WHERE 
                                       JP.JOid = @Id AND JP.JOROCategory = 0 AND JP.IsNewJORO = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOROPRS>(query, jo).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsJOROPRS> GetROPRS(clsRescueOrder ro)
        {
            List<clsJOROPRS> List = new List<clsJOROPRS>();
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"SELECT 
                                        P.Id, P.DateRequested, P.DateReceived, P.Location, P.PRSStatus, P.Remarks, P.PRSCategory, P.PRSCount, P.PRSControlNumber, 
                                        CONCAT(RQB.FirstName, ' ', RQB.MiddleName, ' ', RQB.LastName, ' ', RQB.NameExtension) AS RequestedBy, 
                                        CONCAT(CB.FirstName, ' ', CB.MiddleName, ' ', CB.LastName, ' ', CB.NameExtension) AS CheckedBy, 
                                        CONCAT(VB.FirstName, ' ', VB.MiddleName, ' ', VB.LastName, ' ', VB.NameExtension) AS VerifiedBy, 
                                        CONCAT(AB.FirstName, ' ', AB.MiddleName, ' ', AB.LastName, ' ', AB.NameExtension) AS ApprovedBy, 
                                        CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ', GI.LastName, ' ', GI.NameExtension) AS PreparedBy, 
                                        PD.Id AS PRSDetailId, PD.Specification, PD.Quantity, PD.IPOWQuantity, PD.RemBalQuantity, PD.Remarks AS DetailRemarks, 
                                        U.Unitname, 
                                        PR.ProductDetailName As Product,
										PD.ProductId,
										PD.DefaultProductId
                                    FROM 
                                        FGCIPRSSystem.dbo.PRSJO JP 
                                            INNER JOIN FGCIPRSSystem.dbo.PRS P ON JP.PRSId = P.Id 
                                                LEFT JOIN FGCIPRSSystem.dbo.UserAccounts UA ON P.PreparedById = UA.MasterlistId
                                                        LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI ON GI.Id = UA.MasterlistId
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations RQB ON RQB.Id = P.RequestedById
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations CB ON CB.Id = P.CheckedById
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations VB ON VB.Id = P.VerifiedById
                                                LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AB ON AB.Id = P.ApprovedById 
                                                LEFT JOIN FGCIPRSSystem.dbo.PRSDetails PD ON P.Id = PD.PRSId 
                                                    LEFT JOIN FGCIProductMasterlistDB.dbo.Units U ON PD.UnitId = U.Id 
                                                    LEFT JOIN FGCIProductMasterlistDB.dbo.ProductDetails PR ON PD.ProductId = PR.Id 
                                    WHERE 
                                       JP.JOid = @Id AND JP.JOROCategory = 1 AND JP.IsNewJORO = 1";
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOROPRS>(query, ro).ToList();
                connection.Close();
                return List;
            }
        }
        public List<clsJOROPRS> GetPRSStatusList(String EquipmentName, String Requestor, 
                                            DateTime StartDate, DateTime EndDate, 
                                            Boolean IsDate, Boolean IsDatePrepared,
                                            Boolean IsEquipment, Boolean IsRequestor,
                                            Boolean IsPRSStatus, String PRSStatusId)
                                            
        {
            List<clsJOROPRS> List = new List<clsJOROPRS>();
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"SELECT
                                    P.Id, P.DateRequested, P.DateReceived, P.Location, P.PRSStatus, P.Remarks, P.PRSCategory, P.PRSCount, P.PRSControlNumber,
                                    CONCAT(RQB.FirstName, ' ', RQB.MiddleName, ' ', RQB.LastName, ' ', RQB.NameExtension) AS RequestedBy, 
                                    CONCAT(CB.FirstName, ' ', CB.MiddleName, ' ', CB.LastName, ' ', CB.NameExtension) AS CheckedBy, 
                                    CONCAT(VB.FirstName, ' ', VB.MiddleName, ' ', VB.LastName, ' ', VB.NameExtension) AS VerifiedBy, 
                                    CONCAT(AB.FirstName, ' ', AB.MiddleName, ' ', AB.LastName, ' ', AB.NameExtension) AS ApprovedBy, 
                                    CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ', GI.LastName, ' ', GI.NameExtension) AS PreparedBy, 
                                    PD.Id AS PRSDetailId, PD.Specification, PD.Quantity, PD.IPOWQuantity, PD.RemBalQuantity, PD.Remarks AS DetailRemarks, 
                                    U.Unitname, 
                                    PR.ProductDetailName As Product,
	                                PD.ProductId,
	                                JP.JOROCategory,
	                                (CASE JP.JOROCategory WHEN 0 THEN 
		                                CASE JO.ItemType 
			                                WHEN 0 THEN JEQ.PPEName + ' ' + JEQ.PlateNo
			                                WHEN 1 THEN JSD.ToolName
			                                WHEN 2 THEN JOT.Name
			                                ELSE '' END
	                                ELSE 
		                                CASE RO.ItemType 
			                                WHEN 0 THEN REQ.PPEName 
			                                WHEN 1 THEN RSD.ToolName
			                                WHEN 2 THEN ROT.Name
			                                ELSE '' END
	                                END) AS EquipmentName,
	                                (CASE JP.JOROCategory WHEN  0 THEN 
		                                JO.JONo
	                                WHEN  1 THEN
		                                RO.RONo
	                                ELSE 0	END) AS JORONo,
	                                (CASE JP.JOROCategory WHEN  0 THEN 
		                                JO.RefYear
	                                WHEN  1 THEN 
		                                RO.RefYear
	                                ELSE YEAR(GETDATE()) END) AS RefYear, 
	                                (CASE JP.JOROCategory WHEN  0 THEN 
		                                JO.Status
	                                WHEN  1 THEN 
		                                 RO.Status
	                                END) AS JOROStatus
                                FROM 
                                    FGCIPRSSystem.dbo.PRSJO JP 
                                        INNER JOIN FGCIPRSSystem.dbo.PRS P ON JP.PRSId = P.Id 
                                            LEFT JOIN FGCIPRSSystem.dbo.UserAccounts UA ON P.PreparedById = UA.MasterlistId
                                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI ON GI.Id = UA.MasterlistId
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations RQB ON RQB.Id = P.RequestedById
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations CB ON CB.Id = P.CheckedById
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations VB ON VB.Id = P.VerifiedById
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AB ON AB.Id = P.ApprovedById 
                                            LEFT JOIN FGCIPRSSystem.dbo.PRSDetails PD ON P.Id = PD.PRSId 
                                                LEFT JOIN FGCIProductMasterlistDB.dbo.Units U ON PD.UnitId = U.Id 
                                                LEFT JOIN FGCIProductMasterlistDB.dbo.ProductDetails PR ON PD.ProductId = PR.Id 

			                                LEFT JOIN FGCIJOROSystemDB.dbo.JOs JO ON JO.Id = JP.JOId AND JP.JOROCategory = 0		
			                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS JEQ ON JEQ.Id = JO.EquipmentId
                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS JET ON JEQ.PPETypeId = JET.Id
	                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS JEC ON JEQ.PPEClassId = JEC.Id
		                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS JER ON JEQ.Id =  JER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails JSD  ON JSD.Id = JO.EquipmentId
                                                LEFT JOIN OtherEquipments JOT ON JOT.Id = JO.EquipmentId

			                                LEFT JOIN FGCIJOROSystemDB.dbo.ROs RO ON RO.Id = JP.JOId AND JP.JOROCategory = 1
			                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS REQ ON REQ.Id = RO.EquipmentId
                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS RET ON REQ.PPETypeId = RET.Id 
	                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS REC ON REQ.PPEClassId = REC.Id
		                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS RER ON REQ.Id = RER.DescriptionAndStatusId 
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails RSD  ON RSD.Id = RO.EquipmentId
                                                LEFT JOIN OtherEquipments ROT ON ROT.Id = RO.EquipmentId
                                WHERE 
                                     JP.IsNewJORO = 1 "; // AND REQ.EquipmentStatusId = 3";
                //String query = @"SELECT 
                //                    P.Id, P.DateRequested, P.DateReceived, P.Location, P.PRSStatus, P.Remarks, P.PRSCategory, P.PRSCount, P.PRSControlNumber,
                //                    CONCAT(RQB.FirstName, ' ', RQB.MiddleName, ' ', RQB.LastName, ' ', RQB.NameExtension) AS RequestedBy, 
                //                    CONCAT(CB.FirstName, ' ', CB.MiddleName, ' ', CB.LastName, ' ', CB.NameExtension) AS CheckedBy, 
                //                    CONCAT(VB.FirstName, ' ', VB.MiddleName, ' ', VB.LastName, ' ', VB.NameExtension) AS VerifiedBy, 
                //                    CONCAT(AB.FirstName, ' ', AB.MiddleName, ' ', AB.LastName, ' ', AB.NameExtension) AS ApprovedBy, 
                //                    CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ', GI.LastName, ' ', GI.NameExtension) AS PreparedBy, 
                //                    PD.Id AS PRSDetailId, PD.Specification, PD.Quantity, PD.IPOWQuantity, PD.RemBalQuantity, PD.Remarks AS DetailRemarks, 
                //                    U.Unitname, 
                //                    PR.ProductDetailName As Product,
                //                 PD.ProductId,
                //                 JP.JOROCategory,
                //                 (CASE JP.JOROCategory WHEN 0 THEN 
                //                  CASE JO.ItemType 
                //                   WHEN 0 THEN JEQ.PPEName + ' ' + JEQ.PlateNo
                //                   WHEN 1 THEN JSD.ToolName
                //                   WHEN 2 THEN JOT.Name
                //                   ELSE '' END
                //                 ELSE 
                //                  CASE RO.ItemType 
                //                   WHEN 0 THEN REQ.PPEName 
                //                   WHEN 1 THEN RSD.ToolName
                //                   WHEN 2 THEN ROT.Name
                //                   ELSE '' END
                //                 END) AS EquipmentName,
                //                 (CASE JP.JOROCategory WHEN  0 THEN 
                //                  JO.JONo
                //                 WHEN  1 THEN
                //                  RO.RONo
                //                 ELSE 0	END) AS JORONo,
                //                 (CASE JP.JOROCategory WHEN  0 THEN 
                //                  JO.RefYear
                //                 WHEN  1 THEN 
                //                  RO.RefYear
                //                 ELSE YEAR(GETDATE()) END) AS RefYear
                //                FROM 
                //                    FGCIPRSSystem.dbo.PRSJO JP 
                //                        INNER JOIN FGCIPRSSystem.dbo.PRS P ON JP.PRSId = P.Id 
                //                            LEFT JOIN FGCIPRSSystem.dbo.UserAccounts UA ON P.PreparedById = UA.MasterlistId
                //                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI ON GI.Id = UA.MasterlistId
                //                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations RQB ON RQB.Id = P.RequestedById
                //                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations CB ON CB.Id = P.CheckedById
                //                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations VB ON VB.Id = P.VerifiedById
                //                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AB ON AB.Id = P.ApprovedById 
                //                            LEFT JOIN FGCIPRSSystem.dbo.PRSDetails PD ON P.Id = PD.PRSId 
                //                                LEFT JOIN FGCIProductMasterlistDB.dbo.Units U ON PD.UnitId = U.Id 
                //                                LEFT JOIN FGCIProductMasterlistDB.dbo.ProductDetails PR ON PD.ProductId = PR.Id 

                //                   LEFT JOIN FGCIJOROSystemDB.dbo.JOs JO ON JO.Id = JP.JOId AND JP.JOROCategory = 0		
                //                   LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS JEQ ON JEQ.Id = JO.EquipmentId
                //                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS JET ON JEQ.PPETypeId = JET.Id
                //                                 LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS JEC ON JEQ.PPEClassId = JEC.Id
                //                                  LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS JER ON JEQ.Id =  JER.DescriptionAndStatusId
                //                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails JSD  ON JSD.Id = JO.EquipmentId
                //                                LEFT JOIN OtherEquipments JOT ON JOT.Id = JO.EquipmentId

                //                   LEFT JOIN FGCIJOROSystemDB.dbo.ROs RO ON RO.Id = JP.JOId AND JP.JOROCategory = 1
                //                   LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS REQ ON REQ.Id = RO.EquipmentId
                //                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS RET ON REQ.PPETypeId = RET.Id 
                //                                 LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS REC ON REQ.PPEClassId = REC.Id
                //                                  LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS RER ON REQ.Id = RER.DescriptionAndStatusId 
                //                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails RSD  ON RSD.Id = RO.EquipmentId
                //                                LEFT JOIN OtherEquipments ROT ON ROT.Id = RO.EquipmentId
                //                WHERE 
                //                    JP.IsNewJORO = 1 ";
                if (IsDate)
                {
                    if (IsDatePrepared)
                    {
                        query += " AND CONVERT(varchar,P.DateRequested,101) BETWEEN CONVERT(varchar,'" + StartDate.ToString("MM/dd/yyyy") + "',101) AND CONVERT(varchar,'" + EndDate.ToString("MM/dd/yyyy") + "',101)";
                    }
                    else
                    {
                        query += " AND CONVERT(varchar,P.DateReceived,101) BETWEEN CONVERT(varchar,'" + StartDate.ToString("MM/dd/yyyy") + "',101) AND CONVERT(varchar,'" + EndDate.ToString("MM/dd/yyyy") + "',101)";
                    }

                }
                if (IsEquipment)
                {
                    query += " AND (JEQ.Id IN (" + EquipmentName + ") OR REQ.Id IN (" + EquipmentName + "))";
                }
                if (IsRequestor)
                {
                    query += " AND CONCAT(RQB.FirstName, ' ', RQB.MiddleName, ' ', RQB.LastName, ' ', RQB.NameExtension) LIKE '%" + Requestor + "%'";
                }
                if (IsPRSStatus) //added
                {
                    query += " AND PRSStatus IN (" + PRSStatusId + ")";
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOROPRS>(query).ToList();
                connection.Close();
                return List;
            }
        }

        public List<clsJOROPRS> GetJOROStatusList(String EquipmentName, String Requestor,
                                            DateTime StartDate, DateTime EndDate,
                                            Boolean IsDate, Boolean IsDatePrepared,
                                            Boolean IsEquipment, Boolean IsRequestor,
                                            Boolean IsJOROStatus, String JOROStatusId)

        {
            List<clsJOROPRS> List = new List<clsJOROPRS>();
            using (IDbConnection connection = DbConnection.JOROConnection)
            {
                String query = @"SELECT
                                    P.Id, P.DateRequested, P.DateReceived, P.Location, P.PRSStatus, P.Remarks, P.PRSCategory, P.PRSCount, P.PRSControlNumber,
                                    CONCAT(RQB.FirstName, ' ', RQB.MiddleName, ' ', RQB.LastName, ' ', RQB.NameExtension) AS RequestedBy, 
                                    CONCAT(CB.FirstName, ' ', CB.MiddleName, ' ', CB.LastName, ' ', CB.NameExtension) AS CheckedBy, 
                                    CONCAT(VB.FirstName, ' ', VB.MiddleName, ' ', VB.LastName, ' ', VB.NameExtension) AS VerifiedBy, 
                                    CONCAT(AB.FirstName, ' ', AB.MiddleName, ' ', AB.LastName, ' ', AB.NameExtension) AS ApprovedBy, 
                                    CONCAT(GI.FirstName, ' ', GI.MiddleName, ' ', GI.LastName, ' ', GI.NameExtension) AS PreparedBy, 
                                    PD.Id AS PRSDetailId, PD.Specification, PD.Quantity, PD.IPOWQuantity, PD.RemBalQuantity, PD.Remarks AS DetailRemarks, 
                                    U.Unitname, 
                                    PR.ProductDetailName As Product,
	                                PD.ProductId,
	                                JP.JOROCategory,
	                                (CASE JP.JOROCategory WHEN 0 THEN 
		                                CASE JO.ItemType 
			                                WHEN 0 THEN JEQ.PPEName + ' ' + JEQ.PlateNo
			                                WHEN 1 THEN JSD.ToolName
			                                WHEN 2 THEN JOT.Name
			                                ELSE '' END
	                                ELSE 
		                                CASE RO.ItemType 
			                                WHEN 0 THEN REQ.PPEName 
			                                WHEN 1 THEN RSD.ToolName
			                                WHEN 2 THEN ROT.Name
			                                ELSE '' END
	                                END) AS EquipmentName,
	                                (CASE JP.JOROCategory WHEN  0 THEN 
		                                JO.JONo
	                                WHEN  1 THEN
		                                RO.RONo
	                                ELSE 0	END) AS JORONo,
	                                (CASE JP.JOROCategory WHEN  0 THEN 
		                                JO.RefYear
	                                WHEN  1 THEN 
		                                RO.RefYear
	                                ELSE YEAR(GETDATE()) END) AS RefYear, 
	                                (CASE JP.JOROCategory WHEN  0 THEN 
		                                JO.Status
	                                WHEN  1 THEN 
		                                 RO.Status
	                                END) AS JOROStatus
                                FROM 
                                    FGCIPRSSystem.dbo.PRSJO JP 
                                        INNER JOIN FGCIPRSSystem.dbo.PRS P ON JP.PRSId = P.Id 
                                            LEFT JOIN FGCIPRSSystem.dbo.UserAccounts UA ON P.PreparedById = UA.MasterlistId
                                                    LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations GI ON GI.Id = UA.MasterlistId
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations RQB ON RQB.Id = P.RequestedById
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations CB ON CB.Id = P.CheckedById
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations VB ON VB.Id = P.VerifiedById
                                            LEFT JOIN FGCIHRDMasterlistSystemDB.dbo.GeneralInformations AB ON AB.Id = P.ApprovedById 
                                            LEFT JOIN FGCIPRSSystem.dbo.PRSDetails PD ON P.Id = PD.PRSId 
                                                LEFT JOIN FGCIProductMasterlistDB.dbo.Units U ON PD.UnitId = U.Id 
                                                LEFT JOIN FGCIProductMasterlistDB.dbo.ProductDetails PR ON PD.ProductId = PR.Id 

			                                LEFT JOIN FGCIJOROSystemDB.dbo.JOs JO ON JO.Id = JP.JOId AND JP.JOROCategory = 0		
			                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS JEQ ON JEQ.Id = JO.EquipmentId
                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS JET ON JEQ.PPETypeId = JET.Id
	                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS JEC ON JEQ.PPEClassId = JEC.Id
		                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS JER ON JEQ.Id =  JER.DescriptionAndStatusId
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails JSD  ON JSD.Id = JO.EquipmentId
                                                LEFT JOIN OtherEquipments JOT ON JOT.Id = JO.EquipmentId

			                                LEFT JOIN FGCIJOROSystemDB.dbo.ROs RO ON RO.Id = JP.JOId AND JP.JOROCategory = 1
			                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[DescriptionAndStatus] AS REQ ON REQ.Id = RO.EquipmentId
                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPETypes] AS RET ON REQ.PPETypeId = RET.Id 
	                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].[PPEClasses] AS REC ON REQ.PPEClassId = REC.Id
		                                                LEFT JOIN [FGCIAccountingPPEMonitoringDB].[dbo].RegistrationRenewals AS RER ON REQ.Id = RER.DescriptionAndStatusId 
                                                LEFT JOIN FGCILInventoryDB.dbo.StockDetails RSD  ON RSD.Id = RO.EquipmentId
                                                LEFT JOIN OtherEquipments ROT ON ROT.Id = RO.EquipmentId
                                WHERE"; // AND REQ.EquipmentStatusId = 3";
                if (IsDate)
                {
                    if (IsDatePrepared)
                    {
                        query += " JP.IsNewJORO = 1 AND CONVERT(varchar,P.DateRequested,101) BETWEEN CONVERT(varchar,'" + StartDate.ToString("MM/dd/yyyy") + "',101) AND CONVERT(varchar,'" + EndDate.ToString("MM/dd/yyyy") + "',101)";
                    }
                    else
                    {
                        query += " JP.IsNewJORO = 1 AND CONVERT(varchar,P.DateReceived,101) BETWEEN CONVERT(varchar,'" + StartDate.ToString("MM/dd/yyyy") + "',101) AND CONVERT(varchar,'" + EndDate.ToString("MM/dd/yyyy") + "',101)";
                    }

                }
                if (IsEquipment)
                {
                    query += " AND (JEQ.Id IN (" + EquipmentName + ") OR REQ.Id IN (" + EquipmentName + "))";
                }
                if (IsRequestor)
                {
                    query += " AND CONCAT(RQB.FirstName, ' ', RQB.MiddleName, ' ', RQB.LastName, ' ', RQB.NameExtension) LIKE '%" + Requestor + "%'";
                }
                if (IsJOROStatus && IsDate) //added
                {
                    query += " AND CASE JP.JOROCategory WHEN  0 THEN JO.Status WHEN 1 THEN RO.Status END = (" + JOROStatusId +")";
                }
                else if (IsJOROStatus)
                {
                    query += " AND CASE JP.JOROCategory WHEN  0 THEN JO.Status WHEN 1 THEN RO.Status END = (" + JOROStatusId + ")";

                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                List = connection.Query<clsJOROPRS>(query).ToList();
                connection.Close();
                return List;
            }
        }
    }

}
