using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Common;
using Telerik.WinControls.UI;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Reports.rChecklist;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Configurations.EquipmentType;
using FGCIJOROSystem.Domain.Checklist;
using FGCIJOROSystem.DAL.Repositories.Checklist;
using Telerik.WinControls;
using System.Transactions;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;

namespace FGCIJOROSystem.Presentation.vChecklist
{
    public partial class ucChecklistGenerator : UserControl
    {
        public clsDataEvent DataEvent;
        public clsEquipment Equipment { get; set; }
        public frmMainWindow MainWindowPage;
        public Int64 EquipmentTypeId { get; set; }
        public Int64 ChecklistGeneratorId { get; set; }
        public clsChecklistGenerator ChecklistGenerator { get; set; }
        public clsChecklistTransaction ChecklistGeneratorDetails { get; set; }
        clsChecklist curChecklist { get; set; }
        //ChecklistRepository ChecklistRepo;
        //ChecklistDetailsRepository ChecklistDetailsRepo;
        ChecklistTransactionRepository ChecklistTransactionRepo;
        ChecklistGeneratorRepository ChecklistGeneratorRepo;
        clsChecklistTransaction ChecklistTransaction;
        public FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        public ucChecklistGenerator(clsChecklistGenerator obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new clsDataEvent();
            ChecklistGenerator = obj;
            Equipment = new clsEquipment();
            curChecklist = new clsChecklist();
        }
        public ucChecklistGenerator(Int64 obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new clsDataEvent();
            ChecklistTransaction = new clsChecklistTransaction();
            ChecklistGenerator = new clsChecklistGenerator();
            curChecklist = new clsChecklist();
            EquipmentTypeId = obj;
            ChecklistGeneratorId = obj;
        }
        private void mnuGenaerateChecklist_Click(object sender, EventArgs e)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                ChecklistPrint Checklist = new ChecklistPrint(Equipment.EquipmentMasterlistId) { rptSource = reportChecklistGenerator.ReportSource };
                Checklist.ShowDialog();
            }
            else
            {
                ChecklistPrint Checklist = new ChecklistPrint(ChecklistGenerator.Id) { rptSource = reportChecklistGenerator.ReportSource };
                Checklist.ShowDialog();
            }
        }
         void loadPrint()
        {

            rptChecklist report = new rptChecklist(ChecklistMapProperties());
            var rptSource = new Telerik.Reporting.InstanceReportSource() 
            { 
                ReportDocument = report            
            };
            reportChecklistGenerator.ReportSource = rptSource;
            reportChecklistGenerator.RefreshReport();            
        }
        //clsChecklist MapProperties()
        //{
        //    clsChecklist curChecklist = new clsChecklist();
        //    ChecklistRepo = new ChecklistRepository();
        //    if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
        //    {
        //        curChecklist = ChecklistRepo.FindByID(Equipment.EquipmentMasterlistId);
        //    }
        //    else
        //    {
        //        curChecklist = ChecklistRepo.FindByID(ChecklistGenerator.Id);
        //    }
        //    ChecklistDetailsRepo= new ChecklistDetailsRepository();
        //    curChecklist.ListOfChecklistDetails = ChecklistDetailsRepo.SearchBy("Where cd.ChecklistId =" + curChecklist.Id.ToString());
        //    return curChecklist;
        //}
        #region LoadChecklistItem
        void load()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                ChecklistTransactionRepo = new ChecklistTransactionRepository();
                ChecklistTransaction.ListOfChecklistTransaction = ChecklistTransactionRepo.SearchBy("WHERE IsActive = 1 AND ET.Id = " + EquipmentTypeId);
                clsChecklistTransactionBindingSource.DataSource = ChecklistTransaction.ListOfChecklistTransaction;
            }
            else
            {
                ChecklistTransactionRepo = new ChecklistTransactionRepository();
                ChecklistTransaction.ListOfChecklistTransaction = ChecklistTransactionRepo.LoadDetails(" WHERE IsActive = 1 AND  genDet.ChecklistGeneratorId = " + ChecklistGeneratorId);
                clsChecklistTransactionBindingSource.DataSource = ChecklistTransaction.ListOfChecklistTransaction;
            }
            
        }

        #endregion
        void DisplayProperties()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                tbPPETypeName.Text = Equipment.EquipmentCode + " " + Equipment.EquipmentName;
                tbLocation.Text = Equipment.Location;
            }
            else
            {
                tbPPETypeName.Text = ChecklistGenerator.EquipmentCode + " " + ChecklistGenerator.EquipmentName;
                tbLocation.Text = ChecklistGenerator.Location;
                tbOdometer.Value = ChecklistGenerator.OdometerReading;
                dtReceived.Value = ChecklistGenerator.DateReceived;
                dtEncoded.Value = ChecklistGenerator.DateEncoded;
                dtCompleted.Value = ChecklistGenerator.DateCompleted;
                clsChecklistTransactionBindingSource.DataSource = ChecklistTransaction.ListOfChecklistTransaction;
            }
            tbChecklistNo.Text = ChecklistGenerator.ChecklistNoStr.ToString();
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
           dgvChecklistDetails.EndEdit();
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        ChecklistGeneratorRepo = new ChecklistGeneratorRepository();
                        ChecklistGeneratorRepo.Add(ChecklistMapProperties());
                        ChecklistGenerator.Id = new ChecklistGeneratorRepository().EquipmentId;

                        //DataEvent.ConfirmData(ChecklistMapProperties());
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Success,
                            Message = "The record has been saved successfully!"
                        };
                        MsgBox.ShowDialog();
                        ts.Complete();
                    }
                    catch (Exception ex)
                    {
                        ts.Dispose();
                        throw new Exception(ex.Message);
                    }
                }
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)                
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        try
                        {
                            ChecklistGeneratorRepo = new ChecklistGeneratorRepository();
                            ChecklistGeneratorRepo.Add(ChecklistMapProperties());
                            ChecklistGenerator.Id = new ChecklistGeneratorRepository().ChecklistGeneratorId;

                            //DataEvent.ConfirmData(ChecklistMapProperties());
                            RadMessageBox.Show("Record has been saved successfully.");
                            ts.Complete();
                        }
                        catch (Exception ex)
                        {
                            ts.Dispose();
                            throw new Exception(ex.Message);
                        }
                    }
                }
            loadPrint();
            //mnuGenaerateChecklist.Enabled = true;
        }
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
                currUser.TimeLogin = System.DateTime.Now;
                currUser.TimeLogout = System.DateTime.Now;
                currUser.OnlineUser = false;
                currUser.DayActivity = "Add Checklist Generator (" + tbPPETypeName.Text + ")";
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                currUser.Username = Program.CurrentUser.UserName;
                currUser.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
                currUser.EmpName = Program.CurrentUser.FullName;
                currUser.BranchId = Program.CurrentUser.BranchId;
                currUser.UserLevelId = Program.CurrentUser.UserLevelId;
                currUser.ComputerName = System.Environment.MachineName;
                currUser.TimeLogin = System.DateTime.Now;
                currUser.TimeLogout = System.DateTime.Now;
                currUser.OnlineUser = false;
                currUser.DayActivity = "Update Checklist Generator (" + tbPPETypeName.Text + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        clsChecklistGenerator ChecklistMapProperties()
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                tbPPETypeName.Text = Equipment.EquipmentCode + " " + Equipment.EquipmentName;
                tbLocation.Text = Equipment.Location;
                ChecklistGenerator.EquipmentId = Equipment.EquipmentMasterlistId;
            }
            else
            {
                ChecklistGenerator.EquipmentId = ChecklistGenerator.EquipmentId;
               
            }
            ChecklistGenerator.ChecklistNo.ToString(tbChecklistNo.Text);
            ChecklistGenerator.EquipmentName = tbPPETypeName.Text;
            ChecklistGenerator.Location = tbLocation.Text;
            ChecklistGenerator.OdometerReading = tbOdometer.Value;
            ChecklistGenerator.DateReceived = dtReceived.Value;
            ChecklistGenerator.DateEncoded = dtEncoded.Value;
            ChecklistGenerator.DateCompleted = dtCompleted.Value;
            ChecklistGenerator.ListOfChecklistTransaction = (List<clsChecklistTransaction>)clsChecklistTransactionBindingSource.List;
            ChecklistGenerator.ListOfChecklistTransaction.ForEach(x => x.ChecklistGeneratorId = ChecklistGeneratorId);
            var d = ChecklistGenerator.ListOfChecklistTransaction.Where(y => y.OutgoingStatus == true).ToList();
            ChecklistGenerator.PreparedBy = Program.CurrentUser.FullName;
            ChecklistGenerator.PreparedByPos = Program.CurrentUser.Position;
            return ChecklistGenerator;

        }
        private void ucChecklistGenerator_Load(object sender, EventArgs e)
        {
            load();
            DisplayProperties();
            loadPrint();

            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                mnuGenaerateChecklist.Visibility = ElementVisibility.Hidden;
            }
            else
            {
                mnuGenaerateChecklist.Visibility = ElementVisibility.Visible;

            }
        }

        private void dgvChecklistDetails_Click(object sender, EventArgs e)
        {

        }
    }
}