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
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain.Configurations.EquipmentType;
using FGCIJOROSystem.Domain.Global;

namespace FGCIJOROSystem.Presentation.vChecklist
{
    public partial class ucEquipmentTypeLists : UserControl
    {
        public ucEquipmentTypeLists()
        {
            InitializeComponent();
        }
        #region Properties
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        public frmMainWindow MainWindowPage;
        #endregion

        #region Method
        EquipmentTypeRepository EquipmentTypeRepo;

         private void ucEquipmentTypeLists_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
           EquipmentTypeRepo = new EquipmentTypeRepository();
           clsEquipmentTypeBindingSource.DataSource = EquipmentTypeRepo.GetAll();
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
         private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
             {
                GridCommandCellElement cell = (GridCommandCellElement)sender;
                if (cell.ColumnInfo.Name == "Equipment")
                {
                    MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                    {
                        Name = "pvEquipmentList",
                        Text = "Equipment List",
                        //Image = Properties.Resources.home
                    }, new ucEquipmentList() { EquipmentTypeId = ((clsEquipmentType)clsEquipmentTypeBindingSource.Current).EquipmentTypeId, MainWindowPage =MainWindowPage });
                }
                if (cell.ColumnInfo.Name == "ChecklistItem")
                {
                   
                }
                if (cell.ColumnInfo.Name == "Checklist")
                {
                    ucChecklist EntryPage = new ucChecklist((clsEquipmentType)clsEquipmentTypeBindingSource.Current);
                    EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
                    CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
                    //EntryPage.ShowDialog();
                    
                    MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                    {
                        Name = "pvChecklist",
                        Text = "Checklist",
                        //Image = Properties.Resources.home
                    //}, EntryPage);
                    }, new ucChecklist((clsEquipmentType)clsEquipmentTypeBindingSource.Current) { EquipmentTypeId = ((clsEquipmentType)clsEquipmentTypeBindingSource.Current).EquipmentTypeId });
                }
            }
        }        
        #endregion
          private async void radMenuItem3_Click(object sender, EventArgs e)
         {
             frmChecklistItem EntryPage = await Task.Run(() => new frmChecklistItem());
             EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
             CRUDEMode = await Task.Run(() => Domain.Enums.clsEnums.CRUDEMode.Add);
             EntryPage.ShowDialog();
         }

          private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
          {
              GridDataCellElement cell = (GridDataCellElement)sender;
              if (cell.ColumnInfo.Name == "Checklist")
              {
                  radGridView1.Grid_CellFormatting(sender, e, "Create Checklist Template");
              }
              if (cell.ColumnInfo.Name == "Equipment")
              {
                  radGridView1.Grid_CellFormatting(sender, e, "View Equipment");
              }
          }
    }        
        
}
