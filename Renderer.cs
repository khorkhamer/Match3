using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public struct Sprite
    {
        public Texture2D Texture;

        public Sprite(Texture2D tex)
        {
            Texture = tex;
        }
    }
    public static class Renderer
    {
        public static void Draw(ref SpriteBatch sb, List<GameObject> state)
        {
            foreach (var elem in state)
            {
                elem.Draw(ref sb);
            }
        }
    }
}