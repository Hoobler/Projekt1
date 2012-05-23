using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss5 : Boss_Base
    {
        private int timer;
        private int phase;
        List<Boss5_MiniAirplane> minilist = new List<Boss5_MiniAirplane>();

        public Boss5(Vector2 position)
        {
            this.position = position;
            size = new Point(500, 400);

            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!accessorised && activated)
                Accessorize();

            if (activated && phase == 0)
            {
                speed = new Vector2(0, 1);

                if (Position.Y >= Settings.window.ClientBounds.Height * (1f / 4f))
                {
                    position.Y = Settings.window.ClientBounds.Height * (1f / 4f);
                    speed = new Vector2(1, 0);
                    phase = 1;
                    for (int i = 0; i < accessoryList.Count; i++)
                        accessoryList[i].ReallyActivated = true;
                }
            }
            if (phase == 1)
            {
                
            }
        }

        public override void Accessorize()
        {
            accessoryList.Add(new Boss5_Cannon(Center));
        }
    }
}
