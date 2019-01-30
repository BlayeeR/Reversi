using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Effects
{
    public class RenderCapture
    {
        RenderTarget2D renderTarget;
        GraphicsDevice graphicsDevice;

        public RenderCapture(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            renderTarget = new RenderTarget2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.Depth24);
        }

        //Begins capturing from the graphics device
        public void Begin()
        {
            graphicsDevice.SetRenderTarget(renderTarget);
        }

        //Stop capturing
        public void End()
        {
            graphicsDevice.SetRenderTarget(null);
        }

        //Returns what was captured
        public Texture2D GetTexture()
        {
            return renderTarget;
        }
    }
}
