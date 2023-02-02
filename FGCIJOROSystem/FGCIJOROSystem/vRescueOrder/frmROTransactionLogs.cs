using FGCIJOROSystem.DAL.Repositories.RORepo;
using FGCIJOROSystem.Domain.RescueOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vRescueOrder
{
    public partial class frmROTransactionLogs : Telerik.WinControls.UI.RadForm
    {
        

        
        #region Properties
        clsRescueOrder RescueOrder;
        ROTransactionLogRepository ROTransactionLogRepo;
        RODetailTransactionLogRepository RODetailTransactionLogRepo;
        ROMechTransactionLogRepository ROMechTransactionLogRepo;

        List<clsROTransactionLogs> ListOfJOTransLog;
        List<clsRODetailTransactionLogs> ListOfRODetTransLog;
        List<clsROMechTransactionLogs> ListOfJOMechTransLog;
        #endregion
        #region Methods
        public frmROTransactionLogs()
        {
            InitializeComponent();
        }
        public frmROTransactionLogs(clsRescueOrder obj)
        {
            InitializeComponent();
            RescueOrder = obj;
        }
        private void frmROTransactionLog_Load(object sender, EventArgs e)
        {
            loadJOTransactions();
        }
        void loadJOTransactions()
        {            
            BackgroundWorker bg = new BackgroundWorker();    
            bg.DoWork += (s,e) => 
            {
                ROTransactionLogRepo = new ROTransactionLogRepository();
                ListOfJOTransLog = ROTransactionLogRepo.SearchBy(" WHERE RO.ROId = " + RescueOrder.Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsROTransactionLogsBindingSource.DataSource = ListOfJOTransLog;
            };
            bg.RunWorkerAsync();
            
        }
        private void radGridView4_SelectionChanged(object sender, EventArgs e)
        {
            if (clsROTransactionLogsBindingSource.Current != null)
            {
                loadRODetailTransactions();
                loadROMechanicsTransactions();
            }
        }       
        void loadRODetailTransactions()
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                RODetailTransactionLogRepo = new RODetailTransactionLogRepository();
                ListOfRODetTransLog = RODetailTransactionLogRepo.SearchBy(" WHERE RD.ROTransLogId = " + ((clsROTransactionLogs)clsROTransactionLogsBindingSource.Current).Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsRODetailTransactionLogsBindingSource.DataSource = ListOfRODetTransLog;
            };
            bg.RunWorkerAsync();
            
        }
        void loadROMechanicsTransactions()
        {
            BackgroundWorker bg = new BackgroundWorker();
            
            bg.DoWork += (s, e) =>
            {
                ROMechTransactionLogRepo = new ROMechTransactionLogRepository();
                ListOfJOMechTransLog = ROMechTransactionLogRepo.SearchBy(" WHERE JM.ROTransLogId = " + ((clsROTransactionLogs)clsROTransactionLogsBindingSource.Current).Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsROTransactionLogsBindingSource.DataSource = ListOfJOMechTransLog;
            };
            
        }
        #endregion        


    }
}
