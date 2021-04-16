using System;
using System.Collections.Generic;

namespace Match3
{
    public class GameManager
    {
        private GameState _state;
        private Random _rnd;

        private void Init()
        {
            for (float i = 0; i < 420f; i += 60f)
            {
                _state.Add(CreateMarble(i, 0f));
            }
        }

        private Marble CreateMarble(float x, float y)
        {
            return new Marble((x, y), (MarbleColor) _rnd.Next(1, 5), 60f);
        }

        public List<GameObject> GetState() => _state.Get();

        public GameManager()
        {
            _state = new GameState();
            _rnd = new Random();
            Init();
        }

        public void UpdateState()
        {
            var tmp = new List<GameObject>(100);
            foreach (var elem in _state.Get())
            {
                var neighbor = elem.GetNeighborhood();
                if (!_state.Contains(neighbor["Down"].Item1, neighbor["Down"].Item2)
                    && elem.GetPosition().Item2 < 420f)
                {
                    if (elem.GetVerticalAdjustment() == 0f)
                        elem.SetVerticalAdjustment((float)MarbleSize.MarbleHeight);
                }

                if (elem.GetPosition().Item2 == 0f)
                {
                    tmp.Add(CreateMarble(elem.GetPosition().Item1, neighbor["Up"].Item2));
                }
            }

            foreach (var elem in tmp)
            {
                _state.Add(elem);
            }
        }

        public void UpdateElements(float delta)
        {
            foreach (var elem in _state.Get())
            {
                if (elem.GetVerticalAdjustment() > 0)
                {
                    elem.ReduceVerticalAdjustment(delta);
                    elem.ReduceSecondCoordinate(delta);
                }
            }
        }
    }
}
