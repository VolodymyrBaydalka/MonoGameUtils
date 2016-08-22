using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;
using GameUtils;

namespace GameUtilsSample
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        #region Members
        private GraphicsDeviceManager graphics;
        private GuiManager m_guiManager = null;
        private InputComponent m_inputComponent = null;
        private SpriteBatch m_spriteBatch = null;
        private Life m_life = new Life(160, 100);
        private bool m_isRun = false;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
        }
        #endregion

        #region Implementation
        /// <summary>
        /// 
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            m_inputComponent = new InputComponent(this);
            this.Services.AddService(m_inputComponent);
            this.Components.Add(m_inputComponent);

            m_guiManager = new GuiManager(this);
            this.Components.Add(m_guiManager);
            this.Window.AllowUserResizing = true;

            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            LoadUI();

            m_spriteBatch = new SpriteBatch(this.GraphicsDevice);

            m_life.LoadContent(this);
        }

        private void LoadUI()
        {
            var rootPanel = new StackPanel
            {
                HAlignment = GuiAlignment.Far,
                VAlignment = GuiAlignment.Stretch,
                Padding = 10,
                Background = new BorderDrawable(this.Content.Load<Texture2D>("gui/background"), 10),
                Width = 160
            };

            // logo
            rootPanel.Children.Add(new ImageBlock { Texture = this.Content.Load<Texture2D>("gui/Logo") });

            // fps counter
            rootPanel.Children.Add(new FpsCounter());

            // run button
            var runButtonLabel = new TextBlock { Text = "RUN" };
            var runButton = new ContentButton(runButtonLabel)
            {
                NormalDrawable = new BorderDrawable(this.Content.Load<Texture2D>("gui/Button"), 10),
                ClickedDrawable = new BorderDrawable(this.Content.Load<Texture2D>("gui/ButtonPressed"), 10),
                Margin = 5
            };

            runButton.Click += (s, e) =>
            {
                m_isRun = !m_isRun;
                runButtonLabel.Text = m_isRun ? "PAUSE" : "RUN";
            };

            rootPanel.Children.Add(runButton);

            // speed controls
            var incSpeedButton = new ContentButton
            {
                NormalDrawable = new TextureDrawable(this.Content.Load<Texture2D>("gui/RightButton"))
            };
            var decSpeedButton = new ContentButton
            {
                NormalDrawable = new TextureDrawable(this.Content.Load<Texture2D>("gui/LeftButton"))
            };

            var speedLabel = new TextBlock { Text = m_life.Timer.Interval + " sec" };

            incSpeedButton.Click += (s, e) =>
            {
                m_life.Timer.Interval *= 1.5f;
                speedLabel.Text = m_life.Timer.Interval + " sec";
            };

            decSpeedButton.Click += (s, e) =>
            {
                m_life.Timer.Interval /= 1.5f;
                speedLabel.Text = m_life.Timer.Interval + " sec";
            };

            rootPanel.Children.Add(new StackPanel(decSpeedButton, speedLabel, incSpeedButton)
            {
                Orientation = GuiOrientation.Horizontal,
                Margin = 5
            });

            // infobox
            var textEditBox = new TextEditBox
            {
                TextColor = Color.White,
                Padding = 10,
                Background = new BorderDrawable(this.Content.Load<Texture2D>("gui/textbackground"), 10),
                Text = new StringBuilder()
                    .AppendLine("Left mouse")
                    .AppendLine("- add cell")
                    .AppendLine("Right mouse")
                    .AppendLine("- remove cell")
                    .ToString()
            };

            rootPanel.Children.Add(textEditBox);

            m_guiManager.DefaultFont = this.Content.Load<SpriteFont>("gui/font");
            m_guiManager.RootElement = rootPanel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (m_isRun)
            {
                m_life.Update(gameTime);
            }

            if (m_inputComponent.CurrectMouseState.LeftButton == ButtonState.Pressed)
            {
                int x = m_inputComponent.CurrectMouseState.X / Life.CellSize;
                int y = m_inputComponent.CurrectMouseState.Y / Life.CellSize;

                m_life.Born(x, y);
            }
            else if (m_inputComponent.CurrectMouseState.RightButton == ButtonState.Pressed)
            {
                int x = m_inputComponent.CurrectMouseState.X / Life.CellSize;
                int y = m_inputComponent.CurrectMouseState.Y / Life.CellSize;

                m_life.Kill(x, y);
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_spriteBatch.Begin();
            m_life.Draw(gameTime, m_spriteBatch);
            m_spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
