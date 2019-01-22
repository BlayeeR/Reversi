using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reversi
{
    public class ScoreManager
    { 
        private static string _fileName = "scores.xml";
        public List<Score> _scores;
        public List<Score> Scores { get { return _scores.Take(10).ToList(); } private set { _scores = value; } }
        public ScoreManager() : this(new List<Score>())
        { }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;
        }

        public void Add(Score score)
        {
            Scores.Add(score);
            Scores = Scores.OrderByDescending(c => c.Value).ToList();
            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));
                var scores = (List<Score>)serilizer.Deserialize(reader);
                Scores = scores;
            }
        }

        public void Save()
        {
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                serilizer.Serialize(writer, Scores);
            }
        }
    }
}
