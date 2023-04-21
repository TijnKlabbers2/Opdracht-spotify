using Spotiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify
{
    public class Playlist
    {
        public List<Song> Songs { get; set; }    // Eigenschap die een lijst van nummers in een afspeellijst bevat.
        public string playlistName { get; set; }  // Eigenschap die de naam van een afspeellijst bevat.
        public static bool isPlaying { get; set; } = false;  // Eigenschap die aangeeft of de afspeellijst momenteel wordt afgespeeld.
        public static bool paused { get; set; } = false;     // Eigenschap die aangeeft of de afspeellijst momenteel gepauzeerd is.
        public static int timeElapsed { get; set; } = 0;     // Eigenschap die de tijd bijhoudt die is verstreken tijdens het afspelen van de afspeellijst.

        public static List<Song> AllSongs { get; set; } = new List<Song>  // Lijst met alle nummers in de applicatie.
        {
            new Song("Eenzame kerst", "Andre hazes", 120),
            new Song("Always look on the bright side of live ", "Monthy Python", 180),
            new Song("Olivia", "Antoon", 170)
        };

        public Playlist(string playlistName, List<Song> songs = null)  // Constructor om een nieuwe afspeellijst aan te maken.
        {
            this.playlistName = playlistName;
            Songs = songs ?? new List<Song>();
        }

        public static List<Playlist> AllPlaylists { get; set; } = new List<Playlist>  // Lijst met alle afspeellijsten in de applicatie.
        {
        new Playlist("tijn", new List<Song>(Playlist.AllSongs)),
        new Playlist("tweedeplaylist", new List<Song>(Playlist.AllSongs.Take(2)))
        };

        public static List<Playlist> FriendsPlaylists { get; set; } = new List<Playlist>  // Lijst met afspeellijsten van vrienden.
        {
            new Playlist("my fav playlist", new List<Song>(Song.FriendsSongs)),
        };

        // Functie om alle afspeellijsten in de applicatie te bekijken.
        public static void ViewPlaylists()
        {
            // Print message indicating all playlists are about to be displayed
            Console.WriteLine("All playlists:");

            // Loop through all playlists and print their names
            foreach (Playlist playlist in AllPlaylists)
            {
                Console.WriteLine($"- {playlist.playlistName}");
            }

            // Call PlayPlaylist method
            PlayPlaylist();
        }



        public static void PlayPlaylist()
        {
            Console.WriteLine("To Exit Press Key: Q");
            Console.Write("Choose a playlist: ");
            string selectedPlaylistName = Console.ReadLine();
            Playlist selectedPlaylist = AllPlaylists.FirstOrDefault(p => p.playlistName == selectedPlaylistName);

            if (selectedPlaylistName.ToLower() == "q" || selectedPlaylistName == null)
            {

                return;
            }
            else
            {

                if (AllPlaylists.Contains(selectedPlaylist))
                {
                    Console.WriteLine("Choose 'shuffle' to play a random song, or choose a song u want to play.");
                    Console.WriteLine($"playlist: '{selectedPlaylistName}':");
                    Console.WriteLine("Songs:");
                    foreach (Song song in selectedPlaylist.Songs)
                    {
                        Console.WriteLine($"-- {song.songName}");
                    }
                    Console.WriteLine("Choose a song:");
                    string songChoice = Console.ReadLine();
                    Song selectedSong;

                    if (songChoice.ToLower() == "shuffle")
                    {
                        Random rand = new Random();
                        selectedSong = Playlist.AllSongs[rand.Next(0, Playlist.AllSongs.Count)];
                    }
                    else
                    {
                        selectedSong = Playlist.AllSongs.FirstOrDefault(s => s.songName == songChoice);
                    }
                    if (selectedSong == null)
                    {
                        Console.WriteLine("song not found.");
                        Console.Write("Enter a song name:");
                        songChoice = Console.ReadLine();
                        if (songChoice.ToLower() == "shuffle")
                        {
                            Random rand = new Random();
                            selectedSong = Playlist.AllSongs[rand.Next(0, Playlist.AllSongs.Count)];
                        }
                        else
                        {
                            selectedSong = Playlist.AllSongs.FirstOrDefault(s => s.songName == songChoice);
                        }
                    }
                    isPlaying = true;
                    int songDuration = selectedSong.songDuration;
                    string songArtist = selectedSong.artistName;
                    int currentIndex = Playlist.AllSongs.IndexOf(selectedSong);
                    if (Playlist.AllSongs.Contains(selectedSong))
                    {

                        Console.WriteLine($"Song selected: {selectedSong.songName}");
                        Console.WriteLine($"Artist: {songArtist}");
                        Console.WriteLine($"duration: {songDuration}");

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
                                        timeElapsed = 0;
                                        Console.WriteLine($"Repeating song {selectedSong.songName}");
                                        paused = false;
                                        break;
                                    case ConsoleKey.S:
                                        isPlaying = false;
                                        currentIndex++;
                                        if (currentIndex >= Playlist.AllSongs.Count)
                                        {
                                            currentIndex = 0;
                                        }
                                        selectedSong = Playlist.AllSongs[currentIndex];
                                        isPlaying = true;
                                        paused = false;
                                        timeElapsed = 0;
                                        Console.WriteLine("\nSkipping to next song...");

                                        break;
                                    case ConsoleKey.Q:
                                        isPlaying = false;
                                        Console.WriteLine("\nQuitting...");

                                        break;
                                }
                            }

                            if (!paused)
                            {
                                timeElapsed++;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Playlist '{selectedPlaylistName}' does not exist.");

                }
            }
        }

        public static void CreatePlaylist()
        {
            Console.WriteLine("To Exit Press Key: Q");
            Console.WriteLine("Create a new playlist.");
            Console.Write("Enter the name of the playlist: ");
            string newPlaylistName = Console.ReadLine();

            if (newPlaylistName.ToLower() == "q" || newPlaylistName == null)
            {

                return;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(newPlaylistName))
                {
                    if (!AllPlaylists.Any(p => p.playlistName == newPlaylistName))
                    {
                        Playlist newPlaylist = new Playlist(newPlaylistName);
                        AllPlaylists.Add(newPlaylist);
                        Console.WriteLine($"Playlist '{newPlaylistName}' has been created.");

                        Console.WriteLine("all playlists:");
                        foreach (Playlist playlist in AllPlaylists)
                        {
                            Console.WriteLine(playlist.playlistName);
                        }


                    }
                    else
                    {
                        Console.WriteLine($"Playlist '{newPlaylistName}' already exists.");
                    }
                }
                else
                {
                    Console.WriteLine("Error, please provide a name for the playlist.");
                }
            }
        }

        public static void DeletePlaylist()
        {
            Console.WriteLine("All playlists:");
            foreach (Playlist playlist in AllPlaylists)
            {
                Console.WriteLine(playlist.playlistName);
            }
            Console.WriteLine("To Exit Press Key: Q");
            Console.Write("\nChoose a playlist you want to delete: ");
            string deletePlaylistName = Console.ReadLine();

            if (deletePlaylistName.ToLower() == "q" || deletePlaylistName == null)
            {

                return;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(deletePlaylistName))
                {
                    Playlist playlistToDelete = AllPlaylists.FirstOrDefault(p => p.playlistName == deletePlaylistName);
                    if (playlistToDelete != null)
                    {
                        AllPlaylists.Remove(playlistToDelete);
                        Console.WriteLine($"Playlist '{deletePlaylistName}' has been deleted.");

                        Console.WriteLine("all playlists:");
                        foreach (Playlist playlist in AllPlaylists)
                        {
                            Console.WriteLine(playlist.playlistName);
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Playlist '{deletePlaylistName}' does not exist.");
                    }
                }
                else
                {
                    Console.WriteLine("Error. Please enter a playlist name.");
                }
            }
        }
        public static void ViewSongs()
        {
            Console.WriteLine("\nAll songs: ");
            foreach (Playlist playlist in Playlist.AllPlaylists)
            {
                Console.WriteLine($"Playlist: {playlist.playlistName}");
                foreach (Song songs in Playlist.AllSongs)
                {
                    Console.WriteLine($"- {songs.songName} by {songs.artistName} ({songs.songDuration} seconds)");
                }
            }
        }

        public static void AddSong()
        {
            Console.WriteLine("To which playlist do you want to add a song:");
            foreach (Playlist playlist in Playlist.AllPlaylists)
            {
                Console.WriteLine(playlist.playlistName);
            }

            Console.Write("Enter the name of the playlist you want to add the song to: ");
            string playlistName = Console.ReadLine();
            Playlist addSongToPlaylist = Playlist.AllPlaylists.Find(p => p.playlistName == playlistName);

            if (addSongToPlaylist != null)
            {
                Console.Write("Enter the song name: ");
                string songName = Console.ReadLine();

                Console.Write("Enter the artist name: ");
                string artistName = Console.ReadLine();

                Console.Write("Enter the song duration (in seconds): ");
                int songDuration = int.Parse(Console.ReadLine());
                addSongToPlaylist.Songs.Add(new Song(songName, artistName, songDuration));
                Console.WriteLine($"Added {songName} by {artistName} ({songDuration} seconds) to {playlistName}");
                foreach (Playlist playlist in Playlist.AllPlaylists)
                {
                    Console.WriteLine($"Playlist: {playlist.playlistName}");

                    foreach (Song song in playlist.Songs)
                    {
                        Console.WriteLine(song.songName);
                    }
                }


            }
            else
            {
                Console.WriteLine($"Error: playlist '{playlistName}' not found");
            }
        }

        public static void RemoveSong()
        {
            Console.WriteLine("From which playlist do you want to remove a song:");
            foreach (Playlist playlist in Playlist.AllPlaylists)
            {
                Console.WriteLine(playlist.playlistName);
            }

            Console.Write("Enter the name of the playlist you want to remove the song from: ");
            string playlistName = Console.ReadLine();
            Playlist removeSongFromPlaylist = Playlist.AllPlaylists.Find(p => p.playlistName == playlistName);

            if (removeSongFromPlaylist != null)
            {
                Console.Write("Enter the name of the song you want to remove: ");
                foreach (Song song in AllSongs)
                {
                    Console.WriteLine(song.songName);
                }
                string songName = Console.ReadLine();

                Song removeSong = Playlist.AllSongs.Find(s => s.songName == songName);

                if (removeSong != null)
                {
                    if (removeSongFromPlaylist.Songs.Remove(removeSong))
                    {
                        Console.WriteLine($"Removed {songName} by {removeSong.artistName} from {playlistName}");

                        Playlist.ViewPlaylists();

                    }
                    else
                    {
                        Console.WriteLine($"Error: {songName} not found in {playlistName}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: song '{songName}' not found");
                }
            }
            else
            {
                Console.WriteLine($"Error: playlist '{playlistName}' not found");
            }
        }
    }
}
