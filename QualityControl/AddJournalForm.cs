using QualityControl_Client.Forms.ComponentDirectory;
using QualityControl_Client.Forms.MaterialDirectory;
using QualityControl_Client.Forms.WeldJointDirectory;
using QualityControl_Server;
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
    public partial class AddJournalForm : Form
    {
        List<UilControlName> ControlNames;
        List<ControlMethodTabForm> ControlMethodTabForms;
        List<UilIndustrialObject> IndustrialObjects;
        List<UilCustomer> Customers;

        public UilJournal Journal { get; private set; }

        public delegate void AddRowToDataGrid(UilJournal journal);

        private AddRowToDataGrid AddRowToDataGridDelegate;

        public AddJournalForm()
        {
            InitializeComponent();
        }

        public AddJournalForm(AddRowToDataGrid AddRowToDataGridDelegate, UilEmployee user)
        {
            InitializeComponent();
            dateTimePicker1.Focus();
            this.AddRowToDataGridDelegate = AddRowToDataGridDelegate;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen();
            Journal = new UilJournal
            {
                Request_date = DateTime.Now,
                Control_date = DateTime.Now,
                Request_number = 0,
                Amount = 0,
            };
            Journal.ControlMethodsLib = new UilControlMethodsLib();

            IControlNameRepository controlNameRepository = ServiceChannelManager.Instance.ControlNameRepository;
            var controlNames = controlNameRepository.GetAll();
            ControlNames = new List<UilControlName>();
            ControlMethodTabForms = new List<ControlMethodTabForm>();

            foreach (var controlName in controlNames)
            {
                var control = new UilControl
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

                var tabForm = new ControlMethodTabForm(controlName.Name);
                tabForm.EnableValidateCheckBox();
                ControlMethodTabForms.Add(tabForm);

                tabControl1.TabPages.Add(new ControlMethodTab(tabForm, controlName));
                ControlNames.Add(controlName);
            }

            for (int i = 0; i < ControlNames.Count; i++)
            {
                var control = Journal.ControlMethodsLib.Control[i];
                ControlMethodTabForms[i].SetCurrentControl(control, Journal);
                ControlMethodTabForms[i].AddEmployee(user);
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
                comboBox1.SelectedValue = comboBox1.Items[0];
            }

            Customers = new List<UilCustomer>();
            ICustomerRepository CustomerRepository = ServiceChannelManager.Instance.CustomerRepository;
            var customers = CustomerRepository.GetAll();
            foreach (var element in customers)
            {
                Customers.Add(element);
                comboBox2.Items.Add(element.Organization + "  " + element.Address + "  " + element.Phone);
                
            }
            if (Customers.Count > 0)
            {
                comboBox2.SelectedValue = comboBox2.Items[0];
            }
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
            for (int i = 0; i < controls.Count; i++)
            {
                if (ControlMethodTabForms[j].isControlled == false)
                {
                    controls.RemoveAt(i);
                    i--;
                }
                j++;
            }
        }


        private List<UilControl> Clone(List<UilControl> source)
        {
            List<UilControl> temp = new List<UilControl>();
            foreach(var elem in source)
            {
                temp.Add(CloneControl(elem));
            }
            return temp;
        }

        private UilControl CloneControl(UilControl source)
        {
            UilControl target = new UilControl();
            target.Additionally = source.Additionally;
            target.ControlName = source.ControlName;
            target.Id = source.Id;
            target.Is_сontrolled = source.Is_сontrolled;
            target.Light = source.Light;
            target.Number = source.Number;
            target.ProtocolNumber = source.ProtocolNumber;
            target.ControlMethodDocumentationLib = new UilControlMethodDocumentationLib();
            foreach (var doc in source.ControlMethodDocumentationLib.SelectedControlMethodDocumentation)
            {
                target.ControlMethodDocumentationLib.SelectedControlMethodDocumentation.Add(doc);
            }

            target.EmployeeLib = new UilEmployeeLib();
            foreach (var employee in source.EmployeeLib.SelectedEmployee)
            {
                target.EmployeeLib.SelectedEmployee.Add(employee);
            }

            target.EquipmentLib = new UilEquipmentLib();
            foreach (var Equipment in source.EquipmentLib.SelectedEquipment)
            {
                target.EquipmentLib.SelectedEquipment.Add(Equipment);
            }

            target.RequirementDocumentationLib = new UilRequirementDocumentationLib();
            foreach (var RequirementDocumentation in source.RequirementDocumentationLib.SelectedRequirementDocumentation)
            {
                target.RequirementDocumentationLib.SelectedRequirementDocumentation.Add(RequirementDocumentation);
            }

            target.ResultLib = new UilResultLib();
            foreach (var Result in source.ResultLib.Result)
            {
                target.ResultLib.Result.Add(Result);
            }

            target.ImageLib = new UilImageLib();
            foreach (var Image in source.ImageLib.Image)
            {
                target.ImageLib.Image.Add(Image);
            }

            return target;

        }

        private UilJournal CloneJournal(UilJournal source)
        {
            UilJournal target = new UilJournal();
            target.Amount = source.Amount;
            target.Component = source.Component;
            target.Contract = source.Contract;
            target.Control_date = source.Control_date;
            target.Customer = source.Customer;
            target.Description = source.Description;
            target.Id = source.Id;
            target.IndustrialObject = source.IndustrialObject;
            target.Material = source.Material;
            target.Request_date = source.Request_date;
            target.Request_number = source.Request_number;
            target.Size = source.Size;
            target.Welding_type = source.Welding_type;
            target.WeldJoint = source.WeldJoint;
            target.ControlMethodsLib = new UilControlMethodsLib();
            return target;
        }


        private bool DealWithUnfilledInfo()
        {
            string unfilledInfo = "";
            if (Journal.Component == null)
            {
                unfilledInfo += "\n Объект контроля";
            }                       
            if (Journal.Material == null)
            {
                unfilledInfo += "\n Материал";
            }
            if (Journal.Size == "")
            {
                unfilledInfo += "\n Типовой размер";
            }
            if (Journal.WeldJoint == null)
            {
                unfilledInfo += "\n Тип сварного соединения";
            }
            if (Journal.Welding_type == "")
            {
                unfilledInfo += "\n Способ сварки";
            }
            if (Journal.IndustrialObject == null)
            {
                unfilledInfo += "\n Промышленный объект";
            }
            if (Journal.Customer == null)
            {
                unfilledInfo += "\n Заказчик";
            }
            if (Journal.Contract == null)
            {
                unfilledInfo += "\n Основание/договор";
            }
            
            if(unfilledInfo != "")
            {
                ContinueChoiceForm form = new ContinueChoiceForm("Не заполнены поля: " + unfilledInfo + "\nЖелаете продолжить?");
                form.ShowDialog();
                return form.IsContinue;
            }
            return true;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InitializeJournalViaFormControls();
            if (DealWithUnfilledInfo())
            {
                IJournalRepository repository = ServiceChannelManager.Instance.JournalRepository;
                
                List<UilControl> temp = Clone(Journal.ControlMethodsLib.Control); //оставляю для следующих добавляемых объектов в этом окне
                RemoveUncontrolledMethods();


                Journal = repository.Create(Journal);
                MessageBox.Show("Информация добавлена.", "Оповещение");
                AddRowToDataGridDelegate(Journal);

                for (int i = 0; i < temp.Count; i++)
                {
                    foreach (var createdControl in Journal.ControlMethodsLib.Control)
                    {
                        if (temp[i].ControlName.Id == createdControl.ControlName.Id)
                        {
                            temp[i].Id = createdControl.Id;
                            temp[i].ProtocolNumber = createdControl.ProtocolNumber;
                        }
                    }
                }
                Journal = CloneJournal(Journal);
                Journal.ControlMethodsLib.Control = temp;

                for (int i = 0; i < ControlNames.Count; i++)
                {
                    var control = Journal.ControlMethodsLib.Control[i];
                    ControlMethodTabForms[i].SetCurrentControl(control, Journal);
                }
                isClosed = false;
            }
        }

        public bool isClosed { get; private set; }
        private void button5_Click(object sender, EventArgs e)
        {
            isClosed = true;
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
                        for(int i = 0; i < Journal.ControlMethodsLib.Control.Count; i++)
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
