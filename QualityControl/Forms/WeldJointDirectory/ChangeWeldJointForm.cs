
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
using BLL.Entities;
using DAL.Repositories.Interface;
using BLL.Services.Interface;
using BLL.Services;

namespace QualityControl_Client.Forms.WeldJointDirectory
{
    public partial class ChangeWeldJointForm : ChangeForm
    {
        BllWeldJoint oldWeldJoint;
        IUnitOfWork uow;
        public ChangeWeldJointForm(IUnitOfWork uow) : base()
        {
            InitializeComponent();
            this.uow = uow;
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        }

        public ChangeWeldJointForm() : base()
        {
            InitializeComponent();
        }

        public ChangeWeldJointForm(DirectoryForm parent, BllWeldJoint oldWeldJoint, DataGridViewRow currentRow) : base(parent)
        {
            InitializeComponent();
            this.oldWeldJoint = oldWeldJoint;
            textBox1.Text = (string)currentRow.Cells[0].Value;
            richTextBox1.Text = (string)currentRow.Cells[1].Value;
            pictureBox1.Image = byteArrayToImage(oldWeldJoint.Image);

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                oldWeldJoint.Name = textBox1.Text;
                oldWeldJoint.Description = richTextBox1.Text;
                oldWeldJoint.Image = openFileDialog1.FileName != "" ? imageToByteArray(Image.FromFile(openFileDialog1.FileName)) : oldWeldJoint.Image;

                IWeldJointService Service = new WeldJointService(uow);
                Service.Update(oldWeldJoint);
                base.button2_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
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

        private Image byteArrayToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
