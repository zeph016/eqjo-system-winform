using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;

namespace FGCIJOROSystem.Presentation.vConfiguration.vUsers
{
    public partial class ucUsers : UserControl
    {

        UserRepository _Repo = new UserRepository();
        UserRepository URepo;
        List<clsUser> List;
        clsUser Current;
        public ucUsers()
        {
            InitializeComponent();
        }

        #region Methods
        public void LoadAll() //public void LoadAll()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (s, e) =>
            {
                List = _Repo.GetAll();
            };
            bw.RunWorkerCompleted += (s, e) =>
            {
                ShowData();
            };

            bw.RunWorkerAsync();
        }

        void ShowData()
        {
            clsUserBindingSource.DataSource = List;
        }
        void Add()
        {
            clsUserBindingSource.AddNew();
            Current = (clsUser)clsUserBindingSource.Current;
            ShowEntry(clsEnums.CRUDEMode.Add);
        }

        void Edit()
        {          
            if (radGridView1.SelectedRows.Count != 0)
            { 
                Current = (clsUser)clsUserBindingSource.Current;
            }
            if (Current != null)
            {
                ShowEntry(clsEnums.CRUDEMode.Edit);
            }
            else
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Please select an entry to work with."
                };
                MsgBox.ShowDialog();
            }
        }
        void Delete()
        {
            if (radGridView1.SelectedRows.Count == 0)
            {
                frmMsg MsgBox = new frmMsg()
                {
                    MsgBox = clsEnums.MsgBox.Warning,
                    Message = "Please select an entry to work with!"
                };
                MsgBox.ShowDialog();
            }
            if (radGridView1.SelectedRows.Count != 0)
            {
                Current = (clsUser)clsUserBindingSource.Current;

                if (Current != null)
                {
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += (s, e) =>
                    {
                        URepo = new UserRepository();
                        URepo.Delete((clsUser)clsUserBindingSource.Current);
                        clsUser curItem = (clsUser)clsUserBindingSource.Current;
                        curItem.Active = false;
                        clsUserBindingSource.ResetCurrentItem();
                    };

                    bw.RunWorkerCompleted += (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            radGridView1.Refresh();
                        }
                        else
                        {
                            throw new Exception(e.Error.Message);
                        }
                    };
                    bw.RunWorkerAsync();
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been deactivated successfully!"
                    };
                    MsgBox.ShowDialog();
                }
                else
                {
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Warning,
                        Message = "Please select an entry to work with!"
                    };
                    MsgBox.ShowDialog();
                }
            }
          
        }
        void ShowEntry(clsEnums.CRUDEMode action)
        {
            var form = new frmUserEntry();
            form.CRUDEMode = action;
            form._Repo = _Repo;
            form.Current = Current;
            if (form.ShowDialog() != DialogResult.OK)
            {
                if (action == clsEnums.CRUDEMode.Add)
                {
                    clsUserBindingSource.RemoveCurrent();
                }
            }
            else
            {
                clsUserBindingSource.ResetCurrentItem();
            }
            radGridView1.Refresh();
        }
        #endregion

        #region Listeners
        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void radMenuItem2_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void radMenuItem3_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void ucUsers_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void clsUserBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (clsUserBindingSource.Current != null)
            {
                Current = (clsUser)clsUserBindingSource.Current;
            }
            else
            {
                Current = null;
            }
        }
        #endregion
       
    }
}
