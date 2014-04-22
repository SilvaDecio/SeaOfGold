using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Decio.Animation;

namespace Sea_Of_Gold.Code.Fish
{
    enum EnemieType
    {
        Aparelhado , Chefe , Chines , Eletrico , Tubarao
    }

    class Enemie
    {
        Sprite Animation;

        Vector2 Position;

        public Rectangle BoundingRectangle;

        public int Life, Points, Distance;

        protected int Speed;

        EnemieType Type;

        public Enemie(ContentManager Content , Vector2 position , int distance , int speed , EnemieType type)
        {
            Type = type;

            # region Type

            switch (Type)
            {
                case EnemieType.Aparelhado:

                    Animation = new Sprite(Content.Load<Texture2D>("Source/Peixes/Inimigos/Aparelhado"), 33f,
                        "Source/Peixes/Inimigos/Aparelhado.txt", "Aparelhado");

                    Life = 4;
                    Points = 40;

                    break;

                case EnemieType.Chefe:

                    Animation = new Sprite(Content.Load<Texture2D>("Source/Peixes/Inimigos/Chefe"), 33f,
                        "Source/Peixes/Inimigos/Chefe.txt", "Chefe");

                    Life = 4;
                    Points = 40;

                    break;

                case EnemieType.Chines:

                    Animation = new Sprite(Content.Load<Texture2D>("Source/Peixes/Inimigos/Chines"), 33f,
                        "Source/Peixes/Inimigos/Chines.txt", "Chines");

                    Life = 1;
                    Points = 10;

                    break;

                case EnemieType.Eletrico:

                    Animation = new Sprite(Content.Load<Texture2D>("Source/Peixes/Inimigos/Eletrico"), 33f,
                        "Source/Peixes/Inimigos/Eletrico.txt", "Eletrico");

                    Life = 3;
                    Points = 30;

                    break;

                case EnemieType.Tubarao:

                    Animation = new Sprite(Content.Load<Texture2D>("Source/Peixes/Inimigos/Tubarao"), 33f,
                        "Source/Peixes/Inimigos/Tubarao.txt", "Tubarao");

                    Life = 2;
                    Points = 20;

                    break;
            }

            # endregion

            Position = position;

            BoundingRectangle = new Rectangle((int)Position.X , (int)Position.Y , Animation.AnimationRectangle.Width ,
                Animation.AnimationRectangle.Height);

            Distance = distance;

            Speed = speed;
        }

        public void Update(GameTime gametime)
        {
            Position.X -= Speed;

            Position.Y = Distance + (float)(40 * Math.Sin(2 * Math.PI * (gametime.ElapsedGameTime.TotalSeconds / 2) -
                (Position.X / 100)));

            Animation.Update(gametime);

            BoundingRectangle.X = (int)Position.X;
            BoundingRectangle.Y = (int)Position.Y;
            BoundingRectangle.Width = Animation.AnimationRectangle.Width;
            BoundingRectangle.Height = Animation.AnimationRectangle.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch, Position);
        }
    }
}