using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QualityControl_Server
{
    class AppConfigManager
    {
        private const string connectionStringName = "ServiceDB";
        public string outputLocationTag = "OutputLocation";
        public string clearEquipmentAfterAdding = "clearEquipmentAfterAdding";
        public string clearDefectsAfterAdding = "clearDefectsAfterAdding";
        public string clearEmployeesAfterAdding = "clearEmployeesAfterAdding";
        public string copyEmployeesToAllTypesOfMethods = "copyEmployeesToAllTypesOfProtocols";
        public string userIsReviewer = "userIsReviewer";
        public string hideControlMethods = "hideControlMethods";
        public string daysBeforeDeadline = "daysBeforeDeadline";

        public AppConfigManager()
        {

        }

        public bool ChangeConnectionString(string newValue)
        {
            try
            {
                //CreateXDocument and load configuration file
                XDocument doc = XDocument.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                //Find all connection strings
                var query1 = from p in doc.Descendants("connectionStrings").Descendants()
                             select p;

                //Go through each connection string elements find atribute specified by argument and replace its value with newVAlue
                foreach (var child in query1)
                {
                    foreach (var atr in child.Attributes())
                    {
                        if (atr.Name.LocalName == "name" && atr.Value == connectionStringName)
                            if (atr.NextAttribute != null && atr.NextAttribute.Name == "connectionString")
                            {
                                // Create the EF connection string from existing
                                EntityConnectionStringBuilder entityBuilder =
                                new EntityConnectionStringBuilder(atr.NextAttribute.Value);
                                //
                                entityBuilder.ProviderConnectionString = newValue;
                                //back the modified connection string to the configuration file
                                atr.NextAttribute.Value = entityBuilder.ToString();
                            }
                    }
                }

                doc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

            return connectionString;
        }

        public string GetAttachDbFileName()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

            return builder.AttachDBFilename;
        }

        public string GetFilePath()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
            return config.FilePath;
        }

        public void ChangeOutputLocation(string path)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
            //config.AppSettings.Settings.Add("OutputLocation", "C:\\");
            //config.Save(ConfigurationSaveMode.Minimal);
            config.AppSettings.Settings[outputLocationTag].Value = path;
            config.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public string GetOutputLocation()
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return configuration.AppSettings.Settings["OutputLocation"]?.Value;
        }

        public string GetTagValue(string tag)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return configuration.AppSettings.Settings[tag]?.Value;
        }

        public void ChangeTagValue(string tag, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
            config.AppSettings.Settings[tag].Value = value;
            config.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public void CreateTag(string tag, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(System.Windows.Forms.Application.ExecutablePath);
            config.AppSettings.Settings.Add(tag, value);
            config.Save(ConfigurationSaveMode.Minimal);
        }
    }
}
