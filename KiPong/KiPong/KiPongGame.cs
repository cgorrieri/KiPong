using System;
using System.Collections.Generic;
using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KiPong
{
    public enum Difficulty { EASY, MEDIUM, HARD };

    public enum Side { LEFT, RIGHT, TOP, BOTTOM };

    /// <summary>
    /// Je jeu principale qui contient les menus et le jeu
    /// </summary>
    public class KiPongGame : Game
    {
        /* -- GAME ELEMENT -- */
        public static GameStates gamestate;
        private Menu PlayingMenu, ModeMenu, DifficultyMenu, PauseMenu, EndMenu;
        private Pong jeu;
        private bool IsOnePlayer, IsKinectMode;

        /* -- SCREEN -- */
        private KinectInput kinectInput;
        private KeyboardInput keyboardInput;
        private int screenWidth;
        /// <summary>
        /// Obtient la longueur de l'écran
        /// </summary>
        public int ScreenWidth { get { return screenWidth; } }
        private int screenHeight;
        /// <summary>
        /// Obtient la hauteur de l'écran
        /// </summary>
        public int ScreenHeight { get { return screenHeight; } }
        private Rectangle screen;
        /// <summary>
        /// Obtient la taille de l'écran
        /// </summary>
        public Rectangle ScreenSize { get { return screen; } }
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        /// <summary>
        /// Obtient l'outil de dessin du jeu
        /// </summary>
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }
        private SpriteFont font, fontTitle;
        /// <summary>
        /// Obtient la font du jeu
        /// </summary>
        public SpriteFont Font { get { return font; } }
        /// <summary>
        /// Obtient la font pour les titres du jeu
        /// </summary>
        public SpriteFont FontTitle { get { return fontTitle; } }

        /* -- SPLASH SCREEN -- */
        private Texture2D splashScreen;
        private TimeSpan splashScreenTimer;
        private Vector2 screenSplashPosition;

        /// <summary>
        /// Les différents états du jeu
        /// </summary>
        public enum GameStates
        {
            SplashScreen,
            ModeMenu,
            PlayingMenu,
            DifficultyMenu,
            Running,
            PauseMenu,
            EndMenu
        }

        public KiPongGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ColorManager.InitializeColors();
        }

        /// <summary>
        /// Initialise les éléments du jeu
        /// </summary>
        protected override void Initialize()
        {
            // récupère la taille exacte de l'ecran
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            keyboardInput = new KeyboardInput();
            keyboardInput.IsHoldable = false;
            ModeMenu = new MenuKeyboard(this, keyboardInput);
            SetMenu(ModeMenu, "Mode de jeu", "Choisis ton mode de jeu", new List<string>() { "Clavier", "Kinect", "Quitter" });
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            screen = new Rectangle(0, 0, screenWidth, screenHeight);

            IsKinectMode = false;
            splashScreenTimer = new TimeSpan(0, 0, 3);
            gamestate = GameStates.SplashScreen;

            base.Initialize();
        }

        /// <summary>
        /// Charge les éléments du jeu
        /// </summary>
        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("Font");
            fontTitle = Content.Load<SpriteFont>("FontTitle");
            splashScreen = Content.Load<Texture2D>("SplashScreen");
            screenSplashPosition = new Vector2((screenWidth - splashScreen.Width) / 2, (screenHeight - splashScreen.Height) / 2);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Supprime les éléments du jeu de la mémoire
        /// </summary>
        protected override void UnloadContent()
        {
            if(IsKinectMode)
                kinectInput.UnloadContent();
            Content.Unload();
        }

        /// <summary>
        /// Lance le jeu
        /// </summary>
        /// <param name="d">Difficultée du jeu</param>
        private void Jouer(Difficulty d)
        {
            if (IsKinectMode)
            {
                jeu = new PongKinect(this, d, IsOnePlayer, kinectInput);
            }
            else
            {
                jeu = new PongKeyboard(this, d, IsOnePlayer, keyboardInput);
            }
        }

        /// <summary>
        /// Créer les menus
        /// </summary>
        private void SetMenus()
        {
            if (IsKinectMode)
            {
                IsOnePlayer = true;
                PlayingMenu = new MenuKinect(this, kinectInput);
                DifficultyMenu = new MenuKinect(this, kinectInput);
                PauseMenu = new MenuKinect(this, kinectInput);
                EndMenu = new MenuKinect(this, kinectInput);
            }
            else
            {
                PlayingMenu = new MenuKeyboard(this, keyboardInput);
                DifficultyMenu = new MenuKeyboard(this, keyboardInput);
                PauseMenu = new MenuKeyboard(this, keyboardInput);
                EndMenu = new MenuKeyboard(this, keyboardInput);
            }
            SetMenu(PlayingMenu, "Jouer", "Choisis le nombre de joueurs", new List<string>() { "1 joueur", "2 joueurs" });
            SetMenu(DifficultyMenu, "Difficultés", "Choisis la difficultées", new List<string>() { "Facile", "Moyen", "Difficile" });
            SetMenu(PauseMenu, "Pause", "Que veux-tu faire ?", new List<string>() { "Reprendre", "Menu"});
            SetMenu(EndMenu, "Fin du jeu", "", new List<string>() { "Menu",  });
        }

        /// <summary>
        /// Itialize un menu
        /// </summary>
        /// <param name="menu">Le menu à initializer</param>
        /// <param name="title">Le title du menu</param>
        /// <param name="desc">La description qui sera dite par la synthèse vocale</param>
        /// <param name="items">Les différent boutons</param>
        private void SetMenu(Menu menu, string title, string desc, List<string> items)
        {
            menu.Title = title;
            menu.Description = desc;
            menu.MenuItems = items;
        }

        /// <summary>
        /// Méthode appelé par le système de jeu en boucle
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            keyboardInput.Update();

            if (keyboardInput.Exit)
            {
                this.Exit();
            }

            if (keyboardInput.ChangeColors())
            {
                ColorManager.ChangeColors();
            }

            // Si le joueur demande de l'aide
            bool AskHelping = IsKinectMode ? keyboardInput.Help() || kinectInput.Help() : keyboardInput.Help();
            
            #region Playing
            if (gamestate == GameStates.Running)
            {
                jeu.Help = AskHelping;
                jeu.Update();

                if (jeu.IsFinish)
                {
                    EndMenu.Description = jeu.getMessage();
                    EndMenu.StartDescription();
                    keyboardInput.IsHoldable = false;
                    gamestate = GameStates.EndMenu;
                }
                if (!IsKinectMode ? keyboardInput.Break() : kinectInput.Break())
                {
                    keyboardInput.IsHoldable = false;
                    gamestate = GameStates.PauseMenu;
                    PauseMenu.StartDescription();
                }
            }
            #endregion Playing
            #region ModeMenu
            else if (gamestate == GameStates.ModeMenu)
            {
                ModeMenu.Help = AskHelping;
                ModeMenu.Update();
                
                // Lors de la selection
                if (ModeMenu.Valid)
                {
                    gamestate = GameStates.PlayingMenu;
                    if (ModeMenu.Iterator == 0)
                    {
                        IsKinectMode = false;
                    }
                    else if (ModeMenu.Iterator == 1)
                    {
                        IsKinectMode = true;
                        kinectInput = new KinectInput(this);
                    }
                    else if (ModeMenu.Iterator == 2)
                    {
                        this.Exit();
                    }
                    SetMenus();
                    PlayingMenu.StartDescription();
                }
            }
            #endregion ModeMenu
            #region Menu Jouer
            else if (gamestate == GameStates.PlayingMenu)
            {
                PlayingMenu.Help = AskHelping;
                PlayingMenu.Update();
                // Lors de la selection
                if (PlayingMenu.Valid)
                {
                    gamestate = GameStates.DifficultyMenu;
                    DifficultyMenu.StartDescription();
                    if (PlayingMenu.Iterator == 0)
                    {
                        IsOnePlayer = true;
                    }
                    else if (PlayingMenu.Iterator == 1)
                    {
                        IsOnePlayer = false;
                    }
                }
                if (PlayingMenu.Back)
                {
                    gamestate = GameStates.ModeMenu;
                    ModeMenu.StartDescription();
                    IsKinectMode = false;
                }
            }
            #endregion Menu Jouer
            #region DifficultyMenu
            else if (gamestate == GameStates.DifficultyMenu)
            {
                DifficultyMenu.Help = AskHelping;
                DifficultyMenu.Update();

                // Lors de la selection
                if (DifficultyMenu.Valid)
                {
                    gamestate = GameStates.Running;
                    keyboardInput.IsHoldable = true;
                    if (DifficultyMenu.Iterator == 0)
                    {
                        Jouer(Difficulty.EASY);
                    }
                    else if (DifficultyMenu.Iterator == 1)
                    {
                        Jouer(Difficulty.MEDIUM);
                    }
                    else if (DifficultyMenu.Iterator == 2)
                    {
                        Jouer(Difficulty.HARD);
                    }
                }
                if (DifficultyMenu.Back)
                {
                    gamestate = GameStates.PlayingMenu;
                    PlayingMenu.StartDescription();
                }
            }
            #endregion DifficultyMenu
            #region PauseMenu
            else if (gamestate == GameStates.PauseMenu)
            {
                PauseMenu.Help = AskHelping;
                PauseMenu.Update();

                // Lors de la selection
                if (PauseMenu.Valid)
                {
                    if (PauseMenu.Iterator == 0)
                    {
                        keyboardInput.IsHoldable = true;
                        jeu.SetAfterBreak();
                        gamestate = GameStates.Running;
                    }
                    else if (PauseMenu.Iterator == 1)
                    {
                        gamestate = GameStates.PlayingMenu;
                        PlayingMenu.StartDescription();
                    }
                    else if (PauseMenu.Iterator == 2)
                    {
                        this.Exit();
                    }
                }
                if (PauseMenu.Back)
                {
                    keyboardInput.IsHoldable = true;
                    jeu.SetAfterBreak();
                    gamestate = GameStates.Running;
                }
            }
            #endregion PauseMenu
            #region EndMenu
            else if (gamestate == GameStates.EndMenu)
            {
                EndMenu.Help = AskHelping;
                EndMenu.Update();

                // Lors de la selection
                if (EndMenu.Valid)
                {
                    if (EndMenu.Iterator == 0)
                    {
                        gamestate = GameStates.PlayingMenu;
                        PlayingMenu.StartDescription();
                    }
                    else if (EndMenu.Iterator == 1)
                    {
                        this.Exit();
                    }
                }
            }
            #endregion EndMenu
            #region SplashCreen
            else if (gamestate == GameStates.SplashScreen)
            {
                splashScreenTimer -= gameTime.ElapsedGameTime;
                if (splashScreenTimer.CompareTo(new TimeSpan(0)) <= 0)
                {
                    gamestate = GameStates.ModeMenu;
                    ModeMenu.StartDescription();
                }
            }
            #endregion SplashCreen

            base.Update(gameTime);
        }

        /// <summary>
        /// Méthode appeler régulièrement par le système de jeu pour dessiner à l'écran
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (gamestate == GameStates.Running)
            {
                jeu.Draw();
            }
            else if (gamestate == GameStates.ModeMenu)
            {
                ModeMenu.Draw();
            }
            else if (gamestate == GameStates.PlayingMenu)
            {
                PlayingMenu.Draw();
            }
            else if (gamestate == GameStates.DifficultyMenu)
            {
                DifficultyMenu.Draw();
            }
            else if (gamestate == GameStates.PauseMenu)
            {
                PauseMenu.Draw();
            }
            else if (gamestate == GameStates.EndMenu)
            {
                EndMenu.Draw();
            }
            else if (gamestate == GameStates.SplashScreen)
            {
                GraphicsDevice.Clear(Color.Black);
                SpriteBatch.Draw(splashScreen, screen, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}