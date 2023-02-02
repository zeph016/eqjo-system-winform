using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Presentation.vSystem.vLogin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Threading.Tasks;
using System.Threading;

namespace FGCIJOROSystem.Presentation.vLookups
{
    public partial class frmEmployeeLookup : Telerik.WinControls.UI.RadForm
    {       
        public clsDataEvent DataEvent; 
        public frmEmployeeLookup()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
        }

        public frmEmployeeLookup(FGCIJOROSystem.Domain.Enums.clsEnums.LookupMode lookUpMode)
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();

            switch (lookUpMode)
            {
                case FGCIJOROSystem.Domain.Enums.clsEnums.LookupMode.Employee:
                    pvEmployee.Visible = true;
                    pvNonEmployee.Visible = false;
                    pvDepartment.Visible = false;
                    pvSection.Visible = false;
                    pvEquipment.Visible = false;
                    pvProject.Visible = false;

                    this.pvLookups.SelectedPage = pvEmployee;
                    break;
                case FGCIJOROSystem.Domain.Enums.clsEnums.LookupMode.NonEmployee:
                    pvEmployee.Visible = false;
                    pvNonEmployee.Visible = true;
                    pvDepartment.Visible = false;
                    pvSection.Visible = false;
                    pvEquipment.Visible = false;
                    pvProject.Visible = false;

                    this.pvLookups.SelectedPage = pvNonEmployee;
                    break;
                case FGCIJOROSystem.Domain.Enums.clsEnums.LookupMode.Department:
                    pvEmployee.Visible = false;
                    pvNonEmployee.Visible = false;
                    pvDepartment.Visible = true;
                    pvSection.Visible = false;
                    pvEquipment.Visible = false;
                    pvProject.Visible = false;

                    this.pvLookups.SelectedPage = pvDepartment;
                    break;
                case FGCIJOROSystem.Domain.Enums.clsEnums.LookupMode.Section:
                    pvEmployee.Visible = false;
                    pvNonEmployee.Visible = false;
                    pvDepartment.Visible = false;
                    pvSection.Visible = true;
                    pvEquipment.Visible = false;
                    pvProject.Visible = false;

                    this.pvLookups.SelectedPage = pvSection;
                    break;
                case FGCIJOROSystem.Domain.Enums.clsEnums.LookupMode.Equipment:
                    pvEmployee.Visible = false;
                    pvNonEmployee.Visible = false;
                    pvDepartment.Visible = false;
                    pvSection.Visible = false;
                    pvEquipment.Visible = true;
                    pvProject.Visible = false;

                    this.pvLookups.SelectedPage = pvEquipment;
                    break;
                case FGCIJOROSystem.Domain.Enums.clsEnums.LookupMode.Project:
                    pvEmployee.Visible = false;
                    pvNonEmployee.Visible = false;
                    pvDepartment.Visible = false;
                    pvSection.Visible = false;
                    pvEquipment.Visible = false;
                    pvProject.Visible = true;

                    this.pvLookups.SelectedPage = pvProject;
                    break;
                case FGCIJOROSystem.Domain.Enums.clsEnums.LookupMode.All:
                    pvEmployee.Visible = true;
                    pvNonEmployee.Visible = true;
                    pvDepartment.Visible = true;
                    pvSection.Visible = true;
                    pvEquipment.Visible = true;
                    pvProject.Visible = true;

                    this.pvLookups.SelectedPage = pvEmployee;
                    break;
                default:
                    break;
            }
        }

        #region Properties
        List<clsEmployee> ListOfEmployee;
        List<clsNonEmployee> ListOfNonEmployee;
        List<clsDepartment> ListOfDepartment;
        List<clsSection> ListOfSection;
        List<clsEquipment> ListOfEquipment;
        List<clsProject> ListOfProject;
        #endregion

        #region Get Employee
        private async void GetEmployee()
        {
            ListOfEmployee = new List<clsEmployee>();
            ListOfEmployee = await Task.Run(() => new EmployeeRepository().SearchBy(tbSearch.Text)); //async
            clsEmployeeBindingSource.DataSource = ListOfEmployee;
            dgvEmployee.BestFitColumns();
        }
        #endregion

        #region Get Non-Employee
        private async void GetNonEmployee()
        {
            ListOfNonEmployee = new List<clsNonEmployee>();
            ListOfNonEmployee = await Task.Run(() => new NonEmployeeRepository().SearchBy(tbSearch.Text)); //async
            clsNonEmployeeBindingSource.DataSource = ListOfNonEmployee;
            dgvNonEmployee.BestFitColumns();
        }
        #endregion

        #region Get Department
        private async void GetDepartment()
        {
            ListOfDepartment = new List<clsDepartment>();
            ListOfDepartment = await Task.Run(() => new DepartmentRepository().SearchBy(tbSearch.Text)); //async
            clsDepartmentBindingSource.DataSource = ListOfDepartment;
            dgvDepartment.BestFitColumns();
        }
        #endregion

        #region Get Section
        private async void GetSection()
        {
            ListOfSection = new List<clsSection>();
            ListOfSection = await Task.Run(() => new SectionRepository().SearchBy(tbSearch.Text)); //async
            clsSectionBindingSource.DataSource = ListOfSection;
            dgvSection.BestFitColumns();
        }
        #endregion

        #region Get Equipment
        private async void GetEquipment()
        {
            ListOfEquipment = new List<clsEquipment>();
            ListOfEquipment = await Task.Run(() => new EquipmentRepository().SearchBy(tbSearch.Text)); //async
            clsEquipmentBindingSource.DataSource = ListOfEquipment;
            dgvEquipment.BestFitColumns();
        }
        #endregion

        #region Get Project
        private async void GetProject()
        {
            ListOfProject = new List<clsProject>();
            ListOfProject = await Task.Run(() => new ProjectRepository().SearchBy(tbSearch.Text)); //async
            clsProjectBindingSource.DataSource = ListOfProject;
            dgvProject.BestFitColumns();
        }
        #endregion

        #region Page Change
        private void PageChange()
        {
            if (this.pvLookups.SelectedPage == pvEmployee)
            {
                GetEmployee();
            }
            if (this.pvLookups.SelectedPage == pvNonEmployee)
            {
                GetNonEmployee();
            }
            if (this.pvLookups.SelectedPage == pvDepartment)
            {
                GetDepartment();
            }
            if (this.pvLookups.SelectedPage == pvSection)
            {
                GetSection();
            }
            if (this.pvLookups.SelectedPage == pvEquipment)
            {
                GetEquipment();
            }
            if (this.pvLookups.SelectedPage == pvProject)
            {
                GetProject();
            }
        }
        #endregion
        private void frmEmployeeLookup_Load(object sender, EventArgs e)
        {
            PageChange();
            _changeDateTimeFormat();
            tbSearch.Focus();

            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnSearch, "Search Employee in Masterlist");
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            PageChange();
            
          
        }

        private void pvLookups_SelectedPageChanged(object sender, EventArgs e)
        {
            PageChange();
        }


        void Commit()
        {

            if (this.pvLookups.SelectedPage == pvEmployee)
            {
                DataEvent.ConfirmData(clsEmployeeBindingSource.Current);
            }
            if (this.pvLookups.SelectedPage == pvNonEmployee)
            {
                
            }
            if (this.pvLookups.SelectedPage == pvDepartment)
            {
                
            }
            if (this.pvLookups.SelectedPage == pvSection)
            {
                
            }
            if (this.pvLookups.SelectedPage == pvEquipment)
            {
              
            }
            if (this.pvLookups.SelectedPage == pvProject)
            {
                
            }

        }

        private void dgvEmployee_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                Commit();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Dispose();
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
        void _changeDateTimeFormat()
        {
            foreach (GridViewDataColumn dCol in dgvEmployee.Columns)
            {
                if (dCol.DataType == typeof(DateTime))
                {
                    dCol.FormatString = "{0: MM/dd/yyyy}";
                }
            }
        }
    }
}
