using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.EquipmentReleasal;
using FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Reports.rEquipmentReleasal;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Domain.Global;

namespace FGCIJOROSystem.Presentation.vRescueOrder
{
    public partial class ucROEquipmentReleasalList : UserControl
    {
        #region Properties
        ROReleasalTransactionRepository ROReleasalTransactionRepo;
        ROReleasedRepository ROReleasedRepo;
        EquipmentReleasalRepository ROEquipReleasalRepo;
        List<clsROReleasalTransaction> ListOfROReleasalTrans;
        List<clsROReleased> ListOfROReleased;

        List<clsEquipment> ListOfEquipment;
        #endregion
        #region Methods
        public ucROEquipmentReleasalList()
        {
            InitializeComponent();
        }
        private void ucROEquipmentReleasalList_Load(object sender, EventArgs e)
        {
            pbLoading.Dock = DockStyle.Fill;
            loadJOReleasedTransaction();
        }
        void loadJOReleasedTransaction()
        {
            pbLoading.Visible = true;
            ROReleasalTransactionRepo = new ROReleasalTransactionRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                if (Program.CurrentUser.UserLevelId != 5)
                {
                    ListOfROReleasalTrans = ROReleasalTransactionRepo.SearchBy(" WHERE RR.ROReleasalTransactionId != '' AND RRT.BranchId = " + Program.CurrentUser.BranchId);
                }
                else
                {
                    ListOfROReleasalTrans = ROReleasalTransactionRepo.SearchBy(" WHERE RR.ROReleasalTransactionId != '' ORDER BY RRT.ERNo ASC ");
                }
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                pbLoading.Visible = false;
                clsROReleasalTransactionBindingSource.DataSource = ListOfROReleasalTrans;

                int List = radGridView1.RowCount;
                int JO = radGridView2.RowCount;
                txtRowCount.Text = "Total Rescue Order Transaction: " + List.ToString() + " | Total Rescue Order Released: " + JO.ToString();
            };
            bg.RunWorkerAsync();
        }
        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (clsROReleasalTransactionBindingSource.Current != null)
            {
                loadJOReleased();
            }
        }
        void loadJOReleased()
        {
            ROReleasedRepo = new ROReleasedRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ListOfROReleased = ROReleasedRepo.SearchBy(" WHERE RR.ROReleasalTransactionId = " + ((clsROReleasalTransaction)clsROReleasalTransactionBindingSource.Current).Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                //Application.UseWaitCursor = false;  //chloe
                clsROReleasedBindingSource.DataSource = ListOfROReleased;

                int List = radGridView1.RowCount;
                int JO = radGridView2.RowCount;
                txtRowCount.Text = "Total Rescue Order Transaction: " + List.ToString() + " | Total Rescue Order Released: " + JO.ToString();
            };
            bg.RunWorkerAsync();

            
        }
        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            frmROEquipReleasal JOEquipReleasalPage = new frmROEquipReleasal();
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
            clsROReleasalTransaction ROReleasalTrans = (clsROReleasalTransaction)clsROReleasalTransactionBindingSource.Current;
            ROReleasalTrans.ListOfROReleased = ListOfROReleased;
            frmROEquipReleasal ROEquipReleasalPage = new frmROEquipReleasal(ROReleasalTrans);

            ROEquipReleasalPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            ROEquipReleasalPage.ShowDialog();
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
            rptEquipmentReleasal report = new rptEquipmentReleasal((clsROReleasalTransaction)clsROReleasalTransactionBindingSource.Current);
            frmReportViewer ReportViewerPage = new frmReportViewer(report) { IsRO = false };
            ReportViewerPage.Show();
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mnuPrintSummary_Click(object sender, EventArgs e)
        {
            ROEquipReleasalRepo = new EquipmentReleasalRepository();

            ListOfEquipment = new List<clsEquipment>();
            ListOfEquipment = ROEquipReleasalRepo.PrintSummaryRO();
            frmReportViewer ReportViewerPage = new frmReportViewer();
            var _tariffSummaryReport = new rptReleasalSummaryReport(ListOfEquipment, Program.CurrentUser.FullName, Program.CurrentUser.Position, "EQUIPMENT FOR RELEASAL (RESCUE ORDER)");
            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.Show();
        }
    }
}
