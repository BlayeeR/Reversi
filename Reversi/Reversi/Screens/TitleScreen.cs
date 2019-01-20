using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.GameState;
using Reversi.Menu;
using Reversi.Menus;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class TitleScreen : GameState.GameState
    {
        public MenuManager menuManager = new MenuManager();
        public TitleMenu menu;
        public TitleScreen(GraphicsDevice graphicsDevice, Game game) : base(graphicsDevice, game)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            menu = new TitleMenu(_graphicsDevice, _game);
            menuManager.AddMenu(menu);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            menuManager.Draw(spriteBatch);
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
            menuManager.Update(gameTime);
        }
    }
}
