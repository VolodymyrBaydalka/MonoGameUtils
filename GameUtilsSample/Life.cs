using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameUtils;

namespace GameUtilsSample
{

    enum Neighbors
    {
        None = 0x00,
        TopLeft = 0x01,
        Top = 0x02,
        TopRight = 0x04,
        Left = 0x08,
        Right = 0x10,
        BottomLeft = 0x20,
        Bottom = 0x40,
        BottomRight = 0x80,
    }

    class Life
    {
        #region Constants
        public const int CellSize = 8;
        #endregion

        #region Members
        private CountDownTimer _timer = new CountDownTimer(0.5f, true);

        private readonly int _width;
        private readonly int _height;

        private bool[,] m_cells = null;
        private bool[,] m_temp = null;

        private Texture2D _mask;
        #endregion

        #region Properties
        public CountDownTimer Timer { get { return _timer; } }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xCount"></param>
        /// <param name="yCount"></param>
        public Life(int xCount, int yCount)
        {
            _width = xCount;
            _height = yCount;

            m_cells = new bool[xCount, yCount];
            m_temp = new bool[xCount, yCount];
        }
        #endregion

        #region Public 
        public void LoadContent(Game game)
        {
            _mask = new Texture2D(game.GraphicsDevice, m_cells.GetLength(0), m_cells.GetLength(1));
        }
        #endregion

        #region Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Born(int x, int y)
        {
            if (x >= 0 && x < m_cells.GetLength(0) && y >= 0 && y < m_cells.GetLength(1))
            {
                m_cells[x, y] = true;
                m_temp[x, y] = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Kill(int x, int y)
        {
            if (x >= 0 && x < m_cells.GetLength(0) && y >= 0 && y < m_cells.GetLength(1))
            {
                m_cells[x, y] = false;
                m_temp[x, y] = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool IsLive(int x, int y)
        {
            if (x >= 0 && x < m_cells.GetLength(0) && y >= 0 && y < m_cells.GetLength(1))
                return m_cells[x, y];

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colors = new Color[_mask.Width * _mask.Height];

            for (int y = 0; y < _height; y++)
                for (int x = 0; x < _width; x++)
                {
                    var oldColor = m_temp[x, y] ? new Color(0x00, 0x07F, 0x0E) : Color.Transparent;
                    var newColor = m_cells[x, y] ? new Color(0x00, 0x07F, 0x0E) : Color.Transparent;

                    colors[x + y * _mask.Width] = Color.Lerp(oldColor, newColor, _timer.Progress);
                }


            _mask.SetData(colors);
            spriteBatch.Draw(_mask, new Rectangle(0, 0, _mask.Width * CellSize, _mask.Height * CellSize), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (_timer.Update(gameTime))
                Next();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Next()
        {
            for (int i = 0; i < m_cells.GetLength(0); i++)
            {
                for (int j = 0; j < m_cells.GetLength(1); j++)
                {
                    var neighbors = GetNeighbors(i, j);

                    if (m_cells[i, j])
                    {
                        if (neighbors.Item2 == 2 || neighbors.Item2 == 3)
                        {
                            m_temp[i, j] = true;
                        }
                        else
                        {
                            m_temp[i, j] = false;
                        }
                    }
                    else
                    {
                        if (neighbors.Item2 == 3)
                        {
                            m_temp[i, j] = true;
                        }
                        else
                        {
                            m_temp[i, j] = false;
                        }
                    }
                }
            }

            bool[,] temp = m_cells;
            m_cells = m_temp;
            m_temp = temp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private Tuple<Neighbors, int> GetNeighbors(int x, int y)
        {
            int count = 0;
            var neighbors = Neighbors.None;

            if (IsLive(x - 1, y - 1))
            {
                neighbors |= Neighbors.TopLeft;
                count++;
            }

            if (IsLive(x - 1, y))
            {
                neighbors |= Neighbors.Left;
                count++;
            }

            if (IsLive(x - 1, y + 1))
            {
                neighbors |= Neighbors.BottomLeft;
                count++;
            }

            if (IsLive(x, y - 1))
            {
                neighbors |= Neighbors.Top;
                count++;
            }
            if (IsLive(x, y + 1))
            {
                neighbors |= Neighbors.Bottom;
                count++;
            }

            if (IsLive(x + 1, y - 1))
            {
                neighbors |= Neighbors.TopRight;
                count++;
            }

            if (IsLive(x + 1, y))
            {
                neighbors |= Neighbors.Right;
                count++;
            }

            if (IsLive(x + 1, y + 1))
            {
                neighbors |= Neighbors.BottomRight;
                count++;
            }

            return Tuple.Create(neighbors, count);
        }
        #endregion
    }
}
