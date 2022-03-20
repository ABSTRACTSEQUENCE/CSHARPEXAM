using System;

namespace EXAM
{
    class player
    {
        public int hp { get; set; }//хп 
        public int max_hp { get; set; }
        private int lvl { get; set; }//уровень
        public int skill { get; set; } //это урон, прибавляемый к урону оружия
        int exp { get; set; }
        int required_exp = 100; // необходимый опыт для повышения уровня
        public inventory inventory;

        public player(inventory inventory)
        {
            hp = 100; lvl = 1; exp = 0; skill = 0; max_hp = 100;
            this.inventory = inventory;
        }

        public void lvlup(int exp)
        {
            this.exp += exp;
            while (this.exp >= required_exp)
            {
                skill += 5;
                required_exp += 50;
                lvl++; max_hp += 25;
                hp = max_hp;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Вы получили {exp} опыта");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        public void print()
        {
            Console.WriteLine(
                $"HP: {hp}\n" +
                $"SKILL: {skill}\n" +
                $"LVL: {lvl}\n" +
                $"EXP: {exp}\n");
        }

    }
}

