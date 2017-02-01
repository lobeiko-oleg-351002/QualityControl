using QualityControl_Client.Forms.ComponentDirectory;
using QualityControl_Client.Forms.MaterialDirectory;
using QualityControl_Client.Forms.WeldJointDirectory;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIL.Entities;

namespace QualityControl_Client
{
    public partial class ChangeJournalForm : Form
    {
        List<ControlMethodTabForm> ControlMethodTabForms;
        List<UilIndustrialObject> IndustrialObjects;
        List<UilCustomer> Customers;

        public UilJournal Journal { get; private set; }

        public ChangeJournalForm()
        {
            InitializeComponent();
        }

        public ChangeJournalForm(UilJournal oldJournal, UilEmployee user)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen();
            Journal = oldJournal;

            IControlNameRepository controlNameRepository = ServiceChannelManager.Instance.ControlNameRepository;
            var controlNames = controlNameRepository.GetAll();
            ControlMethodTabForms = new List<ControlMethodTabForm>();
            UilControl control = null;
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
                    control = new UilControl
                    {
                        ImageLib = new UilImageLib(),
                        EquipmentLib = new UilEquipmentLib(),
                        ResultLib = new UilResultLib(),
                        ControlMethodDocumentationLib = new UilControlMethodDocumentationLib(),
                        RequirementDocumentationLib = new UilRequirementDocumentationLib(),
                        EmployeeLib = new UilEmployeeLib()
                    };
                    control.ControlName = controlName;
                    Journal.ControlMethodsLib.Control.Add(control);
                }


                var tabForm = new ControlMethodTabForm(control, oldJournal);
                if (control.Is_сontrolled != null)
                {
                    tabForm.EnableFormControls();
                }
                tabForm.EnableValidateCheckBox();
                //tabForm.AddEmployee(user);
                ControlMethodTabForms.Add(tabForm);
                tabForm.FillComponents();
                tabControl1.TabPages.Add(new ControlMethodTab(tabForm, controlName));
            }

            IndustrialObjects = new List<UilIndustrialObject>();
            IIndustrialObjectRepository industrialObjectRepository = ServiceChannelManager.Instance.IndustrialObjectRepository;
            var industrialObjects = industrialObjectRepository.GetAll();
            foreach (var element in industrialObjects)
            {
                IndustrialObjects.Add(element);
                comboBox1.Items.Add(element.Name);
            }
            if (IndustrialObjects.Count > 0)
            {
                comboBox1.SelectedItem = Journal.IndustrialObject != null ? Journal.IndustrialObject.Name : "";
            }

            Customers = new List<UilCustomer>();
            ICustomerRepository CustomerRepository = ServiceChannelManager.Instance.CustomerRepository;
            var customers = CustomerRepository.GetAll();
            foreach (var element in customers)
            {
                Customers.Add(element);
                comboBox2.Items.Add(element.Organization + " " + element.Address + " " + element.Phone);

            }
            if (Customers.Count > 0)
            {
                comboBox2.SelectedItem = Journal.Customer != null ? Journal.Customer.Organization + " " + Journal.Customer.Address + " " + Journal.Customer.Phone : "";
            }

            dateTimePicker1.Value = Journal.Request_date.Value;
            dateTimePicker2.Value = Journal.Control_date.Value;
            numericUpDown2.Value = Journal.Request_number.Value;
            numericUpDown1.Value = Journal.Amount.Value;
            textBox3.Text = Journal.Size;
            textBox2.Text = Journal.Component != null ? Journal.Component.Name : "";
            textBox4.Text = Journal.Material != null ? Journal.Material.Name : "";
            textBox5.Text = Journal.WeldJoint != null ? Journal.WeldJoint.Name : "";
            textBox6.Text = Journal.Welding_type;
            richTextBox2.Text = Journal.Description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseComponentForm ComponentForm = new ChooseComponentForm();
            ComponentForm.ShowDialog(this);
            UilComponent Component = ComponentForm.GetChosenComponent();
            if (Component != null)
            {
                Journal.Component = Component;
                textBox2.Text = Component.Name;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChooseMaterialForm MaterialForm = new ChooseMaterialForm();
            MaterialForm.ShowDialog(this);
            UilMaterial Material = MaterialForm.GetChosenMaterial();
            if (Material != null)
            {
                Journal.Material = Material;
                textBox4.Text = Material.Name;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseWeldJointForm WeldJointForm = new ChooseWeldJointForm();
            WeldJointForm.ShowDialog(this);
            UilWeldJoint WeldJoint = WeldJointForm.GetChosenWeldJoint();
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
            List<UilControl> controlsForRemoving = new List<UilControl>();
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i].Is_сontrolled == null)
                {
                    controlsForRemoving.Add(controls[i]);
                }
                
            }

            foreach(var control in controlsForRemoving)
            {
                controls.Remove(control);
            }
        }

        private List<UilControl> Clone(List<UilControl> source)
        {
            List<UilControl> temp = new List<UilControl>();
            foreach (var elem in source)
            {
                temp.Add(elem);
            }
            return temp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IJournalRepository repository = ServiceChannelManager.Instance.JournalRepository;
            InitializeJournalViaFormControls();
            //List<UilControl> temp = Clone(Journal.ControlMethodsLib.Control);
            RemoveUncontrolledMethods();

            Journal = repository.Update(Journal);

            isClosed = false;
            MessageBox.Show("Информация обновлена", "Оповещение");
            Close();
        }

        public bool isClosed { get; private set; }
        private void button5_Click(object sender, EventArgs e)
        {
            isClosed = true;
            IJournalRepository repository = ServiceChannelManager.Instance.JournalRepository;
            Journal = repository.Get(Journal.Id);
            Close();
        }

        private void InitializeJournalViaFormControls()
        {
            Journal.Request_date = dateTimePicker1.Value;
            Journal.Control_date = dateTimePicker2.Value;
            Journal.Request_number = (int)numericUpDown2.Value;
            Journal.Amount = (int)numericUpDown1.Value;
            Journal.Size = textBox3.Text;
            Journal.Welding_type = textBox6.Text;
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
