using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class GameScreen : GameState.GameState
    {
        public TileManager diskManager;
        private Basic2D backgroundImage;
        public GameScreen(GraphicsDevice graphicsDevice, Game game, bool singlePlayer = true) : base(graphicsDevice, game)
        {
            diskManager = new TileManager(new Vector2(90), new Vector2(667), singlePlayer);
            backgroundImage = new Basic2D("Game/BackgroundImage", new Vector2(GameState.GameStateManager.Instance.Dimensions.X/2, GameState.GameStateManager.Instance.Dimensions.Y/2), GameState.GameStateManager.Instance.Dimensions);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.AliceBlue);
            spriteBatch.Begin();
            backgroundImage.Draw(spriteBatch);
            diskManager.Draw(spriteBatch);
            
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
                GameState.GameStateManager.Instance.AddScreen(new PauseScreen(_graphicsDevice, _game));
            backgroundImage.Update(gameTime);
            diskManager.Update(gameTime);
        }
    }
}
