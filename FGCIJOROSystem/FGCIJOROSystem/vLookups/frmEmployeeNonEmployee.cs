using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Global;
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
    public partial class frmEmployeeNonEmployeeLookup : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        EmployeeRepository EmployeeRepo;

        public clsDataEvent DataEvent;
        #endregion
        #region Methods
        public frmEmployeeNonEmployeeLookup()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
        }
        private void frmEmployeeNonEmployee_Load(object sender, EventArgs e)
        {
            load();
            _changeDateTimeFormat();
        }
        void load()
        {
            BackgroundWorker bg = new BackgroundWorker();
            EmployeeRepo = new EmployeeRepository();
            bg.DoWork += (s, e) =>
            {
                e.Result = EmployeeRepo.GetEmpNonEmp();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsEmployeeBindingSource.DataSource = (List<clsEmployee>)e.Result;
            };
            bg.RunWorkerAsync();
        }
        private void dgvEmployee_CommandCellClick(object sender, EventArgs e)
        {
            if (clsEmployeeBindingSource.Current != null)
            {
                DataEvent.ConfirmData((clsEmployee)clsEmployeeBindingSource.Current);
                this.Dispose();
            }
        }
        #endregion

        private void dgvEmployee_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                dgvEmployee.Grid_CellFormatting(sender, e, "Select");
            }
        }
        void _changeDateTimeFormat()
        {
            foreach (GridViewDataColumn dCol in dgvEmployee.Columns)
            {
                if (dCol.DataType == typeof(DateTime))
                {
                    dCol.FormatString = "{0: MM/dd/yyyy}";
                }
            }
        }
    }
}
