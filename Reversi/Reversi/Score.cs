using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Score
    {
        public Score(string playerName, int value)
        {
            PlayerName = playerName;
            Value = value;
        }
        public string PlayerName { get; set; }
        public int Value { get; set; }
    }
}
