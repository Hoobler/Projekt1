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
        #region Variables
        //The "distance" to the player
        private int fDistToPlayer = 2000;
        private int eDistToPlayer = 2000;
        private int pDistToPlayer = 2000;
        private int bDistToPlayer = 2000;
        private int nearestObject = 0;
        private int formationList = 0;
        //The x position for the enemy that needs to be killed
        private int formationPosX = 0;
        private int formationPosY = 0;
        private int enemyPosX = 0;
        private int powerUpPosX = 0;
        private int bossPosX = 0;
        private int MoveX = 0;
        //Bools for checking if the enemy is on screen at all
        private bool isFormActive = false;
        private bool isEnemyActive = false;
        private bool goForPowerUp = false;
        private bool goForBoss = false;
        private bool nothingOnScreen = false;
        private bool onScreen = false;
        #endregion

        public MenuPlayer(): base()
        {
            color = Color.White;
            position = new Vector2(Settings.window.ClientBounds.Width / 2 - size.X *1.5f, Settings.window.ClientBounds.Height - size.Y);
            playerID = 0;
            damage = 10;
        }

        #region Methods

        private void ClosestObject()
        {
            for (int i = 0; i < Objects.formationList.Count; i++)
            {
                for (int j = 0; j < Objects.formationList[i].enemyInFormationList.Count; j++)
                {
                    if (Objects.formationList[i].enemyInFormationList[j].Activated)
                    {
                        var tempFDistance = (int)Vector2.Distance(Objects.formationList[i].enemyInFormationList[j].Center, this.Center);
                        if (tempFDistance > 0 && tempFDistance < fDistToPlayer) // fDistToPlayer = formationDistansToPlayer
                        {
                            formationPosX = (int)Objects.formationList[i].enemyInFormationList[j].Center.X;
                            formationPosY = (int)Objects.formationList[i].enemyInFormationList[j].Center.Y;
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
                    if (tempEDistance > 0 && tempEDistance < eDistToPlayer)
                    {
                        enemyPosX = (int)Objects.enemyList[i].Center.X;
                        eDistToPlayer = tempEDistance;
                        isEnemyActive = Objects.enemyList[i].Activated;
                    }
                }
            }
            for (int i = 0; i < Objects.powerUpList.Count; i++)
            {
                var tempPDistance = (int)Vector2.Distance(Objects.powerUpList[i].Position, this.Center);
                if (tempPDistance > 0 && tempPDistance < pDistToPlayer)
                {
                    powerUpPosX = (int)Objects.powerUpList[i].Center.X;
                    pDistToPlayer = tempPDistance;
                    if (tempPDistance <= 600 && fDistToPlayer > 100 && eDistToPlayer > 100)
                    {
                        goForPowerUp = true;
                    }
                    else if (tempPDistance >= 601 && fDistToPlayer < 100 && eDistToPlayer < 100)
                    {
                        goForPowerUp = false;
                    }
                }
            }
            for (int i = 0; i < Objects.bossList.Count; i++)
            {
                for (int j = 0; j < Objects.bossList[i].accessoryList.Count; j++)
                    if (Objects.bossList[i].accessoryList[j].IsKillable && !Objects.bossList[i].accessoryList[j].Killed)
                    {
                        var tempBDistance = (int)Vector2.Distance(Objects.bossList[i].accessoryList[j].Position, this.Center);
                        if (tempBDistance > 0 && tempBDistance < bDistToPlayer)
                        {
                            bossPosX = (int)Objects.bossList[i].accessoryList[j].Position.X;
                            bDistToPlayer = tempBDistance;
                            goForBoss = Objects.bossList[i].accessoryList[j].IsKillable;
                        }
                    }
            }
            if (!isFormActive && !isEnemyActive && !goForPowerUp)
            {
                nothingOnScreen = true;
            }
            else
            {
                nothingOnScreen = false;
            }
            if (isFormActive || isEnemyActive)
            {
                onScreen = true;
            }
            else
            {
                onScreen = false;
            }
            if (fDistToPlayer < eDistToPlayer)
            {
                MoveX = formationPosX;
            }
            else if (eDistToPlayer < fDistToPlayer)
            {
                MoveX = enemyPosX;
            }
        }

        private void MoveTheShip()
        {
            var windowY = Settings.window.ClientBounds.Height -50;

            if (isFormActive || isEnemyActive || goForPowerUp || goForBoss)
            {
                if (!goForPowerUp)
                {
                    if (this.Center.X <= MoveX || this.Center.X >= MoveX)
                    {
                        Fire();
                        fDistToPlayer = 1000;
                        eDistToPlayer = 1000;
                        isFormActive = false;
                        isEnemyActive = false;
                    }
                    if (MoveX > this.Center.X)
                    {
                        GoRight();
                        isFormActive = false;
                        isEnemyActive = false;
                    }
                    if (MoveX < this.Center.X)
                    {
                        GoLeft();
                        isFormActive = false;
                        isEnemyActive = false;
                    }
                }
                if (goForPowerUp && !goForBoss)
                {
                    if (this.Center.X <= powerUpPosX || this.Center.X >= powerUpPosX)
                    {
                        pDistToPlayer = 1000;
                        goForPowerUp = false;
                        if (onScreen)
                        {
                            Fire();
                        }
                    }
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
                }
                if (goForBoss)
                {
                    if (this.Center.X <= bossPosX || this.Center.X >= bossPosX)
                    {
                        Fire();
                        goForBoss = false;
                        bDistToPlayer = 1000;
                    }
                    if (bossPosX > this.Center.X)
                    {
                        goForBoss = false;
                        GoRight();
                    }
                    if (bossPosX < this.Center.X)
                    {
                        goForBoss = false;
                        GoLeft();
                    }
                }
            }
            if (nothingOnScreen)
            {
                var windowCenter = (int)Settings.window.ClientBounds.Width / 2f;
                if (this.Center.X < windowCenter || this.Center.X > this.Center.X)
                { }
                if (windowCenter > this.Center.X)
                {
                    GoRight();
                }
                if (windowCenter < this.Center.X)
                {
                    GoLeft();
                }
            }
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
            spriteBatch.DrawString(FontLibrary.debug, "FormationDist: " + fDistToPlayer.ToString(), new Vector2(100f, 300), Color.Red);
            spriteBatch.DrawString(FontLibrary.debug, "EnemyDist: " + eDistToPlayer.ToString(), new Vector2(100f, 320), Color.Red);
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
