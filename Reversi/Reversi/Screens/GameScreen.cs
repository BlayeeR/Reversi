using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Menu;
using Reversi.Menus;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class GameScreen : GameState.GameState
    {
        public TileManager tileManager;
        private Basic2D backgroundImage;
        private bool gameEnded = false, _singlePlayer;
        private Button2D[] Text = new Button2D[2];
        ScoreManager scoreManager;
        public GameScreen(GraphicsDevice graphicsDevice, Game game, bool singlePlayer = true) : base(graphicsDevice, game)
        {
            tileManager = new TileManager(new Vector2(90), new Vector2(667), singlePlayer);
            backgroundImage = new Basic2D("Game/BackgroundImage", new Vector2(GameState.GameStateManager.Instance.Dimensions.X/2, GameState.GameStateManager.Instance.Dimensions.Y/2), GameState.GameStateManager.Instance.Dimensions);
            tileManager.OnGameEnded += TileManager_OnGameEnded;
            _singlePlayer = singlePlayer;
            Text[0] = new Button2D("TitleScreen/Button", new Vector2(430, 350), "", "MenuFont", true, false);
            Text[1] = new Button2D("TitleScreen/Button", new Vector2(430, 400), "", "MenuFont", true, false);
            scoreManager = ScoreManager.Load();
        }

        private void TileManager_OnGameEnded(object sender, EventArgs e)
        {
            GameEndedEventArgs gameEndedEventArgs = (e as GameEndedEventArgs);
            switch (gameEndedEventArgs.Result)
            {
                case -1:
                    {
                        if (_singlePlayer)
                            Text[0]._text = "You win!";
                        else
                            Text[0]._text = "Black wins!";
                        break;
                    }
                case 0:
                    {
                        Text[0]._text = "Draw!";
                        break;
                    }
                case 1:
                    {
                        if (_singlePlayer)
                            Text[0]._text = "You lose!";
                        else
                            Text[0]._text = "White wins!";
                        break;
                    }
            }
            if (scoreManager.Scores.Count != 0)
            {
                if (gameEndedEventArgs.Scores[0].Value > scoreManager.Scores.LastOrDefault().Value && gameEndedEventArgs.Scores[1].Value > scoreManager.Scores.LastOrDefault().Value)
                {
                    Text[1]._text += $"New high scores: {gameEndedEventArgs.Scores[0].Value}, {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[0].PlayerName = DateTime.Now.ToString();
                    gameEndedEventArgs.Scores[1].PlayerName = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[0]);
                    scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
                else if (gameEndedEventArgs.Scores[0].Value > scoreManager.Scores.LastOrDefault().Value && gameEndedEventArgs.Scores[0].Value != 0)
                {
                    Text[1]._text += $"New high score: {gameEndedEventArgs.Scores[0].Value}";
                    gameEndedEventArgs.Scores[0].PlayerName = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[0]);
                }
                else if (gameEndedEventArgs.Scores[1].Value > scoreManager.Scores.LastOrDefault().Value && gameEndedEventArgs.Scores[1].Value != 0)
                {
                    Text[1]._text += $"New high score: {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[1].PlayerName = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
            }
            else
            {
                if (gameEndedEventArgs.Scores[0].Value != 0 && gameEndedEventArgs.Scores[1].Value != 0)
                {
                    Text[1]._text += $"New high scores: {gameEndedEventArgs.Scores[0].Value}, {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[0].PlayerName = DateTime.Now.ToString();
                    gameEndedEventArgs.Scores[1].PlayerName = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[0]);
                    scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
                else if (gameEndedEventArgs.Scores[0].Value != 0)
                {
                    Text[1]._text += $"New high score: {gameEndedEventArgs.Scores[0].Value}";
                    gameEndedEventArgs.Scores[0].PlayerName = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[0]);
                }
                else if (gameEndedEventArgs.Scores[1].Value != 0)
                {
                    Text[1]._text += $"New high score: {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[1].PlayerName = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
            }
            ScoreManager.Save(scoreManager);
            gameEnded = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.AliceBlue);
            spriteBatch.Begin();
            backgroundImage.Draw(spriteBatch);
            if (!gameEnded)
                tileManager.Draw(spriteBatch);
            else
                foreach (Button2D text in Text)
                    text.Draw(spriteBatch);
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
            if (InputManager.Instance.KeyPressed(Keys.Escape) && !gameEnded)
                GameState.GameStateManager.Instance.AddScreen(new PauseScreen(_graphicsDevice, _game));
            backgroundImage.Update(gameTime);
            if (!gameEnded)
                tileManager.Update(gameTime);
            else
            {
                foreach(Button2D text in Text)
                    text.Update(gameTime);
                if (InputManager.Instance.KeyPressed(Keys.Enter))
                    GameState.GameStateManager.Instance.ChangeScreen(new TitleScreen(_graphicsDevice, _game));
            }
        }
    }
}
