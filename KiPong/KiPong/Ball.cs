namespace KiPong
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Media;

    public class Ball
    {
        private bool isVisible;
        private Vector2 position, resetPos;
        private double direction;
        private Texture2D texture;
        private Rectangle size;
        private float speed, baseSpeed, increaseSpeed, scale;
        private Random rand;
        private SoundEffect test;
        private Game1 game;

        public Ball(Game1 g, Difficulty d)
        {
            // creer la balle
            setSpeed(d);
            game = g;
            int width = g.ScreenWidth / 30;
            texture = g.Content.Load<Texture2D>("balle");
            scale = (float) width / (float) texture.Width;
            size = new Rectangle(0, 0, width, width);
            resetPos = new Vector2(g.ScreenWidth / 2, g.ScreenHeight / 2);
            position = resetPos;
            direction = 0;
            rand = new Random();
            isVisible = false;
            test = g.Content.Load<SoundEffect>("WallHit");
        }

        private void setSpeed(Difficulty d)
        {
            switch (d)
            {
                case Difficulty.EASY:
                    baseSpeed = 8f;
                    increaseSpeed = 1f;
                    break;
                case Difficulty.MEDIUM:
                    baseSpeed = 10f;
                    increaseSpeed = 2f;
                    break;
                default:
                    baseSpeed = 12f;
                    increaseSpeed = 4f;
                    break;
            }
            speed = baseSpeed;
        }

        private void CheckWallHit()
        {
            while (direction > 2 * Math.PI) direction -= 2 * Math.PI;
            while (direction < 0) direction += 2 * Math.PI;
            if ((position.Y <= 0 && direction > Math.PI) || 
                (position.Y >= (game.ScreenHeight - size.Height) && direction < Math.PI) )
            {
                direction = 2 * Math.PI - direction;
                test.Play();
            }
        }

        public Rectangle GetSize()
        {
            return size;
        }

        public double GetDirection()
        {
            return direction;
        }

        public void Stop()
        {
            isVisible = false;
            speed = 0;
        }

        public void IncreaseSpeed()
        {
            speed += increaseSpeed;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetCenter()
        {
            return new Vector2(position.X + size.Width / 2, position.Y + size.Height / 2);
        }

        public void Reset(Side side)
        {
            if (side == Side.LEFT) direction = 0;
            else direction = Math.PI;
            position = resetPos;
            isVisible = true;
            speed = baseSpeed;
            if (rand.Next(2) == 0)
            {
                direction += MathHelper.ToRadians(rand.Next(30));
            }
            else
            {
                direction -= MathHelper.ToRadians(rand.Next(30));
            }
        }

        public void BatHit(int block)
        {
            if (direction > Math.PI * 1.5f || direction < Math.PI * 0.5f)
            {
                switch (block)
                {
                    case 1:
                        direction = MathHelper.ToRadians(220);
                        break;
                    case 2:
                        direction = MathHelper.ToRadians(215);
                        break;
                    case 3:
                        direction = MathHelper.ToRadians(200);
                        break;
                    case 4:
                        direction = MathHelper.ToRadians(195);
                        break;
                    case 5:
                        direction = MathHelper.ToRadians(180);
                        break;
                    case 6:
                        direction = MathHelper.ToRadians(180);
                        break;
                    case 7:
                        direction = MathHelper.ToRadians(165);
                        break;
                    case 8:
                        direction = MathHelper.ToRadians(130);
                        break;
                    case 9:
                        direction = MathHelper.ToRadians(115);
                        break;
                    case 10:
                        direction = MathHelper.ToRadians(110);
                        break;
                }                
            }
            else
            {
                switch (block)
                {
                    case 1:
                        direction = MathHelper.ToRadians(290);
                        break;
                    case 2:
                        direction = MathHelper.ToRadians(295);
                        break;
                    case 3:
                        direction = MathHelper.ToRadians(310);
                        break;
                    case 4:
                        direction = MathHelper.ToRadians(345);
                        break;
                    case 5:
                        direction = MathHelper.ToRadians(0);
                        break;
                    case 6:
                        direction = MathHelper.ToRadians(0);
                        break;
                    case 7:
                        direction = MathHelper.ToRadians(15);
                        break;
                    case 8:
                        direction = MathHelper.ToRadians(50);
                        break;
                    case 9:
                        direction = MathHelper.ToRadians(65);
                        break;
                    case 10:
                        direction = MathHelper.ToRadians(70);
                        break;
                }
            }
            if (rand.Next(2) == 0)
            {
                direction += MathHelper.ToRadians(rand.Next(3));
            }
            else
            {
                direction -= MathHelper.ToRadians(rand.Next(3));
            }
        }

        /// <summary>
        /// Met à jour la position de la balle par rapport à sa vitesse
        /// </summary>
        public void Update()
        {
            size.X = (int)position.X;
            size.Y = (int)position.Y;
            position.X += speed * (float)Math.Cos(direction);
            position.Y += speed * (float)Math.Sin(direction);
            CheckWallHit();
        }

        /// <summary>
        /// Dessine la balle
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch)
        {
            if (isVisible)
            {
                batch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
    }
}
