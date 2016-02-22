using System;
using System.Collections.Generic;
using MetroFramework.Forms;
using Radio;
namespace Radio_Exporter
{
    public partial class Player : MetroForm
    {
        int prevVol = 50;
        rStream radioStream;  
        public Player()
        {
            InitializeComponent();
        }
        private void Player_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if(txtURL.Text != null || txtURL.Text != "" || txtURL.Text != " " || txtURL.Text != string.Empty)
            {
                string url = txtURL.Text.Replace("\"", "");
                Channel temp = new Channel("UNKNOWN", url);
                radioStream = new rStream(new List<Channel>(), true, prevVol);
                radioStream.PlayChannel(temp);
                metroButton2.Enabled = true;
                metroButton3.Enabled = true;
                metroButton4.Enabled = true;
                metroLabel1.Text = "Volume: " + radioStream.Volume;
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            prevVol = radioStream.Volume;
            radioStream.PlayerStop();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            radioStream.refVolume(++radioStream.Volume);
            metroLabel1.Text = "Volume: " + radioStream.Volume;
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            radioStream.refVolume(--radioStream.Volume);
            metroLabel1.Text = "Volume: " + radioStream.Volume;
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            if (radioStream.isMuted)
            {
                radioStream.Unmute();
                metroLabel1.Text = "Volume: " + radioStream.Volume;
                metroButton5.Text = "Mute";
            }
            else
            {
                radioStream.Mute();
                metroLabel1.Text = "MUTED";
                metroButton5.Text = "Unmute";
            }
        }
    }
}
