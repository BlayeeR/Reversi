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
        public bool Side { internal set; get; }
        public Tile DestinationTile { internal set; get; }
        public List<Tile> TakenTiles { internal set; get; }
        public Tile SourceTile { internal set; get; }
        public Movement(bool side,Tile sourceTile, Tile destinationTile, List<Tile> takenTiles)
        {
            Side = side;
            DestinationTile = destinationTile;
            TakenTiles = takenTiles;
            SourceTile = sourceTile;
            destinationTile.DrawingColor = Color.Gray;
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
