using FGCIJOROSystem.DAL.Repositories.GlobalRepo;
using FGCIJOROSystem.DAL.Repositories.JORepo;
using FGCIJOROSystem.DAL.Repositories.WorkAssignment;
using FGCIJOROSystem.Domain.JobOrder;
using FGCIJOROSystem.Domain.WorkAssignment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Transactions;
using System.Linq;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.Enums;
using FGCIJOROSystem.DAL.Repositories.ActualAdvanceRepo;
using FGCIJOROSystem.Domain.ActualAdvance;
namespace FGCIJOROSystem.Presentation.vJobOrder.vWorkAssignment
{
    public partial class frmMechanicWorkAssignment : Telerik.WinControls.UI.RadForm
    {
        #region Properties
        List<clsWorkAssignments> ListOfJOWorkAssign;
        List<clsWorkAssignments> UnselectedJOWorkAssign;
        clsMechanics Mechanics;

        JOWorkAssignmentRepository JOWorkAssignmentRepo;
        JODetailMechanicRepository JODetMechRepo;

        clsActualAdvanceDetails ActualAdvanceDet;
        List<clsActualAdvanceReference> ListOfActualAdvanceRef;

        ActualAdvanceRepository ActualAdvanceRepo;
        ActualAdvanceReferenceRepository ActualAdvanceRefRepo;
        #endregion
        #region Methods
        public frmMechanicWorkAssignment()
        {
            InitializeComponent();
            ListOfJOWorkAssign = new List<clsWorkAssignments>();
            UnselectedJOWorkAssign = new List<clsWorkAssignments>();
        }
        public frmMechanicWorkAssignment(clsMechanics obj)
        {
            InitializeComponent();
            UnselectedJOWorkAssign = new List<clsWorkAssignments>();
            Mechanics = obj;
        }
        private void frmMechanicWorkAssignment_Load(object sender, EventArgs e)
        {
            DisplayProperties();
            load();
            loadActivityUpdates();
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
        void load()
        {
            JODetMechRepo = new JODetailMechanicRepository();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += (s, e) =>
            {
                ListOfJOWorkAssign = JODetMechRepo.SearchByJobDetails(Mechanics);
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
            ListOfJOWorkAssign.ForEach(x =>
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
            tbPosition.Text = Mechanics.Position;
            radGroupBox3.HeaderText = DateTime.Now.Date.ToShortDateString() + " Activity Updates";
        }
        private void MasterTemplate_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            JOAssignment();
            radGridView1.EndEdit();
        }
        void JOAssignment()
        {
            Boolean chkSelect = (Boolean)radGridView1.CurrentRow.Cells["chkSelect"].Value;
            clsWorkAssignments JOWorkAssign = (clsWorkAssignments)clsJOWorkAssignmentsBindingSource.Current;            
            JOWorkAssign.IsActive = chkSelect;
            JOWorkAssign.EmployeeId = Mechanics.EmployeeId;
            if (JOWorkAssign.Id!=0)
            {
                UnselectedJOWorkAssign.Add(JOWorkAssign);
            }
            else
            {
                UnselectedJOWorkAssign.RemoveAll(x => x.JODetailId == JOWorkAssign.JODetailId);
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
            ListOfJOWorkAssign.Where(x => x.IsActive == true).ToList().ForEach(x =>
            {
                if (ListOfActualAdvanceRef.Where(y=> y.JODetailId == x.JODetailId).ToList().Count == 0)
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
                    JOWorkAssignmentRepo = new JOWorkAssignmentRepository();
                    foreach (var x in ListOfJOWorkAssign.Where(x=> x.IsActive == true))
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
                    foreach (var x in UnselectedJOWorkAssign.Where(x=> x.IsActive ==false))
                    {
                        if (x.Id != 0)
                        {
                            JOWorkAssignmentRepo.Update(x);
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

        private void radLabel2_Click(object sender, EventArgs e)
        {

        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }
    }
}
