using FGCIJOROSystem.DAL.Repositories.Reports;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.Reports;
using FGCIJOROSystem.Domain.RescueOrder;
using FGCIJOROSystem.Presentation.vJORODirectPrint;
using FGCIJOROSystem.Presentation.vReport.vReports;
using FGCIJOROSystem.Reports.rReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vReport.vForms
{
    public partial class frmReportViewer : Telerik.WinControls.UI.RadForm
    {
        public Telerik.Reporting.Report rptSource;
        public Boolean IsJO;
        public Boolean IsRO;
        clsJOTransactionLogs JOTransLog;
        clsROTransactionLogs ROTransLog;
        public frmReportViewer()
        {
            InitializeComponent();
        }
        public frmReportViewer(clsJOROReports obj)
        {
            InitializeComponent();
        }

        public frmReportViewer(Telerik.Reporting.Report report_file)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            rptSource = report_file;
            loadReportViewer();
        }
        public frmReportViewer(Telerik.Reporting.Report report_file, clsJOTransactionLogs obj)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            rptSource = report_file;
            JOTransLog = obj;
            loadReportViewer();
        }
        public frmReportViewer(Telerik.Reporting.Report report_file, clsROTransactionLogs obj)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            rptSource = report_file;
            ROTransLog = obj;
            loadReportViewer();
        }
        void loadReportViewer()
        {
            ReportSource report_source = new InstanceReportSource()
            {
                ReportDocument = rptSource
            };

            this.reportViewer1.ReportSource = report_source;
            this.reportViewer1.RefreshReport();
        }
        private void reportViewer1_Print(object sender, CancelEventArgs e)
        {
            if (IsJO)
            {
                e.Cancel = true;
                frmDirectPrinting DirectPrintingPage = new frmDirectPrinting(JOTransLog) { rptSource = this.reportViewer1.ReportSource };
                DirectPrintingPage.ShowDialog();
            }

            if (IsRO)
            {
                e.Cancel = true;
                frmDirectPrinting DirectROPrintingPage = new frmDirectPrinting(ROTransLog) { rptSource = this.reportViewer1.ReportSource };
                DirectROPrintingPage.ShowDialog();
            }
        }

    }
}
