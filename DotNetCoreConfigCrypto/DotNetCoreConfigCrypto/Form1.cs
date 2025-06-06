using System.Configuration;
using System.Text;

namespace DotNetCoreConfigCrypto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string providerName = providerNameText.Text;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = dialog.FileName;

                var backupFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileName(filePath) + ".txt");
                File.Copy(filePath, backupFilePath);

                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                configMap.ExeConfigFilename = filePath;
                var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

                if (config.AppSettings != null && !config.AppSettings.SectionInformation.IsProtected)
                {
                    config.AppSettings.SectionInformation.ProtectSection(providerName);
                    config.AppSettings.SectionInformation.ForceSave = true;
                    config.Save();
                }
            }
        }
    }
}