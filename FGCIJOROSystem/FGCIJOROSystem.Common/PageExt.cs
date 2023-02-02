using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace FGCIJOROSystem.Common
{
    public static class PageExt
    {
        public static void LoadUsercontrol(this UserControl usercontrol)
        {
            //usercontrol.Parent.Enabled = false;
            usercontrol.Location = new Point((usercontrol.Parent.Width - usercontrol.Width) / 2, (usercontrol.Parent.Height - usercontrol.Height) / 2);
            usercontrol.BringToFront();
        }
        public static void ShowPages(this RadPageView pageview, RadPageViewPage pageviewpage, UserControl usercontrol)
        {
            var OpenPages = pageview.Pages;
            if (OpenPages[pageviewpage.Name] != null && OpenPages[pageviewpage.Name].Name == pageviewpage.Name)
            {
                pageview.SelectedPage = OpenPages[pageviewpage.Name];
            }
            else
            {
                pageview.Pages.Add(pageviewpage);
                usercontrol.Dock = DockStyle.Fill;
                pageviewpage.Controls.Add(usercontrol);
                pageview.SelectedPage = pageviewpage;
            }
        }
        public static void AddControl(this RadPageView pageview, RadPageViewPage pageviewpage, UserControl usercontrol)
        {
            usercontrol.Dock = DockStyle.Fill;
            pageviewpage.Controls.Add(usercontrol);
            pageview.SelectedPage = pageviewpage;
        }
        public static void CloseControl(this RadPageView pageview, RadPageViewPage pageviewpage, UserControl usercontrol)
        {
            pageviewpage.Item.ButtonsPanel.CloseButton.Click += CloseButton_Click;
        }

        static void CloseButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

