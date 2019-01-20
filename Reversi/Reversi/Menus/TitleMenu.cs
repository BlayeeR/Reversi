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
        Button2D exitButton, multiplayerButton;
        public TitleMenu(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            exitButton = new Button2D("TitleScreen/Button", new Vector2(300, 300), "Exit", "TitleScreen/DefaultFont");
            exitButton.OnPressed += ExitButton_OnPressed;
            multiplayerButton = new Button2D("TitleScreen/Button", new Vector2(300, 300), "Singleplayer", "TitleScreen/DefaultFont");
            multiplayerButton.OnPressed += MultiplayerButton_OnPressed;
            Items.Add(multiplayerButton);
            Items.Add(new Button2D("TitleScreen/Button", new Vector2(300, 300), "Multiplayer", "TitleScreen/DefaultFont"));
            Items.Add(new Button2D("TitleScreen/Button", new Vector2(300, 300), "Leaderboards", "TitleScreen/DefaultFont"));
            Items.Add(exitButton);
            Axis = "Y";
            _game = game;
            _graphicsDevice = graphicsDevice;
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
