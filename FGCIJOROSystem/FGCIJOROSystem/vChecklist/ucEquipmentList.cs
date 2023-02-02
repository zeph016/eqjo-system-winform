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
using FGCIJOROSystem.Presentation.vChecklist;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain.Checklist;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.Domain.Configurations.EquipmentType;
using FGCIJOROSystem.Presentation.vSystem.vMain;


namespace FGCIJOROSystem.Presentation.vChecklist
{
    public partial class ucEquipmentList : UserControl
    {
        public ucEquipmentList()
        {
            InitializeComponent();
        }
        #region Properties
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        public frmMainWindow MainWindowPage;
        #endregion

        #region Method
        EquipmentRepository EquipmentRepo;
        public Int64 EquipmentTypeId;
        public Int64 EquipmentId;
 
        private void ucEquipmentTypeLists_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            EquipmentRepo = new EquipmentRepository();
            clsEquipmentBindingSource.DataSource = EquipmentRepo.SearchById(EquipmentTypeId);
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                clsEquipmentBindingSource.Add((ChecklistItemRepository)obj);
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                clsEquipmentBindingSource.ResetCurrentItem();
            }
        }
        #endregion
        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "GenerateChecklist")
            {
                ChecklistRepository ChecklistRepo = new ChecklistRepository();
                ChecklistDetailsRepository ChecklistDetails = new ChecklistDetailsRepository();
                clsChecklist Checklist = ChecklistRepo.SearchBy("where c.[EquipmentTypeId] = " + EquipmentTypeId.ToString()).FirstOrDefault();
                Int64 ChecklistId = Checklist == null ? 0 : Checklist.Id;
                if (ChecklistId != 0)
                {
                    if (ChecklistDetails.SearchBy("Where cd.[ChecklistId] = "+ ChecklistId.ToString()).Count == 0)
                    {
                        MessageBox.Show("No Details has been made.");
                    }
                    else
                    {
                        MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                        {
                            Name = "pvChecklistGenerator",
                            Text = "Checklist Generator",
                        }, new ucChecklistGenerator(EquipmentTypeId) { Equipment = (clsEquipment)clsEquipmentBindingSource.Current });
                    }
                } 
                else
                {
                    MessageBox.Show("No Details has been made.");
                } 
            }

            if (cell.ColumnInfo.Name == "PrintLogs")
            {
                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvChecklistTransactionList",
                    Text = "Checklist Transaction List",
                }, new ucChecklistTransactionList() { Equipment = (clsEquipment)clsEquipmentBindingSource.Current,MainWindowPage = MainWindowPage });
            }
        }

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "GenerateChecklist")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Generate Checklist");
            }
            if (cell.ColumnInfo.Name == "PrintLogs")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Print Logs");
            }
        }
    }
}