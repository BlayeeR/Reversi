using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Sprites;
using Reversi.Managers;
using Reversi.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace Reversi.Models
{
    public abstract class MenuModel : IMenu
    {
        public string Axis;
        public List<MenuItem> Items;
        public bool ToLeft = false;
        private int itemNumber;
        protected Game game;
        protected GraphicsDevice graphicsDevice;

        public MenuModel(GraphicsDevice graphicsDevice, Game game)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            itemNumber = 0;
            Axis = "Y";
            Items = new List<MenuItem>();
            ToLeft = false;
        }

        public void AlignMenuItems()
        {
            Vector2 dimensions = Vector2.Zero;
            foreach (MenuItem item in Items)
                dimensions += item.Dimensions ;
            dimensions = new Vector2((GameStateManager.Instance.Dimensions.X - dimensions.X) / 2, (GameStateManager.Instance.Dimensions.Y - dimensions.Y) / 2);
            foreach (MenuItem item in Items)
            {
                if (Axis == "X")
                    item.Position = new Vector2(dimensions.X, (GameStateManager.Instance.Dimensions.Y - item.Dimensions.Y) / 2);
                else if (Axis == "Y")
                    item.Position = new Vector2((GameStateManager.Instance.Dimensions.X ) / 2, dimensions.Y);
                dimensions += item.Dimensions;
                item.OnMouseOver += Item_OnMouseOver;
            }
        }

        private void Item_OnMouseOver(object sender, EventArgs e)
        {
            MenuItem item = (sender as MenuItem);
            if (Items.IndexOf(item) != itemNumber)
            {
                item.IsActive = true;
                Items[itemNumber].IsActive = false;
                itemNumber = Items.IndexOf(item);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem item in Items)
            {
                item.Draw(spriteBatch);
            }
        }

        public void LoadContent()
        {
            AlignMenuItems();
        }

        public void Update(GameTime gameTime)
        {
            if (Axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right))
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                    itemNumber--;
            }
            else if (Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                    itemNumber--;
            }
            if (itemNumber < 0)
                itemNumber = 0;
            else if (itemNumber > Items.Count - 1)
                itemNumber = Items.Count - 1;
            for (int i = 0; i < Items.Count; i++)
            {
                if (i == itemNumber)
                    Items[i].IsActive = true;
                else
                    Items[i].IsActive = false;
                Items[i].Update(gameTime);
            }
        }

        public abstract void LoadContent(ContentManager content);
    }
}
