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
    public partial class ChooseTemplateForm : DirectoryForm
    {
        List<UilTemplate> Templates;
        UilTemplate template;

        public ChooseTemplateForm()
        {
            InitializeComponent();
            RefreshData();
        }

        public override void RefreshData()
        {
            dataGridView1.Rows.Clear();
            ITemplateRepository repository = ServiceChannelManager.Instance.TemplateRepository;
            Templates = repository.GetAll().ToList();
            foreach (var Template in Templates)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = Template.Name;
                row.Cells[1].Value = Template.Material != null ? Template.Material.Name : "";
                row.Cells[2].Value = Template.WeldJoint != null ? Template.WeldJoint.Name : "";
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)row.Cells[3];
                if (Template.EquipmentLib != null)
                {
                    foreach (UilSelectedEquipment eq in Template.EquipmentLib.SelectedEquipment)
                    {
                        comboBoxCell.Items.Add(eq.Equipment.Name);
                    }
                    if (comboBoxCell.Items.Count > 0)
                    {
                        comboBoxCell.Value = comboBoxCell.Items[0];
                    }

                }

                comboBoxCell = (DataGridViewComboBoxCell)row.Cells[4];
                if (Template.ControlNameLib != null)
                {
                    foreach (UilSelectedControlName name in Template.ControlNameLib.SelectedControlName)
                    {
                        comboBoxCell.Items.Add(name.ControlName.Name);
                    }
                    if (comboBoxCell.Items.Count > 0)
                    {
                        comboBoxCell.Value = comboBoxCell.Items[0];
                    }
                }

                row.Cells[5].Value = ((Template.ImageLib != null) && (Template.ImageLib.Image.Count > 0)) ? byteArrayToImage(Template.ImageLib.Image[0].Image) : null;
                row.Cells[6].Value = Template.Description;

                comboBoxCell = (DataGridViewComboBoxCell)row.Cells[7];
                if (Template.RequirementDocumentationLib != null)
                {
                    foreach (UilSelectedRequirementDocumentation doc in Template.RequirementDocumentationLib.SelectedRequirementDocumentation)
                    {
                        comboBoxCell.Items.Add(doc.RequirementDocumentation.Name);
                    }
                    if (comboBoxCell.Items.Count > 0)
                    {
                        comboBoxCell.Value = comboBoxCell.Items[0];
                    }

                }

                row.MinimumHeight = 70;
                dataGridView1.Rows.Add(row);
            }
        }

        override protected void button1_Click(object sender, EventArgs e)
        {
            AddTemplateForm AddTemplateForm = new AddTemplateForm(this);
            AddTemplateForm.ShowDialog(this);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            ITemplateRepository repository = ServiceChannelManager.Instance.TemplateRepository;
            var rows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                repository.Delete(Templates[row.Index]);
            }
            RefreshData();
        }

        protected override void button3_Click(object sender, EventArgs e)
        {
            ITemplateRepository repository = ServiceChannelManager.Instance.TemplateRepository;
            var rows = dataGridView1.SelectedRows;
            List<DataGridViewRow> rowsList = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in rows)
            {
                rowsList.Add(row);
            }
            for (int i = rowsList.Count - 1; i >= 0; i--)
            {
                ChangeTemplateForm changeTemplateForm = new ChangeTemplateForm(this, Templates[rowsList[i].Index]);
                changeTemplateForm.ShowDialog(this);
            }
            RefreshData();
        }

        private Image byteArrayToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                return Image.FromStream(ms);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count != 1)
            {
                MessageBox.Show("Выберите только одну строку таблицы");
            }
            else
            {
                template = Templates[rows[0].Index];
                this.Close();
            }
        }

        public UilTemplate GetChosenTemplate()
        {
            return template;
        }

    }
}
