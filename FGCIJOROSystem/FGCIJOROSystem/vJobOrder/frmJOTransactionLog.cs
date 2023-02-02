using FGCIJOROSystem.DAL.Repositories.JORepo;
using FGCIJOROSystem.Domain.JobOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vJobOrder
{
    public partial class frmJOTransactionLog : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        clsJobOrder JobOrder;
        JOTransactionLogRepository JOTransactionLogRepo;
        JODetailTransactionLogRepository JODetailTransactionLogRepo;
        JOMechTransactionLogRepository JOMechTransactionLogRepo;

        List<clsJOTransactionLogs> ListOfJOTransLog;
        List<clsJODetailTransactionLogs> ListOfJODetTransLog;
        List<clsJOMechTransactionLogs> ListOfJOMechTransLog;
        #endregion
        #region Methods
        public frmJOTransactionLog()
        {
            InitializeComponent();
        }
        public frmJOTransactionLog(clsJobOrder obj)
        {
            InitializeComponent();
            JobOrder = obj;
        }
        private void frmJOTransactionLog_Load(object sender, EventArgs e)
        {
            loadJOTransactions();
        }
        void loadJOTransactions()
        {            
            BackgroundWorker bg = new BackgroundWorker();    
            bg.DoWork += (s,e) => 
            {
                JOTransactionLogRepo = new JOTransactionLogRepository();
                ListOfJOTransLog = JOTransactionLogRepo.SearchBy(" WHERE JO.JOId = " + JobOrder.Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsJOTransactionLogsBindingSource.DataSource = ListOfJOTransLog;
            };
            bg.RunWorkerAsync();
            
        }
        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (clsJOTransactionLogsBindingSource.Current != null)
            {
                loadJODetailTransactions();
                loadJOMechanicsTransactions();
            }
        }
        void loadJODetailTransactions()
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                JODetailTransactionLogRepo = new JODetailTransactionLogRepository();
                ListOfJODetTransLog = JODetailTransactionLogRepo.SearchBy(" WHERE JD.JOTransLogId = " + ((clsJOTransactionLogs)clsJOTransactionLogsBindingSource.Current).Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsJODetailTransactionLogsBindingSource.DataSource = ListOfJODetTransLog;
            };
            bg.RunWorkerAsync();
            
        }
        void loadJOMechanicsTransactions()
        {
            BackgroundWorker bg = new BackgroundWorker();
            
            bg.DoWork += (s, e) =>
            {
                JOMechTransactionLogRepo = new JOMechTransactionLogRepository();
                ListOfJOMechTransLog = JOMechTransactionLogRepo.SearchBy(" WHERE JM.JOTransLogId = " + ((clsJOTransactionLogs)clsJOTransactionLogsBindingSource.Current).Id);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsJOTransactionLogsBindingSource.DataSource = ListOfJOMechTransLog;
            };
            
        }
        #endregion        

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        
       
    }
}
