using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo;
using FGCIJOROSystem.Domain.EquipmentReleasal;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Reports.rEquipmentReleasal;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Domain.Global;

namespace FGCIJOROSystem.Presentation.vJobOrder
{
    public partial class ucJOEquipmentReleasalList : UserControl
    {
        #region Properties
        JOReleasalTransactionRepository JOReleasalTransactionRepo;
        JOReleasedRepository JOReleasedRepo;
        EquipmentReleasalRepository JOEquipReleasalRepo;
        List<clsJOReleasalTransaction> ListOfJOReleasalTrans;
        List<clsJOReleased> ListOfJOReleased;
        List<clsEquipment> ListOfEquipment;
        #endregion
        #region Methods
        public ucJOEquipmentReleasalList()
        {
            InitializeComponent();
        }
        private void ucJOEquipmentReleasalList_Load(object sender, EventArgs e)
        {
            pbLoading.Dock = DockStyle.Fill;
            loadJOReleasedTransaction();
        }
        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            frmJOEquipReleasal JOEquipReleasalPage = new frmJOEquipReleasal();
            JOEquipReleasalPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm; 
            JOEquipReleasalPage.ShowDialog();
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                loadJOReleasedTransaction();
            }
        }
        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            clsJOReleasalTransaction JOReleasalTrans = (clsJOReleasalTransaction)clsJOReleasalTransactionBindingSource.Current;
            JOReleasalTrans.ListOfJOReleased = ListOfJOReleased;
            frmJOEquipReleasal JOEquipReleasalPage = new frmJOEquipReleasal(JOReleasalTrans);

            JOEquipReleasalPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            JOEquipReleasalPage.ShowDialog();
        }
        void loadJOReleasedTransaction()
        {
            pbLoading.Visible = true;
            JOReleasalTransactionRepo = new JOReleasalTransactionRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                if (Program.CurrentUser.UserLevelId != 5)
                {
                    ListOfJOReleasalTrans = JOReleasalTransactionRepo.SearchBy(" WHERE JR.JOReleasalTransactionId != '' AND JRT.BranchId = " + Program.CurrentUser.BranchId);
                }
                else
                {
                    ListOfJOReleasalTrans = JOReleasalTransactionRepo.SearchBy("WHERE JR.JOReleasalTransactionId != '' ORDER BY JRT.ERNo ASC");
                }

            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                pbLoading.Visible = false;
                clsJOReleasalTransactionBindingSource.DataSource = ListOfJOReleasalTrans;

                int List = radGridView1.RowCount;
                int JO = radGridView2.RowCount;
                txtRowCount.Text = "Total Job Order Transaction: " + List.ToString() + " | Total Job Order Released: " + JO.ToString();
            };
            bg.RunWorkerAsync();
        }
        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (clsJOReleasalTransactionBindingSource.Current != null)
            {
                loadJOReleased();
            }
        }
        void loadJOReleased()
        {
            JOReleasedRepo = new JOReleasedRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ListOfJOReleased = JOReleasedRepo.SearchBy(" WHERE JR.JOReleasalTransactionId = " + ((clsJOReleasalTransaction)clsJOReleasalTransactionBindingSource.Current).Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsJOReleasedBindingSource.DataSource = ListOfJOReleased;

                int List = radGridView1.RowCount;
                int JO = radGridView2.RowCount;
                txtRowCount.Text = "Total Job Order Transaction: " + List.ToString() + " | Total Job Order Released: " + JO.ToString();
            };
            bg.RunWorkerAsync();

           // Application.UseWaitCursor = false;  //chloe
        }

        #endregion

        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Update");
            }
        }

        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            rptEquipmentReleasal report = new rptEquipmentReleasal((clsJOReleasalTransaction)clsJOReleasalTransactionBindingSource.Current);
            frmReportViewer ReportViewerPage = new frmReportViewer(report) { IsJO = false };
            ReportViewerPage.Show();
        }

        private void radMenuItem3_Click(object sender, EventArgs e)
        {
            JOEquipReleasalRepo = new EquipmentReleasalRepository();

            ListOfEquipment = new List<clsEquipment>();
            ListOfEquipment = JOEquipReleasalRepo.PrintSummary();
            frmReportViewer ReportViewerPage = new frmReportViewer();
            var _tariffSummaryReport = new rptReleasalSummaryReport(ListOfEquipment, Program.CurrentUser.FullName, Program.CurrentUser.Position, "EQUIPMENT FOR RELEASAL (JOB ORDER)");
            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.Show();
        }
    }
}
