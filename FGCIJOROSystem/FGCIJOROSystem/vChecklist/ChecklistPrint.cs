using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.Checklist;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using FGCIJOROSystem.Domain.Checklist;
using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Domain.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;


namespace FGCIJOROSystem.Presentation.vChecklist
{
    public partial class ChecklistPrint : Telerik.WinControls.UI.RadForm
    {
        public clsDataEvent DataEvent;
        ChecklistPrintLogsRepository ChecklistPrintLogsRepo;
        ChecklistGeneratorRepository ChecklistGeneratorRepo;

        clsChecklistGenerator ChecklistGenerator;
        clsChecklistPrintLogs ChecklistPrintLogs;
        Action SaveAction;
        UsersLogRepository UsersLogRepo;
        clsUsersLog currUser;
        public Telerik.Reporting.ReportSource rptSource { get; set; }
        FGCIJOROSystem.Domain.Enums.clsEnums.CRUDEMode CRUDEMode;

        Int64 ChecklistNo;
        Int64 EquipmentId;
        public ChecklistPrint(Int64 obj)
        {
            InitializeComponent();
            currUser = new clsUsersLog();
            DataEvent = new clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Add;
            EquipmentId = obj;

        }
       
        private void btnPrint_Click(object sender, EventArgs e)
        {
            AddCurrentUser();
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                if (ChecklistGenerator != null)
                {
                    ChecklistGeneratorRepo = new ChecklistGeneratorRepository();
                    ChecklistGenerator.PrintCount = ChecklistGenerator.PrintCount + 1;
                    ChecklistGeneratorRepo.Add(ChecklistGenerator);
                    PrintChecklist();
                }
                else if (ChecklistGenerator != null)
                {
                    ChecklistGeneratorRepo = new ChecklistGeneratorRepository();
                    ChecklistGenerator.PrintCount = ChecklistGenerator.PrintCount + 1;
                    ChecklistGeneratorRepo.Add(ChecklistGenerator);
                    PrintChecklist();
                }
            }
            else if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                if (ChecklistGenerator != null)
                {
                    ChecklistGeneratorRepo = new ChecklistGeneratorRepository();
                    ChecklistGenerator.PrintCount = ChecklistGenerator.PrintCount + 1;
                    ChecklistGeneratorRepo.Add(ChecklistGenerator);
                    PrintChecklist();
                }
                else if (ChecklistGenerator != null)
                {
                    ChecklistGeneratorRepo = new ChecklistGeneratorRepository();
                    ChecklistGenerator.PrintCount = ChecklistGenerator.PrintCount + 1;
                    ChecklistGeneratorRepo.Add(ChecklistGenerator);
                    PrintChecklist();
                }
            }
        }
        clsUsersLog AddMapProperties()
        {
            currUser.Username = Program.CurrentUser.UserName;
            currUser.MLEmployeeId = Program.CurrentUser.MLEmployeeId;
            currUser.EmpName = Program.CurrentUser.FullName;
            currUser.BranchId = Program.CurrentUser.BranchId;
            currUser.UserLevelId = Program.CurrentUser.UserLevelId;
            currUser.ComputerName = System.Environment.MachineName;
            currUser.TimeLogin = System.DateTime.Now;
            currUser.TimeLogout = System.DateTime.Now;
            currUser.OnlineUser = false;
            currUser.DayActivity = "Print Checklist";
            return currUser;
        }
        void AddCurrentUser()
        {
            UsersLogRepo = new UsersLogRepository();
            UsersLogRepo.Add(AddMapProperties());
        }
        public ChecklistPrint(clsChecklistGenerator obj)
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            CRUDEMode = Domain.Enums.clsEnums.CRUDEMode.Edit;
            ChecklistGenerator = obj;
        }
        clsChecklistPrintLogs MapProperties()
        {
            ChecklistGenerator.ChecklistNo = ChecklistPrintLogs.ChecklistNo;
            ChecklistGenerator.DateEncoded = ChecklistPrintLogs.DateEncoded;
            return ChecklistPrintLogs;
        }
        void Add()
        {
            ChecklistPrintLogsRepo = new ChecklistPrintLogsRepository();
            ChecklistPrintLogsRepo.Add(MapProperties());
        }
        private void ChecklistPrint_Load(object sender, EventArgs e)
        {
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Add)
            {
                ChecklistGenerator = new clsChecklistGenerator();
                SaveAction = Add;
            }
            if (CRUDEMode == Domain.Enums.clsEnums.CRUDEMode.Edit)
            {
                //SaveAction = Edit;
            }
            this.BringToFront();

            PopulateInstalledPrintersCombo();
        }
        private void PopulateInstalledPrintersCombo()
        {
            // Add list of installed printers found to the combo box.
            // The pkInstalledPrinters string will be used to provide the display string.
            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                cbPrinterOption.Items.Add(pkInstalledPrinters);
            }
            // LocalPrintServer.GetDefaultPrintQueue().FullName;
            PrinterSettings printerName = new PrinterSettings();
            string defaultPrinter;
            defaultPrinter = printerName.PrinterName;

            int Index = cbPrinterOption.FindStringExact(defaultPrinter);
            cbPrinterOption.SelectedIndex = Index;
        }

        private void PrintChecklist()
        {
            // Obtain the settings of the default printer
            System.Drawing.Printing.PrinterSettings printerSettings
                = new System.Drawing.Printing.PrinterSettings();

            printerSettings.PrinterName = cbPrinterOption.Text;

            // The standard print controller comes with no UI
            System.Drawing.Printing.PrintController standardPrintController =
                new System.Drawing.Printing.StandardPrintController();

            // Print the report using the custom print controller
            Telerik.Reporting.Processing.ReportProcessor reportProcessor
                = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;
            for (int i = 0; i < radSpinEditor1.Value; i++)
            {
                //reportProcessor.PrintReport(typeReportSource, printerSettings);
                reportProcessor.PrintReport(rptSource, printerSettings);
            }            

            //reportProcessor.PrintReport(typeReportSource, printerSettings);
            //reportProcessor.PrintReport(rptSource, printerSettings);

            this.Close();
        }

        
    }
}
