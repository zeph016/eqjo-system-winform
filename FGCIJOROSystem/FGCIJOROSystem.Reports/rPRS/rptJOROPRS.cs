namespace FGCIJOROSystem.Reports.rPRS
{
    using FGCIJOROSystem.DAL.Repositories.PRSRepo;
    using FGCIJOROSystem.Domain.JobOrder;
    using FGCIJOROSystem.Domain.RescueOrder;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for rptJOROPRS.
    /// </summary>
    public partial class rptJOROPRS : Telerik.Reporting.Report
    {
        public rptJOROPRS()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();            
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptJOROPRS(clsJobOrder JO)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objJOPRS.DataSource = new JOROPRSRepository().GetJOPRS(JO);
            txtTitle.Value = "JO Request ( " + JO.JONoStr + " )";
            txtDriver.Value = JO.DriverName;
            ReportParameters["strCustomer"].Value = JO.CustomerName;
            ReportParameters["strRefNo"].Value = JO.JONoStr;
            ReportParameters["strEquipment"].Value = JO.EquipmentCode;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptJOROPRS(clsRescueOrder RO)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            objJOPRS.DataSource = new JOROPRSRepository().GetROPRS(RO);
            txtTitle.Value = "RO Request ( " + RO.RONoStr + " )";
            txtDriver.Value = RO.DriverName;
            ReportParameters["strCustomer"].Value = RO.CustomerName;
            ReportParameters["strRefNo"].Value = RO.RONoStr;
            ReportParameters["strEquipment"].Value = RO.EquipmentCode;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}