using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Match3
{
    public class GameManager
    {
        private GameState _state;
        private double _frameCount = (double)60;

        private GameObject CreateMarble(int x, int y)
        {
            var mr = new Marble((x, y));
            var texture = Resource.Yellow;
            switch (mr.GetColor())
            {
                case MarbleColor.Blue:
                    texture = Resource.Blue;
                    break;
                case MarbleColor.Green:
                    texture = Resource.Green;
                    break;
                case MarbleColor.Purple:
                    texture = Resource.Purple;
                    break;
                case MarbleColor.Red:
                    texture = Resource.Red;
                    break;
                default:
                    break;
            }
            var sp = new Sprite(texture);
            mr.SetSprite(sp);
            return mr;
        }

        private void Init()
        {
            for (var i = 0; i < 8; i++)
            {
                _state.Add(CreateMarble(i, 0));
            }
        }

        public List<GameObject> GetState() => _state.Get();

        public GameManager()
        {
            _state = new GameState();
            Init();
        }

        public void UpdateState(double delta)
        {
            if ((int) Math.Truncate(_frameCount) < 60)
            {
                _frameCount += (delta * 60);
                return;
            }

            var tmp = new List<GameObject>(100);
            foreach (var elem in _state.Get())
            {
                if (elem.GetLogicalCoord().Item2 < 7)
                {
                    if (!_state.Contains((elem.GetLogicalCoord().Item1, elem.GetLogicalCoord().Item2 + 1)))
                    {
                        elem.IsMoving = true;
                    }
                }

                if (_state.Get().Count < 64 && (int)Math.Truncate(_frameCount) == 60)
                {
                    if (!_state.Contains((elem.GetLogicalCoord().Item1, elem.GetLogicalCoord().Item2 - 1)))
                        tmp.Add(CreateMarble(elem.GetLogicalCoord().Item1, -1));
                }
            }

            foreach (var elem in tmp)
            {
                _state.Add(elem);
            }

            _frameCount = (double)0;
        }

        public void MoveElements(double delta)
        {
            foreach (var elem in _state.Get())
            {
                elem.Move(delta);
            }
        }
    }
}