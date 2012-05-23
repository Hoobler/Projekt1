using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class MenuPlayer : BasePlayer
    {
        private int fDistToPlayer = 2000;
        private int eDistToPlayer = 0;
        private int pDistToPlayer = 0;
        private int nearestObject = 0;
        private int formationList = 0;
        private int formationPosX = 0;
        private int enemyPosX = 0;
        private int powerUpPosX = 0;
        private bool isFormActive = false;
        private bool goForPowerUp = false;

        public MenuPlayer(): base()
        {
            color = Color.White;
            position = new Vector2(Settings.window.ClientBounds.Width / 2 - size.X *1.5f, Settings.window.ClientBounds.Height - size.Y);
            playerID = 0;
            damage = 10;
        }

        #region Methods

        public void ClosestObject()
        {
            for (int i = 0; i < Objects.formationList.Count; i++)
            {
                for (int j = 0; j < Objects.formationList[i].enemyInFormationList.Count; j++)
                {
                    if (Objects.formationList[i].enemyInFormationList[j].Activated)
                    {
                        var tempFDistance = (int)Vector2.Distance(Objects.formationList[i].enemyInFormationList[j].Center, this.Center);
                        if (tempFDistance > 0 || tempFDistance < fDistToPlayer) // fDistToPlayer = formationDistansToPlayer
                        {
                            formationPosX = (int)Objects.formationList[i].enemyInFormationList[j].Center.X;
                            fDistToPlayer = tempFDistance;
                            nearestObject = j;
                            formationList = i;
                            isFormActive = Objects.formationList[i].enemyInFormationList[j].Activated;
                        }
                    }
                }
            }
            for (int i = 0; i < Objects.enemyList.Count; i++)
            {
                if (Objects.enemyList[i].Activated)
                {
                    var tempEDistance = (int)Vector2.Distance(Objects.enemyList[i].Center, this.Center);
                    if (tempEDistance > 0 || tempEDistance < eDistToPlayer)
                    {
                        enemyPosX = (int)Objects.enemyList[i].Center.X;
                        eDistToPlayer = tempEDistance;
                    }
                }
            }
            for (int i = 0; i < Objects.powerUpList.Count; i++)
            {
                var tempPDistance = (int)Vector2.Distance(Objects.powerUpList[i].Position, this.Center);
                if (tempPDistance > 0 || tempPDistance < pDistToPlayer)
                {
                    powerUpPosX = (int)Objects.powerUpList[i].Position.X;
                    pDistToPlayer = tempPDistance;
                    if (tempPDistance <= 400)
                    {
                        goForPowerUp = true;
                    }
                    else if (tempPDistance >= 401)
                    {
                        goForPowerUp = false;
                    }
                }
            }
        }


        public void MoveTheShip()
        {
            if (goForPowerUp)
            {
                if (powerUpPosX > this.Center.X)
                {
                    GoRight();
                    goForPowerUp = false;
                }
                if (powerUpPosX < this.Center.X)
                {
                    GoLeft();
                    goForPowerUp = false;
                }
                if (this.Center.X <= powerUpPosX || this.Center.X >= powerUpPosX)
                {
                    goForPowerUp = false;
                }
            }
            else//if (!goForPowerUp)
            {
                if (formationPosX > 0 && isFormActive)
                {
                    if (formationPosX > this.Center.X)
                    {
                        GoRight();
                        isFormActive = false;
                    }
                    if (formationPosX < this.Center.X)
                    {
                        GoLeft();
                        isFormActive = false;
                    }
                    if (this.Center.X <= formationPosX || this.Center.X >= formationPosX)
                    {
                        Fire();
                        isFormActive = false;
                    }
                }
            }

            //else if (!isFormActive)
            //{
            //    if (this.Center.X < Settings.window.ClientBounds.Center.X)
            //    {
            //        GoRight();
            //        isFormActive = false;
            //    }
            //    else if (this.Center.X > Settings.window.ClientBounds.Center.X)
            //    {
            //        GoLeft();
            //    }
            //    else if (this.Center.X <= Settings.window.ClientBounds.Center.X || this.Center.X >= Settings.window.ClientBounds.Center.X)
            //    {
            //        GoDown();
            //    }
            //}
        }

        #endregion 

        public override void Update(KeyboardState keyState, GameTime gameTime)
        {
            base.Update(keyState, gameTime);

            ClosestObject();
            MoveTheShip();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontLibrary.debug, "" + fDistToPlayer.ToString(), new Vector2(100f, 300), Color.Red);
            spriteBatch.DrawString(FontLibrary.debug, "" + nearestObject.ToString(), new Vector2(200f, 200), Color.Red);
            spriteBatch.Draw(texture,
                Rectangle,
                new Rectangle((animationFrame.X * (texture.Bounds.Width - 1) / 3) + 1,
                    (animationFrame.Y * (texture.Bounds.Height - 1) / 3) + 1,
                    ((texture.Bounds.Width - 1) / 3) - 1,
                    ((texture.Bounds.Height - 1) / 3) - 1),
                color,
                angle,
                new Vector2(0, 0),
                spriteEffect,
                0.0f);
        }
    }
}
