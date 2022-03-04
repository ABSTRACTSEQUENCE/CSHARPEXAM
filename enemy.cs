using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{
    class enemy
    {
        public string name;
        public int hp { get; set; }
        public int skill { get; set; }
        public inventory inventory;
        public enemy(string name, int hp, int skill, inventory inv)
        {
            this.hp = hp; this.name = name; this.skill = skill;
            inventory = inv;
        }
    }
}
