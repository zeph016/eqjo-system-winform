using FGCIJOROSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace FGCIJOROSystem.Presentation.vLookups
{
    public partial class frmTariffLookup : Telerik.WinControls.UI.RadForm
    {
        public clsDataEvent DataEvent;

        public frmTariffLookup()
        {
            InitializeComponent();
        }

        private void dgvEmployee_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                dgvEmployee.Grid_CellFormatting(sender, e, "Select");
            }
        }

    }
}
