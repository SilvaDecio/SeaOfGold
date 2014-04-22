using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sea_Of_Gold.Code.Peixes.Jogador;
using Decio.Animation;

namespace Sea_Of_Gold.Code.Peixes
{
    abstract class Peixe
    {
        protected Game game1;


        public Vector2 Position;

        public Rectangle BoundingRectangle;

        public Sprite Animation;
        
        public List<Vector2> posicoes = new List<Vector2>();
        
        public int vida , pontos , distancia;
        
        protected int velocidade;

        public virtual void Update(GameTime gametime)
        {
            Position.X -= velocidade;
            
            Position.Y = distancia + (float)(40 * Math.Sin(2 * Math.PI * (gametime.ElapsedGameTime.TotalSeconds / 2) - (Position.X / 100)));

            Animation.Update(gametime);

            BoundingRectangle.X = (int)Position.X;
            BoundingRectangle.Y = (int)Position.Y;
        }
        
        public virtual void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch, Position);
        }
    }
}