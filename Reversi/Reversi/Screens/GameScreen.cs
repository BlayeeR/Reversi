using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Effects;
using Reversi.Events;
using Reversi.Managers;
using Reversi.Models;
using Reversi.Sprites;
using static System.Net.Mime.MediaTypeNames;

namespace Reversi.Screens
{
    public class GameScreen : GameState
    {
        private TileManager tileManager;
        private Basic2D backgroundImage;
        private bool gameEnded = false, singleplayer, paused = false;
        private Text2D[] texts = new Text2D[2];
        private Text2D creditsText;
        private ScoreManager scoreManager;
        private GaussianBlur gaussianBlur;
        public GameScreen(GraphicsDevice graphicsDevice, Game game, bool singleplayer = true) : base(graphicsDevice, game)
        {
            this.singleplayer = singleplayer;
            backgroundImage = new Basic2D("Game/BackgroundImage", new Vector2(GameStateManager.Instance.Dimensions.X / 2, GameStateManager.Instance.Dimensions.Y / 2), GameStateManager.Instance.Dimensions);
            texts[0] = new Text2D(new Vector2(430, 350), "", "MenuFont", new Color(122, 54, 6), true, false);
            texts[1] = new Text2D(new Vector2(430, 400), "", "MenuFont", new Color(122, 54, 6), true, false);
            creditsText = new Text2D(new Vector2(225, 850), "Created by\nJakub Olech", "TitleScreen/CreditsFont", Color.Black);
        }

        private void TileManager_OnGameEnded(object sender, EventArgs e)
        {
            GameEndedEventArgs gameEndedEventArgs = (e as GameEndedEventArgs);
            switch (gameEndedEventArgs.Result)
            {
                case -1:
                    {
                        if (singleplayer)
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
                        if (singleplayer)
                            texts[0].Text = "You lose!";
                        else
                            texts[0].Text = "White wins!";
                        break;
                    }
            }
            if (scoreManager.Scores.Count != 0)
            {
                if (gameEndedEventArgs.Scores[0].Value > scoreManager.Scores.LastOrDefault().Value && gameEndedEventArgs.Scores[1].Value > scoreManager.Scores.LastOrDefault().Value)
                {
                    texts[1].Text += $"New high scores: {gameEndedEventArgs.Scores[0].Value}, {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[0].Name = DateTime.Now.ToString();
                    gameEndedEventArgs.Scores[1].Name = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[0]);
                    scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
                else if (gameEndedEventArgs.Scores[0].Value > scoreManager.Scores.LastOrDefault().Value && gameEndedEventArgs.Scores[0].Value != 0)
                {
                    texts[1].Text += $"New high score: {gameEndedEventArgs.Scores[0].Value}";
                    gameEndedEventArgs.Scores[0].Name = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[0]);
                }
                else if (gameEndedEventArgs.Scores[1].Value > scoreManager.Scores.LastOrDefault().Value && gameEndedEventArgs.Scores[1].Value != 0)
                {
                    texts[1].Text += $"New high score: {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[1].Name = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
            }
            else
            {
                if (gameEndedEventArgs.Scores[0].Value != 0 && gameEndedEventArgs.Scores[1].Value != 0)
                {
                    texts[1].Text += $"New high scores: {gameEndedEventArgs.Scores[0].Value}, {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[0].Name = DateTime.Now.ToString();
                    gameEndedEventArgs.Scores[1].Name = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[0]);
                    scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
                else if (gameEndedEventArgs.Scores[0].Value != 0)
                {
                    texts[1].Text += $"New high score: {gameEndedEventArgs.Scores[0].Value}";
                    gameEndedEventArgs.Scores[0].Name = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[0]);
                }
                else if (gameEndedEventArgs.Scores[1].Value != 0)
                {
                    texts[1].Text += $"New high score: {gameEndedEventArgs.Scores[1].Value}";
                    gameEndedEventArgs.Scores[1].Name = DateTime.Now.ToString();
                    scoreManager.Add(gameEndedEventArgs.Scores[1]);
                }
            }
            ScoreManager.Save(scoreManager);
            gameEnded = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            backgroundImage.Draw(spriteBatch);
            if (!gameEnded)
                tileManager.Draw(spriteBatch);
            else
            {
                foreach (Text2D text in texts)
                    text.Draw(spriteBatch);
                creditsText.Draw(spriteBatch);
            }
            spriteBatch.End();
            if (paused)
            {
                int[] backBuffer = new int[graphicsDevice.Viewport.Width * graphicsDevice.Viewport.Height];
                graphicsDevice.GetBackBufferData(backBuffer);
                Texture2D tempTexture = new Texture2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat);
                tempTexture.SetData(backBuffer);
                gaussianBlur.Input = tempTexture;
                gaussianBlur.Draw();
                graphicsDevice.GetBackBufferData(backBuffer);
                tempTexture = new Texture2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, false, graphicsDevice.PresentationParameters.BackBufferFormat);
                tempTexture.SetData(backBuffer);
                paused = false;
                GameStateManager.Instance.AddScreen(new PauseScreen(graphicsDevice, game, tempTexture));

            }
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            tileManager = new TileManager(new Vector2(90), new Vector2(667), singleplayer, content);
            tileManager.LoadContent(content);
            tileManager.OnGameEnded += TileManager_OnGameEnded;
            scoreManager = ScoreManager.Load();
            creditsText.LoadContent(content);
            foreach (Text2D text in texts)
                text.LoadContent(content);
            backgroundImage.LoadContent(content);
            gaussianBlur = new GaussianBlur(graphicsDevice, content, 5);
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.KeyPressed(Keys.Escape) && !gameEnded)
            {
                paused = true;
                /*graphicsDevice.SetRenderTarget(renderTarget);
                graphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

                graphicsDevice.Present();
                graphicsDevice.SetRenderTarget(null);
                GameStateManager.Instance.AddScreen(new PauseScreen(graphicsDevice, game));*/
            }
            backgroundImage.Update(gameTime);
            if (!gameEnded)
                tileManager.Update(gameTime);
            else
            {
                creditsText.Update(gameTime);
                foreach(Text2D text in texts)
                    text.Update(gameTime);
                if (InputManager.Instance.KeyPressed(Keys.Enter))
                    GameStateManager.Instance.ChangeScreen(new TitleScreen(graphicsDevice, game));
            }
        }
    }
}
