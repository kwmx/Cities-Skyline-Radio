using System;
using UnityEngine;
using ICities;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Collections;

namespace Radio
{
    public class Radio : IUserMod
    {
        public string Name { get { return "Radio"; } }
        public string Description { get { return "Liesten to radio in game"; } }

        private string rxmPath = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\radio.rxm";
        private rStream radioStream;
        private string[] stationNames;
        private bool initializedVars = false;

        public void OnSettingsUI(UIHelperBase helper)
        {
            if (!initializedVars)
            {
                if (!File.Exists(rxmPath))
                    makeFile();
                radioStream = new rStream(getContent(), false, 50);
                stationNames = getRadioString();
                initializedVars = true;
            }
            UIHelperBase group = helper.AddGroup("Radio");
            group.AddCheckbox("Show radio", radioStream.Active, EventCheck);
            group.AddDropdown("Radio Channels", stationNames, -1, EventSel);
            group.AddSpace(50);
            group.AddSlider("Volume", 0, 100, 1f, (float)radioStream.Volume, EventSlide);
            group.AddButton("Play", playButton);
            group.AddButton("Stop", stopButton);
            group.AddButton("Next", nextButton);
            group.AddButton("Previous", previousButton);
            group.AddSpace(150);
            group.AddButton("Download Radio Config", EventClick);
        }
        //---------------------------------------------------------------------------
        //Events.
        //---------------------------------------------------------------------------
        private void EventCheck(bool c)
        {
            radioStream.Active = c;
            if (!c)
                radioStream.PlayerStop();
        }

        private void EventSlide(float c)
        {
            radioStream.refVolume((int)c);
        }

        private void EventSel(int c)
        {
            radioStream.selectFromList(c);
        }
        private void EventClick()
        {
            downloadXML();
            radioStream.streamChannels = getContent();
            stationNames = getRadioString();
        }
        private void playButton()
        {
            radioStream.playCurrent();
        }
        private void stopButton()
        {
            radioStream.PlayerStop();
        }
        private void nextButton()
        {
            radioStream.playForward();
        }
        private void previousButton()
        {
            radioStream.playBackward();
        }
        /// The following methods are to read and write information from the config file radio.rxm
        /// These methods use the simple class Channels
        //---------------------------------------------------------------------------
        //Send back all the titles in the xml files (except for the sample ones)
        //---------------------------------------------------------------------------
        private string[] getRadioString()
        {
            int x = 0;
            if (!File.Exists(rxmPath))
                makeFile();
            string[] returnArray = new string[getContent().Count];
            using (XmlReader reader = XmlReader.Create(rxmPath))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Channel"))
                    {
                        if (reader.HasAttributes)
                        {
                            string title = reader.GetAttribute("Title");
                            string url = reader.GetAttribute("Url");

                            if (url != "" || url != " " || url != string.Empty)
                            {
                                if (title == "" || title == " " || title == string.Empty)
                                    title = "Unknown";
                                returnArray[x] = title;
                                ++x;
                            }
                        }
                    }
                }
            }
            return returnArray;
        }
        //---------------------------------------------------------------------------
        //Create a new file with a sample channel
        //---------------------------------------------------------------------------
        private void makeFile()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };
            if (!Directory.Exists(Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\"))
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\");
            using (XmlWriter writer = XmlWriter.Create(rxmPath, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Radio");
                Channel[] chn = new Channel[2];
                chn[0] = new Channel("#This will be the title of the channel", "#This will be the url");
                chn[1] = new Channel("#This will be the title of the channel", "#This will be the url");
                foreach (Channel channel in chn)
                {
                    writer.WriteStartElement("Channel-Sample");

                    writer.WriteAttributeString("Title", null, channel.Title);
                    writer.WriteAttributeString("Url", null, channel.Url);

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
        //---------------------------------------------------------------------------
        //Read from radio.rxm file and return it as a Channel list
        //---------------------------------------------------------------------------
        List<Channel> getContent()
        {
            if (!File.Exists(rxmPath))
                makeFile();
            List<Channel> list = new List<Channel>();
            using (XmlReader reader = XmlReader.Create(rxmPath))
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
                            Channel radioChannel = new Channel(title, url);
                            list.Add(radioChannel);
                        }
                    }
                }
            }
            return list;
        }
        //---------------------------------------------------------------------------
        //Check if a current radio.rxm file exits and back it up
        //---------------------------------------------------------------------------
        private void backupFile()
        {
            string DirrxmPath = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\";
            FileInfo fi = new FileInfo(rxmPath);
            if (fi.Exists)
            {
                string newName = "_backup_radio_" + DateTime.Now.ToString().Replace('/', '_').Replace(' ', '_').Replace(':', '_').Replace('\\','_') + ".rxm";
                string fullpath = Path.Combine(DirrxmPath, newName);
                fi.MoveTo(fullpath);
            }
        }
        //---------------------------------------------------------------------------
        //Download a collection of radio streams
        //---------------------------------------------------------------------------
        public void downloadXML()
        { 
            backupFile();
            string downUrl = "https://faisal-k.com/radio/radio.rxm";
            string txt = getText(downUrl);
            File.WriteAllText(rxmPath, txt);
        }

        public string getText(string Link)
        {
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
    }
}
