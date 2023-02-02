using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Presentation.vReport.vForms;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.Reports.rReports;
using FGCIJOROSystem.Domain.Reports;
using FGCIJOROSystem.DAL.Repositories.Reports;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Domain.Configurations.Tariff;
using FGCIJOROSystem.DAL.Repositories.ConfigRepo;
using FGCIJOROSystem.Domain.Configurations.Personnels;
using FGCIJOROSystem.DAL.Repositories;
using FGCIJOROSystem.Domain.Monitoring;
using FGCIJOROSystem.DAL.Repositories.vMonitoringRepo;
using FGCIJOROSystem.Reports.rAttendance;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using FGCIJOROSystem.Domain.ActualAdvance;
using FGCIJOROSystem.DAL.Repositories.ActualAdvanceRepo;
using FGCIJOROSystem.Presentation.vConfiguration.AuditTrail;
using System.Deployment.Application;

namespace FGCIJOROSystem.Presentation.vReport.vReports
{
    public partial class ucReportList : UserControl
    {
        #region Properties
        public frmMainWindow MainWindowPage { get; set; }
        //frmMainWindow MainWindowpage();
        #endregion
        #region Method
        public ucReportList()
        {
            InitializeComponent();
        }
        private void btnSectionSummary_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmJOROSectionSummary JOROReport = new frmJOROSectionSummary();
            JOROReport.ShowDialog();

        }  
        #endregion

        private void btnTariffSummary_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmSelectTariff Select = new frmSelectTariff();
            Select.ShowDialog();
        }

        private void btnPersonnel_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmSelectSectionPersonnel Select = new frmSelectSectionPersonnel();
            Select.ShowDialog();
        }

        private void btnMonitoring_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmMonitoringSelectOption MSO = new frmMonitoringSelectOption();
            MSO.ShowDialog();
        }

        private void btnMechanics_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmSelectActualAdvanceAttendanceDate SelectActualDate = new frmSelectActualAdvanceAttendanceDate();
            SelectActualDate.ShowDialog();
            //frmMsg msgPage = new frmMsg() { MsgBox = Domain.Enums.clsEnums.MsgBox.Warning, Message = "THIS TRANSACTION IS UNDER CONSTRUCTION" };
            //msgPage.ShowDialog();
        }

        private void btnEquipmentHistory_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmEquipmentList EL = new frmEquipmentList();
            EL.ShowDialog();
        }

        private void btnPartsRequest_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmSelectJOROPRSList JOROPRSPage = new frmSelectJOROPRSList(); //frmSelectJOROPRSList JOROPRSPage = new frmSelectJOROPRSList();
            JOROPRSPage.ShowDialog();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmAuditTrailSelectOption AT = new frmAuditTrailSelectOption();
            AT.ShowDialog();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmFilterEquipmentUR FEUR = new frmFilterEquipmentUR();
            FEUR.ShowDialog();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            InstallUpdateSyncWithInfo();
            frmPPEStatus PPE = new frmPPEStatus();
            PPE.ShowDialog();
        }
        private void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {

                    if (!info.IsUpdateRequired)
                    {
                        Boolean doUpdate = true;

                        DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now? Note: Please save your current transaction to prevent loss of data.", "Update Available", MessageBoxButtons.YesNo);
                        if (!(dr == DialogResult.Yes))
                        {
                            doUpdate = false;
                        }
                        else
                        {
                            try
                            {
                                ad.Update();
                                MessageBox.Show("The application has been upgraded, and will now restart.");
                                Application.Restart();
                            }
                            catch (DeploymentDownloadException dde)
                            {
                                MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                                return;
                            }
                            //// Display a message that the app MUST reboot. Display the minimum required version.
                            //MessageBox.Show("This application has detected a mandatory update from your current " +
                            //    "version to version " + info.MinimumRequiredVersion.ToString() +
                            //    ". The application will now install the update and restart.",
                            //    "Update Available", MessageBoxButtons.OK,
                            //    MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
    }
}