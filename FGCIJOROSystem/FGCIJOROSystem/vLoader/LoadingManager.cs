using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.ToastNotifications;
using FGCIJOROSystem.Common;

namespace FGCIJOROSystem.Presentation.vLoader
{
    public class LoadingManager
    {
        private delegate void CloseDelegate();

        private static FrmLoader _loadingScreen;
        private static FrmLoaderV2 _loadingScreenV2;
        public static void ShowLoadScreen()
        {
            if (_loadingScreen != null)
            { return; }

            Thread thread = new Thread(new ThreadStart(ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private static void ShowForm()
        {
            var form = Application.OpenForms.OfType<FrmLoaderV2>().FirstOrDefault();
            if (form == null)
            {
                _loadingScreenV2 = new FrmLoaderV2();
                //_loadingScreenV2.StartPosition = FormStartPosition.CenterScreen;
                _loadingScreenV2.Width = 300;
                _loadingScreenV2.Height = 40;
                _loadingScreenV2.ShowDialog();
            }
        }

        public static async void CloseLoadScreen()
        {
            var form = Application.OpenForms.OfType<FrmLoaderV2>().FirstOrDefault();
            if (form != null)
            {
                _loadingScreenV2 = form;
                await Task.Run(() =>
                {
                    _loadingScreenV2.Invoke(new CloseDelegate(CloseFormInternal));
                });
            }
        }

        private static async void CloseFormInternal()
        {
            GlobalVariable.progressValue = 100;
            await Task.Delay(1000);
            _loadingScreenV2.Close();
            _loadingScreenV2.Dispose();
            _loadingScreenV2 = null;
        }
    }
}
