using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Configurations.Customers;
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
    public partial class frmExternalCustomerLookup : Telerik.WinControls.UI.RadForm
    {
        
        #region Properties
        public clsDataEvent DataEvent;
        IECustomerRepository CustomerRepo;
        #endregion
        #region Methods
        public frmExternalCustomerLookup()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
        }
        private void frmExternalCustomerLookup_Load(object sender, EventArgs e)
        {
            List<clsCustomer> ListOfCustomer = new List<clsCustomer>();
            CustomerRepo = new IECustomerRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, Event) => 
            {
                ListOfCustomer = CustomerRepo.GetAll();
            };
            bg.RunWorkerCompleted += (s, Event) =>
            {
                clsCustomerBindingSource.DataSource = ListOfCustomer;
            };
            bg.RunWorkerAsync();
        }
        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                DataEvent.ConfirmData(clsCustomerBindingSource.Current);
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
    }
}
