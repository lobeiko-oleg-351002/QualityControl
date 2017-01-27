using QualityControl_Client.Forms.EquipmentDirectory;
using QualityControl_Client.Forms.MaterialDirectory;
using QualityControl_Client.Forms.RequirementDocumentationDirectory;
using QualityControl_Client.Forms.WeldJointDirectory;
using ServerWcfService.Services.Interface;
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
using UIL.Entities;

namespace QualityControl_Client.Forms.TemplateDirectory
{
    public partial class ChangeTemplateForm : ChangeForm
    {

        List<Image> imagesForPicturebox = new List<Image>();
        UilEquipmentLib equipmentLib = null;
        UilMaterial material;
        UilWeldJoint weldJoint = null;
        UilImageLib imageLib;
        UilControlNameLib controlNameLib;
        UilTemplate oldTemplate;
        IEnumerable<UilControlName> controlNames;
        UilRequirementDocumentationLib requirementDocumentationLib = new UilRequirementDocumentationLib();
        int currentPositionInImages = 0;

        public ChangeTemplateForm() : base()
        {
            InitializeComponent();
        }
        public ChangeTemplateForm(DirectoryForm parent, UilTemplate oldTemplate) : base(parent)
        {
            InitializeComponent();
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            IControlNameRepository controlNameRepository = ServiceChannelManager.Instance.ControlNameRepository;
            controlNames = controlNameRepository.GetAll();
            foreach (var name in controlNames)
            {
                checkedListBox1.Items.Add(name.Name);
            }
            this.oldTemplate = oldTemplate;

            material = oldTemplate.Material;
            weldJoint = oldTemplate.WeldJoint;
            equipmentLib = oldTemplate.EquipmentLib;
            imageLib = oldTemplate.ImageLib;
            controlNameLib = oldTemplate.ControlNameLib;
            requirementDocumentationLib = oldTemplate.RequirementDocumentationLib;

            if (requirementDocumentationLib != null)
            {
                foreach (var documentation in requirementDocumentationLib.SelectedRequirementDocumentation)
                {
                    listBox1.Items.Add(documentation.RequirementDocumentation.Name);
                }
            }


            if (equipmentLib != null)
            {
                foreach (var eq in equipmentLib.SelectedEquipment)
                {
                    listBox2.Items.Add(eq.Equipment.Name);
                }
            }

            if (imageLib != null)
            {
                foreach (UilImage image in imageLib.Image)
                {
                    imagesForPicturebox.Add(byteArrayToImage(image.Image));
                }
            }

            if (controlNameLib != null)
            {
                foreach (UilSelectedControlName selectedName in controlNameLib.SelectedControlName)
                {
                    foreach(var name in controlNames)
                    {
                        if (selectedName.ControlName.Name == name.Name)
                        {
                            int index = checkedListBox1.Items.IndexOf(name.Name);
                            if (index != -1)
                            {
                                checkedListBox1.SetItemChecked(index, true);
                            }
                            
                        }
                    }
                }
            }
            
            textBox1.Text = oldTemplate.Name;
            richTextBox1.Text = oldTemplate.Description;
            textBox2.Text = material != null ? material.Name : "";
            textBox3.Text = weldJoint != null ? weldJoint.Name : "";
            pictureBox1.Image = imagesForPicturebox.Count != 0 ? imagesForPicturebox[0] : null;

        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите наименование", "Оповещение");
            }
            else
            {
                UilControlNameLib controlNameLib = new UilControlNameLib();
                controlNameLib.Id = oldTemplate.ControlNameLib.Id;
                var checkedIndexes = checkedListBox1.CheckedIndices.Cast<int>().ToArray();
                foreach (var index in checkedIndexes)
                {
                    controlNameLib.SelectedControlName.Add(new UilSelectedControlName { ControlName = controlNames.ElementAt(index) });
                }
                UilTemplate Template = new UilTemplate
                {
                    Name = textBox1.Text,
                    Description = richTextBox1.Text,
                    Material = material,
                    WeldJoint = weldJoint,
                    EquipmentLib = equipmentLib,
                    ImageLib = imageLib,
                    ControlNameLib = controlNameLib,
                    RequirementDocumentationLib = requirementDocumentationLib,
                    Id = oldTemplate.Id
                };
                ITemplateRepository repository = ServiceChannelManager.Instance.TemplateRepository;
                repository.Update(Template);
                base.button2_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseMaterialForm chooseMaterialForm = new ChooseMaterialForm();
            chooseMaterialForm.ShowDialog(this);
            material = chooseMaterialForm.GetChosenMaterial();
            if (material != null)
            {
                textBox2.Text = material.Name;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChooseWeldJointForm chooseWeldJointForm = new ChooseWeldJointForm();
            chooseWeldJointForm.ShowDialog(this);
            weldJoint = chooseWeldJointForm.GetChosenWeldJoint();
            if (weldJoint != null)
            {
                textBox3.Text = weldJoint.Name;
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            //IImageRepository imageRepository = ServiceChannelManager.Instance.ImageRepository;
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                var image = new UilImage
                {
                    Image = imageToByteArray(Image.FromFile(openFileDialog1.FileName))
                };
                imageLib.Image.Add(image);
                //imageRepository.Create(image);
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

        private Image byteArrayToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                return Image.FromStream(ms);
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
            imagesForPicturebox.RemoveAt(currentPositionInImages);
            IImageRepository imageRepository = ServiceChannelManager.Instance.ImageRepository;
            imageRepository.Delete(imageLib.Image[currentPositionInImages]);
            imageLib.Image.RemoveAt(currentPositionInImages);
            if (currentPositionInImages > 0)
            {
                currentPositionInImages--;
            }
            pictureBox1.Image = imagesForPicturebox[currentPositionInImages];
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ChooseRequirementDocumentationForm RequirementDocumentationForm = new ChooseRequirementDocumentationForm();
            RequirementDocumentationForm.ShowDialog(this);
            UilRequirementDocumentation requirementDocumentation = RequirementDocumentationForm.GetChosenRequirementDocumentation();
            if (requirementDocumentation != null)
            {
                requirementDocumentationLib.SelectedRequirementDocumentation.Add(new UilSelectedRequirementDocumentation { RequirementDocumentation = requirementDocumentation });
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
            ChooseEquipmentForm EquipmentForm = new ChooseEquipmentForm();
            EquipmentForm.ShowDialog(this);
            UilEquipment Equipment = EquipmentForm.GetChosenEquipment();
            if (Equipment != null)
            {
                if (equipmentLib == null)
                {
                    equipmentLib = new UilEquipmentLib();
                }
                equipmentLib.SelectedEquipment.Add(new UilSelectedEquipment { Equipment = Equipment });
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
