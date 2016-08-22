using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUtils
{ 
    public static class Extentions
    {
        public static IEnumerator StartCoroutine(this Game game, IEnumerator coroutine) {
            var manager = game.Services.GetService<CoroutinesManager>();

            if (manager == null)
                throw new NullReferenceException("Can't found CoroutinesManager service");

            manager.Start(coroutine);

            return coroutine;
        }

        public static IEnumerator StopCoroutine(this Game game, IEnumerator coroutine)
        {
            var manager = game.Services.GetService<CoroutinesManager>();

            if (manager == null)
                throw new NullReferenceException("Can't found CoroutinesManager service");

            manager.Stop(coroutine);

            return coroutine;
        }
    }
}
