using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
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
using System.Transactions;
using System.Linq;
using FGCIJOROSystem.Domain.Configurations.Status;
using FGCIJOROSystem.Domain.Enums;

namespace FGCIJOROSystem.Presentation.vJobOrder
{
    public partial class frmStatusUpdate : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        clsJODetails JODetails;
        clsJOAuditTrail JOAuditTrail;

        JORepository JORepo;
        JODetailRepository JODetailRepo;
        JOAuditTrailRepository JOAuditTrailRepo;
        StatusRepository StatusRepo;

        public clsDataEvent DataEvent;
        #endregion
        #region Methods
        public frmStatusUpdate(clsJODetails obj)
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            JOAuditTrail = new clsJOAuditTrail();
            StatusRepo = new StatusRepository();
            JODetails = obj;
        }

        private void frmStatusUpdate_Load(object sender, EventArgs e)
        {
            lblJONumber.Text = "WORK DESCRIPTION : " + JODetails.WorkDescription;
            clsStatusBindingSource.DataSource = StatusRepo.GetAll();
            this.cbStatus.TextChanged += new System.EventHandler(this.cbStatus_TextChanged);

            RadToolTip Message = new RadToolTip();
            Message.SetToolTip(btnSave, "Save changes.");
        }
        clsJOAuditTrail MapProperties()
        {
            JOAuditTrail.StatusName = cbStatus.Text;
            JOAuditTrail.JODetailId = JODetails.Id;
            JOAuditTrail.Remarks = tbRemarks.Text;
            JOAuditTrail.StatusId = (Int64)cbStatus.SelectedValue;
            return JOAuditTrail;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    JOAuditTrailRepo = new JOAuditTrailRepository();
                    JOAuditTrailRepo.Add(MapProperties());
                    CheckStatusTransaction();
                    DataEvent.ConfirmData(MapProperties());
                    ts.Complete();
                }
                catch (Exception)
                {
                    ts.Dispose();
                    throw;
                }
            }
        }
        void CheckStatusTransaction()
        {
            clsEnums.JOROStatus Status;
            List<clsStatus> ListOfStatus = new List<clsStatus>();            
            StatusRepo = new StatusRepository();
            ListOfStatus = StatusRepo.GetAll();

            List<clsJODetails> ListOfJODet = new List<clsJODetails>();
            //List<clsJODetails> ApproveStatus = new List<clsJODetails>();

            JODetailRepo = new JODetailRepository();

            Int64 ApproveCount = 0;
            Int64 CompletedCount = 0;
            Int64 CancelledCount = 0;

            ListOfJODet = JODetailRepo.SearchBy(" WHERE JD.JOId = " + JODetails.JOId);
            ListOfStatus.ForEach(x => {
                ApproveCount = ApproveCount + ListOfJODet.Where(y=> y.StatusId == x.Id && x.IsApproval == true).Count();
                CompletedCount = CompletedCount + ListOfJODet.Where(y => y.StatusId == x.Id && x.IsCompleted == true).Count();
                CancelledCount = CancelledCount + ListOfJODet.Where(y => y.StatusId == x.Id && x.IsCancelled == true).Count();                
            });
            if (CompletedCount != 0)
            {
                if (CompletedCount == ListOfJODet.Count)
                {
                    Status = clsEnums.JOROStatus.Completed;
                }
                else
                {
                    Status = clsEnums.JOROStatus.PartiallyCompleted;
                }
            }
            else if (ApproveCount != 0)
            {
                if (ApproveCount == ListOfJODet.Count)
                {
                    Status = clsEnums.JOROStatus.Approved;
                }
                else
                {
                    Status = clsEnums.JOROStatus.PartiallyApproved;
                }
            }            
            else if (CancelledCount != 0)
            {
                if (CancelledCount == ListOfJODet.Count)
                {
                    Status = clsEnums.JOROStatus.Cancelled;
                }
                else
                {
                    Status = clsEnums.JOROStatus.Generated;
                }
            }
            else
            {
                Status = clsEnums.JOROStatus.Generated;
            }
            JORepo = new JORepository();
            JORepo.UpdateJOStatus(Status, JODetails.JOId);
        }
        #endregion

        private void cbStatus_TextChanged(object sender, EventArgs e)
        {
            cbStatus.ShowDropDown();
        }
    }
}
