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
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using GameUtils;

namespace Anim2DContentPipeline
{
	/// <summary>
	/// This class will be instantiated by the XNA Framework Content Pipeline
	/// to write the specified data type into binary .xnb format.
	///
	/// This should be part of a Content Pipeline Extension Library project.
	/// </summary>
	[ContentTypeWriter]
	public class Animation2DWriter : ContentTypeWriter<Animation2DContent>
	{
		protected override void Write(ContentWriter output, Animation2DContent value)
		{
			output.Write(value.Repeat);
			output.WriteObject(value.Texture);
			output.Write(value.Frames.Count);

			foreach (Animation2DContent.Frame frame in value.Frames)
			{
				output.WriteObject(frame.Rect);
				output.WriteObject(frame.Texture);
				output.Write(frame.Delta);
			}
		}

		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			return typeof(Animation2DReader).AssemblyQualifiedName;
		}
	}
}
