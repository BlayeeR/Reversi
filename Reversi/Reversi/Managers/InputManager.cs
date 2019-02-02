using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Managers
{
    public class InputManager
    {
        private KeyboardState currentKeyState, previousKeyState;
        private MouseState currentMouseState, previousMouseState;

        public enum MouseButtons { Left, Middle, Right, X1, X2 };

        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();

                return instance;
            }
        }

        public void Update()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool MouseButtonPressed(params MouseButtons[] buttons)
        {
            foreach(MouseButtons button in buttons)
            {
                switch(button)
                {
                    case MouseButtons.Left:
                        {
                            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                                return true;
                            return false;
                        }
                    case MouseButtons.Middle:
                        {
                            if (currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released)
                                return true;
                            return false;
                        }
                    case MouseButtons.Right:
                        {
                            if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
                                return true;
                            return false;
                        }
                    case MouseButtons.X1:
                        {
                            if (currentMouseState.XButton1 == ButtonState.Pressed && previousMouseState.XButton1 == ButtonState.Released)
                                return true;
                            return false;
                        }
                    case MouseButtons.X2:
                        {
                            if (currentMouseState.XButton2 == ButtonState.Pressed && previousMouseState.XButton2 == ButtonState.Released)
                                return true;
                            return false;
                        }
                }
            }
            return false;
        }

        public bool AnyKeyPressed()
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                if (KeyPressed(key))
                    return true;
            foreach (MouseButtons button in Enum.GetValues(typeof(MouseButtons)))
                if (MouseButtonPressed(button))
                    return true;
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && previousKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool MouseIntersects(Rectangle target)
        {
            if (currentMouseState.Position.X > target.Left && currentMouseState.Position.Y > target.Top && currentMouseState.Position.X < target.Right && currentMouseState.Position.Y < target.Bottom)
                return true;
            return false;
        }
    }
}
