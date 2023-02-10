using System;

namespace Web3MusicStore.API.Models
{
    public struct SongTime
    {
        public int Minute { get; set; }
        public int Second { get; set; }

        public SongTime(int minutes, int seconds)
        {

            Minute = minutes;
            Second = seconds;
        }

        public int TotalSeconds
        {
            get { return Minute * 60 + Second; }
        }
    }
}