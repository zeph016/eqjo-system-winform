using FGCIJOROSystem.Domain.Configurations.Personnels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.SectionJORO
{
    public class clsSectionJORO
    {
        public Int64 Id { get; set; }
        public String SectionName { get; set; }
        public String AttendanceGroupName { get; set; }
        public Int64 BranchId { get; set; }
        public String BranchName { get; set; }
        public Int64 AttendanceGroupId { get; set; }
        public Boolean IsChecklistGroup { get; set; }
        public Boolean Active { get; set; }
        public List<clsPersonnels> ListOfPersonnel { get; set; }
        public Boolean CheckSectionName { get; set; }
        public Boolean CheckEquipment { get; set; }
        public Boolean CheckEquipmentName { get; set; }
        public Int64 PositionId { get; set; }
        public string PositionName { get; set; }
        public List<clsSectionJORO> ListOfSectionJORO { get; set; }
        public clsSectionJORO()
        {
            Id = 0;
            SectionName = "";
            AttendanceGroupId = 0;
            AttendanceGroupName = "";
            BranchId = 0;
            BranchName = "";
            IsChecklistGroup = false;
            Active = true;
            CheckSectionName = true;
            CheckEquipment = false;
            PositionId = 0;
            ListOfPersonnel = new List<clsPersonnels>();
            ListOfSectionJORO = new List<clsSectionJORO>();
        }
        public clsSectionJORO(clsSectionJORO obj)
        {
            Id = obj.Id;
            SectionName = obj.SectionName;
            CheckEquipment = obj.CheckEquipment;
            AttendanceGroupId = obj.AttendanceGroupId;
            AttendanceGroupName = obj.AttendanceGroupName;
            BranchId = obj.BranchId;
            BranchName = obj.BranchName;
            IsChecklistGroup = obj.IsChecklistGroup;
            Active = obj.Active;
            PositionId = obj.PositionId;
            ListOfPersonnel = obj.ListOfPersonnel;
            ListOfSectionJORO = obj.ListOfSectionJORO;
        }
    }
}
