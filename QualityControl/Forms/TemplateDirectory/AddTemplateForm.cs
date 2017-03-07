using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using QualityControl_Client.Forms.EquipmentDirectory;
using QualityControl_Client.Forms.MaterialDirectory;
using QualityControl_Client.Forms.RequirementDocumentationDirectory;
using QualityControl_Client.Forms.WeldJointDirectory;
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


namespace QualityControl_Client.Forms.TemplateDirectory
{
    public partial class AddTemplateForm : AddForm
    {
        List<Image> imagesForPicturebox = new List<Image>();
        BllEquipmentLib equipmentLib = new BllEquipmentLib();
        BllMaterial material;
        BllWeldJoint weldJoint = null;
        BllImageLib imageLib = new BllImageLib();
        IEnumerable<BllControlName> controlNames;
        BllRequirementDocumentationLib requirementDocumentationLib = new BllRequirementDocumentationLib();
        IUnitOfWork uow;
        int currentPositionInImages = 0;

        public AddTemplateForm() : base()
        {
            InitializeComponent();
        }
        public AddTemplateForm(DirectoryForm parent, IUnitOfWork uow) : base(parent)
        {
            InitializeComponent();
            this.uow = uow;
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            IControlNameService controlNameService = new ControlNameService(uow);
            controlNames = controlNameService.GetAll();
            foreach (var name in controlNames)
            {
                checkedListBox1.Items.Add(name.Name);
            }

        }     

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                BllControlNameLib controlNameLib = new BllControlNameLib();
                var checkedIndexes = checkedListBox1.CheckedIndices.Cast<int>().ToArray();
                foreach (var index in checkedIndexes)
                {
                    controlNameLib.SelectedControlName.Add(new BllSelectedControlName { ControlName = controlNames.ElementAt(index) });
                }

                BllTemplate Template = new BllTemplate
                {
                    Name = textBox1.Text,
                    Description = richTextBox1.Text,
                    Material = material,
                    WeldJoint = weldJoint,
                    EquipmentLib = equipmentLib,
                    ImageLib = imageLib,
                    ControlNameLib = controlNameLib,
                    RequirementDocumentationLib = requirementDocumentationLib
                };
                ITemplateService Service = new TemplateService(uow);
                Service.Create(Template);
                base.button2_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            ChooseMaterialForm chooseMaterialForm = new ChooseMaterialForm(uow);
            chooseMaterialForm.ShowDialog(this);
            material = chooseMaterialForm.GetChosenMaterial();
            if (material != null)
            {
                textBox2.Text = material.Name;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {           
            ChooseWeldJointForm chooseWeldJointForm = new ChooseWeldJointForm(uow);
            chooseWeldJointForm.ShowDialog(this);
            weldJoint = chooseWeldJointForm.GetChosenWeldJoint();
            if (weldJoint != null)
            {
                textBox3.Text = weldJoint.Name;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                imageLib.Image.Add(
                    new BllImage
                    {
                        Image = imageToByteArray(Image.FromFile(openFileDialog1.FileName))
                    });
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                imagesForPicturebox.Add(Image.FromFile(openFileDialog1.FileName));
                currentPositionInImages = imagesForPicturebox.Count - 1; 
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

        private void button7_Click(object sender, EventArgs e)
        {
            if (currentPositionInImages > 0)
            {
                currentPositionInImages--;
                pictureBox1.Image = imagesForPicturebox[currentPositionInImages];
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (currentPositionInImages < imagesForPicturebox.Count - 1)
            {
                currentPositionInImages++;
                pictureBox1.Image = imagesForPicturebox[currentPositionInImages];
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (imagesForPicturebox.Count > 0)
            {
                imagesForPicturebox.RemoveAt(currentPositionInImages);
                if (currentPositionInImages > 0)
                {
                    currentPositionInImages--;
                }
                if (imagesForPicturebox.Count > 0)
                {
                    pictureBox1.Image = imagesForPicturebox[currentPositionInImages];
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ChooseRequirementDocumentationForm RequirementDocumentationForm = new ChooseRequirementDocumentationForm(uow);
            RequirementDocumentationForm.ShowDialog(this);
            BllRequirementDocumentation requirementDocumentation = RequirementDocumentationForm.GetChosenRequirementDocumentation();
            if (requirementDocumentation != null)
            {
                requirementDocumentationLib.SelectedRequirementDocumentation.Add(new BllSelectedRequirementDocumentation { RequirementDocumentation = requirementDocumentation });
                listBox1.Items.Add(requirementDocumentation.Name);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            requirementDocumentationLib.SelectedRequirementDocumentation.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ChooseEquipmentForm EquipmentForm = new ChooseEquipmentForm(uow);
            EquipmentForm.ShowDialog(this);
            BllEquipment Equipment = EquipmentForm.GetChosenEquipment();
            if (Equipment != null)
            {
                equipmentLib.SelectedEquipment.Add(new BllSelectedEquipment { Equipment = Equipment });
                listBox2.Items.Add(Equipment.Name);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            equipmentLib.SelectedEquipment.RemoveAt(listBox2.SelectedIndex);
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }
    }
}
