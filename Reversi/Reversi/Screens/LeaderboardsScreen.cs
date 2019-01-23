using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.GameState;
using Reversi.Menu;
using Reversi.Menus;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class LeaderboardsScreen : GameState.GameState
    {
        private Basic2D backgroundImage;
        private Button2D creditsText, highscoresText;
        private ScoreManager scoreManager;
        public LeaderboardsScreen(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            backgroundImage = new Basic2D("Game/BackgroundImage", new Vector2(GameState.GameStateManager.Instance.Dimensions.X / 2, GameState.GameStateManager.Instance.Dimensions.Y / 2), GameState.GameStateManager.Instance.Dimensions);
            creditsText = new Button2D("TitleScreen/Button", new Vector2(225, 850), "Created by\nJakub Olech", "TitleScreen/CreditsFont");
            creditsText.UnselectedFontColor = Color.Black;
            highscoresText = new Button2D("TitleScreen/Button", new Vector2(120, 110), "High scores:\n", "MenuFont", false, false);
            scoreManager = ScoreManager.Load();
            foreach (Score score in scoreManager.Scores)
            {
                highscoresText._text += $"{scoreManager.Scores.IndexOf(score) + 1}: {score.PlayerName}- {score.Value}\n";
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            backgroundImage.Draw(spriteBatch);
            creditsText.Draw(spriteBatch);
            highscoresText.Draw(spriteBatch);
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
            if (InputManager.Instance.KeyPressed(Keys.Escape))
                GameState.GameStateManager.Instance.RemoveScreen();
            backgroundImage.Update(gameTime);
            creditsText.Update(gameTime);
            highscoresText.Update(gameTime);
        }
    }
}
