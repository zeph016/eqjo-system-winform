using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain;
using FGCIJOROSystem.Domain.Configurations.EquipmentType;
using System.Transactions;
using FGCIJOROSystem.Domain.Configurations.SectionJORO;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Presentation.vConfiguration.vSection;
namespace FGCIJOROSystem.Presentation.vChecklist
{
    public partial class ucChecklist : UserControl
    {
        public ucChecklist()
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            Checklist = new clsChecklist();
            DeleteChecklistDet = new List<clsChecklistDetails>();

        }
        public ucChecklist(clsEquipmentType obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new clsDataEvent();
            Checklist = new clsChecklist();
            Checklist.EquipmentTypeId = obj.EquipmentTypeId;
            Checklist.EquipmentType = obj.PPETypeName;
            Checklist.Description = obj.Description;
            Checklist.Id = obj.Id;
            DeleteChecklistDet = new List<clsChecklistDetails>();
            DisplayProperties();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;       
        }
        #region Properties
        public clsDataEvent DataEvent;
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        Action SaveAction;
        SectionJORORepository SectionJoroRepo;
        ChecklistItemRepository ChecklistItemRepo;
        ChecklistRepository ChecklistRepo;
        ChecklistDetailsRepository ChecklistDetailsRepo;
        public Int64 EquipmentTypeId;
        clsChecklist Checklist;
        List<clsChecklistItem> ariellist;
        List<clsChecklistDetails> DeleteChecklistDet;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        #endregion
        #region Method
        private void ucChecklist_Load(object sender, EventArgs e)
        {
            load();
            ChecklistDetailsRepo = new ChecklistDetailsRepository();
            Checklist.ListOfChecklistDetails = ChecklistDetailsRepo.SearchBy(" WHERE ChecklistId = " + Checklist.Id);
            clsChecklistDetailsBindingSource.DataSource = Checklist.ListOfChecklistDetails;
            DisplayChecklistsDetails();

            this.btnCancel.ButtonElement.ToolTipText = "Cancel";
            this.btnSave.ButtonElement.ToolTipText = "Save";
            this.btnAddChecklistItem.ButtonElement.ToolTipText = "Add Checklist Item";
            this.btnRefresh.ButtonElement.ToolTipText = "Refresh";
            this.btnAddSection.ButtonElement.ToolTipText = "Add Section";
        }
        #endregion
        #region LoadChecklistItem
        void load()
        {
            ariellist = new List<clsChecklistItem>();
            {
                //Load all Checklist Item
                ChecklistItemRepo = new ChecklistItemRepository();
                GridViewComboBoxColumn Checklist = (GridViewComboBoxColumn)dgvChecklistDetails.Columns["Checklist"];

                Checklist.DropDownStyle = RadDropDownStyle.DropDown;
                Checklist.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                BackgroundWorker bg = new BackgroundWorker();
                bg.DoWork += (s, e) =>
                {
                    e.Result = ChecklistItemRepo.GetAll();
                    
                };
                bg.RunWorkerCompleted += (s, e) =>
                {
                    clsChecklistItemBindingSource.DataSource = (List<clsChecklistItem>)e.Result;
                };
                Checklist.DataSource = clsChecklistItemBindingSource;
                Checklist.DisplayMember = "Name";
                Checklist.ValueMember = "Id";
                bg.RunWorkerAsync();
                }

                //Load all Sections
                SectionJoroRepo = new SectionJORORepository();
                GridViewComboBoxColumn Section = (GridViewComboBoxColumn)dgvChecklistDetails.Columns["Section"];

                Section.DropDownStyle = RadDropDownStyle.DropDown;
                Section.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += (s, e) =>
                {
                    e.Result = SectionJoroRepo.GetAll();
                };
                bgw.RunWorkerCompleted += (s, e) =>
                {
                    clsSectionsBindingSource.DataSource = (List<clsSectionJORO>)e.Result;
                };
                Section.DataSource = clsSectionsBindingSource;
                Section.DisplayMember = "SectionName";
                Section.ValueMember = "Id";
                bgw.RunWorkerAsync();
        }
        #endregion
        #region AddNewItem
        private void radBindingNavigator5AddNewItem_Click(object sender, EventArgs e)
        {
            clsChecklistDetailsBindingSource.AddNew();
            load();
        }
        #endregion
        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            Save();
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
                currUser.DayActivity = "Add Checklist (" + tbPPETypeName.Text + ")";
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
                currUser.DayActivity = "Update Checklist (" + tbPPETypeName.Text + ")";
            }
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        void Save()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    ChecklistRepo = new ChecklistRepository();
                    ChecklistRepo.Add(MapProperties());
                    Checklist.Id = new ChecklistRepository().ChecklistId;
                    ChecklistDetailsRepo = new ChecklistDetailsRepository();
                    DeleteChecklistDet.ForEach(x => 
                    {
                        ChecklistDetailsRepo.Delete(x);
                    });
                    
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been saved successfully!"
                    };
                    MsgBox.ShowDialog();
                    ts.Complete();
                    dgvChecklistDetails.Enabled = false;
                }
                catch (Exception)
                {
                    ts.Dispose();
                    //throw;
                }
            }
        }
        void DisplayProperties()
        {
            
            tbPPETypeName.Text = Checklist.EquipmentType;
            tbDescription.Text = Checklist.Description;
        }
        clsChecklist MapProperties()
        {
            Checklist.EquipmentType = tbPPETypeName.Text;
            Checklist.Description = tbDescription.Text;
            Checklist.ListOfChecklistDetails = (List<clsChecklistDetails>)clsChecklistDetailsBindingSource.List;
            return Checklist;
        }
        #endregion
        #region ClearProperties
        void ClearProperties()
        {
            tbDescription.Text = "";
            tbPPETypeName.Text = "";
            clsChecklistDetailsBindingSource.Clear();
        }

        #endregion

        private void dgvChecklistDetails_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            clsChecklistDetails CurrentDetail = (clsChecklistDetails)clsChecklistDetailsBindingSource.Current;

            GridViewComboBoxColumn SectionId = (GridViewComboBoxColumn)dgvChecklistDetails.Columns["Section"];
            GridViewComboBoxColumn ChecklistItemId = (GridViewComboBoxColumn)dgvChecklistDetails.Columns["Checklist"];
            if (clsSectionsBindingSource.Current != null)
            {
                CurrentDetail.SectionId = ((clsSectionJORO)clsSectionsBindingSource.Current).Id;
            }
            if (clsChecklistItemBindingSource.Current != null)
            {
                CurrentDetail.ChecklistItemId = ((clsChecklistItem)clsChecklistItemBindingSource.Current).Id;
            }            
                clsChecklistDetailsBindingSource.ResetCurrentItem();
        }
        void DisplayChecklistsDetails()
        {
            foreach (var Checklists in Checklist.ListOfChecklistDetails)
            {
                if (dgvChecklistDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dgvChecklistDetails.ChildRows.Count; i++)
                    {
                        if ((Int64)dgvChecklistDetails.Rows[i].Cells["Id"].Value == Checklists.Id)
                        {
                            dgvChecklistDetails.Rows[i].Cells["Section"].Value = Checklists.SectionId;
                            dgvChecklistDetails.Rows[i].Cells["Checklist"].Value = Checklists.ChecklistItemId;
                        }
                    }
                }
            }
        }

        private void btnAddChecklistItem_Click(object sender, EventArgs e)
        {
            frmChecklistItem EntryPage = new frmChecklistItem();
            EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            EntryPage.ShowDialog();
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                clsChecklistItemBindingSource.Add((ChecklistItemRepository)obj);
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                clsChecklistItemBindingSource.ResetCurrentItem();
            }
        }

        private void radBindingNavigator5DeleteItem_Click(object sender, EventArgs e)
        {
            if (((clsChecklistDetails)clsChecklistDetailsBindingSource.Current).Id != 0)
	        {
                clsChecklistDetails CurChkDet = (clsChecklistDetails)clsChecklistDetailsBindingSource.Current;
                CurChkDet.IsActive = false;
                clsChecklistDetailsBindingSource.ResetCurrentItem();
	        }
            else
            {
                clsChecklistDetailsBindingSource.RemoveCurrent();
            }
            //
            Save();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            load();
        }

        private void btnAddSection_Click(object sender, EventArgs e)
        {
            frmSection Sec = new frmSection();
            Sec.ShowDialog();
        }
       
    }
}