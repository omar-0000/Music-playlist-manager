using System;
using System.Collections.Generic;

namespace MusicPlaylist
{
    public class Song
    {
        public string Artist;
        public string Title;
        public string Album;
        public int DurationInSeconds;
        public string Genre;
        public Song Next;

        public Song(string artist, string title, string album, int durationInSeconds, string genre)
        {
            Artist = artist;
            Title = title;
            Album = album;
            DurationInSeconds = durationInSeconds;
            Genre = genre;
            Next = null;
        }

        public override string ToString()
        {
            return $"{Title} by {Artist} | Album: {Album} | Duration: {DurationInSeconds}s | Genre: {Genre}";
        }
    }

    public class Playlist
    {
        public Song head;
        private Song current;
        private bool loopEnabled = false;


        public Playlist()
        {
            head = null;
            current = null;
        }

        public void AddSong(Song newSong)
        {
            if (head == null)
            {
                head = newSong;
                return;
            }
            Song temp = head;
            while (temp.Next != null)
                temp = temp.Next;
            temp.Next = newSong;
        }

        public void PrintAllSongs()
        {
            Song temp = head;
            while (temp != null)
            {
                Console.WriteLine(temp.ToString());
                temp = temp.Next;
            }
        }

        public void DeleteSongByTitle(string title)
        {
            if (head == null)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }
            if (head.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                head = head.Next;
                Console.WriteLine($"Deleted song: {title}");
                return;
            }
            Song temp = head;
            while (temp.Next != null && !temp.Next.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                temp = temp.Next;
            }
            if (temp.Next == null)
            {
                Console.WriteLine($"Song with title '{title}' not found.");
            }
            else
            {
                temp.Next = temp.Next.Next;
                Console.WriteLine($"Deleted song: {title}");
            }
        }

        public int SearchByTitle(string title)
        {
            Song temp = head;
            int index = 0;

            while (temp != null)
            {
                if (temp.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    return index;
                }
                temp = temp.Next;
                index++;
            }

            return -1;
        }

        public void AddSongManually()
        {
            Console.Write("Enter song title: ");
            string title = Console.ReadLine();
            Console.Write("Enter artist name: ");
            string artist = Console.ReadLine();
            Console.Write("Enter album name: ");
            string album = Console.ReadLine();
            Console.Write("Enter duration in seconds: ");
            int durationInSeconds = int.Parse(Console.ReadLine());
            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();

            Song newSong = new Song(artist, title, album, durationInSeconds, genre);
            AddSong(newSong);

            Console.WriteLine($"Added song: {newSong}");
        }

        public void DeleteSongByPosition(int position)
        {
            if (head == null)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }
            if (position == 0)
            {
                Console.WriteLine($"Deleted song: {head.Title}");
                head = head.Next;
                return;
            }
            if (position < 0)
            {
                Console.WriteLine("Invalid position.");
                return;
            }

            Song temp = head;
            int index = 0;

            while (temp != null && index < position - 1)
            {
                temp = temp.Next;
                index++;
            }

            if (temp == null || temp.Next == null)
            {
                Console.WriteLine("Position out of bounds.");
            }
            else
            {
                Console.WriteLine($"Deleted song: {temp.Next.Title}");
                temp.Next = temp.Next.Next;
            }
        }

        public void Shuffle()
        {
            List<Song> songs = new List<Song>();
            Song temp = head;

            while (temp != null)
            {
                songs.Add(temp);
                temp = temp.Next;
            }

            if (songs.Count == 0)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }

            Random rnd = new Random();
            for (int i = songs.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                (songs[i], songs[j]) = (songs[j], songs[i]);
            }

            head = songs[0];
            temp = head;

            for (int i = 1; i < songs.Count; i++)
            {
                temp.Next = songs[i];
                temp = temp.Next;
            }

            temp.Next = null;
            Console.WriteLine("Playlist shuffled.");
        }

        public void SortByArtist()
        {
            List<Song> songs = new List<Song>();
            Song temp = head;

            while (temp != null)
            {
                songs.Add(temp);
                temp = temp.Next;
            }

            if (songs.Count == 0)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }

            songs.Sort((a, b) => a.Artist.CompareTo(b.Artist));

            head = songs[0];
            temp = head;

            for (int i = 1; i < songs.Count; i++)
            {
                temp.Next = songs[i];
                temp = temp.Next;
            }

            temp.Next = null;
            Console.WriteLine("Playlist sorted by artist.");
        }

        public void SortByDuration()
        {
            List<Song> songs = new List<Song>();
            Song temp = head;

            while (temp != null)
            {
                songs.Add(temp);
                temp = temp.Next;
            }

            if (songs.Count == 0)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }

            songs.Sort((a, b) => a.DurationInSeconds.CompareTo(b.DurationInSeconds));

            head = songs[0];
            temp = head;

            for (int i = 1; i < songs.Count; i++)
            {
                temp.Next = songs[i];
                temp = temp.Next;
            }

            temp.Next = null;
            Console.WriteLine("Playlist sorted by duration.");
        }

        public void PlayNext()
        {
            if (head == null)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }

            if (current == null)
                current = head;
            else if (!loopEnabled)
                current = current.Next;

            if (current != null)
                Console.WriteLine($"Now playing: {current}");
            else
                Console.WriteLine("End of playlist reached.");
        }

        public void PlayPrevious()
        {
            if (head == null)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }

            if (current == null)
            {
                current = head;
                Console.WriteLine($"Now playing: {current}");
                return;
            }

            if (current == head)
            {
                Console.WriteLine("Already at the beginning of the playlist.");
                return;
            }

            Song temp = head;
            while (temp.Next != current)
            {
                temp = temp.Next;
            }

            current = temp;
            Console.WriteLine($"Now playing: {current}");
        }
        public void ToggleLoop()
        {
            loopEnabled = !loopEnabled;
            Console.WriteLine($"Looping is now {(loopEnabled ? "enabled" : "disabled")}.");
        }

        public void DisplayStatistics()
        {
            if (head == null)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }

            int count = 0;
            int totalDuration = 0;
            Song temp = head;

            while (temp != null)
            {
                count++;
                totalDuration += temp.DurationInSeconds;
                temp = temp.Next;
            }

            double average = (double)totalDuration / count;

            Console.WriteLine($"Total songs: {count}");
            Console.WriteLine($"Total duration: {totalDuration} seconds");
            Console.WriteLine($"Average duration: {average:F2} seconds");
        }






    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Music Playlist Manager starting...");

            Playlist playlist = new Playlist();

            ReadCSV.LoadSongs("songs_dataset.csv", playlist);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Music Playlist Menu ---");
                Console.WriteLine("1. Play next song");
                Console.WriteLine("2. Play previous song");
                Console.WriteLine("3. Display all songs");
                Console.WriteLine("4. Search for a song");
                Console.WriteLine("5. Sort by artist");
                Console.WriteLine("6. Sort by duration");
                Console.WriteLine("7. Shuffle playlist");
                Console.WriteLine("8. Add a new song");
                Console.WriteLine("9. Delete by title");
                Console.WriteLine("10. Delete by position");
                Console.WriteLine("11. Exit");
                Console.WriteLine("12. Toggle loop current song");
                Console.WriteLine("13. Display playlist statistics");


                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": playlist.PlayNext(); break;
                    case "2": playlist.PlayPrevious(); break;
                    case "3": playlist.PrintAllSongs(); break;

                    case "4":
                        Console.Write("Enter song title: ");
                        string s = Console.ReadLine();
                        int pos = playlist.SearchByTitle(s);
                        Console.WriteLine(pos == -1 ? "Not found" : $"Found at position {pos}");
                        break;

                    case "5": playlist.SortByArtist(); break;
                    case "6": playlist.SortByDuration(); break;
                    case "7": playlist.Shuffle(); break;
                    case "8": playlist.AddSongManually(); break;

                    case "9":
                        Console.Write("Enter title: ");
                        playlist.DeleteSongByTitle(Console.ReadLine());
                        break;

                    case "10":
                        Console.Write("Enter position: ");
                        playlist.DeleteSongByPosition(int.Parse(Console.ReadLine()));
                        break;

                    case "11":
                        exit = true;
                        break;

                    case "12":
                        playlist.ToggleLoop();
                        break;

                    case "13":
                        playlist.DisplayStatistics();
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }


                if (!exit)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }
    }
}
