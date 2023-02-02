using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Reflection;

namespace FGCIJOROSystem.Presentation
{
    public partial class frmChangelogs : Telerik.WinControls.UI.RadForm
    {
        public string VersionLabel
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
                else
                {
                    var ver = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
                }
            }
        }
        public frmChangelogs()
        {
            InitializeComponent();

            lblVersion.Text = VersionLabel;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
