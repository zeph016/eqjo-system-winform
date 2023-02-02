using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Dashboard
{
    public class clsJORODashboard
    {
        public String BranchName { get; set; }
        public Int64 ReferenceNo { get; set; }
        public clsEnums.TypeOrder ReferenceType { get; set; }
        public String RefYear { get; set; }
        public String ReferenceNoStr { get { return (ReferenceType == clsEnums.TypeOrder.JO ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + ReferenceNo.ToString("0000"); } }
        public DateTime Date { get; set; }
        public clsEnums.JOROStatus Status { get; set; }

    }
}
