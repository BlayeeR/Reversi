﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Managers;

namespace Reversi.Sprites
{
    public class Button2D : Basic2D
    {
        public string _text;
        public SpriteFont font;
        public Color FontColor;
        public Color UnselectedFontColor = new Color(122, 54, 6);
        public Color SelectedFontColor = new Color(193, 86, 9);
        private string _fontPath;

        public override event EventHandler OnPressed;

        public Button2D(string path, Vector2 position, string text, string fontPath) : base(path, position)
        {
            _text = text;
            FontColor = UnselectedFontColor;
            DrawingColor = Color.Transparent;
            _fontPath = fontPath;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                FontColor = SelectedFontColor;
            }
            else
                FontColor = UnselectedFontColor;
            if (IsActive && InputManager.Instance.KeyPressed(Keys.Enter))
                this.OnPressed(this, null);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textDimensions = font.MeasureString(_text);
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, _text, Position + new Vector2(-textDimensions.X / 2, -textDimensions.Y / 2), FontColor);
        }

        public override void LoadContent(ContentManager content)
        {
            if (_fontPath != String.Empty)
                font = content.Load<SpriteFont>(_fontPath);
            Dimensions = font.MeasureString(_text);
        }
    }
}
