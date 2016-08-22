using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameUtils
{
    public class FpsCounter : TextBlock
    {
        private CountDownTimer _timer = new CountDownTimer(1, true);
        private int _frames = 0;

        public override void Update(GameTime gameTime)
        {
            if (_timer.Update((float)gameTime.ElapsedGameTime.TotalSeconds))
            {
                this.Text = string.Format("Fps: {0}", _frames);
                _frames = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sprite)
        {
            base.Draw(sprite);
            _frames++;
        }
    }
}
