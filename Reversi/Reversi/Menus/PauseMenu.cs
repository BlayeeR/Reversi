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
        private Button2D mainMenuButton, resumeButton;
        public PauseMenu(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            Axis = "Y";
            _game = game;
            _graphicsDevice = graphicsDevice;
            resumeButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Resume", "MenuFont");
            resumeButton.OnPressed += ResumeButton_OnPressed;
            mainMenuButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Main menu", "MenuFont");
            mainMenuButton.OnPressed += MainMenuButton_OnPressed;
        }


        private void ResumeButton_OnPressed(object sender, EventArgs e)
        {
            GameStateManager.Instance.RemoveScreen();
        }

        private void MainMenuButton_OnPressed(object sender, EventArgs e)
        {
            GameStateManager.Instance.ChangeScreen(new TitleScreen(_graphicsDevice, _game));
        }

        public override void LoadContent(ContentManager content)
        {
            Items.Add(resumeButton);
            Items.Add(mainMenuButton);
            resumeButton.LoadContent(content);
            mainMenuButton.LoadContent(content);
        }
    }
}
