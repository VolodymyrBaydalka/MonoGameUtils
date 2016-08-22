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

namespace GameUtils
{
	public enum GuiOrientation
	{ 
		Vertical,
		Horizontal
	}

	public class StackPanel : GuiContainer
	{
		#region Members
		private GuiOrientation m_orientation = GuiOrientation.Vertical;
		#endregion

		#region Proeprties
		/// <summary>
		/// 
		/// </summary>
		public GuiOrientation Orientation 
		{
			get { return m_orientation; }
			set
			{
				if (m_orientation != value)
				{
					m_orientation = value;
					this.Invalidate();
				}
			}
		}
        #endregion

        #region Constructor
        public StackPanel() { }
        public StackPanel(params GuiElement[] content): base(content) { }
        #endregion

        #region Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        protected override void OnLayout(Rectangle rect)
		{
			int position = 0;
			Rectangle cRect = new Rectangle();

			foreach (GuiElement element in _children)
			{
				if (m_orientation == GuiOrientation.Vertical)
				{
					cRect.X = rect.X;
					cRect.Y = rect.Y + position;
					cRect.Width = rect.Width;
					cRect.Height = element.MeasureSize.Y;

					position += cRect.Height;
				}
				else
				{
					cRect.X = rect.X + position;
					cRect.Y = rect.Y;
					cRect.Width = element.MeasureSize.X;
					cRect.Height = rect.Height;

					position += cRect.Width;
				}

				element.Layout(cRect);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		protected override Point OnMeasure(Point point)
		{
			Point resultSize = new Point();

			foreach (GuiElement element in _children)
			{
				Point size = element.Measure(point);

				if (m_orientation == GuiOrientation.Vertical)
				{
					resultSize.X = Math.Max(size.X, resultSize.X);
					resultSize.Y += size.Y;
				}
				else
				{
					resultSize.Y = Math.Max(size.Y, resultSize.Y);
					resultSize.X += size.X;
				}
			}

			return resultSize;
		}
		#endregion
	}
}
