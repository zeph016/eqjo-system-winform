namespace FGCIJOROSystem.Reports.rPRS
{
    using FGCIJOROSystem.Domain.PRS;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
using FGCIJOROSystem.Domain.Configurations.Users;

    /// <summary>
    /// Summary description for rptJOROPRSList.
    /// </summary>
    public partial class rptJOROList : Telerik.Reporting.Report
    {
        public rptJOROList()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptJOROList(List<clsJOROPRS> ListOfJOROPRS, String Title, clsUser PreparedBy)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objJOROPRS.DataSource = ListOfJOROPRS.OrderBy(x=> x.EquipmentName);
            ReportParameters["Title"].Value = Title;
            ReportParameters["PreparedBy"].Value = PreparedBy.FullName;
            ReportParameters["Position"].Value = PreparedBy.Position;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}