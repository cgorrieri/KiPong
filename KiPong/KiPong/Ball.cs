namespace KiPong
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Media;
    using System.Collections.Generic;

    public class Ball
    {
        private bool isVisible;
        //private Vector2 position;
        private Vector2 resetPos;
        private double direction;
        private Texture2D texture;
        private Rectangle size;
        private float speed, baseSpeed, increaseSpeed, scale;
        private Random rand;
        private SoundEffect WallHitSong, BatHitSong;
        private Game1 game;

        private int maxSizeListePosition;

        //Liste de position
        List<Vector2> positions;

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
            //position = resetPos;

            maxSizeListePosition = 5;
            positions = new List<Vector2>(maxSizeListePosition);
            positions.Add(resetPos);
            positions.Add(resetPos);
            positions.Add(resetPos);
            positions.Add(resetPos);
            positions.Add(resetPos);
            positions.Add(resetPos);

            direction = 0;
            rand = new Random();
            isVisible = false;
            WallHitSong = g.Content.Load<SoundEffect>("WallHit");
            BatHitSong = g.Content.Load<SoundEffect>("BatHit");
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
            if ((positions[0].Y <= 0 && direction > Math.PI) || 
                (positions[0].Y >= (game.ScreenHeight - size.Height) && direction < Math.PI) )
            {
                direction = 2 * Math.PI - direction;
                WallHitSong.Play();
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
            return positions[0];
        }

        public Vector2 GetCenter()
        {
            return new Vector2(positions[0].X + size.Width / 2, positions[0].Y + size.Height / 2);
        }

        public void Reset(Side side)
        {
            if (side == Side.LEFT) direction = 0;
            else direction = Math.PI;
            positions[0] = resetPos;
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
            BatHitSong.Play();
        }

        /// <summary>
        /// Met à jour la position de la balle par rapport à sa vitesse
        /// </summary>
        public void Update()
        {
            size.X = (int)positions[0].X;
            size.Y = (int)positions[0].Y;

            Vector2 vector = positions[0];

            for (int i = maxSizeListePosition; i > 0; i--)
                positions[i] = positions[i - 1];

            vector.X += speed * (float)Math.Cos(direction);
            vector.Y += speed * (float)Math.Sin(direction);
            positions[0] = vector;

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
                int value = 50;
              
                for (int i = maxSizeListePosition; i > 0; i--)
                {
                    batch.Draw(texture, positions[i], null, new Color(value, value, value, 200), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    value += 25;
                }

                batch.Draw(texture, positions[0], null, new Color(255, 255, 255, 200), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
    }
}
