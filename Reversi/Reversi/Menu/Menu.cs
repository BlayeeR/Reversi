﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Sprites;

namespace Reversi.Menu
{
    public abstract class Menu : IMenu
    {
        public string Axis;
        public List<Button2D> Items;
        int itemNumber;

        public Menu()
        {
            itemNumber = 0;
            Axis = "Y";
            Items = new List<Button2D>();
        }

        public virtual void AlignMenuItems()
        {
            Vector2 dimensions = Vector2.Zero;
            foreach (Button2D button in Items)
                dimensions += button.Dimensions;
            dimensions = new Vector2((GameState.GameStateManager.Instance.Dimensions.X - dimensions.X) / 2, (GameState.GameStateManager.Instance.Dimensions.Y - dimensions.Y) / 2);
            foreach (Button2D button in Items)
            {
                if (Axis == "X")
                    button.Position = new Vector2(dimensions.X, (GameState.GameStateManager.Instance.Dimensions.Y - button.Dimensions.Y) / 2);
                else if (Axis == "Y")
                    button.Position = new Vector2((GameState.GameStateManager.Instance.Dimensions.X - button.Dimensions.X) / 2, dimensions.Y);
                dimensions += button.Dimensions;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button2D button in Items)
            {
                button.Draw(spriteBatch);
            }
        }

        public virtual void LoadContent()
        {
            AlignMenuItems();
        }


        public virtual void Update(GameTime gameTime)
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
    }
}
