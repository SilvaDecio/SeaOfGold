using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;

using Microsoft.Devices.Sensors;

using Decio.Animation;

namespace Sea_Of_Gold.Code.Fish
{
    class Jogador
    {
        Sprite Animation , Tooth;

        Vector2 Position;

        public Rectangle BoundingRectangle;

        public int Points;

        public bool colidiu, bocaAberta;

        float alpha = 1 , a = 0.1f , contBocaAberta;

        Accelerometer LocalAccelerometer;

        Vector2 LocalReading , Speed;

        Viewport Screen;

        public Jogador(ContentManager Content , Viewport screen)
        {            
            Screen = screen;
            
            Animation = new Sprite(Content.Load<Texture2D>("Source/Peixes/Jogador/Flash"), 44f, "Source/Peixes/Jogador/Flash.txt", "Flash");
            
            Tooth = new Sprite(Content.Load<Texture2D>("Source/Peixes/Jogador/Tooth"), 44f, "Source/Peixes/Jogador/Tooth.txt", "Tooth");

            Position = new Vector2((Screen.Width - Animation.AnimationRectangle.Width) / 2,
                (Screen.Height - Animation.AnimationRectangle.Height) / 2);

            BoundingRectangle = new Rectangle((int)Position.X, (int)Position.Y, Animation.AnimationRectangle.Width, Animation.AnimationRectangle.Height);

            Speed = new Vector2(10 , 10);

            LocalReading = new Vector2();

            LocalAccelerometer = new Accelerometer();
            LocalAccelerometer.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
            LocalAccelerometer.Start();
        }

        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            LocalReading.X = e.SensorReading.Acceleration.Y;
            LocalReading.Y = e.SensorReading.Acceleration.X;
        }

        public void Update(GameTime gametime)
        {
            if (bocaAberta)
            {
                contBocaAberta++;
            }

            if(contBocaAberta >= 10)
            {
                bocaAberta = false;

                contBocaAberta = 0;
            }

            if (colidiu)
            {
                alpha -= a;

                if (alpha <= 0 || alpha >= 1)
                {
                    a *= -1;

                    Points -= 1;
                }
            }
            else
            {
                alpha = 1;
            }

            Position += Speed * LocalReading;

            if (bocaAberta)
            {
                Tooth.Update(gametime);

                Position.X = MathHelper.Clamp(Position.X, 0, Screen.Width - Tooth.AnimationRectangle.Width);
                Position.Y = MathHelper.Clamp(Position.Y, 75, Screen.Height - Tooth.AnimationRectangle.Height);

                BoundingRectangle.Width = Tooth.AnimationRectangle.Width;
                BoundingRectangle.Height = Tooth.AnimationRectangle.Height;
            }
            else
            {
                Animation.Update(gametime);

                Position.X = MathHelper.Clamp(Position.X, 0, Screen.Width - Animation.AnimationRectangle.Width);
                Position.Y = MathHelper.Clamp(Position.Y, 75, Screen.Height - Animation.AnimationRectangle.Height);

                BoundingRectangle.Width = Animation.AnimationRectangle.Width;
                BoundingRectangle.Height = Animation.AnimationRectangle.Height;
            }
            
            BoundingRectangle.X = (int)Position.X;
            BoundingRectangle.Y = (int)Position.Y;
       }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (bocaAberta)
            {
                spriteBatch.Draw(Tooth.Sheet , Position , Tooth.AnimationRectangle , Color.White * alpha);
            }
            else
            {
                spriteBatch.Draw(Animation.Sheet, Position, Animation.AnimationRectangle, Color.White * alpha);
            }
        }
    }
}