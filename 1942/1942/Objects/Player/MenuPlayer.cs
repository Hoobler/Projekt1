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
        private int distanceToPlayer = 0;
        private int nearestObject = 0;
        private int formationList = 0;
        private int MoveX = 0;

        public MenuPlayer(): base()
        {
            color = Color.White;
            position = new Vector2(Settings.window.ClientBounds.Width / 2 - size.X *1.5f, Settings.window.ClientBounds.Height - size.Y);
            playerID = 0;
            damage = 10;
        }

        #region Methods

        public void ClosestEnemy()
        {
            for (int i = 0; i < Objects.formationList.Count; i++)
            {
                for (int j = 0; j < Objects.formationList[i].enemyInFormationList.Count; j++)
                {
                    if (Objects.formationList[i].enemyInFormationList[j].Activated)
                    {
                        var tempDistance = (int)Vector2.Distance(Objects.formationList[i].enemyInFormationList[j].Center, this.Center);
                        if (tempDistance > 0 || tempDistance < distanceToPlayer)
                        {
                            MoveX = (int)Objects.formationList[i].enemyInFormationList[j].Center.X;
                            distanceToPlayer = tempDistance;
                            nearestObject = j;
                            formationList = i;
                        }
                    }
                }
            }
        }

        public void MoveTheShip()
        {
            if (MoveX > 0)
            {
                if (MoveX > this.Center.X)
                {
                    GoRight();
                }
                if (MoveX < this.Center.X)
                {
                    GoLeft();
                }
                if (this.Center.X <= MoveX || this.Center.X >= MoveX)
                {
                    Fire();
                }
            }
        }

        #endregion 

        public override void Update(KeyboardState keyState, GameTime gameTime)
        {
            base.Update(keyState, gameTime);

            ClosestEnemy();
            MoveTheShip();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(FontLibrary.debug, "" + distanceToPlayer.ToString(), new Vector2(100f, 300), Color.Red);
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
