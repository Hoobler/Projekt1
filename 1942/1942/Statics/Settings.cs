﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    static class Settings
    {
        static public float level_speed = 2f;
        static public int nr_of_players;
        static public Vector2 windowBounds;
        static public bool gameOver = false;

        static public int score_player1;
        static public int score_player2;

        static public int damage_collision = 10;

        static public float tower_projectile_speed = 3f;
        static public float tower_projectile_frequency = 1f;
        static public Point tower_projectile_size = new Point(2, 6);
        static public Point size_tower = new Point(41, 41);
        static public int tower_projectile_damage = 2;
        static public int tower_health = 100;

        static public Vector2 kamikaze_speed = new Vector2(0, 8);
        static public int kamikaze_health = 1;

        static public Vector2 zero_projectile_speed = new Vector2(0,2);
        static public Point zero_projectile_size = new Point(2, 4);
        static public float zero_projectile_frequency = 0.8f;
        static public Point size_zero = new Point(32, 30);
        static public int zero_projectile_damage = 1;
        static public int zero_health = 30;
        static public Vector2 zero_speed = new Vector2(1f, 1f);
        static public float zero_speed_change = 1f;

        static public Vector2 zeke_projectile_speed = new Vector2(0, 3);
        static public Point zeke_projectile_size = new Point(2, 6);
        static public float zeke_projectile_frequency = 1f;
        static public Point size_zeke = new Point(32, 30);
        static public int zeke_projectile_damage = 2;
        static public int zeke_health = 60;
        static public Vector2 zeke_speed = new Vector2(0, 1f);

        static public Vector2 todjo_projectile_speed = new Vector2(0, 3);
        static public Point todjo_projectile_size = new Point(2, 6);
        static public float todjo_projectile_frequency = 2f;
        static public Point size_todjo = new Point(32, 30);
        static public int todjo_projectile_damage = 5;
        static public int todjo_health = 80;
        static public Vector2 todjo_speed = new Vector2(0, 1f);

        static public Vector2 player_projectile_speed = new Vector2 (0,-7);
        static public Point player_projectile_size = new Point(2, 4);
        static public float player_projectile_frequency = 0.1f;
        static public int player_projectile_damage = 10;
        static public Point size_player = new Point(32 , 30);

        static public int boss1_engine_health = 800;
        static public int boss1_projectile_speed = 7;
        static public int boss1_projectile_damage = 10;
        static public float boss1_projectile_frequency = 1f;

        static public int boss5_projectile_speed = 2;
        static public int boss5_projectile_damage = 30;
        static public int boss5_megaProjectile_damage = 100;
        static public float boss5_projectile_frequency = 6f;

        static public int textInputLength = 5;

        static public Vector2 boat_speed = new Vector2(1,-1);
        static public Point boat_size = new Point (100,50);
        static public int boat_health = 150;
        static public Point boat_tower_size = new Point(41, 41);
        static public float boat_tower_projectile_speed = 3f;
        static public float boat_tower_projectile_frequency = 1f;
        static public Point boat_tower_projectile_size = new Point(2, 4);

        static public Vector2 escort_speed = new Vector2(2, 0);
        static public Point escort_size = new Point(200,90);
        static public int escort_health = 500;

        static public int Nr_Of_Players
        {
            get { return nr_of_players;}
            set { nr_of_players = value;}
		}

        public enum CurrentLevel { Level0, Level1, Level2, Level3, Level4, Level5 };
        static public CurrentLevel currentLevel = CurrentLevel.Level0;
        static public bool LevelHasChanged = false;

        static public void SetNextLevel(string NextLevel)
        {
            currentLevel = (CurrentLevel)Enum.Parse(typeof(CurrentLevel), NextLevel);
            LevelHasChanged = true;
        }
    }
}
