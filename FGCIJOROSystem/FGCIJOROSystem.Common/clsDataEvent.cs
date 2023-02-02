using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGCIJOROSystem.Common
{
    public class clsDataEvent
    {

        public delegate void OnDataConfirmed(Object obj);
        public event OnDataConfirmed OnDataConfirm;

        public void ConfirmData()
        {
            try
            {
                OnDataConfirm.Invoke(null);
            }
            catch
            {
                MessageBox.Show("No Handler have been created for this event." + Environment.NewLine ,
                                "HANDLER CANNOT BE FOUND", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ConfirmData(Object obj)
        {
            try
            {
                OnDataConfirm.Invoke(obj);
            }
            catch(Exception ex)
            {
                MessageBox.Show("No Handler have been created for this event." + Environment.NewLine ,
                                ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

  

    }
}
