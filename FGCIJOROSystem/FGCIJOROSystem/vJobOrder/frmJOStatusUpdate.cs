using FGCIJOROSystem.Domain.JobOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace FGCIJOROSystem.Presentation.vJobOrder
{
    public partial class frmJOStatusUpdate : Telerik.WinControls.UI.RadForm
    {
        public frmJOStatusUpdate(clsJobOrder obj)
        {
            InitializeComponent();
        }
    }
}
