using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Sprites;

namespace Reversi
{
    public class Tile 
    {
        public Vector2 Position { private set; get; }
        public bool Side { set { side = value;
                if (side)
                    _diskSprite.DrawingColor = Color.White;
                else
                    _diskSprite.DrawingColor = new Color(40, 40, 40, 256);
            } get { return side; } }
        private bool side;
        public bool Visible = false;
        private bool hideBackground;
        public Color DrawingColor { set { _tileSprite.DrawingColor = value; } }
        private Basic2D _tileSprite, _diskSprite;
        public event EventHandler OnTilePressed, OnMouseOver, OnMouseOut;
        public Tile(bool side, bool visible, Vector2 position, Vector2 dimensions, bool hideBackground = false)
        {
            Position = position;
            _tileSprite = new Basic2D("Game/Tile", position, dimensions);
            _diskSprite = new Basic2D("Game/Disk", position, dimensions * 0.8f);
            Side = side;
            Visible = visible;
            _tileSprite.OnPressed += TileSprite_OnPressed;
            _tileSprite.OnMouseOver += TileSprite_OnMouseOver;
            _tileSprite.OnMouseOut += TileSprite_OnMouseOut;
            this.hideBackground = hideBackground;
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
            _tileSprite.Update(gameTime);
            _diskSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!hideBackground)
                _tileSprite.Draw(spriteBatch);
            if(Visible)
                _diskSprite.Draw(spriteBatch);
        }

        public void LoadContent(ContentManager content)
        {
            _tileSprite.LoadContent(content);
            _diskSprite.LoadContent(content);
        }

        public void ChangeSide()
        {
            Side = !Side;
        }
    }
}
