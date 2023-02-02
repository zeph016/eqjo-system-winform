using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace FGCIJOROSystem.Presentation.vLookups
{
    public partial class frmPersonnelLookup : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        public clsDataEvent DataEvent;
        PersonnelRepository PersonnelRepo;
        #endregion
        public frmPersonnelLookup()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();            
        }

        private void frmPersonnelLookup_Load(object sender, EventArgs e)
        {
           load();
           dgvEmployee.AutoExpandGroups = true;
        }

        void load()
        {
            BackgroundWorker bg = new BackgroundWorker();
            PersonnelRepo = new PersonnelRepository();
            bg.DoWork += (s, e) =>
            {
                e.Result = PersonnelRepo.SearchBy(" WHERE P.Active = 1");
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsPersonnelsBindingSource.DataSource = (List<clsPersonnels>)e.Result;
            };
            bg.RunWorkerAsync();
        }
        private void dgvEmployee_CommandCellClick(object sender, EventArgs e)
        {
            if (clsPersonnelsBindingSource.Current != null)
            {
                DataEvent.ConfirmData((clsPersonnels)clsPersonnelsBindingSource.Current);
                this.Dispose();
            }
        }

        private void dgvEmployee_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                dgvEmployee.Grid_CellFormatting(sender, e, "Select");
            }
        }
    }
}
