using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.ActualAdvance
{
    public class clsActualAdvance
    {
        public Int64 EmployeeId { get; set; }
        public String EmployeeName { get; set; }
        public String Position { get; set; }
        public String Section { get; set; }
        #region Previous Actual
        public String PAcReferenceNo { get; set; }
        public String PAcEquipmentName { get; set; }
        public String PAcActivity { get; set; }
        public String PAcRemarks { get; set; }
        public String PAcTime { get; set; }
        #endregion
        #region Previous Advance
        public String PAdReferenceNo { get; set; }
        public String PAdEquipmentName { get; set; }
        public String PAdActivity { get; set; }
        public String PAdRemarks { get; set; }
        public String PAdTime { get; set; }
        #endregion
        #region Current Actual
        public String CAcReferenceNo { get; set; }
        public String CAcEquipmentName { get; set; }
        public String CAcActivity { get; set; }
        public String CAcRemarks { get; set; }
        public String CAcTime { get; set; }
        #endregion
        #region Current Advance
        public String CAdReferenceNo { get; set; }
        public String CAdEquipmentName { get; set; }
        public String CAdActivity { get; set; }
        public String CAdRemarks { get; set; }
        public String CAdTime{ get; set; }
        #endregion
        public clsActualAdvance()
        {
            EmployeeId = 0;
            #region Previous Actual
            PAcReferenceNo = "";
            PAcActivity= "";
            PAcRemarks= "";
            PAcTime = "";
            #endregion
            #region Previous Advance
            PAdReferenceNo = "";
            PAdActivity= "";
            PAdRemarks= "";
            PAdTime = "";
            #endregion
            #region Current Actual
            CAcReferenceNo = "";
            CAcActivity = "";
            CAcRemarks = "";
            CAcTime = "";
            #endregion
            #region Current Advance
            CAdReferenceNo = "";
            CAdActivity = "";
            CAdRemarks = "";
            CAdTime = "";
            #endregion
        }
        public clsActualAdvance(clsActualAdvance obj)
        {
            EmployeeId = obj.EmployeeId;
            #region Previous Actual
            PAcReferenceNo = obj.PAcReferenceNo;
            PAcActivity = obj.PAcActivity;
            PAcRemarks = obj.PAcRemarks;
            PAcTime = obj.PAcTime;
            #endregion
            #region Previous Advance
            PAdReferenceNo = obj.PAdReferenceNo;
            PAdActivity = obj.PAdActivity;
            PAdRemarks = obj.PAdRemarks;
            PAdTime = obj.PAdTime;
            #endregion
            #region Current Actual
            CAcReferenceNo = obj.CAcReferenceNo;
            CAcActivity = obj.CAcActivity;
            CAcRemarks = obj.CAcRemarks;
            CAcTime = obj.CAcTime;
            #endregion
            #region Current Advance
            CAdReferenceNo = obj.CAdReferenceNo;
            CAdActivity = obj.CAdActivity;
            CAdRemarks = obj.CAdRemarks;
            CAdTime = obj.CAdTime;
            #endregion
        }
    }
}
