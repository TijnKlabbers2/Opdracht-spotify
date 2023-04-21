using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify
{
    public class Friends
    {
        // lijst van alle vrienden die zijn opgeslagen
        public static List<Friends> friends { get; set; } = new List<Friends>();

        // afspeellijsten van deze vriend
        public List<Playlist> myPlaylist { get; private set; }

        // naam van deze vriend
        public string friendName { get; set; }

        // constructor voor het maken van nieuwe vrienden met afspeellijsten
        public Friends(string friendName, List<Playlist> FriendsPlaylists = null)
        {
            this.friendName = friendName;

            // als er geen afspeellijsten worden doorgegeven, wordt er een lege lijst gemaakt
            myPlaylist = FriendsPlaylists ?? new List<Playlist>();
        }

        public static List<Friends> AllFriends { get; set; } = new List<Friends>
        {
         new Friends("Rick", new List<Playlist>(Playlist.FriendsPlaylists)), // maakt een nieuwe vriend genaamd Rick en voegt de lijst met vrienden afspeellijsten toe
        new Friends("Justin", new List<Playlist>(Playlist.FriendsPlaylists)), // maakt een nieuwe vriend genaamd Justin en voegt de lijst met vrienden afspeellijsten toe
        new Friends("Liam", new List<Playlist>(Playlist.FriendsPlaylists)), // maakt een nieuwe vriend genaamd Liam en voegt de lijst met vrienden afspeellijsten toe
        new Friends("Stijn", new List<Playlist>(Playlist.FriendsPlaylists)) // maakt een nieuwe vriend genaamd Stijn en voegt de lijst met vrienden afspeellijsten toe
            };

        // functie om alle vrienden en hun afspeellijsten te bekijken
        public static void ViewFriends()
        {
            Console.WriteLine("Alle Vrienden:"); // geeft aan dat alle vrienden worden weergegeven
            foreach (Friends friend in AllFriends) // doorloopt de lijst met vrienden
            {
                Console.WriteLine(friend.friendName); // geeft de naam van de vriend weer
                ViewPlaylistOfFriends(friend); // roept de functie ViewPlaylistOfFriends aan om de afspeellijsten van de vriend weer te geven
            }
        }


        // functie om de afspeellijsten van een specifieke vriend te bekijken
        public static void ViewPlaylistOfFriends(Friends friend)
        {
            // Loop through each playlist of the friend
            foreach (Playlist playlist in friend.myPlaylist)
            {
                // Print the name of the playlist
                Console.WriteLine($"- {playlist.playlistName}");

                // Loop through each song in the friends songs list
                foreach (Song song in Song.FriendsSongs)
                {
                    // Print the name of the song
                    Console.WriteLine(song.songName);
                }
            }
        }


        // Vergelijkt de afspeellijsten van de gebruiker met die van een vriend
        public static void ComparePlaylist()
        {
            // Vraagt de gebruiker om de naam van zijn/haar afspeellijst in te voeren
            Console.WriteLine("Voer de naam van je afspeellijst in:");
            // Toont alle afspeellijsten van de gebruiker
            foreach (Playlist ownplaylist in Playlist.AllPlaylists)
            {
                Console.WriteLine(ownplaylist.playlistName);
            }

            // Leest de invoer van de gebruiker
            string myOwnPlaylist = Console.ReadLine();
            // Zoekt de afspeellijst van de gebruiker op basis van de ingevoerde naam
            Playlist OwnPlaylist = Playlist.AllPlaylists.Find(p => p.playlistName == myOwnPlaylist);

            // Als de afspeellijst niet wordt gevonden, wordt er een foutmelding weergegeven en keert het programma terug
            if (OwnPlaylist == null)
            {
                Console.WriteLine($"Afspeellijst {myOwnPlaylist} niet gevonden.");
                return;
            }

            // Vraagt de gebruiker om de naam van de afspeellijst van de vriend in te voeren
            Console.WriteLine("Voer de naam van de afspeellijst van je vriend in:");
            // Toont alle afspeellijsten van de vrienden
            foreach (Playlist friendplaylist in Playlist.FriendsPlaylists)
            {
                Console.WriteLine(friendplaylist.playlistName);
            }

            // Leest de invoer van de gebruiker
            string myfriendsPlaylist = Console.ReadLine();
            // Zoekt de afspeellijst van de vriend op basis van de ingevoerde naam
            Playlist FriendsPlaylist = Playlist.FriendsPlaylists.Find(p => p.playlistName == myfriendsPlaylist);

            // Als de afspeellijst niet wordt gevonden, wordt er een foutmelding weergegeven en keert het programma terug
            if (FriendsPlaylist == null)
            {
                Console.WriteLine($"Afspeellijst {myfriendsPlaylist} niet gevonden.");
                return;
            }

            // Maakt een lege lijst aan voor gemeenschappelijke nummers
            List<Song> commonSongs = new List<Song>();
            // Doorloopt alle nummers in de afspeellijst van de gebruiker en die van de vriend
            foreach (Song ownSong in OwnPlaylist.Songs)
            {
                foreach (Song friendSong in FriendsPlaylist.Songs)
                {
                    // Als het nummer overeenkomt, wordt het aan de lijst met gemeenschappelijke nummers toegevoegd
                    if (ownSong.songName == friendSong.songName)
                    {
                        commonSongs.Add(ownSong);
                        break;
                    }
                }
            }

          
            // De titels van de gedeelde nummers tussen de eigen afspeellijst en de vrienden afspeellijst worden geprint
            Console.WriteLine($"Je afspeellijst '{myOwnPlaylist}' en de afspeellijst van je vriend '{FriendsPlaylist.playlistName}' hebben {commonSongs.Count} nummers gemeen:");

            // De naam van elk gedeeld nummer wordt geprint, voorafgegaan door een streepje.
            foreach (Song song in commonSongs)
            {
                Console.WriteLine($"- {song.songName}");
            }

        }

    }
}
