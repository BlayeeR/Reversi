using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public virtual event EventHandler OnMouseOver, OnMouseOut, OnPressed;
        private bool mouseOverOldState=false;
        private MouseState oldMouseState, currentMouseState;
        public bool IsActive=false;

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
            OnMouseOver += Basic2D_OnMouseOver;
            OnMouseOut += Basic2D_OnMouseOut;
            OnPressed += Basic2D_OnPressed;
        }

        private void Basic2D_OnPressed(object sender, EventArgs e)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            oldMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(InputManager.Instance.MouseState().Position.X, InputManager.Instance.MouseState().Position.Y);
            if (mousePosition.X > Position.X - Dimensions.X / 2 && mousePosition.Y > Position.Y - Dimensions.Y / 2 && mousePosition.X < Position.X + Dimensions.X / 2 && mousePosition.Y < Position.Y + Dimensions.Y / 2)
            {
                if (InputManager.Instance.LMBPressed())
                    OnPressed(this, null);
                OnMouseOver(this, null);
            }
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
            

        }

        private void Basic2D_OnMouseOut(object sender, EventArgs e)
        {
            mouseOverOldState = false;
        }

    }
}
