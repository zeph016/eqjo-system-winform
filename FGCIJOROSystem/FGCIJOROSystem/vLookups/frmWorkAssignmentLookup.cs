using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.WorkAssignment;
using FGCIJOROSystem.Domain.ActualAdvance;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.WorkAssignment;
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
    public partial class frmWorkAssignmentLookup : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        JOWorkAssignmentRepository JOWorkAssignmentRepo;
        List<clsWorkAssignments> ListOfJOWorkAssign;

        List<clsWorkAssignments> SelectedJOWorkAssign;
        clsActualAdvanceDetails ActualAdvanceDetails;

        public clsDataEvent DataEvent;
        #endregion
        #region Methods
        public frmWorkAssignmentLookup()
        {
            InitializeComponent();
        }
        public frmWorkAssignmentLookup(clsActualAdvanceDetails obj)
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            ListOfJOWorkAssign = new List<clsWorkAssignments>();
            SelectedJOWorkAssign = new List<clsWorkAssignments>();
            ActualAdvanceDetails = obj;
        }

        private void frmWorkAssignmentLookup_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            BackgroundWorker bg = new BackgroundWorker();
            JOWorkAssignmentRepo = new JOWorkAssignmentRepository();
            bg.DoWork += (s, e) =>
            {
                ListOfJOWorkAssign = JOWorkAssignmentRepo.SearchJOROBy("WHERE T.EmployeeId = '" + ActualAdvanceDetails.EmployeeId.ToString() + "' AND T.IsActive = 1");
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsJOWorkAssignmentsBindingSource.DataSource = ListOfJOWorkAssign;
            };
            bg.RunWorkerAsync();
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            DataEvent.ConfirmData(SelectedJOWorkAssign);
            this.Dispose();
        }
        private void dgvEmployee_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            Boolean chkSelect = (Boolean)dgvEmployee.CurrentRow.Cells["chkSelect"].Value;
            clsWorkAssignments JOWorkAssign = (clsWorkAssignments)clsJOWorkAssignmentsBindingSource.Current;
            JOWorkAssign.IsActive = chkSelect;
            JOWorkAssign.EmployeeId = ActualAdvanceDetails.EmployeeId;
            JOWorkAssign.MechanicName = ActualAdvanceDetails.EmployeeName;
            JOWorkAssign.Position = ActualAdvanceDetails.Position;
            JOWorkAssign.Section = ActualAdvanceDetails.Section;
            if (chkSelect)
            {
                SelectedJOWorkAssign.Add(JOWorkAssign);
            }
            else
            {
                SelectedJOWorkAssign.RemoveAll(x => x.EmployeeId == JOWorkAssign.EmployeeId);
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
    }
}
