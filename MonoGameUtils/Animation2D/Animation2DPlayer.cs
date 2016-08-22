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
    public class Animation2DPlayer
    {
        #region Members
        private float m_time;
        private Animation2D m_anim;
        private Animation2DFrame m_current;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Animation2DFrame Current
        {
            get
            {
                return m_current;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Finished
        {
            get
            {
                return m_time > m_anim.TotalSeconds;
            }
        }
        public Animation2D Animation
        {
            get { return m_anim; }
            set
            {
                m_anim = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="anim"></param>
        public Animation2DPlayer()
        {
        }
        #endregion

        #region Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds"></param>
        public void Update(float seconds)
        {
            if (m_anim != null)
            {
                m_time += seconds;
                m_current = m_anim[m_time];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="rect"></param>
        public void Draw(SpriteBatch sprite, Rectangle rect, Color? color = null)
        {
            if(m_current != null)
                sprite.Draw(m_current.Texture, rect, m_current.Rect, color ?? Color.White);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="rect"></param>
        public void Draw(SpriteBatch sprite, Rectangle rect, Rectangle sourceRect)
        {
            if (m_current == null)
                return;

            if (m_current.Rect.HasValue)
            {
                sourceRect.X += m_current.Rect.Value.X;
                sourceRect.Y += m_current.Rect.Value.Y;
            }

            sprite.Draw(m_current.Texture, rect, m_current.Rect, Color.White);
        }
        #endregion
    }
}
