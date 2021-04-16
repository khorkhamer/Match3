namespace Match3
{
    enum MarbleColor
    {
        Blue = 1,
        Green,
        Purple,
        Red,
        Yellow
    }
    public static class Direction
    {
        public static (float, float) Up = (0f, 1f);
        public static (float, float) Right = (1f, 0f);
        public static (float, float) Down = (0f, -1f);
        public static (float, float) Left = (-1f, 0f);
    }
    public class GameObject
    {
        private (int, int) _logicalCoordinate;
        private float _adjustment;
        private (float, float) _wordCoordinate;
        private bool _isMoving;
        private float _velocity;
        private (float, float) _direction;
        public (int, int) GetLogicalCoord() => _logicalCoordinate;
    }
}