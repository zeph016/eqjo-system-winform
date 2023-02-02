using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Attendance
{
    public class clsAttendanceStatus
    {
        public Int64 Id { get; set; }
        public String AttendanceStatusName { get; set; }
        public String AttendanceStatusDescription { get; set; }
        public String Symbol { get; set; }
        public int intColor { get; set; }
        public Color clrColor { get { return Color.FromArgb(intColor); } }
        public Boolean IsActive { get; set; }
    }
}
