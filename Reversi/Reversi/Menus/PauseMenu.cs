using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Menu;
using Reversi.Screens;
using Reversi.Sprites;

namespace Reversi.Menus
{
    public class PauseMenu: Menu.Menu
    {
        Button2D mainMenuButton, resumeButton;
        public PauseMenu(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            resumeButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Resume", "MenuFont");
            Items.Add(resumeButton);
            resumeButton.OnPressed += ResumeButton_OnPressed;
            mainMenuButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Main menu", "MenuFont");
            mainMenuButton.OnPressed += MainMenuButton_OnPressed;
            Items.Add(mainMenuButton);
            Axis = "Y";
            _game = game;
            _graphicsDevice = graphicsDevice;
        }


        private void ResumeButton_OnPressed(object sender, EventArgs e)
        {
            GameState.GameStateManager.Instance.RemoveScreen();
        }

        private void MainMenuButton_OnPressed(object sender, EventArgs e)
        {
            GameState.GameStateManager.Instance.ChangeScreen(new TitleScreen(_graphicsDevice, _game));
        }
    }
}
