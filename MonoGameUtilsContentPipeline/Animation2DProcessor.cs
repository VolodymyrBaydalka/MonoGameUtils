#region License
/*******************************************************************************
 * Copyright 2013 Volodymyr Baydalka.
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System.ComponentModel;

namespace Anim2DContentPipeline
{
	/// <summary>
	/// This class will be instantiated by the XNA Framework Content Pipeline
	/// to apply custom processing to content data, converting an object of
	/// type TInput to TOutput. The input and output types may be the same if
	/// the processor wishes to alter data without changing its type.
	///
	/// This should be part of a Content Pipeline Extension Library project.
	///
	/// TODO: change the ContentProcessor attribute to specify the correct
	/// display name for this processor.
	/// </summary>
	[ContentProcessor(DisplayName = "MonoGameUtils - Animation2DProcessor")]
	public class Animation2DProcessor : ContentProcessor<Texture2DContent, Animation2DContent>
	{
		#region Members
		private bool m_repeat = false;
		private int m_frameWidth = -1;
		private int m_frameHeight = -1;
		private float m_timeInterval = 1;
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		[DefaultValue(false)]
		public bool Repeat 
		{
			get
			{
				return m_repeat;
			}
			set
			{
				m_repeat = value;
			}
		}
		[DefaultValue(-1)]
		public int FrameWidth
		{
			get
			{
				return m_frameWidth;
			}
			set
			{
				m_frameWidth = value;
			}
		}
		[DefaultValue(-1)]
		public int FrameHeight
		{
			get
			{
				return m_frameHeight;
			}
			set
			{
				m_frameHeight = value;
			}
		}
		[DefaultValue(1f)]
		public float TimeInterval
		{
			get
			{
				return m_timeInterval;
			}
			set
			{
				m_timeInterval = value;
			}
		}
		#endregion

		#region Implementation
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public override Animation2DContent Process(Texture2DContent input, ContentProcessorContext context)
		{
			Animation2DContent result = new Animation2DContent();

			result.Texture = input;
			result.Repeat = m_repeat;

			if (m_frameWidth < 0 || m_frameHeight < 0)
			{
				result.Frames.Add(new Animation2DContent.Frame());

				return result;
			}

			int width = input.Mipmaps[0].Width;
			int height = input.Mipmaps[0].Height;

			int columns = width / m_frameWidth;
			int rows = height / m_frameHeight;

			for (int y = 0; y < rows; y++)
			{
				for (int x = 0; x < columns; x++)
				{
					Animation2DContent.Frame frame = new Animation2DContent.Frame();

					frame.Delta = m_timeInterval;
					frame.Rect = new Rectangle(x * m_frameWidth, y * m_frameHeight, m_frameWidth, m_frameHeight);

					result.Frames.Add(frame);
				}
			}

			return result;
		}
		#endregion
	}
}