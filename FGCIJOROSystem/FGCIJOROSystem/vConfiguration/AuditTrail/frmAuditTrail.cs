using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Reports.rAuditTrail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace FGCIJOROSystem.Presentation.vConfiguration.AuditTrail
{
    public partial class frmAuditTrail : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        clsUsersLog currUser;
        UsersLogRepository UsersLogRepo;
        public List<clsUsersLog> ListOfUsersLog { get; set; }
        #endregion
        #region Method
        
        public frmAuditTrail()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
        }

        void LoadAuditTrail()
        {
            UsersLogRepo = new UsersLogRepository();
            if (chkDate.Checked)
            {
                clsUsersLogBindingSource.DataSource = UsersLogRepo.GetAllAuditTrail(dtFromList.Value.Date, dtToList.Value.Date);
            }
            else
            {
                clsUsersLogBindingSource.DataSource = UsersLogRepo.GetAllAudit();
            }

            recordCount();
        }
        #endregion

        private void frmAuditTrail_Load(object sender, EventArgs e)
        {
            dtFromList.Value = DateTime.Now;
            dtToList.Value = DateTime.Now;
            LoadAuditTrail();

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnRefresh, "Refresh");
            ToolTip1.SetToolTip(this.btnPrint, "Generate Report");
        }

        private void MasterTemplate_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.CellElement is GridRowHeaderCellElement && e.Row is GridViewDataRowInfo)
            {
                e.CellElement.Text = (e.CellElement.RowIndex + 1).ToString();
                e.CellElement.TextImageRelation = TextImageRelation.ImageBeforeText;
            }
            else
            {
                e.CellElement.ResetValue(LightVisualElement.TextImageRelationProperty, ValueResetFlags.Local);
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
           LoadAuditTrail();
        }
        private void chkDate_CheckStateChanged(object sender, EventArgs e)
        {
            dtFromList.Enabled = chkDate.Checked;
            dtToList.Enabled = chkDate.Checked;
        }
        void recordCount()
        {
            int cn = radGridView1.RowCount;
            lblCount.Text = "Total Record Found: (" + cn.ToString() + ")";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmAuditTrailSelectOption AT = new frmAuditTrailSelectOption();
            AT.ShowDialog();
        }
    }
}
