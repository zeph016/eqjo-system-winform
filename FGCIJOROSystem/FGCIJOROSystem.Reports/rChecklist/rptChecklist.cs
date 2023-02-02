namespace FGCIJOROSystem.Reports.rChecklist
{
    using FGCIJOROSystem.Domain.Checklist;
    using FGCIJOROSystem.Domain.Global;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for rptChecklist.
    /// </summary>
    public partial class rptChecklist : Telerik.Reporting.Report
    {
        //private clsChecklist clsChecklist;

        public rptChecklist()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public rptChecklist(clsChecklistGenerator obj)
        {
            //
            // Required for telerik Reporting designer support
            //


            InitializeComponent();
            ReportParameters["EquipmentName"].Value = obj.EquipmentName;
            ReportParameters["Location"].Value = obj.Location;
            ReportParameters["OdometerReading"].Value = obj.OdometerReading;
            ReportParameters["DateEncoded"].Value = obj.DateEncoded;
            ReportParameters["DateReceived"].Value = obj.DateReceived;
            ReportParameters["DateCompleted"].Value = obj.DateCompleted;
            ReportParameters["ChecklistNo"].Value = obj.ChecklistNoStr;
            ReportParameters["EquipmentTypeName"].Value = obj.EquipmentCode;
            ReportParameters["PreparedBy"].Value = obj.PreparedBy;
            ReportParameters["PreparedByPos"].Value = obj.PreparedByPos;
            objChecklistGeneratorDetails.DataSource = obj.ListOfChecklistTransaction;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
           
        }

        public rptChecklist(clsChecklist obj)
        {
            // TODO: Complete member initialization
            //this.clsChecklist = clsChecklist;
            InitializeComponent();
            ReportParameters["EquipmentName"].Value = obj.EquipmentName;
            ReportParameters["Location"].Value = obj.Location;
        }
    }
}