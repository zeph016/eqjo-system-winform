using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.ActualAdvance
{
    public class clsActivityReport
    {
        public Int64 EmployeeId { get; set; }
        public String CategoryTypeId { get; set; }
        public String CategoryType { get; set; }
        public String Type { get; set; }
        public Int64 TypeId { get; set; }
        public String EmployeeName { get; set; }
        public String Section { get; set; }
        public String Position { get; set; }
        public String EquipmentCode { get; set; }
        public String EquipmentName { get; set; }
        public clsEnums.TypeOrder ReferenceType { get; set; }
        public Int64 ReferenceNo { get; set; }
        public String ReferenceNoStr { get { return (ReferenceType == clsEnums.TypeOrder.JO ? "JO" : "RO") + ReferenceNo.ToString("0000"); } }
        public String Activity { get; set; }
        public DateTime? DateOfUpdate { get; set; }
        public clsActivityReport()
        {
            ReferenceNo = 0;
            ReferenceType = clsEnums.TypeOrder.JO;
        }
        public clsActivityReport(clsActivityReport obj)
        {
            ReferenceNo = obj.ReferenceNo;
            ReferenceType = obj.ReferenceType;
        }
    }
}
