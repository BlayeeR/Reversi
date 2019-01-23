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
        private Basic2D splashImage;

        public SplashScreen(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            base.graphicsDevice = graphicsDevice;
            splashImage = new Basic2D("SplashScreen/SplashImage", new Vector2(100, 120));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            splashImage.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager content)
        {
            splashImage.LoadContent(content);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.KeyPressed(Keys.Enter))
            {
                GameStateManager.Instance.ChangeScreen(new TitleScreen(graphicsDevice, game));
            }
        }
    }
}
