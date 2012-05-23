using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    class Boss3_Gun : Boss_Accessory
    {
        float angleSpeed;
        float timeUntilNextShot = 1f;
        float timeBetweenShots = 1f;
        Vector2 rotationPoint;

        public Boss3_Gun(Vector2 position, float angleSpeed, Vector2 rotationPoint)
        {
            
            angle = (float)Math.PI / 2f;
            size = new Point(20, 20);
            texture = Texture2DLibrary.spaceship;
            color = Color.Black;
            this.angleSpeed = angleSpeed;
            maxHealth = 200;
            health = maxHealth;
            this.rotationPoint = rotationPoint;
            this.position = position;
            killable = true;
        }
        public override void Update(GameTime gameTime, Vector2 speed)
        {
            base.Update(gameTime, speed);
            

            

            if (reallyActivated)
            {
                

                float xPrim = ((Center.X - rotationPoint.X) * (float)Math.Cos(angleSpeed)) - ((Center.Y - rotationPoint.Y) * (float)Math.Sin(angleSpeed)) + rotationPoint.X;
                float yPrim = ((Center.X - rotationPoint.X) * (float)Math.Sin(angleSpeed)) + ((Center.Y - rotationPoint.Y) * (float)Math.Cos(angleSpeed)) + rotationPoint.Y;

                position.X = xPrim - size.X / 2;
                position.Y = yPrim - size.Y / 2;

                timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timeUntilNextShot >= timeBetweenShots)
                {
                    Objects.enemyProjectileList.Add(new Boss3_Projectile(position, angle));
                    timeUntilNextShot -= timeBetweenShots;
                }
            }
        }

        public void Update(float angle)
        {
            this.angle = angle;
        }


        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                    new Rectangle((int)Position.X + (Size.X / 2), (int)Position.Y + (Size.Y / 2), (int)(Size.X * (2f / 3f)), Size.Y),
                    new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height),
                    color,
                    angle + (float)Math.PI / 2,
                    new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2),
                    spriteEffect, layerDepth);
        }
        

    }
}
