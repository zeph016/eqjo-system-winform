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
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Domain.Configurations.Modules;
using System.Transactions;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;

namespace FGCIJOROSystem.Presentation.vConfiguration.vUserAccess
{
    public partial class ucUserAccess : UserControl
    {
        #region Properties
        UserAccessLevelRepository UserLevelRepo;
        ModulesRepository ModulesRepo;
        
        clsModLevelAssignment ModLevelAssignment;
        public List<clsModLevelAssignment> ListOfModLevelAssignment { get; set; }
        List<clsModLevelAssignment> RemoveUserAssignment;

        #endregion
        #region Method
        public ucUserAccess()
        {
            InitializeComponent();
            this.tvUserAccess.ExpandAll();
            ListOfModLevelAssignment = new List<clsModLevelAssignment>();
            RemoveUserAssignment = new List<clsModLevelAssignment>();
        }
        private void cboUserLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            ListOfModLevelAssignment.Clear();
            RemoveUserAssignment.Clear();           
            foreach (var y in tvUserAccess.Nodes)
            {
              y.Checked = false;
               
                foreach (var z in y.Nodes)
                {
                    z.Checked = false;
                }
            }
            var a = cboUserLevel.SelectedValue;
            if (a.GetType() == typeof(Int64))
            {
                loadModuleAssignment();
            }
        }
        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    ModulesRepo = new ModulesRepository();
                    foreach (var x in ListOfModLevelAssignment)
                    {
                        if (x.Id == 0)
                        {
                            ModulesRepo.Add(x);
                        }
                        else
                        {
                            ModulesRepo.Update(x);
                        }
                        frmMsg MsgBox = new frmMsg()
                        {
                            MsgBox = clsEnums.MsgBox.Success,
                            Message = "The record has been saved successfully!"
                        };
                        MsgBox.ShowDialog();
                    }
                    foreach (var x in RemoveUserAssignment)
                    {
                        ModulesRepo.Delete(x);
                    }
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw new Exception(ex.Message);
                }
            }
                  
        }        
        #endregion
        #region Load Module Assignment
        void loadModuleAssignment()
        {
            BackgroundWorker bg = new BackgroundWorker();
            List<clsModLevelAssignment> ListOfMLA = new List<clsModLevelAssignment>();
            ModulesRepo = new ModulesRepository();
            Int64 UserLevelId = (Int64)cboUserLevel.SelectedValue;
            bg.DoWork += (s, e) => 
            {
                ListOfMLA = ModulesRepo.SearchBy("Where UserLevelId = " + UserLevelId);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                foreach (var x in ListOfMLA)
                {
                    foreach (var y in tvUserAccess.Nodes)
                    {
                        Int64 Tag1 = Convert.ToInt64(y.Tag);
                        if (x.ModuleId == Tag1)
                        {
                            y.Checked = true;
                        }
                        foreach (var z in y.Nodes)
                        {
                            Int64 Tag2 = Convert.ToInt64(z.Tag);
                            if (x.ModuleId == Tag2)
                            {
                                z.Checked = true;
                            }
                        }
                    }
                }
            };
            bg.RunWorkerAsync();
        }
        #endregion       
        #region LoadUserLevel
        private void ucUserAccess_Load(object sender, EventArgs e)
        {
            loadUserLevel();
            this.cboUserLevel.TextChanged += new System.EventHandler(this.cboUserLevel_TextChanged);
        }
        void loadUserLevel()
        {
            UserLevelRepo = new UserAccessLevelRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                e.Result = UserLevelRepo.SearchBy("");
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                cboUserLevel.DataSource = e.Result;
                cboUserLevel.DisplayMember = "UserLevelName";
                cboUserLevel.ValueMember = "Id";
                cboUserLevel.Focus();
                loadModuleAssignment();
            };
            bg.RunWorkerAsync();
        }
        #endregion
        #region tvUserAccessSelectChanged
        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            RadTreeViewElement Nodes = (RadTreeViewElement)sender;
            Int64 TagId = Convert.ToInt64(Nodes.SelectedNode.Tag);
            ModulesRepo = new ModulesRepository();
            clsModLevelAssignment ModLevAssign = new clsModLevelAssignment();
            ModLevAssign = ModulesRepo.SearchBy(" WHERE ModuleId = " + TagId + " and UserLevelId = " + (Int64)cboUserLevel.SelectedValue).FirstOrDefault();

            if (ModLevAssign != null)
            {
                cbxAllowAdd.Checked = ModLevAssign.AllowAdd;
                cbxAllowEdit.Checked = ModLevAssign.AllowEdit;
                cbxAllowDelete.Checked = ModLevAssign.AllowDelete;
                cbxAllowView.Checked = ModLevAssign.AllowView;
            }
            else
            {
                ModLevAssign = ListOfModLevelAssignment.Where(x => x.ModuleId == TagId && x.UserLevelId == (Int64)cboUserLevel.SelectedValue).FirstOrDefault();
                if (ModLevAssign != null)
                {
                    cbxAllowAdd.Checked = ModLevAssign.AllowAdd;
                    cbxAllowEdit.Checked = ModLevAssign.AllowEdit;
                    cbxAllowDelete.Checked = ModLevAssign.AllowDelete;
                    cbxAllowView.Checked = ModLevAssign.AllowView;
                }
                else
                {
                    cbxAllowAdd.Checked = false;
                    cbxAllowEdit.Checked = false;
                    cbxAllowDelete.Checked = false;
                    cbxAllowView.Checked = false;
                }
           }                
        }
        #endregion
        private void cbxAllowAdd_CheckStateChanged(object sender, EventArgs e)
        {

        }
        private void tvUserAccess_NodeCheckedChanged(object sender, TreeNodeCheckedEventArgs e)
        {
            Int64 Tag = Convert.ToInt64(e.Node.Tag);
            clsModLevelAssignment ModLevelAssignment = new clsModLevelAssignment();
            ModLevelAssignment.ModuleId = Tag;
            ModLevelAssignment.UserLevelId = (Int64)cboUserLevel.SelectedValue;
            ModLevelAssignment.AllowAdd = cbxAllowAdd.Checked;
            ModLevelAssignment.AllowDelete = cbxAllowDelete.Checked;
            ModLevelAssignment.AllowEdit = cbxAllowEdit.Checked;
            ModLevelAssignment.AllowView = cbxAllowView.Checked;
            if (e.Node.Checked == true || e.Node.CheckState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate)
            {
                ListOfModLevelAssignment.Add(ModLevelAssignment);
                RemoveUserAssignment.RemoveAll(x => x.ModuleId == Tag);
            }
            else
            {
                RemoveUserAssignment.Add(ModLevelAssignment);
                ListOfModLevelAssignment.RemoveAll(x => x.ModuleId == Tag);
            }     
        }
        #region MapProperties
        void MapProperties()
        {
            foreach (var x in tvUserAccess.CheckedNodes)
            {                
                Int64 Tag = Convert.ToInt64(x.Tag);                
                if (Tag != 0)
                {
                    ListOfModLevelAssignment.Add(new clsModLevelAssignment { ModuleId = Tag, UserLevelId = (Int64)cboUserLevel.SelectedValue });
                }                               
            }
        }
        #endregion

        private void cboUserLevel_TextChanged(object sender, EventArgs e)
        {
            cboUserLevel.ShowDropDown();
        }
        #endregion
    }
}