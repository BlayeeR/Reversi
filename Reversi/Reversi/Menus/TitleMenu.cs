using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reversi.Menu;
using Reversi.Sprites;

namespace Reversi.Menus
{
    public class TitleMenu : Menu.Menu
    {
        public TitleMenu() : base()
        {
            Items.Add(new Button2D("TitleScreen/Button", new Vector2(300, 300), "Singleplayer", "TitleScreen/DefaultFont"));
            Items.Add(new Button2D("TitleScreen/Button", new Vector2(300, 300), "Multiplayer", "TitleScreen/DefaultFont"));
            Items.Add(new Button2D("TitleScreen/Button", new Vector2(300, 300), "Leaderboards", "TitleScreen/DefaultFont"));
            Items.Add(new Button2D("TitleScreen/Button", new Vector2(300, 300), "Exit", "TitleScreen/DefaultFont"));
            Axis = "Y";
        }

    }
}
