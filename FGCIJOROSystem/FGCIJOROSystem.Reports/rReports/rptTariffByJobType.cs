using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FGCIJOROSystem.Domain.Configurations.Tariff;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;

namespace FGCIJOROSystem.Reports.rReports
{
    /// <summary>
    /// Summary description for rptTariffByJobType.
    /// </summary>
    public partial class rptTariffByJobType : Telerik.Reporting.Report
    {
        public rptTariffByJobType(List<clsTariff> ListOfTariff, string PreparedBy, string PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            objTariffSummaryByJobType.DataSource = ListOfTariff;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}