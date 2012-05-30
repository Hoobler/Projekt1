using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1942
{
    class Boss5_MiniAirplane : BaseEnemy
    {

        Vector2 rotationPoint;
        float angleSpeed;
        int phase;
        float timer;
        bool mirrored;
        float timeUntilNextShot;
        float timeBetweenShots;

        public Boss5_MiniAirplane(Vector2 rotationPoint, bool mirrored)
        {
            this.mirrored = mirrored;
            texture = Texture2DLibrary.enemy_zero;
            size = Settings.size_zero;
            this.rotationPoint = rotationPoint;
            angle = (float)Math.PI;
            angleSpeed = (float)Math.PI * (2f / 360f);
            
            position = new Vector2(rotationPoint.X + 100 , -size.Y);
            color = Color.White;
            maxHealth = 10;
            health = maxHealth;
            flying = true;
            killable = false;
            
            if (mirrored)
            {
                angleSpeed = -angleSpeed;
                position.X = rotationPoint.X - 100 - size.X;
            }
            timeBetweenShots = 0.01f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (phase == 0)
            {
                position.Y += 2;
                if (Center.Y >= rotationPoint.Y)
                {
                    position.Y = rotationPoint.Y - size.Y/2f;
                    phase = 1;
                }
            }

            if (phase == 1)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                angle += angleSpeed;
                timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

                float xPrim = ((Center.X - rotationPoint.X) * (float)Math.Cos(angleSpeed)) - ((Center.Y - rotationPoint.Y) * (float)Math.Sin(angleSpeed)) + rotationPoint.X;
                float yPrim = ((Center.X - rotationPoint.X) * (float)Math.Sin(angleSpeed)) + ((Center.Y - rotationPoint.Y) * (float)Math.Cos(angleSpeed)) + rotationPoint.Y;

                position.X = xPrim - size.X / 2;
                position.Y = yPrim - size.Y / 2;

                if (timeUntilNextShot >= timeBetweenShots)
                {
                    timeUntilNextShot -= timeBetweenShots;
                    Vector2 shotOrigin = new Vector2(0, 0);
                    shotOrigin.X = (float)Math.Cos(angle - (float)Math.PI / 2f) * size.Y / 2 + Center.X;
                    shotOrigin.Y = (float)Math.Sin(angle - (float)Math.PI / 2f) * size.Y / 2 + Center.Y;
                    Vector2 shot1Origin = new Vector2(0, 0);
                    Vector2 shot2Origin = new Vector2(0, 0);
                    float angleChange = (float)Math.PI / 8;
                    shot1Origin.X = (shotOrigin.X - Center.X) * (float)Math.Cos(angleChange) + (shotOrigin.Y - Center.Y) * (float)Math.Sin(angleChange) + Center.X;
                    shot1Origin.Y = -(shotOrigin.X - Center.X) * (float)Math.Sin(angleChange) + (shotOrigin.Y - Center.Y) * (float)Math.Cos(angleChange) + Center.Y;
                    shot2Origin.X = (shotOrigin.X - Center.X) * (float)Math.Cos(-angleChange) + (shotOrigin.Y - Center.Y) * (float)Math.Sin(-angleChange) + Center.X;
                    shot2Origin.Y = -(shotOrigin.X - Center.X) * (float)Math.Sin(-angleChange) + (shotOrigin.Y - Center.Y) * (float)Math.Cos(-angleChange) + Center.Y;
                    Objects.enemyProjectileList.Add(new Boss3_Projectile_Front(shot1Origin, angle - (float)Math.PI / 2f));
                    Objects.enemyProjectileList.Add(new Boss3_Projectile_Front(shot2Origin, angle - (float)Math.PI / 2f));
                }

                if (timer >= 7.5f)
                    phase = 2;

            }
            if (phase == 2)
            {
                if (mirrored)
                {
                    position.X += 2;
                    if (position.X >= Settings.window.ClientBounds.Width + size.X)
                        dead = true;
                }
                else
                {
                    position.X -= 2;
                    if (position.X <= -size.X)
                        dead = true;
                }
            }
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture,
                new Rectangle((int)Position.X + size.X / 2, (int)Position.Y + size.Y / 2, Size.X, Size.Y),
                new Rectangle(1, 1, size.X, size.Y),
                color,
                angle,
                new Vector2(size.X/2f, size.Y/2f),
                SpriteEffects.None,
                layerDepth);
            
        }
    }
}
