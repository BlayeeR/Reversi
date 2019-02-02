using System;
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
        public string Text;
        private SpriteFont font;
        public Color FontColor;
        public Color UnselectedFontColor = new Color(122, 54, 6);
        public Color SelectedFontColor = new Color(193, 86, 9);
        private string fontPath;

        public override event EventHandler OnPressed;

        public Button2D(string path, Vector2 position, string text, string fontPath) : base(path, position)
        {
            Text = text;
            FontColor = UnselectedFontColor;
            DrawingColor = Color.Transparent;
            this.fontPath = fontPath;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive)
                FontColor = SelectedFontColor;
            else
                FontColor = UnselectedFontColor;
            if (IsActive && (InputManager.Instance.KeyPressed(Keys.Enter)|| InputManager.Instance.LMBPressed()))
                this.OnPressed(this, null);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textDimensions = font.MeasureString(Text);
            base.Draw(spriteBatch);
            //spriteBatch.Draw(SpriteTexture, new Rectangle((int)(Position.X-Dimensions.X/2), (int)(Position.Y-Dimensions.Y/2), Dimensions.ToPoint().X, Dimensions.ToPoint().Y), Color.White);
            spriteBatch.DrawString(font, Text, Position + new Vector2(-textDimensions.X / 2, -textDimensions.Y / 2), FontColor);
        }

        public override void LoadContent(ContentManager content)
        {
            if (fontPath != String.Empty)
                font = content.Load<SpriteFont>(fontPath);
            base.Dimensions = font.MeasureString(Text);
            base.LoadContent(content);
        }
    }
}
