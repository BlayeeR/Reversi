using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.GameState;
using Reversi.Menu;
using Reversi.Menus;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class PauseScreen : GameState.GameState
    {
        public MenuManager menuManager = new MenuManager();
        public PauseMenu menu;
        private Basic2D backgroundImage;
        private Button2D creditsText;
        public PauseScreen(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            backgroundImage = new Basic2D("Game/BackgroundImage", new Vector2(GameState.GameStateManager.Instance.Dimensions.X / 2, GameState.GameStateManager.Instance.Dimensions.Y / 2), GameState.GameStateManager.Instance.Dimensions);
            creditsText = new Button2D("TitleScreen/Button", new Vector2(225, 850), "Created by\nJakub Olech", "TitleScreen/CreditsFont");
            creditsText.UnselectedFontColor = Color.Black;
            menu = new PauseMenu(_graphicsDevice, _game);
            menuManager.AddMenu(menu);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            backgroundImage.Draw(spriteBatch);
            creditsText.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
            // Draw sprites here
            spriteBatch.End();
        }

        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager content)
        {

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
