using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NicoTube.Menu
{
    public partial class Settings : Form
    {
        public const string mail = "mail";
        public const string pass = "pass";


        public Settings()
        {
            InitializeComponent();
        }

        private void SaveSettings()
        {
            var settingsDBHelper = new SettingsDBHelper();
            settingsDBHelper.updateSettings(mail, this.mail_textBox.Text);
            settingsDBHelper.updateSettings(pass, this.pass_textBox.Text);

        }

        static private string getSettingStr(string key)
        {
            var settingsDBHelper = new SettingsDBHelper();
            string rst = settingsDBHelper.getSettingValue(key);
            if (String.IsNullOrEmpty(rst)) return "";
            return rst;
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.mail_textBox.Text = getMail();
            this.pass_textBox.Text = getPass();
        }
        static public string getMail()
        {
            return getSettingStr(mail);
        }
        static public string getPass()
        {
            return getSettingStr(pass);
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }
    }





}
