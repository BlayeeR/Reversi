using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Screens;
using Reversi.Sprites;
using Reversi.Managers;
using Reversi.Models;
using Microsoft.Xna.Framework.Content;

namespace Reversi.Menus
{
    public class PauseMenu : MenuModel
    {
        private MenuItem mainMenuItem, resumeItem;
        public PauseMenu(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            Axis = "Y";
            base.game = game;
            base.graphicsDevice = graphicsDevice;
            resumeItem = new MenuItem(new Button2D("TitleScreen/Button", Vector2.Zero, "Resume", "MenuFont"));
            resumeItem.OnPressed += ResumeButton_OnPressed;
            mainMenuItem = new MenuItem(new Button2D("TitleScreen/Button", Vector2.Zero, "Main menu", "MenuFont"));
            mainMenuItem.OnPressed += MainMenuButton_OnPressed;
        }


        private void ResumeButton_OnPressed(object sender, EventArgs e)
        {
            GameStateManager.Instance.RemoveScreen();
        }

        private void MainMenuButton_OnPressed(object sender, EventArgs e)
        {
            GameStateManager.Instance.ChangeScreen(new TitleScreen(graphicsDevice, game));
        }

        public override void LoadContent(ContentManager content)
        {
            Items.Add(resumeItem);
            Items.Add(mainMenuItem);
            resumeItem.LoadContent(content);
            mainMenuItem.LoadContent(content);
        }
    }
}
