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
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameUtils
{
    public class ContentButton : GuiContainer
    {
        #region Nested types
        enum State
        {
            Normal,
            Highlighted,
            Clicked
        }
        #endregion

        #region Members
        private State m_current = State.Normal;
        private GuiDrawable m_normal = null;
        private GuiDrawable m_clicked = null;
        private GuiDrawable m_highlighted = null;
        #endregion

        #region Events
        public event EventHandler Click;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ContentButton()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public ContentButton(GuiElement content)
            : base(content)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public GuiDrawable NormalDrawable
        {
            get { return m_normal; }
            set
            {
                if (m_normal != value)
                {
                    m_normal = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public GuiDrawable ClickedDrawable
        {
            get { return m_clicked; }
            set
            {
                if (m_clicked != value)
                {
                    m_clicked = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public GuiDrawable HighlightedDrawable
        {
            get { return m_highlighted; }
            set
            {
                if (m_highlighted != value)
                {
                    m_highlighted = value;
                    this.Invalidate();
                }
            }
        }
        #endregion

        #region Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite"></param>
        public override void Draw(SpriteBatch sprite)
        {
            GuiDrawable drawable = this.GetDrawable();

            if (drawable != null)
            {
                drawable.Draw(sprite, m_bounds, Color.White);
            }

            base.Draw(sprite);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public override void OnMouseDown(InputComponent input)
        {
            m_current = State.Clicked;
            this.Manager.CaptureMouse(this);

            base.OnMouseDown(input);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public override void OnMouseUp(InputComponent input)
        {
            m_current = State.Normal;
            this.Manager.ReleaseMouse(this);

            if (Click != null)
            {
                Click(this, EventArgs.Empty);
            }

            base.OnMouseUp(input);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public override void OnMouseEnter(InputComponent input)
        {
            m_current = State.Highlighted;
            base.OnMouseEnter(input);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public override void OnMouseLeave(InputComponent input)
        {
            m_current = State.Normal;
            base.OnMouseLeave(input);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected override Point OnMeasure(Point point)
        {
            var result = base.OnMeasure(point);
            var drawable = this.GetDrawable();

            if (drawable != null)
            {
                var preferSize = drawable.PreferSize;

                if (result.X < preferSize.X)
                    result.X = preferSize.X;

                if (result.Y < preferSize.Y)
                    result.Y = preferSize.Y;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GuiDrawable GetDrawable()
        {
            switch (m_current)
            {
                case State.Normal:
                    return m_normal;

                case State.Highlighted:
                    return m_highlighted == null ? m_normal : m_highlighted;

                case State.Clicked:
                    return m_clicked == null ? m_normal : m_clicked;
            }

            return null;
        }
        #endregion
    }
}
