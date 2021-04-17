using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public enum MarbleColor
    {
        Blue = 1,
        Green,
        Purple,
        Red,
        Yellow
    }
    public enum MarbleSize
    {
        Width = 60,
        Height = Width
    }
    public static class Direction
    {
        public static (double, double) Up = (0.0, -1.0);
        public static (double, double) Right = (1.0, 0.0);
        public static (double, double) Down = (0.0, 1.0);
        public static (double, double) Left = (-1.0, 0.0);
    }
    public class GameObject
    {
        private Sprite _sprite;
        private (int,int) _logicalCoordinate;
        private double _adjustment;
        private (double, double) _worldCoordinate;
        private double _velocity;
        private (double,double) _direction;
        public bool IsMoving { get; set; }

        public void SetSprite(Sprite sp)
        {
            _sprite = sp;
        }

        public Sprite GetSprite() => _sprite;
        public (int,int) GetLogicalCoord() => _logicalCoordinate;
        public (double,double) GetWorldCoord() => _worldCoordinate;

        public (double,double) TranslateCoordinate(int x, int y)
        {
            return ((double) (x * (int) MarbleSize.Width), (double) (y * (int) MarbleSize.Height));
        }

        public void Draw(ref SpriteBatch sB)
        {
            sB.Draw(_sprite.Texture, new Vector2((float)_worldCoordinate.Item1, (float)_worldCoordinate.Item2), Color.White);
        }

        public void Move(double delta)
        {
            if (!IsMoving)
                return;
            var dv = delta * _velocity;
            var dir = (_direction.Item1 * dv, _direction.Item2 * dv);
            _adjustment += dv;
            _worldCoordinate = (_worldCoordinate.Item1 + dir.Item1, _worldCoordinate.Item2 + dir.Item2);
            if ((int)Math.Truncate(_adjustment) == (int)MarbleSize.Width)
            {
                _logicalCoordinate = (_logicalCoordinate.Item1 + (int)Math.Truncate(_direction.Item1),
                    _logicalCoordinate.Item2 + (int)Math.Truncate(_direction.Item2));
                _adjustment = 0;
                IsMoving = false;
            }
        }

        public GameObject((int, int) pos, (double, double) dir, double vel)
        {
            _logicalCoordinate = pos;
            _worldCoordinate = TranslateCoordinate(pos.Item1, pos.Item2);
            _direction = dir;
            _adjustment = (double)MarbleSize.Height;
            _velocity = vel;
        }
    }

    public class Marble : GameObject
    {
        private MarbleColor _color;
        private Random _rnd;
        public MarbleColor GetColor() => _color;

        public Marble((int, int) pos)
            : base(pos, Direction.Down, (double)MarbleSize.Height)
        {
            _rnd = new Random();
            _color = (MarbleColor)_rnd.Next(1, 5);
        }
    }
}