using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    static class Settings
    {
        static public float level_speed = 2f;
        static public int nr_of_players = 2;
        static public GameWindow window;

        static public int damage_collision = 10;

        static public float tower_projectile_speed = 3f;

        static public float tower_projectile_frequency = 1f;

        static public Point tower_projectile_size = new Point(2, 6);
        static public Point size_tower = new Point(41, 41);
        static public int tower_projectile_damage = 2;
        static public int tower_health = 100;

        static public Vector2 kamikaze_speed = new Vector2(0, 8);
        static public int kamikaze_health = 1;

        static public Vector2 zero_projectile_speed = new Vector2(0,3);
        static public Point zero_projectile_size = new Point(2, 4);
        static public float zero_projectile_frequency = 0.8f;
        static public Point size_zero = new Point(32, 30);
        static public int zero_projectile_damage = 1;
        static public int zero_health = 50;
        static public Vector2 zero_speed = new Vector2(0, 1f);

        static public Vector2 zeke_projectile_speed = new Vector2(0, 3);
        static public Point zeke_projectile_size = new Point(2, 6);
        static public float zeke_projectile_frequency = 1f;
        static public Point size_zeke = new Point(32, 30);
        static public int zeke_projectile_damage = 2;
        static public int zeke_health = 80;
        static public Vector2 zeke_speed = new Vector2(0, 1f);

        static public Vector2 todjo_projectile_speed = new Vector2(0, 3);
        static public Point todjo_projectile_size = new Point(2, 6);
        static public float todjo_projectile_frequency = 2f;
        static public Point size_todjo = new Point(32, 30);
        static public int todjo_projectile_damage = 5;
        static public int todjo_health = 100;
        static public Vector2 todjo_speed = new Vector2(0, 1f);

        static public Vector2 player_projectile_speed = new Vector2 (0,-7);
        static public Point player_projectile_size = new Point(2, 4);
        static public float player_projectile_frequency = 0.1f;
        static public int player_projectile_damage = 10;
        static public Point size_player = new Point(32 , 30);

        static public Vector2 boat_speed = new Vector2(1,-1);
        static public Point boat_size = new Point (100,50);
        static public int boat_health = 150;
        static public Point boat_tower_size = new Point(50, 50);
        static public float boat_tower_projectile_speed = 3f;
        static public float boat_tower_projectile_frequency = 1f;
        static public Point boat_tower_projectile_size = new Point(2, 4);

    
        

        public enum CurrentLevel { Level1, Level2, Level3, Level4, Level5 };
        static public CurrentLevel currentLevel = CurrentLevel.Level1;
    }
}
