using QualityControl_Client.Forms;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QualityControl_Client.Forms.SertificateDirectory;
using QualityControl_Client.Forms.CustomerDirectory;
using QualityControl_Client.Forms.EquipmentDirectory;
using QualityControl_Client.Forms.EmployeeDirectory;
using BLL.Services.Interface;
using BLL.Services;
using DAL.Repositories.Interface;
using BLL.Entities.Interface;
using QualityControl_Server;

namespace QualityControl_Client
{
    public partial class EventForm : Form
    {
        public EventForm(IUnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd.MM.yyyy";
            AppConfigManager configManager = new AppConfigManager();
            preventDaysCount = int.Parse(configManager.GetTagValue(configManager.daysBeforeDeadline));
            RefreshDataGrid();
        }

        IUnitOfWork uow;
        int preventDaysCount;
        public void RefreshDataGrid()
        {
            dataGridView1.Rows.Clear();
            AddEventsFromCertificates();
            AddEventsFromCustomers();
            AddEventsFromEquipments();
            AddEventsFromEmployees();
        }

        List<IBllEntity> Entities = new List<IBllEntity>();
        List<string> DirectoryFormClassNames = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddEventsFromCertificates()
        {
            const string obj = "Сертификат";
            const string messageFinal = "Истёк срок действия сертификата";
            const string messagePrevent = "Срок действия сертификата истекает";
            string formClassName = "SertificateDirectoryForm";
            ICertificateService certificateService = new CertificateService(uow);
            var certificates = certificateService.GetAll();
            foreach (var item in certificates)
            {
                var endOfValidity = item.CheckDate.AddYears(item.Duration.HasValue ? item.Duration.Value : 0);
                if (endOfValidity.CompareTo(DateTime.Now) < 0 )
                {
                    AddEventMesssage(obj, item.Title, messageFinal, endOfValidity);
                    DirectoryFormClassNames.Add(formClassName);
                    Entities.Add(item);
                }
                else
                if (endOfValidity.CompareTo(DateTime.Now.AddDays(preventDaysCount)) < 0)
                {
                    AddEventMesssage(obj, item.Title, messagePrevent, endOfValidity);
                    DirectoryFormClassNames.Add(formClassName);
                    Entities.Add(item);
                }
            }
        }

        private void AddEventsFromCustomers()
        {
            const string obj = "Заказчик";
            const string messageFinal = "Истёк срок действия договора";
            const string messagePrevent = "Срок действия договора истекает";
            const string formClassName = "CustomerDirectoryForm";
            ICustomerService CustomerService = new CustomerService(uow);
            var Customers = CustomerService.GetAll();
            foreach (var item in Customers)
            {
                if (item.ContractEndDate.Value.CompareTo(DateTime.Now) < 0)
                {
                    AddEventMesssage(obj, item.Organization, messageFinal, item.ContractEndDate.Value);
                    DirectoryFormClassNames.Add(formClassName);
                    Entities.Add(item);
                }
                else
                if (item.ContractEndDate.Value.CompareTo(DateTime.Now.AddDays(preventDaysCount)) < 0)
                {
                    AddEventMesssage(obj, item.Organization, messagePrevent, item.ContractEndDate.Value);
                    DirectoryFormClassNames.Add(formClassName);
                    Entities.Add(item);
                }
            }
        }

        private void AddEventsFromEquipments()
        {
            const string obj = "Оборудование";
            const string messageFinal = "Необходимо провести техническую поверку";
            const string messagePrevent = "Срок действия технической поверки истекает";
            const string formClassName = "EquipmentDirectoryForm";
            IEquipmentService EquipmentService = new EquipmentService(uow);
            var Equipments = EquipmentService.GetAll();
            foreach (var item in Equipments)
            {
                if (item.NextTechnicalCheckDate.Value.CompareTo(DateTime.Now) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinal, item.NextTechnicalCheckDate.Value);
                    DirectoryFormClassNames.Add(formClassName);
                    Entities.Add(item);
                }
                else
                if (item.NextTechnicalCheckDate.Value.CompareTo(DateTime.Now.AddDays(preventDaysCount)) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messagePrevent, item.NextTechnicalCheckDate.Value);
                    DirectoryFormClassNames.Add(formClassName);
                    Entities.Add(item);
                }
            }
        }

        private void AddEventsFromEmployees()
        {
            const string obj = "Сотрудники";
            const string messageFinalTech = "Необходимо подтвердить квалификацию сотрудника";
            const string messageFinalMed = "Необходимо провести мед. обследование сотрудника";
           // const string messagePreventTech = "Срок действия квалификации сотрудника истекает";
            //const string messagePreventMed = "Срок действия результатов мед. обследования сотрудника истекает";
            string formClassName = "EmployeeDirectoryForm";
            IEmployeeService EmployeeService = new EmployeeService(uow);
            var Employees = EmployeeService.GetAll();
            foreach (var item in Employees)
            {
                var endOfTechValidity = item.KnowledgeCheckDate.Value.AddYears(1);
                bool isAddedMessage = false;
                if (endOfTechValidity.CompareTo(DateTime.Now) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinalTech, endOfTechValidity);
                    isAddedMessage = true;
                }
                else
                if (endOfTechValidity.CompareTo(DateTime.Now.AddDays(preventDaysCount)) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinalTech, endOfTechValidity);
                    isAddedMessage = true;
                }

                var endOfMedValidity = item.MedicalCheckDate.Value.AddYears(1);
                if (endOfMedValidity.CompareTo(DateTime.Now) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinalMed, endOfMedValidity);
                    isAddedMessage = true;
                }
                else
                if (endOfMedValidity.CompareTo(DateTime.Now.AddDays(preventDaysCount)) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinalMed, endOfMedValidity);
                    isAddedMessage = true;
                }

                if (isAddedMessage)
                {
                    DirectoryFormClassNames.Add(formClassName);
                    Entities.Add(item);
                }
            }
        }

        private void AddEventMesssage(string obj, string name, string message, DateTime date)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridView1);
            row.Cells[0].Value = dataGridView1.Rows.Count + 1;
            row.Cells[1].Value = obj;
            row.Cells[2].Value = name;
            row.Cells[3].Value = message;
            row.Cells[4].Value = date;
            dataGridView1.Rows.Add(row);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dataGridView1.SelectedRows[0].Index;
            DirectoryForm form = null;
            switch(DirectoryFormClassNames[id])
            {
                case "SertificateDirectoryForm":
                    form = new SertificateDirectoryForm(uow);
                    break;
                case "EmployeeDirectoryForm":
                    form = new EmployeeDirectoryForm(uow);
                    break;
                case "EquipmentDirectoryForm":
                    form = new EquipmentDirectoryForm(uow);
                    break;
                case "CustomerDirectoryForm":
                    form = new CustomerDirectoryForm(uow);
                    break;

            }
            //var formClass = DirectoryFormClassNames[id].GetType();
            //DirectoryForm form = (DirectoryForm)Activator.CreateInstance(formClass);
            if (form != null)
            {
                form.Show();
                form.SelectRow(Entities[id]);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AppConfigManager configManager = new AppConfigManager();
            preventDaysCount = int.Parse(configManager.GetTagValue(configManager.daysBeforeDeadline));
            RefreshDataGrid();
        }
    }
}
