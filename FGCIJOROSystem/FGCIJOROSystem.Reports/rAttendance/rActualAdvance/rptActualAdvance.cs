namespace FGCIJOROSystem.Reports.rActualAdvance
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
    /// Summary description for rptActualAdvance.
    /// </summary>
    public partial class rptActualAdvance : Telerik.Reporting.Report
    {
        public rptActualAdvance()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptActualAdvance(List<clsActualAdvance> obj, DateTime dtActualDate)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //ReportParameters["dtActualDate"].Value = dtActualDate.ToString("MM/dd/yyyy");
            ActivityReport.DataSource = obj;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}