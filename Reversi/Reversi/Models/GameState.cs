using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Reversi.Interfaces;
using System;
using System.Xml.Serialization;

namespace Reversi.Models
{
    public abstract class GameState : IComponent
    {
        protected GraphicsDevice graphicsDevice;
        protected Game game;
        [XmlIgnore]
        public Type Type;
        public GameState(GraphicsDevice graphicsDevice, Game game)
        {
            this.graphicsDevice = graphicsDevice;
            this.game = game;
            Type = this.GetType();
        }
        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
