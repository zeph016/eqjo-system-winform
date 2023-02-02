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
namespace FGCIJOROSystem.Presentation.vRescueOrder.vROWorkAssignment
{
    public partial class frmROMechanicWorkAssignment : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        List<clsWorkAssignments> ListOfROWorkAssign;
        List<clsWorkAssignments> UnselectedROWorkAssign;
        clsROMechanics Mechanics;

        ROWorkAssignmentRepository ROWorkAssignmentRepo;
        RODetailMechanicRepository RODetMechRepo;

        clsActualAdvanceDetails ActualAdvanceDet;
        List<clsActualAdvanceReference> ListOfActualAdvanceRef;

        ActualAdvanceRepository ActualAdvanceRepo;
        ActualAdvanceReferenceRepository ActualAdvanceRefRepo;
        #endregion
        public frmROMechanicWorkAssignment()
        {
            InitializeComponent();
        }
        public frmROMechanicWorkAssignment(clsROMechanics obj)
        {
            InitializeComponent();
            UnselectedROWorkAssign = new List<clsWorkAssignments>();
            Mechanics = obj;
        }

        private void frmROMechanicWorkAssignment_Load(object sender, EventArgs e)
        {
            load();
            DisplayProperties();
            loadActivityUpdates();
        }
        void load()
        {
            RODetMechRepo = new RODetailMechanicRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ListOfROWorkAssign = RODetMechRepo.SearchByJobDetails(Mechanics);
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsWorkAssignmentsBindingSource.DataSource = ListOfROWorkAssign;
                DisplayWorkAssign();
            };
            bg.RunWorkerAsync();
        }
        void loadActivityUpdates()
        {
            ActualAdvanceRefRepo = new ActualAdvanceReferenceRepository();
            ListOfActualAdvanceRef = new List<clsActualAdvanceReference>();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ListOfActualAdvanceRef = ActualAdvanceRefRepo.SearchBy(" WHERE AD.DateOfUpdate = CONVERT(varchar,'" + DateTime.Now.Date + "',101) AND AD.EmployeeId = " + Mechanics.EmployeeId.ToString());
            };
            bg.RunWorkerCompleted += (s, e) =>
            {
                clsActualAdvanceReferenceBindingSource.DataSource = ListOfActualAdvanceRef;
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
            tbMechanicName.Text = Mechanics.FullName;
        }
        private void MasterTemplate_CellEndEdit(object sender, EventArgs e)
        {
            JOAssignment();
        }
        void JOAssignment()
        {
            Boolean chkSelect = (Boolean)radGridView1.CurrentRow.Cells["chkSelect"].Value;
            clsWorkAssignments JOWorkAssign = (clsWorkAssignments)clsWorkAssignmentsBindingSource.Current;
            JOWorkAssign.IsActive = chkSelect;
            JOWorkAssign.EmployeeId = Mechanics.EmployeeId;
            if (JOWorkAssign.Id != 0)
            {
                UnselectedROWorkAssign.Add(JOWorkAssign);
            }
            else
            {
                UnselectedROWorkAssign.RemoveAll(x => x.JODetailId == JOWorkAssign.JODetailId);
            }
        }
        clsActualAdvanceDetails MapProperties()
        {
            ActualAdvanceDet = new clsActualAdvanceDetails();
            ActualAdvanceDet.EmployeeId = Mechanics.EmployeeId;
            ActualAdvanceDet.BranchId = Program.CurrentUser.BranchId;
            ActualAdvanceDet.Type = clsEnums.ActualAdvance.Actual;
            ActualAdvanceDet.DateOfUpdate = DateTime.Now;
            ActualAdvanceDet.AMStatus = 3;
            ActualAdvanceDet.PMStatus = 3;
            ListOfROWorkAssign.Where(x => x.IsActive == true).ToList().ForEach(x =>
            {
                if (ListOfActualAdvanceRef.Where(y => y.JODetailId == x.JODetailId).ToList().Count == 0)
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
                }
            });
            return ActualAdvanceDet;
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

                    var List = ActualAdvanceRepo.SearchBy(" WHERE DateOfUpdate = CONVERT(varchar,'" + MappedProperties.DateOfUpdate + "',101) AND EmployeeId =" + MappedProperties.EmployeeId).FirstOrDefault();
                    if (List == null)
                    {
                        ActualAdvanceRepo.Add(MappedProperties);
                    }
                    else
                    {
                        List.ListOfActualReference = MappedProperties.ListOfActualReference;
                        ActualAdvanceRepo.Update(List);
                    }               
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

        private void radGroupBox2_Click(object sender, EventArgs e)
        {

        }            
    }
}
