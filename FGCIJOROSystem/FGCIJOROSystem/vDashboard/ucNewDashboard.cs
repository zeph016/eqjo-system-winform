using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.IO;
namespace FGCIJOROSystem.Presentation.vDashboard
{
    public partial class ucNewDashboard : UserControl
    {
        Stream m_XmlFile;
        public ucNewDashboard()
        {
            InitializeComponent();
        }
        static Stream GetFromResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "FGCIJOROSystem.Presentation.Resources.underRepairOperationalStatus.xml";

            return assembly.GetManifestResourceStream(resourceName);
        }

        private async void dashboardViewer1_Load(object sender, EventArgs e)
        {
           // var watch = System.Diagnostics.Stopwatch.StartNew();

            //m_XmlFile = GetFromResources();
            //devDashboard.LoadDashboard(m_XmlFile);

            await Task.Run(() => getResources());

            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //label1.Text = $"Total execution time: {elapsedMs}";
            //label1.Visible = true;
        }
        private void getResources()
        {
            this.Invoke((System.Action)(() =>
            {
                m_XmlFile = GetFromResources();
                devDashboard.LoadDashboard(m_XmlFile);
            }
            ));          
        }
    }
}
