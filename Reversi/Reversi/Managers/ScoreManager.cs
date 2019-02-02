using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reversi.Managers
{
    public class ScoreManager
    {
        private static string filename = "scores.xml"; 
        public List<Score> Scores { get; private set; }
        public ScoreManager()
          : this(new List<Score>())
        {

        }
        public ScoreManager(List<Score> scores)
        {
            Scores = scores;
        }
        public void Add(Score score)
        {
            Scores.Add(score);
            Scores = Scores.OrderByDescending(c => c.Value).ToList(); // Orders the list so that the higher scores are first
        }
        public static ScoreManager Load()
        {
            if (!File.Exists(filename))
                return new ScoreManager();

            using (var reader = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));
                var scores = (List<Score>)serilizer.Deserialize(reader);
                return new ScoreManager(scores);
            }
        }

        public static void Save(ScoreManager scoreManager)
        {
            // Overrides the file if it alreadt exists
            using (var writer = new StreamWriter(new FileStream(filename, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));
                serilizer.Serialize(writer, scoreManager.Scores.Take(10).ToList());
            }
        }
    }
}
