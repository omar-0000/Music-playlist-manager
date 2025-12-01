using System;
namespace MusicPlaylist
{
    public class Song
    {
        public string Artist;
        public string Title;
        public string Album;
        public int DurationInSeconds;
        public string Genre;
        public Song Next; // pointer for linked list

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
        public Playlist()
        {
            head = null;
            current = null;
        }
        // add a song which will be from the csv
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
        // printsall songs 
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
                    return index;  // found
                }

                temp = temp.Next;
                index++;
            }

            return -1; // not found
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
            Console.WriteLine($"Added song: {newSong.ToString()}");
        }

        public void DeleteSongByPosition(int position)
        {
            if head == null)
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
    }
    class Program

    {
        static void Main(string[] args)
        {
            Console.WriteLine("Music Playlist Manager starting...");

            Playlist playlist = new Playlist();

            // loads songs from CSV
            ReadCSV.LoadSongs("songs_dataset.csv", playlist);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Music Playlist Menu ---");
                Console.WriteLine("1. Display all songs");
                Console.WriteLine("2. Add a new song");
                Console.WriteLine("3. Delete a song by title");
                Console.WriteLine("4. Delete a song by position");
                Console.WriteLine("5. Display songs sorted by artist");
                Console.WriteLine("6. Display songs sorted by duration");
                Console.WriteLine("7. Search for a song by title");
                Console.WriteLine("8. Shuffle playlist");
                Console.WriteLine("9. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        playlist.PrintAllSongs();
                        break;

                    case "2":
                        // in here will go add song feature
                        playlist.AddSongManually();
                        break;

                    case "3":
                        Console.Write("Enter song title to delete: ");
                        string delTitle = Console.ReadLine();
                        playlist.DeleteSongByTitle(delTitle);
                        break;

                    case "4":
                        Console.Write("Enter song position to delete: ");
                        int delPos = int.Parse(Console.ReadLine());
                        playlist.DeleteSongByPosition(delPos);
                        break;

                    case "5":
                        Console.WriteLine("Display sorted by artist feature will go here.");
                        break;

                    case "6":
                        Console.WriteLine("Display sorted by duration feature will go here.");
                        break;

                    case "7":
                        Console.Write("Enter song title to search: ");
                        string searchTitle = Console.ReadLine();
                        int pos = playlist.SearchByTitle(searchTitle);

                        if (pos == -1)
                            Console.WriteLine("Song not found.");
                        else
                            Console.WriteLine($"Song found at position {pos}.");
                        break;

                    case "8":
                        Console.WriteLine("Shuffle feature will go here.");
                        break;

                    case "9":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }

            Console.WriteLine("Exiting Music Playlist Manager.");
        }
    }

}
