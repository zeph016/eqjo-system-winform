using FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.EquipmentReleasal;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Domain.JobOrder;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Transactions;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Reports.rEquipmentReleasal;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.DAL.Repositories.EquipmentURRepo;
using FGCIJOROSystem.DAL.Repositories.PPEStatusLogRepo;
using FGCIJOROSystem.Domain.PPEStatusLog;
using System.Threading.Tasks;
using FGCIJOROSystem.Presentation.vLoader;
using Telerik.WinControls;


namespace FGCIJOROSystem.Presentation.vJobOrder
{
    public partial class frmJOEquipReleasal : Telerik.WinControls.UI.RadForm
    {
        #region Properites
        List<clsJobOrder> ListOfJobOrder;
        List<clsJOTransactionLogs> ListOfJOTransLogs;
        List<clsEquipment> ListOfEquipment;
        clsJOReleasalTransaction JOEquipmentReleasal;

        List<clsJOReleased> UnselectedJOReleased;
        EquipmentReleasalRepository JOEquipReleasalRepo;
        JOReleasalTransactionRepository JOReleasalTransactionRepo;
        EquipmentURRepository EquipmentURRepo;
        PPEStatusLogRepository PPEStatusLogRepo;

        clsPPEStatusLog PPEStatusLog;

        JORepository JORepo;
        JODetailRepository JODetailRepo;
        StatusRepository StatusRepo;

        clsEnums.CRUDEMode CRUDEMode;
        public clsDataEvent DataEvent;

        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        #region Methods
        public frmJOEquipReleasal()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            JOEquipmentReleasal = new clsJOReleasalTransaction();
            ListOfJOTransLogs = new List<clsJOTransactionLogs>();
            UnselectedJOReleased = new List<clsJOReleased>();
            CRUDEMode = clsEnums.CRUDEMode.Add;
            currUser = new clsUsersLog();
            PPEStatusLog = new clsPPEStatusLog();
        }
        public frmJOEquipReleasal(clsJOReleasalTransaction obj)
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            ListOfJOTransLogs = new List<clsJOTransactionLogs>();
            UnselectedJOReleased = new List<clsJOReleased>();
            JOEquipmentReleasal = obj;
            CRUDEMode = clsEnums.CRUDEMode.Edit;
            currUser = new clsUsersLog();
            PPEStatusLog = new clsPPEStatusLog();
        }

        private void eqJOReleasal()
        {
            //JOEquipmentReleasal.UserId = Program.CurrentUser.Id;
            //load();
            //int cn = radGridView1.RowCount;
            //txtRowCount.Text = cn.ToString();
            this.Dispose();
            frmJOEquipReleasal frmRefreshForm = new frmJOEquipReleasal();
            frmRefreshForm.ShowDialog();
        }
        private async void frmJOEquipReleasal_Load(object sender, EventArgs e)
        {
            JOEquipmentReleasal.UserId = Program.CurrentUser.Id;
            //await LoadData();
            await LoadDataAsync();
            int cn = radGridView1.RowCount;
            txtRowCount.Text = cn.ToString();
        }

        async Task LoadData()
        {
            BackgroundWorker bg = new BackgroundWorker();
            JOEquipReleasalRepo = await Task.Run(() => new EquipmentReleasalRepository());
            bg.DoWork += (s, e) =>
            {                
                if (CRUDEMode == clsEnums.CRUDEMode.Add)
                {
                    if (Program.CurrentUser.UserLevelId != 5)
                    {
                        ListOfEquipment = JOEquipReleasalRepo.SearchBy(" AND List.BranchId = " + Program.CurrentUser.BranchId + " Order by EquipmentCode asc ");
                    }
                    else
                    {
                        ListOfEquipment = JOEquipReleasalRepo.SearchBy(" Order by EquipmentCode asc ");
                    }
                }
                if (CRUDEMode == clsEnums.CRUDEMode.Edit)
                {
                    ListOfEquipment = JOEquipReleasalRepo.SearchBy(JOEquipmentReleasal.Id);
                }
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsEquipmentBindingSource.DataSource = ListOfEquipment;
                DisplayJobDetails();
            };           
            bg.RunWorkerAsync();
            int list = radGridView1.RowCount;
            int jo = radGridView2.RowCount;
            txtRowCount.Text = @"List of Equipment: " + list.ToString() + @" | Job Order: " + jo.ToString();
        }

        async Task LoadDataAsync()
        {
            JOEquipmentReleasal.UserId = Program.CurrentUser.Id;
            LoadingManager.ShowLoadScreen();
            Task t = Task.WhenAll(LoadListOfEquipment());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {

                Task t1 = Task.WhenAll(LoadEquipmentDataSource());
                await t1;
                if (t1.Status == TaskStatus.RanToCompletion)
                {
                    DisplayJobDetails();
                    LoadingManager.CloseLoadScreen();
                }
                else
                {
                    LoadingManager.CloseLoadScreen();
                    RadMessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
                
                int list = radGridView1.RowCount;
                int jo = radGridView2.RowCount;
                txtRowCount.Text = @"List of Equipment: " + list.ToString() + @" | Job Order: " + jo.ToString();
            }
            else
            {
                LoadingManager.CloseLoadScreen();
                RadMessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        async Task LoadListOfEquipment()
        {
            JOEquipReleasalRepo = new EquipmentReleasalRepository();
            if (CRUDEMode == clsEnums.CRUDEMode.Add)
            {
                #region Previous Code
                //if (Program.CurrentUser.UserLevelId != 5)
                //{
                //    ListOfEquipment = await Task.Run(() => JOEquipReleasalRepo.SearchBy(" AND List.BranchId = " + Program.CurrentUser.BranchId + " Order by EquipmentCode asc "));
                //}
                //else
                //{
                //    ListOfEquipment = await Task.Run(() => JOEquipReleasalRepo.SearchBy(" ORDER BY EquipmentCode ASC "));
                //}
                #endregion

                ListOfEquipment = await Task.Run(() => JOEquipReleasalRepo.SearchBy(" ORDER BY EquipmentCode ASC "));
            }
            if (CRUDEMode == clsEnums.CRUDEMode.Edit)
            {
                ListOfEquipment = await Task.Run(() => JOEquipReleasalRepo.SearchBy(JOEquipmentReleasal.Id));
            }
        }

        async Task LoadEquipmentDataSource()
        {
            clsEquipmentBindingSource.DataSource = await Task.Run(() =>ListOfEquipment);
        }

        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (clsEquipmentBindingSource.Current != null)
            {
                LoadJobDetails();
            }
        }

        void LoadJobDetails() //void loadJobDetails()
        {
            BackgroundWorker bg = new BackgroundWorker();
            JORepo = new JORepository();
            bg.DoWork += (s, e) =>
            {
                if (CRUDEMode == clsEnums.CRUDEMode.Add)
                {
                    if (Program.CurrentUser.UserLevelId != 5)
                    {
                        ListOfJobOrder = JORepo.SearchBy(" WHERE (JO.Status = " + (Int64)clsEnums.JOROStatus.Completed + " OR JO.Status = " + (Int64)clsEnums.JOROStatus.Sustained + " OR JO.Status = " + (Int64)clsEnums.JOROStatus.PartiallyCompleted + +(Int64)clsEnums.JOROStatus.Cancelled + ") AND (JO.EquipmentId = " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId + " AND JO.ItemType = " + (Int64)((clsEquipment)clsEquipmentBindingSource.Current).EquipmentType + ") AND JO.BranchId =" + Program.CurrentUser.BranchId);
                    }
                    else
                    {
                        ListOfJobOrder = JORepo.SearchBy(" WHERE (JO.Status = " + (Int64)clsEnums.JOROStatus.Completed + " OR JO.Status = " + (Int64)clsEnums.JOROStatus.Sustained + " OR JO.Status = " + (Int64)clsEnums.JOROStatus.PartiallyCompleted + +(Int64)clsEnums.JOROStatus.Cancelled + ") AND (JO.EquipmentId = " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId + " AND JO.ItemType = " + (Int64)((clsEquipment)clsEquipmentBindingSource.Current).EquipmentType + ")");
                    }
                }
                if (CRUDEMode == clsEnums.CRUDEMode.Edit)
                {
                    if (Program.CurrentUser.UserLevelId != 5)
                    {
                        ListOfJobOrder = JORepo.SearchBy(" WHERE JO.EquipmentId = " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId + " AND JO.ItemType = " + (Int64)((clsEquipment)clsEquipmentBindingSource.Current).EquipmentType + " AND JO.BranchId = " + Program.CurrentUser.BranchId);
                    }
                    else
                    {
                        ListOfJobOrder = JORepo.SearchBy(" WHERE JO.EquipmentId = " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId + " AND JO.ItemType = " + (Int64)((clsEquipment)clsEquipmentBindingSource.Current).EquipmentType );
                    }
                }
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsJobOrderBindingSource.DataSource = ListOfJobOrder;

                int List = radGridView1.RowCount;
                int JO = radGridView2.RowCount;
                txtRowCount.Text = "List of Equipment: " + List.ToString() + " | List of Job Order: " + JO.ToString();
            };
            bg.RunWorkerAsync();
        }       
        private void radGridView1_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            JOReleasal();
            radGridView1.EndEdit();
        }
        void DisplayJobDetails()
        {
            if (JOEquipmentReleasal.ListOfJOReleased.Count != 0)
            {
                if (radGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < this.radGridView1.ChildRows.Count; i++)
                    {
                        //radGridView1.Rows[i].Cells["chkSelect"].Value = true;
                        radGridView1.Rows[i].Cells["chkSelect"].Value = false;
                    }
                }     
            }
        }
        void JOReleasal()
        {
            Boolean chkSelect = (Boolean)radGridView1.CurrentRow.Cells["chkSelect"].Value;
            if (chkSelect)
            {
                ListOfJobOrder.ForEach(x =>
                {
                    JOEquipmentReleasal.ListOfJOReleased.Add(new clsJOReleased { JOId = x.Id, EquipmentId = x.EquipmentId, Status = x.Status});
                    UnselectedJOReleased.RemoveAll(y=> y.Id == x.Id);
                    ListOfJOTransLogs.Add(MapTransactionLog(x));
                });
            }
            else
            {
                ListOfJobOrder.ForEach(x =>
                {
                    UnselectedJOReleased.Add(new clsJOReleased { JOId = x.Id, EquipmentId = x.EquipmentId ,Status = x.Status });
                    JOEquipmentReleasal.ListOfJOReleased.RemoveAll(y => y.JOId == x.Id);
                    ListOfJOTransLogs.RemoveAll(y => y.Id == x.Id);
                });
            }
        }
        clsJOTransactionLogs MapTransactionLog(clsJobOrder JobOrder)
        {
            clsJOTransactionLogs TransactionLog = new clsJOTransactionLogs();
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
            return TransactionLog;
        }
        void MapProperties()
        {
            JOEquipmentReleasal.BranchId = Program.CurrentUser.BranchId;           
        }
        private void mRelease_Click(object sender, EventArgs e)
        {
            radGridView1.EndEdit();
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    MapProperties();
                    JOReleasalTransactionRepo = new JOReleasalTransactionRepository();
                    if (JOEquipmentReleasal.Id == 0)
                    {
                        JOReleasalTransactionRepo.Add(JOEquipmentReleasal);
                    }
                    else
                    {
                        JOReleasalTransactionRepo.Update(JOEquipmentReleasal);
                    }

                    foreach (var x in JOEquipmentReleasal.ListOfJOReleased)
                    {
                        if (CRUDEMode == clsEnums.CRUDEMode.Add)
                        {
                            ((List<clsEquipment>)clsEquipmentBindingSource.List).ToList().Where(y=> y.EquipmentMasterlistId == x.EquipmentId).ToList().ForEach(y => {
                                if (y.JOROStatus == clsEnums.JOROStatus.Sustained)
                                {
                                    new JORepository().UpdateJOStatus(clsEnums.JOROStatus.ReleaseSustained, x.JOId);
                                }
                                else
                                {
                                    new JORepository().UpdateJOStatus(clsEnums.JOROStatus.Released, x.JOId);
                                }
                            });
                        }                        
                    }
                    foreach (var x in UnselectedJOReleased)
                    {
                        if (CRUDEMode == clsEnums.CRUDEMode.Edit)
                        {
                            ((List<clsEquipment>)clsEquipmentBindingSource.List).ToList().Where(y => y.EquipmentMasterlistId == x.EquipmentId).ToList().ForEach(y =>
                            {
                                if (y.JOROStatus == clsEnums.JOROStatus.ReleaseSustained)
                                {
                                    new JORepository().UpdateJOStatus(clsEnums.JOROStatus.Sustained, x.JOId);
                                }
                                else
                                {
                                    new JORepository().UpdateJOStatus(clsEnums.JOROStatus.Completed, x.JOId);
                                }
                            });
                        }
                    }
                    foreach (var x in ListOfJOTransLogs)
                    {
                        new JOTransactionLogRepository().Add(x);
                    }
                    SavePPEStatusLog();
                    UpdatePPEStatus();
                    mPrint.Enabled = true;
                    //mRelease.Enabled = false; 
                    AddCurrentUser();
                    ts.Complete();
                    //DataEvent.ConfirmData(JOEquipmentReleasal);

                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The equipment has been released."
                    };
                    ts.Dispose();
                    MsgBox.ShowDialog();
                    this.Close();
                    eqJOReleasal();
                    
                    
                }
                catch (Exception)
                {
                    ts.Dispose();
                    throw;
                }
            }
        }
        #region SavePPEStatusLog
        void SavePPEStatusLog()
        {
            PPEStatusLogRepo = new PPEStatusLogRepository();
            PPEStatusLogRepo.Add(PPEStatusLogMapProperties());
        }
        clsPPEStatusLog PPEStatusLogMapProperties()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                PPEStatusLog.EquipmentId = ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId;
                //PPEStatusLog.PPETypeId = ((clsEquipment)clsEquipmentBindingSource.Current).;
                //PPEStatusLog.PPEClassId = ((clsEquipment)clsEquipmentBindingSource.Current).PPEClassId;
                PPEStatusLog.EquipmentStatusId = 1;
                PPEStatusLog.EmployeeId = Program.CurrentUser.MLEmployeeId;
                PPEStatusLog.SystemName = Application.ProductName;
                PPEStatusLog.DateUpdate = System.DateTime.Now;
                PPEStatusLog.TimeUpdate = System.DateTime.Now;
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {

            }
            return PPEStatusLog;
        }
        void UpdatePPEStatus()
        {
            EquipmentURRepo = new EquipmentURRepository();
            EquipmentURRepo.UpdateAsOperational(((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId);
        }
        private void mPrint_Click(object sender, EventArgs e)
        {
            rptEquipmentReleasal report = new rptEquipmentReleasal(JOEquipmentReleasal);
            frmReportViewer ReportViewerPage = new frmReportViewer(report) { IsJO = false };
            ReportViewerPage.Show();
        }
        #endregion
        #region SaveReleasedAuditTrail
        
        clsUsersLog AddMapProperties()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                currUser.Username = Program.CurrentUser.UserName;
                currUser.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
                currUser.EmpName = Program.CurrentUser.FullName;
                currUser.BranchId = Program.CurrentUser.BranchId;
                currUser.UserLevelId = Program.CurrentUser.UserLevelId;
                currUser.ComputerName = System.Environment.MachineName;
                currUser.DateLogin = System.DateTime.Now;
                currUser.TimeLogin = System.DateTime.Now;
                currUser.DateLogout = System.DateTime.Now;
                currUser.TimeLogout = System.DateTime.Now;
                currUser.OnlineUser = false;
                currUser.ActivityType = 9;
                currUser.DayActivity = "Add Release JO " + "(" + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentCode + " - " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentName + ")";
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                currUser.Username = Program.CurrentUser.UserName;
                currUser.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
                currUser.EmpName = Program.CurrentUser.FullName;
                currUser.BranchId = Program.CurrentUser.BranchId;
                currUser.UserLevelId = Program.CurrentUser.UserLevelId;
                currUser.ComputerName = System.Environment.MachineName;
                currUser.DateLogin = System.DateTime.Now;
                currUser.TimeLogin = System.DateTime.Now;
                currUser.DateLogout = System.DateTime.Now;
                currUser.TimeLogout = System.DateTime.Now;
                currUser.OnlineUser = false;
                currUser.ActivityType = 10;
                currUser.DayActivity = "Update Released JO " + "(" + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentCode + " - " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentName + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        #endregion
        #endregion
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            //await LoadData(); 
            await LoadDataAsync();
            radGridView1.ClearSelection();
        }
        private void mnuPrintSummary_Click(object sender, EventArgs e)
        {
            ListOfEquipment = new List<clsEquipment>();
            ListOfEquipment = JOEquipReleasalRepo.PrintSummary();
            frmReportViewer ReportViewerPage = new frmReportViewer();
            var _tariffSummaryReport = new rptReleasalSummaryReport(ListOfEquipment, Program.CurrentUser.FullName, Program.CurrentUser.Position, "EQUIPMENT FOR RELEASAL (JOB ORDER)");
            ReportViewerPage = new frmReportViewer(_tariffSummaryReport);
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.Show();
        }

        private void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            //var current = (clsEquipment)clsEquipmentBindingSource.Current;
            //current.chkSelect = !current.chkSelect;
            //clsEquipmentBindingSource.ResetCurrentItem();
        }

        private async void FrmJOEquipReleasal_Activated(object sender, EventArgs e) //dhaniele
        {
            //JOEquipmentReleasal.UserId = Program.CurrentUser.Id;
            //Task t = Task.WhenAll(LoadData());
            //Task t = Task.WhenAll(LoadDataAsync());
            //await t;
            //if (t.Status == TaskStatus.RanToCompletion)
            //{
            //    int cn = radGridView1.RowCount;
            //    txtRowCount.Text = cn.ToString();
            //}
            //else
            //{
            //    RadMessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            //}
        }

        private void MnuPrintSummary2_Click(object sender, EventArgs e)
        {
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