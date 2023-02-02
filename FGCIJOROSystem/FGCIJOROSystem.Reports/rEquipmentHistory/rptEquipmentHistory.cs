namespace FGCIJOROSystem.Reports.rEquipmentHistory
{
    using FGCIJOROSystem.Domain.Reports;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Summary description for rptEquipmentHistory.
    /// </summary>
    public partial class rptEquipmentHistory : Telerik.Reporting.Report
    {
        //private List<clsEquipmentHistoryReport> ListOfEquipment;
        //private string filter;

        public rptEquipmentHistory()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptEquipmentHistory(List<clsEquipmentHistoryReport> ListOfEquipment, string filterBy, string PreparedBy, string PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.objEquipmentHistory.DataSource = ListOfEquipment.ToList();
            ReportParameters["FilterBy"].Value = filterBy;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}