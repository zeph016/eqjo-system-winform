namespace FGCIJOROSystem.Reports.rAttendance
{
    using FGCIJOROSystem.Domain.ActualAdvance;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for rptDailyAttendance.
    /// </summary>
    public partial class rptDailyAttendance : Telerik.Reporting.Report
    {
        public rptDailyAttendance()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptDailyAttendance(List<clsAttendance> obj, String PreparedBy, DateTime DateOfUpdate, string PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objDailyAttendance.DataSource = obj;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["DateOfUpdate"].Value = DateOfUpdate.Date;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}