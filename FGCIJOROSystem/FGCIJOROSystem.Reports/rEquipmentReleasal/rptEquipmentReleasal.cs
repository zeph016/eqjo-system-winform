namespace FGCIJOROSystem.Reports.rEquipmentReleasal
{
    using FGCIJOROSystem.DAL.Repositories.EquipReleasalRepo;
    using FGCIJOROSystem.Domain.EquipmentReleasal;
    using FGCIJOROSystem.Domain.Global;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for rptEquipmentReleasal.
    /// </summary>
    public partial class rptEquipmentReleasal : Telerik.Reporting.Report
    {
        public rptEquipmentReleasal()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptEquipmentReleasal(clsJOReleasalTransaction obj)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            ReportParameters["ERNo"].Value = obj.ERNoStr;
            ReportParameters["DateReleased"].Value = obj.DateReleased;            
            objEquipment.DataSource = new EquipmentReleasalRepository().SearchBy(obj.Id);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        public rptEquipmentReleasal(clsROReleasalTransaction obj)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            ReportParameters["ERNo"].Value = obj.ERNoStr;
            ReportParameters["DateReleased"].Value = obj.DateReleased;
            objEquipment.DataSource = new EquipmentReleasalRepository().SearchROBy(obj.Id);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}