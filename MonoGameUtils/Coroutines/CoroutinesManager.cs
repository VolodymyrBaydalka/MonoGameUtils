using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUtils
{
    public class CoroutinesManager : GameComponent
    {
        private readonly List<IEnumerator> _coroutines = new List<IEnumerator>();

        public CoroutinesManager(Game game) : base(game)
        {
        }

        public void Start(IEnumerator enumerator)
        {
            _coroutines.Add(enumerator);
        }

        public void Stop(IEnumerator enumerator)
        {
            _coroutines.Remove(enumerator);
        }

        public void Update()
        {
            for (int i = _coroutines.Count - 1; i >= 0; i--)
            {
                IEnumerator enumerator = _coroutines[i];

                if (!enumerator.MoveNext())
                    _coroutines.RemoveAt(i);
            }
        }
    }
}
