using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using System.Collections.Generic;
using FGCIJOROSystem.Domain.Configurations.Tariff;

namespace FGCIJOROSystem.Reports.rReports
{
    /// <summary>
    /// Summary description for rptTariffByJobCategory.
    /// </summary>
    public partial class rptTariffByJobCategory : Telerik.Reporting.Report
    {
        public rptTariffByJobCategory(List<clsTariff> ListOfTariff, string PreparedBy, string PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            objTariffSummaryByJobCategory.DataSource = ListOfTariff;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}