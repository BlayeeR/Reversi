using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Events
{
    public class GameEndedEventArgs :EventArgs
    {
        int _result;
        private Score[] _scores;
        public int Result { get { return _result; } private set { _result = value; } }
        public Score[] Scores { get { return _scores; } private set { _scores = value; } }
        public GameEndedEventArgs(int result, Score[] scores)
        {
            Result = result;
            Scores = scores;
        }

    }
}
