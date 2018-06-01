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

namespace NicoTube
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            NicoTube.API.Login.LoginToNicovideo("mail", "pass");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtVidId.Text == "") return;

            var client = NicoVideo.GetWebClientForFlv(txtVidId.Text);

            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;

            var info = FlvInfo.Create(txtVidId.Text);
            client.DownloadFileAsync(new Uri(info.FlvUrl), txtVidId.Text + ".flv");
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
    }
}
