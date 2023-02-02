namespace FGCIJOROSystem.Reports.rEquipmentReleasal
{
    using Domain.Global;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for rptReleasalSummaryReport.
    /// </summary>
    public partial class rptReleasalSummaryReport : Telerik.Reporting.Report
    {
        public rptReleasalSummaryReport(List<clsEquipment>ListOfReleasal, String PreparedBy, String PreparedByPos, String Title)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.objEquipmentReleasalSummary.DataSource = ListOfReleasal.ToList();
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            ReportParameters["Title"].Value = Title;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}