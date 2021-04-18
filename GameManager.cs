using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Match3
{
    public class GameManager
    {
        private GameState _state;
        private double _frameLenght;
        private double _frameCounter;
        private Random _rnd;

        public GameManager()
        {
            _frameLenght = 60.0;
            _frameCounter = 0.0;
            _state = new GameState();
            _rnd = new Random();
            Init();
        }

        public double GetFrameCounter() => _frameCounter;

        public void Init()
        {
            for (var i = 0; i < 8; i++)
            {
                _state.Add(CreateMarble(i, 0));
            }
        }

        public List<GameObject> GetState() => _state.Get();

        public void IncFrameCounter(double delta)
        {
            _frameCounter += (delta * 60);
        }

        public void RefreshFrameCounter()
        {
            _frameCounter = 0.0;
        }

        public bool IsFrameEnd()
        {
//            return Math.Abs(_frameCounter - _frameLenght) < 0.000000001;
            return Math.Round(_frameCounter) == _frameLenght;
        }
        private GameObject CreateMarble(int x, int y)
        {
            var mr = new Marble((x, y), (MarbleColor)_rnd.Next(1, 6));
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
            mr.SetSprite(new Sprite(texture));
            return mr;
        }

        public void UpdateState()
        {
            if (IsFrameEnd())
            {
                foreach (var elem in _state.Get())
                {
                    if (elem.GetLogicalCoord().Y < 7)
                    {
                        if (!_state.Contains((elem.GetLogicalCoord().X, elem.GetLogicalCoord().Y + 1)))
                            elem.IsMoving = true;
                    }

                    if (elem.GetLogicalCoord().Y == 7)
                        elem.IsMoving = false;
                }
            }
        }

        public void UpdateLocalCoord()
        {
            if (IsFrameEnd())
            {
                foreach (var elem in _state.Get())
                {
                    if (elem.IsMoving)
                    {
                        elem.MoveInLocalCoord();
                        var coord = elem.TranslateToWorldCoordinate(elem.GetLogicalCoord());
                        elem.SetWorldCoordinate(coord);
                    }
                }
                RefreshFrameCounter();
            }
        }

        public void UpdateWorldCoord(double delta)
        {
            if (!IsFrameEnd())
            {
                foreach (var elem in _state.Get())
                {
                    if (elem.IsMoving)
                        elem.MoveInWorldCoord(delta);
                }
            }
        }
    }
}