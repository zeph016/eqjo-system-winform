using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Users
{
    public class clsUsersLog
    {
        public Int64 Id { get; set; }
        public string Username { get; set; }
        public Int64 MLEmployeeId { get; set; }
        public string EmpName { get; set; }
        public Int64 UserLevelId { get; set; }
        public string UserLevelName { get; set; }
        public Int64 BranchId { get; set; }
        public string BranchName{ get; set; }
        public string ComputerName { get; set; }
        public DateTime DateLogin { get; set; }
        public DateTime TimeLogin { get; set; }
        public DateTime DateLogout { get; set; }
        public DateTime TimeLogout { get; set; }
        public String DayActivity { get; set; }
        public int ActivityType { get; set; }
        public bool OnlineUser { get; set; }
        public List<clsUsersLog>ListOfUsersLog{ get; set; }
        public int AddActivity { get; set; }
        public int UpdateActivity { get; set; }
        public int AddROActivity { get; set; }
        public int UpdateROActivity { get; set; }
        public int ActualAdvanceActivity{ get; set; }
        public clsUsersLog()
        {
            Id = 0;
            Username = "";
            MLEmployeeId = 0;
            EmpName = "";
            UserLevelId = 0;
            BranchId = 0;
            ComputerName = "";
            DateLogin = DateTime.Now;
            TimeLogin = DateTime.Now;
            DateLogout = DateTime.Now;
            TimeLogout = DateTime.Now;
            DayActivity = "";
            ActivityType = 0;
            OnlineUser = true;
            ListOfUsersLog = new List<clsUsersLog>();
            AddActivity = 0;
            UpdateActivity = 0;
            AddROActivity = 0;
            UpdateROActivity = 0;
            ActualAdvanceActivity = 0;
        }
    }
}
