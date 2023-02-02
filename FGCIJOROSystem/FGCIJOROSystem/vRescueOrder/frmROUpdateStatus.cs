using FGCIJOROSystem.Common;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.DAL.Repositories.RORepo;
using FGCIJOROSystem.Domain.Configurations.Status;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.RescueOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using System.Transactions;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
namespace FGCIJOROSystem.Presentation.vRescueOrder
{
    public partial class frmROUpdateStatus : Telerik.WinControls.UI.RadForm
    {
        clsRODetails RODetails;
        clsROAuditTrail ROAuditTrail;

        RORepository RORepo;
        RODetailRepository RODetailRepo;
        ROAuditTrailRepository ROAuditTrailRepo;

        ROTransactionLogRepository ROTransactionLogRepo;
        StatusRepository StatusRepo;

        public clsDataEvent DataEvent;
        public frmROUpdateStatus()
        {
            InitializeComponent();
        }
        public frmROUpdateStatus(clsRODetails obj)
        {
            InitializeComponent();
            DataEvent = new clsDataEvent();
            ROAuditTrail = new clsROAuditTrail();
            StatusRepo = new StatusRepository();
            RODetails = obj;
        }

        private void frmROUpdateStatus_Load(object sender, EventArgs e)
        {
            lblJONumber.Text = "WORK DESCRIPTION : " + RODetails.WorkDescription;
            cbStatus.DataSource = StatusRepo.GetAll();
            cbStatus.DisplayMember = "StatusName";
            cbStatus.ValueMember = "Id";
        }
        clsROAuditTrail MapProperties()
        {
            
            ROAuditTrail.StatusName = cbStatus.Text;
            ROAuditTrail.RODetailId = RODetails.Id;
            ROAuditTrail.Remarks = tbRemarks.Text;
            ROAuditTrail.StatusId = (Int64)cbStatus.SelectedValue;
            return ROAuditTrail;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    ROAuditTrailRepo = new ROAuditTrailRepository();
                    ROAuditTrailRepo.Add(MapProperties());           
                    CheckStatusTransaction();
                    DataEvent.ConfirmData(MapProperties());
                    frmMsg MsgBox = new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been saved successfully!"
                    };
                    MsgBox.ShowDialog();
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

            List<clsRODetails> ListOfRODet = new List<clsRODetails>();
            //List<clsJODetails> ApproveStatus = new List<clsJODetails>();

            RODetailRepo = new RODetailRepository();

            Int64 ApproveCount = 0;
            Int64 CompletedCount = 0;
            Int64 CancelledCount = 0;

            ListOfRODet = RODetailRepo.SearchBy(" WHERE RD.ROId = " + RODetails.ROId);
            ListOfStatus.ForEach(x =>
            {
                ApproveCount = ApproveCount + ListOfRODet.Where(y => y.StatusId == x.Id && x.IsApproval == true).Count();
                CompletedCount = CompletedCount + ListOfRODet.Where(y => y.StatusId == x.Id && x.IsCompleted == true).Count();
                CancelledCount = CancelledCount + ListOfRODet.Where(y => y.StatusId == x.Id && x.IsCancelled == true).Count();
            });
            if (CompletedCount != 0)
            {
                if (CompletedCount == ListOfRODet.Count)
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
                if (ApproveCount == ListOfRODet.Count)
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
                if (CancelledCount == ListOfRODet.Count)
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
            RORepo = new RORepository();
            RORepo.UpdateROStatus(Status, RODetails.ROId);
        }
    }
}
