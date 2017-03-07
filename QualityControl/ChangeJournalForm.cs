using QualityControl_Client.Forms.ComponentDirectory;
using QualityControl_Client.Forms.MaterialDirectory;
using QualityControl_Client.Forms.WeldJointDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BLL.Entities;
using BLL.Services.Interface;
using DAL.Repositories.Interface;
using BLL.Services;

namespace QualityControl_Client
{
    public partial class ChangeJournalForm : Form
    {
        List<ControlMethodTabForm> ControlMethodTabForms;
        List<BllIndustrialObject> IndustrialObjects;
        List<BllCustomer> Customers;
        BllUser User;

        public BllJournal Journal { get; private set; }

        public ChangeJournalForm()
        {
            InitializeComponent();
        }
        IUnitOfWork uow;
        public ChangeJournalForm(BllJournal oldJournal, BllUser user, IUnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen();
            Journal = oldJournal;
            User = user;
            IControlNameService controlNameService = new ControlNameService(uow);
            var controlNames = controlNameService.GetAll();
            ControlMethodTabForms = new List<ControlMethodTabForm>();
            BllControl control = null;
            foreach (var controlName in controlNames)
            {
                bool isExistControl = false;
                
                foreach (var currentControl in oldJournal.ControlMethodsLib.Control)
                {
                    if (currentControl.ControlName.Name == controlName.Name)
                    {
                        control = currentControl;
                        isExistControl = true;
                        break;
                    }

                }
                if (isExistControl == false)
                {
                    control = new BllControl
                    {
                        ImageLib = new BllImageLib(),
                        EquipmentLib = new BllEquipmentLib(),
                        ResultLib = new BllResultLib(),
                        ControlMethodDocumentationLib = new BllControlMethodDocumentationLib(),
                        RequirementDocumentationLib = new BllRequirementDocumentationLib(),
                        EmployeeLib = new BllEmployeeLib()
                    };
                    control.ControlName = controlName;
                    Journal.ControlMethodsLib.Control.Add(control);
                }


                var tabForm = new ControlMethodTabForm(control, oldJournal, uow);
                if (control.IsControlled != null)
                {
                    tabForm.EnableFormControls();
                }
                tabForm.EnableValidateCheckBox();
                //tabForm.AddEmployee(user);
                ControlMethodTabForms.Add(tabForm);
                tabForm.FillComponents();
                tabControl1.TabPages.Add(new ControlMethodTab(tabForm, controlName));
            }

            IndustrialObjects = new List<BllIndustrialObject>();
            IIndustrialObjectService industrialObjectService = new IndustrialObjectService(uow);
            var industrialObjects = industrialObjectService.GetAll();
            foreach (var element in industrialObjects)
            {
                IndustrialObjects.Add(element);
                comboBox1.Items.Add(element.Name);
            }
            if (IndustrialObjects.Count > 0)
            {
                comboBox1.SelectedItem = Journal.IndustrialObject != null ? Journal.IndustrialObject.Name : "";
            }

            Customers = new List<BllCustomer>();
            ICustomerService CustomerService = new CustomerService(uow);
            var customers = CustomerService.GetAll();
            foreach (var element in customers)
            {
                Customers.Add(element);
                comboBox2.Items.Add(element.Organization + " " + element.Address + " " + element.Phone);

            }
            if (Customers.Count > 0)
            {
                comboBox2.SelectedItem = Journal.Customer != null ? Journal.Customer.Organization + " " + Journal.Customer.Address + " " + Journal.Customer.Phone : "";
            }

            dateTimePicker1.Value = Journal.RequestDate.Value;
            dateTimePicker2.Value = Journal.ControlDate.Value;
            numericUpDown2.Value = Journal.RequestNumber.Value;
            numericUpDown1.Value = Journal.Amount.Value;
            textBox3.Text = Journal.Size;
            textBox2.Text = Journal.Component != null ? Journal.Component.Name : "";
            textBox4.Text = Journal.Material != null ? Journal.Material.Name : "";
            textBox5.Text = Journal.WeldJoint != null ? Journal.WeldJoint.Name : "";
            textBox6.Text = Journal.WeldingType;
            richTextBox2.Text = Journal.Description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseComponentForm ComponentForm = new ChooseComponentForm(uow);
            ComponentForm.ShowDialog(this);
            BllComponent Component = ComponentForm.GetChosenComponent();
            if (Component != null)
            {
                Journal.Component = Component;
                textBox2.Text = Component.Name;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChooseMaterialForm MaterialForm = new ChooseMaterialForm(uow);
            MaterialForm.ShowDialog(this);
            BllMaterial Material = MaterialForm.GetChosenMaterial();
            if (Material != null)
            {
                Journal.Material = Material;
                textBox4.Text = Material.Name;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseWeldJointForm WeldJointForm = new ChooseWeldJointForm(uow);
            WeldJointForm.ShowDialog(this);
            BllWeldJoint WeldJoint = WeldJointForm.GetChosenWeldJoint();
            if (WeldJoint != null)
            {
                Journal.WeldJoint = WeldJoint;
                textBox5.Text = WeldJoint.Name;
            }
        }

        private void RemoveUncontrolledMethods()
        {
            var controls = Journal.ControlMethodsLib.Control;
            int j = 0;
            List<BllControl> controlsForRemoving = new List<BllControl>();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].IsControlled == null)
                {
                    controlsForRemoving.Add(controls[i]);
                }
                
            }

            foreach(var control in controlsForRemoving)
            {
                controls.Remove(control);
            }
        }

        private List<BllControl> Clone(List<BllControl> source)
        {
            List<BllControl> temp = new List<BllControl>();
            foreach (var elem in source)
            {
                temp.Add(elem);
            }
            return temp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IJournalService Service = new JournalService(uow);
            InitializeJournalViaFormControls();
            //List<BllControl> temp = Clone(Journal.ControlMethodsLib.Control);
            RemoveUncontrolledMethods();
            Journal.ModifiedDate = DateTime.Now;
            Journal.UserModifierLogin = User != null ? User.Login : "";

            Journal = Service.Update(Journal);

            isClosed = false;
            MessageBox.Show("Информация обновлена", "Оповещение");
            Close();
        }

        public bool isClosed { get; private set; }
        private void button5_Click(object sender, EventArgs e)
        {
            isClosed = true;
            IJournalService Service = new JournalService(uow);
            Journal = Service.Get(Journal.Id);
            Close();
        }

        private void InitializeJournalViaFormControls()
        {
            Journal.RequestDate = dateTimePicker1.Value;
            Journal.ControlDate = dateTimePicker2.Value;
            Journal.RequestNumber = (int)numericUpDown2.Value;
            Journal.Amount = (int)numericUpDown1.Value;
            Journal.Size = textBox3.Text;
            Journal.WeldingType = textBox6.Text;
            Journal.Contract = textBox9.Text;
            Journal.Description = richTextBox2.Text;
            Journal.Customer = comboBox2.SelectedIndex != -1 ? Customers[comboBox2.SelectedIndex] : null;
            Journal.IndustrialObject = comboBox1.SelectedIndex != -1 ? IndustrialObjects[comboBox1.SelectedIndex] : null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Journal.Component != null)
            {
                var template = Journal.Component.Template;
                if (template != null)
                {
                    Journal.Material = template.Material;
                    Journal.WeldJoint = template.WeldJoint;
                    Journal.Description = template.Description;

                    textBox4.Text = Journal.Material != null ? Journal.Material.Name : "";
                    textBox5.Text = Journal.WeldJoint != null ? Journal.WeldJoint.Name : "";
                    richTextBox2.Text = Journal.Description;

                    foreach (var controlName in template.ControlNameLib.SelectedControlName)
                    {
                        for (int i = 0; i < Journal.ControlMethodsLib.Control.Count; i++)
                        {
                            var controls = Journal.ControlMethodsLib.Control;
                            if (controlName.ControlName.Name.Equals(controls[i].ControlName.Name))
                            {
                                ControlMethodTabForms[i].CopyNewEquipmentFromLib(template.EquipmentLib);
                                ControlMethodTabForms[i].CopyNewImagesFromLib(template.ImageLib);
                                ControlMethodTabForms[i].CopyNewRequirementDocumentationFromLib(template.RequirementDocumentationLib);

                            }
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Шаблон не указан", "Оповещение");
                }
            }
            else
            {
                MessageBox.Show("Выберите объект контроля", "Оповещение");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox9.Text = Customers[comboBox2.SelectedIndex].Contract;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var currentControl = ControlMethodTabForms[tabControl1.SelectedIndex].currentControl;
            for (int i = 0; i < Journal.ControlMethodsLib.Control.Count; i++)
            {
                if (!currentControl.Equals(Journal.ControlMethodsLib.Control[i]))
                {
                    var controls = Journal.ControlMethodsLib.Control;
                    ControlMethodTabForms[i].CopyNewEmployeeFromLib(currentControl.EmployeeLib);
                    ControlMethodTabForms[i].SetLight(currentControl.Light != null ? (float)currentControl.Light : 0);
                    ControlMethodTabForms[i].CopyNewResultFromLib(currentControl.ResultLib);
                    ControlMethodTabForms[i].SetAdditionally(currentControl.Additionally);
                    //ControlMethodTabForms[i].CopyNewEquipmentFromLib(currentControl.EquipmentLib);
                    ControlMethodTabForms[i].CopyNewImagesFromLib(currentControl.ImageLib);
                    ControlMethodTabForms[i].CopyNewRequirementDocumentationFromLib(currentControl.RequirementDocumentationLib);
                }

            }
        }


    }
}
