using Microsoft.Office.Interop.Word;
using ORM;
using QualityControl_Client;
using QualityControl_Client.Forms;
using QualityControl_Client.Forms.ComponentDirectory;
using QualityControl_Client.Forms.ControlMethodDocumentationDirectory;
using QualityControl_Client.Forms.CustomerDirectory;
using QualityControl_Client.Forms.EmployeeDirectory;
using QualityControl_Client.Forms.EquipmentDirectory;
using QualityControl_Client.Forms.IndustrialObjectDirectory;
using QualityControl_Client.Forms.MaterialDirectory;
using QualityControl_Client.Forms.RequirementDocumentationDirectory;
using QualityControl_Client.Forms.TemplateDirectory;
using QualityControl_Client.Forms.UserDirectory;
using QualityControl_Client.Forms.WeldJointDirectory;
using QualityControl_Server;
using ServerWcfService.Services;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIL.Entities;

namespace QualityControl
{
    public partial class MainForm : Form
    {
        List<UilJournal> Journals;
        List<UilControlName> ControlNames;
        List<ControlMethodTabForm> ControlMethodTabForms;
        //DebugForm debugForm;

        UilUser User = null;

        Func<UilJournal, bool> filtration = (UilJournal journal) => { return true; };

        bool isActivatedLicense = false;
        bool isConnectedToServer = false;
        bool isFirstStart = true;
        public MainForm()
        {
            InitializeComponent();
            
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            UiUnitOfWork.Instance.Init(new ServiceDB());
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CenterToScreen();
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd.MM.yyyy";
            ConnectToServer();
            if (isConnectedToServer)
            {
                Authorization();
                EventForm eventForm = new EventForm();
                eventForm.Show();
            }
            isFirstStart = false;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new System.Drawing.Font("Arial", 9F, GraphicsUnit.Pixel);
            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.HeaderCell.Style = style;
            }
            PutScrollBarDown();
            toolStripTextBox1.Text = "Исполнитель: " + User.Employee.Sirname + " " + User.Employee.Name[0] + "." + User.Employee.Fathername[0] + ".";

            //debugForm = new DebugForm();
            //debugForm.Show();
        }

        private void PutScrollBarDown()
        {
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
        }

        private void SetGuestPermission()
        {
            администрированиеToolStripMenuItem.Enabled = false;
            добавитьToolStripMenuItem.Enabled = false;
            редактироватьToolStripMenuItem.Enabled = false;
            удалитьToolStripMenuItem.Enabled = false;
        }

        private void SetWorkerPermission()
        {
            администрированиеToolStripMenuItem.Enabled = false;
        }

        private void Authorization()
        {
            LogInForm loginForm = new LogInForm();
            loginForm.ShowDialog();
            User = loginForm.User;
            if (User != null)
            {
                if (User.Role != null)
                {
                    if (User.Role.Name == "Гость")
                    {
                        SetGuestPermission();
                    }
                    if (User.Role.Name == "Работник")
                    {
                        SetWorkerPermission();
                    }
                }
            }
        }

        private void ShowLicenseActivationForm()
        {
            ProtectionKeyForm protectionKeyForm = new ProtectionKeyForm();
            protectionKeyForm.ShowDialog();
            isActivatedLicense = protectionKeyForm.isActivated;
        }

        private void InitAdminAndRoles()
        {
            IUserRepository repository = ServiceChannelManager.Instance.UserRepository;
            var users = repository.GetAll();
            if (users.Count() == 0)
            {
                IRoleRepository roleRepository = ServiceChannelManager.Instance.RoleRepository;
                var roles = roleRepository.GetAll();
                if (roles.Count() < 3)
                {
                    UilRole adminRole = new UilRole
                    {
                        Name = "Администратор"
                    };
                    roleRepository.Create(adminRole);
                    UilRole workerRole = new UilRole
                    {
                        Name = "Работник"
                    };
                    roleRepository.Create(workerRole);
                    UilRole guestRole = new UilRole
                    {
                        Name = "Гость"
                    };
                    roleRepository.Create(guestRole);
                }
                var role = roleRepository.GetRoleByName("Администратор");
                UilUser admin = new UilUser
                {
                    Login = "admin",
                    Password = "admin",
                    Role = role,
                };
                repository.Create(admin);
            }
        }

        private void ConnectToServer()
        {
            try
            {
                if (!isConnectedToServer)
                {
                    IControlNameRepository controlNameRepository = ServiceChannelManager.Instance.ControlNameRepository;
                    var controlNames = controlNameRepository.GetAll();
                    if (controlNames.Count() == 0)
                    {
                        controlNameRepository.Create(new UilControlName { Name = "ВИК" });
                        controlNames = controlNameRepository.GetAll();
                    }
                    if (controlNames.Count() < 4)
                    {
                        if (isFirstStart)
                        {
                            ShowLicenseActivationForm();
                        }

                        if (isActivatedLicense)
                        {
                            controlNameRepository.Create(new UilControlName { Name = "УЗК" });
                            controlNameRepository.Create(new UilControlName { Name = "ПВК" });
                            controlNameRepository.Create(new UilControlName { Name = "РГК" });
                            controlNames = controlNameRepository.GetAll();
                        }
                    }

                    InitAdminAndRoles();

                    ControlNames = new List<UilControlName>();
                    ControlMethodTabForms = new List<ControlMethodTabForm>();
                    foreach (var controlName in controlNames)
                    {
                        var tabForm = new ControlMethodTabForm(controlName.Name);
                        tabForm.DisableFormControls();
                        ControlMethodTabForms.Add(tabForm);
                        tabControl1.TabPages.Add(new ControlMethodTab(tabForm, controlName));
                        ControlNames.Add(controlName);
                    }

                    isConnectedToServer = true;
                    RefreshDataGridUsingServer();
                    //Authorization();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сервер не найден " + ex.Message, "Ошибка подключения");
            }


        }

        private void SetCurrentControlsInControlMethodTabs(UilControlMethodsLib lib, UilJournal currentJournal)
        {
            // IJournalRepository repository = ServiceChannelManager.Instance.JournalRepository;
            for (int i = 0; i < ControlNames.Count; i++)
            {
                //if (i+1 > lib.Control.Count)
                //{
                //    lib.Control.Add(new UilControl
                //    {
                //        ImageLib = new UilImageLib(),
                //        EquipmentLib = new UilEquipmentLib(),
                //        ResultLib = new UilResultLib(),
                //        ControlMethodDocumentationLib = new UilControlMethodDocumentationLib(),
                //        RequirementDocumentationLib = new UilRequirementDocumentationLib(),
                //        EmployeeLib = new UilEmployeeLib(),
                //        ControlName = ControlNames[i]
                //    });
                //    repository.Update(currentJournal);
                //}
                foreach (var control in lib.Control)
                {
                    if (ControlNames[i].Id == control.ControlName.Id)
                    {
                        ControlMethodTabForms[i].SetCurrentControl(control, currentJournal);
                        ControlMethodTabForms[i].FillComponents();
                        break;
                    }
                }
            }

        }

        public void tabPage2_Click(object sender, EventArgs e)
        {

        }

        public void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }


        Stopwatch sw = new Stopwatch();
        private void StartDiagnostics()
        {
            sw.Start();
        }

        private void FinishDiagnostics(string message)
        {
            sw.Stop();
            //debugForm.AddNewEvent(message, (float)sw.Elapsed.TotalMilliseconds);
            sw.Reset();
        }

        private void FillRowUsingJournal(DataGridViewRow row, UilJournal journal)
        {
            if (dataGridView1.Rows.IndexOf(row) == -1)
            {
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = dataGridView1.Rows.Count + 1;
            }
            
            row.Cells[1].Value = journal.Request_date;
            row.Cells[2].Value = journal.Control_date;
            row.Cells[3].Value = journal.Request_number;
            row.Cells[4].Value = journal.Component != null ? journal.Component.Name : null;
            row.Cells[5].Value = journal.Amount;
            row.Cells[6].Value = journal.Size;
            row.Cells[7].Value = journal.Material != null ? journal.Material.Name : null;
            row.Cells[8].Value = journal.WeldJoint != null ? journal.WeldJoint.Name : null;
            row.Cells[9].Value = journal.Welding_type;
            const int controlsCount = 4;
            for(int i = 9; i <= 9 + controlsCount; i++)
            {
                row.Cells[i].Value = "";
            }
            foreach (var control in journal.ControlMethodsLib.Control)
            {
                var control_id = control.ControlName.Id;
                row.Cells[5 + control_id].Value = "  +";   // 9 a ne 5
            }

        }

        public void AddRowToDataGrid(UilJournal journal)
        {
            DataGridViewRow row = new DataGridViewRow();
            FillRowUsingJournal(row, journal);
            dataGridView1.Rows.Add(row);                    
        }

        public void AddNewJournal(UilJournal journal)
        {
            Journals.Add(journal);
            AddRowToDataGrid(journal);
        }

        public void UpdateRowInDataGrid(UilJournal journal, int rowNumber)
        {
            Journals[rowNumber] = journal;
            DataGridViewRow row = dataGridView1.Rows[rowNumber];
            FillRowUsingJournal(row, journal);
        }

        private void RefreshDataGridUsingServer()
        {
           // StartDiagnostics();

            IJournalRepository journalRepository = ServiceChannelManager.Instance.JournalRepository;
            var journals = journalRepository.GetAll().ToList();
            Journals = new List<UilJournal>();

           // FinishDiagnostics("Get all journals");

            foreach (var journal in journals)
            {
                AddNewJournal(journal);
            }
        }

        private void RefreshDataGrid()
        {
            //dataGridView1.Rows.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox9.Clear();
            richTextBox2.Clear();
            foreach(ControlMethodTab tab in tabControl1.TabPages)
            {
                tab.tabForm.ClearData();
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (filtration(Journals[row.Index]))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.Rows[0].Visible)
                {
                    dataGridView1.Rows[0].Selected = true;
                    dataGridView1_RowStateChanged(null, new DataGridViewRowStateChangedEventArgs(dataGridView1.Rows[0], DataGridViewElementStates.Selected));
                }
            }
        }

        
        private void сертификатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SertificateDirectoryForm sertificateForm = new SertificateDirectoryForm();
            sertificateForm.ShowDialog(this);
            //RefreshData();
        }

        private void документацияМетодовКонтроляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlMethodDocumentationDirectoryForm controlMethodDocumentationForm = new ControlMethodDocumentationDirectoryForm();
            controlMethodDocumentationForm.ShowDialog(this);
            //RefreshData();
        }

        private void заказчикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerDirectoryForm customerForm = new CustomerDirectoryForm();
            customerForm.ShowDialog(this);
           // RefreshData();
        }

        private void оборудованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EquipmentDirectoryForm equipmentForm = new EquipmentDirectoryForm();
            equipmentForm.ShowDialog(this);
            //RefreshData();
        }

        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialDirectoryForm materialForm = new MaterialDirectoryForm();
            materialForm.ShowDialog(this);
            //RefreshData();
        }

        private void документацияТребованийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequirementDocumentationDirectoryForm requirementDocuemntationForm = new RequirementDocumentationDirectoryForm();
            requirementDocuemntationForm.ShowDialog(this);
            //RefreshData();
        }

        private void сварочныеСоединенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeldJointDirectoryForm weldJointForm = new WeldJointDirectoryForm();
            weldJointForm.ShowDialog(this);
            //RefreshData();
        }

        private void шаблоныИМетодыКонтроляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateDirectoryForm templateForm = new TemplateDirectoryForm();
            templateForm.ShowDialog(this);
           // RefreshData();
        }

        private void деталиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComponentDirectoryForm directoryForm = new ComponentDirectoryForm();
            directoryForm.ShowDialog(this);
            //RefreshData();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeDirectoryForm employeeForm = new EmployeeDirectoryForm();
            employeeForm.ShowDialog(this);
            //RefreshData();
        }

        private void промышленныеОбъектыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IndustrialObjectDirectoryForm industrialObjectForm = new IndustrialObjectDirectoryForm();
            industrialObjectForm.ShowDialog(this);
            //RefreshData();
        }


        private void IncorrectDateMessage(int row, int column)
        {
            MessageBox.Show("Неверный вормат даты (требуется ДД.ММ.ГГГГ)", string.Format("Ошибка: строка {0}, столбец {1}", row + 1, column + 1));
        }

        private void button13_Click(object sender, EventArgs e)
        {
            RefreshDataGridUsingServer();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if ((e.StateChanged != DataGridViewElementStates.Selected) || (dataGridView1.SelectedRows.Count == 0))
            {
                clearDataContainers();
                tabControl1.Enabled = false;
                return;
            }

            var currentJournal = Journals[dataGridView1.SelectedRows[0].Index];

            if (currentJournal.IndustrialObject != null)
            {
                textBox1.Text = currentJournal.IndustrialObject.Name;
            }

            if (currentJournal.Customer != null)
            {
                textBox2.Text = currentJournal.Customer.Organization + "  " + currentJournal.Customer.Address + "  " + currentJournal.Customer.Phone;
            }

            textBox9.Text = currentJournal.Contract;
            richTextBox2.Text = currentJournal.Description;

            SetCurrentControlsInControlMethodTabs(currentJournal.ControlMethodsLib, currentJournal);

            tabControl1.Enabled = true;

            tabControl1.Invalidate();

        }

        private void clearDataContainers()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox9.Text = "";
            richTextBox2.Text = "";
            if (ControlMethodTabForms != null)
            {
                foreach (var tab in ControlMethodTabForms)
                {
                    tab.ClearData();
                }
            }
        }



        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private bool isAnyRowSelected() { return dataGridView1.SelectedRows.Count != 0; }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        delegate void ExportMethod(UilControlName controlName, List<UilJournal> journals, string folderPath);



        private void Connect(object sender, EventArgs e)
        {
            ConnectToServer();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Document document = app.Documents.Open(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\help.docx");
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventForm eventForm = new EventForm();
            eventForm.Show();
            eventForm.Focus();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage tp = tabControl1.TabPages[e.Index];

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;  //optional

            // This is the rectangle to draw "over" the tabpage title
            RectangleF headerRect = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2);

            // This is the default colour to use for the non-selected tabs
            SolidBrush sb = new SolidBrush(Color.AntiqueWhite);

            // This changes the colour if we're trying to draw the selected tabpage
            //if (tabControl1.SelectedIndex == e.Index)
            //    sb.Color = Color.Aqua;

            var form = ControlMethodTabForms[e.Index];

            if (form.isRejected == true)
            {
                sb.Color = Color.LightPink;
            }
            if (form.isRejected == false)
            {
                sb.Color = Color.LightGreen;
            }




            // Colour the header of the current tabpage based on what we did above
            g.FillRectangle(sb, e.Bounds);

            //Remember to redraw the text - I'm always using black for title text
            if (e.Index == tabControl1.SelectedIndex)
            {
                g.DrawString(tp.Text, tabControl1.Font, new SolidBrush(Color.Black), headerRect, sf);
            }
            else
            {
                g.DrawString(tp.Text, tabControl1.Font, new SolidBrush(Color.Brown), headerRect, sf);
            }
        }

        private void администрированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (User == null)
            {
                Authorization();
            }
            if (User != null)
            {
                if (User.Role != null)
                {
                    if (User.Role.Name == "Администратор")
                    {
                        UserDirectoryForm form = new UserDirectoryForm();
                        form.ShowDialog();
                    }
                }
            }

        }

        private void вверхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFirstRow();
        }

        private void SelectFirstRow()
        {
            ClearSelectedRows();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void ClearSelectedRows()
        {
            DataGridViewSelectionMode oldmode = dataGridView1.SelectionMode;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.ClearSelection();

            dataGridView1.SelectionMode = oldmode;
        }

        private void внизДоУпораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectLastRow();
        }

        private void SelectLastRow()
        {
            ClearSelectedRows();
            var count = dataGridView1.Rows.Count;
            if (count > 0)
            {
                dataGridView1.Rows[count - 1].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = count - 1;
            }
        }

        private void вверхToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var count = dataGridView1.Rows.Count;
            if (count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    SelectLastRow();
                }
                else
                {
                    var rowToSelect = dataGridView1.SelectedRows[0].Index - 1;
                    if (rowToSelect > -1)
                    {
                        ClearSelectedRows();
                        dataGridView1.Rows[rowToSelect].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = rowToSelect;
                    }
                }
            }
        }

        private void внизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var count = dataGridView1.Rows.Count;
            if (count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    SelectFirstRow();
                }
                else
                {
                    var rowToSelect = dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count - 1].Index + 1;
                    if (rowToSelect < count)
                    {
                        ClearSelectedRows();
                        dataGridView1.Rows[rowToSelect].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = rowToSelect;
                    }
                }
            }
        }


        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddJournalForm addJournalForm = new AddJournalForm(AddNewJournal, User.Employee);
            addJournalForm.ShowDialog(this);
            PutScrollBarDown();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                ChangeJournalForm changeJournalForm = new ChangeJournalForm(Journals[row.Index], User.Employee);
                changeJournalForm.ShowDialog(this);
                UpdateRowInDataGrid(changeJournalForm.Journal, row.Index);
            }

        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContinueChoiceForm form = new ContinueChoiceForm("Вы действительно хотите удалить запись?");
            form.ShowDialog();
            if (form.IsContinue)
            {
                IJournalRepository repository = ServiceChannelManager.Instance.JournalRepository;
                var rows = dataGridView1.SelectedRows;
                List<UilJournal> journalsForRemoving = new List<UilJournal>();
                foreach (DataGridViewRow row in rows)
                {
                    journalsForRemoving.Add(Journals[row.Index]);
                    repository.Delete(Journals[row.Index]);
                    dataGridView1.Rows.Remove(row);
                }
                foreach (var journal in journalsForRemoving)
                {
                    Journals.Remove(journal);
                }
            }
        }

        Filtration filtrationForm = null;
        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filtrationForm == null)
            {
                filtrationForm = new Filtration(RefreshDataGrid);
                filtration = filtrationForm.JournalFiltration;
            }
            filtrationForm.Show();

        }

        private void протоколыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportControlResultForm chooseControlNameForm = new ExportControlResultForm();
            chooseControlNameForm.ShowDialog(this);
            List<UilJournal> SelectedJournals = new List<UilJournal>();

            if (chooseControlNameForm.SelectedControlName != null)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Visible)
                    {
                        SelectedJournals.Add(Journals[row.Index]);
                    }
                }
                ExportMethod exportMethod;
                saveFileDialog1.FileName = chooseControlNameForm.SelectedControlName.Name;
                if (chooseControlNameForm.isPdfSelected)
                {
                    saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
                    
                    exportMethod = ConvertManager.ConvertChosenControlResultsToPdf;
                }
                else
                {
                    saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
                    exportMethod = ConvertManager.ConvertChosenControlResultsToExcel;
                }

                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    exportMethod(chooseControlNameForm.SelectedControlName, SelectedJournals, saveFileDialog1.FileName);
                }

            }
        }

        private void экспортPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Журнал";
            saveFileDialog1.Filter = "PDF file (*.pdf)|*.pdf";
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                ConvertManager.ConvertDataGridToPdf(dataGridView1, saveFileDialog1.FileName);
            }
        }

        private void экспортExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog1.FileName = "Журнал";
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                ConvertManager.ConvertDataGridToExcel(dataGridView1, saveFileDialog1.FileName);
            }
        }
    }
}
