namespace FGCIJOROSystem.Reports.rAuditTrail
{
    using FGCIJOROSystem.Domain.Configurations.Users;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;

    /// <summary>
    /// Summary description for rptAuditTrailRecordCountSum.
    /// </summary>
    public partial class rptAuditTrailRecordCountSum : Telerik.Reporting.Report
    {
        public rptAuditTrailRecordCountSum()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptAuditTrailRecordCountSum(List<clsUsersLog> ListOfUsersLog, string filterBy, string PreparedBy, string PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objAuditTrailReport.DataSource = ListOfUsersLog.ToList();
            ReportParameters["FilterBy"].Value = filterBy;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}