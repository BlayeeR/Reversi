using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.GameState;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class TitleScreen : GameState.GameState
    {
        public Button2D exitButton;
        public TitleScreen(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            exitButton = new Button2D("TitleScreen/Button", new Vector2(300, 300), "Exit", "TitleScreen/DefaultFont");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            exitButton.Draw(spriteBatch);
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
            exitButton.Update();
        }
    }
}
