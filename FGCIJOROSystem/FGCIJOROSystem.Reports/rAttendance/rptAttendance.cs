namespace FGCIJOROSystem.Reports.rAttendance
{
    using FGCIJOROSystem.Domain.ActualAdvance;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for rptAttendance.
    /// </summary>
    public partial class rptAttendance : Telerik.Reporting.Report
    {
        public rptAttendance()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptAttendance(object obj)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objAttendance.DataSource = obj;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}