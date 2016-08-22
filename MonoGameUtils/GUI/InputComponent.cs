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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace GameUtils
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputComponent : GameComponent
	{
		#region Members
		private MouseState m_lastMouseState;
		private MouseState m_currectMouseState;

		private KeyboardState m_lastKeyboardState;
		private KeyboardState m_currectKeyboardState;
		#endregion

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public MouseState LastMouseState
		{
			get { return m_lastMouseState; }
		}
		/// <summary>
		/// 
		/// </summary>
		public MouseState CurrectMouseState
		{
			get { return m_currectMouseState; }
		}
		/// <summary>
		/// 
		/// </summary>
		public KeyboardState LastKeyboardState
		{
			get { return m_lastKeyboardState; }
		}
		/// <summary>
		/// 
		/// </summary>
		public KeyboardState CurrectKeyboardState
		{
			get { return m_currectKeyboardState; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		public bool IsLeftClick
		{
			get
			{
				return (m_lastMouseState.LeftButton == ButtonState.Pressed)
					&& (m_currectMouseState.LeftButton == ButtonState.Released);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsRightClick
		{
			get
			{
				return (m_lastMouseState.RightButton == ButtonState.Pressed)
					&& (m_currectMouseState.RightButton == ButtonState.Released);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsMouseUp
		{
			get
			{
				if (m_lastMouseState.LeftButton == ButtonState.Pressed
					&& m_currectMouseState.LeftButton == ButtonState.Released) return true;
				if (m_lastMouseState.RightButton == ButtonState.Pressed
					&& m_currectMouseState.RightButton == ButtonState.Released) return true;
				if (m_lastMouseState.MiddleButton == ButtonState.Pressed
					&& m_currectMouseState.MiddleButton == ButtonState.Released) return true;
				if (m_lastMouseState.XButton1 == ButtonState.Pressed
					&& m_currectMouseState.XButton1 == ButtonState.Released) return true;
				if (m_lastMouseState.XButton2 == ButtonState.Pressed
					&& m_currectMouseState.XButton2 == ButtonState.Released) return true;

				return false;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsMouseDown
		{
			get
			{
				if (m_lastMouseState.LeftButton == ButtonState.Released
					&& m_currectMouseState.LeftButton == ButtonState.Pressed) return true;
				if (m_lastMouseState.RightButton == ButtonState.Released
					&& m_currectMouseState.RightButton == ButtonState.Pressed) return true;
				if (m_lastMouseState.MiddleButton == ButtonState.Released
					&& m_currectMouseState.MiddleButton == ButtonState.Pressed) return true;
				if (m_lastMouseState.XButton1 == ButtonState.Released
					&& m_currectMouseState.XButton1 == ButtonState.Pressed) return true;
				if (m_lastMouseState.XButton2 == ButtonState.Released
					&& m_currectMouseState.XButton2 == ButtonState.Pressed) return true;

				return false;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsMouseMove
		{
			get
			{
				return m_currectMouseState.X != m_lastMouseState.X
					|| m_currectMouseState.Y != m_lastMouseState.Y;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsMouseChanged
		{
			get
			{
				return m_currectMouseState != m_lastMouseState;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsKeyboardChanged
		{
			get
			{
				return m_currectKeyboardState != m_lastKeyboardState;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public Point MousePosition
		{
			get
			{
				return new Point(m_currectMouseState.X, m_currectMouseState.Y);
			}
			set
			{
				Mouse.SetPosition(value.X, value.Y);
			}
		}
		#endregion

		#region Constructor
		/// <summary>
		/// 
		/// </summary>
		/// <param name="game"></param>
		public InputComponent(Game game)
			: base(game)
		{
		}
		#endregion

		#region Implementation
		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			m_currectKeyboardState = Keyboard.GetState();
			m_lastKeyboardState = m_currectKeyboardState;

			m_currectMouseState = Mouse.GetState();
			m_lastMouseState = m_currectMouseState;

			base.Initialize();
		}
		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			m_lastKeyboardState = m_currectKeyboardState;
			m_currectKeyboardState = Keyboard.GetState();

			m_lastMouseState = m_currectMouseState;
			m_currectMouseState = Mouse.GetState();

			base.Update(gameTime);
		}
		#endregion
	}
}