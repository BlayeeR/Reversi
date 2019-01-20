using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Sprites
{
    public class Basic2D
    {
        public Vector2 Position, Dimensions;
        private Texture2D _texture;
        public event EventHandler OnMouseOver, OnMouseOut, OnPressed;
        private bool mouseOverOldState=false;
        public bool IsActive;

        public Color DrawingColor = Color.White;

        public Basic2D(string path, Vector2 position, Vector2 dimensions) :  this()
        {
            Position = position;
            Dimensions = dimensions;
            _texture = Globals.Content.Load<Texture2D>(path);
        }

        public Basic2D(string path, Vector2 position) : this()
        {
            Position = position;
            _texture = Globals.Content.Load<Texture2D>(path);
            Dimensions = new Vector2(_texture.Width, _texture.Height);
        }

        private Basic2D()
        {
            OnPressed += Basic2D_OnPressed;
            OnMouseOver += Basic2D_OnMouseOver;
            OnMouseOut += Basic2D_OnMouseOut;
        }


        public virtual void Update(GameTime gameTime)
        {
            Vector2 mousePosition = new Vector2(InputManager.Instance.MouseState().Position.X, InputManager.Instance.MouseState().Position.Y);
            if(mousePosition.X >= Position.X - Dimensions.X / 2 && mousePosition.Y >= Position.Y - Dimensions.Y / 2 && mousePosition.X <= Position.X + Dimensions.X / 2 && mousePosition.Y <=Position.Y + Dimensions.Y / 2)
                OnMouseOver(this, null);
            else if (mouseOverOldState)
                OnMouseOut(this, null);

        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(_texture !=  null)
            {
                spriteBatch.Draw(_texture, new Rectangle((Position).ToPoint(), Dimensions.ToPoint()), null, DrawingColor, 0.0f, new Vector2(_texture.Bounds.Width / 2, _texture.Bounds.Height / 2), SpriteEffects.None, 0);
            }
        }

        private void Basic2D_OnMouseOver(object sender, EventArgs e)
        {
            mouseOverOldState = true;
            if (InputManager.Instance.LMBPressed())
                OnPressed(this, null);

        }

        private void Basic2D_OnButtonSelected(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Basic2D_OnMouseOut(object sender, EventArgs e)
        {
            mouseOverOldState = false;
        }

        private void Basic2D_OnPressed(object sender, EventArgs e)
        {
            
        }
    }
}
