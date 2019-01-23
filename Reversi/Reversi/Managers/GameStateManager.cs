using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Models;
using System;
using System.Collections.Generic;

namespace Reversi.Managers
{
    public class GameStateManager
    {
        private static GameStateManager instance;
        private ContentManager content;
        public Vector2 Dimensions { private set; get; }
        private Stack<GameState> screens = new Stack<GameState>();

        public static GameStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameStateManager();
                }
                return instance;
            }
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
        }

        public GameStateManager()
        {
            Dimensions = new Vector2(851, 992);
        }

        public void AddScreen(GameState screen)
        {
            try
            {
                screens.Push(screen);
                screens.Peek().Initialize();
                if (content != null)
                {
                    screens.Peek().LoadContent(content);
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // Log the exception
            }
        }

        public void RemoveScreen()
        {
            if (screens.Count > 0)
            {
                try
                {
                    var screen = screens.Peek();
                    screens.Pop();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    // Log the exception
                }
            }
        }

        public void ClearScreens()
        {
            while (screens.Count > 0)
            {
                screens.Pop();
            }
        }

        public void ChangeScreen(GameState screen)
        {
            try
            {
                ClearScreens();
                AddScreen(screen);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // Log the exception
            }
        }

        public void Update(GameTime gameTime)
        {
            try
            {
                if (screens.Count > 0)
                {
                    screens.Peek().Update(gameTime);
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // Log the exception
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (screens.Count > 0)
                {
                    screens.Peek().Draw(spriteBatch);
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // Log the exception
            }
        }

        public void UnloadContent()
        {
            foreach (GameState state in screens)
            {
                state.UnloadContent();
            }
        }
    }
}