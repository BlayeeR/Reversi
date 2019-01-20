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
    public class Disk 
    {
        private bool _side;
        private Vector2 _position;
        public bool Side { get; }
        public bool Visible = false;
        public Random rnd = new Random();
        public Vector2 Position { get { return _position; } }
        private Basic2D sprite;
        public Disk(bool side, Vector2 position, Vector2 dimensions,bool visible)
        {
            
            _side = side;
            _position = position;
            sprite = new Basic2D("Game/Disk", position, dimensions);
            if (_side)
                sprite.DrawingColor = Color.White;
            else
                sprite.DrawingColor = new Color(40,40,40, 256);
            Visible = visible;
            sprite.OnPressed += Sprite_OnPressed;
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
            
        }

        private void Sprite_OnPressed(object sender, EventArgs e)
        {
            ChangeSide();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(Visible)
                sprite.Draw(spriteBatch);
        }

        public void ChangeSide()
        {
            _side = !_side;
            if (_side)
                sprite.DrawingColor = Color.White;
            else
                sprite.DrawingColor = new Color(40, 40, 40, 256);
        }

    }
}
