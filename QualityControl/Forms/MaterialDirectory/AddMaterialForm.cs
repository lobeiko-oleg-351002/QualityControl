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
    public partial class AddMaterialForm : AddForm
    {
        public AddMaterialForm() : base()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        public AddMaterialForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                BllMaterial Material = new BllMaterial
                {
                    Name = textBox1.Text,
                    Description = richTextBox1.Text
                };
                IMaterialService Service = new MaterialService(uow);
                Service.Create(Material);
                base.button2_Click(sender, e);
            }
        }


    }
}
