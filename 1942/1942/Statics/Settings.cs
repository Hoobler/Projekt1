using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _1942
{
    static class Settings
    {
        static public float level_speed = 1f;
        static public int nr_of_players = 2;
        static public GameWindow window;

        static public int damage_collision = 10;

        static public float tower_projectile_speed = 2f;
        static public float tower_projectile_frequency = 1f;
        static public float zero_projectile_frequency = 1f;
    }
}
