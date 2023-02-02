using FGCIJOROSystem.Domain.Configurations.Users;
using FGCIJOROSystem.Presentation.vDashboard;
using FGCIJOROSystem.Presentation.vSystem.vLogin;
using FGCIJOROSystem.Presentation.vSystem.vMain;
using FGCIJOROSystem.Presentation.vSystem.vMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation
{
    static class Program
    {
        public static clsUser CurrentUser { get; set; }
        public static clsUserLevel UserLevel { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMainWindow());
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            //RadMessageBox.ThemeName = "Windows8";
            //RadMessageBox.Show(e.Exception.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            frmMsg MessageBox = new frmMsg() 
            { 
                MsgBox = Domain.Enums.clsEnums.MsgBox.Error,
                Message = e.Exception.Message
            };
            MessageBox.ShowDialog();
        }

        public static string ReportHeaderImagePath
        {
            get
            {
                return "";//Application.StartupPath + "\\Logo_Only_FGCI_with_Border.jpg";
            }
        }
    }
}
