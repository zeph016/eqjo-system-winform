using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.DAL.Repositories.PRSRepo;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Domain.PRS;
using FGCIJOROSystem.Reports.rPRS;
using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using DevExpress.CodeParser;

namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmSelectJOROPRSList : Telerik.WinControls.UI.RadForm
    {
        EquipmentRepository EquipmentRepo;
        List<clsEquipment> ListOfEquipments;

        JOROPRSRepository JOROPRSRepo;
        List<clsJOROPRS> ListOfJOROPRS;

        public frmSelectJOROPRSList()
        {
            InitializeComponent();
            dgvEquipment.Enabled = false;
        }
        private void frmSelectJOROPRSList_Load(object sender, EventArgs e)
        {
            LoadAllJOROPRSList();
            pbLoading.Visible = false;
            pbLoading.Dock = DockStyle.None;

            //loadequipmentAsync();
            //loadEquipment();
            dtStartDate.Value = DateTime.Now;
            dtEndDate.Value = DateTime.Now;
            //this.ControlBox = false;
        }
        
        void LoadAllJOROPRSList()
        {
            cbPRSchkList.DataSource = EnumHelper<clsEnums.PRSStatus>.GetTypeEnum2();
            cbPRSchkList.DisplayMember = "Name";
            cbPRSchkList.ValueMember = "Id";



            //cbJOROchkList.DataSource = Enum.GetValues(typeof(clsEnums.JOROStatus));
            
            cbJOROchkList.DataSource = EnumHelper<clsEnums.JOROStatus>.GetTypeEnum2();
            cbJOROchkList.DisplayMember = "Name";
            cbJOROchkList.ValueMember = "Id";
            
            // List<string> AreaNameId = cbAreaName.CheckedItems.Select(x => "'" + x.Text + "'").ToList();

        }
        void loadEquipment()
        {
            pbLoading.Visible = true;
            EquipmentRepo = new EquipmentRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                try
                {
                    //Application.UseWaitCursor = true;  //chloe
                    ListOfEquipments = EquipmentRepo.GetAll();
                    clsEquipmentBindingSource.DataSource = ListOfEquipments;
                }
                catch (Exception)
                {
                }
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                pbLoading.Visible = false;
                pbLoading.Enabled = false;
                //pbLoading.Location = new System.Drawing.Point(1, 1);
                dgvEquipment.Visible = true;
                //Application.UseWaitCursor = false;
                dgvEquipment.Enabled = true;

            };
            bg.RunWorkerAsync();
        }

        async Task loadequipmentAsync()
        {
            pbLoading.Visible = true;
            EquipmentRepo = new EquipmentRepository();

            //Application.UseWaitCursor = true;  //chloe
            ListOfEquipments = await Task.Run(() => EquipmentRepo.GetAll());

            clsEquipmentBindingSource.DataSource = ListOfEquipments;

            pbLoading.Visible = false;
            pbLoading.Enabled = false;
            //pbLoading.Location = new System.Drawing.Point(1, 1);
            dgvEquipment.Visible = true;
            //Application.UseWaitCursor = false;
            dgvEquipment.Enabled = true;
            btnCancel.Enabled = true;
            pbLoading.Visible = true;
        }

        private void Checkbox_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            radGroupBox2.Enabled = chkDate.Checked;
            tbRequestedBy.Enabled = chkRequestedBy.Checked;
            //dgvEquipment.Enabled = chkEquipment.Checked;
            //dgvEquipment.ReadOnly = false;
            //dgvEquipment.AllowSearchRow = true;
            //dgvEquipment.AllowMultiColumnSorting = true;

            //DataGridViewCheckBoxColumn ck = new DataGridViewCheckBoxColumn();
            //dgvEquipment.Columns.Insert(0, ck);
        }

        private void Date_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            JOROPRSRepo = new JOROPRSRepository();
            List<long> EquipmentIdList = new List<long>();
            for (int i = 0; i <= dgvEquipment.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(this.dgvEquipment.Rows[i].Cells["chkSelect"].Value) == true)
                {
                    EquipmentIdList.Add(Convert.ToInt64(this.dgvEquipment.Rows[i].Cells["EquipmentMasterlistId"].Value));
                }                
            }

            if (chkJOROStatus.Checked == true)
            {
                string EquipmentNames = string.Join(",", EquipmentIdList);

                List<string> JOROStatusId = cbJOROchkList.CheckedItems.Select(x => "'" + x.Value + "'").ToList();
                ListOfJOROPRS = JOROPRSRepo.GetJOROStatusList(EquipmentNames, tbRequestedBy.Text,
                                                        dtStartDate.Value.Date, dtEndDate.Value.Date,
                                                        chkDate.Checked, rbDatePrepared.IsChecked,
                                                        chkEquipment.Checked, chkRequestedBy.Checked,
                                                        chkJOROStatus.Checked, String.Join(", ", JOROStatusId));


            }
            if (chkPRSStatus.Checked == true)
            {
                string EquipmentNames = string.Join(",", EquipmentIdList);

                List<string> PRSStatusId = cbPRSchkList.CheckedItems.Select(x => "'" + x.Value + "'").ToList();
                ListOfJOROPRS = JOROPRSRepo.GetPRSStatusList(EquipmentNames, tbRequestedBy.Text,
                                                        dtStartDate.Value.Date, dtEndDate.Value.Date,
                                                        chkDate.Checked, rbDatePrepared.IsChecked,
                                                        chkEquipment.Checked, chkRequestedBy.Checked,
                                                        chkPRSStatus.Checked, String.Join(", ", PRSStatusId));
            }
            
            String Title = "";
            
            if (chkDate.Checked)
            {
                if (rbDatePrepared.IsChecked)
                {
                    //Title = "DATE PREPARED
                    Title = "DATE RANGE ( From " + dtStartDate.Value.ToString("MMMM dd, yyyy") + " to " + dtEndDate.Value.ToString("MMMM dd, yyyy") + " )";
                }
                else if (rbDateReceived.IsChecked)
                {
                    //Title = "DATE PREPARED
                    Title = "DATE RANGE ( From " + dtStartDate.Value.ToString("MMMM dd, yyyy") + " to " + dtEndDate.Value.ToString("MMMM dd, yyyy") + " )";
                }
            }
            else
            {
                Title = "AS OF " + DateTime.Now.ToShortDateString();
            }

            if(chkPRSStatus.Checked == true)
            {
                rptJOROPRSList report = new rptJOROPRSList(ListOfJOROPRS, Title, Program.CurrentUser);
                var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
                frmReportViewer ReportViewerPage = new frmReportViewer();
                ReportViewerPage.reportViewer1.ReportSource = reportSource;
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.ShowDialog();
            }
            if (chkJOROStatus.Checked == true)
            {
                rptJOROList report = new rptJOROList(ListOfJOROPRS, Title, Program.CurrentUser);
                var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
                frmReportViewer ReportViewerPage = new frmReportViewer();
                ReportViewerPage.reportViewer1.ReportSource = reportSource;
                ReportViewerPage.reportViewer1.RefreshReport();
                ReportViewerPage.ShowDialog();
            }

            if (chkPRSStatus.Checked == false && chkJOROStatus.Checked == false)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Please select filter to work with."
                };
                MsgBox.ShowDialog();
            }
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void frmSelectJOROPRSList_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.UseWaitCursor = false;
        }

        private void ChkJOROStatus_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkJOROStatus.Checked == true)
            {
                cbJOROchkList.Enabled = true;

                chkPRSStatus.Checked = false;
                chkPRSStatus.Enabled = false;
            }
            else
            {
                cbJOROchkList.Enabled = false;

                chkPRSStatus.Enabled = true;

            }
        }

        private void ChkPRSStatus_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkPRSStatus.Checked == true)
            {
                cbPRSchkList.Enabled = true;

                chkJOROStatus.Checked = false;
                chkJOROStatus.Enabled = false;
            }
            else
            {
                cbPRSchkList.Enabled = false;

                chkJOROStatus.Enabled = true;
            }
        }

        private async void chkEquipment_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            //loadEquipment();
            if (chkEquipment.CheckState == CheckState.Checked)
            {

                btnGenerate.Enabled = false;
                btnCancel.Enabled = false;
                pbLoading.Visible = false;
                //loadEquipment();
                await loadequipmentAsync();
                dgvEquipment.Enabled = true;
                dgvEquipment.Visible = true;
                pbLoading.Visible = false;
                
            }
            
        }

        private async void dgvEquipment_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            var current = await Task.Run(() => (clsEquipment)clsEquipmentBindingSource.Current); //async
            current.chkSelect = !current.chkSelect;
            clsEquipmentBindingSource.ResetCurrentItem();
        }
    }
}
