using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Managers;
using Reversi.Menus;
using Reversi.Models;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class PauseScreen : GameState
    {
        private MenuManager menuManager = new MenuManager();
        private PauseMenu menu;
        private Basic2D backgroundImage;
        private Text2D creditsText;
        public PauseScreen(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            base.game = game;
            base.graphicsDevice = graphicsDevice;
            menu = new PauseMenu(base.graphicsDevice, base.game);
            backgroundImage = new Basic2D("Game/BackgroundImage", new Vector2(GameStateManager.Instance.Dimensions.X / 2, GameStateManager.Instance.Dimensions.Y / 2), GameStateManager.Instance.Dimensions);
            creditsText = new Text2D(new Vector2(225, 850), "Created by\nJakub Olech", "TitleScreen/CreditsFont", Color.Black);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            backgroundImage.Draw(spriteBatch);
            creditsText.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager content)
        {
            backgroundImage.LoadContent(content);
            creditsText.LoadContent(content);
            menuManager.LoadContent(content);
            menu.LoadContent(content);
            menuManager.AddMenu(menu);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            backgroundImage.Update(gameTime);
            creditsText.Update(gameTime);
            menuManager.Update(gameTime);
        }
    }
}
