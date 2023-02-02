using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsSection
   {
        public Int64 Id { get; set; }
        public String SectionName { get; set; }
        public String AttendanceGroupName { get; set; }
        public Int64 AttendanceGroupId { get; set; }
        public Boolean IsChecklistGroup { get; set; }
        public Boolean Active { get; set; }
        public List<clsChecklistDetails> ListOfChecklistDetails { get; set; }
        public clsSection()
        {
            Id = 0;
            SectionName = "";
            AttendanceGroupId = 0;
            AttendanceGroupName = "";
            IsChecklistGroup = false;
            Active = true;
            ListOfChecklistDetails = new List<clsChecklistDetails>();
        }
        public clsSection(clsSection obj)
        {
            Id = obj.Id;
            SectionName = obj.SectionName;
            AttendanceGroupId = obj.AttendanceGroupId;
            AttendanceGroupName = obj.AttendanceGroupName;
            IsChecklistGroup = obj.IsChecklistGroup;
            Active = obj.Active;
            ListOfChecklistDetails = obj.ListOfChecklistDetails;
        }
    }
}
