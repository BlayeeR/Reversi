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
    public class TitleMenu : Menu.Menu
    {
        Button2D exitButton, multiplayerButton, singleplayerButton;
        public TitleMenu(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            singleplayerButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Singleplayer", "MenuFont");
            Items.Add(singleplayerButton);
            singleplayerButton.OnPressed += SingleplayerButton_OnPressed;
            multiplayerButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Multiplayer", "MenuFont");
            multiplayerButton.OnPressed += MultiplayerButton_OnPressed;
            Items.Add(multiplayerButton);
            Items.Add(new Button2D("TitleScreen/Button", Vector2.Zero, "Leaderboards", "MenuFont"));
            exitButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Exit", "MenuFont");
            exitButton.OnPressed += ExitButton_OnPressed;
            Items.Add(exitButton);
            Axis = "Y";
            _game = game;
            _graphicsDevice = graphicsDevice;
        }

        private void SingleplayerButton_OnPressed(object sender, EventArgs e)
        {
            GameState.GameStateManager.Instance.ChangeScreen(new SingleplayerScreen(_graphicsDevice, _game));
        }

        private void MultiplayerButton_OnPressed(object sender, EventArgs e)
        {
            GameState.GameStateManager.Instance.ChangeScreen(new MultiplayerScreen(_graphicsDevice, _game));
        }

        private void ExitButton_OnPressed(object sender, EventArgs e)
        {
            _game.Exit();
        }

    }
}
