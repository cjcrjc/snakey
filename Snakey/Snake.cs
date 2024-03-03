using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Snakey
{
    enum Direction {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    enum Direction_Change
    {
        Vertical = 0,
        Horizontal = 1,
        TopLeft = 2,
        TopRight = 3,
        BottomLeft = 4,
        BottomRight = 5
    }
    class Snake
    {
        public Texture2D[] headTextures = new Texture2D[4];
        public Texture2D[] bodyTextures = new Texture2D[6];
        public Texture2D[] tailTextures = new Texture2D[4];
        public Direction dir = Direction.Down;
        public Direction prev_dir;
        public Direction_Change dir_change = Direction_Change.Vertical;
        public Vector2 pos, prev_pos;
        public int screensizeWidth, screensizeHeight;
        public int bodyLength = 1;
        public Snake(Vector2 startPos, Direction prevDir, int screenWidth, int screenHeight)
        {
            pos = startPos;
            prev_dir = prevDir;
            screensizeHeight = screenHeight;
            screensizeWidth = screenWidth;
        }
        public void Move()
        {
            prev_dir = dir;
            prev_pos = pos;
            switch (dir)
            {
                case Direction.Up:
                        pos.Y--;
                    break;
                case Direction.Down:
                        pos.Y++;
                    break;
                case Direction.Left:
                        pos.X--;
                    break;
                case Direction.Right:
                        pos.X++;
                    break;
            }
        }

        public void Follow(Direction next_dir)
        {
            dir = next_dir;
        }

        public void GetDirChange()
        {
            switch (dir)
                {
                    case Direction.Up:
                        if (prev_dir == Direction.Right)
                            dir_change = Direction_Change.TopLeft;
                        else if (prev_dir == Direction.Left)
                            dir_change = Direction_Change.TopRight;
                        else
                            dir_change = Direction_Change.Vertical;
                        break;
                    case Direction.Down:
                        if (prev_dir == Direction.Right)
                            dir_change = Direction_Change.BottomLeft;
                        else if (prev_dir == Direction.Left)
                            dir_change = Direction_Change.BottomRight;
                        else
                            dir_change = Direction_Change.Vertical;
                        break;
                    case Direction.Left:
                        if (prev_dir == Direction.Down)
                            dir_change = Direction_Change.TopLeft;
                        else if (prev_dir == Direction.Up)
                            dir_change = Direction_Change.BottomLeft;
                        else
                            dir_change = Direction_Change.Horizontal;
                        break;
                    case Direction.Right:
                        if (prev_dir == Direction.Down)
                            dir_change = Direction_Change.TopRight;
                        else if (prev_dir == Direction.Up)
                            dir_change = Direction_Change.BottomRight;
                        else
                            dir_change = Direction_Change.Horizontal;
                        break;
            }
            prev_dir = dir;
        }
        public Texture2D GetBodyTexture()
        {
            Texture2D texture = bodyTextures[(int)dir_change];
            return texture;
        }
        public Texture2D GetHeadTexture()
        {
            Texture2D texture = headTextures[(int)prev_dir];
            return texture;
        }
        public Texture2D GetTailTexture()
        {
            Texture2D texture = tailTextures[(int)prev_dir];
            return texture;
        }

    }
}
