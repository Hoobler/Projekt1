using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1942
{
    static class Objects
    {
        static public List<BasePlayer> playerList = new List<BasePlayer>();
        static public List<BaseEnemy> enemyList = new List<BaseEnemy>();

        static public List<BaseEnemy> deadList = new List<BaseEnemy>();

        static public List<Projectile_Player> playerProjectileList = new List<Projectile_Player>();
        static public List<BaseProjectile> enemyProjectileList = new List<BaseProjectile>();
    }
}
