namespace FGCIJOROSystem.Reports.rAttendance
{
    using FGCIJOROSystem.Domain.RescueOrder;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;
    /// <summary>
    /// Summary description for rptDriversAttendance.
    /// </summary>
    public partial class rptDriversAttendance : Telerik.Reporting.Report
    {
        public rptDriversAttendance(List<clsRescueOrder> ListOfDriversAttendance, String Filter, String PreparedBy, String PreparedByPos,DateTime DateFrom, DateTime DateTo)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objDriversAttendance.DataSource = ListOfDriversAttendance.ToList();
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            ReportParameters["Filter"].Value = Filter;
            ReportParameters["DateFrom"].Value = DateFrom;
            ReportParameters["DateTo"].Value = DateTo;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}