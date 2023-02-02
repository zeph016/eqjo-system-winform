using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Common
{
    public class NumericExt
    {
        public static bool IsNumeric(String str)
        {
            try
            {
                decimal.Parse(str.ToString());
                return true;
            }
            catch
            {
            }
            return false;
        }
    }
}
