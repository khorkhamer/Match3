using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public struct Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;

        public Sprite(Texture2D tex, Vector2 pos)
        {
            Texture = tex;
            Position = pos;
        }
    }
    public static class Renderer
    {
        public static void Draw(ref SpriteBatch sb, List<GameObject> state)
        {
            foreach (var elem in state)
            {
                sb.Draw(elem.GetSprite().Texture, elem.GetSprite().Position, Color.White);
            }
        }
    }
}