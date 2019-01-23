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
    public class TitleMenu : MenuModel
    {
        Button2D exitButton, multiplayerButton, singleplayerButton, leaderboardsButton;
        public TitleMenu(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            Axis = "Y";
            base.game = game;
            base.graphicsDevice = graphicsDevice;
            singleplayerButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Singleplayer", "MenuFont");
            singleplayerButton.OnPressed += SingleplayerButton_OnPressed;
            multiplayerButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Multiplayer", "MenuFont");
            multiplayerButton.OnPressed += MultiplayerButton_OnPressed;
            leaderboardsButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Leaderboards", "MenuFont");
            leaderboardsButton.OnPressed += LeaderboardsButton_OnPressed;
            exitButton = new Button2D("TitleScreen/Button", Vector2.Zero, "Exit", "MenuFont");
            exitButton.OnPressed += ExitButton_OnPressed;
        }

        private void LeaderboardsButton_OnPressed(object sender, EventArgs e)
        {
            GameStateManager.Instance.AddScreen(new LeaderboardsScreen(graphicsDevice, game));
        }

        private void SingleplayerButton_OnPressed(object sender, EventArgs e)
        {
            GameStateManager.Instance.ChangeScreen(new GameScreen(graphicsDevice, game, true));
        }

        private void MultiplayerButton_OnPressed(object sender, EventArgs e)
        {
            GameStateManager.Instance.ChangeScreen(new GameScreen(graphicsDevice, game, false));
        }

        private void ExitButton_OnPressed(object sender, EventArgs e)
        {
            game.Exit();
        }

        public override void LoadContent(ContentManager content)
        {
            singleplayerButton.LoadContent(content);
            multiplayerButton.LoadContent(content);
            leaderboardsButton.LoadContent(content);
            exitButton.LoadContent(content);
            Items.Add(singleplayerButton);
            Items.Add(multiplayerButton);
            Items.Add(leaderboardsButton);
            Items.Add(exitButton);
        }
    }
}
