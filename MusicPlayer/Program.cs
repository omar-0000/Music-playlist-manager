using System;

namespace MusicPlaylist
{
    // Song class (only one definition in the project)
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

    // Playlist class (singly linked list)
    public class Playlist
    {
        public Song head;
        private Song current; // for playback simulation

        public Playlist()
        {
            head = null;
            current = null;
        }

        // Add a song to the end
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

        // Print all songs
        public void PrintAllSongs()
        {
            Song temp = head;
            while (temp != null)
            {
                Console.WriteLine(temp.ToString());
                temp = temp.Next;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Playlist playlist = new Playlist();

            //load songs from the csv file 
            ReadCSV.LoadSongs("songs_dataset.csv", playlist);

            Console.WriteLine("\nCurrent Playlist:");
            playlist.PrintAllSongs();
        }
    }
}
