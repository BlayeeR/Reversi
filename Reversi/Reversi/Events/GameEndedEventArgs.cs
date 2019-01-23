using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Events
{
    public class GameEndedEventArgs :EventArgs
    {
        int result;
        private Score[] scores;
        public int Result { get { return result; } private set { result = value; } }
        public Score[] Scores { get { return scores; } private set { scores = value; } }
        public GameEndedEventArgs(int result, Score[] scores)
        {
            Result = result;
            Scores = scores;
        }

    }
}
