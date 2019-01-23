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
using Reversi.Managers;
using Reversi.Models;

namespace Reversi.Screens
{
    public class SplashScreen : GameState
    {
        Basic2D SplashImage;

        public SplashScreen(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            _graphicsDevice = graphicsDevice;
            SplashImage = new Basic2D("SplashScreen/SplashImage", new Vector2(100, 120));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            SplashImage.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager content)
        {
            SplashImage.LoadContent(content);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.KeyPressed(Keys.Enter))
            {
                GameStateManager.Instance.ChangeScreen(new TitleScreen(_graphicsDevice, _game));
            }
        }
    }
}
