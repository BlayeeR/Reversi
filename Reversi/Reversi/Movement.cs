using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Movement
    {
        public bool Side { private set; get; }
        public Tile DestinationTile { private set; get; }
        public List<Tile> TakenTiles { private set; get; }
        public Tile SourceTile { private set; get; }
        public Movement(bool side,Tile sourceTile, Tile destinationTile, List<Tile> takenTiles, bool isPlayer=true)
        {
            Side = side;
            DestinationTile = destinationTile;
            TakenTiles = takenTiles;
            SourceTile = sourceTile;
            if(isPlayer)
                destinationTile.DrawingColor = Color.DarkGray;
        }
        public int Score()
        {
            return TakenTiles.Count;
        }
        public void Perform()
        {
            foreach (Tile tile in TakenTiles)
                tile.ChangeSide();
            DestinationTile.Side = Side;
            DestinationTile.Visible = true;
        }
    }
}
