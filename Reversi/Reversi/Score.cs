using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reversi
{
    public class Score
    {
        [XmlElement("PlayerName")]
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
