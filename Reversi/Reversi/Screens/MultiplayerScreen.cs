using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class MultiplayerScreen : GameState.GameState
    {
        public DiskManager diskManager;
        public MultiplayerScreen(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            diskManager = new DiskManager(new Vector2(50), new Vector2(500));
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.AliceBlue);
            spriteBatch.Begin();
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
            diskManager.Update(gameTime);
        }
    }
}
