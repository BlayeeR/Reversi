using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Managers
{
    public class MenuManager
    {
        private Stack<MenuModel> menus = new Stack<MenuModel>();
        private ContentManager content;

        public void LoadContent(ContentManager content)
        {
            this.content = content;
        }
        public void AddMenu(MenuModel menu)
        {
            try
            {
                menus.Push(menu);
                //_menus.Peek().Initialize();
                if (content != null)
                {
                    menus.Peek().LoadContent(content);
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
            if (menus.Count > 0)
            {
                try
                {
                    var menu = menus.Peek();
                    menus.Pop();
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
            while (menus.Count > 0)
            {
                menus.Pop();
            }
        }

        public void ChangeScreen(MenuModel menu)
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
                if (menus.Count > 0)
                {
                    menus.Peek().Update(gameTime);
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
                if (menus.Count > 0)
                {
                    menus.Peek().Draw(spriteBatch);
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // Log the exception
            }
        }
    }
}
