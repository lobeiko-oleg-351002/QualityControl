using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QualityControl_Client.Forms.WeldJointDirectory
{
    public partial class AddWeldJointForm : AddForm
    {
        IUnitOfWork uow;
        public AddWeldJointForm() : base()
        {
            InitializeComponent();
        }
        public AddWeldJointForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                BllWeldJoint WeldJoint = new BllWeldJoint
                {
                    Name = textBox1.Text,
                    Description = richTextBox1.Text,
                    Image = pictureBox1.Image != null ? imageToByteArray(Image.FromFile(openFileDialog1.FileName)) : null,
                };
                IWeldJointService Service = new WeldJointService(uow);
                Service.Create(WeldJoint);
                base.button2_Click(sender, e);
            }
        }

        private byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           if (DialogResult.OK == openFileDialog1.ShowDialog())
           {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
           }
        }
    }
}
