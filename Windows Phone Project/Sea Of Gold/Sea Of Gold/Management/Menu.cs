using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.GamerServices;

using System.IO.IsolatedStorage;
using System.Xml.Serialization;

using Microsoft.Phone.Tasks;
using Microsoft.Phone.Net.NetworkInformation;

using Sea_Of_Gold.BaseClasses;
using Sea_Of_Gold.Management;
using Sea_Of_Gold.DataBase;

namespace Sea_Of_Gold.Management
{
    class Menu : State
    {
        Button PlayButton, DirectionsButton, CreditsButton, RankingButton,
            SettingsButton;

        public Menu(StateManager Father)
        {
            Manager = Father;

            # region Buttons

            PlayButton = new Button(Manager.Game.Content.Load<Texture2D>
                ("Buttons/Menu/Play"), new Vector2(455, 45));
            DirectionsButton = new Button(Manager.Game.Content.Load<Texture2D>
                ("Buttons/Menu/Directions"), new Vector2(355, 160));
            CreditsButton = new Button(Manager.Game.Content.Load<Texture2D>
                ("Buttons/Menu/Credits"), new Vector2(480, 240));
            RankingButton = new Button(Manager.Game.Content.Load<Texture2D>
                ("Buttons/Menu/Ranking"), new Vector2(605, 160));
            SettingsButton = new Button(Manager.Game.Content.Load<Texture2D>
                ("Buttons/Menu/Settings"), new Vector2(700, 20));

            # endregion

            # region Language

            switch (StateManager.CurrentLanguage)
            {
                case GameLanguage.English:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("English/BackGroundImages/Menu");

                    break;

                case GameLanguage.Portugues:

                    BackGroundImage = Manager.Game.Content.Load<Texture2D>
                        ("Portugues/Telas/Menu");

                    break;
            }

            # endregion

            if (StateManager.HasAudioControl)
            {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(Manager.MenuSong);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            # region Device's BackButton

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Manager.Game.Exit();
            }

            # endregion

            #region Buttons

            if (Manager.Touched)
            {
                if (PlayButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    if (StateManager.HasAudioControl)
                    {
                        MediaPlayer.Stop();    
                    }

                    Manager.GoToGamePlay();
                }

                else if (DirectionsButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToDirections();
                }

                else if (CreditsButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToCredits();
                }

                else if (RankingButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToRanking();
                }

                else if (SettingsButton.Rectangle.Contains(Manager.TouchedPlace))
                {
                    Manager.GoToSettings();
                }                
            }

            # endregion

            base.Update(gameTime);
        }

        private void OnEndShowMessageBox(IAsyncResult result) {}

        public override void Draw(GameTime gameTime)
        {
            Manager.spriteBatch.Draw(BackGroundImage, Vector2.Zero, Color.White);

            PlayButton.Draw(Manager.spriteBatch);
            CreditsButton.Draw(Manager.spriteBatch);
            DirectionsButton.Draw(Manager.spriteBatch);
            RankingButton.Draw(Manager.spriteBatch);
            SettingsButton.Draw(Manager.spriteBatch);

            base.Draw(gameTime);
        }
    }
}