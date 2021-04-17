using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public static class Resource
    {
        public static Texture2D Blue;
        public static Texture2D Green;
        public static Texture2D Purple;
        public static Texture2D Red;
        public static Texture2D Yellow;

        public static void Init(ContentManager content)
        {
            Blue = content.Load<Texture2D>("blue");
            Green = content.Load<Texture2D>("green");
            Purple = content.Load<Texture2D>("purple");
            Red = content.Load<Texture2D>("red");
            Yellow = content.Load<Texture2D>("yellow");
        }
    }
}