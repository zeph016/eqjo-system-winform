namespace FGCIJOROSystem.Reports.rPPEStatusLog
{
    using FGCIJOROSystem.Domain.PPEStatusLog;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;
    /// <summary>
    /// Summary description for rptPPEStatusLog.
    /// </summary>
    public partial class rptPPEStatusLog : Telerik.Reporting.Report
    {
        public rptPPEStatusLog(List<clsPPEStatusLog> ListOfStatusLog, String FilterBy, String PreparedBy, String PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objPPEStatusLog.DataSource = ListOfStatusLog.ToList();
            ReportParameters["FilterBy"].Value = FilterBy;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}