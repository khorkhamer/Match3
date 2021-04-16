using System;
using System.Collections.Generic;

namespace Match3
{
     public enum MarbleSize
     {
         MarbleWidth = 60,
         MarbleHeight = 60
     }
     public enum MarbleColor
     {
         Blue = 1,
         Green,
         Purple,
         Red,
         Yellow
     }

     public class GameObject
     {
         private (float, float) _position;
         private MarbleColor _color;
         private float _verticalAdjustment;

         protected GameObject((float, float) pos, MarbleColor color, float va)
         {
             _position = pos;
             _color = color;
             _verticalAdjustment = va;
         }

         public (float, float) GetPosition() => _position;
         public MarbleColor GetColor() => _color;
         public float GetVerticalAdjustment() => _verticalAdjustment;

         public void SetVerticalAdjustment(float v)
         {
             _verticalAdjustment = v;
         }

         public void ReduceVerticalAdjustment(float v)
         {
             _verticalAdjustment -= v;
         }

         public void ReduceSecondCoordinate(float v)
         {
             _position.Item2 += v;
         }

         public Dictionary<string, (float, float)> GetNeighborhood()
         {
             return new Dictionary<string, (float, float)>()
             {
                 {"Up", (_position.Item1, _position.Item2 - (int) MarbleSize.MarbleHeight)},
                 {"Right", (_position.Item1 + (int) MarbleSize.MarbleWidth, _position.Item2)},
                 {"Down", (_position.Item1, _position.Item2 + (int) MarbleSize.MarbleHeight)},
                 {"Left", (_position.Item1 - (int) MarbleSize.MarbleWidth, _position.Item2)}
             };
         }
     }

     public class Marble : GameObject
     {
         public Marble((float, float) pos, MarbleColor color, float va)
             : base(pos, color, va)
         {
             //
         }
     }
    public class GameState
    {
        private List<GameObject> _state;
        public List<GameObject> Get() => _state;

        public void Add(GameObject go)
        {
            _state.Add(go);
        }
        public bool Contains(float x, float y)
        {
            foreach (var elem in _state)
            {
                if (elem.GetPosition() == (x, y))
                    return true;
            }

            return false;
        }
        public GameState()
        {
            _state = new List<GameObject>(100);
        }
    }
}
