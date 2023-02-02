using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Personnels
{
    public class clsPersonnels 
    {
        public Boolean CheckPersonnel { get; set; }
        public Int64 Id { get; set; }
        public Int64 EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String NameExtension { get; set; }
        public String FullName { get { return LastName + ", " +FirstName + " " + MiddleName + " " + NameExtension; } }
        public Int64 SectionId { get; set; }
        public String SectionName { get; set; }
        public Int64 PositionId { get; set; }
        public String PositionName { get; set; }
        public Boolean IsSectionHead { get; set; }
        public Int64 BranchId { get; set; }
        public String BranchName { get; set; }
        public Boolean Active { get; set; }
        public String Location { get; set; }
        public List<clsPersonnels> ListOfPersonnel{ get; set; }
        public clsPersonnels()
        {
            CheckPersonnel = false;
            Id = 0;
            EmployeeId = 0;
            SectionId = 0;
            PositionId = 0;
            BranchId = 0;
            IsSectionHead = false;
            Active = false;
            Location = "";
            ListOfPersonnel = new List<clsPersonnels>();
        }
    }
}
