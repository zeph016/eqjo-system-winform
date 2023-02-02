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
using FGCIJOROSystem.DAL.Repositories.RORepo;
using FGCIJOROSystem.Domain.RescueOrder;
using FGCIJOROSystem.Domain.Enums;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vRescueOrder.vROWorkAssignment;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Status;
using System.Transactions;
using Telerik.WinControls;
using FGCIJOROSystem.Presentation.vJORODirectPrint;
using FGCIJOROSystem.Presentation.vJobOrder.vWorkAssignment;
using FGCIJOROSystem.Reports.rJobOrder;
using FGCIJOROSystem.Presentation.vJobOrder;
using FGCIJOROSystem.Reports.rPRS;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Presentation.vLoader;
using Telerik.WinControls.Data;

namespace FGCIJOROSystem.Presentation.vRescueOrder
{
    public partial class ucROList : UserControl
    {
        #region Properties
        RORepository RORepo;
        RODetailRepository RODetailRepo;
        ROMechanicRepository ROMechanicRepo;
        ROAuditTrailRepository ROAuditTrailRepo;
        StatusRepository StatusRepo;

        clsEnums.CRUDEMode CRUDEMode;

        List<clsRODetails> ListOfRODetails;
        List<clsROMechanics> ListOfROMechanics;
        List<clsROAuditTrail> ListOfROAuditTrail;
        List<clsStatus> ListOfStatus;
        List<clsRescueOrder> ListOfRescueOrder;

        clsROAuditTrail ROAuditTrail;

        public frmMainWindow MainWindowPage;
        public bool taskComplete;
        public Progress<int> Progress = new Progress<int>();
        private long currSelectedROId;
        #endregion

        public ucROList()
        {
            InitializeComponent();
            ListOfRODetails = new List<clsRODetails>();
            ListOfROMechanics = new List<clsROMechanics>();
            ListOfROAuditTrail = new List<clsROAuditTrail>();
            ListOfStatus = new List<clsStatus>();
            ListOfRescueOrder = new List<clsRescueOrder>();

            this.dgvRODetails.Columns["btnStatus"].IsVisible = true;
            this.dgvRODetails.Columns["WorkPercentage"].IsVisible = false;
            this.dgvRODetails.Columns["NoOfMechanics"].IsVisible = false;
            this.dgvRODetails.Columns["WorkTimeSpan"].IsVisible = false;
            this.dgvRODetails.Columns["IsActive"].IsVisible = false;
        }

        #region Form Load
        private async void ucROList_Load(object sender, EventArgs e)
        {
            LoadingManager.ShowLoadScreen();
            dtDateFrom.Format = DateTimePickerFormat.Custom;
            dtDateFrom.CustomFormat = "MM/dd/yyyy";
            dtDateTo.Format = DateTimePickerFormat.Custom;
            dtDateTo.CustomFormat = "MM/dd/yyyy";

            dtDateFrom.Value = DateTime.Now;
            dtDateTo.Value = DateTime.Now;

            clsRODetailsBindingSource.DataSource = ListOfRODetails;
            clsROMechanicsBindingSource.DataSource = ListOfROMechanics;

            Task t = Task.WhenAll(loadROStatus(), LoadBranch(), LoadListStatus());
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
        void load() //Depreciated
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                //Application.UseWaitCursor = true;  //chloe
                RORepo = new RORepository();
                e.Result = RORepo.SearchBy("WHERE RO.Status != 4 AND RO.Status != 5 AND RO.Status != 7 ORDER BY RO.RODate DESC");//RO.Status DESC, 
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                //Application.UseWaitCursor = false;  //chloe
                clsRescueOrderBindingSource.DataSource = (List<clsRescueOrder>)e.Result;

                int cn = rgvRO.RowCount; //
                txtRowCount.Text = "Total Number of Rescue Orders :  " + cn.ToString(); //
            };
            bg.RunWorkerAsync();
        }
        #endregion

        #region Async Load
        private async Task newLoad()
        {
            string SearchStatement = "";
            if (cbEquipmentCode.IsChecked)
            {
                SearchStatement += " EQ.PPEName LIKE '" + tbxEquipmentCode.Text + "%' AND";
            }
            if (cbEquipmentLocation.IsChecked)
            {
                SearchStatement += " RO.Location LIKE '" + tbxEquipmentLocation.Text + "%'";
            }
            if (cbDate.IsChecked)
            {
                SearchStatement += " RO.RODate BETWEEN '" + dtDateFrom.Value.Date + "' AND '" + dtDateTo.Value.Date + "' AND";
            }
            if (cbStatus.IsChecked)
            {
                SearchStatement += " RO.Status = " + rddStatus.SelectedValue + " AND";
            }
            if (cbStatus.IsChecked)
            {
                if (String.IsNullOrEmpty(tbxOrderNo.Text))
                {
                    RadMessageBox.Show(@"Order Number is Empty", @"Error: Empty", MessageBoxButtons.RetryCancel, RadMessageIcon.Exclamation);
                    return;
                }
                else
                {
                    SearchStatement += " RO.RONo LIKE '%" + tbxOrderNo.Text + "%' AND";
                }
            }

            char[] removeEnd = { 'D', 'N', 'A' };
            string finalSearchStatmement = SearchStatement.TrimEnd(removeEnd);

           
            RORepo = new RORepository();
            ListOfRescueOrder = await Task.Run(() => RORepo.SearchBy(" WHERE " + finalSearchStatmement + " ORDER BY RO.RODate DESC"));//RO.Status DESC, 
            clsRescueOrderBindingSource.DataSource = await Task.Run(() => ListOfRescueOrder);
        }
        #endregion

        #region MasterTemplate Selection Changed
        private async void MasterTemplate_SelectionChanged(object sender, EventArgs e)
        {
            //LoadingManager.ShowLoadScreen();
            //if (clsRescueOrderBindingSource.Current != null)
            //{
            //    Task t = Task.WhenAll(NewLoadRODetails(), NewLoadROMechanics(), NewLoadAuditTrail(), NewLoadPRS());
            //    await t;
            //    if (t.Status == TaskStatus.RanToCompletion)
            //    {
            //        if (((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released)
            //        {
            //            //dgvRODetails.Columns["cbStatus"].ReadOnly = true;
            //            //dgvRODetails.Columns["btnStatus"].ReadOnly = true;
            //            //dgvRODetails.Columns["Remarks"].ReadOnly = true;
            //            dgvRODetails.Columns["cbStatus"].ReadOnly = false;
            //            dgvRODetails.Columns["btnStatus"].ReadOnly = false;
            //            dgvRODetails.Columns["Remarks"].ReadOnly = false;
            //        }
            //        else
            //        {
            //            dgvRODetails.Columns["cbStatus"].ReadOnly = false;
            //            dgvRODetails.Columns["btnStatus"].ReadOnly = false;
            //            dgvRODetails.Columns["Remarks"].ReadOnly = false;
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
            //dgvRODetails.Enabled = true;
        }
        #endregion

        #region Load RO Details
        private void loadRODetails() //DEPRECIATED
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                RODetailRepo = new RODetailRepository();
                ListOfRODetails = RODetailRepo.SearchBy(" WHERE RD.ROId = " + ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id + " AND RD.IsActive = 1");
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsRODetailsBindingSource.DataSource = ListOfRODetails;
                DisplayStatus();
                LoadPrint();
            };
            bg.RunWorkerAsync();
            clsRODetailsBindingSource.ResetBindings(true);
        }
        #endregion

        #region Async Load RO Status
        private async Task loadROStatus()
        {
            rddStatus.DataSource = await Task.Run(() => EnumHelper<FGCIJOROSystem.Domain.Enums.clsEnums.JOROStatus>.GetTypeEnum2());
            rddStatus.ValueMember = "Id";
            rddStatus.DisplayMember = "Name";

            #region Old Code
            //rddStatus.DataSource = new StatusRepository().GetAll();
            //rddStatus.ValueMember = "Id";
            //rddStatus.DisplayMember = "StatusName";
            //rddStatus.SelectedValue = 3;
            #endregion
        }
        #endregion

        #region Async Load Branch
        private async Task LoadBranch()
        {
            BranchRepository BranchRepo = new BranchRepository();
            cbBranch.DataSource = await Task.Run(() => BranchRepo.GetAllActive());
            cbBranch.DisplayMember = "BranchName";
            cbBranch.ValueMember = "Id";
        }
        #endregion

        #region Async Load RO Details
        private async Task NewLoadRODetails()
        {
            RODetailRepo = new RODetailRepository();
            ListOfRODetails = await Task.Run(() => RODetailRepo.SearchBy(" WHERE RD.ROId = " + ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id + " AND RD.IsActive = 1"));
            clsRODetailsBindingSource.DataSource = ListOfRODetails.ToList();

            Task t = Task.WhenAll(NewDisplayStatus());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                clsRODetailsBindingSource.ResetBindings(true);
                LoadPrint();
            }
        }
        #endregion

        #region Load RO Mechanics
        private void loadROMechanics() //DEPRECIATED
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ROMechanicRepo = new ROMechanicRepository();
                ListOfROMechanics = ROMechanicRepo.SearchBy(" WHERE RM.ROId = " + ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id + " AND RM.IsActive = 1");
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsROMechanicsBindingSource.DataSource = ListOfROMechanics;
            };
            bg.RunWorkerAsync();
            clsRODetailsBindingSource.ResetBindings(true);
        }
        #endregion

        #region Async Load RO Mechanics
        private async Task NewLoadROMechanics()
        {
            ROMechanicRepo = new ROMechanicRepository();
            ListOfROMechanics = await Task.Run(() => ROMechanicRepo.SearchBy(" WHERE RM.ROId = " + ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id + " AND RM.IsActive = 1"));

            clsROMechanicsBindingSource.DataSource = ListOfROMechanics;

            clsRODetailsBindingSource.ResetBindings(true);
        }
        #endregion

        #region Load Audit Trail
        private void loadAuditTrail() //DEPRECIATED
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ROAuditTrailRepo = new ROAuditTrailRepository();
                ListOfROAuditTrail = ROAuditTrailRepo.SearchBy(" WHERE RD.ROId = " + ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsROAuditTrailBindingSource.DataSource = ListOfROAuditTrail;
            };
            bg.RunWorkerAsync();
            //clsROAuditTrailBindingSource.ResetBindings(true);

            //Application.UseWaitCursor = false; //chloe
        }
        #endregion

        #region Async Load Audit Trail
        private async Task NewLoadAuditTrail()
        {
            ROAuditTrailRepo = new ROAuditTrailRepository();
            ListOfROAuditTrail = await Task.Run(() => ROAuditTrailRepo.SearchBy(" WHERE RD.ROId = " + ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id));
            clsROAuditTrailBindingSource.DataSource = ListOfROAuditTrail;

            progressBarRO.Value = 80;
        }
        #endregion

        #region Async Load List Status
        private async Task LoadListStatus()
        {
            StatusRepo = new StatusRepository();
            ListOfStatus = await Task.Run(() => StatusRepo.SearchBy(" Active = 1"));
        }
        #endregion

        #region radGridView10 Command Cell Click
        private async void radGridView10_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnStatus")
            {
                //SaveStatus();
                await SaveStatusAsync();
            }
            if (cell.ColumnInfo.Name == "btnAssignMech")
            {
                frmRODetailWorkAssignment RODetailWorkAssignPage = new frmRODetailWorkAssignment((clsRODetails)clsRODetailsBindingSource.Current);
                //StatusUpdatePage.DataEvent.OnDataConfirm += Status_DataEvent_OnDataConfirm;
                RODetailWorkAssignPage.ShowDialog();
            }
        }
        #endregion

        #region Create New
        private void mCreateNew_Click(object sender, EventArgs e)
        {
            ucROEntry ROEntryPage = new ucROEntry();
            ROEntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvROEntry",
                Text = @"Rescue Order Entry",
                //Image = Properties.Resources.home
            }, ROEntryPage);
        }

        private async void DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                await newLoad();
                //load();
            }
        }
        #endregion

        #region Update RO Form
        private void mUpdateRO_Click(object sender, EventArgs e)
        {
            if (((clsRescueOrder)clsRescueOrderBindingSource.Current).Status != clsEnums.JOROStatus.Completed && ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status != clsEnums.JOROStatus.Released && ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status != clsEnums.JOROStatus.ReleaseSustained)
            {
                ucROEntry ROEntryPage = new ucROEntry(MapProperties());
                ROEntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvJOUpdateEntry",
                    Text = @"Update Rescue Order Entry",
                    //Image = Properties.Resources.home
                }, ROEntryPage);
            }
        }
        #endregion

        #region Map Properties
        clsRescueOrder MapProperties()
        {
            clsRescueOrder curRO = new clsRescueOrder();
            ROMechanicRepo = new ROMechanicRepository();
            RODetailRepo = new RODetailRepository();

            curRO = (clsRescueOrder)clsRescueOrderBindingSource.Current;
            curRO.UserId = Program.CurrentUser.Id;
            curRO.EncoderName = Program.CurrentUser.FullName;
            curRO.EncoderPosition = Program.CurrentUser.Position;
            curRO.ListOfRODetails = RODetailRepo.SearchBy(" WHERE RD.ROId = " + ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id + "");
            curRO.ListOfROMechanics = ROMechanicRepo.SearchBy(" WHERE RM.ROId = " + ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id + "");
            return curRO;
        }
        #endregion

        private void MasterTemplate_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnTransLogs")
            {
                frmROTransactionLogs TransLogPage = new frmROTransactionLogs((clsRescueOrder)clsRescueOrderBindingSource.Current);
                TransLogPage.ShowDialog();
            }
        }
        #region MapTransactionLogs
        clsROTransactionLogs MapTransactionLog()
        {
            clsROTransactionLogs TransactionLog = new clsROTransactionLogs();
            clsRescueOrder RescueOrder = (clsRescueOrder)clsRescueOrderBindingSource.Current;
            TransactionLog.ROId = RescueOrder.Id;
            TransactionLog.RONo = RescueOrder.RONo;
            TransactionLog.Type = RescueOrder.Type;
            TransactionLog.RODate = RescueOrder.RODate;
            //TransactionLog.EquipmentOnBranch = RescueOrder.EquipmentOnBranch;
            //TransactionLog.PartsRequest = RescueOrder.PartsRequest;
            TransactionLog.CustomerType = RescueOrder.CustomerType;
            TransactionLog.CustomerId = RescueOrder.CustomerId;
            TransactionLog.BranchId = RescueOrder.BranchId;
            TransactionLog.ItemType = RescueOrder.ItemType;
            TransactionLog.EquipmentId = RescueOrder.EquipmentId;
            TransactionLog.ContractorId = RescueOrder.ContractorId;
            TransactionLog.DriverId = RescueOrder.DriverId;
            TransactionLog.UserId = Program.CurrentUser.Id;
            TransactionLog.UserName = Program.CurrentUser.FullName;
            TransactionLog.UserPosition = Program.CurrentUser.Position;
            ((List<clsRODetails>)clsRODetailsBindingSource.List).ForEach(y =>
            {
                TransactionLog.ListOfRODetailTransactionLogs.Add(
                    new clsRODetailTransactionLogs
                    {
                        ROId = y.ROId,
                        RODetailId = y.Id,
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
            ((List<clsROMechanics>)clsROMechanicsBindingSource.List).ForEach(y =>
            {
                TransactionLog.ListOfROMechanicsTransactionLogs.Add(
                new clsROMechTransactionLogs
                {
                    ROId = y.ROId,
                    ROMechanicId = y.Id,
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
            rptRescueOrder report = new rptRescueOrder(MapProperties());
            var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            this.reportViewer1.ReportSource = reportSource;
            this.reportViewer1.RefreshReport();
        }
        #endregion

        #region Report Viewer
        private void reportViewer1_Print(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            frmDirectPrinting DirectPrintingPage = new frmDirectPrinting(MapTransactionLog()) { rptSource = this.reportViewer1.ReportSource };
            DirectPrintingPage.ShowDialog();
        }
        #endregion

        #region Mechanics
        private void radGridView11_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnAssignJob")
            {
                frmROMechanicWorkAssignment RODetailWorkAssignPage = new frmROMechanicWorkAssignment((clsROMechanics)clsROMechanicsBindingSource.Current);
                RODetailWorkAssignPage.ShowDialog();
            }
        }
        #endregion

        #region dgvRODetails Cell Formatting
        private void dgvRODetails_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnStatus")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Update Status");
            }
            if (e.CellElement.ColumnInfo.Name == "btnAssignMech")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Assign Mechanic");
            }
            if (e.CellElement.ColumnInfo.Name == "WorkPercentage")
            {
                radGridView1.Grid_CellFormatting(sender, e);
            }


            //formatting amount/price
            if (this.dgvRODetails.Columns[e.ColumnIndex].Name == "Price")
            {
                GridViewDecimalColumn priceFormat = this.dgvRODetails.Columns["Price"] as GridViewDecimalColumn;
                priceFormat.FormatString = "{0:F2}";
                priceFormat.NullValue = 0;
            }

            if (this.dgvRODetails.Columns[e.ColumnIndex].Name == "Amount")
            {
                GridViewDecimalColumn priceFormat = this.dgvRODetails.Columns["Amount"] as GridViewDecimalColumn;
                priceFormat.FormatString = "{0:F2}";
                priceFormat.NullValue = 0;
            }
        }
        #endregion

        #region MyRegradGridView10 Cell Formatting
        private void radGridView10_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "btnAssignJob")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Assign Work|Job Description");
            }
        }
        #endregion

        #region Status
        void loadStatus()
        {
            GridViewComboBoxColumn cbDescription = (GridViewComboBoxColumn)dgvRODetails.Columns["cbStatus"];
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
            cbDescription.DataSource = clsStatusBindingSource;
            cbDescription.DisplayMember = "StatusName";
            cbDescription.ValueMember = "Id";
            bg.RunWorkerAsync();

            //rclbStatus.DataSource = Enum.GetValues(typeof(clsEnums.JOROStatus));
        }
        void DisplayStatus() //DEPERECIATED
        {
            foreach (var jo in ListOfRODetails)
            {
                if (dgvRODetails.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dgvRODetails.ChildRows.Count; i++)
                    {
                        if ((Int64)dgvRODetails.Rows[i].Cells["StatusId"].Value == jo.StatusId)
                        {
                            dgvRODetails.Rows[i].Cells["cbStatus"].Value = jo.StatusId;
                        }
                    }
                }
            }
        }

        private async Task NewDisplayStatus()
        {
            foreach (var jo in ListOfRODetails)
            {
                if (dgvRODetails.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dgvRODetails.ChildRows.Count; i++)
                    {
                        if ((Int64)dgvRODetails.Rows[i].Cells["StatusId"].Value == jo.StatusId)
                        {
                            dgvRODetails.Rows[i].Cells["cbStatus"].Value = jo.StatusId;
                        }
                    }
                }
            }

            this.dgvRODetails.Columns["cbStatus"].ReadOnly = true;
            this.dgvRODetails.Columns["btnStatus"].ReadOnly = true;
            this.dgvRODetails.Columns["WorkPercentage"].ReadOnly = true;
            this.dgvRODetails.Columns["NoOfMechanics"].ReadOnly = true;
            this.dgvRODetails.Columns["WorkTimeSpan"].ReadOnly = true;
        }
        private void dgvRODetails_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            //LoadingManager.ShowLoadScreen();
            ROAuditTrail = new clsROAuditTrail();
            clsRODetails RODet = (clsRODetails)clsRODetailsBindingSource.Current;
            ROAuditTrail.UserId = Program.CurrentUser.Id;
            ROAuditTrail.Username = Program.CurrentUser.FullName;
            ROAuditTrail.RODetailId = RODet.Id;
            ROAuditTrail.StatusId = RODet.StatusId;
            ROAuditTrail.Remarks = RODet.Remarks;
            if (e.Column.Name == "cbStatus")
            {
                //LoadingManager.CloseLoadScreen();
                ROAuditTrail.StatusId = ((clsStatus)clsStatusBindingSource.Current).Id;
            }
            if (e.Column.Name == "Remarks")
            {
                //LoadingManager.CloseLoadScreen();
                ROAuditTrail.Remarks = RODet.Remarks;
            }
            //LoadingManager.CloseLoadScreen();
        }

        #region Save Status
        private async void SaveStatus()
        {
            LoadingManager.ShowLoadScreen();
            //if (((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Completed || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.ReleaseSustained || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Cancelled)
            if (((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Completed || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Cancelled)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Unable to save Rescue Order detail status. Please check job order status above to work with."
                };
                MsgBox.ShowDialog();
            }
            else
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        ROAuditTrailRepo = new ROAuditTrailRepository();
                        ROAuditTrailRepo.Add(ROAuditTrail);

                        if (ROAuditTrail.StatusId == 3) //Approved
                        {
                            RODetailRepo.UpdateDateApproved(DateTime.Now, ((clsRODetails)clsRODetailsBindingSource.Current).Id);
                        }

                        SaveStatusTransaction(); //UPDATE STATUS
                        newLoad();
                        //load();
                        LoadPrint();
                        ts.Complete();
                        LoadingManager.CloseLoadScreen();
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Success,
                            Message = "The record has been successfully saved."
                        };
                        MsgBox.ShowDialog();
                    }
                    catch (Exception)
                    {
                        LoadingManager.CloseLoadScreen();
                        ts.Dispose();
                        throw;
                    }
                }
            }
        }
        #endregion

        #region Save Status Async
        private async Task SaveStatusAsync()
        {
            LoadingManager.ShowLoadScreen();
            if (((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Completed || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Cancelled)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Unable to save Rescue Order detail status. Please check job order status above to work with."
                };
                MsgBox.ShowDialog();
            }
            else if (ROAuditTrail != null)
            {
                Task t = Task.WhenAll(SaveAuditTrailAsync(), SaveDateApproved(), SaveStatusTransactionAsync());
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    Task u = Task.WhenAll(UpdateROListRemove(currSelectedROId));
                    await u;
                    if (u.Status == TaskStatus.RanToCompletion)
                        await UpdateROListReAdd(currSelectedROId);
                }
                LoadPrint();
                LoadingManager.CloseLoadScreen();
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Success,
                    Message = "The record has been successfully saved."
                };
                MsgBox.ShowDialog();
            }
        }
        #endregion

        #region Save Status Transaction
        private void SaveStatusTransaction()
        {
            clsEnums.JOROStatus Status;
          

            List<clsRODetails> ListOfJODet = new List<clsRODetails>();

            RODetailRepo = new RODetailRepository();

            Int64 ApproveCount = 0;
            Int64 CompletedCount = 0;
            Int64 SustainedCount = 0;
            Int64 CancelledCount = 0;

            ListOfJODet = RODetailRepo.SearchBy(" WHERE RD.ROId = " + ((clsRODetails)clsRODetailsBindingSource.Current).ROId + " AND RD.IsActive = 1");
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
            RORepo = new RORepository();
            RORepo.UpdateROStatus(Status, ((clsRODetails)clsRODetailsBindingSource.Current).ROId);
        }
        #endregion

        #region Async Save Status Transaction
        private async Task SaveStatusTransactionAsync()
        {
            clsEnums.JOROStatus Status;

            List<clsRODetails> ListOfJODet = new List<clsRODetails>();

            RODetailRepo = new RODetailRepository();

            Int64 ApproveCount = 0;
            Int64 CompletedCount = 0;
            Int64 SustainedCount = 0;
            Int64 CancelledCount = 0;

            ListOfJODet = RODetailRepo.SearchBy(" WHERE RD.ROId = " + ((clsRODetails)clsRODetailsBindingSource.Current).ROId + " AND RD.IsActive = 1");
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
            await Task.Run(() => UpdateROStatusAsync(Status));
        }
        #endregion

        #region Update RO Status
        private async Task UpdateROStatusAsync(clsEnums.JOROStatus Status)
        {
            RORepo = new RORepository();
            await Task.Run(() => RORepo.UpdateROStatus(Status, ((clsRODetails)clsRODetailsBindingSource.Current).ROId));
        }
        #endregion

        #region Update RO List - REMOVE
        private async Task UpdateROListRemove(long currSelectedROId)
        {
           await Task.Run(() => ListOfRescueOrder.RemoveAll(x => x.Id == currSelectedROId));
        }
        #endregion

        #region Update RO List - READD
        private async Task UpdateROListReAdd(long currSelectedROId)
        {
            RORepo = new RORepository();
            clsRescueOrder updatedRO = await Task.Run(() => RORepo.FindByID(currSelectedROId));
            ListOfRescueOrder.Add(updatedRO);
            clsRescueOrderBindingSource.DataSource = await Task.Run(() => ListOfRescueOrder);
            rgvRO.DataSource = clsRescueOrderBindingSource;
            clsRescueOrderBindingSource.ResetBindings(true);

            SortTableRONoDescending();
        }
        #endregion

        #region Sort Table BY RO No Descending
        private void SortTableRONoDescending()
        {
            SortDescriptor descriptor = new SortDescriptor();
            descriptor.PropertyName = "RONoStr";
            descriptor.Direction = ListSortDirection.Descending;
            this.rgvRO.MasterTemplate.SortDescriptors.Add(descriptor);
        }
        #endregion

        #region Save Audit Trail Async
        private async Task SaveAuditTrailAsync()
        {
            ROAuditTrailRepo = new ROAuditTrailRepository();
            await Task.Run(() => ROAuditTrailRepo.Add(ROAuditTrail));
        }

        private async Task SaveDateApproved()
        {
            if (ROAuditTrail.StatusId == 3) //Approved
            {
               await Task.Run(() => RODetailRepo.UpdateDateApproved(DateTime.Now, ((clsRODetails)clsRODetailsBindingSource.Current).Id));
            }
        }
        #endregion

        #region Search Options
        private async void tbSearch_Click(object sender, EventArgs e)
        {
            LoadingManager.ShowLoadScreen();
            Task t = Task.WhenAll(newLoad());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                int cn = radGridView1.RowCount;
                txtRowCount.Text = "Total Number of Job Orders by filter:  " + cn.ToString();
                LoadingManager.CloseLoadScreen();
            }
        }
        #endregion

        private async Task Search()
        {
            await newLoad();

            #region Previous Code
            //String SearchStatement = "";
            //string value = "'%" + tbName.Text + "%'";
            //if (rbJONo.IsChecked)
            //{
            //    string ROValue = tbName.Text;
            //    SearchStatement += " AND RO.RONo LIKE " + ROValue + " ";
            //}

            //if (rbEquipmentType.IsChecked)
            //{
            //    SearchStatement += " AND EQ.PPEName LIKE " + value + " ";
            //}

            //if (rbEquipmentName.IsChecked)
            //{
            //    SearchStatement += " AND ET.PPETypeName LIKE " + value + " ";
            //}

            //if (rbEquipmentClass.IsChecked)
            //{
            //    //RadMessageBox.Show("This filter not included in gridview.");
            //    SearchStatement += " AND EC.PPEClassname LIKE " + value + " ";
            //}

            //if (rbJobContractor.IsChecked)
            //{
            //    SearchStatement += " AND C.Firstname LIKE " + value + " OR C.Lastname LIKE " + value + "OR C.Middlename LIKE" + value + "OR C.Middlename LIKE" + value + "OR C.CompanyName LIKE" + value + "OR Sec.GroupDescription LIKE" + value + "OR SC.CompanyName LIKE" + value + "OR SDp.DepartmentName LIKE" + value + "OR SS.SectionName LIKE" + value + "";
            //}

            //if (rbJODate.IsChecked)
            //{
            //    SearchStatement += " AND RO.RODate BETWEEN '" + dtDateFrom.Value.ToString() + "' AND '" + dtDateTo.Value.ToString() + "'";
            //}

            //if (rbDateEncoded.IsChecked)
            //{
            //    RadMessageBox.Show("This filter not included in gridview.");
            //    //SearchStatement += " AND JO.EffectivityDate BETWEEN '" + dtDateFrom.Value.ToString() + "' AND '" + dtDateTo.Value.ToString() + "'";
            //}

            //if (rbDateEncoded.IsChecked)
            //{
            //    RadMessageBox.Show("This filter not included in gridview.");
            //    //SearchStatement += " AND JO.TargetDate BETWEEN '" + dtDateFrom.Value.ToString() + "' AND '" + dtDateTo.Value.ToString() + "'";
            //}

            //if (rclbStatus.CheckedItems.Count > 0)
            //{
            //    SearchStatement += " AND RO.Status IN (" + String.Join(", ", rclbStatus.CheckedItems.Select(x => Convert.ToInt32((clsEnums.JOROStatus)x.Value)).ToArray()) + ")";
            //}

            //SearchStatement += " ORDER BY " + "RO.RODate DESC, RO.RONo DESC";

            //loadSearchJoDetails(SearchStatement);
            //DisableProperties();
            #endregion
        }

        #region Load Search JO Details
        void loadSearchJoDetails(String whereQuery)
        {

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (s, e) =>
            {
                RORepo = new RORepository();
                if (cbBranch.Items == null || cbBranch.Items.Count == 0)
                {
                    e.Result = RORepo.SearchBy(" WHERE RO.BranchId = " + Program.CurrentUser.BranchId + " " + whereQuery);
                }
                else
                {
                    e.Result = RORepo.SearchBy(" WHERE RO.BranchId = " + cbBranch.SelectedValue.ToString() + " " + whereQuery);
                }
            };
            bw.RunWorkerCompleted += (s, e) =>
            {
                clsRescueOrderBindingSource.DataSource = (List<clsRescueOrder>)e.Result;
            };

            bw.RunWorkerAsync();
        }
        #endregion

        private void radGridView9_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnTransactionLogs")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Transaction Logs");
            }
        }
        #endregion

        private void mUpdateStatus_Click(object sender, EventArgs e)
        {
            frmROEquipReleasal ROEquipRelPage = new frmROEquipReleasal();
            ROEquipRelPage.ShowDialog();
        }

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

        #region Parts Request
        void LoadPRS() //DEPRECIATED
        {
            rptJOROPRS report = new rptJOROPRS(MapProperties());
            var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            this.reportViewer2.ReportSource = reportSource;
            this.reportViewer2.RefreshReport();
        }

        async Task NewLoadPRS()
        {
            rptJOROPRS report = new rptJOROPRS(MapProperties());
            var reportSource = await Task.Run (() => new Telerik.Reporting.InstanceReportSource() { ReportDocument = report });
            this.reportViewer2.ReportSource = reportSource;
            this.reportViewer2.RefreshReport();

            progressBarRO.Value = 100;
        }
        #endregion

        #region radGridView9 Cell Click
        private async void radGridView9_CellClick(object sender, GridViewCellEventArgs e)
        {
            //long SelectedRow = ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id;
            //Check if same
            if (currSelectedROId == ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id)
                return;

            DisableProperties();
            LoadingManager.ShowLoadScreen();
            currSelectedROId = ((clsRescueOrder)clsRescueOrderBindingSource.Current).Id;
            if (clsRescueOrderBindingSource.Current != null)
            {
                Task t = Task.WhenAll(NewLoadRODetails(), NewLoadROMechanics(), NewLoadAuditTrail(), NewLoadPRS());
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    if (((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released)
                    {
                        dgvRODetails.Columns["cbStatus"].ReadOnly = false;
                        dgvRODetails.Columns["btnStatus"].ReadOnly = false;
                        dgvRODetails.Columns["Remarks"].ReadOnly = false;
                    }
                    else
                    {
                        dgvRODetails.Columns["cbStatus"].ReadOnly = false;
                        dgvRODetails.Columns["btnStatus"].ReadOnly = false;
                        dgvRODetails.Columns["Remarks"].ReadOnly = false;
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
            dgvRODetails.Enabled = true;
        }
        #endregion

        #region Disable Properties
        private void DisableProperties()
        {
            if (((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Completed || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Released || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.ReleaseSustained || ((clsRescueOrder)clsRescueOrderBindingSource.Current).Status == clsEnums.JOROStatus.Cancelled)
            {
                //dgvRODetails.Enabled = false;
                dgvRODetails.Enabled = true;
            }
            else
            {
                dgvRODetails.Enabled = true;
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
                tbxEquipmentCode.Clear();
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
                tbxEquipmentLocation.Clear();
            }        }

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
            }        }

        #endregion
    }
}
