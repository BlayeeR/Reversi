using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class TileManager
    {
        List<List<Tile>> tiles;
        private Vector2 _gameBoardPosition, _gameBoardDimensions;
        private int boardSize = 8;
        public TileManager(Vector2 gameBoardPosition, Vector2 gameBoardDimensions)
        {
            _gameBoardPosition = gameBoardPosition;
            _gameBoardDimensions = gameBoardDimensions;
            tiles = new List<List<Tile>>();
            Vector2 tileSize = new Vector2(_gameBoardDimensions.X / boardSize * 0.96f);
            for(int i =0; i < boardSize; i++)
            {
                List<Tile> tempTiles = new List<Tile>();
                for (int j = 0; j < boardSize; j++)
                {
                    Tile tile = new Tile(true, false, new Vector2(_gameBoardPosition.X + 0.02f * _gameBoardDimensions.X + j * tileSize.X + tileSize.X / 2, _gameBoardPosition.Y + 0.02f * _gameBoardDimensions.Y + i * tileSize.Y + tileSize.Y / 2), tileSize);
                    //Tile tile = new Tile(_gameBoardPosition, tileSize);
                    tempTiles.Add(tile);
                    
                }
                tiles.Add(tempTiles);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (List<Tile> tilei in tiles)
                foreach (Tile tile in tilei)
                    tile.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            foreach (List<Tile> tilei in tiles)
                foreach (Tile tile in tilei)
                    tile.Update(gameTime);
        }
    }
}
