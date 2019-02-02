using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Models
{
    public class MenuItem
    {
        private Button2D button;
        public Vector2 Dimensions { get { return button.Dimensions; } }
        public Vector2 Position { get { return button.Dimensions; } set { button.Position = value; } }
        public event EventHandler OnMouseOver, OnPressed;
        public bool IsActive;

        public MenuItem(Button2D button)
        {
            this.button = button;
            IsActive = false;
            button.OnPressed += Button_OnPressed;
            button.OnMouseOver += Button_OnMouseOver;
        }

        private void Button_OnMouseOver(object sender, EventArgs e)
        {
            OnMouseOver(this, null);
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
                button.FontColor = button.SelectedFontColor;
            else
                button.FontColor = button.UnselectedFontColor;
            button.Update(gameTime);
        }

        public void LoadContent(ContentManager content)
        {
            button.LoadContent(content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            button.Draw(spriteBatch);
        }

        private void Button_OnPressed(object sender, EventArgs e)
        {
            if (IsActive)
                OnPressed(this, null);
        }
    }
}
