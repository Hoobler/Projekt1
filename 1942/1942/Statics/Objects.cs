﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _1942
{
    static class Objects
    {
        static public List<BasePlayer> playerList = new List<BasePlayer>();
        static public List<BaseEnemy> enemyList = new List<BaseEnemy>();
        static public List<BaseFormation> formationList = new List<BaseFormation>();

        static public List<BaseEnemy> deadList = new List<BaseEnemy>();

        static public List<Projectile_Player> playerProjectileList = new List<Projectile_Player>();
        static public List<BaseProjectile> enemyProjectileList = new List<BaseProjectile>();

        static public List<Particle_Base> particleList = new List<Particle_Base>();


        static public void Update(KeyboardState keyState, GameTime gameTime)
        {

            for (int i = 0; i < Objects.playerList.Count; i++)
                Objects.playerList[i].Update(keyState, gameTime);

            for (int i = 0; i < Objects.playerProjectileList.Count; i++)
                Objects.playerProjectileList[i].Update(gameTime);

            for (int i = 0; i < Objects.deadList.Count; i++)
                Objects.deadList[i].Update(gameTime);

            for (int i = 0; i < Objects.enemyList.Count; i++)
                Objects.enemyList[i].Update(gameTime);

            for (int i = 0; i < Objects.enemyProjectileList.Count; i++)
                Objects.enemyProjectileList[i].Update(gameTime);

            for (int i = 0; i < Objects.formationList.Count; i++)
                Objects.formationList[i].Update(gameTime);

            for (int i = 0; i < Objects.particleList.Count; i++)
                Objects.particleList[i].Update(gameTime);

        }
        static public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Objects.deadList.Count; i++)
                Objects.deadList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.playerProjectileList.Count; i++)
                Objects.playerProjectileList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.enemyProjectileList.Count; i++)
                Objects.enemyProjectileList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.enemyList.Count; i++)
                Objects.enemyList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.playerList.Count; i++)
                Objects.playerList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.formationList.Count; i++)
                Objects.formationList[i].Draw(spriteBatch);

            for (int i = 0; i < Objects.particleList.Count; i++)
                Objects.particleList[i].Draw(spriteBatch);
        }

        static public void DeadRemoval()
        {
            for (int i = Objects.enemyList.Count - 1; i >= 0; i--)
                if (Objects.enemyList[i].IsDead())
                    Objects.enemyList.RemoveAt(i);

            for (int i = Objects.deadList.Count - 1; i >= 0; i--)
                if (Objects.deadList[i].IsDead())
                    Objects.deadList.RemoveAt(i);

            for (int i = Objects.playerProjectileList.Count - 1; i >= 0; i--)
                if (Objects.playerProjectileList[i].IsDead())
                    Objects.playerProjectileList.RemoveAt(i);

            for (int i = Objects.enemyProjectileList.Count - 1; i >= 0; i--)
                if (Objects.enemyProjectileList[i].IsDead())
                    Objects.enemyProjectileList.RemoveAt(i);

            for (int i = Objects.formationList.Count - 1; i >= 0; i--)
            {
                for (int j = Objects.formationList[i].list_Zero.Count - 1; j >= 0; j--)
                {
                    if (Objects.formationList[i].list_Zero[j].IsDead())
                        Objects.formationList[i].list_Zero.RemoveAt(j);
                }
            }
        }

    }
}
