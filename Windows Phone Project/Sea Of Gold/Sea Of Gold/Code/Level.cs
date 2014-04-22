using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

using Sea_Of_Gold.Code.Fish;
using Sea_Of_Gold.Management;

namespace Sea_Of_Gold.Code
{
    enum LevelType
    {
        Level1 , Level2 , Level3 , Level4
    }

    class Level
    {
        public List<Enemie> Enemies;

        public LevelType CurrentLevel;

        public float AlphaChannel;

        bool IsOnTransition;

        Texture2D TransitionImage;

        int FishCount , CreatedFishes;

        float ElapsedTime, Interval;

        public Level(ContentManager Content , Viewport screen , LevelType type)
        {
            Enemies = new List<Enemie>();

            CurrentLevel = type;

            # region Type

            switch (CurrentLevel)
            {
                case LevelType.Level1:

                    for (int i = 0; i < 30; i++)
                    {
                        if (i < 10)
                        {
                            Enemies.Add(new Enemie(Content , new Vector2(1300 + 300 * i, 300), 235, 2 , EnemieType.Chines));
                        }
                        else if (i < 20)
                        {
                            Enemies.Add(new Enemie(Content, new Vector2(1300 + 300 * i, 300), 120, 3, EnemieType.Chines));
                        }
                        else if (i < 30)
                        {
                            Enemies.Add(new Enemie(Content, new Vector2(1000 + 300 * i, 300), 350, 4, EnemieType.Chines));
                        }
                    }

                    FishCount = 30;

                    //Interval = ;

                    TransitionImage = Content.Load<Texture2D>("Source/Levels/Level1");

                    break;

                case LevelType.Level2:

                    for (int i = 0; i < 40; i++)
                    {
                        if (i < 10)
                        {
                            Enemies.Add(new Enemie(Content, new Vector2(1300 + 300 * i, 500), 170, 4, EnemieType.Chines));
                        }
                        else if (i < 20)
                        {
                            Enemies.Add(new Enemie(Content, new Vector2(1300 + 300 * (i - 10), 500), 340, 4, EnemieType.Chines));
                        }
                        else if (i < 30)
                        {
                            Enemies.Add(new Enemie(Content, new Vector2(2300 + 300 * i, 500), 140, 5, EnemieType.Tubarao));
                        }
                        else if (i < 40)
                        {
                            Enemies.Add(new Enemie(Content, new Vector2(2300 + 300 * (i - 10), 500), 310, 5, EnemieType.Tubarao));
                        }

                        FishCount = 40;

                        //Interval = ;
                    }

                    TransitionImage = Content.Load<Texture2D>("Source/Levels/Level2");

                    break;

                case LevelType.Level3:

                    for (int i = 0; i < 40; i++)
                    {
                        if (i < 10)
                        {
                            Enemies.Add(new Enemie(Content , new Vector2(1300 + 300 * i, 500), 310, 5 , EnemieType.Tubarao));
                        }
                        else if (i < 20)
                        {
                            Enemies.Add(new Enemie(Content , new Vector2(1500 + 300 * i, 500), 140, 6, EnemieType.Tubarao));
                        }
                        else if (i < 30)
                        {
                            Enemies.Add(new Enemie(Content , new Vector2(2000 + 300 * i, 500), 350, 5, EnemieType.Eletrico));
                        }
                        else if (i < 40)
                        {
                            Enemies.Add(new Enemie(Content , new Vector2(2400 + 300 * i, 500), 150, 6, EnemieType.Eletrico));
                        }

                        FishCount = 40;

                        //Interval = ;
                    }

                    TransitionImage = Content.Load<Texture2D>("Source/Levels/Level3");

                    break;

                case LevelType.Level4:

                    for (int i = 0; i < 40; i++)
                    {
                        if (i < 10)
                        {
                            Enemies.Add(new Enemie(Content , new Vector2(1300 + 300 * i, 500), 150, 4, EnemieType.Eletrico));
                        }
                        else if (i < 20)
                        {
                            Enemies.Add(new Enemie(Content, new Vector2(1300 + 300 * i, 500), 350, 5, EnemieType.Eletrico));
                        }
                        else if (i < 30)
                        {
                            Enemies.Add(new Enemie(Content , new Vector2(2300 + 300 * i, 500), 140, 4, EnemieType.Aparelhado));
                        }
                        else if (i < 40)
                        {
                            Enemies.Add(new Enemie(Content , new Vector2(2600 + 300 * i, 500), 330, 5, EnemieType.Aparelhado));
                        }

                        FishCount = 40;

                        //Interval = ;
                    }

                    TransitionImage = Content.Load<Texture2D>("Source/Levels/Level4");

                    break;
            }

            # endregion

            CreatedFishes = 0;

            ElapsedTime = 0f;
        }

        public void Update(GameTime gameTime)
        {
            if (CreatedFishes < FishCount)
            {
                ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                if (ElapsedTime >= Interval)
                {
                    ElapsedTime = 0f;

                    switch (CurrentLevel)
                    {
                        case LevelType.Level1:



                            break;

                        case LevelType.Level2:



                            break;

                        case LevelType.Level3:



                            break;

                        case LevelType.Level4:



                            break;
                    }
                }
            }

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                Enemies[i].Update(gameTime);

                if (Enemies[i].BoundingRectangle.Right <= 0)
                {
                    Enemies.RemoveAt(i);
                }
            }

            # region Transição e Alpha

            if (IsOnTransition == false)
            {
                AlphaChannel += 0.01f;
            }

            if (AlphaChannel >= 1)
            {
                IsOnTransition = true;
            }

            if (IsOnTransition == true)
            {
                AlphaChannel -= 0.01f;
            }

            AlphaChannel = MathHelper.Clamp(AlphaChannel, 0, 1);

            # endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Draw(spriteBatch);
            }

            spriteBatch.Draw(TransitionImage , Vector2.Zero, Color.White * AlphaChannel);
        }
    }
}