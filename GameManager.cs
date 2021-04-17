using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Match3
{
    public class GameManager
    {
        private GameState _state;

        private GameObject CreateMarble(int x, int y)
        {
            var go = new Marble((x, y));
            var texture = Resource.Yellow;
            switch (go.GetColor())
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
            go.SetSprite(new Sprite(texture,
                new Vector2((float)go.GetWorldCoord().Item1, (float)go.GetWorldCoord().Item2)));
            return go;
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

        public void UpdateState()
        {
            foreach (var elem in _state.Get())
            {
                if (elem.GetLogicalCoord().Item2 != 7)
                {
                    if (!_state.Contains((elem.GetLogicalCoord().Item1, elem.GetLogicalCoord().Item2 - 1)))
                    {
                        elem.IsMoving = true;
                    }
                }
            }
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