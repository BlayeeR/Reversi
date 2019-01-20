using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Menu
{
    public class MenuManager
    {
        private Stack<Menu> _menus = new Stack<Menu>();
        public void AddMenu(Menu menu)
        {
            try
            {
                _menus.Push(menu);
                //_menus.Peek().Initialize();
                if (Globals.Content != null)
                {
                    _menus.Peek().LoadContent();
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // Log the exception
            }
        }

        public void RemoveMenu()
        {
            if (_menus.Count > 0)
            {
                try
                {
                    var menu = _menus.Peek();
                    _menus.Pop();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    // Log the exception
                }
            }
        }

        public void ClearMenus()
        {
            while (_menus.Count > 0)
            {
                _menus.Pop();
            }
        }

        public void ChangeScreen(Menu menu)
        {
            try
            {
                ClearMenus();
                AddMenu(menu);
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
                if (_menus.Count > 0)
                {
                    _menus.Peek().Update(gameTime);
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
                if (_menus.Count > 0)
                {
                    _menus.Peek().Draw(spriteBatch);
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
        }
    }
}
