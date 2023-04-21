using System;
using System.Collections.Generic;
using NAudio.Wave;
using System.Timers;
using System.Media;
using Spotify;

namespace Spotiy
{
    class Program
    {
        static void Main(string[] args)
        {
            // Oneindige loop om de gebruiker te laten kiezen uit verschillende opties
            while (true)
            {
                Console.WriteLine("<------------- Kies een optie ------------->");
                Console.WriteLine("==============================================");
                Console.WriteLine("1. Bekijk afspeellijsten");
                Console.WriteLine("2. Maak afspeellijsten");
                Console.WriteLine("3. Verwijder afspeellijsten");
                Console.WriteLine("4. Voeg nummer toe");
                Console.WriteLine("5. Verwijder nummer");
                Console.WriteLine("6. Bekijk albums");
                Console.WriteLine("7. Voeg album toe aan afspeellijst");
                Console.WriteLine("8. Bekijk vrienden");
                Console.WriteLine("9. Vergelijk afspeellijst met vriend");
                Console.WriteLine("==============================================");
                Console.Write("Kies een optie: ");
                string choice = Console.ReadLine();

                // Schakel tussen verschillende opties op basis van de gebruikerskeuze
                switch (choice)
                {
                    case "1":
                        Playlist.ViewPlaylists(); // Bekijk de afspeellijsten
                        break;

                    case "2":
                        Playlist.CreatePlaylist(); // Maak een afspeellijst
                        break;

                    case "3":
                        Playlist.DeletePlaylist(); // Verwijder een afspeellijst
                        break;

                    case "4":
                        Playlist.AddSong(); // Voeg een nummer toe aan een afspeellijst
                        break;

                    case "5":
                        Playlist.RemoveSong(); // Verwijder een nummer uit een afspeellijst
                        break;

                    case "6":
                        Album.ViewAlbums(); // Bekijk de albums
                        break;

                    case "7":
                        Album.AddAlbumToPlaylist(); // Voeg een album toe aan een afspeellijst
                        break;

                    case "8":
                        Friends.ViewFriends(); // Bekijk vrienden
                        break;

                    case "9":
                        Friends.ComparePlaylist(); // Vergelijk afspeellijst met een vriend
                        break;

                    default:
                        Console.WriteLine("Ongeldige optie."); // Geef een foutmelding voor een ongeldige keuze
                        break;
                }
            }
            Console.Read(); // Wacht op gebruikersinvoer
        }
    }
}
