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
        public static (int X,int Y) Up = (0, -1);
        public static (int X,int Y) Right = (1, 0);
        public static (int X,int Y) Down = (0, 1);
        public static (int X,int Y) Left = (-1, 0);
    }
    public class GameObject
    {
        private Sprite _sprite;
        private (int X,int Y) _logicalCoordinate;
        private (double X,double Y) _worldCoordinate;
        private double _adjustment;
        private double _velocity;
        private (int X,int Y) _direction;
        public bool IsMoving { get; set; }

        public void SetSprite(Sprite sp)
        {
            _sprite = sp;
        }

        public void SetWorldCoordinate((double X, double Y) coord)
        {
            _worldCoordinate = coord;
        }

        public (int X,int Y) GetLogicalCoord() => _logicalCoordinate;
        public (double X,double Y) GetWorldCoord() => _worldCoordinate;

        public (double X,double Y) TranslateToWorldCoordinate(int x, int y)
        {
            return ((double)(x * (int) MarbleSize.Width), (double)(y * (int) MarbleSize.Height));
        }

        public void Draw(ref SpriteBatch sB)
        {
            sB.Draw(_sprite.Texture, new Vector2((float)_worldCoordinate.X, (float)_worldCoordinate.Y), Color.White);
        }
/*
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
                _logicalCoordinate = (_logicalCoordinate.Item1 + _direction.Item1,
                    _logicalCoordinate.Item2 + _direction.Item2);
                _adjustment = 0;
                IsMoving = false;
            }
        }
*/
        public void MoveInLocalCoord()
        {
            _logicalCoordinate = (_logicalCoordinate.X + _direction.X, _logicalCoordinate.Y + _direction.Y);
            IsMoving = false;
        }

        public void MoveInWorldCoord(double delta)
        {
            var v = delta * 60.0;
            _worldCoordinate = (_worldCoordinate.X + (v * _direction.X), _worldCoordinate.Y + (v * _direction.Y));
            Console.WriteLine("{0} -- {1}", _worldCoordinate.X, _worldCoordinate.Y);
        }

        public GameObject((int X,int Y) pos, (int X,int Y) dir, double vel)
        {
            _logicalCoordinate = pos;
            _worldCoordinate = TranslateToWorldCoordinate(pos.X, pos.Y);
            _direction = dir;
            _adjustment = 0;
            _velocity = vel;
        }
    }

    public class Marble : GameObject
    {
        private MarbleColor _color;
//        private Random _rnd;
        public MarbleColor GetColor() => _color;

        public Marble((int, int) pos, MarbleColor color)
            : base(pos, Direction.Down, (double)MarbleSize.Height)
        {
//            _rnd = new Random();
//            _color = (MarbleColor)_rnd.Next(1, 5);
            _color = color;
        }
    }
}