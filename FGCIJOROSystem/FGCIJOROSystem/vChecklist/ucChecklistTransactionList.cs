using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Checklist;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.DAL.Repositories.Checklist;

namespace FGCIJOROSystem.Presentation.vChecklist
{
    public partial class ucChecklistTransactionList : UserControl
    {
        ChecklistGeneratorRepository ChecklistGeneratorRepo;
        ChecklistTransactionRepository ChecklistTransactionRepo;
        public clsChecklistTransaction ChecklistTransaction { get; set; }

        public clsDataEvent DataEvent;
        public frmMainWindow MainWindowPage;
        public Int64 EquipmentTypeId;
        public Int64 ChecklistGeneratorId { get; set; }

        public clsEquipment Equipment { get; set; }
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;
        public ucChecklistTransactionList()
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            ChecklistTransaction = new clsChecklistTransaction();
        }
        public ucChecklistTransactionList(Int64 obj)
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            EquipmentTypeId = obj;
            ChecklistGeneratorId = obj;
        }
        void DataEvent_OnDataConfirm(object obj)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                clsChecklistGeneratorBindingSource.Add((ChecklistGeneratorRepository)obj);
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                clsChecklistGeneratorBindingSource.ResetCurrentItem();
            }
        }
        void load()
        {
            ChecklistGeneratorRepo = new ChecklistGeneratorRepository();
            clsChecklistGeneratorBindingSource.DataSource = ChecklistGeneratorRepo.SearchBy(" WHERE EquipmentId = " + Equipment.EquipmentMasterlistId);
        }

        private void ucChecklistTransactionList_Load(object sender, EventArgs e)
        {
            load();
            _changeDateTimeFormat();
        }

        private void radGridView1_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                ChecklistGeneratorId = ((clsChecklistGenerator)clsChecklistGeneratorBindingSource.Current).Id;
                ucChecklistGenerator EntryPage = new ucChecklistGenerator(ChecklistGeneratorId) { ChecklistGenerator = (clsChecklistGenerator)clsChecklistGeneratorBindingSource.Current };
                EntryPage.DataEvent.OnDataConfirm += DataEvent_OnDataConfirm;
                EntryPage.CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;

                MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
                {
                    Name = "pvChecklistGenerator",
                    Text = "Checklist Generator",
                }, EntryPage);
            }
        }
        void loadChecklistDetailTransactions()
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ChecklistTransactionRepo = new ChecklistTransactionRepository();
                ChecklistTransaction.ListOfChecklistTransaction = ChecklistTransactionRepo.LoadDetails(" WHERE genDet.ChecklistGeneratorId = " + ((clsChecklistGenerator)clsChecklistGeneratorBindingSource.Current).Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsChecklistTransactionBindingSource.DataSource = ChecklistTransaction.ListOfChecklistTransaction;
            };
            bg.RunWorkerAsync();

        }
        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (clsChecklistGeneratorBindingSource.Current != null)
            {
                loadChecklistDetailTransactions();
            }
        }
        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            if (cell.ColumnInfo.Name == "btnUpdate")
            {
                radGridView1.Grid_CellFormatting(sender, e, "Update");
            }
            
        }
        void _changeDateTimeFormat()
        {
            foreach(GridViewDataColumn dCol in radGridView1.Columns)
            {
              if (dCol.DataType == typeof(DateTime))
              {
                  dCol.FormatString = "{0: MM/dd/yyyy}";
              }
            }
        }

    }
}