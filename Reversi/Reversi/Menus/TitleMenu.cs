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
        MenuItem exitItem, mutliplayerItem, singleplayerItem, leaderboardsItem;
        public TitleMenu(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            Axis = "Y";
            base.game = game;
            base.graphicsDevice = graphicsDevice;
            singleplayerItem = new MenuItem(new Button2D("TitleScreen/Button", Vector2.Zero, "Singleplayer", "MenuFont"));
            singleplayerItem.OnPressed += SingleplayerButton_OnPressed;
            mutliplayerItem = new MenuItem(new Button2D("TitleScreen/Button", Vector2.Zero, "Multiplayer", "MenuFont"));
            mutliplayerItem.OnPressed += MultiplayerButton_OnPressed;
            leaderboardsItem = new MenuItem(new Button2D("TitleScreen/Button", Vector2.Zero, "Leaderboards", "MenuFont"));
            leaderboardsItem.OnPressed += LeaderboardsButton_OnPressed;
            exitItem = new MenuItem(new Button2D("TitleScreen/Button", Vector2.Zero, "Exit", "MenuFont"));
            exitItem.OnPressed += ExitButton_OnPressed;
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
            singleplayerItem.LoadContent(content);
            mutliplayerItem.LoadContent(content);
            leaderboardsItem.LoadContent(content);
            exitItem.LoadContent(content);
            Items.Add(singleplayerItem);
            Items.Add(mutliplayerItem);
            Items.Add(leaderboardsItem);
            Items.Add(exitItem);
            base.LoadContent(content);
        }
    }
}
