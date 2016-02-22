using System.Collections.Generic;
using System.Linq;
using WMPLib;

namespace Radio
{
    public class rStream
    {
        private List<Channel> Channels;
        private bool activeRadio = false; // Checks if the user set the radio to on or off
        private WindowsMediaPlayer player = new WindowsMediaPlayer(); // Player for radio
        private int volume = 100; // Volume for the player
        private Channel currentChannel;
        private int tracker = 0;
        private int prevVol = 0;
        private bool isMute = false;
        private bool isplaying = false;

        public string currentTitle { get { return currentChannel.Title; } }
        public string currentUrl { get { return currentChannel.Url; } }

        public rStream(List<Channel> channels, bool active = false, int vol = 100)
        {
            Channels = channels;
            activeRadio = active;
            volume = vol;
            if (channels.Count > 0)
                currentChannel = channels.ElementAt(0);
            else
                currentChannel = null;
        }
        /// The following methods are used for the functionality of the player
        /// Some of these methods were pasted from 
        /// "https://social.msdn.microsoft.com/Forums/vstudio/en-US/4e73cd2a-dc53-406b-9641-921d9578bb26/internet-radio-in-my-c-application?forum=csharpgeneral"
        ///  for faster refrence
        //---------------------------------------------------------------------------
        //                  Play from new url.
        //---------------------------------------------------------------------------
        public void PlayChannel(Channel channel)
        {
            currentChannel = channel;
            player.URL = currentChannel.Url;

            player.settings.volume = volume;
            if (activeRadio)
            {
                player.controls.play();
                isplaying = true;
            }
        }
        public void playCurrent()
        {
            player.URL = currentChannel.Url;

            player.settings.volume = volume;
            if (activeRadio)
            {
                player.controls.play();
                isplaying = true;
            }
        }
        //---------------------------------------------------------------------------
        //                  Stop the player.
        //---------------------------------------------------------------------------
        public void PlayerStop()
        {
            player.controls.stop();
        }
        //---------------------------------------------------------------------------
        //                  Refresh the player.
        //---------------------------------------------------------------------------
        public void PlayerRestart()
        {
            if(activeRadio)
                playCurrent();
            else
                player.controls.stop();
        }
        //---------------------------------------------------------------------------
        //                  Set the player volume.
        //---------------------------------------------------------------------------
        public void refVolume(int newVolume)
        {
            Volume = newVolume;
            player.settings.volume = volume;
        }
        //---------------------------------------------------------------------------
        //                  Move forward on the list.
        //---------------------------------------------------------------------------
        public void playForward()
        {
            ++tracker;
            if(tracker > Channels.Count - 1)
                tracker = 0;
            currentChannel = Channels.ElementAt(tracker);
            PlayChannel(currentChannel);
        }
        //---------------------------------------------------------------------------
        //                  Move backward on the list.
        //---------------------------------------------------------------------------
        public void playBackward()
        {
            --tracker;
            if (tracker < 0)
                tracker = Channels.Count - 1;
            currentChannel = Channels.ElementAt(tracker);
            PlayChannel(currentChannel);
        }
        //---------------------------------------------------------------------------
        //                  Select and play something from the list.
        //---------------------------------------------------------------------------
        public void playFromList(int elementNumber)
        {
            if (elementNumber > 0 && elementNumber <= Channels.Count - 1)
            {
                currentChannel = Channels.ElementAt(elementNumber);
                PlayChannel(currentChannel);
                tracker = elementNumber;
            }
        }
        //---------------------------------------------------------------------------
        //                  Select element from the list and play if player is on.
        //---------------------------------------------------------------------------
        public void selectFromList(int elementNumber)
        {
            if (elementNumber > 0 && elementNumber <= Channels.Count - 1)
            {
                currentChannel = Channels.ElementAt(elementNumber);
                if (isplaying)
                    playCurrent();
            }
        }
        //---------------------------------------------------------------------------
        //                  Mute and store previous volume.
        //---------------------------------------------------------------------------
        public void Mute()
        {
            if (!isMute)
            {
                prevVol = Volume;
                refVolume(0);
                isMute = true;
            }
        }
        //---------------------------------------------------------------------------
        //                  Unmute and restore volume.
        //---------------------------------------------------------------------------
        public void Unmute()
        {
            if (isMute)
            {
                refVolume(prevVol);
                isMute = false;
            }
        }

        //---------------------------------------------------------------------------
        //                  Get and Set for active boolean
        //---------------------------------------------------------------------------
        public bool Active { get { return activeRadio; } set { activeRadio = value; } }
        //---------------------------------------------------------------------------
        //                  Get and Set for volume
        //---------------------------------------------------------------------------
        public int Volume
        {
            get
            {
                return volume;
            }
            set
            {
                if (value <= 100 && value >= 0)
                    volume = value;
            }
        }
        //---------------------------------------------------------------------------
        //    Give others the option of adding a channel not in the list of channels
        //---------------------------------------------------------------------------
        public Channel streamChannel { get { return currentChannel; } set { currentChannel = value; } }
        //---------------------------------------------------------------------------
        //                  Get ismute boolean
        //---------------------------------------------------------------------------
        public bool isMuted { get { return isMute; } }
        //---------------------------------------------------------------------------
        //                  Get isplaying boolean
        //---------------------------------------------------------------------------
        public bool isPlaying { get { return isplaying; } }
        //---------------------------------------------------------------------------
        //                  Get and Set for Channel list
        //---------------------------------------------------------------------------
        public List<Channel> streamChannels
        {
            get
            {
                return Channels;
            }
            set
            {
                tracker = 0;
                Channels = value;
                selectFromList(0);
            }
        }
    }
}
