using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class DiskManager
    {
        List<List<Disk>> disks;
        private Vector2 _gameBoardPosition, _gameBoardDimensions;
        private int boardSize = 8;
        public DiskManager(Vector2 gameBoardPosition, Vector2 gameBoardDimensions)
        {
            _gameBoardPosition = gameBoardPosition;
            _gameBoardDimensions = gameBoardDimensions;
            disks = new List<List<Disk>>();
            Vector2 diskSize = new Vector2(gameBoardDimensions.X / boardSize * 0.8f);
            for(int i =0; i < boardSize; i++)
            {
                List<Disk> tempDisks = new List<Disk>();
                for (int j = 0; j < boardSize; j++)
                {
                    Disk disk = new Disk(true, new Vector2(_gameBoardPosition.X + gameBoardDimensions.X * 0.03f + gameBoardDimensions.X * 0.02f * j + j * diskSize.X + diskSize.X / 2, _gameBoardPosition.Y + gameBoardDimensions.Y * 0.03f + gameBoardDimensions.Y * 0.02f * i + i * diskSize.Y + diskSize.Y / 2), new Vector2(gameBoardDimensions.X*0.8f/boardSize, gameBoardDimensions.Y*0.8f/boardSize),true);
                    tempDisks.Add(disk);
                }
                disks.Add(tempDisks);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (List<Disk> diski in disks)
                foreach (Disk disk in diski)
                    disk.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            foreach (List<Disk> diski in disks)
                foreach (Disk disk in diski)
                    disk.Update(gameTime);
        }
    }
}
