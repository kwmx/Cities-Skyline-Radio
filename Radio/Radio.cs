using System;
using UnityEngine;
using ICities;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Collections;
using ColossalFramework.UI;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.XPath;
using UnityEngine.Assertions;

namespace Radio
{
    public class Radio : IUserMod
    {
        public string Name { get { return "Radio"; } }
        public string Description { get { return "listen to radio in game"; } }

        private string rxmPath = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\radio.rxm";
        public static rStream radioStream;
        private string[] stationNames;
        private bool initializedVars = false;
        public void iniVars()
        {
            if (!initializedVars)
            {
                if (!File.Exists(rxmPath))
                    makeFile();
                radioStream = new rStream(getContent(), false, 50);
                stationNames = getRadioString();
                initializedVars = true;
            }
        }
        public void OnSettingsUI(UIHelperBase helper)
        {
            iniVars();
            UIHelperBase group = helper.AddGroup("Radio");
            group.AddCheckbox("Enable radio", radioStream.Active, EventCheck);
            group.AddDropdown("Radio Channels", stationNames, -1, EventSel);
            group.AddSpace(50);
            group.AddSlider("Volume", 0, 100, 1f, (float)radioStream.Volume, EventSlide);
            group.AddButton("Play", playButton);
            group.AddButton("Stop", stopButton);
            group.AddButton("Next", nextButton);
            group.AddButton("Previous", previousButton);
            group.AddSpace(150);
            group.AddButton("Refresh", refreshBtn);
            group.AddButton("Open config folder", openBtn);
            group.AddButton("Backup file", backupFile);
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

        private void refreshBtn()
        {
            radioStream = new rStream(getContent(), radioStream.Active, radioStream.Volume);
            stationNames = getRadioString();
        }
        private void openBtn()
        {
            string pth = Environment.GetEnvironmentVariable("LocalAppData") + @"\Colossal Order\Cities_Skylines\Addons\Mods\Radio\";
            if (!Directory.Exists(pth))
                Directory.CreateDirectory(pth);
            Process.Start(pth);
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
        //---------------------------------------------------------------------------
        //Send back all the titles in the xml files (except for the sample ones)
        //---------------------------------------------------------------------------
        // The following methods are to read and write information from the config file radio.rxm
        // These methods use the simple class Channels
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
                chn[1] = new Channel("Mix FM", "http://soho.wavestreamer.com:3696/Live");
                foreach (Channel channel in chn)
                {
                    if (channel.Url.Contains("#"))
                        writer.WriteStartElement("Channel-Sample");
                    else
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
        
    }
    //---------------------------------------------------------------------------
    //In-game UI
    //---------------------------------------------------------------------------
    public class LoadingExtension : LoadingExtensionBase
    {
        public GameObject obj = null;
        public override void OnLevelLoaded(LoadMode mode)
        {
            // Get the UIView object. This seems to be the top-level object for most
            // of the UI.
            var uiView = UIView.GetAView();

            // Add a new button to the view.
            var button = (UIButton)uiView.AddUIComponent(typeof(UIButton));

            // Set the text to show on the button.
            button.text = "Radio";
            button.tooltip = "Radio control panel";
            // Set the button dimensions.
            button.width = 100;
            button.height = 30;

            // Style the button to look like a menu button.
            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenuFocused";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.textColor = new Color32(255, 255, 255, 255);
            button.disabledTextColor = new Color32(7, 7, 7, 255);
            button.hoveredTextColor = new Color32(7, 132, 255, 255);
            button.focusedTextColor = new Color32(255, 255, 255, 255);
            button.pressedTextColor = new Color32(30, 30, 44, 255);

            // Enable button sounds.
            button.playAudioEvents = false;

            // Place the button.
            button.transformPosition = new Vector3(-1.65f, 0.97f);

            // Respond to button click.
            button.eventClick += openPanel;
        }

        private void openPanel(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (obj == null)
            {
                UIView v = UIView.GetAView();
                obj = new GameObject("Panel", typeof(RadioPanel));
                UIComponent uic = v.AttachUIComponent(obj);
            }
            else
            {
                GameObject.DestroyObject(obj);
                obj = null;
            }
        }
    }
    
    //whatever child class of UIComponent, here UIPanel
    public class RadioPanel : UIPanel
    {
        private UILabel currentTitle;
        private UILabel vol;
        //these overrides are not necessary, but obviously helpful
        //this seems to be called initially
        public override void Start()
        {
            
            this.backgroundSprite = "GenericPanel";
            this.color = Color.grey;
            this.width = 165;
            this.height = 65;
            this.transformPosition = new Vector3(-1.65f, 0.90f);
            currentTitle = this.AddUIComponent<UILabel>();
            vol = this.AddUIComponent<UILabel>();
            currentTitle.text = shortenString(Radio.radioStream.currentTitle);
            vol.text = "Volume: " + Radio.radioStream.Volume;
            var btnPlay = this.AddUIComponent<UIButton>();
            iniBtn(ref btnPlay, "►", 20, 20);
            var btnStop = this.AddUIComponent<UIButton>();
            iniBtn(ref btnStop, "■", 20, 20);
            var btnVolUp = this.AddUIComponent<UIButton>();
            iniBtn(ref btnVolUp, "▲", 20, 20);
            var btnVolDown = this.AddUIComponent<UIButton>();
            iniBtn(ref btnVolDown, "▼", 20, 20);
            var btnNext = this.AddUIComponent<UIButton>();
            iniBtn(ref btnNext, "►►", 35, 20);
            var btnBack = this.AddUIComponent<UIButton>();
            iniBtn(ref btnBack, "◄◄", 35, 20);
            var btnClose = this.AddUIComponent<UIButton>();
            iniBtn(ref btnClose, "✘", 15, 15);
            
            
            
            // Place the buttons.
            btnPlay.relativePosition = new Vector3(82f, 25f);
            btnStop.relativePosition = new Vector3(62f, 25f);
            btnVolUp.relativePosition = new Vector3(102f, 25f);
            btnVolDown.relativePosition = new Vector3(42f, 25f);
            btnNext.relativePosition = new Vector3(127f, 25f);
            btnBack.relativePosition = new Vector3(3f, 25f);
            //Place the close button
            btnClose.relativePosition = new Vector3(0f,0f);

            //Place labels
            currentTitle.relativePosition = new Vector3(20f, 5f);
            vol.relativePosition = new Vector3(42f, 48f);
            //Buttons and their events
            btnPlay.eventClick += btnPlayClicked;
            btnStop.eventClick += btnStopClicked;
            btnVolUp.eventClick += btnVolUpClicked;
            btnVolDown.eventClick += btnVolDownClicked;
            btnNext.eventClick += btnNextClicked;
            btnBack.eventClick += btnBackClicked;
            btnClose.eventClick += btnCloseClicked;
        }
        //Handle play button
        public void btnPlayClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            Radio.radioStream.playCurrent();
        }
        //Handle stop button
        public void btnStopClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            Radio.radioStream.PlayerStop();
        }
        //Handle next button
        public void btnNextClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            Radio.radioStream.playForward();
        }
        //Handle back button
        public void btnBackClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            Radio.radioStream.playBackward();
        }
        //Handle volume up button
        public void btnVolUpClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            Radio.radioStream.refVolume(++Radio.radioStream.Volume);
        }
        //Handle volume down button
        public void btnVolDownClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            Radio.radioStream.refVolume(--Radio.radioStream.Volume);
        }
        //Hanlde the close button
        public void btnCloseClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            DestroyObject(this.gameObject);
        }
        //Set a button text, coloring and size
        public void iniBtn(ref UIButton button, string text, int Width, int Heigth, bool soundEnabled = false)
        {
            // Set the text to show on the button.
            button.text = text;
            // Set the button dimensions.
            button.width = Width;
            button.height = Heigth;
            // Style the button to look like a menu button.
            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenuFocused";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.textColor = new Color32(255, 255, 255, 255);
            button.disabledTextColor = new Color32(7, 7, 7, 255);
            button.hoveredTextColor = new Color32(7, 132, 255, 255);
            button.focusedTextColor = new Color32(255, 255, 255, 255);
            button.pressedTextColor = new Color32(30, 30, 44, 255);
            
            // Enable button sounds.
            button.playAudioEvents = soundEnabled;

        }
        public override void Update()
        {
            currentTitle.text = shortenString(Radio.radioStream.currentTitle);
            vol.text = "Volume: " + Radio.radioStream.Volume;
        }
        public string shortenString(string input, int maxLength = 13, string replaceWith = "...")
        {
            string output = "";
            if (input.Length > maxLength)
            {
                output = input.Substring(0, maxLength) + replaceWith;
            }
            else
                return input;
            return output;
        }
    }
}
