using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Decio.Animation
{
    class Scenario
    {
        public Texture2D[] Images;

        public Vector2[] Positions;

        public Vector2 Speed;

        readonly int TotalWidth , TotalHeight;

        bool IsVertical;

        Viewport Screen;

        public Scenario(Viewport screen , bool Isvertical , Vector2 speed , params Texture2D[] images)
        {
            Screen = screen;

            IsVertical = Isvertical;

            Images = images;

            Positions = new Vector2[Images.Length];

            Speed = speed;

            TotalWidth = 0;
            TotalHeight = 0;

            if (IsVertical)
            {
                for (int i = 0; i < Images.Length; i++)
                {
                    Positions[i].Y = TotalHeight;
                    TotalHeight += Images[i].Height;
                } 
            }
            else
            {
                for (int i = 0; i < Images.Length; i++)
                {
                    Positions[i].X = TotalWidth;
                    TotalWidth += Images[i].Width;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsVertical)
            {
                for (int i = 0; i < Images.Length; i++)
                {
                    Positions[i].Y += Speed.Y;

                    if (Speed.Y < 0)
                    {
                        if (Positions[i].Y < (Screen.Height - TotalHeight))
                        {
                            Positions[i].Y = Screen.Height + Speed.Y;
                        }
                    }
                    if (Speed.Y > 0)
                    {
                        if (Positions[i].Y > TotalHeight - Images[i].Height)
                        {
                            Positions[i].Y = -Images[i].Height + Speed.Y;
                        }
                    }
                }
                
            }
            else
            {
                for (int i = 0; i < Images.Length; i++)
                {
                    Positions[i].X += Speed.X;

                    if (Speed.X < 0)
                    {
                        if (Positions[i].X < (Screen.Width - TotalWidth))
                        {
                            Positions[i].X = Screen.Width + Speed.X;
                        }
                    }
                    if (Speed.X > 0)
                    {
                        if (Positions[i].X > TotalWidth - Images[i].Width)
                        {
                            Positions[i].X = -Images[i].Width + Speed.X;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Images.Length; i++)
            {
                spriteBatch.Draw(Images[i], Positions[i], Color.White);
            }            
        }
    }
}