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
	public abstract class GuiDrawable
	{
		public abstract void Draw(SpriteBatch spriteBatch, Rectangle rect, Color color);

		/// <summary>
		/// 
		/// </summary>
		public virtual Point PreferSize
		{
			get { return Point.Zero; }
		}
    }

	public class TextureDrawable : GuiDrawable
	{
		#region Members
		private int m_border = 0;
		private Texture2D m_texture = null;
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public Texture2D Texture
		{
			get
			{
				return m_texture;
			}
			set
			{
				m_texture = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Border
		{
			get
			{
				return m_border;
			}
			set
			{
				m_border = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public override Point PreferSize
		{
			get
			{
				return new Point(m_texture.Width, m_texture.Height);
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// 
		/// </summary>
		public TextureDrawable()
		{ 
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="texture"></param>
		public TextureDrawable(Texture2D texture)
		{
			m_texture = texture;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public override void Draw(SpriteBatch spriteBatch, Rectangle rect, Color color)
		{
			if (m_texture != null)
			{
				spriteBatch.Draw(m_texture, rect, color);
			}
		}
        #endregion
    }

	public class BorderDrawable : GuiDrawable
	{
		#region Members
		private int m_border = 0;
		private Texture2D m_texture = null;
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public Texture2D Texture 
		{
			get
			{
				return m_texture;
			}
			set
			{
				m_texture = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Border 
		{
			get
			{
				return m_border;
			}
			set
			{
				m_border = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public override Point PreferSize
		{
			get
			{
				return new Point(m_texture.Width, m_texture.Height);
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// 
		/// </summary>
		public BorderDrawable()
		{ 
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="padding"></param>
		public BorderDrawable(Texture2D texture, int padding)
		{
			m_texture = texture;
			m_border = padding;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public override void Draw(SpriteBatch spriteBatch, Rectangle rect, Color color)
		{
			if (m_texture != null)
			{
				var srcRect = new Rectangle(0, 0, m_texture.Width, m_texture.Height);

				spriteBatch.Draw(m_texture, GetLeftTop(rect), GetLeftTop(srcRect), color);
				spriteBatch.Draw(m_texture, GetLeftCenter(rect), GetLeftCenter(srcRect), color);
				spriteBatch.Draw(m_texture, GetLeftBottom(rect), GetLeftBottom(srcRect), color);

				spriteBatch.Draw(m_texture, GetCenterTop(rect), GetCenterTop(srcRect), color);
				spriteBatch.Draw(m_texture, GetCenterCenter(rect), GetCenterCenter(srcRect), color);
				spriteBatch.Draw(m_texture, GetCenterBottom(rect), GetCenterBottom(srcRect), color);

				spriteBatch.Draw(m_texture, GetRightTop(rect), GetRightTop(srcRect), color);
				spriteBatch.Draw(m_texture, GetRightCenter(rect), GetRightCenter(srcRect), color);
				spriteBatch.Draw(m_texture, GetRightBottom(rect), GetRightBottom(srcRect), color);
			}
		}
		#endregion

		#region Implementation
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private Rectangle GetLeftTop(Rectangle rect)
		{
			return new Rectangle(rect.X, rect.Y, m_border, m_border);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private Rectangle GetLeftCenter(Rectangle rect)
		{
			return new Rectangle(rect.X, rect.Y + m_border, m_border, rect.Height - 2 * m_border);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private Rectangle GetLeftBottom(Rectangle rect)
		{
			return new Rectangle(rect.X, rect.Bottom - m_border, m_border, m_border);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private Rectangle GetCenterTop(Rectangle rect)
		{
			return new Rectangle(rect.X + m_border, rect.Y, 
				rect.Width - 2 * m_border, m_border);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private Rectangle GetCenterCenter(Rectangle rect)
		{
			return new Rectangle(rect.X + m_border, rect.Y + m_border,
				rect.Width - 2 * m_border, rect.Height - 2 * m_border);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private Rectangle GetCenterBottom(Rectangle rect)
		{
			return new Rectangle(rect.X + m_border, rect.Bottom - m_border, 
				rect.Width - 2 * m_border, m_border);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private Rectangle GetRightTop(Rectangle rect)
		{
			return new Rectangle(rect.Right - m_border, rect.Y, m_border, m_border);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private Rectangle GetRightCenter(Rectangle rect)
		{
			return new Rectangle(rect.Right - m_border, rect.Y + m_border,
				m_border, rect.Height - 2 * m_border);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		private Rectangle GetRightBottom(Rectangle rect)
		{
			return new Rectangle(rect.Right - m_border, rect.Bottom - m_border, m_border, m_border);
		}
		#endregion
	}
}
