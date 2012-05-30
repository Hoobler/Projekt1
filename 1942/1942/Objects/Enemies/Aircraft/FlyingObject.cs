using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _1942
{
    class FlyingObject: BaseEnemy
    {
        float timeUntilSpeedChange;
        float timeBetweenSpeedChanges;

        public FlyingObject()
            : base()
        {
            flying = true;
            timeBetweenSpeedChanges = Settings.zero_speed_change;
            timeUntilSpeedChange = timeBetweenSpeedChanges / 2f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            position.Y += Settings.level_speed;

            if (activated)
            {
                //if (Objects.escortList.Count > 0)
                //{
                //    int nearestEscort = 0;
                //    for (int i = 1; i < Objects.escortList.Count; i++)
                //    {
                //        float distanceCurrent = (float)Math.Sqrt(
                //            (Center.X - Objects.escortList[i].Center.X) * (Center.X - Objects.escortList[i].Center.X) +
                //            (Center.Y - Objects.escortList[i].Center.Y) * (Center.Y - Objects.escortList[i].Center.Y)
                //            );

                //        float distancePrevious = (float)Math.Sqrt(
                //            (Position.X - Objects.escortList[i - 1].Center.X) * (Center.X - Objects.escortList[i - 1].Center.X) +
                //            (Position.Y - Objects.escortList[i - 1].Center.Y) * (Center.Y - Objects.escortList[i - 1].Center.Y)
                //            );

                //        if (distanceCurrent < distancePrevious)
                //            nearestEscort = i;
                //    }

                //    if (Center.X <= Objects.escortList[nearestEscort].Center.X)
                //        position.X += 1;
                //    if (Center.X >= Objects.escortList[nearestEscort].Center.X)
                //        position.X -= 1;
                //}

                timeUntilSpeedChange += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeUntilSpeedChange >= timeBetweenSpeedChanges)
                {
                    timeUntilSpeedChange -= timeBetweenSpeedChanges;
                    speed.X = -speed.X;
                }

            }
        }

        

        
        public Vector2 Speed
        {
            set { speed = value; }
            get { return speed; }
        }
    }
}
