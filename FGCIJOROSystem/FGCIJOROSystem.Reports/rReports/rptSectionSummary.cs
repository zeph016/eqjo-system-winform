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
    /// Summary description for rptSectionSummary.
    /// </summary>
    public partial class rptSectionSummary : Telerik.Reporting.Report
    {
        public rptSectionSummary(List<clsJOROReports> ListOfJOROReports, string filterBy, string PreparedBy, string PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.JOROReportDataSource.DataSource = ListOfJOROReports.ToList();
            ReportParameters["FIlterBy"].Value = filterBy; ;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptSectionSummary(clsJOROReports obj)
        {
        }
    }
}