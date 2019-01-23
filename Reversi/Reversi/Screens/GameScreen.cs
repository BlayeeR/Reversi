using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Events;
using Reversi.Managers;
using Reversi.Models;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class GameScreen : GameState
    {
        private TileManager tileManager;
        private Basic2D backgroundImage;
        private bool gameEnded = false, _singlePlayer;
        private Text2D[] texts = new Text2D[2];
        private ScoreManager _scoreManager;
        public GameScreen(GraphicsDevice graphicsDevice, Game game, bool singlePlayer = true) : base(graphicsDevice, game)
        {
            _singlePlayer = singlePlayer;
            backgroundImage = new Basic2D("Game/BackgroundImage", new Vector2(GameStateManager.Instance.Dimensions.X / 2, GameStateManager.Instance.Dimensions.Y / 2), GameStateManager.Instance.Dimensions);
            texts[0] = new Text2D(new Vector2(430, 350), "", "MenuFont", Color.Black, true, false);
            texts[1] = new Text2D(new Vector2(430, 400), "", "MenuFont", Color.Black, true, false);
        }

        private void TileManager_OnGameEnded(object sender, EventArgs e)
        {
            GameEndedEventArgs gameEndedEventArgs = (e as GameEndedEventArgs);
            switch (gameEndedEventArgs.Result)
            {
                case -1:
                    {
                        if (_singlePlayer)
                            texts[0].Text = "You win!";
                        else
                            texts[0].Text = "Black wins!";
                        break;
                    }
                case 0:
                    {
                        texts[0].Text = "Draw!";
                        break;
                    }
                case 1:
                    {
                        if (_singlePlayer)
                            texts[0].Text = "You lose!";
                        else
                            texts[0].Text = "White wins!";
                        break;
                    }
            }
            if (_scoreManager.Scores.Count != 0)
            {
                if (gameEndedEventArgs.Scores[0].Value > _scoreManager.Scores.LastOrDefault().Value && gameEndedEventArgs.Scores[1].Value > _scoreManager.Scores.LastOrDefault().Value)
                {
                    texts[1].Text += $"New high scores: {gameEndedEventArgs.Scores[0].Value}, {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[0].PlayerName = DateTime.Now.ToString();
                    gameEndedEventArgs.Scores[1].PlayerName = DateTime.Now.ToString();
                    _scoreManager.Add(gameEndedEventArgs.Scores[0]);
                    _scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
                else if (gameEndedEventArgs.Scores[0].Value > _scoreManager.Scores.LastOrDefault().Value && gameEndedEventArgs.Scores[0].Value != 0)
                {
                    texts[1].Text += $"New high score: {gameEndedEventArgs.Scores[0].Value}";
                    gameEndedEventArgs.Scores[0].PlayerName = DateTime.Now.ToString();
                    _scoreManager.Add(gameEndedEventArgs.Scores[0]);
                }
                else if (gameEndedEventArgs.Scores[1].Value > _scoreManager.Scores.LastOrDefault().Value && gameEndedEventArgs.Scores[1].Value != 0)
                {
                    texts[1].Text += $"New high score: {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[1].PlayerName = DateTime.Now.ToString();
                    _scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
            }
            else
            {
                if (gameEndedEventArgs.Scores[0].Value != 0 && gameEndedEventArgs.Scores[1].Value != 0)
                {
                    texts[1].Text += $"New high scores: {gameEndedEventArgs.Scores[0].Value}, {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[0].PlayerName = DateTime.Now.ToString();
                    gameEndedEventArgs.Scores[1].PlayerName = DateTime.Now.ToString();
                    _scoreManager.Add(gameEndedEventArgs.Scores[0]);
                    _scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
                else if (gameEndedEventArgs.Scores[0].Value != 0)
                {
                    texts[1].Text += $"New high score: {gameEndedEventArgs.Scores[0].Value}";
                    gameEndedEventArgs.Scores[0].PlayerName = DateTime.Now.ToString();
                    _scoreManager.Add(gameEndedEventArgs.Scores[0]);
                }
                else if (gameEndedEventArgs.Scores[1].Value != 0)
                {
                    texts[1].Text += $"New high score: {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[1].PlayerName = DateTime.Now.ToString();
                    _scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
            }
            ScoreManager.Save(_scoreManager);
            gameEnded = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.AliceBlue);
            spriteBatch.Begin();
            backgroundImage.Draw(spriteBatch);
            if (!gameEnded)
                tileManager.Draw(spriteBatch);
            else
                foreach (Text2D text in texts)
                    text.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            tileManager = new TileManager(new Vector2(90), new Vector2(667), _singlePlayer, content);
            tileManager.LoadContent(content);
            tileManager.OnGameEnded += TileManager_OnGameEnded;
            _scoreManager = ScoreManager.Load();
            foreach (Text2D text in texts)
                text.LoadContent(content);
            backgroundImage.LoadContent(content);
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.KeyPressed(Keys.Escape) && !gameEnded)
                GameStateManager.Instance.AddScreen(new PauseScreen(graphicsDevice, game));
            backgroundImage.Update(gameTime);
            if (!gameEnded)
                tileManager.Update(gameTime);
            else
            {
                foreach(Text2D text in texts)
                    text.Update(gameTime);
                if (InputManager.Instance.KeyPressed(Keys.Enter))
                    GameStateManager.Instance.ChangeScreen(new TitleScreen(graphicsDevice, game));
            }
        }
    }
}
