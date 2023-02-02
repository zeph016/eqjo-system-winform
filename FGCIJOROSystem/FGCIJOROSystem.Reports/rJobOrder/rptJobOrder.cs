namespace FGCIJOROSystem.Reports.rJobOrder
{
    using FGCIJOROSystem.Domain.JobOrder;
    using FGCIJOROSystem.Domain.ActualAdvance;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Summary description for rptJobOrder.
    /// </summary>
    public partial class rptJobOrder : Telerik.Reporting.Report
    {
        //public rptJobOrder()
        //{
            //
            // Required for telerik Reporting designer support
            //
            //InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        //}
        public rptJobOrder(clsJobOrder obj, List<clsActivityReport> activityReportObj)
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
            ReportParameters["Location"].Value = obj.Location;
            ReportParameters["JONo"].Value = obj.JONoStr;
            ReportParameters["JOStatus"].Value = obj.Status.ToString().ToUpper();
            ReportParameters["Mileage"].Value = obj.Mileage.ToString();

            var JOdatetime = obj.JODate.ToShortDateString();

            ReportParameters["JODate"].Value = JOdatetime;
            ReportParameters["ReceivedBy"].Value = obj.ContractorSectionHead;
            ReportParameters["ReceivedByPosition"].Value = "Section Head";
            ReportParameters["ApprovedName"].Value = obj.ApproverName;
            ReportParameters["ApprovedDesignation"].Value = obj.ApproverPosition;
            ReportParameters["EncoderName"].Value = obj.EncoderName;
            ReportParameters["EncoderPosition"].Value = obj.EncoderPosition;
            JODetail.DataSource = obj.ListOfJODetails.Where(x => x.IsActive == true).ToList();
            JOMechanics.DataSource = obj.ListOfMechanics.Where(x => x.IsActive == true).ToList();
            if (activityReportObj != null)
                JOActivityReport.DataSource = activityReportObj.ToList();
            else
            {
                JOActivityReport.DataSource = null;
                table2.Visible = false;
                textBox90.Visible = false;
            }
               
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

    }
}