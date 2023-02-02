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
    /// Summary description for rptAccomplishmentReport.
    /// </summary>
    public partial class rptAccomplishmentReport : Telerik.Reporting.Report
    {
        public rptAccomplishmentReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptAccomplishmentReport(List<clsAttendance> obj, String PreparedBy, DateTime DateOfUpdate, string PreparedByPos, string dateUpdate)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;

            
            ReportParameters["DateOfUpdate"].Value = dateUpdate;
           
            

            objAccomplishmentReport.DataSource = obj;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}