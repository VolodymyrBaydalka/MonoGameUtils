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
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameUtils
{
	public class TextEditBox : TextBlock
	{
		#region Consrants
		private readonly static Dictionary<Keys, char> c_keyMap = new Dictionary<Keys, char>();
        #endregion

        #region Members
        private CountDownTimer _cursorTimer = new CountDownTimer(0.5f, true);
        private bool _cursorVisible = true;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        static TextEditBox()
		{
			// TODO... 
			c_keyMap.Add(Keys.Q, 'q');
			c_keyMap.Add(Keys.W, 'w');
			c_keyMap.Add(Keys.E, 'e');
			c_keyMap.Add(Keys.R, 'r');
			c_keyMap.Add(Keys.T, 't');
			c_keyMap.Add(Keys.Y, 'y');
			c_keyMap.Add(Keys.U, 'u');
			c_keyMap.Add(Keys.I, 'i');
			c_keyMap.Add(Keys.O, 'o');
			c_keyMap.Add(Keys.P, 'p');
			c_keyMap.Add(Keys.A, 'a');
			c_keyMap.Add(Keys.S, 's');
			c_keyMap.Add(Keys.D, 'd');
			c_keyMap.Add(Keys.F, 'f');
			c_keyMap.Add(Keys.G, 'g');
			c_keyMap.Add(Keys.H, 'h');
			c_keyMap.Add(Keys.J, 'j');
			c_keyMap.Add(Keys.K, 'k');
			c_keyMap.Add(Keys.L, 'l');
			c_keyMap.Add(Keys.Z, 'z');
			c_keyMap.Add(Keys.X, 'x');
			c_keyMap.Add(Keys.C, 'c');
			c_keyMap.Add(Keys.V, 'v');
			c_keyMap.Add(Keys.B, 'b');
			c_keyMap.Add(Keys.N, 'n');
			c_keyMap.Add(Keys.M, 'm');
			c_keyMap.Add(Keys.Space, ' ');
            c_keyMap.Add(Keys.D1, '1');
            c_keyMap.Add(Keys.D2, '2');
            c_keyMap.Add(Keys.D3, '3');
            c_keyMap.Add(Keys.D4, '4');
            c_keyMap.Add(Keys.D5, '5');
            c_keyMap.Add(Keys.D6, '6');
            c_keyMap.Add(Keys.D7, '7');
            c_keyMap.Add(Keys.D8, '8');
            c_keyMap.Add(Keys.D9, '9');
            c_keyMap.Add(Keys.D0, '0');

        }
        #endregion

        #region Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite"></param>
        public override void Draw(SpriteBatch sprite)
		{
            if (this.Background != null)
                this.Background.Draw(sprite, this.Bounds, Color.White);

            var font = this.Font;

            if (font != null && !string.IsNullOrEmpty(this.Text))
			{
				string text = this.Text;

				if (this.Manager.FocusedElement == this && _cursorVisible)
				{
					text += "|";
				}

				sprite.DrawString(font, text, new Vector2(m_bounds.X + m_padding, m_bounds.Y + m_padding), this.TextColor);
			}
		}
        public override void Update(GameTime gameTime)
        {
            if (this.Manager.FocusedElement == this && _cursorTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds))
                _cursorVisible = !_cursorVisible;

            base.Update(gameTime);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public override void OnKeyboardChanged(InputComponent input)
		{
            if (input.CurrectKeyboardState.IsKeyDown(Keys.Back))
            {
                if (!string.IsNullOrEmpty(this.Text))
                    this.Text = this.Text.Substring(0, this.Text.Length - 1);
            }
            else if (input.CurrectKeyboardState.IsKeyDown(Keys.Enter)) {
                this.Text += Environment.NewLine;
            }
            else
            {
                bool shift = input.CurrectKeyboardState[Keys.LeftShift] == KeyState.Down;

                foreach (Keys key in c_keyMap.Keys)
                {
                    if (input.CurrectKeyboardState.IsKeyDown(key))
                    {
                        this.Text += shift ? char.ToUpper(c_keyMap[key]) : c_keyMap[key];
                        break;
                    }
                }
            }
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		public override void OnMouseDown(InputComponent input)
		{
			this.Manager.FocusedElement = this;
			base.OnMouseDown(input);
		}
		#endregion
	}
}
