using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify
{
    public class Song
    {
        // Properties voor het opslaan van de naam, artiest en duur van een liedje
        public string songName { get; set; }
        public string artistName { get; set; }
        public int songDuration { get; set; }

        // Constructor die wordt aangeroepen om een nieuw liedje object te maken
        public Song(string songName, string artistName, int songDuration)
        {
            this.songName = songName;
            this.artistName = artistName;
            this.songDuration = songDuration;
        }

        // Statische lijst van liedjes, waar ik twee liedjes heb toegevoegd
        public static List<Song> FriendsSongs { get; set; } = new List<Song>
        {
            new Song("eenzame kerst", "andre hazes", 210), // Dit liedje heet "eenzame kerst", is gemaakt door "Andre hazes" en duurt 210 seconden
            new Song("Whatever you want", "Status Quo", 180), // Dit liedje heet "Whatever you want" is gemaakt door "Status Quo" en duurt 180 seconden
        };
    }
}
