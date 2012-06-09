namespace KiPong
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Media;
    using System.Collections.Generic;

    public class Ball : PongItem
    {
        public static String BallColor;
        public static bool ColorChanged;

        /// <summary>
        /// Obtien le centre de la balle
        /// </summary>
        public Vector2 Center
        {
            get { return new Vector2(position.X + size.Width / 2, position.Y + size.Height / 2); }
        }
        // Si la balle est visible ou non
        private bool isVisible;
        // La position de départ de la balle
        private Vector2 resetPos;
        // Direction de la balle
        private double direction;
        /// <summary>
        /// Obtien la direction de la balle
        /// </summary>
        public double Direction
        {
            get { return direction; }
        }
        // Vitesse de la balle
        private float speed, baseSpeed, increaseSpeed, scale;
        private Random rand;
        // Différents son que la balle fait
        private SoundEffect WallHitSong, BatHitSong;
        // Liste de positions de la trainée
        private int maxSizeListePosition;
        List<Vector2> traineePosition;

        public Ball(KiPongGame g, Difficulty d)
            : base(g)
        {
            // creer la balle
            setSpeed(d);
            direction = 0;
            int width = g.ScreenWidth / 20;
            changeBallColor();
            scale = (float)width / (float)texture.Width;
            size = new Rectangle(0, 0, width, width);
            resetPos = new Vector2(g.ScreenWidth / 2, g.ScreenHeight / 2);

            // Création de la trainée
            position = resetPos;
            maxSizeListePosition = 4;
            traineePosition = new List<Vector2>(maxSizeListePosition) { resetPos, resetPos, resetPos, resetPos};

            // Initialisation des autres composants
            rand = new Random();
            isVisible = false;
            WallHitSong = g.Content.Load<SoundEffect>("WallHit");
            BatHitSong = g.Content.Load<SoundEffect>("BatHit");
        }

        /// <summary>
        /// Met en place la vitesse et l'augmentation de vitesse en fonction de la difficultée
        /// </summary>
        /// <param name="d">Difficultée du jeu</param>
        private void setSpeed(Difficulty d)
        {
            switch (d)
            {
                case Difficulty.EASY:
                    baseSpeed = 7f;
                    increaseSpeed = 1f;
                    break;
                case Difficulty.MEDIUM:
                    baseSpeed = 8f;
                    increaseSpeed = 1.5f;
                    break;
                default:
                    baseSpeed = 9f;
                    increaseSpeed = 2f;
                    break;
            }
            speed = baseSpeed;
        }

        /// <summary>
        /// Regarde si la balle tape un mur, si oui elle change de direction
        /// </summary>
        private void CheckWallHit()
        {
            while (direction > 2 * Math.PI) direction -= 2 * Math.PI;
            while (direction < 0) direction += 2 * Math.PI;
            if ((position.Y <= 0 && direction > Math.PI) || 
                (position.Y >= (game.ScreenHeight - size.Height) && direction < Math.PI) )
            {
                direction = 2 * Math.PI - direction;
                WallHitSong.Play();
            }
        }

        /// <summary>
        /// Arète la balle et la rend invisible
        /// </summary>
        public void Stop()
        {
            isVisible = false;
            speed = 0;
        }

        /// <summary>
        /// Augmente la vitesse de la balle
        /// </summary>
        public void IncreaseSpeed()
        {
            speed += increaseSpeed;
        }

        /// <summary>
        /// Remet la balle au centre de l'écran et réinitialise sa vitesse
        /// </summary>
        /// <param name="side">Le coté qui a marqué le point</param>
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

        /// <summary>
        /// Change la direction de la balle quand elle touche une batte
        /// </summary>
        /// <param name="block">Le block de la batte sur lequel la balle a tapée</param>
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

        public override void Update()
        {
            if (ColorChanged)
            {
                changeBallColor();
                ColorChanged = false;
            }
            for (int i = maxSizeListePosition - 1; i > 0; i--)
                traineePosition[i] = traineePosition[i - 1];

            position.X += speed * (float)Math.Cos(direction);
            position.Y += speed * (float)Math.Sin(direction);

            traineePosition[0] = position;
            
            CheckWallHit();
        }

        public override void Draw()
        {
            if (isVisible)
            {
                // Valeur de fin du alpha
                int value = 50;
                // On dessine la trainée
                for (int i = maxSizeListePosition-1; i >= 0; i--)
                {
                    game.SpriteBatch.Draw(texture, traineePosition[i], null, new Color(value, value, value, 200), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    value += 25;
                }
                // On dessine la balle
                game.SpriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        public void changeBallColor()
        {
            texture = game.Content.Load<Texture2D>("balle" + BallColor);
        }
    }
}
