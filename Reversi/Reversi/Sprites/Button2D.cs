using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Reversi.Sprites
{
    public class Button2D : Basic2D
    {
        public string _text;

        public SpriteFont font;
        

        public Button2D(string path, Vector2 position, string text, string fontPath) : base(path, position)
        {
            _text = text;
            if (fontPath != String.Empty)
                font = Globals.Content.Load<SpriteFont>(fontPath);
            this.OnMouseOut += Button2D_OnMouseOut;
            this.OnMouseOver += Button2D_OnMouseOver;
            this.OnPressed += Button2D_OnPressed;
            
        }

        private void Button2D_OnPressed(object sender, EventArgs e)
        {
        }

        private void Button2D_OnMouseOver(object sender, EventArgs e)
        {
            DrawingColor = Color.Blue;
        }

        private void Button2D_OnMouseOut(object sender, EventArgs e)
        {
            DrawingColor = Color.White;
        }

        public Button2D(string path, Vector2 position, Vector2 dimensions, string text, string fontPath) : base(path, position, dimensions)
        {
            _text = text;
            if (fontPath != String.Empty)
                font = Globals.Content.Load<SpriteFont>(fontPath);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textDimensions = font.MeasureString(_text);
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, _text, Position + new Vector2(-textDimensions.X / 2, -textDimensions.Y / 2), Color.Green);

        }

    }
}
