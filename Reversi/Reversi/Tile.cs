using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Sprites;

namespace Reversi
{
    public class Tile 
    {
        public Vector2 Position { private set; get; }
        public bool Side { set { _side = value;
                if (_side)
                    diskSprite.DrawingColor = Color.White;
                else
                    diskSprite.DrawingColor = new Color(40, 40, 40, 256);
            } get { return _side; } }
        private bool _side;
        public bool Visible = false;
        private bool _hideBackground;
        public Color DrawingColor { set { tileSprite.DrawingColor = value; } }
        private Basic2D tileSprite, diskSprite;
        public event EventHandler OnTilePressed, OnMouseOver, OnMouseOut;
        public Tile(bool side, bool visible, Vector2 position, Vector2 dimensions, bool hideBackground = false)
        {
            Position = position;
            tileSprite = new Basic2D("Game/Tile", position, dimensions);
            diskSprite = new Basic2D("Game/Disk", position, dimensions * 0.8f);
            Side = side;
            Visible = visible;
            tileSprite.OnPressed += TileSprite_OnPressed;
            tileSprite.OnMouseOver += TileSprite_OnMouseOver;
            tileSprite.OnMouseOut += TileSprite_OnMouseOut;
            _hideBackground = hideBackground;
        }

        private void TileSprite_OnPressed(object sender, EventArgs e)
        {
            OnTilePressed(this, null);
        }

        private void TileSprite_OnMouseOut(object sender, EventArgs e)
        {
            OnMouseOut(this, null);
        }

        private void TileSprite_OnMouseOver(object sender, EventArgs e)
        {
            OnMouseOver(this, null);
        }

        public void Update(GameTime gameTime)
        {
            tileSprite.Update(gameTime);
            diskSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!_hideBackground)
                tileSprite.Draw(spriteBatch);
            if(Visible)
                diskSprite.Draw(spriteBatch);
        }

        public void ChangeSide()
        {
            Side = !Side;
        }
    }
}
