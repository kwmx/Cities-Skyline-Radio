using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radio
{
    /// <summary>
    /// Simple class to store the url and title of a radio stream channel
    /// </summary>
    public class Channel
    {
        private string _title;
        private string _url;
        /// <summary>
        /// Class constructor takes in the title of the stream and the url
        /// </summary>
        public Channel(string title, string url)
        {
            this._title = title;
            this._url = url;
        }
        public string Title { get { return _title; } }
        public string Url { get { return _url; } }
    }
}
