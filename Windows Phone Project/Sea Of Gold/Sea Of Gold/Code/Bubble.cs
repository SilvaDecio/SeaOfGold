using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Sea_Of_Gold.Code
{
    enum BubbleType
    {
        Oxigen , Carbon
    }

    class Bubble
    {
        Texture2D Image;

        Vector2 Position;

        public Rectangle BoundingRectangle;

        float Acceleration , Distance;

        BubbleType Type;

        Random Raffle;

        public Bubble(ContentManager Content , Vector2 position , BubbleType type)
        {
            Raffle = new Random();

            Type = type;

            switch (Type)
            {
                case BubbleType.Oxigen:

                    Image = Content.Load<Texture2D>("Source/Bubbles/Oxigen");

                    Position = new Vector2(800 + Image.Width , Raffle.Next(100 , 400));

                    break;

                case BubbleType.Carbon:

                    Image = Content.Load<Texture2D>("Source/Bubbles/Carbon");

                    Position = position;

                    break;
            }

            BoundingRectangle = new Rectangle ((int)Position.X , (int)Position.Y , Image.Width , Image.Height);
            
            Distance = new Random().Next(100 , 400);
        }

        public void Update(GameTime gameTime)
        {
            switch (Type)
            {
                case BubbleType.Oxigen:

                    Acceleration += 0.005f;

                    Position.X -= Acceleration;

                    Position.Y = Distance + (float)(40 * Math.Sin(2 * Math.PI * (gameTime.ElapsedGameTime.TotalSeconds / 2) -
                        (Position.X / 100)));

                    break;

                case BubbleType.Carbon:

                    Acceleration += 0.5f;

                    Position.X += Acceleration;

                    break;
            }

            BoundingRectangle.X = (int)Position.X;
            BoundingRectangle.Y = (int)Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Image, Position, Color.White);
        }
    }
}