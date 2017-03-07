using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms.MaterialDirectory
{
    public partial class ChangeMaterialForm : ChangeForm
    {
        BllMaterial oldMaterial;
        public ChangeMaterialForm() : base()
        {
            InitializeComponent();
        }

        IUnitOfWork uow;

        public ChangeMaterialForm(DirectoryForm parent, BllMaterial oldMaterial, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            this.oldMaterial = oldMaterial;
            textBox1.Text = oldMaterial.Name;
            richTextBox1.Text = oldMaterial.Description;

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldMaterial.Name = textBox1.Text;
                oldMaterial.Description = richTextBox1.Text;
                IMaterialService Service = new MaterialService(uow);
                Service.Update(oldMaterial);
                base.button2_Click(sender, e);
            }
        }
    }
}
