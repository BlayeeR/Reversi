using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Models;

namespace Reversi.Sprites
{
    public class Text2D : Sprite
    {
        public string Text;
        private SpriteFont _font;
        public Color FontColor;
        public bool VerticalCenter, HorizontalCenter;
        public Vector2 Position;
        private string _fontPath;

        public Text2D(Vector2 position, string text, string fontPath, Color fontColor, bool verticalCenter = true, bool horizontalCenter = true)
        {
            Text = text;
            VerticalCenter = verticalCenter;
            HorizontalCenter = horizontalCenter;
            Position = position;
            FontColor = fontColor;
            _fontPath = fontPath;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textDimensions = _font.MeasureString(Text);
            spriteBatch.DrawString(_font, Text, Position + new Vector2(VerticalCenter ? -textDimensions.X / 2 : 0, HorizontalCenter ? -textDimensions.Y / 2 : 0), FontColor);
        }

        public override void LoadContent(ContentManager content)
        {
            if (_fontPath != String.Empty)
                _font = content.Load<SpriteFont>(_fontPath);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
