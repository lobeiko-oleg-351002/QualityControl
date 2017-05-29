using QualityControl_Client.Forms.ControlMethodDocumentationDirectory;
using QualityControl_Client.Forms.EmployeeDirectory;
using QualityControl_Client.Forms.EquipmentDirectory;
using QualityControl_Client.Forms.RequirementDocumentationDirectory;
using QualityControl_Client.Forms.ResultDirectory;

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
using DAL.Repositories.Interface;
using BLL.Entities;
using DAL.Repositories;
using BLL.Services.Interface;
using BLL.Services;

namespace QualityControl_Client
{
    public partial class ControlMethodTabForm : Form
    {
        public Panel panel;
        public BllControl currentControl { get; private set; }
        private BllJournal currentJournal;
        public bool isControlled = false; // проведено ли
        public bool? isRejected = null;
        public ControlMethodTabForm()
        {
            InitializeComponent();

        }

        public ControlMethodTabForm(string controlName, IUnitOfWork uow, AddJournalForm parent)
        {
            InitializeComponent();
            addJournalForm = parent;
            this.uow = uow;
            panel = panel1;
            label1.Text = controlName;
            DisableFormControls();
            
        }

        IUnitOfWork uow;
        AddJournalForm addJournalForm;
        public ControlMethodTabForm(BllControl control, BllJournal journal, IUnitOfWork uow, AddJournalForm parent)
        {
            InitializeComponent();
            addJournalForm = parent;
            this.uow = uow;
            panel = panel1;
            SetCurrentControl(control, journal);
            DisableFormControls();
        }


        public void SetCurrentControl(BllControl control, BllJournal journal)
        {
            currentControl = control;
            currentJournal = journal;
            label1.Text = control.ControlName.Name;
            label11.Text = control.ProtocolNumber.ToString();
            imagesForPicturebox.Clear();
            if (control.IsControlled != null)
            {
                isControlled = true;
            }
            else
            {
                isControlled = false;
            }
            SetEquipment(control.EquipmentLib);
            SetImages(control.ImageLib);
            SetRequirementDocumentation(control.RequirementDocumentationLib);
            SetEmployee(control.EmployeeLib);
        }

        public void EnableValidateCheckBox()
        {
            checkBox1.Enabled = true;
        }

        public void FillComponents()
        {
            if (currentControl.IsControlled != null)
            {
                checkBox1.Checked = true;
                SetControlCheck(currentControl.IsControlled.Value);
            }
            if (currentControl.ControlMethodDocumentationLib != null)
            {
                SetControlMethodDocumentation(currentControl.ControlMethodDocumentationLib);
            }
            if (currentControl.RequirementDocumentationLib != null)
            {
                SetRequirementDocumentation(currentControl.RequirementDocumentationLib);
            }
            SetLight(currentControl.Light != null ? (float)currentControl.Light : 0);
            SetAdditionally(currentControl.Additionally);
            if (currentControl.EquipmentLib != null)
            {
                SetEquipment(currentControl.EquipmentLib);
            }
            if (currentControl.EmployeeLib != null)
            {
                SetEmployee(currentControl.EmployeeLib);
            }
            SetResult(currentControl.ResultLib);
        }

        public void SetControlCheck(bool isControlled)
        {
            
            if (isControlled)
            {
                currentControl.IsControlled = true;
                radioButton2.Checked = true;
                isRejected = false;                
            }
            else
            {
                currentControl.IsControlled = false;
                radioButton1.Checked = true;
                isRejected = true;
            }
        }

        public void SetControlMethodDocumentation(BllControlMethodDocumentationLib documentationLib)
        {
            currentControl.ControlMethodDocumentationLib = documentationLib;
            if (documentationLib != null)
            {
                foreach(var documentation in documentationLib.SelectedControlMethodDocumentation)
                {
                    listBox2.Items.Add(documentation.ControlMethodDocumentation.Name);
                }
            }
        }

        public void SetRequirementDocumentation(BllRequirementDocumentationLib documentationLib)
        {
            currentControl.RequirementDocumentationLib = documentationLib;
            listBox3.Items.Clear();
            if (documentationLib != null)
            {
                foreach (var documentation in documentationLib.SelectedRequirementDocumentation)
                {
                    listBox3.Items.Add(documentation.RequirementDocumentation.Name);
                }
            }
        }

        public void CopyNewRequirementDocumentationFromLib(BllRequirementDocumentationLib documentationLib)
        {
            currentControl.RequirementDocumentationLib.SelectedRequirementDocumentation.Clear();
            foreach (var doc in documentationLib.SelectedRequirementDocumentation)
            {
                currentControl.RequirementDocumentationLib.SelectedRequirementDocumentation.Add(new BllSelectedRequirementDocumentation { RequirementDocumentation = doc.RequirementDocumentation });
            }
            SetRequirementDocumentation(currentControl.RequirementDocumentationLib);
        }

        public void SetLight(float light)
        {
            textBox3.Text = light.ToString();
        }

        public void SetAdditionally(string additionally)
        {
            textBox4.Text = additionally;
        }

        public void SetEquipment(BllEquipmentLib equipmentLib)
        {
            currentControl.EquipmentLib = equipmentLib;
            listBox1.Items.Clear();
            foreach (var equipment in equipmentLib.SelectedEquipment)
            {
                listBox1.Items.Add(equipment.Equipment.Name);              
            }

        }

        public void CopyNewEquipmentFromLib(BllEquipmentLib equipmentLib)
        {
            currentControl.EquipmentLib.SelectedEquipment.Clear();
            foreach (var eq in equipmentLib.SelectedEquipment)
            {
                currentControl.EquipmentLib.SelectedEquipment.Add(new BllSelectedEquipment { Equipment = eq.Equipment });
            }
            SetEquipment(currentControl.EquipmentLib);
        }

        public void SetImages(BllImageLib lib)
        {
            currentControl.ImageLib = lib;
            imagesForPicturebox.Clear();
            foreach (var image in currentControl.ImageLib.Image)
            {
                imagesForPicturebox.Add(byteArrayToImage(image.Image));
            }
            if (imagesForPicturebox.Count > 0)
            {
                pictureBox1.Image = imagesForPicturebox[0];
            }            
            currentPositionInImages = 0;
        }

        public void CopyNewImagesFromLib(BllImageLib lib)
        {
            currentControl.ImageLib.Image.Clear();
            foreach (var img in lib.Image)
            {
                currentControl.ImageLib.Image.Add(new BllImage { Image = img.Image });
            }
            SetImages(currentControl.ImageLib);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseControlMethodDocumentationForm ControlMethodDocumentationForm = new ChooseControlMethodDocumentationForm(uow);
            ControlMethodDocumentationForm.ShowDialog(this);
            BllControlMethodDocumentation controlMethodDocumentation = ControlMethodDocumentationForm.GetChosenControlMethodDocumentation();
            if (controlMethodDocumentation != null)
            {
                currentControl.ControlMethodDocumentationLib.SelectedControlMethodDocumentation.Add(new BllSelectedControlMethodDocumentation { ControlMethodDocumentation = controlMethodDocumentation });
                listBox2.Items.Add(controlMethodDocumentation.Pressmark + " " + controlMethodDocumentation.Name);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            ChooseRequirementDocumentationForm RequirementDocumentationForm = new ChooseRequirementDocumentationForm(uow);
            RequirementDocumentationForm.ShowDialog(this);
            BllRequirementDocumentation requirementDocumentation = RequirementDocumentationForm.GetChosenRequirementDocumentation();
            if (requirementDocumentation != null)
            {
                currentControl.RequirementDocumentationLib.SelectedRequirementDocumentation.Add(new BllSelectedRequirementDocumentation { RequirementDocumentation = requirementDocumentation });
                listBox3.Items.Add(requirementDocumentation.Pressmark + " " + requirementDocumentation.Name);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChooseEquipmentForm EquipmentForm = new ChooseEquipmentForm(uow);
            EquipmentForm.ShowDialog(this);
            BllEquipment equipment = EquipmentForm.GetChosenEquipment();
            if (equipment != null)
            {
                currentControl.EquipmentLib.SelectedEquipment.Add(new BllSelectedEquipment {Equipment = equipment} );
                listBox1.Items.Add(equipment.Name);
            }
        }

        public void AddEmployee(BllEmployee employee)
        {
            if (employee != null)
            {
                currentControl.EmployeeLib.SelectedEmployee.Add(new BllSelectedEmployee { Employee = employee });
                listBox4.Items.Add(employee.Sirname + " " + employee.Name + " " + employee.Fathername);
            }
        }

        public void SetEmployee(BllEmployeeLib employeeLib)
        {
            currentControl.EmployeeLib = employeeLib;
            listBox4.Items.Clear();
            if (employeeLib != null)
            {
                foreach (var employee in employeeLib.SelectedEmployee)
                {
                    listBox4.Items.Add(employee.Employee.Sirname + " " + employee.Employee.Name + " " + employee.Employee.Fathername);
                }
            }
        }

        public void CopyNewEmployeeFromLib(BllEmployeeLib EmployeeLib)
        {
            currentControl.EmployeeLib.SelectedEmployee.Clear();
            foreach (var eq in EmployeeLib.SelectedEmployee)
            {
                currentControl.EmployeeLib.SelectedEmployee.Add(new BllSelectedEmployee { Employee = eq.Employee });
            }
            SetEmployee(currentControl.EmployeeLib);
        }

        public void SetResult(BllResultLib ResultLib)
        {
            currentControl.ResultLib = ResultLib;
        }

        public void CopyNewResultFromLib(BllResultLib ResultLib)
        {
            currentControl.ResultLib.Result.Clear();
            foreach (var res in ResultLib.Result)
            {
                currentControl.ResultLib.Result.Add(new BllResult { Welder = res.Welder, Mark = res.Mark});
            }
            SetResult(currentControl.ResultLib);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChooseEmployeeForm EmployeeForm = new ChooseEmployeeForm(uow);
            EmployeeForm.ShowDialog(this);
            BllEmployee employee = EmployeeForm.GetChosenEmployee();
            if (employee != null)
            {
                currentControl.EmployeeLib.SelectedEmployee.Add(new BllSelectedEmployee { Employee = employee });
                listBox4.Items.Add(employee.Sirname + " " + employee.Name + " " + employee.Fathername);
            }
            addJournalForm.AddCurrentEmployeeLibToAllMethods();
        }

        List<Image> imagesForPicturebox = new List<Image>();
        int currentPositionInImages = 0;

        private void button11_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                var image = new BllImage
                {
                    Image = imageToByteArray(Image.FromFile(openFileDialog1.FileName))
                };
                currentControl.ImageLib.Image.Add(image);
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

        private void button10_Click(object sender, EventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
        {
            if (imagesForPicturebox.Count > 0)
            {
                IImageService imageService = new ImageService(uow);
                if (currentControl.ImageLib.Image[currentPositionInImages].Id != 0)
                {
                    imageService.Delete(currentControl.ImageLib.Image[currentPositionInImages]);
                }
                imagesForPicturebox.RemoveAt(currentPositionInImages);
                currentControl.ImageLib.Image.RemoveAt(currentPositionInImages);
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (currentControl != null)
            {
                if (radioButton.Checked)
                {
                    currentControl.IsControlled = false;
                }
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (currentControl != null)
            {
                if (radioButton.Checked)
                {
                    currentControl.IsControlled = true;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            float light;
            if (textBox3.Text != "")
            {
                if (float.TryParse(textBox3.Text, out light))
                {
                    if (currentControl != null)
                    {
                        currentControl.Light = light;
                    }

                }
                else
                {
                    MessageBox.Show("Неверный формат числа (Освещённость)", "Ошибка ввода");
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                currentControl.Additionally = textBox4.Text;
            }
            
        }

        public void ClearData()
        {
            checkBox1.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            imagesForPicturebox.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            textBox3.Clear();
            textBox4.Clear();
            listBox1.Items.Clear();
            pictureBox1.Image = null;
            listBox4.Items.Clear();
            isRejected = null;
            label11.Text = "";
            currentControl = null;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (currentControl != null)
            {
                ResultDirectoryForm resultDirectoryForm = new ResultDirectoryForm(currentControl.ResultLib);
                resultDirectoryForm.ShowDialog(this);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                currentControl.ControlMethodDocumentationLib.SelectedControlMethodDocumentation.RemoveAt(listBox2.SelectedIndex);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите элемент списка", "Оповещение");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1)
            {
                currentControl.RequirementDocumentationLib.SelectedRequirementDocumentation.RemoveAt(listBox3.SelectedIndex);
                listBox3.Items.RemoveAt(listBox3.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите элемент списка", "Оповещение");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                currentControl.EquipmentLib.SelectedEquipment.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите элемент списка", "Оповещение");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1)
            {
                currentControl.EmployeeLib.SelectedEmployee.RemoveAt(listBox4.SelectedIndex);
                listBox4.Items.RemoveAt(listBox4.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Выберите элемент списка", "Оповещение");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
            
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                ConvertManager.ConvertControlResultToPdf(currentControl, currentJournal, saveFileDialog1.FileName);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                ConvertManager.ConvertControlResultToExcel(currentControl, currentJournal);
            }
        }

        public void DisableFormControls()
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button9.Visible = false;
            button11.Visible = false;
            button13.Visible = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            label11.Text = "";
        }

        public void EnableFormControls()
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button9.Visible = true;
            button11.Visible = true;
            button13.Visible = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            label11.Text = "";
        }

        public void ClearProtocolNumber()
        {
            label11.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                EnableFormControls();
                isControlled = true;
                radioButton2.Checked = true;
                currentControl.IsControlled = true;
            }
            else
            {
                DisableFormControls();
                isControlled = false;
                currentControl.IsControlled = null;
                radioButton1.Checked = false;
                radioButton1.Checked = false;
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (pb.Dock == DockStyle.None)
            {
                pb.Dock = DockStyle.Fill;
                pb.BringToFront();
            }
            else
            {
                pb.Dock = DockStyle.None;
            }
        }
    }
}
