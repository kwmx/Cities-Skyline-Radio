using System;
using MetroFramework.Forms;
using System.Windows.Forms;
using Radio;
using System.Collections.Generic;
using System.Xml;

namespace Radio_Exporter
{
    public partial class Simple : MetroForm
    {

        public Simple()
        {
            InitializeComponent();
        }

        private void Simple_Load(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {
            txtTitle.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string url = txtUrl.Text;
            if (title == "" || title == " " || title == null || title == string.Empty)
                title = "Unknown";
            if (url != "" && url != " " && url != null)
            {
                txtList.Text += "\"" + title + "\"" + "-!-\"" + url + "\"" + Environment.NewLine;
                if (!btnExport.Enabled)
                    btnExport.Enabled = true;
                txtTitle.Text = string.Empty;
                txtUrl.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Incorrect format for URL");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            List<Channel> channels = new List<Channel>();
            bool writeToFile = true;
            string[] lines = txtList.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                string[] words = line.Split(new string[] { "-!-" }, StringSplitOptions.None);
                if (words.Length < 2)
                {
                    continue;
                }
                if (words.Length != 2 || !words[0].Contains("\"") || !words[1].Contains("\"") || !words[1].Contains("http"))
                {
                    writeToFile = false;
                    MessageBox.Show("Error where line = '" + line + "'. Incorrect format" + " 0: " + words[0] + " 1:" + words[1] + ". Line has been ignored");
                    continue;
                }
                else
                {
                    string title = words[0].Replace("\"", "");
                    string url = words[1].Replace("\"", "");
                    Channel temp = new Channel(title, url);
                    channels.Add(temp);
                }
            }
            if (writeToFile && channels.Count > 0)
                listToXml(channels, txtName.Text);
        }
        //---------------------------------------------------------------------------
        //Get list of channels and file name and then write it to file
        //---------------------------------------------------------------------------
        private void listToXml(List<Channel> channelsList, string name = "radio")
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };
            name += ".rxm";
            using (XmlWriter writer = XmlWriter.Create(name, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Radio");
                foreach (Channel channel in channelsList)
                {
                    writer.WriteStartElement("Channel");
                    writer.WriteAttributeString("Title", null, channel.Title);
                    writer.WriteAttributeString("Url", null, channel.Url);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
