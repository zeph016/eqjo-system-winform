using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
namespace FGCIJOROSystem.Presentation.vChecklist
{
    public partial class frmChecklistPrintLogs : Telerik.WinControls.UI.RadForm
    {
        public clsDataEvent DataEvent;
        ChecklistGeneratorRepository ChecklistGeneratorRepo;
        public frmChecklistPrintLogs()
        {
            InitializeComponent();
        }
        void load()
        {
            ChecklistGeneratorRepo = new ChecklistGeneratorRepository();
            clsChecklistGeneratorBindingSource.DataSource = ChecklistGeneratorRepo.GetAll();
        }

        private void frmChecklistPrintLogs_Load(object sender, EventArgs e)
        {
            load();
        }
    }
}
