using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.PRS
{
    public class clsJOROPRS
    {
        #region PRS
        public Int64 PRSId { get; set; }
        public String ControlNo { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateReceived { get; set; }
        public String Location { get; set; }
        public String RequestedBy { get; set; }
        public String CheckedBy { get; set; }
        public String VerifiedBy { get; set; }
        public String ApprovedBy { get; set; }
        public clsEnums.PRSStatus PRSStatus { get; set; }
        public String PreparedBy { get; set; }
        public String Remarks { get; set; }
        public String PRSCategory { get; set; }
        public Int32 PRSCount { get; set; }
        #endregion
        #region PRS Details
        public Int64 PRSDetailId { get; set; }
        public String EquipmentName { get; set; }
        public Int64 JORONo { get; set; }
        public clsEnums.TypeOrder JOROCategory { get; set; }
        public String RefYear { get; set; }
        public String JORONoStr { get { return JORONo != 0 ? (JOROCategory == 0 ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + JORONo.ToString("0000") : ""; } }
        public String Specification { get; set; }
        public String Unit { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal IPOWQty { get; set; }
        public Decimal RemBalQuantity { get; set; }
        public String DetailRemarks { get; set; }
        public String Product { get; set; }
        public clsEnums.JOROStatus JOROStatus { get; set; }
        #endregion
        #region Constructors
        public clsJOROPRS()
        {
            PRSId = 0;
            ControlNo = "";
            DateRequested = DateTime.Now;
            DateReceived = DateTime.Now;
            Location = "";
            RequestedBy = "";
            CheckedBy = "";
            VerifiedBy = "";
            ApprovedBy = "";
            PRSStatus = clsEnums.PRSStatus.None; //clsEnums.PRSStatus.Generated
            PreparedBy = "";
            Remarks = "";
            PRSCategory = "";
            PRSCount = 0;

            PRSDetailId = 0;
            EquipmentName = "";
            JORONo = 0;
            JOROCategory = clsEnums.TypeOrder.JO;
            RefYear = "";
            Specification = "";
            Unit = "";
            Quantity = 0;
            IPOWQty = 0;
            RemBalQuantity = 0;
            DetailRemarks = "";
            Product = "";
        }

        public clsJOROPRS
            (Object prsid,
            Object control_no,
            Object date_requested,
            Object date_received,
            Object location,
            Object requested_by,
            Object checked_by,
            Object verified_by,
            Object approved_by,
            Object prs_status,
            Object prepared_by,
            Object remarks,
            Object prs_category,
            Object prs_count,

            Object prsdetailid,
            Object EquipmentName,
            Object specification,
            Object unit,
            Object quantity,
            Object ipow_qty,
            Object rembalqty,
            Object detail_remarks,
            Object product)
        {
            PRSId = (prsid != DBNull.Value ? Convert.ToInt64(prsid) : 0);
            ControlNo = (control_no != DBNull.Value ? Convert.ToString(control_no) : "");
            if (date_requested != DBNull.Value)
            {
                DateRequested = Convert.ToDateTime(date_requested);
            }
            if (date_received != DBNull.Value)
            {
                DateReceived = Convert.ToDateTime(date_received);
            }
            Location = (location != DBNull.Value ? Convert.ToString(location) : "");
            RequestedBy = (requested_by != DBNull.Value ? Convert.ToString(requested_by) : "");
            CheckedBy = (checked_by != DBNull.Value ? Convert.ToString(checked_by) : "");
            VerifiedBy = (verified_by != DBNull.Value ? Convert.ToString(verified_by) : "");
            ApprovedBy = (approved_by != DBNull.Value ? Convert.ToString(approved_by) : "");
            PRSStatus = (prs_status != DBNull.Value ? (clsEnums.PRSStatus)prs_status : clsEnums.PRSStatus.None); //clsEnums.PRSStatus.Generated
            PreparedBy = (prepared_by != DBNull.Value ? Convert.ToString(prepared_by) : "");
            Remarks = (remarks != DBNull.Value ? Convert.ToString(remarks) : "");
            PRSCategory = (remarks != DBNull.Value ? Convert.ToString(prs_category) : "");
            PRSCount = (prs_count != DBNull.Value ? Convert.ToInt32(prs_count) : 0);

            PRSDetailId = (prsdetailid != DBNull.Value ? Convert.ToInt64(prsdetailid) : 0);
            EquipmentName = (EquipmentName != DBNull.Value ? Convert.ToString(EquipmentName) : "");
            Specification = (specification != DBNull.Value ? Convert.ToString(specification) : "");
            Unit = (unit != DBNull.Value ? Convert.ToString(unit) : "");
            Quantity = (quantity != DBNull.Value ? Convert.ToDecimal(quantity) : 0);
            IPOWQty = (ipow_qty != DBNull.Value ? Convert.ToDecimal(ipow_qty) : 0);
            RemBalQuantity = (rembalqty != DBNull.Value ? Convert.ToDecimal(rembalqty) : 0);
            DetailRemarks = (detail_remarks != DBNull.Value ? Convert.ToString(detail_remarks) : "");
            Product = (product != DBNull.Value ? Convert.ToString(product) : "");
        }

        #endregion
    }
}
