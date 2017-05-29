using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Configuration;
using System.Data.SqlClient;
using QualityControl;
using System.IO;

namespace QualityControl_Server
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        MainForm parent;
        AppConfigManager appConfigManager;


        public Settings(MainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            appConfigManager = new AppConfigManager();
        }

        private string connectionString = "";

        private void Settings_Load(object sender, EventArgs e)
        {           
            openFileDialog1.Filter = "MDF files (*.mdf)|*.mdf";
            connectionString = appConfigManager.GetConnectionString();
            textBox1.Text = appConfigManager.GetAttachDbFileName();
            textBox2.Text = appConfigManager.GetTagValue(appConfigManager.outputLocationTag);
            checkBox1.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.clearEquipmentAfterAdding));
            checkBox2.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.clearDefectsAfterAdding));
            checkBox3.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.clearEmployeesAfterAdding));
            checkBox4.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.copyEmployeesToAllTypesOfMethods));
            checkBox5.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.userIsReviewer));
            checkBox6.Checked = bool.Parse(appConfigManager.GetTagValue(appConfigManager.hideControlMethods));
            numericUpDown1.Value = int.Parse(appConfigManager.GetTagValue(appConfigManager.daysBeforeDeadline));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveDbLocation();
            SaveOutputLocation();
            appConfigManager.ChangeTagValue(appConfigManager.clearEquipmentAfterAdding, checkBox1.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.clearDefectsAfterAdding, checkBox2.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.clearEmployeesAfterAdding, checkBox3.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.copyEmployeesToAllTypesOfMethods, checkBox4.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.userIsReviewer, checkBox5.Checked.ToString());
            appConfigManager.ChangeTagValue(appConfigManager.hideControlMethods, checkBox6.Checked.ToString());
            if (checkBox6.Checked)
            {
                parent.HideControlMethodTab();
            }
            else
            {
                parent.ShowControlMethodTab();
            }
            appConfigManager.ChangeTagValue(appConfigManager.daysBeforeDeadline, numericUpDown1.Value.ToString());
            Close();
        }

        private void SaveOutputLocation()
        {
            appConfigManager.ChangeOutputLocation(textBox2.Text);
            parent.SetOutputLocation();
            Close();
        }

        private void SaveDbLocation()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            if (appConfigManager.GetAttachDbFileName() != textBox1.Text)
            {
                builder.AttachDBFilename = textBox1.Text;
                appConfigManager.ChangeConnectionString(builder.ConnectionString);
                MessageBox.Show("База данных изменена успешно. Необходимо перезапустить программу.", "Оповещение");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string path = Directory.GetFiles(fbd.SelectedPath)[0];
                    textBox2.Text = path.Substring(0, path.LastIndexOf("\\") + 1);

                    // System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }


    }
}
