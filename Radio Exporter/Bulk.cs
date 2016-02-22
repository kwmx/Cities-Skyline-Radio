using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Xml;
using Radio;
namespace Radio_Exporter
{
    public partial class Bulk : MetroForm
    {
        public Bulk()
        {
            InitializeComponent();
        }
        //---------------------------------------------------------------------------
        //Export settings to file
        //---------------------------------------------------------------------------
        private void metroButton1_Click(object sender, EventArgs e)
        {
            List<Channel> channels = new List<Channel>();
            bool writeToFile = true;
            string[] lines = txtChannels.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach(string line in lines)
            {
                string[] words = line.Split(new string[] { "-!-" }, StringSplitOptions.None);
                if(words.Length < 2)
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
                listToXml(channels, txtFileName.Text);
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
        //---------------------------------------------------------------------------
        //Read from a file
        //---------------------------------------------------------------------------
        private void metroButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Radio XML file (.rxm)|*.rxm";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Channel"))
                        {
                            if (reader.HasAttributes)
                            {
                                string title = reader.GetAttribute("Title");
                                string url = reader.GetAttribute("Url");
                                if (title == "" || title == " ")
                                    title = "Unknown";
                                txtChannels.Text += "\"" + title + "\"-!-" + "\"" + url + "\"" + Environment.NewLine;
                            }
                        }
                    }
                }
            }
        }
        //---------------------------------------------------------------------------
        //Add an empty line to the textbox (""-!-"")
        //---------------------------------------------------------------------------
        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (txtChannels.Text.Length < 1)
            {
                txtChannels.Text +="\"\"-!-\"\"";
                return;
            }
            string[] txt = txtChannels.Text.Split('\n');
            txtChannels.Text = string.Empty;
            foreach(string text in txt)
            {
                if (txtChannels.Text.Length < 1)
                    txtChannels.Text += text;
                else
                    txtChannels.Text += Environment.NewLine + text;
            }
            txtChannels.Text += Environment.NewLine + "\"\"-!-\"\"";
        }
    }
}
