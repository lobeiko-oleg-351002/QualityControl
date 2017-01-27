using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIL.Entities;

namespace QualityControl_Client
{
    public class ControlMethodTab : TabPage
    {
        public ControlMethodTabForm tabForm;
        public ControlMethodTab(ControlMethodTabForm tabForm, UilControlName controlName)
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
