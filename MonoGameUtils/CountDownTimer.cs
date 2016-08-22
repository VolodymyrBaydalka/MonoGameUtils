using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUtils
{
    public class CountDownTimer
    {
        private float _time;
        private bool _repeatable;
        private float _interval;

        public float Time
        {
            get;
            set;
        }

        public float Progress
        {
            get
            {
                return _time / _interval;
            }
        }

        public bool Repeatable
        {
            get { return _repeatable; }
            set { _repeatable = value; }
        }

        public float Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public CountDownTimer(float interval, bool repeatable) {
            _interval = interval;
            _repeatable = repeatable;
        }

        public bool Update(float seconds) {

            _time += seconds;

            if (_time > _interval)
            {
                if (_repeatable)
                {
                    _time -= _interval;
                }

                return true;
            }

            return false;
        }
        public bool Update(GameTime gameTime)
        {
            return Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
