using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client
{
    public class ControlMethodTab : TabPage
    {
        public ControlMethodTabForm tabForm;
        public ControlMethodTab(ControlMethodTabForm tabForm, BllControlName controlName)
        {
            this.tabForm = tabForm;
            Panel formPanel = tabForm.panel;
            Controls.Add(formPanel);
            this.BackColor = tabForm.BackColor;
            this.Text = controlName.Name;
            this.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
        }


    }
}
