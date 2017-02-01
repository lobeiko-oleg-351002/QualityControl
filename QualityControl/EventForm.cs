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

namespace QualityControl_Client
{
    public partial class EventForm : Form
    {
        public EventForm()
        {
            InitializeComponent();
            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd.MM.yyyy";
            AddEventsFromCertificates();
            AddEventsFromCustomers();
            AddEventsFromEquipments();
            AddEventsFromEmployees();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddEventsFromCertificates()
        {
            const string obj = "Сертификат";
            const string messageFinal = "Истёк срок действия сертификата";
            const string messagePrevent = "Срок действия сертификата истекает";
            const int preventDaysCount = 31;
            ICertificateRepository certificateRepository = ServiceChannelManager.Instance.CertificateRepository;
            var certificates = certificateRepository.GetAll();
            foreach (var item in certificates)
            {
                var endOfValidity = item.CheckDate.AddYears(item.Duration.HasValue ? item.Duration.Value : 0);
                if (endOfValidity.CompareTo(DateTime.Now) < 0 )
                {
                    AddEventMesssage(obj, item.Title, messageFinal, endOfValidity);
                }
                else
                if (endOfValidity.CompareTo(DateTime.Now.AddDays(preventDaysCount)) < 0)
                {
                    AddEventMesssage(obj, item.Title, messagePrevent, endOfValidity);
                }
            }
        }

        private void AddEventsFromCustomers()
        {
            const string obj = "Заказчик";
            const string messageFinal = "Истёк срок действия договора";
            const string messagePrevent = "Срок действия договора истекает";
            const int preventDaysCount = 31;
            ICustomerRepository CustomerRepository = ServiceChannelManager.Instance.CustomerRepository;
            var Customers = CustomerRepository.GetAll();
            foreach (var item in Customers)
            {
                if (item.ContractEndDate.Value.CompareTo(DateTime.Now) < 0)
                {
                    AddEventMesssage(obj, item.Organization, messageFinal, item.ContractEndDate.Value);
                }
                else
                if (item.ContractEndDate.Value.CompareTo(DateTime.Now.AddDays(preventDaysCount)) < 0)
                {
                    AddEventMesssage(obj, item.Organization, messagePrevent, item.ContractEndDate.Value);
                }
            }
        }

        private void AddEventsFromEquipments()
        {
            const string obj = "Оборудование";
            const string messageFinal = "Необходимо провести техническую поверку";
            const string messagePrevent = "Срок действия технической поверки истекает";
            IEquipmentRepository EquipmentRepository = ServiceChannelManager.Instance.EquipmentRepository;
            const int preventDaysCount = 31;
            var Equipments = EquipmentRepository.GetAll();
            foreach (var item in Equipments)
            {
                if (item.NextTechnicalCheckDate.Value.CompareTo(DateTime.Now) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinal, item.NextTechnicalCheckDate.Value);
                }
                else
                if (item.NextTechnicalCheckDate.Value.CompareTo(DateTime.Now.AddDays(preventDaysCount)) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messagePrevent, item.NextTechnicalCheckDate.Value);
                }
            }
        }

        private void AddEventsFromEmployees()
        {
            const string obj = "Сотрудники";
            const string messageFinalTech = "Необходимо подтвердить квалификацию сотрудника";
            const string messageFinalMed = "Необходимо провести мед. обследование сотрудника";
            const string messagePreventTech = "Срок действия квалификации сотрудника истекает";
            const string messagePreventMed = "Срок действия результатов мед. обследования сотрудника истекает";
            IEmployeeRepository EmployeeRepository = ServiceChannelManager.Instance.EmployeeRepository;
            const int preventDaysCount = 31;
            var Employees = EmployeeRepository.GetAll();
            foreach (var item in Employees)
            {
                var endOfTechValidity = item.KnowledgeCheckDate.Value.AddYears(1);
                if (endOfTechValidity.CompareTo(DateTime.Now) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinalTech, endOfTechValidity);
                }
                else
                if (endOfTechValidity.CompareTo(DateTime.Now.AddDays(preventDaysCount)) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinalTech, endOfTechValidity);
                }

                var endOfMedValidity = item.MedicalCheckDate.Value.AddYears(1);
                if (endOfMedValidity.CompareTo(DateTime.Now) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinalMed, endOfMedValidity);
                }
                else
                if (endOfMedValidity.CompareTo(DateTime.Now.AddDays(preventDaysCount)) <= 0)
                {
                    AddEventMesssage(obj, item.Name, messageFinalMed, endOfMedValidity);
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
    }
}
