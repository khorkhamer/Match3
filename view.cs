using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Match3
{
    public struct Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;
//        public Rectangle SourceRec;
        public Color Color;
        /*
        public float Rotation;
        public Vector2 Origin;
        public SpriteEffects Effects;
        public float LayerDepth;

        public Sprite(Texture2D tex, Vector2 pos, Rectangle rec, Color color,
            float rot, Vector2 or, SpriteEffects ef, float lay)
        {
            Texture = tex;
            Position = pos;
            SourceRec = rec;
            Color = color;
            Rotation = rot;
            Origin = or;
            Effects = ef;
            LayerDepth = lay;
        }
        */
        public Sprite(Texture2D tex, Vector2 pos, Color color)
        {
            Texture = tex;
            Position = pos;
            Color = color;
        }
    }
    public static class Resource
    {
        public static Texture2D BlueMarble;
        public static Texture2D GreenMarble;
        public static Texture2D PurpleMarble;
        public static Texture2D RedMarble;
        public static Texture2D YellowMarble;

        public static void Init(ContentManager content)
        {
            BlueMarble = content.Load<Texture2D>("blue");
            GreenMarble = content.Load<Texture2D>("green");
            PurpleMarble = content.Load<Texture2D>("purple");
            RedMarble = content.Load<Texture2D>("red");
            YellowMarble = content.Load<Texture2D>("yellow");
        }
    }

    public class Renderer
    {
        private List<Sprite> _renderStruct;
        private (int, int) Translate(int x, int y) => (x, y);
        public void UpdateRenderStruct(List<GameObject> actualGameState)
        {
            foreach (var elem in actualGameState)
            {
                Texture2D texture = Resource.YellowMarble;
                switch (elem.GetColor())
                {
                    case MarbleColor.Blue:
                        texture = Resource.BlueMarble;
                        break;
                    case MarbleColor.Green:
                        texture = Resource.GreenMarble;
                        break;
                    case MarbleColor.Purple:
                        texture = Resource.PurpleMarble;
                        break;
                    case MarbleColor.Red:
                        texture = Resource.RedMarble;
                        break;
                    default:
                        break;
                }
                _renderStruct.Add(new Sprite(texture, new Vector2(elem.GetPosition().Item1, elem.GetPosition().Item2), Color.White));
            }
        }

        public void Draw(ref SpriteBatch sp)
        {
            foreach (var elem in _renderStruct)
            {
                sp.Draw(elem.Texture, elem.Position, elem.Color);
            }
            _renderStruct.Clear();
        }

        public void QDraw(ref SpriteBatch sp)
        {
            sp.Draw(Resource.BlueMarble, new Vector2(60, 0), Color.White);
        }
        public Renderer()
        {
            _renderStruct = new List<Sprite>(100);
        }
    }
}
