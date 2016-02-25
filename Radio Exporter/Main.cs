using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.IO;
using System.Diagnostics;
using Radio;
using System.Net;

namespace Radio_Exporter
{
    public partial class Main : MetroForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Bulk blk = new Bulk();
            blk.Show();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            string rxmPath = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio";
            if(!Directory.Exists(rxmPath))
                Directory.CreateDirectory(rxmPath);
               Process.Start(rxmPath);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Simple s = new Simple();
            s.Show();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Player p = new Player();
            p.Show();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            downloadXML();
        }
        public void downloadXML()
        {
            string rxmPath = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\radio.rxm";
            string dir = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (File.Exists(rxmPath))
                backupFile();
            string downUrl = "https://faisal-k.com/radio/radio.rxm";
            string txt = getText(downUrl);
            File.WriteAllText(rxmPath, txt);
        }

        public string getText(string Link)
        {
            string rxmPath = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\radio.rxm";
            string output = string.Empty;
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                var webRequest = WebRequest.Create(@"" + Link);

                using (var response = webRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    var strContent = reader.ReadToEnd();
                    output = strContent;
                }
            }
            return output;
        }
        private void backupFile()
        {
            string rxmPath = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\radio.rxm";
            string DirrxmPath = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\";
            FileInfo fi = new FileInfo(rxmPath);
            if (fi.Exists)
            {
                string newName = "_backup_radio_" + DateTime.Now.ToString().Replace('/', '_').Replace(' ','_').Replace(':','_') + ".rxm";
                string fullpath = Path.Combine(DirrxmPath, newName);
                fi.MoveTo(fullpath);
            }
        }
    }
}
