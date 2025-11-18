using System;
using System.IO;

namespace MusicPlaylist
{
    public class ReadCSV
    {
        public static void LoadSongs(string filename, Playlist playlist)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("CSV file not found: " + filename);
                return;
            }
            StreamReader sr = new StreamReader(File.OpenRead(filename));
            string line = null;
            int row = 0, col = 0;

            while (!sr.EndOfStream)
            {
                row++;
                line = sr.ReadLine();
                var val = line.Split(',');
                col = val.Length;
            }
            sr.Close();

            sr = new StreamReader(File.OpenRead(filename));
            int i = 0;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                var val = line.Split(',');

                if (i > 0) // skip header
                {
                    // the colums for the csv such as Title, Artist, Album, Duration, Genre
                    string title = val[1].Trim();
                    string artist = val[2].Trim();
                    string album = val[3].Trim();
                    string durationStr = val[4].Trim();
                    string genre = val[5].Trim();

                    // convert MM:SS to seconds
                    int durationSeconds = 0;
                    var timeParts = durationStr.Split(':');
                    if (timeParts.Length == 2)
                    {
                        durationSeconds = int.Parse(timeParts[0]) * 60 + int.Parse(timeParts[1]);
                    }

                    // create Song node and add to playlist
                    Song newSong = new Song(artist, title, album, durationSeconds, genre);
                    playlist.AddSong(newSong);
                }
                i++;
            }

            sr.Close();
        }
    }
}
