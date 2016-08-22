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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameUtils
{
	/// <summary>
	/// 
	/// </summary>
	public enum GuiAlignment
	{ 
		/// <summary>
		/// 
		/// </summary>
		Near,
		/// <summary>
		/// 
		/// </summary>
		Center,
		/// <summary>
		/// 
		/// </summary>
		Far,
		/// <summary>
		/// 
		/// </summary>
		Stretch
	}

	/// <summary>
	///
	/// </summary>
	public abstract class GuiElement
	{
		#region Members
		/// <summary>
		/// 
		/// </summary>
		protected Rectangle m_bounds = Rectangle.Empty;
		/// <summary>
		/// 
		/// </summary>
		protected Point m_measureSize = Point.Zero;
		/// <summary>
		/// 
		/// </summary>
		protected GuiElement m_parent = null;
		/// <summary>
		/// 
		/// </summary>
		protected GuiManager m_manager = null;
        /// <summary>
        /// 
        /// </summary>
        protected GuiAlignment m_vAlignment = GuiAlignment.Center;
        /// <summary>
        /// 
        /// </summary>
        protected GuiAlignment m_hAlignment = GuiAlignment.Center;
        /// <summary>
        /// 
        /// </summary>
        protected int? m_width = null;
        /// <summary>
        /// 
        /// </summary>
        protected int? m_height = null;
        /// <summary>
        /// 
        /// </summary>
        protected int m_padding = 0;
        /// <summary>
        /// 
        /// </summary>
        protected int m_margin = 0;
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public Rectangle Bounds
		{
			get	{ return m_bounds; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Point MeasureSize
		{ 
			get { return m_measureSize; }
		}
		/// <summary>
		/// 
		/// </summary>
		public GuiAlignment VAlignment
		{
			get { return m_vAlignment; }
			set 
			{
				if (m_vAlignment != value)
				{
					m_vAlignment = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public GuiAlignment HAlignment
		{
			get { return m_hAlignment; }
			set 
			{
				if (m_hAlignment != value)
				{
					m_hAlignment = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public GuiManager Manager
		{
			get
			{
				if (m_manager == null && m_parent != null)
					return m_parent.Manager;

				return m_manager;
			}
			internal set
			{
				if (m_manager != value)
				{
					m_manager = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public GuiElement Parent
		{
			get
			{
				return m_parent;
			}
			internal set
			{
				if (m_parent != value)
				{
					m_parent = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Width
		{
			get { return m_width; }
			set
			{
				if (m_width != value)
				{
					m_width = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Height
		{
			get { return m_height; }
			set
			{
				if (m_height != value)
				{
					m_height = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Padding
		{
			get { return m_padding; }
			set
			{
				if (m_padding != value)
				{
					m_padding = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Margin
		{
			get { return m_margin; }
			set
			{
				if (m_margin != value)
				{
					m_margin = value;
					this.Invalidate();
				}
			}
		}
        public GuiDrawable Background { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual GuiElement HitTest(int x, int y)
		{
			return m_bounds.Contains(x, y) ? this : null;
		}

		/// <summary>
		/// Апдейт
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void Update(GameTime gameTime)
		{ 
		}
		/// <summary>
		/// Рисуем
		/// </summary>
		/// <param name="sprite"></param>
		public virtual void Draw(SpriteBatch sprite)
		{
            if (this.Background != null)
                this.Background.Draw(sprite, this.Bounds, Color.White);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		public void Layout(Rectangle rect)
		{
			switch (m_hAlignment)
			{
				case GuiAlignment.Near:
					m_bounds.Width = m_measureSize.X;
					m_bounds.X = rect.X;
					break;

				case GuiAlignment.Center:
					m_bounds.Width = m_measureSize.X;
					m_bounds.X = rect.X + (rect.Width - m_measureSize.X) / 2;
					break;

				case GuiAlignment.Far:
					m_bounds.Width = m_measureSize.X;
					m_bounds.X = rect.X + (rect.Width - m_measureSize.X);
					break;

				case GuiAlignment.Stretch:
					m_bounds.Width = Math.Max( m_measureSize.X, rect.Width);
					m_bounds.X = rect.X + (rect.Width - m_measureSize.X) / 2;
					break;
			}

			switch (m_vAlignment)
			{
				case GuiAlignment.Near:
					m_bounds.Height = m_measureSize.Y;
					m_bounds.Y = rect.Y;
					break;

				case GuiAlignment.Center:
					m_bounds.Height = m_measureSize.Y;
					m_bounds.Y = rect.Y + (rect.Height - m_measureSize.Y) / 2;
					break;

				case GuiAlignment.Far:
					m_bounds.Height = m_measureSize.Y;
					m_bounds.Y = rect.Y + (rect.Height - m_measureSize.Y);
					break;

				case GuiAlignment.Stretch:
					m_bounds.Height = Math.Max(m_measureSize.Y, rect.Height);
					m_bounds.Y = rect.Y + (rect.Height - m_bounds.Height) / 2;
					break;
			}

			m_bounds.Inflate(-m_margin, -m_margin);

			Rectangle layoutRect = m_bounds;

			layoutRect.Inflate(-m_padding, -m_padding);

			this.OnLayout(layoutRect);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public Point Measure(Point point)
		{
			if (m_width.HasValue)
				point.X = m_width.Value;
			else
				point.X -= 2 * m_margin;

			if (m_height.HasValue)
				point.Y = m_height.Value;
			else
				point.Y -= 2 * m_margin;

			point.X -= 2 * m_padding;
			point.Y -= 2 * m_padding;

			// OnMeasure
			m_measureSize = this.OnMeasure(point);

			if (m_width.HasValue)
				m_measureSize.X = m_width.Value;
			else
				m_measureSize.X += 2 * m_padding;

			if (m_height.HasValue)
				m_measureSize.Y = m_height.Value;
			else
				m_measureSize.Y += 2 * m_padding;

			m_measureSize.X += 2 * m_margin;
			m_measureSize.Y += 2 * m_margin;

			return m_measureSize;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		public virtual void OnMouseEnter(InputComponent input)
		{
			if (m_parent != null)
				m_parent.OnMouseEnter(input);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		public virtual void OnMouseLeave(InputComponent input)
		{
			if (m_parent != null)
				m_parent.OnMouseLeave(input);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		public virtual void OnMouseMove(InputComponent input)
		{
			if (m_parent != null)
				m_parent.OnMouseMove(input);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		public virtual void OnMouseDown(InputComponent input)
		{
			if (m_parent != null)
				m_parent.OnMouseDown(input);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		public virtual void OnMouseUp(InputComponent input)
		{
			if (m_parent != null)
				m_parent.OnMouseUp(input);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		public virtual void OnKeyboardChanged(InputComponent input)
		{
			if (m_parent != null)
				m_parent.OnKeyboardChanged(input);
		}
		#endregion

		#region Implementation
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		protected virtual void OnLayout(Rectangle rect)
		{ 
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		protected virtual Point OnMeasure(Point point)
		{
			return Point.Zero;
		}
		/// <summary>
		/// 
		/// </summary>
		protected void Invalidate()
		{
			GuiManager manager = this.Manager;

			if (manager != null)
				manager.InvalidateLayout();
		}
		#endregion
	}
}
