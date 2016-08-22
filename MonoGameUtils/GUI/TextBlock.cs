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
	/// <summary>
	/// 
	/// </summary>
	public class TextBlock : GuiElement
	{
		#region Members
		private string m_text = null;
		private SpriteFont m_font = null;
		private Color m_textColor = Color.Black;
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public string Text 
		{
			get { return m_text; }
			set 
			{
				if (m_text != value)
				{
					m_text = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public SpriteFont Font
		{
			get
            {
                if (m_font == null && this.Manager != null)
                    return this.Manager.DefaultFont;

                return m_font;
            }
			set
			{
				if (m_font != value)
				{
					m_font = value;
					this.Invalidate();
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public Color TextColor 
		{
			get
			{
				return m_textColor;
			}
			set
			{
				m_textColor = value;
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
            base.Draw(sprite);

            var font = this.Font;

            if (font != null && !string.IsNullOrEmpty(m_text))
				sprite.DrawString(font, m_text, new Vector2(m_bounds.X, m_bounds.Y), m_textColor);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		protected override Point OnMeasure(Point point)
		{
            var font = this.Font;

            if (font != null && !string.IsNullOrEmpty(m_text))
			{
				Vector2 textSize = font.MeasureString(m_text);
				return new Point((int)Math.Ceiling(textSize.X), (int)Math.Ceiling(textSize.Y));
			}

			return Point.Zero;
		}
		#endregion
	}
}
