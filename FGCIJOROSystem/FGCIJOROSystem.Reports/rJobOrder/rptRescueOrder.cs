namespace FGCIJOROSystem.Reports.rJobOrder
{
    using FGCIJOROSystem.Domain.RescueOrder;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;

    /// <summary>
    /// Summary description for rptRescueOrder.
    /// </summary>
    public partial class rptRescueOrder : Telerik.Reporting.Report
    {
        public rptRescueOrder()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptRescueOrder(clsRescueOrder obj)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            ReportParameters["PrintCount"].Value = obj.PrintCount;
            ReportParameters["BranchName"].Value = obj.BranchName;
            ReportParameters["CustomerName"].Value = obj.CustomerName;
            ReportParameters["EquipmentType"].Value = obj.EquipmentCode;
            ReportParameters["EquipmentName"].Value = obj.EquipmentName;
            ReportParameters["DriverName"].Value = obj.DriverName;
            ReportParameters["Location"].Value = obj.EquipmentLocation;
            ReportParameters["RONo"].Value = obj.RONoStr;
            ReportParameters["Mileage"].Value = obj.Mileage.ToString();
            ReportParameters["ROStatus"].Value = obj.Status.ToString().ToUpper();

            var ROdatetime = obj.RODate.ToShortDateString();

            ReportParameters["RODate"].Value = ROdatetime;
            ReportParameters["ServiceVehicle"].Value = obj.ServiceVehicleName;
            ReportParameters["ServiceDriver"].Value = obj.ServiceDriverName;
            ReportParameters["ReceivedBy"].Value = obj.ContractorSectionHead;
            ReportParameters["ReceivedByPosition"].Value = "Section Head";
            ReportParameters["ApprovedName"].Value = obj.ApproverName;
            ReportParameters["ApprovedDesignation"].Value = obj.ApproverPosition;
            ReportParameters["EncoderName"].Value = obj.EncoderName;
            ReportParameters["EncoderPosition"].Value = obj.EncoderPosition;
            RODetail.DataSource = obj.ListOfRODetails.Where(x => x.IsActive == true).ToList();
            ROMechanics.DataSource = obj.ListOfROMechanics.Where(x => x.IsActive == true).ToList();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}