using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Effects
{
    public class PostProcessor
    {
        //pixel shader
        public Effect Effect { get; protected set; }
        //Texture to process
        public Texture2D Input { get; set; }

        protected GraphicsDevice graphicsDevice;
        protected static SpriteBatch spriteBatch;

        
        public PostProcessor(Effect effect, GraphicsDevice graphicsDevice)
        {
            this.Effect = effect;
            if(spriteBatch == null)
               spriteBatch = new SpriteBatch(graphicsDevice);
            this.graphicsDevice = graphicsDevice;
        }
        //Draws the input texture using the pixel shader postprocessor
        public virtual void Draw()
        {
            //Set effect parameters if necessary
            if(Effect.Parameters["ScreenWidth"] != null)
                Effect.Parameters["ScreenWidth"].SetValue(graphicsDevice.Viewport.Width);
            if(Effect.Parameters["ScreenHeight"] != null)
                Effect.Parameters["ScreenHeight"].SetValue(graphicsDevice.Viewport.Height);

            //Initialize the spritebatch and effect
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

            Effect.Techniques[0].Passes[0].Apply();
            //Draw the input texture
            spriteBatch.Draw(Input, Vector2.Zero, Color.White);
            
            //End the spritebatch and effect
            spriteBatch.End();

            //Clean up render states changed by the spritebatch
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.BlendState = BlendState.Opaque;
        }
    }
}
