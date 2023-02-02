namespace FGCIJOROSystem.Reports.rReports
{
    using FGCIJOROSystem.Domain.Configurations.Tariff;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;
    /// <summary>
    /// Summary description for rptTariffSummary.
    /// </summary>
    public partial class rptTariffSummary : Telerik.Reporting.Report
    {
        public rptTariffSummary(List<clsTariff> ListOfTariff, string PreparedBy, string PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.objTariffSummary.DataSource = ListOfTariff.ToList(); ;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;

            
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}