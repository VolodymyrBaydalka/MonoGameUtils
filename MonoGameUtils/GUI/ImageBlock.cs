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
	public class ImageBlock : GuiElement
	{
		#region Members
		private Texture2D m_texture = null;
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public Texture2D Texture 
		{
			get { return m_texture; }
			set
			{
				if (m_texture != value)
				{
					m_texture = value;
					this.Invalidate();
				}
			}
		}
		#endregion

		#region Public methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sprite"></param>
		public override void Draw(SpriteBatch sprite)
		{
            base.Draw(sprite);

            if (m_texture != null)
				sprite.Draw(m_texture, m_bounds, Color.White);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		protected override Point OnMeasure(Point point)
		{
			return m_texture == null 
                ? Point.Zero : new Point(m_texture.Width, m_texture.Height);
		}
		#endregion
	}
}
