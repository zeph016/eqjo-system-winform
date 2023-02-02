using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace FGCIJOROSystem.Common
{
    public static class clsDesign
    {
        public static void Grid_CellFormatting(this RadGridView GridView, object sender, CellFormattingEventArgs e, String ToolTipName)
        {
            GridDataCellElement cell = (GridDataCellElement)sender;
            GridCommandCellElement btn = (GridCommandCellElement)e.CellElement;
            btn.CommandButton.ToolTipText = ToolTipName;
            btn.CommandButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            
            /*if (cell.ColumnInfo.Name == "btnUpdate")
            {
                GridCommandCellElement btn = (GridCommandCellElement)e.CellElement;
                if (btn.ColumnInfo.Name == "btnUpdate")
                {
                    btn.CommandButton.ToolTipText = ToolTipName;
                    btn.CommandButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                }
            }
            if (cell.ColumnInfo.Name == "Checklist")
            {
                GridCommandCellElement btn = (GridCommandCellElement)e.CellElement;
                if (btn.ColumnInfo.Name == "Checklist")
                {
                    btn.CommandButton.ToolTipText = ToolTipName;
                    btn.CommandButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                }
            }
            if (cell.ColumnInfo.Name == "Equipment")
            {
                GridCommandCellElement btn = (GridCommandCellElement)e.CellElement;
                if (btn.ColumnInfo.Name == "Equipment")
                {
                    btn.CommandButton.ToolTipText = ToolTipName;
                    btn.CommandButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                }
            }
            if (cell.ColumnInfo.Name == "GenerateChecklist")
            {
                GridCommandCellElement btn = (GridCommandCellElement)e.CellElement;
                if (btn.ColumnInfo.Name == "GenerateChecklist")
                {
                    btn.CommandButton.ToolTipText = ToolTipName;
                    btn.CommandButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                }
            }
            if (cell.ColumnInfo.Name == "PrintLogs")
            {
                GridCommandCellElement btn = (GridCommandCellElement)e.CellElement;
                if (btn.ColumnInfo.Name == "PrintLogs")
                {
                    btn.CommandButton.ToolTipText = ToolTipName;
                    btn.CommandButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                }
            }
            if (cell.ColumnInfo.Name == "btnTransactionLogs")
            {
                GridCommandCellElement btn = (GridCommandCellElement)e.CellElement;
                if (btn.ColumnInfo.Name == "btnTransactionLogs")
                {
                    btn.CommandButton.ToolTipText = ToolTipName;
                    btn.CommandButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                }
            }
            if (cell.ColumnInfo.Name == "btnSave")
            {
                GridCommandCellElement btn = (GridCommandCellElement)e.CellElement;
                if (btn.ColumnInfo.Name == "btnSave")
                {
                    btn.CommandButton.ToolTipText = ToolTipName;
                    btn.CommandButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                }
            }
            if (cell.ColumnInfo.Name == "btnSelect")
            {
                GridCommandCellElement btn = (GridCommandCellElement)e.CellElement;
                if (btn.ColumnInfo.Name == "btnSelect")
                {
                    btn.CommandButton.ToolTipText = ToolTipName;
                    btn.CommandButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                }
            }*/
        }
        public static void Grid_CellFormatting(this RadGridView GridView, object sender, CellFormattingEventArgs e)
        {
            RadProgressBarElement progressBarElement;
                if (e.CellElement.Children.Count == 0)
                {
                    progressBarElement = new RadProgressBarElement();
                    e.CellElement.Children.Add(progressBarElement);
                    progressBarElement.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    
 
                }
                else
                {
                    progressBarElement = e.CellElement.Children[0] as RadProgressBarElement;
                }
                progressBarElement.StretchHorizontally = true;
                progressBarElement.StretchVertically = true; 
                int value = 0;
                if (e.CellElement.Value != null)
                {
                    try
                    {
                        Int32.TryParse(((GridDataCellElement)e.CellElement).Value.ToString(), out value);
                    }
                    catch
                    {
                        value = 0;
                    }
                }
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 100)
                {
                    value = 100;
                }
                progressBarElement.Value1 = value;
                progressBarElement.Text = value.ToString() + "%";
                e.CellElement.DrawText = false;
            }
    
        
    }
}
