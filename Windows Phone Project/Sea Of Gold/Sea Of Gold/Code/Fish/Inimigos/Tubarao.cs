using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

using Decio.Animation;

namespace Sea_Of_Gold.Code.Peixes.Inimigos
{
    class Tubarao : Peixe
    {
        public Tubarao(Game game, Vector2 pos, int finalyX, int velocidade)
        {
            this.velocidade = velocidade;

            distancia = finalyX;

            game1 = game;

            Position = pos;

            Animation = new Sprite(game1.Content.Load<Texture2D>("Source/Peixes/Inimigos/Tubarao"), 44f, "Source/Peixes/Inimigos/Tubarao.txt", "Tubarao");

            BoundingRectangle = new Rectangle((int)pos.X, (int)pos.Y, Animation.AnimationRectangle.Width, Animation.AnimationRectangle.Height);

            vida = 2;
            pontos = 20;
        }
    }
}