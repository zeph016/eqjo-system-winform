using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Attendance
{
    public class clsAttendanceGroup
    {
        public Int64 Id { get; set; }
        public String AttendanceGroupName { get; set; }
        public String AttendanceGroupDescription { get; set; }
        public int intColor { get; set; }
        public Color clrColor { get { return Color.FromArgb(intColor); } }
        public Boolean IsActive { get; set; }
    }
}