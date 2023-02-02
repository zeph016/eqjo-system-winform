namespace FGCIJOROSystem.Reports.rReports
{
    using FGCIJOROSystem.Domain.Monitoring;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;
    /// <summary>
    /// Summary description for rptMonitoringSummary.
    /// </summary>
    public partial class rptMonitoringSummary : Telerik.Reporting.Report
    {
        public rptMonitoringSummary(List<clsMonitoring>ListOfMonitoring, string PreparedBy, string PreparedByPos, string FilterBy)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.objMonitoringSummary.DataSource = ListOfMonitoring.ToList();
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            ReportParameters["FilterBy"].Value = FilterBy;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}