using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Reports.rJobOrder;
using FGCIJOROSystem.Presentation.vJobOrder.vWorkAssignment;
using FGCIJOROSystem.Domain.Configurations.Status;
using FGCIJOROSystem.Domain.ActualAdvance;
using System.Transactions;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.ActualAdvanceRepo;
using Telerik.WinControls;
using FGCIJOROSystem.Presentation.vJORODirectPrint;
using FGCIJOROSystem.Reports.rPRS;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using DevExpress.XtraGrid.Views.Grid;
using FGCIJOROSystem.Presentation.vLoader;
using Telerik.WinControls.Data;

namespace FGCIJOROSystem.Presentation.vJobOrder
{
    public partial class ucJOList : UserControl
    {        
        #region Properties
        public frmMainWindow MainWindowPage;

        JORepository JORepo;
        JODetailRepository JODetailRepo;
        JOMechanicRepository MechaRepo;
        JOAuditTrailRepository JOAuditTrailRepo;
        ActivityReportRepository ActivityReportRepo;

        clsEnums.CRUDEMode CRUDEMode;

        List<clsJODetails> ListOfJODetails;
        List<clsMechanics> ListOfMechanics;
        List<clsJOAuditTrail> ListOfJOAuditTrail;
        List<clsJobOrder> ListOfJobOrder;
        List<clsActivityReport> ListOfActivity;
        List<clsActivityReport> uniqueListActivity;
        List<clsStatus> ListOfStatus;

        clsJobOrder JobOrder;
        clsJODetails JODetails;
        clsJOAuditTrail JOAuditTrail;
     
        StatusRepository StatusRepo;

        public bool taskComplete;
        public Progress<int> Progress = new Progress<int>();
        private bool isSameMechanic;
        private bool isSameActivity;
        private bool isSameDate;
        private long currSelectedJOId;

        System.Diagnostics.Stopwatch timeLapsed = new System.Diagnostics.Stopwatch(); //timelapsed checker
        #endregion

        public ucJOList()
        {
            InitializeComponent();
            ListOfJODetails = new List<clsJODetails>();
            ListOfMechanics = new List<clsMechanics>();
            ListOfJOAuditTrail = new List<clsJOAuditTrail>();
            ListOfActivity = new List<clsActivityReport>();
            ListOfStatus = new List<clsStatus>();

            this.dgvJODetails.Columns["btnStatus"].IsVisible = true;
            this.dgvJODetails.Columns["WorkPercentage"].IsVisible = false;
            this.dgvJODetails.Columns["NoOfMechanics"].IsVisible = false;
            this.dgvJODetails.Columns["WorkTimeSpan"].IsVisible = false;
        }
        private void mCreateNew_Click(object sender, EventArgs e)
        {
            ucJOEntry JOEntryPage = new ucJOEntry();
            CRUDEMode = clsEnums.CRUDEMode.Add;
            JOEntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvJOEntry",
                Text = "JO Order Entry",
                //Image = Properties.Resources.home
            }, JOEntryPage);
        }

        private void mUpdateJO_Click(object sender, EventArgs e)
        {
            if (((clsJobOrder)clsJobOrderBindingSource.Current).Status != clsEnums.JOROStatus.Completed && ((clsJobOrder)clsJobOrderBindingSource.Current).Status != clsEnums.JOROStatus.Released && ((clsJobOrder)clsJobOrderBindingSource.Current).Status != clsEnums.JOROStatus.ReleaseSustained)
            {
                ucJOEntry JOEntryPage = new ucJOEntry(MapProperties(), ListOfActivity);
                CRUDEMode = clsEnums.CRUDEMode.Edit;
                JOEntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvJOUpdateEntry",
                    Text = @"Update JO Order",
                    //Image = Properties.Resources.home
                }, JOEntryPage);
            }
        }
        private void mUpdateStatus_Click(object sender, EventArgs e)
        {
            frmJOEquipReleasal JOEquipRelPage = new frmJOEquipReleasal();
            JOEquipRelPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            JOEquipRelPage.ShowDialog();
        }

        private async void DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                await newLoad();
                //load();
            }
        }


        #region Load Form
        private async void ucJOList_Load(object sender, EventArgs e)
        {
            LoadingManager.ShowLoadScreen();
            dtDateFrom.Format = DateTimePickerFormat.Custom;
            dtDateFrom.CustomFormat = "MM/dd/yyyy";
            dtDateTo.Format = DateTimePickerFormat.Custom;
            dtDateTo.CustomFormat = "MM/dd/yyyy";

            dtDateFrom.Value = DateTime.Now;
            dtDateTo.Value = DateTime.Now;

            clsJODetailsBindingSource.DataSource = ListOfJODetails;
            clsMechanicsBindingSource.DataSource = ListOfMechanics;

            Task t = Task.WhenAll(LoadJOStatus(), LoadBranch(), LoadListOfStatus());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                loadStatus();
                LoadingManager.CloseLoadScreen();
            }

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnRefresh, "Refresh");
        }
        #endregion

        #region Load 
        void load() //DEPRECIATED
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                JORepo = new JORepository();
                e.Result = JORepo.SearchBy("WHERE JO.Status != 4 AND JO.Status != 5 AND JO.Status != 7 ORDER BY JO.JODate DESC");//JO.Status DESC, 
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsJobOrderBindingSource.DataSource = (List<clsJobOrder>)e.Result;

                int cn = rgvJO.RowCount;
                txtRowCount.Text = "Total Number of Job Orders by Filter :  " + cn.ToString();
            };
            bg.RunWorkerAsync();
        }


        #endregion

        #region Async New Load
        private async Task newLoad()
        {
            String SearchStatement = "";
            if (cbEquipmentCode.IsChecked)
            {
                SearchStatement += " EQ.PPEName LIKE '" + tbxEquipmentCode.Text + "%' AND";
            }
            if (cbEquipmentLocation.IsChecked)
            {
                SearchStatement += " JO.Location LIKE '" + tbxEquipmentLocation.Text + "%' AND";
            }
            if (cbDate.IsChecked)
            {
                SearchStatement += " JO.JODate BETWEEN '" + dtDateFrom.Value.Date + "' AND '" + dtDateTo.Value.Date + "' AND";
            }
            if (cbStatus.IsChecked)
            {
                SearchStatement += " JO.Status = " + rddStatus.SelectedValue + " AND";
            }
            if (cbOrderNo.IsChecked)
            {
                if (String.IsNullOrEmpty(tbxOrderNo.Text))
                {
                    RadMessageBox.Show(@"Order Number is Empty", @"Error: Empty", MessageBoxButtons.RetryCancel, RadMessageIcon.Exclamation);
                    return;
                }
                else
                {
                    SearchStatement += " JO.JONo LIKE '%" + tbxOrderNo.Text + "%' AND";
                }
            }
            char[] removeEnd = { 'D', 'N', 'A' };
            string finalSearchStatmement = SearchStatement.TrimEnd(removeEnd);

            #region Old Code
            //List<clsJobOrder> Result;
            //Result = await Task.Run(() => JORepo.SearchBy("WHERE JO.Status != 4 AND JO.Status != 5 AND JO.Status != 7" + SearchStatement + " ORDER BY JO.JODate DESC"));//JO.Status DESC, 
            /* Result = await Task.Run(() => JORepo.SearchBy("WHERE " + finalSearchStatmement + " ORDER BY JO.JODate DESC"));*///JO.Status DESC,
            #endregion

            JORepo = new JORepository();
            ListOfJobOrder = await Task.Run(() => JORepo.SearchBy("WHERE " + finalSearchStatmement + " ORDER BY JO.JODate DESC"));
            clsJobOrderBindingSource.DataSource = await Task.Run(() => ListOfJobOrder);
        }
        #endregion

        #region Load Jo Details
        void LoadJODetails() //DEPRECIATED
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                JODetailRepo = new JODetailRepository();
                ListOfJODetails = JODetailRepo.SearchBy(" WHERE JD.JOId = " + ((clsJobOrder)clsJobOrderBindingSource.Current).Id + " AND JD.IsActive = 1");

            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsJODetailsBindingSource.DataSource = ListOfJODetails.ToList();
                LoadPrint();
                DisplayStatus();
            };
            bg.RunWorkerAsync();
            clsJODetailsBindingSource.ResetBindings(true);
        }
        #endregion

        #region Async Load JO Details
        private async Task NewLoadJODetails()
        {
            JODetailRepo = new JODetailRepository();
            ListOfJODetails = await Task.Run(() => JODetailRepo.SearchBy(" WHERE JD.JOId = " + ((clsJobOrder)clsJobOrderBindingSource.Current).Id + " AND JD.IsActive = 1"));
            clsJODetailsBindingSource.DataSource = ListOfJODetails.ToList();
            Task t = Task.WhenAll(newDisplayStatus());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                clsJODetailsBindingSource.ResetBindings(true);
                LoadPrint();
            }
            //DisplayStatus();
        }
        #endregion

        #region LoadMechanics
        void LoadMechanics() //DEPRECIATED
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                MechaRepo = new JOMechanicRepository();
                ListOfMechanics = MechaRepo.SearchBy(" WHERE JM.JOId = " + ((clsJobOrder)clsJobOrderBindingSource.Current).Id + " AND JM.IsActive = 1");
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsMechanicsBindingSource.DataSource = ListOfMechanics;
            };
            bg.RunWorkerAsync();
            clsMechanicsBindingSource.ResetBindings(true);
        }
        #endregion

        #region Async Load Mechanics
        private async Task NewLoadMechanics()
        {
            MechaRepo = new JOMechanicRepository();
            ListOfMechanics = MechaRepo.SearchBy(" WHERE JM.JOId = " + ((clsJobOrder)clsJobOrderBindingSource.Current).Id + " AND JM.IsActive = 1");
            clsMechanicsBindingSource.DataSource = await Task.Run(() => ListOfMechanics);
            clsMechanicsBindingSource.ResetBindings(true);
        }
        #endregion

        #region loadAuditTrail
        void loadAuditTrail() //DEPRECIATED
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                JOAuditTrailRepo = new JOAuditTrailRepository();
                ListOfJOAuditTrail = JOAuditTrailRepo.SearchBy(" WHERE JO.Id = " + ((clsJobOrder)clsJobOrderBindingSource.Current).Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                //Application.UseWaitCursor = false;  //chloe

            };
            bg.RunWorkerAsync();
            clsJOAuditTrailBindingSource.ResetBindings(true);
        }
        #endregion

        #region Async Load Audit Trail
        private async Task NewLoadAuditTrail()
        {
            JOAuditTrailRepo = new JOAuditTrailRepository();
            ListOfJOAuditTrail = JOAuditTrailRepo.SearchBy(" WHERE JO.Id = " + ((clsJobOrder)clsJobOrderBindingSource.Current).Id);
            clsJOAuditTrailBindingSource.DataSource = await Task.Run(() => ListOfJOAuditTrail);
            clsJOAuditTrailBindingSource.ResetBindings(true);
        }
        #endregion

        #region MasterTemplate Selection Changed
        private async void MasterTemplate_SelectionChanged(object sender, EventArgs e)
        {
            //LoadingManager.ShowLoadScreen();
            //currSelectedJOId = ((clsJobOrder)clsJobOrderBindingSource.Current).Id;
            //if (clsJobOrderBindingSource.Current != null)
            //{
            //    Task t = Task.WhenAll(NewLoadJODetails(), NewLoadMechanics(), NewLoadAuditTrail(), NewLoadPRS());
            //    await t;
            //    if (t.Status == TaskStatus.RanToCompletion)
            //    {
            //        if (((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released)
            //        {
            //            dgvJODetails.Columns["cbStatus"].ReadOnly = true;
            //            dgvJODetails.Columns["btnStatus"].ReadOnly = true;
            //            dgvJODetails.Columns["Remarks"].ReadOnly = true;
            //        }
            //        else
            //        {
            //            dgvJODetails.Columns["cbStatus"].ReadOnly = false;
            //            dgvJODetails.Columns["btnStatus"].ReadOnly = false;
            //            dgvJODetails.Columns["Remarks"].ReadOnly = false;
            //        }
            //        LoadingManager.CloseLoadScreen();
            //    }
            //    else
            //    {
            //        LoadingManager.CloseLoadScreen();
            //        RadMessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            //    }
            //    DisplayStatus();
            //}
            //dgvJODetails.Enabled = true;
        }
        #endregion

        private void MasterTemplate_CommandCellClick(object sender, GridViewCellEventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnTransactionLogs")
            {
                frmJOTransactionLog TransLogPage = new frmJOTransactionLog((clsJobOrder)clsJobOrderBindingSource.Current);
                TransLogPage.ShowDialog();
            }
        }
        private async void dgvJODetails_CommandCellClick(object sender, GridViewCellEventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnStatus")
            {
                //SaveStatus();
                await SaveStatusAsync();
            }
            else if (cell.ColumnInfo.Name == "btnAssignMech")
            {
                frmJODetailWorkAssignment JODetailWorkAssignPage = new frmJODetailWorkAssignment((clsJODetails)clsJODetailsBindingSource.Current);
                //StatusUpdatePage.DataEvent.OnDataConfirm += Status_DataEvent_OnDataConfirm;
                JODetailWorkAssignPage.ShowDialog();
            }
        }

        #region Map Properties
        clsJobOrder MapProperties()
        {
            clsJobOrder curJO = new clsJobOrder();
            JODetailRepo = new JODetailRepository();
            MechaRepo = new JOMechanicRepository();

            curJO = (clsJobOrder)clsJobOrderBindingSource.Current;
            curJO.UserId = Program.CurrentUser.Id;
            curJO.EncoderName = Program.CurrentUser.FullName;
            curJO.EncoderPosition = Program.CurrentUser.Position;
            curJO.ListOfJODetails = JODetailRepo.SearchBy(" WHERE JD.JOId = " + ((clsJobOrder)clsJobOrderBindingSource.Current).Id + "");
            curJO.ListOfMechanics = MechaRepo.SearchBy(" WHERE JM.JOId = " + ((clsJobOrder)clsJobOrderBindingSource.Current).Id + "");
            MapActivity();
            return curJO;
        }
        #endregion

        #region Map Activity
        private void MapActivity()
        {
            ActivityReportRepo = new ActivityReportRepository();
            ListOfActivity = ActivityReportRepo.GetListActivityByRefNo(((clsJobOrder)clsJobOrderBindingSource.Current).JONo, 0);
            uniqueListActivity = new List<clsActivityReport>();
            foreach (clsActivityReport items in ListOfActivity)
            {
                isSameMechanic = false; isSameActivity = false; isSameDate = false;
                isSameMechanic = uniqueListActivity.Any(x => x.EmployeeId == items.EmployeeId);
                isSameActivity = uniqueListActivity.Any(y => y.Activity == items.Activity);
                if (isSameMechanic && isSameActivity)
                    isSameDate = uniqueListActivity.Any(z => z.DateOfUpdate == items.DateOfUpdate);
                if (isSameDate == false)
                    uniqueListActivity.Add(items);
            }
        }
        #endregion

        #region MapTransactionLogs
        clsJOTransactionLogs MapTransactionLog()
        {
            clsJOTransactionLogs TransactionLog = new clsJOTransactionLogs();
            clsJobOrder JobOrder = (clsJobOrder)clsJobOrderBindingSource.Current; 
            TransactionLog.JOId = JobOrder.Id;
            TransactionLog.JONo = JobOrder.JONo;
            TransactionLog.Type = JobOrder.Type;
            TransactionLog.JODate = JobOrder.JODate;
            TransactionLog.EquipmentOnBranch = JobOrder.EquipmentOnBranch;
            TransactionLog.PartsRequest = JobOrder.PartsRequest;
            TransactionLog.CustomerType = JobOrder.CustomerType;
            TransactionLog.CustomerId = JobOrder.CustomerId;
            TransactionLog.BranchId = JobOrder.BranchId;
            TransactionLog.ItemType = JobOrder.ItemType;
            TransactionLog.EquipmentId = JobOrder.EquipmentId;
            TransactionLog.ContractorId = JobOrder.ContractorId;
            TransactionLog.DriverId = JobOrder.DriverId;
            TransactionLog.UserId = Program.CurrentUser.Id;
            TransactionLog.UserName = Program.CurrentUser.FullName;
            TransactionLog.UserPosition = Program.CurrentUser.Position;
            TransactionLog.ItemType = JobOrder.ItemType;
            ((List<clsJODetails>)clsJODetailsBindingSource.List).ForEach(y =>
            {
                TransactionLog.ListOfJODetailTransactionLogs.Add(
                    new clsJODetailTransactionLogs
                    {
                        JOId = y.JOId,
                        JODetailId = y.Id,
                        TariffId = y.TariffId,
                        WorkDescription = y.WorkDescription,
                        Section = y.Section,
                        JobCategory = y.JobCategory,
                        JobType = y.JobType,
                        Unit = y.Unit,
                        NoOfMechanics = y.NoOfMechanics,
                        EffectivityDate = y.EffectivityDate,
                        TargetDate = y.TargetDate,
                        WorkTimeSpan = y.WorkTimeSpan,
                        Price = y.Price,
                        Amount = y.Amount,
                        Remarks = y.Remarks,
                        StatusId = y.StatusId,
                        Tag = y.Tag,
                        IsActive = y.IsActive
                    });
            });
            ((List<clsMechanics>)clsMechanicsBindingSource.List).ForEach(y =>
            {
                TransactionLog.ListOfMechanicsTransactionLogs.Add(
                new clsJOMechTransactionLogs
                {
                    JOId = y.JOId,
                    JOMechanicId = y.Id,
                    EmployeeId = y.EmployeeId,
                    Tag = y.Tag,
                    IsActive = y.IsActive
                });
            });
            return TransactionLog;
        }
        #endregion

        #region Load Print
        void LoadPrint()
        {
            rptJobOrder report = new rptJobOrder(MapProperties(), uniqueListActivity);
            var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            this.reportViewer1.ReportSource = reportSource;
            this.reportViewer1.RefreshReport();
        }
        private void reportViewer1_Print(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            frmDirectPrinting DirectPrintingPage = new frmDirectPrinting(MapTransactionLog()) { rptSource = this.reportViewer1.ReportSource };
            DirectPrintingPage.ShowDialog();
        }
        #endregion

        #region Report Viewer
        #endregion

        #region Mechanics
        private void MasterTemplate_CommandCellClick_1(object sender, EventArgs e)
        {
            frmMechanicWorkAssignment frmMechWorkAssign = new frmMechanicWorkAssignment((clsMechanics)clsMechanicsBindingSource.Current);
            frmMechWorkAssign.ShowDialog();
        }
        #endregion

        #region JO|Mechanics Details Cell Formatting
        private void MasterTemplate_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnStatus")
            {
                rgvJO.Grid_CellFormatting(sender, e, "Update Status");
            }
            if (e.CellElement.ColumnInfo.Name == "btnAssignMech")
            {
                rgvJO.Grid_CellFormatting(sender, e, "Assign Mechanics");
            }
            if (e.CellElement.ColumnInfo.Name == "WorkPercentage")
            {
                rgvJO.Grid_CellFormatting(sender, e);
            }


            //formatting
            if (this.dgvJODetails.Columns[e.ColumnIndex].Name == "Price")
            {
                GridViewDecimalColumn priceFormat = this.dgvJODetails.Columns["Price"] as GridViewDecimalColumn;
                priceFormat.FormatString = "{0:F2}";
                priceFormat.NullValue = 0;
            }

            if (this.dgvJODetails.Columns[e.ColumnIndex].Name == "Amount")
            {
                GridViewDecimalColumn priceFormat = this.dgvJODetails.Columns["Amount"] as GridViewDecimalColumn;
                priceFormat.FormatString = "{0:F2}";
                priceFormat.NullValue = 0;
            }
        }
        private void radGridView3_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnWorkAssignment")
            {
                rgvJO.Grid_CellFormatting(sender, e, "Assign Work|Job Description");
            }
        }
        #endregion

        #region Status
        void loadStatus()
        {           
            GridViewComboBoxColumn cbDescription = (GridViewComboBoxColumn)dgvJODetails.Columns["cbStatus"];
            StatusRepo = new StatusRepository();
            cbDescription.DropDownStyle = RadDropDownStyle.DropDown;
            cbDescription.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                e.Result = StatusRepo.GetAll();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsStatusBindingSource.DataSource = (List<clsStatus>)e.Result;
            };
            cbDescription.DisplayMember = "StatusName";
            cbDescription.ValueMember = "Id";
            cbDescription.DataSource = clsStatusBindingSource;            
            bg.RunWorkerAsync();
            
        }
        #endregion

        #region Display Status
        void DisplayStatus() //DEPRECIATED
        {
            foreach (var jo in ListOfJODetails)
            {
                if (dgvJODetails.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dgvJODetails.ChildRows.Count; i++)
                    {
                        if ((Int64)dgvJODetails.Rows[i].Cells["StatusId"].Value == jo.StatusId)
                        {
                            dgvJODetails.Rows[i].Cells["cbStatus"].Value = jo.StatusId;
                        }
                    }
                }
            }
        }
        #endregion

        #region Async Display Status
        private async Task newDisplayStatus()
        {
            foreach (var jo in ListOfJODetails)
            {
                if (dgvJODetails.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dgvJODetails.ChildRows.Count; i++)
                    {
                        if ((Int64)dgvJODetails.Rows[i].Cells["StatusId"].Value == jo.StatusId)
                        {
                            dgvJODetails.Rows[i].Cells["cbStatus"].Value = jo.StatusId;
                        }
                    }
                }
            }
        }
        #endregion

        private void dgvJODetails_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            //LoadingManager.ShowLoadScreen();
            JOAuditTrail = new clsJOAuditTrail();
            clsJODetails JODet = (clsJODetails)clsJODetailsBindingSource.Current;
            JOAuditTrail.JODetailId = JODet.Id;
            JOAuditTrail.UserId = Program.CurrentUser.Id;
            JOAuditTrail.Username = Program.CurrentUser.FullName;
            JOAuditTrail.StatusId = JODet.StatusId;
            JOAuditTrail.Remarks = JODet.Remarks;
            if (e.Column.Name == "cbStatus")
            {
                JOAuditTrail.StatusId = ((clsStatus)clsStatusBindingSource.Current).Id;
                //LoadingManager.CloseLoadScreen();
            }
            if (e.Column.Name == "Remarks")
            {
                JOAuditTrail.Remarks = JODet.Remarks;
                //LoadingManager.CloseLoadScreen();
            }
            //LoadingManager.CloseLoadScreen();
        }

        #region Save Status
        private void SaveStatus()
        {
            timeLapsed = System.Diagnostics.Stopwatch.StartNew();
            LoadingManager.ShowLoadScreen();
            if (((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Completed || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Cancelled)
            //if (((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Completed || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.ReleaseSustained || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Cancelled)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Unable to save Order detail status. Please check order status above to work with."
                };
                MsgBox.ShowDialog();
            }
            else
            {
                if (JOAuditTrail != null)
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        try
                        {
                            JOAuditTrailRepo = new JOAuditTrailRepository();
                            JOAuditTrailRepo.Add(JOAuditTrail);

                            if (JOAuditTrail.StatusId == 3) //Approved
                            {
                                JODetailRepo.UpdateDateApproved(DateTime.Now, ((clsJODetails)clsJODetailsBindingSource.Current).Id);
                            }

                            SaveStatusTransaction(); //UPDATE STATUS
                            newLoad();
                            LoadPrint();
                            JOAuditTrail = null;
                            ts.Complete();
                            LoadingManager.CloseLoadScreen();
                            frmMsg MsgBox = new frmMsg()
                            {
                                MsgBox = clsEnums.MsgBox.Success,
                                Message = "Record has been successfully saved."
                            };
                            MsgBox.ShowDialog();


                            //timeLapsed checker
                            long elapsedMs = timeLapsed.ElapsedMilliseconds / 1000;
                            RadMessageBox.Show(@"Total time lapsed: " + elapsedMs + " seconds");

                            #region unused code
                            //load();
                            //NewLoadJODetails();
                            //LoadJODetails();
                            //NewLoadMechanics();
                            //LoadMechanics();
                            //loadAuditTrail();
                            #endregion
                        }
                        catch (Exception)
                        {
                            LoadingManager.CloseLoadScreen();
                            ts.Dispose();
                            throw;
                        }
                    }
                }
                else
                {
                    LoadingManager.CloseLoadScreen();
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "No changed detected, save cancelled."
                    };
                    MsgBox.ShowDialog();
                }
            }
        }
        #endregion

        #region Save Status Async
        private async Task SaveStatusAsync()
        {
            LoadingManager.ShowLoadScreen();
            if (((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Completed || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Cancelled)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Unable to save Order detail status. Please check order status above to work with."
                };
                MsgBox.ShowDialog();
            }
            else if (JOAuditTrail != null)
            {
                Task t = Task.WhenAll(SaveAuditTrailAsync(), SaveDateApproved(), SaveStatusTransactionSAync());
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    Task u = Task.WhenAll(UpdateJOListRemove(currSelectedJOId));
                    await u;
                    if (u.Status == TaskStatus.RanToCompletion)
                        await UpdateJOlistReAdd(currSelectedJOId);
                    LoadPrint();
                    LoadingManager.CloseLoadScreen();
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "Record has been successfully saved."
                    };
                    MsgBox.ShowDialog();
                }
            }
            else
            {
                LoadingManager.CloseLoadScreen();
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "No changed detected, save cancelled."
                };
                MsgBox.ShowDialog();
            }
        }
        #endregion

        #region Save Status Transaction
        private void SaveStatusTransaction()
        {
            clsEnums.JOROStatus Status;
            //List<clsStatus> ListOfStatus = new List<clsStatus>();
            //StatusRepo = new StatusRepository();
            //ListOfStatus = StatusRepo.SearchBy(" Active = 1");

            List<clsJODetails> ListOfJODet = new List<clsJODetails>();
            //List<clsJODetails> ApproveStatus = new List<clsJODetails>();

            JODetailRepo = new JODetailRepository();

            Int64 ApproveCount = 0;
            Int64 CompletedCount = 0;
            Int64 SustainedCount = 0;
            Int64 CancelledCount = 0;

            ListOfJODet = JODetailRepo.SearchBy(" WHERE JD.JOId = " + ((clsJODetails)clsJODetailsBindingSource.Current).JOId + " AND JD.IsActive = 1");
            ListOfStatus.ForEach(x =>
            {
                ApproveCount += ListOfJODet.Where(y => y.StatusId == x.Id && x.IsApproval == true).Count();
                CompletedCount += ListOfJODet.Where(y => y.StatusId == x.Id && x.IsCompleted == true).Count();
                SustainedCount += ListOfJODet.Where(y => y.StatusId == x.Id && x.IsSustained == true).Count();
                CancelledCount += ListOfJODet.Where(y => y.StatusId == x.Id && x.IsCancelled == true).Count();
            });

            if (SustainedCount != 0)
            {
                Status = clsEnums.JOROStatus.Sustained;
            }
            else if (CompletedCount != 0)
            {
                if (CompletedCount == ListOfJODet.Count)
                {
                    Status = clsEnums.JOROStatus.Completed;
                }
                else
                {
                    Status = clsEnums.JOROStatus.PartiallyCompleted;
                }
            }
            else if (ApproveCount != 0)
            {
                if (ApproveCount == ListOfJODet.Count)
                {
                    Status = clsEnums.JOROStatus.Approved;
                }
                else
                {
                    Status = clsEnums.JOROStatus.PartiallyApproved;
                }
            }
            else if (CancelledCount != 0)
            {
                if (CancelledCount == ListOfJODet.Count)
                {
                    Status = clsEnums.JOROStatus.Cancelled;
                }
                else
                {
                    Status = clsEnums.JOROStatus.Generated;
                }
            }
            else
            {
                Status = clsEnums.JOROStatus.Generated;
            }
            JORepo = new JORepository();
            JORepo.UpdateJOStatus(Status, ((clsJODetails)clsJODetailsBindingSource.Current).JOId);
        }
        #endregion

        #region Save Audit Trail Async
        private async Task SaveAuditTrailAsync()
        {
            JOAuditTrailRepo = new JOAuditTrailRepository();
            await Task.Run(() => JOAuditTrailRepo.Add(JOAuditTrail));
        }
        private async Task SaveDateApproved()
        {
            if (JOAuditTrail.StatusId == 3) //Approved
            {
                await Task.Run(() => JODetailRepo.UpdateDateApproved(DateTime.Now, ((clsJODetails)clsJODetailsBindingSource.Current).Id));
            }
        }
        #endregion

        #region Save Status Transaction Async
        private async Task SaveStatusTransactionSAync()
        {
            clsEnums.JOROStatus Status;

            List<clsJODetails> ListOfJODet = new List<clsJODetails>();

            JODetailRepo = new JODetailRepository();

            Int64 ApproveCount = 0;
            Int64 CompletedCount = 0;
            Int64 SustainedCount = 0;
            Int64 CancelledCount = 0;

            ListOfJODet = JODetailRepo.SearchBy(" WHERE JD.JOId = " + ((clsJODetails)clsJODetailsBindingSource.Current).JOId + " AND JD.IsActive = 1");
            ListOfStatus.ForEach(x =>
            {
                ApproveCount += ListOfJODet.Where(y => y.StatusId == x.Id && x.IsApproval == true).Count();
                CompletedCount += ListOfJODet.Where(y => y.StatusId == x.Id && x.IsCompleted == true).Count();
                SustainedCount += ListOfJODet.Where(y => y.StatusId == x.Id && x.IsSustained == true).Count();
                CancelledCount += ListOfJODet.Where(y => y.StatusId == x.Id && x.IsCancelled == true).Count();
            });

            if (SustainedCount != 0)
            {
                Status = clsEnums.JOROStatus.Sustained;
            }
            else if (CompletedCount != 0)
            {
                if (CompletedCount == ListOfJODet.Count)
                {
                    Status = clsEnums.JOROStatus.Completed;
                }
                else
                {
                    Status = clsEnums.JOROStatus.PartiallyCompleted;
                }
            }
            else if (ApproveCount != 0)
            {
                if (ApproveCount == ListOfJODet.Count)
                {
                    Status = clsEnums.JOROStatus.Approved;
                }
                else
                {
                    Status = clsEnums.JOROStatus.PartiallyApproved;
                }
            }
            else if (CancelledCount != 0)
            {
                if (CancelledCount == ListOfJODet.Count)
                {
                    Status = clsEnums.JOROStatus.Cancelled;
                }
                else
                {
                    Status = clsEnums.JOROStatus.Generated;
                }
            }
            else
            {
                Status = clsEnums.JOROStatus.Generated;
            }
            await Task.Run(() => UpdateJOStatusAsync(Status));
        }
        #endregion

        #region Update JO Status
        private async Task UpdateJOStatusAsync(clsEnums.JOROStatus Status)
        {
            JORepo = new JORepository();
            await Task.Run(() => JORepo.UpdateJOStatus(Status, ((clsJODetails)clsJODetailsBindingSource.Current).JOId));
        }
        #endregion

        #region Update JO List - REMOVE
        private async Task UpdateJOListRemove(long currSelectedJOId)
        {
            await Task.Run(() => ListOfJobOrder.RemoveAll(x => x.Id == currSelectedJOId));
        }
        #endregion

        #region Update JO List - READD
        private async Task UpdateJOlistReAdd(long currSelectedJOId)
        {
            JORepo = new JORepository();
            clsJobOrder updatedJO = await Task.Run(() => JORepo.FindByID(currSelectedJOId));
            ListOfJobOrder.Add(updatedJO);
            clsJobOrderBindingSource.DataSource = await Task.Run(() => ListOfJobOrder);
            rgvJO.DataSource = clsJobOrderBindingSource;
            clsJobOrderBindingSource.ResetBindings(true);

            SortTableJONoDescending();
        }
        #endregion

        #region Sort Table By JO No Descending
        private void SortTableJONoDescending()
        {
            SortDescriptor descriptor = new SortDescriptor();
            descriptor.PropertyName = "JONoStr";
            descriptor.Direction = ListSortDirection.Descending;
            this.rgvJO.MasterTemplate.SortDescriptors.Add(descriptor);
        }
        #endregion

        #region Refresh
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadingManager.ShowLoadScreen();
            await newLoad();
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnRefresh, "Refresh");
            LoadingManager.CloseLoadScreen();
        }
        #endregion

        #region Search Button
        private async void tbSearch_Click(object sender, EventArgs e)
        {
            timeLapsed = System.Diagnostics.Stopwatch.StartNew();
            LoadingManager.ShowLoadScreen();
            await ClearSources();

            Task t = Task.WhenAll(newLoad());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                int cn = rgvJO.RowCount;
                txtRowCount.Text = "Total Number of Job Orders by filter:  " + cn.ToString();

                LoadingManager.CloseLoadScreen();
            }
        }
        #endregion

        private void radCollapsiblePanel1_Expanded(object sender, EventArgs e)
        {
            //rclbStatus.DataSource = Enum.GetValues(typeof(clsEnums.JOROStatus));
            //if (cbBranch.Items == null || cbBranch.Items.Count == 0)
            //{
            //    loadBranch();
            //}
        }

        #region Load Branch
        private async Task LoadBranch()
        {
            BranchRepository BranchRepo = new BranchRepository();
            cbBranch.DataSource = await Task.Run(() => BranchRepo.GetAllActive());
            cbBranch.DisplayMember = "BranchName";
            cbBranch.ValueMember = "Id";

            #region Previous Code
            //BranchRepository BranchRepo = new BranchRepository();
            //cbBranch.DataSource = BranchRepo.SearchBy(" where Active = 1");
            //cbBranch.DisplayMember = "BranchName";
            //cbBranch.ValueMember = "Id";
            #endregion
        }


        #endregion

        #region Load JO Status
        private async Task LoadJOStatus()
        {
            //rddStatus.DataSource = new StatusRepository().GetAll();
            rddStatus.DataSource = await Task.Run(() => EnumHelper<FGCIJOROSystem.Domain.Enums.clsEnums.JOROStatus>.GetTypeEnum2());
            rddStatus.ValueMember = "Id";
            rddStatus.DisplayMember = "Name";
            //rddStatus.SelectedValue = 3;
        }
        #endregion

        #region Load List of Status
        private async Task LoadListOfStatus()
        {
            StatusRepo = new StatusRepository();
            ListOfStatus = await Task.Run(() => StatusRepo.SearchBy(" Active = 1"));
        }
        #endregion

        #region Clear Sources
        private async Task ClearSources()
        {
            clsJODetailsBindingSource.Clear();
            clsMechanicsBindingSource.Clear();
            clsJOAuditTrailBindingSource.Clear();
            this.reportViewer1.RefreshReport();
            //await newLoad();
        }
        #endregion

        #region New Load Search Details
        async Task newLoadSearchDetails(string whereQuery)
        {
            JORepo = new JORepository();
            if (cbBranch.Items == null || cbBranch.Items.Count == 0)
            {
                ListOfJobOrder = JORepo.SearchBy(" WHERE JO.BranchId = " + Program.CurrentUser.BranchId + " " + whereQuery);
            }
            else
            {
                ListOfJobOrder = JORepo.SearchBy(" WHERE JO.BranchId = " + cbBranch.SelectedValue.ToString() + " " + whereQuery);
            }

            clsJobOrderBindingSource.DataSource = (List<clsJobOrder>)ListOfJobOrder;
        }
        #endregion

        #region Old Search Job Details
        void loadSearchJoDetails(String whereQuery)
        {

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (s, e) =>
            {
                JORepo = new JORepository();
                if (cbBranch.Items == null || cbBranch.Items.Count == 0)
                {
                    e.Result = JORepo.SearchBy(" WHERE JO.BranchId = " + Program.CurrentUser.BranchId + " " + whereQuery);
                }
                else
                {
                    e.Result = JORepo.SearchBy(" WHERE JO.BranchId = " + cbBranch.SelectedValue.ToString() + " " + whereQuery);
                }

            };
            bw.RunWorkerCompleted += (s, e) =>
            {
                clsJobOrderBindingSource.DataSource = (List<clsJobOrder>)e.Result;
            };

            bw.RunWorkerAsync();
        }
        #endregion


        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnTransactionLogs")
            {
                rgvJO.Grid_CellFormatting(sender, e, "Transaction Logs");
            }
        }       

        #region Parts Request
        void LoadPRS() //DEPRECIATED
        {
            rptJOROPRS report = new rptJOROPRS(MapProperties());
            var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            this.reportViewer2.ReportSource = reportSource;
            this.reportViewer2.RefreshReport();
        }

        private async Task NewLoadPRS()
        {
            rptJOROPRS report = new rptJOROPRS(MapProperties());
            var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            this.reportViewer2.ReportSource = await Task.Run (() => reportSource);
            this.reportViewer2.RefreshReport();
        }

        #endregion

        #region radGridView1 Cell Click
        private async void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            //long SelectedRow = ((clsJobOrder)clsJobOrderBindingSource.Current).Id;
            //Check if same
            if (currSelectedJOId == ((clsJobOrder)clsJobOrderBindingSource.Current).Id)
                return;

            DisableProperties();
            LoadingManager.ShowLoadScreen();
            currSelectedJOId = ((clsJobOrder)clsJobOrderBindingSource.Current).Id;
            if (clsJobOrderBindingSource.Current != null)
            {
                Task t = Task.WhenAll(NewLoadJODetails(), NewLoadMechanics(), NewLoadAuditTrail(), NewLoadPRS());
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    if (((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released)
                    {
                        dgvJODetails.Columns["cbStatus"].ReadOnly = true;
                        dgvJODetails.Columns["btnStatus"].ReadOnly = true;
                        dgvJODetails.Columns["Remarks"].ReadOnly = true;
                    }
                    else
                    {
                        dgvJODetails.Columns["cbStatus"].ReadOnly = false;
                        dgvJODetails.Columns["btnStatus"].ReadOnly = false;
                        dgvJODetails.Columns["Remarks"].ReadOnly = false;
                    }
                    LoadingManager.CloseLoadScreen();
                }
                else
                {
                    LoadingManager.CloseLoadScreen();
                    RadMessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
                DisplayStatus();
            }
            dgvJODetails.Enabled = true;
        }
        #endregion

        #region Disable properties
        void DisableProperties()
        {
            //if ((bool)(radGridView1.DataSource = null))
            //{
            //    return;
            //}
            if (((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Completed || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.ReleaseSustained || ((clsJobOrder)clsJobOrderBindingSource.Current).Status == clsEnums.JOROStatus.Cancelled)
            {
                //dgvJODetails.Enabled = false;
            }
            else
            {
                dgvJODetails.Enabled = true;
            }
        }
        #endregion

        #region tbxEquipmentLocation Enter
        private void tbxEquipmentLocation_Enter(object sender, EventArgs e)
        {
            tbxEquipmentLocation.SelectionStart = 0;
            tbxEquipmentLocation.SelectionLength = tbxEquipmentLocation.Text.Length;
        }

        private void tbxEquipmentLocation_Click(object sender, EventArgs e)
        {
            tbxEquipmentLocation.SelectionStart = 0;
            tbxEquipmentLocation.SelectionLength = tbxEquipmentLocation.Text.Length;
        }
        #endregion

        #region tbxEquipmentCode Enter
        private void tbxEquipmentCode_Enter(object sender, EventArgs e)
        {
            tbxEquipmentCode.SelectionStart = 0;
            tbxEquipmentCode.SelectionLength = tbxEquipmentCode.Text.Length;
        }

        private void tbxEquipmentCode_Click(object sender, EventArgs e)
        {
            tbxEquipmentCode.SelectionStart = 0;
            tbxEquipmentCode.SelectionLength = tbxEquipmentCode.Text.Length;
        }
        #endregion

        #region Toggledstate on search filters
        private void cbEquipmentCode_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (cbEquipmentCode.IsChecked)
            {
                tbxEquipmentCode.Enabled = true;

                cbDate.IsChecked = false;
                cbOrderNo.Checked = false;
                cbEquipmentLocation.IsChecked = false;
                cbStatus.IsChecked = false;
            }
            else
            {
                tbxEquipmentCode.Enabled = false;
            }
        }

        private void cbEquipmentLocation_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (cbEquipmentLocation.IsChecked)
            {
                tbxEquipmentLocation.Enabled = true;

                cbDate.IsChecked = false;
                cbOrderNo.IsChecked = false;
                cbEquipmentCode.IsChecked = false;
                cbStatus.IsChecked = false;
            }
            else
            {
                tbxEquipmentLocation.Enabled = false;
            }
        }

        private void cbDate_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (cbDate.IsChecked)
            {
                dtDateFrom.Enabled = true;
                dtDateTo.Enabled = true;

                cbOrderNo.IsChecked = false;
                cbEquipmentCode.IsChecked = false;
                cbEquipmentLocation.IsChecked = false;
                cbStatus.IsChecked = false;
            }
            else
            {
                dtDateFrom.Enabled = false;
                dtDateTo.Enabled = false;
            }
        }

        private void cbStatus_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (cbStatus.IsChecked)
            {
                rddStatus.Enabled = true;

                cbDate.IsChecked = false;
                cbOrderNo.IsChecked = false;
                cbEquipmentCode.IsChecked = false;
                cbEquipmentLocation.IsChecked = false;
            }
            else
            {
                rddStatus.Enabled = false;
                rddStatus.SelectedValue = 0;
            }
        }
        private void cbOrderNo_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (cbOrderNo.IsChecked)
            {
                tbxOrderNo.Clear();
                tbxOrderNo.Enabled = true;

                cbDate.IsChecked = false;
                cbEquipmentCode.IsChecked = false;
                cbEquipmentLocation.IsChecked = false;
                cbStatus.IsChecked = false;
            }
            else
            {
                tbxOrderNo.Text = @"last 4 digits";
                tbxOrderNo.Enabled = false;
            }
        }
        #endregion

        private void radGridView3_CommandCellClick(object sender, GridViewCellEventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnWorkAssignment")
            {
                frmMechanicWorkAssignment frmMechWorkAssign = new frmMechanicWorkAssignment((clsMechanics)clsMechanicsBindingSource.Current);
                frmMechWorkAssign.ShowDialog();
            }
        }
    }
}
