using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

using Sea_Of_Gold.BaseClasses;
using Sea_Of_Gold.DataBase;
using Sea_Of_Gold.Code;
using Sea_Of_Gold.Code.Fish;

using Microsoft.Devices;

using Decio.Animation;

namespace Sea_Of_Gold.Management
{
    class GamePlay : State
    {
        float ElapsedTime;

        List<Bubble> Carbons, Oxigens;

        List<Gold> Coins;

        Jogador Jpeixe;

        Level CurrentLevel;

        Random Raffle;

        InterFace interFace;

        Scenario Back;

        TouchCollection Touches;

        int Score;

        SoundEffect EfeitoBolha, EfeitoMoeda;

        public int valorDosO2;

        public float gastoDeCO2;

        float OxigenElapsedTime, OxigenInterval, GoldElapsedTime, GoldInterval;

        public GamePlay(StateManager Father)
        {
            Manager = Father;

            Raffle = new Random();

            OxigenInterval = 30000f;

            GoldInterval = 20000f;

            Restart();
        }

        public override void Restart()
        {
            ElapsedTime = 0f;

            valorDosO2 = 2;

            gastoDeCO2 = 0.25f;

            Score = 0;

            Carbons = new List<Bubble>();

            Oxigens = new List<Bubble>();

            Coins = new List<Gold>();

            Jpeixe = new Jogador(Manager.Game.Content, Manager.Game.GraphicsDevice.Viewport);

            interFace = new InterFace(Manager.Game.Content);

            Back = new Scenario(Manager.Game.GraphicsDevice.Viewport, false, new Vector2(-2, 0),
                Manager.Game.Content.Load<Texture2D>("Source/Cenario/Fundo"),
                Manager.Game.Content.Load<Texture2D>("Source/Cenario/Fundo"));

            Touches = new TouchCollection();

            CurrentLevel = new Level(Manager.Game.Content , Manager.Game.GraphicsDevice.Viewport , LevelType.Level1);
            
            EfeitoBolha = Manager.Game.Content.Load<SoundEffect>("Audio/SoundEffects/Bolhas");
            EfeitoMoeda = Manager.Game.Content.Load<SoundEffect>("Audio/SoundEffects/Moeda");

            OxigenElapsedTime = 0f;

            GoldElapsedTime = 0f;

            if (StateManager.HasAudioControl)
            {
                MediaPlayer.Play(Manager.GamePlaySong);
            }

            base.Restart();
        }

        public override void Update(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            OxigenElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            GoldElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Manager.GoToPause();
            }

            # endregion

            interFace.Update(gameTime);

            Back.Update(gameTime);

            # region Levels

            if (CurrentLevel.Enemies.Count == 0)
            {
                switch (CurrentLevel.CurrentLevel)
                {
                    case LevelType.Level1:

                        CurrentLevel = new Level(Manager.Game.Content, Manager.Game.GraphicsDevice.Viewport, LevelType.Level2);

                        break;

                    case LevelType.Level2:

                        CurrentLevel = new Level(Manager.Game.Content, Manager.Game.GraphicsDevice.Viewport, LevelType.Level3);

                        break;

                    case LevelType.Level3:

                        CurrentLevel = new Level(Manager.Game.Content, Manager.Game.GraphicsDevice.Viewport, LevelType.Level4);

                        break;

                    case LevelType.Level4:

                        if (StateManager.HasVibrationControl)
                        {
                            Manager.Vibrate.Start(new TimeSpan(0, 0, 0, 0, 500));
                        }

                        Manager.GoToWon(Score);

                        break;
                }
            }

            #endregion

            # region Input

            Touches = TouchPanel.GetState();

            List<Vector2> Positions = new List<Vector2>();

            for (int i = 0; i < Touches.Count; i++)
            {
                if (Touches[i].State == TouchLocationState.Moved || Touches[i].State == TouchLocationState.Pressed)
                {
                    Positions.Add(Touches[i].Position);
                }
            }

            # endregion

            Jpeixe.Update(gameTime);

            CurrentLevel.Update(gameTime);

            # region Carbons

            if (Positions.Count > 0 && !Jpeixe.bocaAberta)
            {
                if (StateManager.HasAudioControl)
                {
                    EfeitoBolha.Play();
                }

                Jpeixe.bocaAberta = true;

                Carbons.Add(new Bubble(Manager.Game.Content , new Vector2(Jpeixe.BoundingRectangle.Right , Jpeixe.BoundingRectangle.Y) ,
                    BubbleType.Carbon));

                interFace.Emitter.tempo -= gastoDeCO2;
            }

            for (int i = Carbons.Count - 1; i >= 0; i--)
            {
                Carbons[i].Update(gameTime);

                if (!Manager.Game.GraphicsDevice.Viewport.Bounds.Contains(Carbons[i].BoundingRectangle))
                {
                    Carbons.RemoveAt(i);
                }
            }
            
            #endregion

            #region Oxigens

            if (OxigenElapsedTime >= OxigenInterval)
            {
                OxigenElapsedTime = 0f;

                Oxigens.Add(new Bubble(Manager.Game.Content, new Vector2(), BubbleType.Oxigen));
            }

            for (int i = Oxigens.Count - 1; i >= 0; i--)
            {
                Oxigens[i].Update(gameTime);

                if (Jpeixe.BoundingRectangle.Intersects(Oxigens[i].BoundingRectangle))
                {
                    interFace.Emitter.tempo += valorDosO2;

                    if (StateManager.HasAudioControl)
                    {
                        EfeitoBolha.Play();
                    }

                    Oxigens.RemoveAt(i);
                }

                if (!Manager.Game.GraphicsDevice.Viewport.Bounds.Contains(Oxigens[i].BoundingRectangle))
                {
                    Oxigens.RemoveAt(i);
                }
            }

            #endregion

            # region Golds

            if (GoldElapsedTime >= GoldInterval)
            {
                GoldElapsedTime = 0f;

                Coins.Add(new Gold(Manager.Game.Content));
            }

            for (int i = Coins.Count - 1; i >= 0; i--)
            {
                Coins[i].Update(gameTime);

                if (Jpeixe.BoundingRectangle.Intersects(Coins[i].BoundingRectangle))
                {
                    Coins.RemoveAt(i);

                    if (StateManager.HasAudioControl)
                    {
                        EfeitoMoeda.Play();
                    }

                    Score += 20;
                }

                if (!Manager.Game.GraphicsDevice.Viewport.Bounds.Contains(Coins[i].BoundingRectangle))
                {
                    Coins.RemoveAt(i);
                }
            }

            # endregion

            # region Carbons - Enemies
            
            for (int i = Carbons.Count - 1; i >= 0; i--)
            {
                for (int j = CurrentLevel.Enemies.Count - 1; j >= 0; j--)
                {
                    if (CurrentLevel.Enemies[j].BoundingRectangle.Intersects(Carbons[i].BoundingRectangle))
                    {
                        --CurrentLevel.Enemies[j].Life;

                        Carbons.RemoveAt(i);

                        if (StateManager.HasAudioControl)
                        {
                            EfeitoBolha.Play();
                        }
                    }

                    if (CurrentLevel.Enemies[j].Life <= 0)
                    {
                        Score += CurrentLevel.Enemies[j].Points;

                        CurrentLevel.Enemies.RemoveAt(j);
                    }
                }
            }

            #endregion

            # region Flash - Enemies

            for (int i = CurrentLevel.Enemies.Count - 1; i >= 0; i--)
            {
                if (Jpeixe.BoundingRectangle.Intersects(CurrentLevel.Enemies[i].BoundingRectangle))
                {
                    Jpeixe.colidiu = true;

                    interFace.Emitter.tempo -= gastoDeCO2;

                    //if (fases[k].Enemies[i].Position.Y < Jpeixe.Position.Y)
                    //{
                    //    Jpeixe.Position.Y = fases[k].Enemies[i].Position.Y + 1 + Jpeixe.BoundingRectangle.Height;
                    //}
                    //else if (Jpeixe.Position.Y + Jpeixe.BoundingRectangle.Height > fases[k].Enemies[i].Position.Y)
                    //{
                    //    Jpeixe.Position.Y = fases[k].Enemies[i].Position.Y - Jpeixe.BoundingRectangle.Height - 1;
                    //}
                    //else
                    //{
                    //    Jpeixe.posicao.Y = fases[k].peixes[i].posicao.Y - fases[k].peixes[i].retpeixes.Y;
                    //}
                }
            }

            # endregion

            # region Lost

            if (interFace.Emitter.tempo <= 0f)
            {
                if (StateManager.HasVibrationControl)
                {
                    Manager.Vibrate.Start(new TimeSpan(0 , 0 , 0 , 0 , 500));
                }

                Manager.GoToLost(Score);
            }

            # endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Back.Draw(Manager.spriteBatch);

            interFace.Draw(Manager.spriteBatch, Score);

            for (int i = 0; i < Carbons.Count; i++)
            {
                Carbons[i].Draw(Manager.spriteBatch);
            }

            for (int i = 0; i < Oxigens.Count; i++)
            {
                Oxigens[i].Draw(Manager.spriteBatch);
            }

            for (int i = 0; i < Coins.Count; i++)
            {
                Coins[i].Draw(Manager.spriteBatch);
            }

            Jpeixe.Draw(Manager.spriteBatch);

            CurrentLevel.Draw(Manager.spriteBatch);
            
            base.Draw(gameTime);
        }
    }
}