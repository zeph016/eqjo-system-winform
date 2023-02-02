using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.EquipmentHistory
{
    public class clsEquipmentHistory
    {
        public String BranchName { get; set; }
        public clsEnums.TypeOrder TransType { get; set; }
        public Int64 RefNo { get; set; }
        public String RefNoStr { get { return (TransType == clsEnums.TypeOrder.JO ? "JO" : "RO") + RefNo.ToString("000");} }
        public DateTime TransDate { get; set; }
        public String Customer { get; set; }
        public String Contractor { get; set; }
        public String ContractorSectionHead { get; set; }
        public String DriverName { get; set; }

        
        public String WorkDescription { get; set; }
        public String Section { get; set; }
        public String JobCategory { get; set; }
        public String JobType { get; set; }
        public String User { get; set; }
        public String Status { get; set; }
    }
}
