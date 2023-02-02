using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.Common;
using Telerik.WinControls.UI;
using FGCIJOROSystem.Presentation.vJobOrder;
using System.Deployment.Application;

namespace FGCIJOROSystem.Presentation.vMenus
{
    public partial class ucJOMenu : UserControl
    {
        public ucJOMenu()
        {
            InitializeComponent();
        }
        #region Properties
        public frmMainWindow MainWindowPage;
        #endregion
        #region Methods
        private void mJOEntry_Click(object sender, EventArgs e)
        {
          
        }
        private void mJOList_Click(object sender, EventArgs e)
        {
              
        }
        private void mJOAuditTrail_Click(object sender, EventArgs e)
        {
            
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            //Application.UseWaitCursor = true;  //chloe
            InstallUpdateSyncWithInfo();
            MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvJOList",
                Text = "Job Order Lists",
                //Image = Properties.Resources.home
            }, new ucJOList() { MainWindowPage = MainWindowPage }); 
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            //Application.UseWaitCursor = true;  //chloe
            InstallUpdateSyncWithInfo();
            ucJOEntry joEntry = new ucJOEntry();
            MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvJOEntry",
                Text = "Job Order Entry",
                //Image = Properties.Resources.home
            }, joEntry); //await Task.Run(() => joEntry)); //async

            //joEntry.DataEvent.OnDataConfirm += DataConfirm;
        }

        private void btnReleasal_Click(object sender, EventArgs e)
        {
            //Application.UseWaitCursor = true;  //chloe
            InstallUpdateSyncWithInfo();
            MainWindowPage.pvMain.ShowPages(new RadPageViewPage()
            {
                Name = "pvJOERList",
                Text = "JO Equipment Released Lists",
                //Image = Properties.Resources.home
            }, new ucJOEquipmentReleasalList());   
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
        #endregion
    }
}
