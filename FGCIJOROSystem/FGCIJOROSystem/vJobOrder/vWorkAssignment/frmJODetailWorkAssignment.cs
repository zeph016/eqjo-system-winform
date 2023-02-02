using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.DAL.Repositories.WorkAssignment;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.WorkAssignment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.Domain.ActualAdvance;
using FGCIJOROSystem.DAL.Repositories.ActualAdvanceRepo;

namespace FGCIJOROSystem.Presentation.vJobOrder.vWorkAssignment
{
    public partial class frmJODetailWorkAssignment : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        List<clsWorkAssignments> ListOfJOWorkAssign;
        List<clsWorkAssignments> UnselectedJOWorkAssign;
        clsJODetails JODetails;

        JOWorkAssignmentRepository JOWorkAssignmentRepo;
        JODetailMechanicRepository JODetMechRepo;

        List<clsActualAdvanceDetails> ListOfActualAdvanceDet;
        List<clsActualAdvanceReference> ListOfActualAdvanceRef;

        ActualAdvanceRepository ActualAdvanceRepo;
        ActualAdvanceReferenceRepository ActualAdvanceRefRepo;
        #endregion
        #region Methods
        public frmJODetailWorkAssignment()
        {
            InitializeComponent();
        }
        public frmJODetailWorkAssignment(clsJODetails obj)
        {
            InitializeComponent();
            ListOfJOWorkAssign = new List<clsWorkAssignments>();
            UnselectedJOWorkAssign = new List<clsWorkAssignments>();
            JODetails = obj;
        }
        private void frmJODetailWorkAssignment_Load(object sender, EventArgs e)
        {

            DisplayProperties();
            load();
        }
        void load()
        {
            JODetMechRepo = new JODetailMechanicRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ListOfJOWorkAssign = JODetMechRepo.SearchByMechanics(JODetails);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsJOWorkAssignmentsBindingSource.DataSource = ListOfJOWorkAssign;
                DisplayWorkAssign();
            };
            bg.RunWorkerAsync();
        }
        void DisplayWorkAssign()
        {
            ListOfJOWorkAssign.ForEach(x => { 
                if (radGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < this.radGridView1.ChildRows.Count; i++)
                    {
                        if (x.Id != 0)
                        {
                            if ((Int64)radGridView1.Rows[i].Cells["Id"].Value == x.Id)
                            {
                                radGridView1.Rows[i].Cells["chkSelect"].Value = x.IsActive;
                            }
                        }                        
                    }
                }
            });            
        }
        void DisplayProperties()
        {            
            tbWorkDescription.Text = JODetails.WorkDescription;
            radGroupBox3.HeaderText = DateTime.Now.Date.ToShortDateString() + " Activity Updates";
        }
        private void MasterTemplate_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            JOAssignment();
        }
        void JOAssignment()
        {
            Boolean chkSelect = (Boolean)radGridView1.CurrentRow.Cells["chkSelect"].Value;
            clsWorkAssignments JOWorkAssign = (clsWorkAssignments)clsJOWorkAssignmentsBindingSource.Current;
            JOWorkAssign.IsActive = chkSelect;
            JOWorkAssign.JODetailId = JODetails.Id;
            JOWorkAssign.WorkDescription = tbWorkDescription.Text;
            if (JOWorkAssign.Id!=0)
            {
                UnselectedJOWorkAssign.Add(JOWorkAssign);
            }
            else
            {
                UnselectedJOWorkAssign.RemoveAll(x => x.JODetailId == JOWorkAssign.JODetailId);
            }
        }
        List<clsActualAdvanceDetails> MapProperties()
        {
            ListOfActualAdvanceDet = new List<clsActualAdvanceDetails>();
            ListOfJOWorkAssign.Where(x=> x.IsActive == true).ToList().ForEach(x =>
            {
                clsActualAdvanceDetails ActualAdvanceDet = new clsActualAdvanceDetails();
                if (ListOfActualAdvanceDet.Where(y=> y.EmployeeId == x.EmployeeId).Count() == 0)
                {
                    ActualAdvanceDet.EmployeeId = x.EmployeeId;
                    ActualAdvanceDet.BranchId = Program.CurrentUser.BranchId;
                    ActualAdvanceDet.Type = clsEnums.ActualAdvance.Actual;
                    ActualAdvanceDet.DateOfUpdate = DateTime.Now;
                    ActualAdvanceDet.AMStatus = 3;
                    ActualAdvanceDet.PMStatus = 3;
                    if (ActualAdvanceRefRepo.SearchBy(" WHERE AD.DateOfUpdate = CONVERT(varchar,'" + DateTime.Now.Date + "',101) AND AD.EmployeeId = " + ((clsWorkAssignments)clsJOWorkAssignmentsBindingSource.Current).EmployeeId.ToString() + " AND AR.JODetailId = " + x.JODetailId).ToList().Count == 0)
                    {
                        ActualAdvanceDet.ListOfActualReference.Add(new clsActualAdvanceReference()
                        {
                            Type = clsEnums.ActualAdvance.Actual,
                            Activity = x.WorkDescription,
                            EncoderId = Program.CurrentUser.Id,
                            JODetailId = x.JODetailId,
                            ReferenceNo = x.ReferenceNo,
                            RefYear = x.RefYear,
                            ReferenceType = x.ReferenceType,
                            EquipmentId = x.EquipmentId,
                            EquipmentType = x.EquipmentType
                        });
                        ListOfActualAdvanceDet.Add(ActualAdvanceDet);
                    }
                }
                else
                {
                    ActualAdvanceDet = ListOfActualAdvanceDet.Where(y => y.EmployeeId == x.EmployeeId).SingleOrDefault();
                    ActualAdvanceDet.ListOfActualReference.Add(new clsActualAdvanceReference()
                    {
                        Type = clsEnums.ActualAdvance.Actual,
                        Activity = x.WorkDescription,
                        EncoderId = Program.CurrentUser.Id,
                        JODetailId = x.JODetailId,
                        ReferenceNo = x.ReferenceNo,
                        RefYear = x.RefYear,
                        ReferenceType = x.ReferenceType,
                        EquipmentId = x.EquipmentId,
                        EquipmentType = x.EquipmentType
                    });
                }
            });
            return ListOfActualAdvanceDet;
        }       
        private void mSave_Click(object sender, EventArgs e)
        {
            radGridView1.EndEdit();
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    JOWorkAssignmentRepo = new JOWorkAssignmentRepository();
                    foreach (var x in ListOfJOWorkAssign.Where(x => x.IsActive == true))
                    {
                        if (x.Id == 0)
                        {
                            JOWorkAssignmentRepo.Add(x);
                        }
                        else
                        {
                            JOWorkAssignmentRepo.Update(x);
                        }
                    }
                    foreach (var x in UnselectedJOWorkAssign.Where(x => x.IsActive == false))
                    {
                        if (x.Id != 0)
                        {
                            JOWorkAssignmentRepo.Update(x);
                        }
                    }
                    ActualAdvanceRepo = new ActualAdvanceRepository();
                    var MappedProperties = MapProperties();
                    MappedProperties.ToList().ForEach(x =>
                    {
                        var List = ActualAdvanceRepo.SearchBy(" WHERE DateOfUpdate = CONVERT(varchar,'" + x.DateOfUpdate.Value + "',101) AND EmployeeId = '" + x.EmployeeId + "'").FirstOrDefault();
                        if (List == null)
                        {
                            ActualAdvanceRepo.Add(x);
                        }
                        else
                        {
                            x.Id = List.Id;
                            ActualAdvanceRepo.Update(x);
                        }
                    });
                    new frmMsg()
                    {
                        MsgBox = clsEnums.MsgBox.Success,
                        Message = "The record has been saved successfully!"
                    }.ShowDialog();
                    ts.Complete();
                }
                catch (Exception)
                {
                    ts.Dispose();
                    throw;
                }
            }
        }
        #endregion

        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (clsJOWorkAssignmentsBindingSource.Current != null)
            {
                loadActivityUpdates();
            }
        }
        void loadActivityUpdates()
        {
            ActualAdvanceRefRepo = new ActualAdvanceReferenceRepository();
            ListOfActualAdvanceRef = new List<clsActualAdvanceReference>();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ListOfActualAdvanceRef = ActualAdvanceRefRepo.SearchBy(" WHERE AD.DateOfUpdate = CONVERT(varchar,'" + DateTime.Now.Date + "',101) AND AD.EmployeeId = " + ((clsWorkAssignments)clsJOWorkAssignmentsBindingSource.Current).EmployeeId.ToString());
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsActualAdvanceReferenceBindingSource.DataSource = ListOfActualAdvanceRef;
            };
            bg.RunWorkerAsync();
        }

        private void tbWorkDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
