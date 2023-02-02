using FGCIJOROSystem.DAL.Repositories.JORepo;
using FGCIJOROSystem.DAL.Repositories.RORepo;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.RescueOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vJORODirectPrint
{
    public partial class frmDirectPrinting : Telerik.WinControls.UI.RadForm
    {
        public Telerik.Reporting.ReportSource rptSource { get; set; }
        clsJOTransactionLogs JOTransLog;
        clsROTransactionLogs ROTransLog;
        JOTransactionLogRepository JOTransactionLogRepo;
        ROTransactionLogRepository ROTransactionLogRepo;
        public frmDirectPrinting()
        {
            InitializeComponent();
        }
        public frmDirectPrinting(clsJOTransactionLogs obj)
        {
            InitializeComponent();
            JOTransLog = obj;
            chkOrig.Checked = JOTransLog.PrintCount <= 1 ? true : false;
        }
        public frmDirectPrinting(clsROTransactionLogs obj)
        {
            InitializeComponent();
            ROTransLog = obj;
            chkOrig.Checked = ROTransLog.PrintCount <= 1 ? true : false;
        }
        private void frmDirectPrinting_Load(object sender, EventArgs e)
        {
            PopulateInstalledPrintersCombo();
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnPrint, "Print");

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
                rptSource.Parameters["PrintCount"].Value = 1;
                if (chkOrig.Checked)
                {
                    rptSource.Parameters["PrintCount"].Value = i <= 0 ? 0 : 1;
                }                

                //reportProcessor.PrintReport(typeReportSource, printerSettings);
                reportProcessor.PrintReport(rptSource, printerSettings);
            }            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (JOTransLog != null)
            {
                JOTransactionLogRepo = new JOTransactionLogRepository();
                JOTransLog.PrintCount = JOTransLog.PrintCount + 1;
                JOTransactionLogRepo.Add(JOTransLog);
                PrintChecklist();
            }
            if (ROTransLog != null)
            {
                ROTransactionLogRepo = new ROTransactionLogRepository();
                ROTransLog.PrintCount = ROTransLog.PrintCount + 1;
                ROTransactionLogRepo.Add(ROTransLog);
                PrintChecklist();
            }
            this.Dispose();
        }
    }
}
