using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{
    class player
    {
        public int hp {get; set;}//хп 
        public int max_hp { get; set; }
        public int lvl { get; set; }//уровень
        public int skill { get; set; } //это урон, прибавляемый к урону оружия
        int exp { get; set; }
        int required_exp = 100; // необходимый опыт для повышения уровня
        public inventory inventory;

        public player(inventory inventory)
        {
            hp = 100; lvl = 1; exp = 0; skill = 20; max_hp = 100;
            this.inventory = inventory;
        }

        public void lvlup(int exp)
        {
            this.exp += exp;
            if (this.exp >= required_exp)
            {
                skill += 10;
                required_exp += 50;
                lvl++; max_hp += 25;
                hp = max_hp;
                inventory.equipped.dmg += 10;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Вы перешли на новый уровень! Вы теперь на { lvl } уровне!");
                Console.ForegroundColor = ConsoleColor.White;
            }

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
