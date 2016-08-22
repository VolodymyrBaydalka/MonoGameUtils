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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace GameUtils
{
    /// <summary>
    /// 
    /// </summary>
    public class GuiContainer : GuiElement
    {
        #region Members
        protected ObservableCollection<GuiElement> _children = new ObservableCollection<GuiElement>();
        #endregion

        #region Properties
        public ObservableCollection<GuiElement> Children
        {
            get { return _children; }
        }
        #endregion

        #region Constructor
        public GuiContainer()
        {
            _children.CollectionChanged += OnChildrenChanged;
        }
        public GuiContainer(params GuiElement[] content)
            : this()
        {
            if (content != null)
                foreach (var item in content)
                {
                    _children.Add(item);
                }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override GuiElement HitTest(int x, int y)
        {
            GuiElement result = null;

            for (int i = 0; i < _children.Count && result == null; i++)
            {
                result = _children[i].HitTest(x, y);
            }

            return result == null ? base.HitTest(x, y) : result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite"></param>
        public override void Draw(SpriteBatch sprite)
        {
            base.Draw(sprite);

            foreach (GuiElement element in _children)
            {
                element.Draw(sprite);
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (GuiElement element in _children)
            {
                element.Update(gameTime);
            }

            base.Update(gameTime);
        }
        #endregion

        #region Implementation
        protected virtual void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (GuiElement item in e.NewItems)
                    item.Parent = this;

            if (e.OldItems != null)
                foreach (GuiElement item in e.OldItems)
                    item.Parent = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        protected override void OnLayout(Rectangle rect)
        {
            foreach (GuiElement element in _children)
            {
                element.Layout(rect);
            }

            base.OnLayout(rect);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected override Point OnMeasure(Point point)
        {
            foreach (GuiElement element in _children)
            {
                element.Measure(point);
            }

            return base.OnMeasure(point);
        }
        #endregion
    }
}
