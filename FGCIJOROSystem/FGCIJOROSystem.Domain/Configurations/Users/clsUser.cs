using FGCIJOROSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Users
{
    public class clsUser
    {
        public int Id { get; set; }
        public String UserName { get; set; }
        public String UserPassword { get; set; }
        public int UserLevelId { get; set; }
        public string UserLevelName { get; set; }
        public Int64 MLEmployeeId { get; set; } 
        public String FullName { get; set; }
        public DateTime DateHired { get; set; }
        public Int64 EmployeeStatusId { get; set; }
        public String EmployeeStatus { get; set; }
        public byte[] EmpPicture { get; set; }

        public Int64 PositionId { get; set; }
        public String Position { get; set; }

        public Int64 SectionId { get; set; }
        public String Section { get; set; }

        public Int64 DepartmentId { get; set; }
        public String Department { get; set; }

        public Int64 BranchId { get; set; }
        public String Branch { get; set; }
        public bool Active { get; set; }
        public bool OnlineUser { get; set; }
        //public List<clsUser> ListOfUsers { get; set; }

        public string EncryptPassword
        {
            get
            {
                return EncryptorManager.EncryptPassword(UserPassword);
            }
        }

        public string DecryptPassword
        {
            get
            {
                return EncryptorManager.DecryptPassword(UserPassword);
            }
        }

        
    }
}
