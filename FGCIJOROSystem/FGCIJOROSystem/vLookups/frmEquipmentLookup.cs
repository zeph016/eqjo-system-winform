using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.EquipHistoryRepo;
using FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.EquipmentHistory;
using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace FGCIJOROSystem.Presentation.vLookups
{
    public partial class frmEquipmentLookup : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        public clsDataEvent DataEvent;

        EquipmentRepository EquipmentRepo;
        EquipHistoryRepository EquipHistoryRepo;
        StatusRepository StatusRepo;

        public Boolean EquipmentHistoryPanel { get; set; }
        public Boolean ShowAll { get; set; }

        Int64 Types;
        long equipmentId;
        #endregion
        #region Methods
        public frmEquipmentLookup()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
        }
        private void frmEquipmentLookup_Load(object sender, EventArgs e)
        {
            ShowEquipmentHistory();
            if (ShowAll == true)
            {
                load();
            }
            else
            {
                loadByEquipments();
            }            
        }
        void load()
        {
            BackgroundWorker bg = new BackgroundWorker();
            EquipmentRepo = new EquipmentRepository();
            bg.DoWork += (s, e) =>
            {
                e.Result = EquipmentRepo.GetAll();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsEquipmentBindingSource.DataSource = (List<clsEquipment>)e.Result;
            };
            bg.RunWorkerAsync();
        }
        void loadByEquipments()
        {
            BackgroundWorker bg = new BackgroundWorker();
            EquipmentRepo = new EquipmentRepository();
            bg.DoWork += (s, e) =>
            {
                e.Result = EquipmentRepo.GetByEquipments();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsEquipmentBindingSource.DataSource = (List<clsEquipment>)e.Result;
            };
            bg.RunWorkerAsync();
        }
        private void MasterTemplate_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                DataEvent.ConfirmData((clsEquipment)clsEquipmentBindingSource.Current);
                this.Dispose();
            }
        }
        void ShowEquipmentHistory()
        {
            if (EquipmentHistoryPanel == true)
            {                
                //loadEquipmentHistory();
            }
            else
            {
                radSplitContainer1.SplitPanels["splitPanel2"].Dispose();
                splitPanel1.Dock = DockStyle.Fill;  
            }
        }
        private void dgvEmployee_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (EquipmentHistoryPanel == true)
            {
                loadEquipmentHistory(false, 0);
            }            
        }
        void loadEquipmentHistory(Boolean IsAll, Int64 Status)
        {
            EquipHistoryRepo = new EquipHistoryRepository();

            clsEquipmentHistoryBindingSource.DataSource = EquipHistoryRepo.Searchby(" EquipmentId = " + ((clsEquipment)clsEquipmentBindingSource.Current).EquipmentMasterlistId + (IsAll == true ? "" : " AND IsClosure = " + Status));
            radGridView1.BestFitColumns();
        }
        #endregion        

        private void menuOP_Click(object sender, EventArgs e)
        {
            RadMenuItem item = (RadMenuItem)sender; 
            this.ddbShow.Text = item.Text;             
            if (item.Text == "ONGOING / PENDING")
            {
                loadEquipmentHistory(false, 0);
            }
            if (item.Text == "DONE / CANCELLED")
            {
                loadEquipmentHistory(false, 1);
            }
            if (item.Text == "ALL")
            {
                loadEquipmentHistory(true, 0);
            }
        }

        private void dgvEmployee_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                dgvEmployee.Grid_CellFormatting(sender, e, "Select");
            }
        }
    }
}
