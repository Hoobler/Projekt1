using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{

    class Boss3 : Boss_Base
    {
        float angleGun;
        float angleSpeed;
        float timer;
        int phase;
        float radius;
        Vector2 rotationPoint;

        float timeUntilNextShot;
        float timeBetweenShots = 0.1f;

        public Boss3(Vector2 position, float timer)
        {
            size = new Point(100, 100);
            speed = new Vector2(0, 0);
            radius = Settings.window.ClientBounds.Height/2 - size.X/2;
            rotationPoint = new Vector2(Settings.window.ClientBounds.Width / 2, Settings.window.ClientBounds.Height / 2);
            this.position = position;
            this.position.X = Settings.window.ClientBounds.Width/2 + radius;
            angle = (float)Math.PI;
            angleSpeed = (float)Math.PI / 360f;
            texture = Texture2DLibrary.spaceship;
            color = Color.White;
            this.timer = timer+1f;
            maxHealth = 1000;
            health = maxHealth;
            score = 500;
            

            
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (activated && accessoryList.Count <= 0)
                killed = true;
            if(activated)
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (angle >= 0 && angle < (float)Math.PI / 2f)
                angleGun = 0;
            else if (angle >= (float)Math.PI / 2f && angle < (float)Math.PI)
                angleGun = (float)Math.PI / 2f;
            else if (angle >= (float)Math.PI && angle < (float)Math.PI * (3f / 2f))
                angleGun = (float)Math.PI;
            else if (angle >= (float)Math.PI * (3f / 2f) && angle < (float)Math.PI * 2f)
                angleGun = (float)Math.PI * (3f / 2f);

            if (angle >= (float)Math.PI * 2f)
                angle -= (float)Math.PI * 2f;
            else if (angle < 0)
                angle += (float)Math.PI * 2f;

            if (phase >= 2 && !killed)
            {
                timer++;
                if (timer == 600)
                    Objects.powerUpList.Add(new PowerUpShield(new Vector2(100, -50)));
                if (timer == 1200)
                    Objects.powerUpList.Add(new PowerUpDamage(new Vector2(400, -50)));
                if (timer >= 1200)
                    timer = 0;
            }

            if (phase == 0 && timer <= 0)
            {
                MusicManager.SetMusic(SoundLibrary.Boss1);
                phase = 1;
                speed = new Vector2(0, 1);
            }

            if (phase == 1 && position.Y >= Settings.window.ClientBounds.Height / 2 - size.Y / 2)
            {
                position.Y = Settings.window.ClientBounds.Height / 2 - size.Y / 2;
                phase = 2;
                speed = new Vector2(0, 0);
            }

            if (phase >= 2)
            {
                timeUntilNextShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                
                if (timeUntilNextShot >= timeBetweenShots)
                {
                    timeUntilNextShot -= timeBetweenShots;
                    Vector2 shotOrigin = new Vector2(0, 0);
                    shotOrigin.X = (float)Math.Cos(angle - (float)Math.PI / 2f) * size.Y / 2 + Center.X;
                    shotOrigin.Y = (float)Math.Sin(angle - (float)Math.PI/2f) * size.Y / 2 + Center.Y;
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

                if (phase == 2)
                {
                    angle += angleSpeed;
                    for (int i = 0; i < accessoryList.Count; i++)
                        if (!accessoryList[i].ReallyActivated)
                            accessoryList[i].ReallyActivated = true;

                    float xPrim = ((Center.X - rotationPoint.X) * (float)Math.Cos(angleSpeed)) - ((Center.Y - rotationPoint.Y) * (float)Math.Sin(angleSpeed)) + rotationPoint.X;
                    float yPrim = ((Center.X - rotationPoint.X) * (float)Math.Sin(angleSpeed)) + ((Center.Y - rotationPoint.Y) * (float)Math.Cos(angleSpeed)) + rotationPoint.Y;

                    position.X = xPrim - size.X/2;
                    position.Y = yPrim - size.Y/2;
                    for (int i = 0; i < accessoryList.Count; i++)
                        accessoryList[i].AngleUpdate(angleGun + (float)Math.PI / 2f);
                }
            }

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (activated)
            {
                spriteBatch.Draw(texture,
                        new Rectangle((int)Position.X + (Size.X / 2), (int)Position.Y + (Size.Y / 2), Size.X, Size.Y),
                        new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height),
                        color,
                        angle,
                        new Vector2(texture.Bounds.Width / 2, texture.Bounds.Height / 2),
                        spriteEffect, layerDepth);
                for (int i = 0; i < accessoryList.Count; i++)
                {
                    accessoryList[i].Draw(spriteBatch);
                }

                if (phase == 1)
                    spriteBatch.DrawString(FontLibrary.Hud_Font, "DODGE EVERYTHING HOLY FISHSTICKS\nAlso shoot the cannons", new Vector2(200, 200), Color.Red);
            }

            

        }

        

        public override void Accessorize()
        {
            base.Accessorize();
            accessoryList.Add(new Boss3_Gun(new Vector2(Center.X-10, Center.Y-size.Y/2), angleSpeed, rotationPoint));
            accessoryList.Add(new Boss3_Gun(new Vector2(Center.X-10, Center.Y-10), angleSpeed, rotationPoint));
            accessoryList.Add(new Boss3_Gun(new Vector2(Center.X-10, Center.Y+size.Y/2-20), angleSpeed, rotationPoint));
        }
    }
}
