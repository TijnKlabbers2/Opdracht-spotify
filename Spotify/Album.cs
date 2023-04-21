using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify
{
    public class Album
    {
        // Declaratie van de publieke eigenschap "albumName" van het type string
        public string albumName { get; set; }
        // Declaratie van de publieke eigenschap "albumArtist" van het type string
        public string albumArtist{ get; set; }
        // Declaratie van de publieke statische eigenschap "isPlaying" van het type bool, met een standaardwaarde van false
        public static bool isPlaying { get; set; } = false;

        // Declaratie van de publieke statische eigenschap "paused" van het type bool, met een standaardwaarde van false
        public static bool paused { get; set; } = false;

        // Declaratie van de publieke statische eigenschap "timeElapsed" van het type int, met een standaardwaarde van 0
        public static int timeElapsed { get; set; } = 0;

        // Declaratie van de publieke statische eigenschap "Albums" van het type List<Album>, die een lege lijst van het type Album bevat
        public static List<Album> Albums { get; set; } = new List<Album>();

        // Declaratie van de eigenschap "songs" van het type List<Song>, die een privé-setter heeft
        public List<Song> songs { get; private set; }




        public static List<Song> AllAlbumSongs { get; set; } = new List<Song>
        {
            // Lijst van alle nummers die op elk album beschikbaar zijn
            new Song("my boy", "elvis presley", 120),
            new Song("if i can dream", "elvis presley", 140),
            new Song("return to sender", "elvis presley", 160),
            new Song("my way", "elvis presley", 180),
            new Song("always on my mind", "elvis presley", 200)
        };

        public Album(string albumName, string albumArtistName, List<Song> AllAlbumSongs = null)
        {
            // Naam van het album instellen
            this.albumName = albumName;
            // Naam van de artiest die het album heeft gemaakt instellen
            this.albumArtist = albumArtistName;
            // Lijst van nummers op het album instellen, als er geen nummers zijn opgegeven wordt er een lege lijst aangemaakt
            songs = AllAlbumSongs ?? new List<Song>();
        }

        public static List<Album> AllAlbums { get; set; } = new List<Album>
        {
        // Lijst van alle albums
        new Album("elvis Presley", "elvis presley", new List<Song>(Album.AllAlbumSongs))
        };


        // Methode om alle albums te bekijkenw
        public static void ViewAlbums()
        {
            Console.WriteLine("All albums:"); // Print een bericht om aan te geven dat alle albums worden weergegeven
            foreach (Album albums in AllAlbums)
            {
                Console.WriteLine($"- {albums.albumName}"); // Weergeeft de naam van het album
            }
            PlayAlbum(); // Speelt het album af na het weergeven van alle albums
        }

        //Methode om albums af te spelen
        public static void PlayAlbum()
        {
            
            Console.WriteLine("Press Q to quit");// laat de gebruiker weten dat je q met indrukken om te quitten
            Console.Write("Choose an album: ");
            string selectedAlbumName = Console.ReadLine(); 
            Album selectedAlbum = Album.AllAlbums.FirstOrDefault(a => a.albumName == selectedAlbumName);

            if (selectedAlbumName.ToLower() == "q" || selectedAlbumName == null)
            {
                
                return;
            }
            else
            {
                
                if (Album.AllAlbums.Contains(selectedAlbum))
                {
                    Console.WriteLine("Choose 'shuffle' to play a random song, or choose a song you want to play.");
                    Console.WriteLine($"album: '{selectedAlbumName}':");
                    Console.WriteLine("Songs:");
                    foreach (Song songs in Album.AllAlbumSongs)
                    {
                        Console.WriteLine($"-- {songs.songName}");
                    }

                    Console.WriteLine("Choose a song:");
                    string songChoice = Console.ReadLine();
                    Song selectedSong;

                    if (songChoice.ToLower() == "shuffle")
                    {
                        Random rand = new Random();
                        selectedSong = selectedAlbum.songs[rand.Next(0, selectedAlbum.songs.Count)];
                    }
                    else
                    {
                        selectedSong = selectedAlbum.songs.FirstOrDefault(s => s.songName == songChoice);
                    }

             
                    isPlaying = true;
                    int songDuration = selectedSong.songDuration;
                    string songArtist = selectedSong.artistName;

                    if (selectedSong == null)
                    {
                        Console.WriteLine("album doesnt contain song");
                        return;
                    }
                        
                        Console.WriteLine($"Song selected: {selectedSong.songName}");
                        Console.WriteLine($"Artist: {songArtist}");
                        Console.WriteLine($"Duration: {songDuration}");

                        while (isPlaying)
                        {
                            if (!paused)
                            {
                                Console.SetCursorPosition(0, Console.CursorTop);
                                Console.Write($"playing... {selectedSong.songName} ({timeElapsed}/{songDuration})");

                                if (timeElapsed > songDuration)
                                {
                                    Console.WriteLine("\nSong finished!");
                                    isPlaying = false;
                                }
                            }

                            if (Console.KeyAvailable)
                            {
                                ConsoleKey key = Console.ReadKey(true).Key;

                                switch (key)
                                {
                                    case ConsoleKey.P:
                                        if (!paused)
                                        {
                                            paused = true;
                                            Console.WriteLine("\nPaused Song.");
                                        }
                                        break;
                                    case ConsoleKey.R:
                                        if (paused)
                                        {
                                            paused = false;
                                            Console.WriteLine($"Resuming song {selectedSong.songName}.");
                                        }
                                        break;
                                    case ConsoleKey.E:
                                        if (paused)
                                        {
                                            timeElapsed = 0;
                                            Console.WriteLine($"Repeating song {selectedSong.songName}");
                                            paused = false;
                                        }
                                        break;
                                    case ConsoleKey.S:
                                        isPlaying = false;
                                        Console.WriteLine("\nSong skipped.");
                                        
                                        
                                        break;
                                    case ConsoleKey.Q:
                                        isPlaying = false;
                                        Console.WriteLine("\nQuitting...");
                                        
                                        timeElapsed = 0;
                                        
                                        break;
                                }
                            }

                       
                            if (!paused)
                            {
                                timeElapsed++;
                            }
                        }
                    
                }
                else
                {
                    Console.WriteLine($"Album '{selectedAlbumName}' does not exist.");
                }
            }
        }

        public static void AddAlbumToPlaylist()
        {
            Console.WriteLine("All albums:");
            foreach (Album album in AllAlbums)
            {
                Console.WriteLine($"- {album.albumName}");
            }

            Console.WriteLine("Which album do you want to add to a playlist:");
            string choiceAlbumName = Console.ReadLine();
            Album selectedAlbum = Album.AllAlbums.FirstOrDefault(a => a.albumName == choiceAlbumName);

            if (choiceAlbumName.ToLower() == "q" || choiceAlbumName == null || selectedAlbum == null)
            {
                Console.WriteLine($"Album {choiceAlbumName} does not exist.");
                
                
                return;
            }
            else
            {
                Console.WriteLine("All playlists:");
                foreach (Playlist playlist in Playlist.AllPlaylists)
                {
                    Console.WriteLine($"- {playlist.playlistName}");
                }

                Console.WriteLine("Enter the name of the playlist to add the album songs to:");
                string choicePlaylistName = Console.ReadLine();
                Playlist chosenPlaylist = Playlist.AllPlaylists.Find(p => p.playlistName == choicePlaylistName);

                if (chosenPlaylist != null)
                {
                    foreach (Song song in selectedAlbum.songs)
                    {
                        chosenPlaylist.Songs.Add(song);
                    }
                
                    Console.WriteLine($"Added {selectedAlbum.songs.Count} songs from the '{selectedAlbum.albumName}' album to the '{chosenPlaylist.playlistName}' playlist.");
                }
                else
                {
                    Console.WriteLine($"Playlist '{choicePlaylistName}' not found.");
                    return;
                    
                }
            }
        }       
    }
}
