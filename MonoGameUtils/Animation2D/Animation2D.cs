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
	/// <summary>
	/// 
	/// </summary>
	public class Animation2D
	{
		#region Members
		private Animation2DFrame[] m_frames;
		private float m_totalTime;
		private bool m_repeat;
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Animation2DFrame this[int index]
		{
			get
			{
				return m_frames[index];
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Animation2DFrame this[float time]
		{
			get
			{
				if (m_repeat)
					time = time % m_totalTime;
				
				if(time < m_totalTime)
				{ 
					foreach (Animation2DFrame frame in m_frames)
					{
						if (time < frame.Delta)
						{
							return frame;
						}

						time -= frame.Delta;
					}
				}

				return m_frames[m_frames.Length - 1];
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int FramesCount
		{
			get
			{
				return m_frames.Length;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public float TotalSeconds
		{
			get
			{
				return m_totalTime;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Repeat
		{
			get
			{
				return m_repeat;
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// 
		/// </summary>
		/// <param name="frames"></param>
		public Animation2D(Animation2DFrame[] frames, bool repeat)
		{
			m_frames = frames;
			m_repeat = repeat;

			foreach (Animation2DFrame frame in m_frames)
			{
				m_totalTime += frame.Delta;
			}
		}
		#endregion

		#region Implementation
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public Animation2DPlayer CreatePlayer()
		{
			return new Animation2DPlayer { Animation = this };
		}
		#endregion
	}
}
