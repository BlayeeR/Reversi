using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Reversi.GameStates
{
    public class TitleScreen : GameState
    {
        public TitleScreen(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
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

        }
    }
}
