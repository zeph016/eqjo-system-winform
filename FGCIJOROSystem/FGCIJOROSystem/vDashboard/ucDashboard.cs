using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.DAL.Repositories.DashboardRepo;
using FGCIJOROSystem.Domain.Global;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using Telerik.Pivot.Core;
using Telerik.WinControls.UI;
using Telerik.Pivot.Core.Aggregates;
using Telerik.Charting;
using Telerik.WinControls.UI.PivotGrid;
using FGCIJOROSystem.DAL.Repositories.EquipmentURRepo;
using FGCIJOROSystem.Common;
using FGCIJOROSystem.Domain.EquipmentUR;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Reports.rEquipmentUR;

namespace FGCIJOROSystem.Presentation.vDashboard
{
    public partial class ucDashboard : UserControl
    {
        
        #region Properties
        UREquipmentRepository UREquipmentRepo;
        JORODashboardRepository JORODashboardRepo;
        BranchRepository BranchRepo;
        EquipmentURRepository EquipmentURRepo;

        List<clsEquipmentUR> ListOfEquipmentUR;

        LocalDataSourceProvider provider; 
        #endregion
        #region Methods
        public ucDashboard()
        {
            InitializeComponent();
        }
        private void ucDashboard_Load(object sender, EventArgs e)
        {
            dtYear.Text = System.DateTime.Now.Year.ToString();
            loadBranch();
            cbBranch.SelectedValue = Program.CurrentUser.BranchId; 
            SetupAxes();

            loadEquipment();
            rbColumn.IsChecked = true;
        }
        #region EquipmentUnderRepair
        void loadEquipment()
        {
            //EquipmentURRepo = new EquipmentURRepository();
            //clsEquipmentURBindingSource.DataSource = EquipmentURRepo.GetAllUR();
            //UREquipmentRepo = new UREquipmentRepository();

            EquipmentURRepo = new EquipmentURRepository();
            BackgroundWorker bg = new BackgroundWorker();
            List<clsEquipmentUR> ListOfEquipmentUR = new List<clsEquipmentUR>();
            String GetJORO = rbAll.IsChecked == true ? " " : rbJO.IsChecked == true ? " WHERE ReferenceType = 0 " : " WHERE ReferenceType = 1";
            bg.DoWork += (s, e) => { ListOfEquipmentUR = EquipmentURRepo.GetAllUR((Int64)cbBranch.SelectedValue, dtYear.Value.Year.ToString(), GetJORO);
            };
            bg.RunWorkerCompleted += (s, e) => {
                clsEquipmentURBindingSource.DataSource = ListOfEquipmentUR;
                SetupPivot();
                recordCount();
            };
            bg.RunWorkerAsync();
        }
        #endregion 
        #region LoadSettings
        private void LoadSettings() 
        { 
            this.radChartView1.ChartElement.LegendPosition = LegendPosition.Right; 
            this.radChartView1.ChartElement.LegendElement.Alignment = ContentAlignment.TopCenter; 
            //this.checkBoxDelayUpdates.Checked = this.radPivotGrid1.ChartDataProvider.DelayUpdate; 
            //this.checkBoxSelectionOnly.Checked = this.radPivotGrid1.ChartDataProvider.SelectionOnly; 
            //this.checkBoxColumnSubTotals.Checked = this.radPivotGrid1.ChartDataProvider.IncludeColumnSubTotals; 
            //this.checkBoxRowSubTotals.Checked = this.radPivotGrid1.ChartDataProvider.IncludeRowSubTotals; 
            //this.checkBoxColumnGrandTotals.Checked = this.radPivotGrid1.ChartDataProvider.IncludeColumnGrandTotals; 
            //this.checkBoxRowGrandTotals.Checked = this.radPivotGrid1.ChartDataProvider.IncludeRowGrandTotals; 
            //if (this.radPivotGrid1.ChartDataProvider.SeriesAxis == PivotAxis.Rows) 
            //{ 
            //    this.radioRows.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On; 
            //} 
            //else 
            //{ 
            //    this.radioColumns.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On; 
            //} 
            //if (this.radPivotGrid1.ChartDataProvider.GeneratedSeriesType == GeneratedSeriesType.Bar) 
            //{ 
            //    this.radioBarSeries.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On; 
            //}
            //else if (this.radPivotGrid1.ChartDataProvider.GeneratedSeriesType == GeneratedSeriesType.Line) 
            //{ 
            //    this.radioLineSeries.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On; 
            //} 
            //else if (this.radPivotGrid1.ChartDataProvider.GeneratedSeriesType == GeneratedSeriesType.Area)
            //{ 
            //    this.radioAreaSeries.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On; 
            //} 

        }
        private void SetupAxes() 
        { 
            LinearAxis verticalAxis = new LinearAxis(); verticalAxis.AxisType = AxisType.Second; 
            CategoricalAxis horizontalAxis = new CategoricalAxis(); 
            horizontalAxis.LabelFitMode = AxisLabelFitMode.MultiLine; 

            this.radChartView1.Area.Axes.Add(horizontalAxis); 
            this.radChartView1.Area.Axes.Add(verticalAxis); 
        } 
        #endregion
        #region PivotGrid and ChartView
        void LoadData()
        {
            JORODashboardRepo = new JORODashboardRepository();
            String GetJORO = "";
            if (rbJO.IsChecked)
            {
                GetJORO = " AND T.ReferenceType = 0 ";
            }
            else if (rbRO.IsChecked)
            {
                GetJORO = " AND T.ReferenceType = 1 ";
            }
            else if (rbAll.IsChecked)
            {
                GetJORO = " AND T.ReferenceType = 0 OR T.ReferenceType = 1 ";
            }
            clsJORODashboardBindingSource.DataSource = JORODashboardRepo.SearchBy(" Where T.BranchId = " + cbBranch.SelectedValue.ToString() + " AND Year(T.Date) = " + dtYear.Value.Year + GetJORO);
            SetupPivot();
            LoadSettings();
        }
        private void SetupPivot() 
        {
            this.provider = new LocalDataSourceProvider() { ItemsSource = this.clsJORODashboardBindingSource };

            this.provider.RowGroupDescriptions.Add(new PropertyGroupDescription() { PropertyName = "ReferenceType", GroupComparer = new GroupNameComparer(), });
            this.provider.RowGroupDescriptions.Add(new PropertyGroupDescription() { PropertyName = "Status", GroupComparer = new GroupNameComparer(), ShowGroupsWithNoData = true });

            this.provider.ColumnGroupDescriptions.Add(new DateTimeGroupDescription() { PropertyName = "Date", Step = DateTimeStep.Month, ShowGroupsWithNoData = true });
            //this.provider.ColumnGroupDescriptions.Add(new PropertyGroupDescription() { PropertyName = "Status", GroupComparer = new GroupNameComparer(), ShowGroupsWithNoData = true }); 
            
            this.provider.AggregateDescriptions.Add(new PropertyAggregateDescription() { PropertyName = "ReferenceNo", CustomName = "JO|RO Count", AggregateFunction = AggregateFunctions.Count }); 

            this.radPivotGrid1.DataProvider = this.provider;
            this.radChartView1.DataSource = this.radPivotGrid1;

            RadPivotGrid pivot = radPivotGrid1;
            pivot.RowGroupsExpandBehavior = new GroupsExpandBehavior()
            {
                Expanded = false,
                UpToLevel = 2
            };
            pivot.ColumnGroupsExpandBehavior = new GroupsExpandBehavior() { Expanded = false };

            if (this.radPivotGrid1.PivotGridElement.ColumnRootGroup != null)
            {
                //PivotGroupNode firstNode = this.radPivotGrid1.PivotGridElement.ColumnRootGroup.Children[0];
                this.radPivotGrid1.PivotGridElement.SelectAll();
            }
            //this.radPivotGrid1.ChartDataProvider.UpdateCompleted += new EventHandler(ChartDataProvider_UpdateCompleted); 
            //this.radPivotGrid1.PivotGridElement.SelectAll();
        }
        void ChartDataProvider_UpdateCompleted(object sender, EventArgs e) { UpdateSeriesCombineMode(); }
        void UpdateSeriesCombineMode() 
        { 
            if (this.radPivotGrid1.ChartDataProvider.GeneratedSeriesType != GeneratedSeriesType.Bar) 
            { 
                foreach (CartesianSeries series in this.radChartView1.Series) 
                { 
                    series.CombineMode = ChartSeriesCombineMode.Stack; 
                } 
            } 
        }  
        #endregion
        #region WireEvents
        //protected override void WireEvents() 
        //{ 
        //    //this.checkBoxDelayUpdates.ToggleStateChanged += new StateChangedEventHandler(checkBox_ToggleStateChanged); 
        //    //this.checkBoxSelectionOnly.ToggleStateChanged += new StateChangedEventHandler(checkBox_ToggleStateChanged); 
        //    //this.checkBoxColumnSubTotals.ToggleStateChanged += new StateChangedEventHandler(checkBox_ToggleStateChanged); 
        //    //this.checkBoxRowSubTotals.ToggleStateChanged += new StateChangedEventHandler(checkBox_ToggleStateChanged); 
        //    //this.checkBoxColumnGrandTotals.ToggleStateChanged += new StateChangedEventHandler(checkBox_ToggleStateChanged); 
        //    //this.checkBoxRowGrandTotals.ToggleStateChanged += new StateChangedEventHandler(checkBox_ToggleStateChanged); 
        //    //this.radioRows.ToggleStateChanged += new StateChangedEventHandler(radioRows_ToggleStateChanged); 
        //    //this.radioColumns.ToggleStateChanged += new StateChangedEventHandler(radioRows_ToggleStateChanged); 
        //    //this.radioBarSeries.ToggleStateChanged += new StateChangedEventHandler(radioSeries_ToggleStateChanged); 
        //    //this.radioLineSeries.ToggleStateChanged += new StateChangedEventHandler(radioSeries_ToggleStateChanged); 
        //    //this.radioAreaSeries.ToggleStateChanged += new StateChangedEventHandler(radioSeries_ToggleStateChanged); 
        //    this.radPivotGrid1.ChartDataProvider.UpdateCompleted += new EventHandler(ChartDataProvider_UpdateCompleted); 
        //}  
        #endregion
        #region Branch
        private void cbBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbBranch.Items != null && cbBranch.Items.Count != 0)
            {
                LoadData();
                loadEquipment();
            }            
        }
        void loadBranch()
        {
            BranchRepo = new BranchRepository();
            cbBranch.ValueMember = "Id";
            cbBranch.DisplayMember = "BranchName";
            cbBranch.DataSource = BranchRepo.SearchBy(" Where Active = 1");            
        } 
        #endregion
        #region Year
        private void dtYear_ValueChanged(object sender, EventArgs e)
        {
            if (cbBranch.Items != null && cbBranch.Items.Count != 0)
            {
                LoadData();
                loadEquipment();
            }
        }
        #endregion        
        #region FilterbyJORO
        private void rbJO_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            LoadData();
            loadEquipment();
        }

        private void rbRO_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            LoadData();
            loadEquipment();
        }

        private void rbAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            LoadData();
            loadEquipment();
        }
        #endregion

        private void rbSeries_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            this.radPivotGrid1.ChartDataProvider.SeriesAxis = this.rbRows.IsChecked ? PivotAxis.Rows : PivotAxis.Columns; 
        }
        private void lnkReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmFilterEquipmentUR FEUR = new frmFilterEquipmentUR();
            FEUR.ShowDialog();
        }

        void recordCount()
        {
            int cn = radGridView1.RowCount;
            lblCount.Text = "Total Record Found: (" + cn.ToString() + ")";
        }
        #endregion
    }
}