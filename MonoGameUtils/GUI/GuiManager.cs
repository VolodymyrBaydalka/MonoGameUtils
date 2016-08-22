#region License
/*******************************************************************************
 * Copyright 2016 Volodymyr Baydalka.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;


namespace GameUtils
{
    public class GuiManager : DrawableGameComponent
    {
        #region Members
        private GuiElement m_rootElement = null;
        private SpriteBatch m_spriteBatch = null;
        private bool m_needLayout = false;
        private GuiElement m_hitElement = null; //current elemtn under mouse cursor
        private GuiElement m_captureElement = null; //element that locked mouse
        private GuiElement m_focusedElement = null; //element that locked keyboard
        private InputComponent m_input = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private SpriteFont _font;

        public SpriteFont DefaultFont
        {
            get { return _font; }
            set
            {

                if (_font != value)
                {
                    _font = value;
                    this.InvalidateLayout();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GuiElement RootElement
        {
            get
            {
                return m_rootElement;
            }
            set
            {
                if (m_rootElement != value)
                {
                    if (m_rootElement != null)
                        m_rootElement.Manager = null;

                    m_rootElement = value;
                    m_needLayout = true;
                    m_hitElement = null;

                    if (m_rootElement != null)
                        m_rootElement.Manager = this;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public GuiElement FocusedElement
        {
            get
            {
                return m_focusedElement;
            }
            set
            {
                if (m_focusedElement != value)
                {
                    m_focusedElement = value;
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public GuiManager(Game game)
            : base(game)
        {
        }
        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void CaptureMouse(GuiElement element)
        {
            if (m_captureElement == null && element.Manager == this)
            {
                m_captureElement = element;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void ReleaseMouse(GuiElement element)
        {
            if (m_captureElement == element)
            {
                m_captureElement = null;
            }
        }
        #endregion

        #region Implementation
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            m_input = this.Game.Services.GetService(typeof(InputComponent)) as InputComponent;

            this.Game.Window.ClientSizeChanged += new EventHandler<EventArgs>(OnClientSizeChanged);

            base.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(this.GraphicsDevice);
            base.LoadContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (m_rootElement != null)
            {
                m_rootElement.Update(gameTime);

                if (this.Game.IsActive && m_input != null)
                {
                    this.ProcessMouseInput(m_input);
                    this.ProcessKeyboardInput(m_input);
                }

                if (m_needLayout)
                {
                    Rectangle screen = new Rectangle();

                    screen.X = this.GraphicsDevice.Viewport.X;
                    screen.Y = this.GraphicsDevice.Viewport.Y;
                    screen.Width = this.GraphicsDevice.Viewport.Width;
                    screen.Height = this.GraphicsDevice.Viewport.Height;

                    m_rootElement.Measure(new Point(screen.Width, screen.Height));
                    m_rootElement.Layout(screen);

                    m_needLayout = false;
                }
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (m_rootElement != null)
            {
                m_spriteBatch.Begin(SpriteSortMode.Deferred);
                m_rootElement.Draw(m_spriteBatch);
                m_spriteBatch.End();
            }

            base.Draw(gameTime);
        }
        /// <summary>
        /// 
        /// </summary>
        public void InvalidateLayout()
        {
            m_needLayout = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        private void ProcessMouseInput(InputComponent input)
        {
            if (input.IsMouseChanged)
            {
                GuiElement mouseElement = null;

                if (m_captureElement == null)
                {
                    if (input.IsMouseMove && m_rootElement != null)
                    {
                        GuiElement hitElement = m_rootElement.HitTest(input.CurrectMouseState.X,
                            input.CurrectMouseState.Y);

                        if (m_hitElement != hitElement)
                        {
                            if (m_hitElement != null)
                                m_hitElement.OnMouseLeave(input);

                            m_hitElement = hitElement;

                            if (m_hitElement != null)
                                m_hitElement.OnMouseEnter(input);
                        }
                    }

                    mouseElement = m_hitElement;
                }
                else
                {
                    mouseElement = m_captureElement;
                }

                if (mouseElement != null)
                {
                    if (input.IsMouseMove)
                        mouseElement.OnMouseMove(input);

                    if (input.IsMouseDown)
                        mouseElement.OnMouseDown(input);

                    if (input.IsMouseUp)
                        mouseElement.OnMouseUp(input);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        private void ProcessKeyboardInput(InputComponent input)
        {
            if (m_focusedElement != null)
            {
                if (input.IsKeyboardChanged)
                {
                    m_focusedElement.OnKeyboardChanged(input);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            InvalidateLayout();
        }
        #endregion
    }
}