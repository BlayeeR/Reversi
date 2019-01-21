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
        public Vector2 Position { internal set; get; }
        public bool Side { internal set; get; }
        public bool Visible = false;
        private Basic2D tileSprite, diskSprite;
        public Tile(bool side, bool visible, Vector2 position, Vector2 dimensions)
        {
            Position = position;
            tileSprite = new Basic2D("Game/Tile", position, dimensions);
            diskSprite = new Basic2D("Game/Disk", position, dimensions * 0.8f);
            Side = side;
            if (Side)
                diskSprite.DrawingColor = Color.White;
            else
                diskSprite.DrawingColor = new Color(40, 40, 40, 256);
            Visible = visible;
            tileSprite.OnPressed += Sprite_OnPressed;
            tileSprite.OnMouseOver += TileSprite_OnMouseOver;
            tileSprite.OnMouseOut += TileSprite_OnMouseOut;

        }

        private void TileSprite_OnMouseOut(object sender, EventArgs e)
        {
            tileSprite.DrawingColor = Color.White;
        }

        private void TileSprite_OnMouseOver(object sender, EventArgs e)
        {
            tileSprite.DrawingColor =Color.Gray;
        }

        public void Update(GameTime gameTime)
        {
            tileSprite.Update(gameTime);
            diskSprite.Update(gameTime);
        }

        private void Sprite_OnPressed(object sender, EventArgs e)
        {
            if (Visible)
                ChangeSide();
            else
                Visible = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
             tileSprite.Draw(spriteBatch);
            if(Visible)
                diskSprite.Draw(spriteBatch);
        }

        public void ChangeSide()
        {
            Side = !Side;
            if (Side)
                diskSprite.DrawingColor = Color.White;
            else
                diskSprite.DrawingColor = new Color(40, 40, 40, 256);
        }
    }
}
