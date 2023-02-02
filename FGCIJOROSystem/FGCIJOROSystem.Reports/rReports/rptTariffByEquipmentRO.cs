using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using System.Linq;
using FGCIJOROSystem.Domain.Configurations.Tariff;
using System.Collections.Generic;
using System.Globalization;

namespace FGCIJOROSystem.Reports.rReports
{
    /// <summary>
    /// Summary description for rptTariffByEquipmentRO.
    /// </summary>
    public partial class rptTariffByEquipmentRO : Telerik.Reporting.Report
    {
        decimal totalAmount = 0.00M;
        public rptTariffByEquipmentRO(List<clsTariffEqRO> ListOfTariffEqRO, string PreparedBy, string PreparedByPos, bool isDate, DateTime dateFrom, DateTime dateTo)
        {
            
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.objTariffEqSummaryRO.DataSource = ListOfTariffEqRO.ToList();
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            ReportParameters["DateFrom"].Value = DateTime.Now.ToShortDateString();
            ReportParameters["DateTo"].Value = DateTime.Now.ToShortDateString();
            if (isDate)
            {
                ReportParameters["DateFrom"].Value = dateFrom.ToShortDateString();
                ReportParameters["DateTo"].Value = dateTo.ToShortDateString();
                textBox1.Value = "Date Range: " + dateFrom.ToShortDateString() + " - " + dateTo.ToShortDateString();
            }
            else
            {
                textBox1.Value = string.Empty;
            }
            
            foreach (var item in ListOfTariffEqRO)
                    totalAmount = totalAmount + (decimal)item.Amount;
            tbxSum.Value =  String.Format(CultureInfo.GetCultureInfo("en-PH"), "{0:C}", totalAmount);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}