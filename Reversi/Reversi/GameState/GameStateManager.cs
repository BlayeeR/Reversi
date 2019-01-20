﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Reversi.GameState
{
    public class GameStateManager
    {
        // Instance of the game state manager     
        private static GameStateManager _instance;
        public Vector2 Dimensions { private set; get; }

        // Stack for the screens     
        private Stack<GameState> _screens = new Stack<GameState>();

        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        public GameStateManager()
        {
            Dimensions = new Vector2(1280, 720);
        }

        // Adds a new screen to the stack 
        public void AddScreen(GameState screen)
        {
            try
            {
                // Add the screen to the stack
                _screens.Push(screen);
                // Initialize the screen
                _screens.Peek().Initialize();
                // Call the LoadContent on the screen
                if (Globals.Content != null)
                {
                    _screens.Peek().LoadContent(Globals.Content);
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // Log the exception
            }
        }

        // Removes the top screen from the stack
        public void RemoveScreen()
        {
            if (_screens.Count > 0)
            {
                try
                {
                    var screen = _screens.Peek();
                    _screens.Pop();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    // Log the exception
                }
            }
        }

        // Clears all the screen from the list
        public void ClearScreens()
        {
            while (_screens.Count > 0)
            {
                _screens.Pop();
            }
        }

        // Removes all screens from the stack and adds a new one 
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

        // Updates the top screen. 
        public void Update(GameTime gameTime)
        {
            try
            {
                if (_screens.Count > 0)
                {
                    _screens.Peek().Update(gameTime);
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // Log the exception
            }
        }

        // Renders the top screen.
        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (_screens.Count > 0)
                {
                    _screens.Peek().Draw(spriteBatch);
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // Log the exception
            }
        }

        // Unloads the content from the screen
        public void UnloadContent()
        {
            foreach (GameState state in _screens)
            {
                state.UnloadContent();
            }
        }
    }
    
}