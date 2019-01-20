using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml.Serialization;

namespace Reversi.GameState
{
    public abstract class GameState : IGameState
    {
        protected GraphicsDevice _graphicsDevice;
        protected Game _game;
        [XmlIgnore]
        public Type Type;
        public GameState(GraphicsDevice graphicsDevice, Game game)
        {
            _graphicsDevice = graphicsDevice;
            _game = game;
            Type = this.GetType();
        }
        public abstract void Initialize();
        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
