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
    class Gold
    {
        Texture2D Image;
        
        Vector2 Position;

        public Rectangle BoundingRectangle;

        float Acceleration, Distance;
        
        Random Raffle;

         public Gold(ContentManager Content)
        {
            Raffle = new Random();

            Image = Content.Load<Texture2D>("Source/Gold");
            
            Position = new Vector2(800 + Image.Width, Raffle.Next(100, 400));

            BoundingRectangle = new Rectangle((int)Position.X , (int)Position.Y , Image.Width , Image.Height);

            Distance = Raffle.Next(100, 400);
            
            Acceleration = 0f;
        }

        public void Update(GameTime gametime)
        {
            Acceleration += 0.005f;

            Position.X -= Acceleration;

            Position.Y = Distance + (float)(40 * Math.Sin(2 * Math.PI * (gametime.ElapsedGameTime.TotalSeconds / 2) -
                (Position.X / 100)));

            BoundingRectangle.X = (int)Position.X;
            BoundingRectangle.Y = (int)Position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, Color.White);
        }
    }
}