using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.RescueOrder;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Presentation.vLookups;
using FGCIJOROSystem.Domain.Configurations.Customers;
using FGCIJOROSystem.Domain.Configurations.Contractors;
using FGCIJOROSystem.Domain.Global;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Domain.Configurations.Tariff;
using Telerik.WinControls;
using System.Transactions;
using FGCIJOROSystem.DAL.Repositories.RORepo;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Reports.rJobOrder;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Presentation.vConfiguration.vTariff;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.DAL.Repositories.EquipmentURRepo;
using FGCIJOROSystem.DAL.Repositories.PPEStatusLogRepo;
using FGCIJOROSystem.Domain.PPEStatusLog;
using FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo;

namespace FGCIJOROSystem.Presentation.vRescueOrder
{
    public partial class ucROEntry : UserControl
    {
        #region Properties
        long equipmentId;
        EmployeeRepository EmployeeRepo;
        SectionJORORepository SectionRepo;
        TariffRepository TariffRepo;
        JobCategoryRepository JobCategoryRepo;
        JobTypeRepository JobTypeRepo;
        RORepository RORepo;
        EquipmentURRepository EquipmentURRepo;
        PPEStatusLogRepository PPEStatusLogRepo;

        clsPPEStatusLog PPEStatusLog;
        clsEmployee ServiceDriver;
        clsEquipment ServiceVehicle;
        clsRescueOrder RescueOrder;
        clsROTransactionLogs TransactionLog;

        clsEnums.CRUDEMode CRUDEMode;
        Action SaveAction;

        public clsDataEvent DataEvent;
        clsEnums.JOROStatus JOROStatus = new clsEnums.JOROStatus();

        UsersLogRepository UsersLogRepo;
        clsUsersLog UsersLog;
        #endregion
        #region Methods
        public ucROEntry()
        {
            InitializeComponent();
            RescueOrder = new clsRescueOrder();
            TransactionLog = new clsROTransactionLogs();
            CRUDEMode = clsEnums.CRUDEMode.Add;
            DataEvent = new clsDataEvent();
            UsersLog = new clsUsersLog();
            PPEStatusLog = new clsPPEStatusLog();
        }
        public ucROEntry(clsRescueOrder obj)
        {
            InitializeComponent();
            RescueOrder = obj;
            TransactionLog = new clsROTransactionLogs();
            CRUDEMode = clsEnums.CRUDEMode.Edit;
            DataEvent = new clsDataEvent();
            UsersLog = new clsUsersLog();
            PPEStatusLog = new clsPPEStatusLog();
        }
        #region Load
        private void ucROEntry_Load(object sender, EventArgs e)
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
            dtRODate.Value = DateTime.Now;
            clsRODetailsBindingSource.DataSource = RescueOrder.ListOfRODetails;
            clsROMechanicsBindingSource.DataSource = RescueOrder.ListOfROMechanics;
            DisplayJOBDetails();
            //Application.UseWaitCursor = false;  //chloe
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
                RescueOrder.CustomerType = CustomerInfo.CustomerType;
                RescueOrder.CustomerId = CustomerInfo.Id;
                RescueOrder.CustomerName = CustomerInfo.CustomerName;
                RescueOrder.CustomerLocation = CustomerInfo.CustomerAddress;

                tbCustomerType.Text = CustomerInfo.CustomerType.ToString();
                tbCustomerName.Text = CustomerInfo.CustomerName;
                tbCustomerAddress.Text = CustomerInfo.CustomerAddress;

                RescueOrder.CustomerName = tbCustomerName.Text;
                RescueOrder.CustomerRemarks = tbCustomerRemarks.Text;
            }
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
                    RescueOrder.EquipmentId = EquipmentInfo.EquipmentMasterlistId;
                    RescueOrder.ItemType = EquipmentInfo.EquipmentType;

                    tbEquipmentCode.Text = EquipmentInfo.EquipmentCode;
                    tbEquipmentName.Text = EquipmentInfo.EquipmentName;
                    tbLocation.Text = EquipmentInfo.Location;

                    RescueOrder.EquipmentCode = tbEquipmentCode.Text;
                    RescueOrder.EquipmentName = tbEquipmentName.Text;
                    RescueOrder.EquipmentRemarks = tbEquipmentRemarks.Text;
                    RescueOrder.EquipmentLocation = tbLocation.Text;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Sorry, the equipment being selected is still in releasal list. Would you like to release it now ?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    frmROEquipReleasal form = new frmROEquipReleasal();
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
                RescueOrder.ContractorId = ContractorInfo.Id;
                RescueOrder.ContractorType = ContractorInfo.ContractorType;
                RescueOrder.ContractorCategory = ContractorInfo.ContractorCategory;
                RescueOrder.ContractorName = ContractorInfo.ContractorSection;
                RescueOrder.ContractorSectionHead = ContractorInfo.SectionHead;

                tbContractorName.Text = ContractorInfo.ContractorSection;
                tbContractorSectionHead.Text = ContractorInfo.SectionHead;

                gbJobDescription.Enabled = true;
                gbMechanics.Enabled = true;
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
        #region Tariff
        private void radBindingNavigator1AddNewItem_Click(object sender, EventArgs e)
        {
            clsRODetailsBindingSource.AddNew();
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
            clsRODetails curRODetails = (clsRODetails)clsRODetailsBindingSource.Current;
            if (curRODetails.Id == 0)
            {
                clsRODetailsBindingSource.RemoveCurrent();
            }
            else
            {
                curRODetails.IsActive = false;
                clsRODetailsBindingSource.ResetCurrentItem();
            }
        }
        private void dgvTariff_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            clsRODetails curRODetails = new clsRODetails();
            if (clsTariffBindingSource.Current != null)
            {
                if (e.Column.Name == "cbDescription")
                {
                    curRODetails = (clsRODetails)clsRODetailsBindingSource.Current;
                    curRODetails.TariffId = ((clsTariff)clsTariffBindingSource.Current).Id;
                    curRODetails.Section = ((clsTariff)clsTariffBindingSource.Current).SectionName;
                    curRODetails.JobCategory = ((clsTariff)clsTariffBindingSource.Current).JobCategoryName;
                    curRODetails.JobType = ((clsTariff)clsTariffBindingSource.Current).JobTypeName;
                    curRODetails.NoOfMechanics = ((clsTariff)clsTariffBindingSource.Current).NoOfMechanics;
                    curRODetails.WorkTimeSpan = ((clsTariff)clsTariffBindingSource.Current).WorkTimeSpan;
                    curRODetails.Unit = ((clsTariff)clsTariffBindingSource.Current).UnitName;
                    curRODetails.Price = ((clsTariff)clsTariffBindingSource.Current).Price;
                    clsRODetailsBindingSource.ResetCurrentItem();
                }
            }
            else
            {
                throw new Exception("Please select Tariff.");
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
                if (RescueOrder.ContractorCategory == clsEnums.ContractorCategory.Person && RescueOrder.ContractorType == clsEnums.ContractorType.Internal)
                {
                    e.Result = TariffRepo.SearchBy(" WHERE T.SectionId =" + RescueOrder.ContractorId);
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
            cbDescription.DataSource = clsTariffBindingSource;
            cbDescription.DisplayMember = "WorkDescription";
            cbDescription.ValueMember = "Id";
            bg.RunWorkerAsync();


        }
        void DisplayJOBDetails()
        {
            foreach (var ro in RescueOrder.ListOfRODetails)
            {
                if (dgvTariff.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dgvTariff.ChildRows.Count; i++)
                    {
                        if ((Int64)dgvTariff.Rows[i].Cells["TariffId"].Value == ro.TariffId)
                        {
                            dgvTariff.Rows[i].Cells["cbDescription"].Value = ro.TariffId;
                        }
                    }
                }
            }
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
        #endregion
        #region Mechanics
        private void radBindingNavigator2AddNewItem_Click(object sender, EventArgs e)
        {
            //frmEmployeeNonEmployeeLookup EmployeeLookupPage = new frmEmployeeNonEmployeeLookup();
            //EmployeeLookupPage.DataEvent.OnDataConfirm += Employee_DataEvent_OnDataConfirm;
            //EmployeeLookupPage.ShowDialog();

            frmPersonnelLookup EmployeeLookupPage = new frmPersonnelLookup();
            EmployeeLookupPage.DataEvent.OnDataConfirm += Employee_DataEvent_OnDataConfirm;
            EmployeeLookupPage.ShowDialog();

        }
        void Employee_DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                clsPersonnels EmployeeInfo = (clsPersonnels)obj;
                clsROMechanics MechanicInfo = new clsROMechanics();
                MechanicInfo.EmployeeId = EmployeeInfo.EmployeeId;
                MechanicInfo.FirstName = EmployeeInfo.FirstName;
                MechanicInfo.MiddleName = EmployeeInfo.MiddleName;
                MechanicInfo.Lastname = EmployeeInfo.LastName;
                MechanicInfo.NameExtension = EmployeeInfo.NameExtension;
                MechanicInfo.Position = EmployeeInfo.PositionName;
                clsROMechanicsBindingSource.Add(MechanicInfo);
            }
        }
        private void radBindingNavigator2DeleteItem_Click(object sender, EventArgs e)
        {
            clsROMechanics curROMechanics = (clsROMechanics)clsROMechanicsBindingSource.Current;
            if (curROMechanics.Id == 0)
            {
                clsROMechanicsBindingSource.RemoveCurrent();
            }
            else
            {
                curROMechanics.IsActive = false;
                clsROMechanicsBindingSource.ResetCurrentItem();
            }
        }
        #endregion
        #region Services
        private void btnServiceVehicle_Click(object sender, EventArgs e)
        {
            frmEmployeeNonEmployeeLookup EmployeeLookupPage = new frmEmployeeNonEmployeeLookup();
            EmployeeLookupPage.DataEvent.OnDataConfirm += Mechanics_DataEvent_OnDataConfirm;
            EmployeeLookupPage.ShowDialog();

        }
        void Mechanics_DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                clsEmployee EmployeeInfo = (clsEmployee)obj;
                ServiceDriver = EmployeeInfo;
                RescueOrder.ServiceDriverId = EmployeeInfo.Id;

                tbServiceDriver.Text = EmployeeInfo.FullName;
            }
        }
        private void btnServiceDriver_Click(object sender, EventArgs e)
        {
            frmEquipmentLookup EquipmentLookupPage = new frmEquipmentLookup() { EquipmentHistoryPanel = false, ShowAll = false };
            EquipmentLookupPage.DataEvent.OnDataConfirm += ServiceVehicle_DataEvent_OnDataConfirm;
            EquipmentLookupPage.ShowDialog();
        }
        void ServiceVehicle_DataEvent_OnDataConfirm(object obj)
        {
            if (obj != null)
            {
                clsEquipment EquipmentInfo = (clsEquipment)obj;
                ServiceVehicle = EquipmentInfo;
                RescueOrder.ServiceVehicleId = EquipmentInfo.EquipmentMasterlistId;
                //RescueOrder.ItemType = EquipmentInfo.EquipmentType;

                tbServiceVehicle.Text = EquipmentInfo.EquipmentName + " : " + EquipmentInfo.EquipmentCode;
                //tbEquipmentName.Text = EquipmentInfo.EquipmentName;
                //tbLocation.Text = EquipmentInfo.Location;
            }
        }

        #region JobOut
        private void chkJOBOUT_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            btnServiceDriver.Enabled = chkJOBOUT.Checked == true ? false : true;
            btnServiceVehicle.Enabled = chkJOBOUT.Checked == true ? false : true;
        }
        #endregion
        #endregion
        #region DisplayProperties
        void DisplayProperties()
        {
            gbJobDescription.Enabled = true;
            gbMechanics.Enabled = true;
            tbChecklistNo.Text = RescueOrder.ChecklistNo;
            tbContractorName.Text = RescueOrder.ContractorName;
            tbContractorSectionHead.Text = RescueOrder.ContractorSectionHead;
            tbCustomerAddress.Text = RescueOrder.CustomerLocation;
            tbCustomerName.Text = RescueOrder.CustomerName;
            tbCustomerRemarks.Text = RescueOrder.CustomerRemarks;
            tbCustomerType.Text = RescueOrder.CustomerType.ToString();
            tbEquipmentCode.Text = RescueOrder.EquipmentCode;
            tbEquipmentName.Text = RescueOrder.EquipmentName;
            tbEquipmentRemarks.Text = RescueOrder.EquipmentRemarks;
            tbLocation.Text = RescueOrder.EquipmentLocation;
            tbServiceDriver.Text = RescueOrder.ServiceDriverName;
            tbServiceVehicle.Text = RescueOrder.ServiceVehicleName;
            dtRODate.Value = RescueOrder.RODate;
            tbRoNo.Text = RescueOrder.RONoStr;
            chkJOBOUT.Checked = RescueOrder.IsJobOut;
            loadTariff();
            if (RescueOrder.DriverId != 0)
            {
                loadDrivers();
                mcbDriversName.SelectedValue = RescueOrder.DriverId;
                mcbDriversName.Text = RescueOrder.DriverName;
            }
            ServiceDriver = new clsEmployee();
            ServiceDriver.Id = RescueOrder.ServiceDriverId;
            ServiceVehicle = new clsEquipment();
            ServiceVehicle.EquipmentMasterlistId = RescueOrder.ServiceVehicleId;
            string finalSearchStatmement = string.Empty;
            if (RescueOrder.Mileage.Contains("mi."))
            {
                char[] removeEnd = { 'm', 'i', '.' };
                finalSearchStatmement = RescueOrder.Mileage.TrimEnd(removeEnd);

                rddMetric.SelectedIndex = 0;
            }
            else if (RescueOrder.Mileage.Contains("km."))
            {
                char[] removeEnd = { 'k', 'm', '.' };
                finalSearchStatmement = RescueOrder.Mileage.TrimEnd(removeEnd);
                rddMetric.SelectedIndex = 1;
            }
            tbxMileage.Text = finalSearchStatmement;
        }
        #endregion
        #region MapProperties
        clsRescueOrder MapProperties()
        {
            RescueOrder.BranchId = Program.CurrentUser.BranchId;
            RescueOrder.ChecklistNo = tbChecklistNo.Text;
            RescueOrder.CustomerRemarks = tbCustomerRemarks.Text;
            RescueOrder.EquipmentRemarks = tbEquipmentRemarks.Text;
            RescueOrder.IsJobOut = chkJOBOUT.Checked;
            RescueOrder.RODate = dtRODate.Value;
            RescueOrder.EquipmentLocation = tbLocation.Text;
            RescueOrder.UserId = Program.CurrentUser.Id;
            RescueOrder.EncoderName = Program.CurrentUser.FullName;
            RescueOrder.EncoderPosition = Program.CurrentUser.Position;

            if (clsEmployeeBindingSource.Current != null)
            {
                RescueOrder.DriverId = (Int64)mcbDriversName.SelectedValue;
                RescueOrder.DriverName = mcbDriversName.Text;
            }

            RescueOrder.ServiceDriverId = chkJOBOUT.Checked == true ? 0 : ServiceDriver.Id;
            RescueOrder.ServiceVehicleId = chkJOBOUT.Checked == true ? 0 : ServiceVehicle.EquipmentMasterlistId;

            RescueOrder.CustomerLocation = tbCustomerAddress.Text;

            RescueOrder.ServiceDriverName = chkJOBOUT.Checked == true ? "" : tbServiceDriver.Text;
            RescueOrder.ServiceVehicleName = chkJOBOUT.Checked == true ? "" : tbServiceVehicle.Text;

            RescueOrder.Status = JOROStatus;
            RescueOrder.ListOfRODetails.ForEach(x =>
            {
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
                RescueOrder.Mileage = tbxMileage.Text + " mi.";
            else if (rddMetric.Text.Contains("Kilometers"))
                RescueOrder.Mileage = tbxMileage.Text + " km.";
            return RescueOrder;
        }
        #endregion
        #region MapTransactionLogs
        clsROTransactionLogs MapROTransactionLog()
        {
            TransactionLog.ROId = RescueOrder.Id;
            TransactionLog.BranchId = RescueOrder.BranchId;
            TransactionLog.ContractorCategory = RescueOrder.ContractorCategory;
            TransactionLog.ContractorId = RescueOrder.ContractorId;
            TransactionLog.ContractorType = RescueOrder.ContractorType;
            TransactionLog.CustomerId = RescueOrder.CustomerId;
            TransactionLog.CustomerCategory = RescueOrder.CustomerCategory;
            TransactionLog.CustomerType = RescueOrder.CustomerType;
            TransactionLog.CustomerRemarks = RescueOrder.CustomerRemarks;
            TransactionLog.DriverId = RescueOrder.DriverId;
            TransactionLog.EquipmentId = RescueOrder.EquipmentId;
            TransactionLog.EquipmentRemarks = RescueOrder.EquipmentRemarks;
            TransactionLog.IsJobOut = RescueOrder.IsJobOut;
            TransactionLog.ItemType = RescueOrder.ItemType;
            TransactionLog.Location = RescueOrder.EquipmentLocation;
            TransactionLog.RONo = RescueOrder.RONo;
            TransactionLog.RefYear = RescueOrder.RefYear;
            TransactionLog.ServiceDriverId = RescueOrder.ServiceDriverId;
            TransactionLog.ServiceVehicleId = RescueOrder.ServiceVehicleId;
            TransactionLog.Status = RescueOrder.Status;
            TransactionLog.Type = RescueOrder.Type;
            TransactionLog.UserId = Program.CurrentUser.Id;
            TransactionLog.UserName = Program.CurrentUser.FullName;
            TransactionLog.UserPosition = Program.CurrentUser.Position;
            TransactionLog.ApproverId = RescueOrder.ApproverId;
            TransactionLog.ApproverName = RescueOrder.ApproverName;
            TransactionLog.ApproverPosition = RescueOrder.ApproverPosition;
            TransactionLog.PrintCount = RescueOrder.PrintCount;
            TransactionLog.Mileage = RescueOrder.Mileage;
            RescueOrder.ListOfRODetails.ForEach(y =>
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
            RescueOrder.ListOfROMechanics.ForEach(y =>
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
        #region Validation
        Boolean Validation()
        {
            if (RescueOrder.EquipmentId == 0)
            {
                throw new Exception("Please select Equipment.");
            }
            if (RescueOrder.CustomerId == 0)
            {
                throw new Exception("Please select Customer.");
            }
            if (RescueOrder.ContractorId == 0)
            {
                throw new Exception("Please select Contractor.");
            }
            if (RescueOrder.ApproverId == 0)
            {
                throw new Exception("Please select Approver.");
            }
            if (mcbDriversName.SelectedValue != null)
            {
                //throw new Exception("Please select Driver");
            }
            if (chkJOBOUT.Checked == false)
            {
                if (RescueOrder.ServiceDriverId == 0)
                {
                    throw new Exception("Please select Service Driver.");
                }
                if (RescueOrder.ServiceVehicleId == 0)
                {
                    throw new Exception("Please select Service Vehicle.");
                }
            }
            if (RescueOrder.ListOfRODetails.Count == 0)
            {
                throw new Exception("Please input RESCUE ORDER DETAILS.");
            }
            if (RescueOrder.ListOfROMechanics.Count == 0)
            {
                throw new Exception("Please input MECHANIC DETAILS.");
            }
            if (!string.IsNullOrWhiteSpace(tbxMileage.Text))
                if (string.IsNullOrWhiteSpace(rddMetric.Text))
                    throw new Exception("Please select mileage metric!");
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
                    Validation();
                    SaveAction.Invoke();
                    RescueOrder.ListOfRODetails = new RODetailRepository().SearchBy("Where RD.ROId = " + RescueOrder.Id);
                    RescueOrder.ListOfROMechanics = new ROMechanicRepository().SearchBy("Where RM.ROId = " + RescueOrder.Id);
                    new ROTransactionLogRepository().Add(MapROTransactionLog());
                    RescueOrder.ListOfRODetails.ForEach(x =>
                    {
                        if (JOROStatus == clsEnums.JOROStatus.Approved)
                        {
                            if (new ROAuditTrailRepository().SearchBy(" Where RAT.RODetailId = " + x.Id + " AND S.IsApproval = 1").Count == 0)
                            {
                                clsROAuditTrail ROAuditTrail = new clsROAuditTrail();
                                ROAuditTrail.RODetailId = x.Id;
                                ROAuditTrail.StatusId = x.StatusId;
                                ROAuditTrail.Remarks = x.Remarks;
                                ROAuditTrail.UserId = Program.CurrentUser.Id;
                                new ROAuditTrailRepository().Add(ROAuditTrail);
                            }
                        }
                    });
                    SaveAuditTrail();
                    UpdatePPEStatus();
                    SavePPEStatusLog();
                    DisableControls();
                    ts.Complete();
                    //DataEvent.ConfirmData(RescueOrder);
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
        }

        void UpdatePPEStatus()
        {
            EquipmentURRepo = new EquipmentURRepository();
            EquipmentURRepo.Update(RescueOrder.EquipmentId);
        }
        void Add()
        {
            RORepo = new RORepository();
            RORepo.Add(RescueOrder);
            RescueOrder.Id = RORepo.ROId;
            RescueOrder.RONo = RORepo.FindByID(RescueOrder.Id).RONo;
            RescueOrder.RefYear = RORepo.FindByID(RescueOrder.Id).RefYear;
            tbRoNo.Text = RescueOrder.RONoStr;
        }
        void Edit()
        {
            RORepo = new RORepository();
            RORepo.Update(RescueOrder);
        }
        #endregion       
        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptRescueOrder report = new rptRescueOrder(MapProperties());
            frmReportViewer ReportViewerPage = new frmReportViewer(report, MapROTransactionLog()) { IsRO = true };
            ReportViewerPage.Show();
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
                RescueOrder.ApproverId = EmployeeInfo.Id;
                RescueOrder.ApproverName = EmployeeInfo.FullName;
                RescueOrder.ApproverPosition = EmployeeInfo.Position;

                tbApprover.Text = EmployeeInfo.FullName;
                tbApproverPosition.Text = EmployeeInfo.Position;
            }
        }
        #endregion       
        #region Disable Controls
        void DisableControls()
        {
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
            tbChecklistNo.Text = "CH0000";
            tbRoNo.Text = "AUTO-GENERATED";
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

            clsRODetailsBindingSource.Clear();
            clsROMechanicsBindingSource.Clear();
        }
        #endregion

        private void radBindingNavigator1AddTariffItem_Click(object sender, EventArgs e)
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

                clsRODetails CurJODet = (clsRODetails)clsRODetailsBindingSource.Current;

                TariffRepo = new TariffRepository();
                clsTariff CurTariff = TariffRepo.SearchBy(" WHERE T.JobCategoryId = " + ((clsTariff)obj).JobCategoryId + " AND T.JobTypeId = " + ((clsTariff)obj).JobTypeId + " AND T.SectionId = " + ((clsTariff)obj).SectionId + " AND T.WorkDescription = '" + ((clsTariff)obj).WorkDescription.Trim() + "'").SingleOrDefault();

                if (CurJODet == null)
                {
                    clsRODetailsBindingSource.AddNew();
                }
                CurJODet = (clsRODetails)clsRODetailsBindingSource.Current;
                if (CurJODet.TariffId != 0)
                {
                    clsRODetailsBindingSource.AddNew();
                    CurJODet = (clsRODetails)clsRODetailsBindingSource.Current;
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

                clsRODetailsBindingSource.ResetCurrentItem();
            }
        }
        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {
            EnableControls();
            RescueOrder = new clsRescueOrder();
            CRUDEMode = clsEnums.CRUDEMode.Add;
            SaveAction = Add;
            dtRODate.Value = DateTime.Now;
            DataEvent = new clsDataEvent();
            clsRODetailsBindingSource.DataSource = RescueOrder.ListOfRODetails;
            clsROMechanicsBindingSource.DataSource = RescueOrder.ListOfROMechanics;
            ClearProperties();
        }

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
                UsersLog.ActivityType = 5;
                UsersLog.DayActivity = "Add Rescue Order " + "(" + RescueOrder.RONoStr + " - " + tbEquipmentCode.Text + " - " + tbContractorName.Text + ")";
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
                UsersLog.ActivityType = 6;
                UsersLog.DayActivity = "Update Rescue Order " + "(" + RescueOrder.RONoStr + " - " + tbEquipmentCode.Text + " - " + tbContractorName.Text + ")";
            }
            return UsersLog;
        }
        void SavePPEStatusLog()
        {
            PPEStatusLogRepo = new PPEStatusLogRepository();
            PPEStatusLogRepo.Add(PPEStatusLogMapProperties());
        }
        clsPPEStatusLog PPEStatusLogMapProperties()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                PPEStatusLog.EquipmentId = RescueOrder.EquipmentId;
                PPEStatusLog.PPETypeId = RescueOrder.PPETypeId;
                PPEStatusLog.PPEClassId = RescueOrder.PPEClassId;
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
        private bool IsEquipmentReleased(clsEquipment _equipment)
        {
            clsEquipment equipment = new clsEquipment();
            equipment = new EquipmentReleasalRepository().FindEquipmentIfReleasedRO(_equipment.EquipmentMasterlistId, _equipment.EquipmentType);
            if (equipment != null)
            {
                return true;
            }
            return false;
        }

        private void BtnClearFields_Click(object sender, EventArgs e)
        {
            tbCustomerType.Clear();
            tbCustomerName.Clear();
            tbCustomerAddress.Clear();
            tbCustomerRemarks.Clear();
            tbEquipmentCode.Clear();
            tbEquipmentName.Clear();
            mcbDriversName.Text = "";
            tbLocation.Clear();
            tbEquipmentRemarks.Clear();
            tbContractorName.Clear();
            tbContractorSectionHead.Clear();
            tbServiceDriver.Clear();
            tbServiceVehicle.Clear();
            tbApprover.Clear();
            tbApproverPosition.Clear();
        }
    }
}
