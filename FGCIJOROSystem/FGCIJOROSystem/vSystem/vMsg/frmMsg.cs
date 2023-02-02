using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vSystem.vMsg
{
    public partial class frmMsg : Telerik.WinControls.UI.RadForm
    {
        public clsEnums.MsgBox MsgBox;
        public String Message { get; set; }
        public frmMsg()
        {
            InitializeComponent();
        }

        private void frmMsg_Load(object sender, EventArgs e)
        {
            if (MsgBox == clsEnums.MsgBox.Success)
            {
                SuccessMessage();
            }
            else if (MsgBox == clsEnums.MsgBox.Warning)
            {
                WarningMessage();
            }
            else if (MsgBox == clsEnums.MsgBox.Error)
            {
                ErrorMessage();
            }
        }
        void SuccessMessage()
        {
            //lblTitle.Text = "SUCCESS";
            lblContent.Text = Message;
            pictureBox1.Image = FGCIJOROSystem.Presentation.Properties.Resources.Success_80;
        }
        void WarningMessage()
        {
            //lblTitle.Text = "WARNING!";
            //lblTitle.Text = "NOTICE";
            lblContent.Text = Message;
            pictureBox1.Image = FGCIJOROSystem.Presentation.Properties.Resources.Error_80;
        }
        void ErrorMessage()
        {
            //lblTitle.Text = "ERROR";
            lblContent.Text = Message;
            pictureBox1.Image = FGCIJOROSystem.Presentation.Properties.Resources.Error_80;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
