using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.BLL
{
    public class CalculateDays
    {
        public static Decimal CalculateNoOfDays(DateTime StartDate, DateTime EndDate)
        {
            return (Decimal)(EndDate - StartDate).TotalDays;
        }
    }
}
