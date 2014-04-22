using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

using Sea_Of_Gold.Management;
using Sea_Of_Gold.Code.Particulas;

namespace Sea_Of_Gold.Code
{
    class InterFace
    {
        public ParticleEmitter Emitter;

        Texture2D Image, barraEnergetica , CountourImage;

        public Vector2 BarPosition , ContourPosition;

        Texture2D[] pontos = new Texture2D[10];

        public Rectangle BoundingRectangle;

        float alpha = 1;

        float a = 0.1f;

        bool alert;
        
        SoundEffect EfeitoAlerta;
        
        int indice = 0;

        public InterFace(ContentManager Content)
        {
            Image = Content.Load<Texture2D>("Source/InterFace/Cenario-InterFace");
            
            barraEnergetica = Content.Load<Texture2D>("Source/InterFace/BarraEnergetica/BarraEnergia");
            
            Emitter = new ParticleEmitter(Content , new Vector2(275, 40));
            
            BarPosition = new Vector2(225, 40-(barraEnergetica.Height/2));

            BoundingRectangle = new Rectangle(0, 0, Image.Width, Image.Height);

            for (int i = 0; i < pontos.Length; i++)
            {
                pontos[i] = Content.Load<Texture2D>("Source/Pontos\\" + i);
            }
            
            CountourImage = Content.Load<Texture2D>("Source/InterFace\\BarraEnergetica\\Contorno");

            ContourPosition = new Vector2(270 , 40 - (CountourImage.Height / 2));

            EfeitoAlerta = Content.Load<SoundEffect>("Audio/SoundEffects/Alert");
        }

        public void Update(GameTime gameTime)
        {
            Emitter.Update();

            if (Emitter.tempo <= 5)
            {
                alert = true;

                alpha -= a;

                if (alert)
                {
                    if (indice == 3  *60)
                    {
                        if (StateManager.HasAudioControl)
                        {
                            EfeitoAlerta.Play();    
                        }
                                                
                        indice = 0;
                    }

                    indice++;

                    if (alpha <= 0 || alpha >= 1)
                    {
                        a *= -1;
                    } 
                }
            }
            else
            {
                alert = false;

                alpha = 0.1f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, int potuacao)
        {
            spriteBatch.Draw(Image, Vector2.Zero, Color.White);            
            
            spriteBatch.Draw(barraEnergetica, BarPosition,null, Color.White,0,Vector2.Zero,1f,SpriteEffects.FlipHorizontally,0);
            
            Emitter.Draw(spriteBatch);
            
            for (int i = 0; i < potuacao.ToString().Length; i++)
            {
                spriteBatch.Draw(pontos[Convert.ToInt32(potuacao.ToString().Substring(i, 1))], new Vector2(675 + 24 * i, 27), Color.White);
            }

            if (alert)
            {
                spriteBatch.Draw(CountourImage, ContourPosition, Color.White * alpha);
            }   
        }
    }
}