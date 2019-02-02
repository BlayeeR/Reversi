using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Managers;
using Reversi.Menus;
using Reversi.Models;
using Reversi.Sprites;

namespace Reversi.Screens
{
    public class PauseScreen : GameState
    {
        private MenuManager menuManager = new MenuManager();
        private PauseMenu menu;
        private Basic2D backgroundImage;
        public PauseScreen(GraphicsDevice graphicsDevice, Game game, Texture2D backgroundTexture) : base(graphicsDevice, game)
        {
            base.game = game;
            base.graphicsDevice = graphicsDevice;
            menu = new PauseMenu(base.graphicsDevice, base.game);
            this.backgroundImage = new Basic2D(backgroundTexture, new Vector2(GameStateManager.Instance.Dimensions.X / 2, GameStateManager.Instance.Dimensions.Y / 2), GameStateManager.Instance.Dimensions);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            backgroundImage.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void LoadContent(ContentManager content)
        {
            backgroundImage.LoadContent(content);
            menuManager.LoadContent(content);
            menuManager.AddMenu(menu);
        }

        public override void UnloadContent()
        {
            menu.UnloadContent();
            backgroundImage.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            backgroundImage.Update(gameTime);
            menuManager.Update(gameTime);
        }
    }
}
