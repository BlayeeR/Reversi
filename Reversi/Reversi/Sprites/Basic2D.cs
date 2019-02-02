using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Managers;
using Reversi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Sprites
{
    public class Basic2D : Sprite
    {
        public Vector2 Position { get { return position; } set {
                position = value;
                hitboxRectangle.Location = new Point((int)(Position.X - Dimensions.X / 2), (int)(Position.Y - Dimensions.Y / 2));
            } }
        public Vector2 Dimensions { get { return dimensions; } set {
                dimensions = value;
                hitboxRectangle.Location = new Point((int)(Position.X - Dimensions.X / 2), (int)(Position.Y - Dimensions.Y / 2));
                hitboxRectangle.Size = Dimensions.ToPoint();
            } }

        public bool IsMouseOver { get; private set; }
        public Vector2 position, dimensions;
        protected Texture2D SpriteTexture;
        public virtual event EventHandler OnMouseOver, OnMouseOut, OnPressed;
        private bool mouseOverOldState=false;
        public bool IsActive=false;
        public Color DrawingColor = Color.White;
        private Rectangle hitboxRectangle = new Rectangle();
        private string path;

        public Basic2D(string path, Vector2 position, Vector2 dimensions) :  this()
        {
            Position = position;
            this.path = path;
            this.SpriteTexture = null;
            Dimensions = dimensions;
        }

        public Basic2D(string path, Vector2 position) : this()
        {
            Position = position;
            this.path = path;
            this.SpriteTexture = null;
            Dimensions = Vector2.Zero;
        }
        public Basic2D(Texture2D texture, Vector2 position, Vector2 dimensions) : this()
        {
            Position = position;
            this.path = String.Empty;
            this.SpriteTexture = texture;
            Dimensions = dimensions;
        }

        public Basic2D(Texture2D texture, Vector2 position) : this()
        {
            Position = position;
            this.path = String.Empty;
            this.SpriteTexture = texture;
            Dimensions = Vector2.Zero;
        }

        private Basic2D()
        {
            IsMouseOver = false;
            OnMouseOver += Basic2D_OnMouseOver;
            OnMouseOut += Basic2D_OnMouseOut;
            OnPressed += Basic2D_OnPressed;
        }

        protected virtual void Basic2D_OnPressed(object sender, EventArgs e)
        {
            return;
        }

        protected virtual void Basic2D_OnMouseOver(object sender, EventArgs e)
        {
            mouseOverOldState = true;
            IsMouseOver = true;
        }

        protected virtual void Basic2D_OnMouseOut(object sender, EventArgs e)
        {
            mouseOverOldState = false;
            IsMouseOver = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (SpriteTexture != null)
            {
                spriteBatch.Draw(SpriteTexture, new Rectangle((Position).ToPoint(), Dimensions.ToPoint()), null, DrawingColor, 0.0f, new Vector2(SpriteTexture.Bounds.Width / 2, SpriteTexture.Bounds.Height / 2), SpriteEffects.None, 0);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            if(SpriteTexture == null)
                SpriteTexture = content.Load<Texture2D>(path);
            Dimensions = Dimensions==Vector2.Zero?new Vector2(SpriteTexture.Width, SpriteTexture.Height):Dimensions;
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.MouseIntersects(hitboxRectangle))
            {
                if (InputManager.Instance.LMBPressed())
                    OnPressed(this, null);
                OnMouseOver(this, null);
            }
            else if (mouseOverOldState)
                OnMouseOut(this, null);
        }
    }
}
