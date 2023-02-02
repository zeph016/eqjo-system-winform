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
    /// Summary description for rptEquipmentUR.
    /// </summary>
    public partial class rptEquipmentUR : Telerik.Reporting.Report
    {
        public rptEquipmentUR(List<clsEquipmentUR> ListOfEquipmentUR, String FilterBy, string PreparedBy, string PreparedByPos)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            this.objEquipmentUR.DataSource = ListOfEquipmentUR.ToList();
            ReportParameters["FilterBy"].Value = FilterBy;
            ReportParameters["PreparedBy"].Value = PreparedBy;
            ReportParameters["PreparedByPos"].Value = PreparedByPos;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}