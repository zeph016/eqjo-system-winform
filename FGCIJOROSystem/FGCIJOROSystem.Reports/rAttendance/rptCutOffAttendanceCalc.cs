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
    using System.Linq;
    /// <summary>
    /// Summary description for rptCutOffAttendance.
    /// </summary>
    public partial class rptCutOffAttendanceCalc : Telerik.Reporting.Report
    {
        public rptCutOffAttendanceCalc()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptCutOffAttendanceCalc(List<clsAttendance> ListOfAttendance, String PreparedBy, DateTime DateOfUpdateFrom, string PreparedByPos, DateTime DateOfUpdateTo, string FilterBy)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //ReportParameters["NoOfDays"].Value = Math.Abs(DateOfUpdateFrom.Date.Subtract(DateOfUpdateTo.Date).Days) -2;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["DateOfUpdateFrom"].Value = DateOfUpdateFrom.Date;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            ReportParameters["DateOfUpdateTo"].Value = DateOfUpdateTo;
            ReportParameters["FilterBy"].Value = FilterBy;
            objAttendance.DataSource = ListOfAttendance.ToList();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public static Color ColorFromName(string colorName)
        {
            if (!string.IsNullOrEmpty(colorName))
            {
                return Color.FromName(colorName);
            }
            return Color.Transparent;
        }

    }
}