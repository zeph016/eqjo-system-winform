using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Presentation.vLookups;
using FGCIJOROSystem.Domain.Configurations.Contractors;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Tariff;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.Configurations.Customers;
using System.Transactions;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Reports.rJobOrder;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Presentation.vConfiguration.vTariff;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.DAL.Repositories.EquipmentURRepo;
using FGCIJOROSystem.Domain.EquipmentUR;
using FGCIJOROSystem.DAL.Repositories.PPEStatusLogRepo;
using FGCIJOROSystem.Domain.PPEStatusLog;
using FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo;
using FGCIJOROSystem.Domain.ActualAdvance;

namespace FGCIJOROSystem.Presentation.vJobOrder
{
    public partial class ucJOEntry : UserControl
    {
        #region Properties
        EmployeeRepository EmployeeRepo;
        //SectionJORORepository SectionRepo;
        TariffRepository TariffRepo;
        //JobCategoryRepository JobCategoryRepo;
        //JobTypeRepository JobTypeRepo;
        EquipmentURRepository EquipmentURRepo;
        PPEStatusLogRepository PPEStatusLogRepo;
        JORepository JORepo;
        UsersLogRepository UsersLogRepo;
        //EquipmentReleasalRepository EquipmentReleasalRepo;
        clsUsersLog UsersLog;
        clsPPEStatusLog PPEStatusLog;
        clsJobOrder JobOrder;
        clsJOTransactionLogs TransactionLog;
        clsEquipmentUR EquipmentUR;
        List<clsActivityReport> ListOfActivity;
        List<clsActivityReport> uniqueListActivity;
        //clsStatus Status;
        clsEnums.CRUDEMode CRUDEMode;
        Action SaveAction;
        public clsDataEvent DataEvent;
        clsEnums.JOROStatus JOROStatus = new clsEnums.JOROStatus();

        long equipmentId;
        #endregion
        #region Methods
        public ucJOEntry()
        {
            InitializeComponent();
            JobOrder = new clsJobOrder();
            TransactionLog = new clsJOTransactionLogs();
            CRUDEMode = clsEnums.CRUDEMode.Add;
            DataEvent = new clsDataEvent();
            UsersLog = new clsUsersLog();
            PPEStatusLog = new clsPPEStatusLog();
            EquipmentUR = new clsEquipmentUR();
        }
        public ucJOEntry(clsJobOrder obj, List<clsActivityReport> activityList)
        {
            InitializeComponent();
            JobOrder = obj;
            ListOfActivity = activityList;
            TransactionLog = new clsJOTransactionLogs();
            CRUDEMode = clsEnums.CRUDEMode.Edit;
            DataEvent = new clsDataEvent();
            UsersLog = new clsUsersLog();
            PPEStatusLog = new clsPPEStatusLog();
            EquipmentUR = new clsEquipmentUR();
        }
        #region Load
        private void ucJOEntry_Load(object sender, EventArgs e)
        {
            HideControlsFromEncoders();
            if (CRUDEMode == clsEnums.CRUDEMode.Add)
            {
                SaveAction = Add;
            }
            if (CRUDEMode == clsEnums.CRUDEMode.Edit)
            {
                SaveAction = Edit;
                DisplayProperties();
                btnSaveApprove.Enabled = false;
                if (Program.CurrentUser.UserLevelId == 5)
                {
                    btnPrint.Enabled = true;
                }
                else
                {
                    btnPrint.Enabled = false;
                }
            }
            dtJODate.Value = DateTime.Now;

           
           
            
            clsJODetailsBindingSource.DataSource =  JobOrder.ListOfJODetails; //async
            clsMechanicsBindingSource.DataSource =  JobOrder.ListOfMechanics; //async
            DisplayJOBDetails(); //async
            //Application.UseWaitCursor = false;  //chloe
            if (Program.CurrentUser.BranchId != 1)
            {
                tbChecklistNo.Text = "CH000";
            }
        }
        #endregion
        private void HideControlsFromEncoders()
        {
            if (Program.CurrentUser.UserLevelId == 3)
            {
                dgvTariff.Columns[16].IsVisible = false;
                dgvTariff.Columns[17].IsVisible = false;
            }
        }
        #region Customer Lookup
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmExternalCustomerLookup CustomerLookupPage = new frmExternalCustomerLookup();
            CustomerLookupPage.DataEvent.OnDataConfirm += Customer_DataEvent_OnDataConfirm;
            CustomerLookupPage.ShowDialog();
        }
        void Customer_DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                clsCustomer CustomerInfo = (clsCustomer)obj;
                JobOrder.CustomerType = CustomerInfo.CustomerType;
                JobOrder.CustomerId = CustomerInfo.Id;
                JobOrder.CustomerName = CustomerInfo.CustomerName;
                JobOrder.Location = CustomerInfo.CustomerAddress;

                tbCustomerType.Text = CustomerInfo.CustomerType.ToString();
                tbCustomerName.Text = CustomerInfo.CustomerName;
                tbCustomerAddress.Text = CustomerInfo.CustomerAddress;                
            }
        }
        #endregion
        #region Search Drivers
        private void btnDrivers_Click(object sender, EventArgs e)
        {
            loadDrivers();
        }
        void loadDrivers()
        {
            EmployeeRepo = new EmployeeRepository();
            clsEmployeeBindingSource.DataSource = EmployeeRepo.SearchBy(mcbDriversName.Text.ToString());
            mcbDriversName.Focus();
        }
        private void mcbDriversName_SelectedIndexChanged(object sender, EventArgs e)
        {
            mcbDriversName.Text = ((clsEmployee)clsEmployeeBindingSource.Current).FullName;            
        }
        #endregion       
        #region Equipment Lookup
        private void btnEquipment_Click(object sender, EventArgs e)
        {
            frmEquipmentLookup EquipmentLookupPage = new frmEquipmentLookup() { EquipmentHistoryPanel = true, ShowAll = true };
            EquipmentLookupPage.DataEvent.OnDataConfirm += Equipment_DataEvent_OnDataConfirm;
            EquipmentLookupPage.ShowDialog();
        }

        void Equipment_DataEvent_OnDataConfirm(object obj)
        {
            clsEquipment EquipmentInfo = (clsEquipment)obj;
            equipmentId = EquipmentInfo.EquipmentMasterlistId;
            if (!IsEquipmentReleased(EquipmentInfo))
            {
                if (obj != null)
                {
                    JobOrder.EquipmentId = EquipmentInfo.EquipmentMasterlistId;
                    JobOrder.ItemType = EquipmentInfo.EquipmentType;
                    JobOrder.EquipmentCode = EquipmentInfo.EquipmentCode;
                    JobOrder.EquipmentName = EquipmentInfo.EquipmentName;
                    JobOrder.EquipmentLocation = EquipmentInfo.Location;

                    tbEquipmentCode.Text = EquipmentInfo.EquipmentCode;
                    tbEquipmentName.Text = EquipmentInfo.EquipmentName;
                    tbLocation.Text = EquipmentInfo.Location;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Sorry, the equipment being selected is still in releasal list. Would you like to release it now ?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    frmJOEquipReleasal form = new frmJOEquipReleasal();
                    form.ShowDialog();
                }
                else if (result == DialogResult.No)
                {
                    new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "The transaction has been canceled."
                    }.ShowDialog();
                }
            }
        }
        #endregion       
        #region Contractor Lookup
        private void btnContractor_Click(object sender, EventArgs e)
        {
            frmExternalContratorLookup ExternalContratorPage = new frmExternalContratorLookup();
            ExternalContratorPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            ExternalContratorPage.ShowDialog();
        }

        void DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                clsContractor ContractorInfo = (clsContractor)obj;
                JobOrder.ContractorId = ContractorInfo.Id;
                JobOrder.ContractorType = ContractorInfo.ContractorType;
                JobOrder.ContractorCategory = ContractorInfo.ContractorCategory;
                JobOrder.ContractorName = ContractorInfo.ContractorSection;
                JobOrder.ContractorSectionHead = ContractorInfo.SectionHead;

                tbContractorName.Text = ContractorInfo.ContractorSection;
                tbContractorSectionHead.Text = ContractorInfo.SectionHead;

                gbJobDescription.Enabled = true;
                gbMechanics.Enabled = true;
            }
        }
        #endregion
        #region Tariff
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            clsJODetailsBindingSource.AddNew();
            loadTariff();
            dgvTariff.Columns["Section"].ReadOnly = true;
            dgvTariff.Columns["JobCategory"].ReadOnly = true;
            dgvTariff.Columns["JobType"].ReadOnly = true;
            dgvTariff.Columns["Price"].ReadOnly = true;
            dgvTariff.Columns["Unit"].ReadOnly = true;
            dgvTariff.Columns["NoOfMechanics"].ReadOnly = true;
        }
        private void radBindingNavigator1DeleteItem_Click(object sender, EventArgs e)
        {
            clsJODetails curJODetails = (clsJODetails)clsJODetailsBindingSource.Current;
            if (curJODetails.Id == 0)
            {
                clsJODetailsBindingSource.RemoveCurrent();                
            }
            else
            {
                curJODetails.IsActive = false;
                clsJODetailsBindingSource.ResetCurrentItem();
            }            
        }
        private void dgvTariff_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (e.Column.Name == "cbDescription")
            {
                //GridViewComboBoxColumn cbDescription = (GridViewComboBoxColumn)dgvTariff.Columns["cbDescription"];
                //cbDescription.FilteringMode = GridViewFilteringMode.DisplayMember;
            }
        }
        private void dgvTariff_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            clsJODetails curJODetails = new clsJODetails();
            if (clsTariffBindingSource.Current != null)
            {
                if (e.Column.Name == "cbDescription")
                {
                    curJODetails = (clsJODetails)clsJODetailsBindingSource.Current;
                    curJODetails.TariffId = ((clsTariff)clsTariffBindingSource.Current).Id;
                    curJODetails.Section = ((clsTariff)clsTariffBindingSource.Current).SectionName;
                    curJODetails.JobCategory = ((clsTariff)clsTariffBindingSource.Current).JobCategoryName;
                    curJODetails.JobType = ((clsTariff)clsTariffBindingSource.Current).JobTypeName;
                    curJODetails.NoOfMechanics = ((clsTariff)clsTariffBindingSource.Current).NoOfMechanics;
                    curJODetails.WorkTimeSpan = ((clsTariff)clsTariffBindingSource.Current).WorkTimeSpan;
                    curJODetails.Unit = ((clsTariff)clsTariffBindingSource.Current).UnitName;
                    curJODetails.Price = ((clsTariff)clsTariffBindingSource.Current).Price;
                    clsJODetailsBindingSource.ResetCurrentItem();    
                }
            }
            else
            {
                throw new Exception("Please select Tariff.");
            }
        }
        void DisplayJOBDetails()
        {
            foreach (var jo in JobOrder.ListOfJODetails)
            {                
                if (dgvTariff.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dgvTariff.ChildRows.Count; i++)
                    {
                        if ((Int64)dgvTariff.Rows[i].Cells["TariffId"].Value == jo.TariffId)
                        {
                            dgvTariff.Rows[i].Cells["cbDescription"].Value = jo.TariffId;
                        }
                    }
                }
            }
        }
        void loadTariff()
        {
            TariffRepo = new TariffRepository();
            GridViewComboBoxColumn cbDescription = (GridViewComboBoxColumn)dgvTariff.Columns["cbDescription"];
           
            cbDescription.DropDownStyle = RadDropDownStyle.DropDown;
            cbDescription.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                if (JobOrder.ContractorCategory == clsEnums.ContractorCategory.Person && JobOrder.ContractorType == clsEnums.ContractorType.Internal)
                {
                    e.Result = TariffRepo.SearchBy(" WHERE T.Active = 1 AND T.SectionId =" + JobOrder.ContractorId);
                }
                else
                {
                    e.Result = TariffRepo.GetAll();
                }
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsTariffBindingSource.DataSource = (List<clsTariff>)e.Result;
            };
            cbDescription.DisplayMember = "WorkDescription";
            cbDescription.ValueMember = "Id";
            cbDescription.DataSource = clsTariffBindingSource;            
            bg.RunWorkerAsync();
        }
        private void dgvTariff_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement btnViewTariff = (GridCommandCellElement)sender;            
            if (btnViewTariff.ColumnInfo.Name == "btnTariffView")
            {
                frmTariffLookup TarriffLookupPage = new frmTariffLookup();
                TarriffLookupPage.ShowDialog();
            }
        }
        #region Add Tariff
        private void radBindingNavigator1AddTariff_Click(object sender, EventArgs e)
        {
            frmTariff TarrifEntry = new frmTariff();
            TarrifEntry.DataEvent.OnDataConfirm += Tariff_DataEvent_OnDataConfirm;
            if (TarrifEntry.ShowDialog() == DialogResult.OK)
            {
                //clsJODetailsBindingSource.AddNew();

            }
        }
        void Tariff_DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                loadTariff();

                clsJODetails CurJODet = (clsJODetails)clsJODetailsBindingSource.Current;

                TariffRepo = new TariffRepository();
                clsTariff CurTariff = TariffRepo.SearchBy(" WHERE T.JobCategoryId = " + ((clsTariff)obj).JobCategoryId + " AND T.JobTypeId = " + ((clsTariff)obj).JobTypeId + " AND T.SectionId = " + ((clsTariff)obj).SectionId + " AND T.WorkDescription = '" + ((clsTariff)obj).WorkDescription.Trim() + "'").FirstOrDefault();//SingleOrDefault();

                if (CurJODet == null)
                {
                    clsJODetailsBindingSource.AddNew();
                }
                CurJODet = (clsJODetails)clsJODetailsBindingSource.Current;
                if (CurJODet.TariffId != 0)
                {
                    clsJODetailsBindingSource.AddNew();
                    CurJODet = (clsJODetails)clsJODetailsBindingSource.Current;
                }
                CurJODet.Section = CurJODet.TariffId == 0 ? CurTariff.SectionName : "";
                CurJODet.JobCategory = CurJODet.TariffId == 0 ? CurTariff.JobCategoryName : "";
                CurJODet.JobType = CurJODet.TariffId == 0 ? CurTariff.JobTypeName : "";
                CurJODet.NoOfMechanics = CurJODet.TariffId == 0 ? CurTariff.NoOfMechanics : 0;
                CurJODet.WorkTimeSpan = CurJODet.TariffId == 0 ? CurTariff.WorkTimeSpan : 0;
                CurJODet.Unit = CurJODet.TariffId == 0 ? CurTariff.UnitName : "";
                CurJODet.Price = CurJODet.TariffId == 0 ? CurTariff.Price : 0;
                CurJODet.TariffId = CurJODet.TariffId == 0 ? CurTariff.Id : 0;

                dgvTariff.Rows[dgvTariff.CurrentRow.Index].Cells["cbDescription"].Value = CurTariff.Id;

                clsJODetailsBindingSource.ResetCurrentItem();
            }
        }
        #endregion
        #endregion     
        #region Mechanics
        private void radBindingNavigator2AddNewItem_Click(object sender, EventArgs e)
        {
            frmPersonnelLookup EmployeeLookupPage = new frmPersonnelLookup();
            EmployeeLookupPage.DataEvent.OnDataConfirm += Employee_DataEvent_OnDataConfirm;
            EmployeeLookupPage.ShowDialog();
            //frmEmployeeNonEmployeeLookup EmployeeLookupPage = new frmEmployeeNonEmployeeLookup();
            //EmployeeLookupPage.DataEvent.OnDataConfirm += Employee_DataEvent_OnDataConfirm;
            //EmployeeLookupPage.ShowDialog();
            
        }
        void Employee_DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                clsPersonnels EmployeeInfo = (clsPersonnels)obj;
                clsMechanics MechanicInfo = new clsMechanics();
                MechanicInfo.EmployeeId = EmployeeInfo.EmployeeId;
                MechanicInfo.FirstName = EmployeeInfo.FirstName;
                MechanicInfo.MiddleName = EmployeeInfo.MiddleName;
                MechanicInfo.Lastname = EmployeeInfo.LastName;
                MechanicInfo.NameExtension = EmployeeInfo.NameExtension;
                clsMechanicsBindingSource.Add(MechanicInfo);
            }
        }
        private void radBindingNavigator2DeleteItem_Click(object sender, EventArgs e)
        {
            clsMechanics curJOMechanics = (clsMechanics)clsMechanicsBindingSource.Current;
            if (curJOMechanics.Id == 0)
            {
                clsMechanicsBindingSource.RemoveCurrent();
            }
            else
            {
                curJOMechanics.IsActive = false;
                clsJODetailsBindingSource.ResetCurrentItem();
            }
        }
        #endregion
        #region DisplayProperties
        void DisplayProperties()
        {
            tbJONumber.Text = JobOrder.JONoStr;
            tbChecklistNo.Text = JobOrder.ChecklistNo;
            dtJODate.Value = JobOrder.JODate;

            tbCustomerName.Text = JobOrder.CustomerName;
            tbCustomerAddress.Text = JobOrder.Location;
            //Type type = e.GetType();
            tbCustomerType.Text = JobOrder.CustomerType.ToString();

            tbEquipmentCode.Text = JobOrder.EquipmentCode;
            tbEquipmentName.Text = JobOrder.EquipmentName;
            tbEquipmentRemarks.Text = JobOrder.EquipmentRemarks;
            tbLocation.Text = JobOrder.EquipmentLocation;

            tbContractorName.Text = JobOrder.ContractorName;
            tbContractorSectionHead.Text = JobOrder.ContractorSectionHead;

            tbApprover.Text = JobOrder.ApproverName;
            tbApproverPosition.Text = JobOrder.ApproverPosition;
            if (JobOrder.DriverId != 0)
            {
                loadDrivers();
                mcbDriversName.SelectedValue = JobOrder.DriverId;
                mcbDriversName.Text = JobOrder.DriverName;                
            }            

            chkEquipOnBranch.Checked = JobOrder.EquipmentOnBranch;
            chkPartsRequest.Checked = JobOrder.PartsRequest;

            gbJobDescription.Enabled = true;
            gbMechanics.Enabled = true;
            string finalSearchStatmement = string.Empty;
            if (JobOrder.Mileage.Contains("mi."))
            {
                char[] removeEnd = { 'm', 'i', '.' };
                finalSearchStatmement = JobOrder.Mileage.TrimEnd(removeEnd);
               
                rddMetric.SelectedIndex = 0;
            }
            else if (JobOrder.Mileage.Contains("km."))
            {
                char[] removeEnd = { 'k', 'm', '.' };
                finalSearchStatmement = JobOrder.Mileage.TrimEnd(removeEnd);
                rddMetric.SelectedIndex = 1;
            }
            tbxMileage.Text = finalSearchStatmement;

            loadTariff();
        }
        #endregion
        #region MapProperties
        clsJobOrder MapProperties()
        {
            JobOrder.ContractorSectionHead = tbContractorSectionHead.Text;
            JobOrder.JODate = dtJODate.Value;            
            JobOrder.BranchId = Program.CurrentUser.BranchId;
            JobOrder.ChecklistNo = tbChecklistNo.Text;
            JobOrder.CustomerRemarks = tbCustomerRemarks.Text;
            JobOrder.EquipmentOnBranch = chkEquipOnBranch.Checked;
            JobOrder.Location = tbLocation.Text;
            JobOrder.PartsRequest = chkPartsRequest.Checked;
            JobOrder.EquipmentRemarks = tbEquipmentRemarks.Text;
            JobOrder.UserId = Program.CurrentUser.Id;
            JobOrder.EncoderName = Program.CurrentUser.FullName;
            JobOrder.EncoderPosition = Program.CurrentUser.Position;
            if (clsEmployeeBindingSource.Current != null)
            {
                JobOrder.DriverId = (Int64)mcbDriversName.SelectedValue;
                JobOrder.DriverName = mcbDriversName.Text;
            }            
            JobOrder.UserId = Program.CurrentUser.Id;
            JobOrder.EncoderName = Program.CurrentUser.FullName;
            JobOrder.Status = JOROStatus;
            JobOrder.ListOfJODetails.ForEach(x => {
                if (JOROStatus == clsEnums.JOROStatus.Generated)
                {
                    x.StatusId = new StatusRepository().SearchBy(" IsDefault = '1'").FirstOrDefault().Id;
                }
                else 
                {
                    x.StatusId = new StatusRepository().SearchBy(" IsApproval = '1'").FirstOrDefault().Id;
                }
            });
            if (rddMetric.Text.Contains("Miles"))
                JobOrder.Mileage = tbxMileage.Text + " mi.";
            else if (rddMetric.Text.Contains("Kilometers"))
                JobOrder.Mileage = tbxMileage.Text + " km.";
            return JobOrder;
        }
        #endregion
        #region MapTransactionLogs
        clsJOTransactionLogs MapTransactionLog()
        {
            clsJOTransactionLogs TransactionLog = new clsJOTransactionLogs();
            TransactionLog.ChecklistNo = JobOrder.ChecklistNo;
            TransactionLog.JOId = JobOrder.Id;
            TransactionLog.JONo = JobOrder.JONo;
            TransactionLog.RefYear = JobOrder.RefYear;
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
            TransactionLog.ApproveId = JobOrder.ApproverId;
            TransactionLog.ApproverName = JobOrder.ApproverName;
            TransactionLog.ApproverPosition = JobOrder.ApproverPosition;
            TransactionLog.PrintCount = JobOrder.PrintCount;
            TransactionLog.Mileage = JobOrder.Mileage;
            JobOrder.ListOfJODetails.ForEach( y=> 
            {
                TransactionLog.ListOfJODetailTransactionLogs.Add(
                    new clsJODetailTransactionLogs { 
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
            JobOrder.ListOfMechanics.ForEach(y => {
                TransactionLog.ListOfMechanicsTransactionLogs.Add(
                new clsJOMechTransactionLogs { 
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
        #region Validation
        Boolean CanSave()
        {
            if (JobOrder.DriverId == 0)
            {
                //throw new Exception("Select Driver!");
            }
            if (JobOrder.CustomerId == 0)
            {
                throw new Exception("Please select CUSTOMER!");
            }
            if (JobOrder.ContractorId == 0)
            {
                throw new Exception("Please select CONTRACTOR!");
            }
            if (JobOrder.EquipmentId == 0)
            {
                throw new Exception("Please select EQUIPMENT!");
            }
            if (JobOrder.ApproverId == 0)
            {
                throw new Exception("Please select APPROVER!");
            }
            if (JobOrder.ListOfJODetails.Count == 0)
            {
                throw new Exception("Please input JOB ORDER DETAILS!");
            }
            if (String.IsNullOrWhiteSpace(JobOrder.ChecklistNo))
            {
                throw new Exception("Please input Checklist Number!");
            }
            if (JobOrder.ListOfMechanics.Count == 0)
            {
                throw new Exception("Please input MECHANIC DETAILS!");
            }
            if (!string.IsNullOrWhiteSpace(tbxMileage.Text))
            {
                if(string.IsNullOrWhiteSpace(rddMetric.Text))
                    throw new Exception("Please select mileage metric!");
            }
            return true;
        }
        #endregion
        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            JOROStatus = clsEnums.JOROStatus.Generated;
            Save();
        }
        private void btnSaveApprove_Click(object sender, EventArgs e)
        {
            JOROStatus = clsEnums.JOROStatus.Approved;
            Save();
        }
        void Save()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    MapProperties();
                    CanSave();
                    SaveAction.Invoke();
                    JobOrder.ListOfJODetails = new JODetailRepository().SearchBy("Where JD.JOId = " + JobOrder.Id);
                    JobOrder.ListOfMechanics = new JOMechanicRepository().SearchBy("Where JM.JOId = " + JobOrder.Id);
                    new JOTransactionLogRepository().Add(MapTransactionLog());
                    JobOrder.ListOfJODetails.ForEach(x=> {
                        if (JOROStatus == clsEnums.JOROStatus.Approved)
                        {
                            if (new JOAuditTrailRepository().SearchBy(" Where JAT.JODetailId = " + x.Id + " AND S.IsApproval = 1").Count == 0)
                            {
                                clsJOAuditTrail JOAuditTrail = new clsJOAuditTrail();
                                JOAuditTrail.JODetailId = x.Id;
                                JOAuditTrail.StatusId = x.StatusId;
                                JOAuditTrail.Remarks = x.Remarks;
                                JOAuditTrail.UserId = Program.CurrentUser.Id;
                                new JOAuditTrailRepository().Add(JOAuditTrail);
                            }
                        }
                    });
                    SaveAuditTrail();
                    UpdatePPEStatus();
                    SavePPEStatusLog();
                    DisableControls();
                    ts.Complete();
                    //DataEvent.ConfirmData(JobOrder);

                    new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been saved successfully!"
                    }.ShowDialog();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw new Exception(ex.Message);
                }
            }
            mnuAddNew.Enabled = true;
        }
        #endregion  
        #region Add|Edit
        void Add()
        {
            JORepo = new JORepository();
            JORepo.Add(MapProperties());
            JobOrder.Id = JORepo.JOId;
            JobOrder.JONo = JORepo.FindByID(JobOrder.Id).JONo;
            JobOrder.RefYear = JORepo.FindByID(JobOrder.Id).RefYear;
            tbJONumber.Text = JobOrder.JONoStr;
        }
        void Edit()
        {
            JORepo = new JORepository();
            JORepo.Update(MapProperties());
        }
        #endregion
        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptJobOrder report = new rptJobOrder(MapProperties(), ListOfActivity);
            frmReportViewer ReportViewerPage = new frmReportViewer(report, MapTransactionLog()) { IsJO = true };
            ReportViewerPage.Show();

            //rptJobOrder report = new rptJobOrder(MapProperties(), uniqueListActivity);
            //var reportSource = new Telerik.Reporting.InstanceReportSource() { ReportDocument = report };
            //this.reportViewer1.ReportSource = reportSource;
            //this.reportViewer1.RefreshReport();
        }
        #endregion
        #region Approver
        private void btnApprover_Click(object sender, EventArgs e)
        {
            frmEmployeeNonEmployeeLookup EmployeeLookupPage = new frmEmployeeNonEmployeeLookup();
            EmployeeLookupPage.DataEvent.OnDataConfirm += Approver_DataEvent_OnDataConfirm;
            EmployeeLookupPage.ShowDialog();
        }
        void Approver_DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                clsEmployee EmployeeInfo = (clsEmployee)obj;
                //clsMechanics MechanicInfo = new clsMechanics();
                JobOrder.ApproverId = EmployeeInfo.Id;
                JobOrder.ApproverName = EmployeeInfo.FullName;
                JobOrder.ApproverPosition = EmployeeInfo.Position;

                tbApprover.Text = EmployeeInfo.FullName;
                tbApproverPosition.Text = EmployeeInfo.Position;
            }
        }
        #endregion        
        #region Controls
        void DisableControls()
        {
            mnuAddNew.Enabled = true;
            btnPrint.Enabled = true;
            btnSave.Enabled = false;
            btnSaveApprove.Enabled = false;
            pnDetails.Enabled = false;
        }
        void EnableControls()
        {
            btnPrint.Enabled = false;
            btnSave.Enabled = true;
            btnSaveApprove.Enabled = true;
            pnDetails.Enabled = true;
        }
        void ClearProperties()
        {
            tbChecklistNo.Text = "CH0001";
            tbJONumber.Text = "AUTO-GENERATED";
            tbCustomerType.Text = "";
            tbCustomerName.Text = "";
            tbCustomerRemarks.Text = "";
            tbCustomerAddress.Text = "";

            tbEquipmentCode.Text = "";
            tbEquipmentName.Text = "";
            tbEquipmentRemarks.Text = "";
            tbLocation.Text = "";

            tbContractorName.Text = "";
            tbContractorSectionHead.Text = "";

            tbApprover.Text = "";
            tbApproverPosition.Text = "";

            clsJODetailsBindingSource.Clear();
            clsMechanicsBindingSource.Clear();
        }
        #endregion
        #region AddNew
        private void mnuAddNew_Click(object sender, EventArgs e)
        {
            EnableControls();
            JobOrder = new clsJobOrder();
            CRUDEMode = clsEnums.CRUDEMode.Add;
            SaveAction = Add;
            dtJODate.Value = DateTime.Now;
            //DataEvent = new clsDataEvent();
            clsJODetailsBindingSource.DataSource = JobOrder.ListOfJODetails;
            clsMechanicsBindingSource.DataSource = JobOrder.ListOfMechanics;
            ClearProperties();
        }
        #endregion                
        #region SaveAuditTrail
        
        void SaveAuditTrail()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        clsUsersLog AddMapProperties()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                UsersLog.Username = Program.CurrentUser.UserName;
                UsersLog.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
                UsersLog.EmpName = Program.CurrentUser.FullName;
                UsersLog.BranchId = Program.CurrentUser.BranchId;
                UsersLog.UserLevelId = Program.CurrentUser.UserLevelId;
                UsersLog.ComputerName = System.Environment.MachineName;
                UsersLog.DateLogin = System.DateTime.Now;
                UsersLog.TimeLogin = System.DateTime.Now;
                UsersLog.DateLogout = System.DateTime.Now;
                UsersLog.TimeLogout = System.DateTime.Now;
                UsersLog.OnlineUser = false;
                UsersLog.ActivityType = 3;
                UsersLog.DayActivity = "Add Job Order " + "(" + JobOrder.JONoStr + " - " + tbEquipmentCode.Text + " - " + tbContractorName.Text + ")";
                
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                UsersLog.Username = Program.CurrentUser.UserName;
                UsersLog.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
                UsersLog.EmpName = Program.CurrentUser.FullName;
                UsersLog.BranchId = Program.CurrentUser.BranchId;
                UsersLog.UserLevelId = Program.CurrentUser.UserLevelId;
                UsersLog.ComputerName = System.Environment.MachineName;
                UsersLog.DateLogin = System.DateTime.Now;
                UsersLog.TimeLogin = System.DateTime.Now;
                UsersLog.DateLogout = System.DateTime.Now;
                UsersLog.TimeLogout = System.DateTime.Now;
                UsersLog.OnlineUser = false;
                UsersLog.ActivityType = 4;
                UsersLog.DayActivity = "Update Job Order " + "(" + JobOrder.JONoStr + " - " + tbEquipmentCode.Text + " - " + tbContractorName.Text + ")";
            }
            return UsersLog;
        }

        #endregion
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
                PPEStatusLog.EquipmentId = JobOrder.EquipmentId;
                PPEStatusLog.PPETypeId = JobOrder.PPETypeId;
                PPEStatusLog.PPEClassId = JobOrder.PPEClassId;
                PPEStatusLog.EquipmentStatusId = 3;
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
            EquipmentURRepo.Update(JobOrder.EquipmentId);
        }
        #endregion
        #region IsEquipmentReleased?
        private bool IsEquipmentReleased(clsEquipment _equipment)
        {
            clsEquipment equipment = new clsEquipment();
            equipment = new EquipmentReleasalRepository().FindEquipmentIfReleasedJO(_equipment.EquipmentMasterlistId, _equipment.EquipmentType);
            if (equipment != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #endregion

        private void BtnClearFields_Click(object sender, EventArgs e)
        {
            tbCustomerType.Clear();
            tbCustomerName.Clear();
            tbCustomerRemarks.Clear();
            tbCustomerAddress.Clear();
            tbEquipmentCode.Clear();
            tbEquipmentName.Clear();
            tbEquipmentRemarks.Clear();
            mcbDriversName.Text = "";
            tbApproverPosition.Clear();
            tbLocation.Clear();
            tbContractorName.Clear();
            tbContractorSectionHead.Clear();
            tbApprover.Clear();
        }
    }
}
