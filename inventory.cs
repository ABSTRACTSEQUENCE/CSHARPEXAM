using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAM
{
	class inventory
	{
		public List<item> items = new List<item>();
		public item equipped;
		public void add(item item)
		{
			items.Add(item);
		}
		public void print(int choise)
        {
            if (choise <= 0)
            {
                print(1);
                return;
            }
			if (choise > items.Count)
				choise = items.Count;
            for (int i = 0; i < items.Count; i++)
            {
				if(i + 1 == choise)
					Console.Write(">");

				if (items[i].equiped)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("<ЭКИПИРОВАНО> ");
					Console.ForegroundColor = ConsoleColor.White;
				}
				Console.WriteLine($"{items[i].name} ({items[i].dmg})");
            }
		}
		public void equip(item item)
        {
			if (!items.Contains(item))
				return;
            foreach(item i in items)
            {
				if (i.equiped)
					i.equiped = false;
            }
			equipped = item;
			item.equiped = true;
        }
	}
}
