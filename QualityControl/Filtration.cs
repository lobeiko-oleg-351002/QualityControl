using QualityControl_Client.Forms.ComponentDirectory;
using QualityControl_Client.Forms.IndustrialObjectDirectory;
using QualityControl_Client.Forms.MaterialDirectory;
using QualityControl_Client.Forms.WeldJointDirectory;
using QualityControl_Client.Forms.CustomerDirectory;
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
using DAL.Repositories.Interface;

namespace QualityControl_Client
{
    public partial class Filtration : Form
    {
        DateTime requestDateLeft = DateTime.Now, requestDateRight = DateTime.Now, controlDateLeft = DateTime.Now, controlDateRight = DateTime.Now;
        int amountLeft, amountRight;
        int requestNumber;
        BllComponent component;
        BllMaterial material;
        BllWeldJoint weldJoint;
        BllIndustrialObject industrialObject;
        BllCustomer customer;
        string contract;
        string size;
        IUnitOfWork uow;
        List<Func<BllJournal, bool>> filters = new List<Func<BllJournal, bool>>();

        bool fit_vik, fit_uzk, fit_pvk, fit_rgk, unfit_vik, unfit_uzk, unfit_pvk, unfit_rgk;

        public Filtration(IUnitOfWork uow)
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            this.uow = uow;
        }

        public Filtration()
        {
            InitializeComponent();
        }

        Action RefreshDataGrid;

        public Filtration(Action RefreshDataGrid)
        {
            InitializeComponent();
            this.RefreshDataGrid = RefreshDataGrid;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                filters.Add(RequestDateFiltration);
            }
            else
            {
                filters.Remove(RequestDateFiltration);
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                filters.Add(ControlDateFiltration);
                dateTimePicker3.Enabled = true;
                dateTimePicker4.Enabled = true;
            }
            else
            {
                filters.Remove(ControlDateFiltration);
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                filters.Add(AmountFiltration);
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
            }
            else
            {
                filters.Remove(AmountFiltration);
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                filters.Add(RequestNumberFiltration);
                numericUpDown3.Enabled = true;
            }
            else
            {
                filters.Remove(RequestNumberFiltration);
                numericUpDown3.Enabled = false;
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {
                filters.Add(ComponentFiltration);
                textBox1.Enabled = true;
                button1.Enabled = true;
            }
            else
            {
                filters.Remove(ComponentFiltration);
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {
                filters.Add(MaterialFiltration);
                textBox2.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                filters.Remove(MaterialFiltration);
                textBox2.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked)
            {
                filters.Add(WeldJointFiltration);
                textBox3.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                filters.Remove(WeldJointFiltration);
                textBox3.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                filters.Add(SizeFiltration);
                textBox4.Enabled = true;
            }
            else
            {
                filters.Remove(SizeFiltration);
                textBox4.Enabled = false;
            }
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked)
            {
                filters.Add(IndustrialObjectFiltration);
                textBox5.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                filters.Remove(IndustrialObjectFiltration);
                textBox5.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                fit_vik = true;
            }
            else
            {
                fit_vik = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                unfit_vik = true;
            }
            else
            {
                unfit_vik = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                fit_uzk = true;
            }
            else
            {
                fit_uzk = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                unfit_uzk = true;
            }
            else
            {
                unfit_uzk = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                fit_pvk = true;
            }
            else
            {
                fit_pvk = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                unfit_pvk = true;
            }
            else
            {
                unfit_pvk = false;
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                fit_rgk = true;
            }
            else
            {
                fit_rgk = false;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                unfit_rgk = true;
            }
            else
            {
                unfit_rgk = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        public bool JournalFiltration(BllJournal journal)
        {
            bool isFiltered = true;
            foreach (var filter in filters)
            {
                if (!filter(journal))
                {
                    isFiltered = false;
                    break;
                }
            }

            if (isFiltered)
            {
                return true;
            }
            return false;
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked)
            {
                filters.Add(CustomerFiltration);
                textBox6.Enabled = true;
                button6.Enabled = true;
            }
            else
            {
                filters.Remove(CustomerFiltration);
                textBox6.Enabled = false;
                button6.Enabled = false;
            }
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked)
            {
                filters.Add(ContractFiltration);
                textBox7.Enabled = true;
            }
            else
            {
                filters.Remove(ContractFiltration);
                textBox7.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseComponentForm form = new ChooseComponentForm(uow);
            form.ShowDialog();
            component = form.GetChosenComponent();
            if (component != null)
            {
                textBox1.Text = component.Name;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChooseMaterialForm form = new ChooseMaterialForm(uow);
            form.ShowDialog();
            material = form.GetChosenMaterial();
            if (material != null)
            {
                textBox2.Text = material.Name;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseWeldJointForm form = new ChooseWeldJointForm(uow);
            form.ShowDialog();
            weldJoint = form.GetChosenWeldJoint();
            if (weldJoint != null)
            {
                textBox3.Text = weldJoint.Name;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChooseIndustrialObjectForm form = new ChooseIndustrialObjectForm(uow);
            form.ShowDialog();
            industrialObject = form.GetChosenIndustrialObject();
            if (industrialObject != null)
            {
                textBox1.Text = industrialObject.Name;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChooseCustomerForm form = new ChooseCustomerForm(uow);
            form.ShowDialog();
            customer = form.GetChosenCustomer();
            if (customer != null)
            {
                textBox1.Text = customer.Organization;
            }
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked)
            {
                filters.Add(VikFiltration);
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
            }
            else
            {
                filters.Remove(VikFiltration);
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
            }
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked)
            {
                filters.Add(UzkFiltration);
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
            }
            else
            {
                filters.Remove(UzkFiltration);
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
            }
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                filters.Add(PvkFiltration);
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }
            else
            {
                filters.Remove(PvkFiltration);
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
            }
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox18.Checked)
            {
                filters.Add(RgkFiltration);             
                checkBox7.Enabled = true;
                checkBox8.Enabled = true;
            }
            else
            {
                filters.Remove(RgkFiltration);
                checkBox7.Enabled = false;
                checkBox8.Enabled = false;
            }
        }



        //FILTRATION____________________________________________

        private bool RequestDateFiltration(BllJournal journal)
        {
            if (journal.RequestDate.Value.Date.CompareTo(requestDateLeft.Date) >= 0 && journal.RequestDate.Value.Date.CompareTo(requestDateRight.Date) <= 0)
            {
                return true;
            }
            return false;
        }

        private bool ControlDateFiltration(BllJournal journal)
        {
            if (journal.ControlDate.Value.Date.CompareTo(controlDateLeft.Date) >= 0 && journal.ControlDate.Value.Date.CompareTo(controlDateRight.Date) <= 0)
            {
                return true;
            }
            return false;
        }

        private bool AmountFiltration(BllJournal journal)
        {
            if (journal.Amount >= amountLeft && journal.Amount <= amountRight)
            {
                return true;
            }
            return false;
        }

        private bool RequestNumberFiltration(BllJournal journal)
        {
            if (journal.RequestNumber == requestNumber)
            {
                return true;
            }
            return false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            amountLeft = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            amountRight = (int)numericUpDown2.Value;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            requestDateLeft = dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            requestDateRight = dateTimePicker2.Value;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            controlDateLeft = dateTimePicker3.Value;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            controlDateRight = dateTimePicker4.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            requestNumber = (int)numericUpDown3.Value;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            size = textBox4.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            contract = textBox7.Text;
        }

        private bool ComponentFiltration(BllJournal journal)
        {
            if (journal.Component != null && component != null)
            {
                if (journal.Component.Id == component.Id)
                {
                    return true;
                }
            }
            return false;
        }

        private bool MaterialFiltration(BllJournal journal)
        {
            if (journal.Material != null && material != null)
            {
                if (journal.Material.Id == material.Id)
                {
                    return true;
                }
            }
            return false;
        }

        private bool WeldJointFiltration(BllJournal journal)
        {
            if (journal.WeldJoint != null && weldJoint != null)
            {
                if (journal.WeldJoint.Id == weldJoint.Id)
                {
                    return true;
                }
            }
            return false;
        }

        private bool SizeFiltration(BllJournal journal)
        {
            if (journal.Size == size)
            {
                return true;
            }
            return false;
        }

        private bool IndustrialObjectFiltration(BllJournal journal)
        {
            if (journal.IndustrialObject != null && industrialObject != null)
            {
                if (journal.IndustrialObject.Id == industrialObject.Id)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CustomerFiltration(BllJournal journal)
        {
            if (journal.Customer != null && customer != null)
            {
                if (journal.Customer.Id == customer.Id)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContractFiltration(BllJournal journal)
        {
            if (journal.Contract == contract)
            {
                return true;
            }
            return false;
        }

        private bool VikFiltration(BllJournal journal)
        {
            var controls = journal.ControlMethodsLib.Control;
            foreach(var control in controls)
            {
                if (control.ControlName.Name == "ВИК")
                {
                    if (fit_vik && control.IsControlled.Value) return true;
                    if (unfit_vik && !control.IsControlled.Value) return true;
                    break;
                }
            }
            if (!fit_vik && !unfit_vik) return true;
            return false;
        }

        private bool UzkFiltration(BllJournal journal)
        {
            var controls = journal.ControlMethodsLib.Control;
            foreach (var control in controls)
            {
                if (control.ControlName.Name == "УЗК")
                {
                    if (fit_uzk && control.IsControlled.Value) return true;
                    if (unfit_uzk && !control.IsControlled.Value) return true;
                    break;
                }
            }
            if (!fit_uzk && !unfit_uzk) return true;
            return false;
        }

        private bool PvkFiltration(BllJournal journal)
        {
            var controls = journal.ControlMethodsLib.Control;
            foreach (var control in controls)
            {
                if (control.ControlName.Name == "ПВК")
                {
                    if (fit_pvk && control.IsControlled.Value) return true;
                    if (unfit_pvk && !control.IsControlled.Value) return true;
                    break;
                }
            }
            if (!fit_pvk && !unfit_pvk) return true;
            return false;
        }

        private bool RgkFiltration(BllJournal journal)
        {
            var controls = journal.ControlMethodsLib.Control;
            foreach (var control in controls)
            {
                if (control.ControlName.Name == "РГК")
                {
                    if (fit_rgk && control.IsControlled.Value) return true;
                    if (unfit_rgk && !control.IsControlled.Value) return true;
                    break;
                }
            }
            if (!fit_rgk && !unfit_rgk) return true;
            return false;
        }
    }
}
