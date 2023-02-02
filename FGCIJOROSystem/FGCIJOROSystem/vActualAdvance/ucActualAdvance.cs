using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using Telerik.WinControls.UI;
using FGCIJOROSystem.DAL.Repositories.ActualAdvanceRepo;
using FGCIJOROSystem.Domain.ActualAdvance;
using Telerik.WinControls.Data;
using FGCIJOROSystem.Presentation.vLookups;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.WorkAssignment;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using Telerik.WinControls;
using FGCIJOROSystem.Domain.Configurations.Attendance;
using System.Transactions;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Reports.rActualAdvance;
using FGCIJOROSystem.Reports.rAttendance;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Presentation.vLoader;


namespace FGCIJOROSystem.Presentation.vActualAdvance
{
    public partial class ucActualAdvance : UserControl
    {
        #region Properties
        public frmMainWindow MainWindowPage;
        EmployeeActivityRepository EmployeeActivityRepo;
        ActualAdvanceRepository ActualAdvanceRepo;
        ActualAdvanceReferenceRepository ActualAdvanceRefRepo;
        AttendanceStatusRepository AttendanceStatusRepo;
        UsersLogRepository UsersLogRepo;
        BranchRepository BranchRepo;
        clsUsersLog currUser;

        List<clsActualAdvanceReference> ListOfActualReference;
        List<clsActualAdvanceReference> ListOfAdvanceReference;
        List<clsActualAdvanceDetails> ListOfActualAdvance;

        List<clsActualAdvanceReference> RemoveActualReference;
        List<clsActualAdvanceReference> RemoveAdvanceReference;

        List<clsAttendance> ListOfAttendance;

        #endregion

        #region Main
        #region List
        public ucActualAdvance()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            ListOfActualAdvance = new List<clsActualAdvanceDetails>();
            ListOfActualReference = new List<clsActualAdvanceReference>();
            ListOfAdvanceReference = new List<clsActualAdvanceReference>();
            RemoveActualReference = new List<clsActualAdvanceReference>();
            RemoveAdvanceReference = new List<clsActualAdvanceReference>();
            ListOfAttendance = new List<clsAttendance>();

            dgvCurActual.Columns["TimeStarted"].IsVisible = false;
            dgvCurActual.Columns["TimeEnded"].IsVisible = false;
            dgvCurActual.Columns["WorkPercentage"].IsVisible = false;

            dgvCurAdvance.Columns["TimeStarted"].IsVisible = false;
            dgvCurAdvance.Columns["TimeEnded"].IsVisible = false;
            dgvCurAdvance.Columns["WorkPercentage"].IsVisible = false;
            //radCollapsiblePanel2.IsExpanded = false;
            rddActualDate.Value = DateTime.Now;
        }
       
        private async void ucActualAdvance_Load(object sender, EventArgs e)
        {
            #region Previous code
            //LoadingManager.ShowLoadScreen();
            //rddActualDate.Value = DateTime.Now;
            //loadLists();
            //radGridView1.AutoExpandGroups = true;
            //if (rddBranch.Items == null || rddBranch.Items.Count == 0)
            //{
            //    loadBranch();

            //}
            //if (rddAttendanceGroup.Items == null || rddAttendanceGroup.Items.Count == 0)
            //{
            //    loadAttendaneGroup();
            //}
            //loadAMAttendanceStatus();
            //loadPMAttendanceStatus();

            //System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            //ToolTip1.SetToolTip(this.btnRefresh, "Refresh");
            #endregion

            LoadingManager.ShowLoadScreen();
            Task t = Task.WhenAll(LoadDropDownLists(), LoadPersons());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                loadAMAttendanceStatus();
                loadPMAttendanceStatus();
                //loadActual();
                //loadAdvance();
                //loadPrevActual();
                //loadPrevAdvance();
                LoadingManager.CloseLoadScreen();
            }
            else
            {
                RadMessageBox.Show("An Error occurred", "Error", MessageBoxButtons.AbortRetryIgnore, RadMessageIcon.Error);
                LoadingManager.CloseLoadScreen();
            }

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnRefresh, "Refresh");
        }

        private void loadLists()
        {
            EmployeeActivityRepo = new EmployeeActivityRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ListOfActualAdvance = EmployeeActivityRepo.ActualAdvanceList(" WHERE T.BranchId = " + Program.CurrentUser.BranchId.ToString() + " AND T.IsChecklistGroup = 1", rddActualDate.Value);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsActualAdvanceDetailsBindingSource.DataSource = ListOfActualAdvance;
                //DisplayEmployeeAttendanceStatus();
            };
            bg.RunWorkerAsync();
        }

        private void dtActualDate_ValueChanged(object sender, EventArgs e)
        {
            //loadLists();
            //ListOfActualReference.Clear();
            //clsActualAdvanceReferenceBindingSource.Clear();
            //ListOfAdvanceReference.Clear();
            //clsActualAdvanceReferenceBindingSource1.Clear();
        }
        #endregion
        #region Attendance Status
        private void loadAMAttendanceStatus()
        {
            AttendanceStatusRepo = new AttendanceStatusRepository();
            GridViewComboBoxColumn cbAMStatus = (GridViewComboBoxColumn)radGridView1.Columns["cbAMStatus"];

            cbAMStatus.DropDownStyle = RadDropDownStyle.DropDown;
            cbAMStatus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                e.Result = AttendanceStatusRepo.GetAllActiveStatus();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                AMbindingSource.DataSource = (List<clsAttendanceStatus>)e.Result;
            };
            cbAMStatus.DataSource = AMbindingSource;
            cbAMStatus.DisplayMember = "Symbol";
            cbAMStatus.ValueMember = "Id";
            bg.RunWorkerAsync();
        }
        private void loadPMAttendanceStatus()
        {
            AttendanceStatusRepo = new AttendanceStatusRepository();
            GridViewComboBoxColumn cbPMStatus = (GridViewComboBoxColumn)radGridView1.Columns["cbPMStatus"];

            cbPMStatus.DropDownStyle = RadDropDownStyle.DropDown;
            cbPMStatus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                e.Result = AttendanceStatusRepo.GetAllActiveStatus();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                PMbindingSource.DataSource = (List<clsAttendanceStatus>)e.Result;
            };
            cbPMStatus.DataSource = PMbindingSource;
            cbPMStatus.DisplayMember = "Symbol";
            cbPMStatus.ValueMember = "Id";
            bg.RunWorkerAsync();
        }
        private void DisplayAttendanceStatus()
        {
            if (radGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < this.radGridView1.Rows.Count; i++)
                {
                    if ((Int64)radGridView1.Rows[i].Cells["EmployeeId"].Value == ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId)
                    {
                        radGridView1.Rows[i].Cells["cbAMStatus"].Value = ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).AMStatus;
                        radGridView1.Rows[i].Cells["cbPMStatus"].Value = ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).PMStatus;
                    }
                }
            }
        }
        private void DisplayEmployeeAttendanceStatus()
        {
            foreach (var item in ListOfActualAdvance)
            {
                if (radGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < this.radGridView1.Rows.Count; i++)
                    {
                        if ((Int64)radGridView1.Rows[i].Cells["EmployeeId"].Value == item.EmployeeId)
                        {
                            radGridView1.Rows[i].Cells["cbAMStatus"].Value = item.AMStatus;
                            radGridView1.Rows[i].Cells["cbPMStatus"].Value = item.PMStatus;
                        }
                    }
                }
            }
        }
        #endregion
        #region Main Grid
        private void radGridView1_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "cbAMStatus")
            {
                if (AMbindingSource.Current != null)
                {
                    clsActualAdvanceDetails CurActualAdvance = new clsActualAdvanceDetails();
                    CurActualAdvance = (clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current;                    
                    CurActualAdvance.AMStatus = ((clsAttendanceStatus)AMbindingSource.Current).Id;
                    clsActualAdvanceDetailsBindingSource.ResetCurrentItem();                    
                }
            }
            if (e.Column.Name == "cbPMStatus")
            {
                if (PMbindingSource.Current != null)
                {
                    clsActualAdvanceDetails CurActualAdvance = new clsActualAdvanceDetails();
                    CurActualAdvance = (clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current;                   
                    CurActualAdvance.PMStatus = ((clsAttendanceStatus)PMbindingSource.Current).Id;
                    clsActualAdvanceDetailsBindingSource.ResetCurrentItem();                    
                }
            }
            DisplayAttendanceStatus();
        }
        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            LoadingManager.ShowLoadScreen();
            if (clsActualAdvanceDetailsBindingSource.Current != null)
            {
                clsActualAdvanceReferenceBindingSource.Clear();
                clsActualAdvanceReferenceBindingSource1.Clear();
                loadActual();
                loadAdvance();
                loadPrevActual();
                loadPrevAdvance();
            }
            LoadingManager.CloseLoadScreen();
        }
        private void radGridView1_SelectionChanging(object sender, GridViewSelectionCancelEventArgs e)
        {
            if (clsActualAdvanceReferenceBindingSource.List.Count != 0)
            {
                ((List<clsActualAdvanceReference>)clsActualAdvanceReferenceBindingSource.List).ToList().ForEach(x =>
                {
                    if (ListOfActualReference.Where(y => x.DummyId == y.DummyId).Count() == 0)
                    {
                        ListOfActualReference.Add(x);
                    }
                });
            }
            if (clsActualAdvanceReferenceBindingSource1.List.Count != 0)
            {
                ((List<clsActualAdvanceReference>)clsActualAdvanceReferenceBindingSource1.List).ToList().ForEach(x =>
                {
                    if (ListOfAdvanceReference.Where(y => x.DummyId == y.DummyId).Count() == 0)
                    {
                        ListOfAdvanceReference.Add(x);
                    }
                });
            }
        }        
        #endregion        
        #endregion

        #region Current
        #region Actual
        #region Load
        private void loadActual()
        {
            ActualAdvanceRefRepo = new ActualAdvanceReferenceRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ActualAdvanceRefRepo.SearchBy(" WHERE AR.ActualAdvanceId = " + ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).Id + " AND AR.Type = 0 AND AD.EmployeeId = " + ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).ToList().ForEach(x =>
                {
                    if (ListOfActualReference.Where(y => y.Id == x.Id).Count() == 0)
                    {
                        ListOfActualReference.Add(x);
                    }
                });
                if (((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).DateOfUpdate == null)
                {
                    if (ListOfActualReference.ToList().Count() == 0)
                    {
                        if (ListOfActualReference.Where(x => x.EmployeeId == ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).Count() == 0)
                        {
                            List<clsActualAdvanceReference> List = new List<clsActualAdvanceReference>();
                            List = ActualAdvanceRefRepo.SearchBy(" WHERE AD.DateOfUpdate = CONVERT(varchar,'" + rddActualDate.Value.AddDays(-1) + "',101) AND AR.Type = 1 AND AD.EmployeeId = " + ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId);
                            List.ToList().ForEach(x =>
                            {
                                if (ListOfActualReference.Where(y => x.Id == y.Id).Count() == 0)
                                {
                                    x.Id = 0;
                                    x.Type = clsEnums.ActualAdvance.Actual;
                                    ListOfActualReference.Add(x);
                                }
                            });
                            if (List.Count != 0)
                            {
                                clsActualAdvanceDetails ActualAdvance = (clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current;
                                if (ActualAdvance.AMStatus == 0 && ActualAdvance.PMStatus == 0)
                                {
                                    ActualAdvance.AMStatus = 3;
                                    ActualAdvance.PMStatus = 3;
                                }
                                DisplayAttendanceStatus();
                                clsActualAdvanceDetailsBindingSource.ResetCurrentItem();
                                
                            }
                        }
                    }
                }                
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsActualAdvanceReferenceBindingSource.DataSource = ListOfActualReference.Where(x => x.EmployeeId == ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).ToList();
                DisplayAttendanceStatus();
            };
            bg.RunWorkerAsync();
            clsActualAdvanceReferenceBindingSource.ResetBindings(true);
        }     
        #endregion
        #region Add Function
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            frmWorkAssignmentLookup WorkAssignmentLookupPage = new frmWorkAssignmentLookup((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current);
            WorkAssignmentLookupPage.DataEvent.OnDataConfirm += Actual_DataEvent_OnDataConfirm;
            WorkAssignmentLookupPage.ShowDialog();
        }
        void Actual_DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                List<clsActualAdvanceReference> Lists = new List<clsActualAdvanceReference>();
                Lists = ListOfActualReference.Where(item => item.EmployeeId == ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).ToList();
                foreach (var x in (List<clsWorkAssignments>)obj)
                {
                    Lists.Add(new clsActualAdvanceReference
                    {
                        EmployeeId = x.EmployeeId,
                        ReferenceType = x.ReferenceType,
                        ReferenceNo = x.ReferenceNo,
                        RefYear = x.RefYear,
                        EquipmentId = x.EquipmentId,
                        EquipmentName = x.EquipmentName + " " + x.EquipmentCode,
                        Activity = x.WorkDescription,
                        JODetailId = x.JODetailId,
                        Type = clsEnums.ActualAdvance.Actual,
                        DummyId = dgvCurActual.Rows.Count() + 1
                    });                   

                    clsActualAdvanceReferenceBindingSource.DataSource = Lists;
                    clsActualAdvanceReferenceBindingSource.ResetBindings(true);
                    clsActualAdvanceDetails ActualAdvance = (clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current;
                    if (ActualAdvance.AMStatus == 0 && ActualAdvance.PMStatus == 0)
                    {
                        ActualAdvance.AMStatus = 3;
                        ActualAdvance.PMStatus = 3;
                    }
                    clsActualAdvanceDetailsBindingSource.ResetCurrentItem();
                    DisplayAttendanceStatus();                    
                }
            }
        }
        private void MasterTemplate_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            clsActualAdvanceDetails ActualAdvance = (clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current;
            if (ActualAdvance.AMStatus == 0 && ActualAdvance.PMStatus == 0)
            {
                ActualAdvance.AMStatus = 3;
                ActualAdvance.PMStatus = 3;
            }
            clsActualAdvanceDetailsBindingSource.ResetCurrentItem();
            DisplayAttendanceStatus();
        }
        #endregion
        #region Remove Function
        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            ListOfActualReference.RemoveAll(x => x.DummyId == ((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource.Current).DummyId && x.EmployeeId == ((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource.Current).EmployeeId);
            RemoveActualReference.Add((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource.Current);
            clsActualAdvanceReferenceBindingSource.RemoveCurrent();
        }
        #endregion
        #region Add Others | Update
        private void dgvCurActual_DefaultValuesNeeded(object sender, GridViewRowEventArgs e)
        {
            var CurEmpInfo = (clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current;
            e.Row.Cells["DummyId"].Value = dgvCurAdvance.Rows.Count() + 1;
            e.Row.Cells["EmployeeId"].Value = CurEmpInfo.EmployeeId;
            e.Row.Cells["Type"].Value = clsEnums.ActualAdvance.Actual;
        }
        private void dgvCurActual_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            for (int i = 1; i <= dgvCurActual.Rows.Count; i++)
            {
                dgvCurActual.Rows[i - 1].Cells["DummyId"].Value = i;
            }
        }
        #endregion
        #endregion
        #region Advance
        #region Load
        void loadAdvance()
        {
            ActualAdvanceRefRepo = new ActualAdvanceReferenceRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ActualAdvanceRefRepo.SearchBy(" WHERE AR.ActualAdvanceId = " + ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).Id + " AND AR.Type = 1 AND AD.EmployeeId = " + ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).ToList().ForEach(x =>
                {
                    if (ListOfAdvanceReference.Where(y => y.Id == x.Id).Count() == 0)
                    {
                        ListOfAdvanceReference.Add(x);
                    }
                });
                if (((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).DateOfUpdate == null)
                {
                    if (ListOfAdvanceReference.ToList().Count() == 0)
                    {
                        if (ListOfAdvanceReference.Where(x => x.EmployeeId == ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).Count() == 0)
                        {
                            ActualAdvanceRefRepo.SearchBy(" WHERE AD.DateOfUpdate = CONVERT(varchar,'" + rddActualDate.Value.AddDays(-1) + "',101) AND AR.Type = 1 AND AD.EmployeeId = " + ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).ToList().ForEach(x =>
                            {
                                if (ListOfAdvanceReference.Where(y => y.Id == x.Id).Count() == 0)
                                {
                                    x.Id = 0;
                                    x.Type = clsEnums.ActualAdvance.Advance;
                                    ListOfAdvanceReference.Add(x);
                                }
                            });
                        }
                    }
                }
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsActualAdvanceReferenceBindingSource1.DataSource = ListOfAdvanceReference.Where(x => x.EmployeeId == ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).ToList();
            };
            bg.RunWorkerAsync();
            clsActualAdvanceReferenceBindingSource1.ResetBindings(true);
        }

        #endregion
        #region Add Function
        private void radBindingNavigator2AddNewItem_Click(object sender, EventArgs e)
        {
            frmWorkAssignmentLookup WorkAssignmentLookupPage = new frmWorkAssignmentLookup((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current);
            WorkAssignmentLookupPage.DataEvent.OnDataConfirm += Advance_DataEvent_OnDataConfirm;
            WorkAssignmentLookupPage.ShowDialog();
        }
        void Advance_DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                List<clsActualAdvanceReference> Lists = new List<clsActualAdvanceReference>();
                Lists = ListOfAdvanceReference.Where(item => item.EmployeeId == ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).ToList();
                foreach (var x in (List<clsWorkAssignments>)obj)
                {
                    Lists.Add(new clsActualAdvanceReference
                    {
                        EmployeeId = x.EmployeeId,
                        ReferenceType = x.ReferenceType,
                        ReferenceNo = x.ReferenceNo,
                        RefYear = x.RefYear,
                        EquipmentId = x.EquipmentId,
                        EquipmentName = x.EquipmentName + " " + x.EquipmentCode,
                        Activity = x.WorkDescription,
                        Type = clsEnums.ActualAdvance.Advance,
                        JODetailId = x.JODetailId,
                        DummyId = dgvCurAdvance.Rows.Count() + 1
                    });
                    clsActualAdvanceReferenceBindingSource1.DataSource = Lists;
                    clsActualAdvanceReferenceBindingSource1.ResetBindings(true);
                }
            }
        }
        #endregion
        #region Remove Function
        private void radBindingNavigator2DeleteItem_Click(object sender, EventArgs e)
        {
            ListOfAdvanceReference.RemoveAll(x => x.DummyId == ((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource1.Current).DummyId && x.EmployeeId == ((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource1.Current).EmployeeId);
            RemoveAdvanceReference.Add((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource1.Current);
            clsActualAdvanceReferenceBindingSource1.RemoveCurrent();
        }
        #endregion
        #region Add Others| Update
        private void MasterTemplate_DefaultValuesNeeded(object sender, GridViewRowEventArgs e)
        {
            var CurEmpInfo = (clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current;
            e.Row.Cells["DummyId"].Value = dgvCurAdvance.Rows.Count() + 1;
            e.Row.Cells["EmployeeId"].Value = CurEmpInfo.EmployeeId;
            e.Row.Cells["Type"].Value = clsEnums.ActualAdvance.Advance;
        }
        private void dgvCurAdvance_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            for (int i = 1; i <= dgvCurAdvance.Rows.Count; i++)
            {
                dgvCurAdvance.Rows[i - 1].Cells["DummyId"].Value = i;
            }
        }
        #endregion
        #endregion       
        #endregion

        #region Previous
        void loadPrevActual()
        {
            ActualAdvanceRefRepo = new ActualAdvanceReferenceRepository();
            BackgroundWorker bg = new BackgroundWorker();
            List<clsActualAdvanceReference> ListOfPrevActualRef = new List<clsActualAdvanceReference>();
            bg.DoWork += (s, e) =>
            {
                ListOfPrevActualRef = ActualAdvanceRefRepo.SearchBy(" WHERE AD.DateOfUpdate = CONVERT(varchar,'" + rddActualDate.Value.AddDays(-1) + "',101) AND AR.Type = 0 AND AD.EmployeeId = " +((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsActualAdvanceReferenceBindingSource2.DataSource = ListOfPrevActualRef.Where(x => x.EmployeeId == ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).ToList();
            };
            bg.RunWorkerAsync();
            clsActualAdvanceReferenceBindingSource2.ResetBindings(true);
        }
        void loadPrevAdvance()
        {
            ActualAdvanceRefRepo = new ActualAdvanceReferenceRepository();
            BackgroundWorker bg = new BackgroundWorker();
            List<clsActualAdvanceReference> ListOfPrevAdvanceRef = new List<clsActualAdvanceReference>();
            bg.DoWork += (s, e) =>
            {
                ListOfPrevAdvanceRef = ActualAdvanceRefRepo.SearchBy(" WHERE AD.DateOfUpdate = CONVERT(varchar,'" + rddActualDate.Value.AddDays(-1) + "',101) AND AR.Type = 1 AND AD.EmployeeId = " + ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsActualAdvanceReferenceBindingSource3.DataSource = ListOfPrevAdvanceRef.Where(x => x.EmployeeId == ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeId).ToList();
            };
            bg.RunWorkerAsync();
            clsActualAdvanceReferenceBindingSource3.ResetBindings(true);
        }    
        #endregion

        #region Print and Save
        clsActualAdvanceDetails MapProperties()
        {
            clsActualAdvanceDetails ActualAdvanceDetails = (clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current;
            ActualAdvanceDetails.DateOfUpdate = (DateTime)rddActualDate.Value;
            ActualAdvanceDetails.ListOfActualReference = (List<clsActualAdvanceReference>)clsActualAdvanceReferenceBindingSource.List;
            ActualAdvanceDetails.ListOfAdvanceReference = (List<clsActualAdvanceReference>)clsActualAdvanceReferenceBindingSource1.List;
            return ActualAdvanceDetails;
        }
        private async void MasterTemplate_CommandCellClick_1(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    ActualAdvanceRepo = new ActualAdvanceRepository();
                    var ActualAdvanceDetail = (clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current;
                    if (ActualAdvanceDetail.Id == 0)
                    {
                        ActualAdvanceRepo.Add(MapProperties());
                        ListOfActualReference.RemoveAll(x => x.EmployeeId == MapProperties().EmployeeId);
                        ListOfAdvanceReference.RemoveAll(x => x.EmployeeId == MapProperties().EmployeeId);
                    }
                    else
                    {
                        ActualAdvanceRepo.Update(MapProperties());
                        ListOfActualReference.RemoveAll(x => x.EmployeeId == MapProperties().EmployeeId);
                        ListOfAdvanceReference.RemoveAll(x => x.EmployeeId == MapProperties().EmployeeId);
                    }
                    foreach (var item in RemoveActualReference)
                    { 
                        if (item.Id != 0)
                        {
                            new ActualAdvanceReferenceRepository().Delete(item);
                        }
                        
                    }
                    foreach (var item in RemoveAdvanceReference)
                    {
                        if (item.Id != 0)
                        {
                            new ActualAdvanceReferenceRepository().Delete(item);
                        }                       
                    }
                    //AddCurrentUser();

                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been saved successfully"
                    };
                    MsgBox.ShowDialog();

                    //loadLists();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    LoadingManager.CloseLoadScreen();
                    ts.Dispose();
                    throw new Exception(ex.Message);
                }
            }

            await SaveActivity();
        }

        private async Task SaveActivity()
        {
            LoadingManager.ShowLoadScreen();

            Task t = Task.WhenAll(SearchOptionsAsync());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
            {
                loadActual();
                loadAdvance();
                LoadingManager.CloseLoadScreen();
            }

        }
        private void mPrint_Click(object sender, EventArgs e)
        {
            ActivityReportRepository ActivityReportRepo = new ActivityReportRepository();
            rptActualAdvance report = new rptActualAdvance(ActivityReportRepo.GetListACD(rddActualDate.Value), rddActualDate.Value);
            var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            frmReportViewer ReportViewerPage = new frmReportViewer();                      
            ReportViewerPage.reportViewer1.ReportSource = reportSource;
            ReportViewerPage.reportViewer1.RefreshReport();
            ReportViewerPage.ShowDialog();
        }  
        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            frmSelectActualAdvanceAttendanceDate _attendance = new frmSelectActualAdvanceAttendanceDate();
            _attendance.ShowDialog();
        }
        #endregion

        private void cbBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            //loadSections();
        }
        private void cbAttendanceGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            loadSections();
        }

        #region Load Attendance Group OLD
        async void loadAttendaneGroup()
        {
            clsAttendanceGroupBindingSource.Clear();
            AttendanceGroupRepository AttendanceGroupRepo = new AttendanceGroupRepository();
            clsAttendanceGroupBindingSource.DataSource = await Task.Run(() => AttendanceGroupRepo.SearchBy(" WHERE IsActive = 1 ")); //async
        }
        #endregion

        void loadSections()
        {
            if (rddAttendanceGroup.Items != null && rddAttendanceGroup.Items.Count != 0)
            {
                clsSectionJOROBindingSource.Clear();
                SectionJORORepository SectionRepo = new SectionJORORepository();
                clsSectionJOROBindingSource.DataSource = SectionRepo.SearchBy(" WHERE B.Id = '" + rddBranch.SelectedValue.ToString() + "' AND s.AttendanceGroupId = '" + rddAttendanceGroup.SelectedValue.ToString() + "'");
            }            
        }

        #region Load Branch Old
        //void loadBranch()
        //{
        //    BranchRepository BranchRepo = new BranchRepository();
        //    clsBranchBindingSource.DataSource = BranchRepo.GetAllActive();
        //    //radCollapsiblePanel2.IsExpanded = false;
        //}
        #endregion

        private async void tbSearch_Click(object sender, EventArgs e)
        {
            //SearchOptions();
            await SearchOptionsAsync();
        }

        #region Search Options
        void SearchOptions()
        {
            EmployeeActivityRepo = new EmployeeActivityRepository();
            BackgroundWorker bg = new BackgroundWorker();
            String SectionIds = "0";
            foreach (var item in lcSection.CheckedItems)
            {
                SectionIds += "," + item.Value;
            }
            bg.DoWork += (s, e) =>
            {
                if (cbSection.IsChecked && lcSection.CheckedItems.Count != 0)
                {
                    ListOfActualAdvance = EmployeeActivityRepo.ActualAdvanceList(" WHERE T.SectionId IN(" + SectionIds + ") AND T.BranchId = " + rddBranch.SelectedValue.ToString(), rddActualDate.Value);
                }
                else
                {
                    ListOfActualAdvance = EmployeeActivityRepo.ActualAdvanceList(" WHERE T.AttendanceGroupId = " + rddAttendanceGroup.SelectedValue.ToString() + " AND T.BranchId = " + rddBranch.SelectedValue.ToString(), rddActualDate.Value);
                }
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsActualAdvanceDetailsBindingSource.DataSource = ListOfActualAdvance;
                //DisplayEmployeeAttendanceStatus();
            };
            bg.RunWorkerAsync();
        }
        #endregion

        #region Search Options Async

        private async Task SearchOptionsAsync()
        {
            LoadingManager.ShowLoadScreen();
            EmployeeActivityRepo = new EmployeeActivityRepository();
            String SectionIds = "0";
            foreach (var item in lcSection.CheckedItems)
            {
                SectionIds += "," + item.Value;
            }
            if (cbSection.IsChecked && lcSection.CheckedItems.Count != 0)
            {
                Task t = Task.WhenAll(SearchSection(SectionIds));
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                    DisplayAttendanceStatus();
            }
            else
            {
                Task t = Task.WhenAll(SearchAttendanceGroup(SectionIds));
                await t;
                if (t.Status == TaskStatus.RanToCompletion)
                    DisplayAttendanceStatus();
            }
            LoadingManager.CloseLoadScreen();
        }

        private async Task SearchSection(string SectionIds)
        {
            ListOfActualAdvance = await Task.Run(() => EmployeeActivityRepo.ActualAdvanceList(" WHERE T.SectionId IN(" + SectionIds + ") AND T.BranchId = " + rddBranch.SelectedValue.ToString(), rddActualDate.Value));
            clsActualAdvanceDetailsBindingSource.DataSource = await Task.Run(() => ListOfActualAdvance);
            loadActual();
        }

        private async Task SearchAttendanceGroup(string SectionIds)
        {
            ListOfActualAdvance = await Task.Run(() => EmployeeActivityRepo.ActualAdvanceList(" WHERE T.AttendanceGroupId = " + rddAttendanceGroup.SelectedValue.ToString() + " AND T.BranchId = " + rddBranch.SelectedValue.ToString(), rddActualDate.Value));
            clsActualAdvanceDetailsBindingSource.DataSource = await Task.Run(() => ListOfActualAdvance);
            loadActual();
        }
        #endregion


        #region CellFormatting
        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSave")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Save Record");
            }
        }
        #endregion

        #region AuditTrail
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        clsUsersLog AddMapProperties()
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
            currUser.ActivityType = 8;
            currUser.DayActivity = "Update Actual Advance: Actual ( Employee: " + ((clsActualAdvanceDetails)clsActualAdvanceDetailsBindingSource.Current).EmployeeName + " - Ref:" + ((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource.Current).ReferenceNoStr
                                                             + " - Equip.: " + ((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource.Current).EquipmentName + " -Activity: " + ((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource.Current).Activity
                                                             + " -Remarks: " + ((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource.Current).Remarks + " - Work %: " + ((clsActualAdvanceReference)clsActualAdvanceReferenceBindingSource.Current).WorkPercentage + ")";
            return currUser;
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadLists();
            loadAMAttendanceStatus();
            loadPMAttendanceStatus();
        }
        #endregion

        private async Task LoadDropDownLists()
        {
            Task t = Task.WhenAll(LoadBranch(), LoadAttendanceGroup());
            await t;
            if (t.Status == TaskStatus.Faulted)
            {
                RadMessageBox.Show("Error loading lists, Please check connection", "Error", MessageBoxButtons.AbortRetryIgnore, RadMessageIcon.Error);
            }
        }

        private async Task LoadBranch()
        {
            BranchRepo = new BranchRepository();
            var branchList = await Task.Run(() => BranchRepo.GetAll());
            rddBranch.DataSource = branchList;
        }

        private async Task LoadAttendanceGroup()
        {
            AttendanceGroupRepository AttendanceGroupRepo = new AttendanceGroupRepository();
            clsAttendanceGroupBindingSource.DataSource = await Task.Run(() => AttendanceGroupRepo.SearchBy(" WHERE IsActive = 1 "));
        }
        private async Task LoadPersons()
        {
            EmployeeActivityRepo = new EmployeeActivityRepository();
            Task t = Task.WhenAll(LoadActualAdvancedPersons());
            await t;
            if (t.Status == TaskStatus.RanToCompletion)
                clsActualAdvanceDetailsBindingSource.DataSource = ListOfActualAdvance;
            //DisplayEmployeeAttendanceStatus();
        }

        private async Task LoadActualAdvancedPersons()
        {
            ListOfActualAdvance = await Task.Run(() => EmployeeActivityRepo.ActualAdvanceList(" WHERE T.BranchId = " + Program.CurrentUser.BranchId.ToString() + " AND T.IsChecklistGroup = 1", rddActualDate.Value));
        }

        #region Toggle State Change

        private void cbActualName_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (cbActualDate.IsChecked)
            {
                rddActualDate.Enabled = true;

           
                cbBranch.IsChecked = false;
                cbAttendanceGroup.IsChecked = false;
                cbSection.IsChecked = false;

                rddBranch.Enabled = false;
                rddAttendanceGroup.Enabled = false;
                lcSection.Enabled = false;
            }
        }

        private void cbBranch_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (cbBranch.IsChecked)
            {
                rddBranch.Enabled = true;

                
                cbActualDate.IsChecked = false;
                cbAttendanceGroup.IsChecked = false;
                cbSection.IsChecked = false;

                rddActualDate.Enabled = false;
                rddAttendanceGroup.Enabled = false;
                lcSection.Enabled = false;
            }
        }

        private void cbAttendanceGroup_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (cbAttendanceGroup.IsChecked)
            {
                rddAttendanceGroup.Enabled = true;

                cbActualDate.IsChecked = false;
                cbBranch.IsChecked = false;
                cbSection.IsChecked = false;

                rddActualDate.Enabled = false;
                rddBranch.Enabled = false;
                lcSection.Enabled = false;
            }
        }

        private void cbSection_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (cbSection.IsChecked)
            {
                lcSection.Enabled = true;

                cbActualDate.IsChecked = false;
                cbBranch.IsChecked = false;
                cbAttendanceGroup.IsChecked = false;

                rddActualDate.Enabled = false;
                rddBranch.Enabled = false;
                rddAttendanceGroup.Enabled = false;
            }
            else
                lcSection.UncheckAllItems();
        }

        #endregion

        private void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {

        }

        
    }
}
