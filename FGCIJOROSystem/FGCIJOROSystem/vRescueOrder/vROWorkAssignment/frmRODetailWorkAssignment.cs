using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.DAL.Repositories.WorkAssignment;
using FGCIJOROSystem.Domain.RescueOrder;
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
    public partial class frmRODetailWorkAssignment : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        List<clsWorkAssignments> ListOfROWorkAssign;
        List<clsWorkAssignments> UnselectedROWorkAssign;
        clsRODetails RODetails;

        ROWorkAssignmentRepository ROWorkAssignmentRepo;
        RODetailMechanicRepository RODetMechRepo;

        List<clsActualAdvanceDetails> ListOfActualAdvanceDet;
        List<clsActualAdvanceReference> ListOfActualAdvanceRef;

        ActualAdvanceRepository ActualAdvanceRepo;
        ActualAdvanceReferenceRepository ActualAdvanceRefRepo;
        #endregion
        public frmRODetailWorkAssignment()
        {
            InitializeComponent();
        }
        public frmRODetailWorkAssignment(clsRODetails obj)
        {
            InitializeComponent();
            ListOfROWorkAssign = new List<clsWorkAssignments>();
            UnselectedROWorkAssign = new List<clsWorkAssignments>();
            RODetails = obj;
        }

        private void frmRODetailWorkAssignment_Load(object sender, EventArgs e)
        {
            DisplayProperties();
            load();
        }
        void load()
        {
            RODetMechRepo = new RODetailMechanicRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ListOfROWorkAssign = RODetMechRepo.SearchByMechanics(RODetails);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsWorkAssignmentsBindingSource.DataSource = ListOfROWorkAssign;
                DisplayWorkAssign();
            };
            bg.RunWorkerAsync();
        }
       
        void DisplayWorkAssign()
        {
            
            ListOfROWorkAssign.ForEach(x =>
            {
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
            tbWorkDescription.Text = RODetails.WorkDescription;
            radGroupBox3.HeaderText = DateTime.Now.Date.ToShortDateString() + " Activity Updates";
        }
        private void MasterTemplate_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            ROAssignment();
        }
        void ROAssignment()
        {
            Boolean chkSelect = (Boolean)radGridView1.CurrentRow.Cells["chkSelect"].Value;
            clsWorkAssignments JOWorkAssign = (clsWorkAssignments)clsWorkAssignmentsBindingSource.Current;
            JOWorkAssign.IsActive = chkSelect;
            JOWorkAssign.JODetailId = RODetails.Id;
            JOWorkAssign.WorkDescription = tbWorkDescription.Text;
            if (JOWorkAssign.Id != 0)
            {
                UnselectedROWorkAssign.Add(JOWorkAssign);
            }
            else
            {
                UnselectedROWorkAssign.RemoveAll(x => x.JODetailId == JOWorkAssign.JODetailId);
            }
        }
        List<clsActualAdvanceDetails> MapProperties()
        {
            ListOfActualAdvanceDet = new List<clsActualAdvanceDetails>();
            ListOfROWorkAssign.Where(x => x.IsActive == true).ToList().ForEach(x =>
            {
                clsActualAdvanceDetails ActualAdvanceDet = new clsActualAdvanceDetails();
                if (ListOfActualAdvanceDet.Where(y => y.EmployeeId == x.EmployeeId).Count() == 0)
                {
                    ActualAdvanceDet.EmployeeId = x.EmployeeId;
                    ActualAdvanceDet.BranchId = Program.CurrentUser.BranchId;
                    ActualAdvanceDet.Type = clsEnums.ActualAdvance.Actual;
                    ActualAdvanceDet.DateOfUpdate = DateTime.Now;
                    ActualAdvanceDet.AMStatus = 3;
                    ActualAdvanceDet.PMStatus = 3;
                    if (ActualAdvanceRefRepo.SearchBy(" WHERE AD.DateOfUpdate = CONVERT(varchar,'" + DateTime.Now.Date + "',101) AND AD.EmployeeId = " + ((clsWorkAssignments)clsWorkAssignmentsBindingSource.Current).EmployeeId.ToString() + " AND AR.JODetailId = " + x.JODetailId).ToList().Count == 0)
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
                    ROWorkAssignmentRepo = new ROWorkAssignmentRepository();
                    foreach (var x in ListOfROWorkAssign.Where(x => x.IsActive == true))
                    {
                        if (x.Id == 0)
                        {
                            ROWorkAssignmentRepo.Add(x);
                        }
                        else
                        {
                            ROWorkAssignmentRepo.Update(x);
                        }
                    }
                    foreach (var x in UnselectedROWorkAssign.Where(x => x.IsActive == false))
                    {
                        if (x.Id != 0)
                        {
                            ROWorkAssignmentRepo.Update(x);
                        }
                    }
                    ActualAdvanceRepo = new ActualAdvanceRepository();
                    var MappedProperties = MapProperties();
                    MappedProperties.ToList().ForEach(x =>
                    {
                        var List = ActualAdvanceRepo.SearchBy(" WHERE DateOfUpdate = CONVERT(varchar,'" + x.DateOfUpdate.Value.Date + "',101) AND EmployeeId = '" + x.EmployeeId + "'").FirstOrDefault();
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
        private void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (clsWorkAssignmentsBindingSource.Current != null)
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
                ListOfActualAdvanceRef = ActualAdvanceRefRepo.SearchBy(" WHERE AD.DateOfUpdate = CONVERT(varchar,'" + DateTime.Now.Date + "',101) AND AD.EmployeeId = " + ((clsWorkAssignments)clsWorkAssignmentsBindingSource.Current).EmployeeId.ToString());
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsActualAdvanceReferenceBindingSource.DataSource = ListOfActualAdvanceRef;
            };
            bg.RunWorkerAsync();
        }

   
    }
}
