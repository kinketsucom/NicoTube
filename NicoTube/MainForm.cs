using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using NicoTube.API;
using System.Web;
using NicoTube.Menu;
namespace NicoTube
{
    public partial class MainForm : Form
    {
        private CookieContainer cc = new CookieContainer();

        public MainForm()
        {
            InitializeComponent();
            SettingsDBHelper sdb = new SettingsDBHelper();
            string mail = sdb.getSettingValue("mail");
            string pass = sdb.getSettingValue("pass");
            cc = Login.LoginToNicovideo(mail, pass);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtVidId.Text == "") return;

            Console.WriteLine("button1_Click");
            // クッキー確認
            foreach (Cookie c in cc.GetCookies(new Uri("https://secure.nicovideo.jp/")))
            {
                Console.WriteLine("クッキー名:" + c.Name.ToString());
                Console.WriteLine("値:" + c.Value.ToString());
                Console.WriteLine("ドメイン名:" + c.Domain.ToString());
            }

            var client = NicoVideo.GetWebClientForFlv(txtVidId.Text,cc);

            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;

            var info = FlvInfo.Create(txtVidId.Text,cc);
            string url = info.FlvUrl;
            string deurl = HttpUtility.UrlDecode(url);
            string murl = info.MessageServer;
            string demurl = HttpUtility.UrlDecode(murl);
            var uri = new Uri(deurl);
            string thread = info.ThreadId;
            //ダウンロード実行
            //client.DownloadFileAsync(uri, txtVidId.Text + ".flv");
            string comment_xml = NicoVideo.GetComment(demurl,thread,cc);
            Console.WriteLine(comment_xml);

        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Finished");
            (sender as CustomWebClient).Dispose();
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressDownload.Value = e.ProgressPercentage;
        }

        private void アカウント設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form setting_form = new NicoTube.Menu.Settings();
            setting_form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new NicoTube.Menu.SettingsDBHelper().onCreate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
