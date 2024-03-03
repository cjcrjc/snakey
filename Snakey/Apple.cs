using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snakey
{
    class Apple
    {
        Random rnd = new Random();
        public int screensizeWidth, screensizeHeight;

        public Vector2 pos = new Vector2(3, 7);
        public Texture2D texture;

        public void Move()
        {
            pos = new Vector2(rnd.Next(screensizeWidth), rnd.Next(screensizeHeight));
        }

        public void Reset()
        {
            pos = new Vector2(3, 7);
        }
    }
}
