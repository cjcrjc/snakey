using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snakey
{
    public class Game1 : Game
    {
        public TimeSpan moveTick, directionTick;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public int score = 0;
        public int highScore = 0;
        public double speed = 0.15;
        public int screenWidth, screenHeight;
        Boolean canMove = true;
        Boolean gameOn = false;

        Texture2D snakeBody,snakeTail;
        SpriteFont font, font2;
        List<Snake> snake = new List<Snake>();
        Apple apple = new Apple();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            screenWidth = Window.ClientBounds.Width/40;
            screenHeight = Window.ClientBounds.Height/40;
            apple.screensizeWidth = Window.ClientBounds.Width / 40;
            apple.screensizeHeight = Window.ClientBounds.Height / 40;
        }

        protected override void Initialize()
        {
            moveTick = TimeSpan.Zero;
            directionTick = TimeSpan.Zero;
            snake.Add(new Snake(new Vector2(3, 3), Direction.Down, screenWidth, screenHeight));
            snake.Add(new Snake(new Vector2(3, 2), Direction.Down, screenWidth, screenHeight));
            snake.Add(new Snake(new Vector2(3, 1), Direction.Down, screenWidth, screenHeight));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            apple.texture = Content.Load<Texture2D>("assets/apple");

            snake[0].headTextures[(int)Direction.Up] = Content.Load<Texture2D>("assets/head_up");
            snake[0].headTextures[(int)Direction.Down] = Content.Load<Texture2D>("assets/head_down");
            snake[0].headTextures[(int)Direction.Left] = Content.Load<Texture2D>("assets/head_left");
            snake[0].headTextures[(int)Direction.Right] = Content.Load<Texture2D>("assets/head_right");
            for (int i = 1; i < snake.Count; i++)
            {
                snake[i].bodyTextures[(int)Direction_Change.Vertical] = Content.Load<Texture2D>("assets/body_vertical");
                snake[i].bodyTextures[(int)Direction_Change.Horizontal] = Content.Load<Texture2D>("assets/body_horizontal");
                snake[i].bodyTextures[(int)Direction_Change.BottomLeft] = Content.Load<Texture2D>("assets/body_bottomleft");
                snake[i].bodyTextures[(int)Direction_Change.BottomRight] = Content.Load<Texture2D>("assets/body_bottomright");
                snake[i].bodyTextures[(int)Direction_Change.TopLeft] = Content.Load<Texture2D>("assets/body_topleft");
                snake[i].bodyTextures[(int)Direction_Change.TopRight] = Content.Load<Texture2D>("assets/body_topright");
                snake[i].tailTextures[(int)Direction.Down] = Content.Load<Texture2D>("assets/tail_up");
                snake[i].tailTextures[(int)Direction.Up] = Content.Load<Texture2D>("assets/tail_down");
                snake[i].tailTextures[(int)Direction.Right] = Content.Load<Texture2D>("assets/tail_left");
                snake[i].tailTextures[(int)Direction.Left] = Content.Load<Texture2D>("assets/tail_right");
            }

            font = Content.Load<SpriteFont>("File");
            font2 = Content.Load<SpriteFont>("File2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!gameOn && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                gameOn = true;
                
                snake.Clear();
                snake.Add(new Snake(new Vector2(3, 3), Direction.Down, screenWidth, screenHeight));
                snake.Add(new Snake(new Vector2(3, 2), Direction.Down, screenWidth, screenHeight));
                snake.Add(new Snake(new Vector2(3, 1), Direction.Down, screenWidth, screenHeight));
                snake[0].headTextures[(int)Direction.Up] = Content.Load<Texture2D>("assets/head_up");
                snake[0].headTextures[(int)Direction.Down] = Content.Load<Texture2D>("assets/head_down");
                snake[0].headTextures[(int)Direction.Left] = Content.Load<Texture2D>("assets/head_left");
                snake[0].headTextures[(int)Direction.Right] = Content.Load<Texture2D>("assets/head_right");
                for (int i =0; i < snake.Count; i++)
                {
                    snake[i].bodyTextures[(int)Direction_Change.Vertical] = Content.Load<Texture2D>("assets/body_vertical");
                    snake[i].bodyTextures[(int)Direction_Change.Horizontal] = Content.Load<Texture2D>("assets/body_horizontal");
                    snake[i].bodyTextures[(int)Direction_Change.BottomLeft] = Content.Load<Texture2D>("assets/body_bottomleft");
                    snake[i].bodyTextures[(int)Direction_Change.BottomRight] = Content.Load<Texture2D>("assets/body_bottomright");
                    snake[i].bodyTextures[(int)Direction_Change.TopLeft] = Content.Load<Texture2D>("assets/body_topleft");
                    snake[i].bodyTextures[(int)Direction_Change.TopRight] = Content.Load<Texture2D>("assets/body_topright");
                    snake[i].tailTextures[(int)Direction.Down] = Content.Load<Texture2D>("assets/tail_up");
                    snake[i].tailTextures[(int)Direction.Up] = Content.Load<Texture2D>("assets/tail_down");
                    snake[i].tailTextures[(int)Direction.Right] = Content.Load<Texture2D>("assets/tail_left");
                    snake[i].tailTextures[(int)Direction.Left] = Content.Load<Texture2D>("assets/tail_right");
                }
                apple.Reset();
                score = 0;
            }
            if (gameOn)
            {

                if ((gameTime.TotalGameTime - moveTick).TotalSeconds >= speed)
                {
                    moveTick = gameTime.TotalGameTime;
                    snake[0].Move();
                    for (int i = 1; i < snake.Count; i++)
                    {
                        snake[i].GetDirChange();
                        snake[i].prev_pos = snake[i].pos;
                        snake[i].pos = snake[i - 1].prev_pos;
                    }
                }
                if ((gameTime.TotalGameTime - directionTick).TotalSeconds >= speed)
                    canMove = true;

                if ((Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) && snake[0].dir != Direction.Right && canMove)
                {
                    snake[0].dir = Direction.Left;
                    directionTick = gameTime.TotalGameTime;
                    canMove = false;
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) && snake[0].dir != Direction.Left && canMove)
                {
                    snake[0].dir = Direction.Right;
                    directionTick = gameTime.TotalGameTime;
                    canMove = false;
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && snake[0].dir != Direction.Down && canMove)
                {
                    snake[0].dir = Direction.Up;
                    directionTick = gameTime.TotalGameTime;
                    canMove = false;
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && snake[0].dir != Direction.Up && canMove)
                {
                    snake[0].dir = Direction.Down;
                    directionTick = gameTime.TotalGameTime;
                    canMove = false;
                }

                snake[1].dir = snake[0].dir;
                for (int i = 2; i < snake.Count; i++)
                {
                    snake[i].Follow(snake[i - 1].prev_dir);
                }

                if (apple.pos == snake[0].pos)
                {
                    score++;
                    snake.Add(new Snake(snake[snake.Count - 1].prev_pos, snake[snake.Count - 1].prev_dir, screenWidth, screenHeight));
                    snake[snake.Count - 1].bodyTextures[(int)Direction_Change.Vertical] = Content.Load<Texture2D>("assets/body_vertical");
                    snake[snake.Count - 1].bodyTextures[(int)Direction_Change.Horizontal] = Content.Load<Texture2D>("assets/body_horizontal");
                    snake[snake.Count - 1].bodyTextures[(int)Direction_Change.BottomLeft] = Content.Load<Texture2D>("assets/body_bottomleft");
                    snake[snake.Count - 1].bodyTextures[(int)Direction_Change.BottomRight] = Content.Load<Texture2D>("assets/body_bottomright");
                    snake[snake.Count - 1].bodyTextures[(int)Direction_Change.TopLeft] = Content.Load<Texture2D>("assets/body_topleft");
                    snake[snake.Count - 1].bodyTextures[(int)Direction_Change.TopRight] = Content.Load<Texture2D>("assets/body_topright");
                    snake[snake.Count - 1].tailTextures[(int)Direction.Down] = Content.Load<Texture2D>("assets/tail_up");
                    snake[snake.Count - 1].tailTextures[(int)Direction.Up] = Content.Load<Texture2D>("assets/tail_down");
                    snake[snake.Count - 1].tailTextures[(int)Direction.Right] = Content.Load<Texture2D>("assets/tail_left");
                    snake[snake.Count - 1].tailTextures[(int)Direction.Left] = Content.Load<Texture2D>("assets/tail_right");

                    apple.Move();
                }
                for (int i = 1; i < snake.Count - 1; i++)
                {
                    if (snake[i].pos == snake[0].pos)
                        gameOn = false;
                    if (snake[i].pos == apple.pos)
                        apple.Move();
                }

                for (int i = 0; i < snake.Count-1; i++)
                {
                    if (snake[i].pos.X < 0 || snake[i].pos.X > screenWidth-1 || snake[i].pos.Y < 0 || snake[i].pos.Y > screenHeight-1)
                        gameOn = false;
                }
                if (score > highScore)
                    highScore = score;
            }    
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(apple.texture, apple.pos*40, Color.White);
            
            //Drawing Snake Segments
            _spriteBatch.Draw(snake[0].GetHeadTexture(), snake[0].pos * 40, Color.White);
            for (int i = 1; i < snake.Count; i++)
            {
                if (i == snake.Count - 1)
                {
                    _spriteBatch.Draw(snake[i].GetTailTexture(), snake[i].pos * 40, Color.White);
                    break;
                }
                _spriteBatch.Draw(snake[i].GetBodyTexture(), snake[i].pos * 40, Color.White);
            }

            _spriteBatch.DrawString(font, $"Score:{score}", new Vector2(15, Window.ClientBounds.Height - 30), Color.Black);
            _spriteBatch.DrawString(font, $"High Score:{highScore}", new Vector2(100, Window.ClientBounds.Height - 30), Color.Black);
            if (!gameOn)
                _spriteBatch.DrawString(font2, $"Press Space To Start", new Vector2(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}