using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Configurations.Contractors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using System.Linq;
namespace FGCIJOROSystem.Presentation.vLookups
{
    public partial class frmExternalContratorLookup : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        public clsDataEvent DataEvent;

        IEContractorRepository IEContractorRepo = new IEContractorRepository();
        #endregion
        #region Methods
        public frmExternalContratorLookup()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
        }

        private void frmExternalContratorLookup_Load(object sender, EventArgs e)
        {
            tbSearch.Focus();
            load();
            //radGridView1.EnableCustomFiltering = true;
        }
        void load()
        {
            BackgroundWorker bg = new BackgroundWorker();
            //List<clsContractor> Lists = new List<clsContractor>();
            bg.DoWork += (s, e) =>
            {
                e.Result = IEContractorRepo.SearchBy();
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsContractorBindingSource.DataSource = (List<clsContractor>)e.Result;
            };
            bg.RunWorkerAsync();
        }
        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                DataEvent.ConfirmData((clsContractor)clsContractorBindingSource.Current);
                this.Dispose();
            }
        }
        #endregion

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Select");
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (clsContractorBindingSource.List.Count > 0)
            {
               //radGridView1.FilterDescriptors
            }
            
        }

        private void radGridView1_CustomFiltering(object sender, GridViewCustomFilteringEventArgs e)
        {
            if (string.IsNullOrEmpty(tbSearch.Text.Trim()))
            {
                e.Visible = true;
                for (int i = 0; i < radGridView1.ColumnCount; i++)
                {
                    e.Row.Cells[i].Style.Reset();
                    e.Row.InvalidateRow();
                }
            }
            e.Visible = false;
            for (int i = 0; i < radGridView1.ColumnCount; i++)
            {                
                if (e.Row.Cells[i].Value != null)
                {
                    string text = e.Row.Cells[i].Value.ToString();
                    if (text.IndexOf(tbSearch.Text, StringComparison.InvariantCultureIgnoreCase) > 0)
                    {
                        e.Row.Cells[i].Style.CustomizeFill = true;
                        e.Row.Cells[i].Style.DrawFill = true;
                        e.Row.Cells[i].Style.BackColor = Color.Azure;
                        e.Visible = false;
                    }
                    else
                    {
                        e.Row.Cells[i].Style.Reset();
                        e.Row.InvalidateRow();
                    }                       
                }
            }
        }
    }
}
