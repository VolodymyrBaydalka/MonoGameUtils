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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameUtils
{
	/// <summary>
	/// This class will be instantiated by the XNA Framework Content
	/// Pipeline to read the specified data type from binary .xnb format.
	/// 
	/// Unlike the other Content Pipeline support classes, this should
	/// be a part of your main game project, and not the Content Pipeline
	/// Extension Library project.
	/// </summary>
	public class Animation2DReader : ContentTypeReader<Animation2D>
	{
		protected override Animation2D Read(ContentReader input, Animation2D existingInstance)
		{
			bool repeat = input.ReadBoolean();
			Texture2D texture = input.ReadObject<Texture2D>();
			int count = input.ReadInt32();
			Animation2DFrame[] frames = new Animation2DFrame[count];

			for (int i = 0; i < count; i++)
			{
				Rectangle? rect = input.ReadObject<Rectangle?>();
				Texture2D subTexture = input.ReadObject<Texture2D>();
				float time = input.ReadSingle();

				frames[i] = new Animation2DFrame(subTexture == null ? texture : subTexture, rect, time);
			}

			return new Animation2D(frames, repeat);
		}
	}
}
