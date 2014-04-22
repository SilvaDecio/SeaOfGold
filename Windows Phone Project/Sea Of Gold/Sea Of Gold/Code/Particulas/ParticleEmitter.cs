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
    class ParticleEmitter
    {
        Random Raffle;

        public List<Particle> Particles;

        public Vector2 Position;

        public float tempo = 15;

        ContentManager Content;

        public ParticleEmitter(ContentManager content , Vector2 position)
        {
            Content = content;

            Position = position;

            Particles = new List<Particle>();

            Raffle = new Random();
        }

        public void Update()
        {
            for (int i = 0; i < 10; i++)
            {
                Particles.Add(GerarParticulas());
            }

            for (int i = Particles.Count - 1; i >= 0; i--)
            {
                Particles[i].Update();

                if (Particles[i].LifeTime <= 0)
                {
                    Particles.RemoveAt(i);
                }
            }
        }

        public Particle GerarParticulas()
        {
            Vector2 speed = new Vector2(5f * (float)(Raffle.NextDouble() * 2), 1f * (float)(Raffle.NextDouble() * 2 - 1));

            float angle = 0;

            float angleSpeed = 0.1f * (float)(Raffle.NextDouble() * 2 - 1);

            return new Particle(Content, Position, speed, angle, angleSpeed, tempo);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Draw(spriteBatch);
            }
        }
    }
}