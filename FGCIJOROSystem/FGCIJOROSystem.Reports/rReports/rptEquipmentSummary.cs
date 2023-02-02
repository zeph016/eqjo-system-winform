namespace FGCIJOROSystem.Reports.rReports
{
    using FGCIJOROSystem.Domain.Reports;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;

    /// <summary>
    /// Summary description for rptEquipmentSummary.
    /// </summary>
    public partial class rptEquipmentSummary : Telerik.Reporting.Report
    {
        public rptEquipmentSummary(List<clsJOROReports> ListOfJOROReports, string filterBy, string PreparedBy, string PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.JOROReportDataSource.DataSource = ListOfJOROReports.ToList();
            ReportParameters["FilterBy"].Value = filterBy;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}