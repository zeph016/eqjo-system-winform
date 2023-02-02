using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo;
using FGCIJOROSystem.DAL.Repositories.RORepo;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.EquipmentReleasal;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.RescueOrder;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Reports.rEquipmentReleasal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.DAL.Repositories.EquipmentURRepo;
using FGCIJOROSystem.DAL.Repositories.PPEStatusLogRepo;
using FGCIJOROSystem.Domain.PPEStatusLog;
using FGCIJOROSystem.Presentation.vLoader;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vRescueOrder
{
    public partial class frmROEquipReleasal : Telerik.WinControls.UI.RadForm
    {  
        #region Properites
        List<clsRescueOrder> ListOfRescueOrder;
        List<clsROTransactionLogs> ListOfROTransLogs;
        List<clsEquipment> ListOfEquipment;
        clsROReleasalTransaction ROEquipmentReleasal;

        List<clsROReleased> UnselectedROReleased;
        RORepository RORepo;
        EquipmentReleasalRepository ROEquipReleasalRepo;
        ROReleasalTransactionRepository ROReleasalTransactionRepo;
        EquipmentURRepository EquipmentURRepo;
        PPEStatusLogRepository PPEStatusLogRepo;

        clsPPEStatusLog PPEStatusLog;
        clsEnums.CRUDEMode CRUDEMode;
        public clsDataEvent DataEvent;

        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion

        #region Methods
        public frmROEquipReleasal()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            ROEquipmentReleasal = new clsROReleasalTransaction();
            ListOfROTransLogs = new List<clsROTransactionLogs>();
            UnselectedROReleased = new List<clsROReleased>();
            CRUDEMode = clsEnums.CRUDEMode.Add;
            currUser = new clsUsersLog();
            PPEStatusLog = new clsPPEStatusLog();
        }
        public frmROEquipReleasal(clsROReleasalTransaction obj)
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            ListOfROTransLogs = new List<clsROTransactionLogs>();
            UnselectedROReleased = new List<clsROReleased>();
            ROEquipmentReleasal = obj;
            CRUDEMode = clsEnums.CRUDEMode.Edit;
            currUser = new clsUsersLog();
            PPEStatusLog = new clsPPEStatusLog();
        }

        private void eqROReleasal()
        {
            this.Dispose();
            frmROEquipReleasal frmrRefreshForm = new frmROEquipReleasal();
            frmrRefreshForm.ShowDialog();
        }
        private async void frmROEquipReleasal_Load(object sender, EventArgs e)
        {
            //ROEquipmentReleasal.UserId = Program.CurrentUser.Id;
            //load();

            await LoadDataAsync();
        }


        void load()
        {
            BackgroundWorker bg = new BackgroundWorker();
            ROEquipReleasalRepo = new EquipmentReleasalRepository();
            bg.DoWork += (s, e) =>
            {
                if (CRUDEMode == clsEnums.CRUDEMode.Add)
                {
                    if (Program.CurrentUser.UserLevelId != 5)
                    {
                        ListOfEquipment = ROEquipReleasalRepo.SearchROBy(" AND LIST.BranchId = " + Program.CurrentUser.BranchId + " ORDER BY EquipmentCode ASC");
                    }
                    else
                    {
                        ListOfEquipment = ROEquipReleasalRepo.SearchROBy(" ORDER BY EquipmentCode ASC");
                    }

                }
                if (CRUDEMode == clsEnums.CRUDEMode.Edit)
                {
                    ListOfEquipment = ROEquipReleasalRepo.SearchROBy(ROEquipmentReleasal.Id);
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
            txtRowCount.Text = @"List of Equipment: " + list.ToString() + @" | List of Rescue Order: " + jo.ToString();
        }

        async Task LoadDataAsync()
        {
            ROEquipmentReleasal.UserId = Program.CurrentUser.Id;
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
            }
            else
            {
                LoadingManager.CloseLoadScreen();
                RadMessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }

            int list = radGridView1.RowCount;
            int jo = radGridView2.RowCount;
            txtRowCount.Text = @"List of Equipment: " + list.ToString() + @" | List of Rescue Order: " + jo.ToString();
        }

        async Task LoadListOfEquipment()
        {
            ROEquipReleasalRepo = new EquipmentReleasalRepository();

            if (CRUDEMode == clsEnums.CRUDEMode.Add)
            {
                #region Previous Code
                //if (Program.CurrentUser.UserLevelId != 5)
                //{
                //    ListOfEquipment = await Task.Run(() => ROEquipReleasalRepo.SearchROBy(" AND LIST.BranchId = " + Program.CurrentUser.BranchId + " ORDER BY EquipmentCode ASC"));
                //}
                //else
                //{
                //    ListOfEquipment = await Task.Run(() => ROEquipReleasalRepo.SearchROBy(" ORDER BY EquipmentCode ASC"));
                //}
                #endregion

                ListOfEquipment = await Task.Run(() => ROEquipReleasalRepo.SearchROBy(" ORDER BY EquipmentCode ASC"));
            }
            if (CRUDEMode == clsEnums.CRUDEMode.Edit)
            {
                ListOfEquipment = await Task.Run(() => ROEquipReleasalRepo.SearchROBy(ROEquipmentReleasal.Id));
            }

        }

        async Task LoadEquipmentDataSource()
        {
            clsEquipmentBindingSource.DataSource = await Task.Run(() => ListOfEquipment);
        }
        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            loadJobDetails();
        }
        void loadJobDetails()
        {
            BackgroundWorker bg = new BackgroundWorker();
            RORepo = new RORepository();
            bg.DoWork += (s, e) =>
            {
                if (CRUDEMode == clsEnums.CRUDEMode.Add)
                {
                    if (Program.CurrentUser.UserLevelId != 5)
                    {
                        ListOfRescueOrder = RORepo.SearchBy(" WHERE (RO.Status = " + (Int64)clsEnums.JOROStatus.Completed + " OR RO.Status = " + (Int64)clsEnums.JOROStatus.Sustained + ") AND ( RO.EquipmentId = " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId + " AND RO.ItemType = " + (Int64)((clsEquipment)clsEquipmentBindingSource.Current).EquipmentType + ") AND RO.BranchId =" + Program.CurrentUser.BranchId);
                    }
                    else
                    {
                        ListOfRescueOrder = RORepo.SearchBy(" WHERE (RO.Status = " + (Int64)clsEnums.JOROStatus.Completed + " OR RO.Status = " + (Int64)clsEnums.JOROStatus.Sustained + ") AND ( RO.EquipmentId = " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId + " AND RO.ItemType = " + (Int64)((clsEquipment)clsEquipmentBindingSource.Current).EquipmentType + ")");
                    }
                }
                if (CRUDEMode == clsEnums.CRUDEMode.Edit)
                {
                    if (Program.CurrentUser.UserLevelId != 5)
                    {
                        ListOfRescueOrder = RORepo.SearchBy(" WHERE RO.EquipmentId = " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId + " AND RO.ItemType = " + (Int64)((clsEquipment)clsEquipmentBindingSource.Current).EquipmentType + " AND RO.BranchId =" + Program.CurrentUser.BranchId);
                    }
                    else
                    {
                        ListOfRescueOrder = RORepo.SearchBy(" WHERE RO.EquipmentId = " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId + " AND RO.ItemType = " + (Int64)((clsEquipment)clsEquipmentBindingSource.Current).EquipmentType );
                    }
                }
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsRescueOrderBindingSource.DataSource = ListOfRescueOrder;

                int List = radGridView1.RowCount;
                int JO = radGridView2.RowCount;
                txtRowCount.Text = @"List of Equipment: " + List.ToString() + @" | List of Rescue Order: " + JO.ToString();
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
            if (ROEquipmentReleasal.ListOfROReleased.Count != 0)
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
                ListOfRescueOrder.ForEach(x =>
                {
                        ROEquipmentReleasal.ListOfROReleased.Add(new clsROReleased { ROId = x.Id, EquipmentId = x.EquipmentId, Status = x.Status });
                        UnselectedROReleased.RemoveAll(y => y.Id == x.Id);
                        ListOfROTransLogs.Add(MapTransactionLog(x));
                });
            }
            else
            {
                ListOfRescueOrder.ForEach(x =>
                {
                        UnselectedROReleased.Add(new clsROReleased { ROId = x.Id, EquipmentId = x.EquipmentId, Status = x.Status });
                        ROEquipmentReleasal.ListOfROReleased.RemoveAll(y => y.ROId == x.Id);
                        ListOfROTransLogs.RemoveAll(y => y.Id == x.Id);
                });
            }
        }
        clsROTransactionLogs MapTransactionLog(clsRescueOrder JobOrder)
        {
            clsROTransactionLogs TransactionLog = new clsROTransactionLogs();
            TransactionLog.ROId = JobOrder.Id;
            TransactionLog.RONo = JobOrder.RONo;
            TransactionLog.Type = JobOrder.Type;
            TransactionLog.RODate = JobOrder.RODate;
            //TransactionLog.EquipmentOnBranch = JobOrder.EquipmentOnBranch;
            //TransactionLog.PartsRequest = JobOrder.PartsRequest;
            TransactionLog.CustomerType = JobOrder.CustomerType;
            TransactionLog.CustomerId = JobOrder.CustomerId;
            TransactionLog.BranchId = JobOrder.BranchId;
            TransactionLog.ItemType = JobOrder.ItemType;
            TransactionLog.EquipmentId = JobOrder.EquipmentId;
            TransactionLog.ContractorId = JobOrder.ContractorId;
            TransactionLog.DriverId = JobOrder.DriverId;
            TransactionLog.UserId = Program.CurrentUser.Id;
            return TransactionLog;
        }
        void MapProperties()
        {
            ROEquipmentReleasal.BranchId = Program.CurrentUser.BranchId;
        }
        private void mRelease_Click(object sender, EventArgs e)
        {
            radGridView1.EndEdit();
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                        MapProperties();
                        ROReleasalTransactionRepo = new ROReleasalTransactionRepository();
                        if (ROEquipmentReleasal.Id == 0)
                        {
                            ROReleasalTransactionRepo.Add(ROEquipmentReleasal);
                        }
                        else
                        {
                            ROReleasalTransactionRepo.Update(ROEquipmentReleasal);
                        }
                        foreach (var x in ROEquipmentReleasal.ListOfROReleased)
                        {
                            if (CRUDEMode == clsEnums.CRUDEMode.Add)
                            {
                                ((List<clsEquipment>)clsEquipmentBindingSource.List).ToList().Where(y => y.EquipmentMasterlistId == x.EquipmentId).ToList().ForEach(y =>
                                {
                                    if (y.JOROStatus == clsEnums.JOROStatus.Sustained)
                                    {
                                        new RORepository().UpdateROStatus(clsEnums.JOROStatus.ReleaseSustained, x.ROId);
                                    }
                                    else
                                    {
                                        new RORepository().UpdateROStatus(clsEnums.JOROStatus.Released, x.ROId);
                                    }
                                });
                            }
                        }
                        foreach (var x in UnselectedROReleased)
                        {
                            if (CRUDEMode == clsEnums.CRUDEMode.Edit)
                            {
                                ((List<clsEquipment>)clsEquipmentBindingSource.List).ToList().Where(y => y.EquipmentMasterlistId == x.EquipmentId).ToList().ForEach(y =>
                                {
                                    if (y.JOROStatus == clsEnums.JOROStatus.ReleaseSustained)
                                    {
                                        new RORepository().UpdateROStatus(clsEnums.JOROStatus.Sustained, x.ROId);
                                    }
                                    else
                                    {
                                        new RORepository().UpdateROStatus(clsEnums.JOROStatus.Completed, x.ROId);
                                    }
                                });
                            }
                        }
                        foreach (var x in ListOfROTransLogs)
                        {
                            new ROTransactionLogRepository().Add(x);
                        }
                        //DataEvent.ConfirmData(ROEquipmentReleasal);
                        SavePPEStatusLog();
                        UpdatePPEStatus();
                        mPrint.Enabled = true;
                        //mRelease.Enabled = false;
                        AddCurrentUser();
                        ts.Complete();

                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Success,
                            Message = "The equipment has been released."
                        };
                        MsgBox.ShowDialog();
                    ts.Dispose();
                    //load();
                    this.Close();
                    eqROReleasal();
                    
                }
                catch (Exception)
                {
                    ts.Dispose();
                    throw;
                }
            }         
        }
        #endregion
        void UpdatePPEStatus()
        {
            EquipmentURRepo = new EquipmentURRepository();
            EquipmentURRepo.UpdateAsOperational(((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId);
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
        #endregion
        private void mPrint_Click(object sender, EventArgs e)
        {
            rptEquipmentReleasal report = new rptEquipmentReleasal(ROEquipmentReleasal);
            frmReportViewer ReportViewerPage = new frmReportViewer(report) { IsRO = false };
            ReportViewerPage.Show();
        }
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
                currUser.DayActivity = "Add Released RO " + "(" + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentCode + " - " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentName + ")";
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
                currUser.DayActivity = "Update Release RO " + "(" + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentCode + " - " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentName + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        #endregion

        private void mnuPrintSummary_Click(object sender, EventArgs e)
        {
            ListOfEquipment = new List<clsEquipment>();
            ListOfEquipment = ROEquipReleasalRepo.PrintSummaryRO();
            frmReportViewer ReportViewerPage = new frmReportViewer();
            var tariffSummaryReport = new rptReleasalSummaryReport(ListOfEquipment, Program.CurrentUser.FullName, Program.CurrentUser.Position, "EQUIPMENT FOR RELEASAL (RESCUE ORDER)");
            ReportViewerPage = new frmReportViewer(tariffSummaryReport);
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.Show();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            //load();

            await LoadDataAsync();

            radGridView1.ClearSelection();
        }

        private void radGridView1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //var current = (clsEquipment)clsEquipmentBindingSource.Current;
            //current.chkSelect = !current.chkSelect;
            //clsEquipmentBindingSource.ResetCurrentItem();
        }

        private void FrmROEquipReleasal_Activated(object sender, EventArgs e)
        {
            //load();
        }

        private void MnuPrintSummary2_Click(object sender, EventArgs e)
        {
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
