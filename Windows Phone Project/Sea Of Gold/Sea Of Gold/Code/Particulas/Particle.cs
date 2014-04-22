using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;

namespace Sea_Of_Gold.Code.Particulas
{
    class Particle
    {
        public Texture2D Image;

        public Vector2 Position , Speed;

        public Rectangle BoundingRectangle;

        public float Angle , AngularSpeed , LifeTime;

        public Particle(ContentManager Content , Vector2 position, Vector2 speed , float angle , float angularSpeed , float lifeTime)
        {
            Image = Content.Load<Texture2D>("Source/Bubbles/Bubble");
            
            Position = position;

            BoundingRectangle = new Rectangle((int)Position.X , (int)Position.Y , Image.Width, Image.Height);

            Speed = speed;
            
            Angle = angle;
            
            AngularSpeed = angularSpeed;
            
            LifeTime = lifeTime;
        }

        public void Update()
        {
            LifeTime--;

            Position += Speed;

            Angle += AngularSpeed;

            BoundingRectangle.X = (int)Position.X;
            BoundingRectangle.Y = (int)Position.Y;

            if (Position.X >= 410 || Position.Y <= 40 - 16.5f || Position.Y >= 40 + 16.5f)
            {
                LifeTime = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, BoundingRectangle, Color.White, Angle, new Vector2(Image.Width / 2, Image.Height / 2) ,
                1f, SpriteEffects.None, 0f);
        }
    }
}