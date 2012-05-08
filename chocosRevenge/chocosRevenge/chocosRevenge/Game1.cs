using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace chocosRevenge
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ParticleEngine particleEngine;
        Texture2D TitleScreen;
        Texture2D PauseScreen;

        Player player;
        Enemy enemy;
        LargeEnemy largeEnemy;
        HealthBar playerHealth;
        Scrolling background1;
        Scrolling background2;
        Stats stats;

        SpriteFont font;

        const int screenWidth = 1000;
        const int screenHeight = 700;

        bool IsTitleScreenShown;
        bool IsPauseScreenShown;

        SoundEffect backingTrack;
        bool songStart = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this) { PreferredBackBufferWidth = screenWidth, PreferredBackBufferHeight = screenHeight };
            Content.RootDirectory = "Content";
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 100);
        }

        protected override void Initialize()
        {
            player = new Player();
            enemy = new Enemy();
            largeEnemy = new LargeEnemy();
            playerHealth = new HealthBar();
            stats = new Stats(player);

            background1 = new Scrolling(Content.Load<Texture2D>("Backgrounds/background1"), new Rectangle(0, 0, screenWidth, screenHeight));
            background2 = new Scrolling(Content.Load<Texture2D>("Backgrounds/background1"), new Rectangle(screenWidth, 0, screenWidth, screenHeight));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("circle"));
            //textures.Add(Content.Load<Texture2D>("star"));
            //textures.Add(Content.Load<Texture2D>("diamond"));

            particleEngine = new ParticleEngine(textures, new Vector2(400, 240));

            font = Content.Load<SpriteFont>("SpriteFont1");

            TitleScreen = Content.Load<Texture2D>("TitleScreen");

            enemy.characterTexture = Content.Load<Texture2D>("enemy_small_spritesheet");
            enemy.LoadContent();
            largeEnemy.characterTexture = Content.Load<Texture2D>("enemy_large_spritesheet");
            largeEnemy.LoadContent();
            player.characterTexture = Content.Load<Texture2D>("choco_spritesheet_handgun");
            player.LoadContent();
            playerHealth.mHealthBar = Content.Load<Texture2D>("HealthBar");
            player.LoadContent();

            stats.LoadContent(Content);

            PauseScreen = Content.Load<Texture2D>("PauseScreen");
            IsTitleScreenShown = true;
            IsPauseScreenShown = false;

            backingTrack = Content.Load<SoundEffect>("game_music");
            MediaPlayer.IsRepeating = true; 
        }

        protected override void Update(GameTime gameTime)
        {
            particleEngine.EmitterLocation = player.particleTarget;
            particleEngine.Update();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            if (IsTitleScreenShown)
            {
                UpdateTitleScreen();
            }
            else
            {
                UpdatePauseScreen();

                if (IsPauseScreenShown == false)
                {
                    if (!songStart)
                    {
                        backingTrack = Content.Load<SoundEffect>("game_music");
                        songStart = true;
                        backingTrack.Play();
                    }
                    
                    player.Update(gameTime, enemy);
                    enemy.SeekPlayer(gameTime, player);
                    largeEnemy.SeekPlayer(gameTime, player);
                    playerHealth.Update(player);
                    background1.Update(player);
                    background2.Update(player);

                    if (background1.rec.X + background1.texture.Width <= 0)
                        background1.rec.X = background2.rec.X + background2.texture.Width;
                    if (background2.rec.X + background2.texture.Width <= 0)
                        background2.rec.X = background1.rec.X + background1.texture.Width;
                }
            }
            base.Update(gameTime);

            stats.Update(player);
        }

        private void UpdatePauseScreen()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true && IsPauseScreenShown == true)
            {
                Initialize();
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.U) == true && IsPauseScreenShown == true)
            {
                IsPauseScreenShown = false;
                return;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.P) == true && IsPauseScreenShown == false)
            {
                IsPauseScreenShown = true;
                return;
            }
        }

        private void UpdateTitleScreen()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) == true)
            {
                IsTitleScreenShown = false;
                return;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (IsTitleScreenShown)
            {
                DrawTitleScreen();
            }
            else
            {
                background1.Draw(spriteBatch);

                background2.Draw(spriteBatch);

                player.Draw(spriteBatch);

                enemy.Draw(spriteBatch);

                largeEnemy.Draw(spriteBatch);

                stats.Draw(spriteBatch);

                if (IsPauseScreenShown)
                {
                    DrawPauseScreen();
                }particleEngine.Draw(spriteBatch);
            }
                spriteBatch.End();
                
                base.Draw(gameTime);
            

            
        }

        private void DrawPauseScreen()
        {
            spriteBatch.Draw(PauseScreen, Vector2.Zero, Color.White);
        }

        private void DrawTitleScreen()
        {
            spriteBatch.Draw(TitleScreen, Vector2.Zero, Color.White);
        }
    }
}
