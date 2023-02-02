namespace FGCIJOROSystem.Reports.rEquipmentUR
{
    using FGCIJOROSystem.Domain.EquipmentUR;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;
    /// <summary>
    /// Summary description for rptSummaryOfEquipment.
    /// </summary>
    public partial class rptSummaryOfEquipment : Telerik.Reporting.Report
    {
        public rptSummaryOfEquipment(List<clsEquipmentUR> ListOfEquipmentUR, String FilterBy, String PreparedBy, String PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objSummaryOfEquipment.DataSource = ListOfEquipmentUR.ToList();
            ReportParameters["FilterBy"].Value = FilterBy;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}