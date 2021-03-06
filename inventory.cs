using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; //концовки в игре нет, тодько умереть можно
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
		public void print()
		{
			foreach (item i in items)
			{
				if (i.equiped) 
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("ЭКИПИРОВАНО ");
					Console.ForegroundColor = ConsoleColor.White;
				}
				Console.WriteLine($">{i.name} ({i.dmg})");
			}
		} //экипировано. У игрока может быть много оружий
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
		//уже работает, я починил
		public bool upgrade(bool check) // если check = true, то метод просто проверит можно ли улучшить предмет
        {     // если check = false, то метод улучшит все улучшаемые предметы
			int counter = 0;
			item upgradable = null;
			for(int i = 0; i < items.Count; i++)
            {
				if (counter >= 3) //this- это ссылка на объект класса для которого вызывается метод
				{
					upgradable = items[i-1];
					break;
				}
				counter = 0;
				foreach (item j in items)
				{
					if (items[i].name == j.name)
						counter++;
				}
            }
			if (counter >= 3) 
			{
                if (!check)
                {
					for(int i = 0; i < items.Count; i++)
                    {
						if (items[i].name == upgradable.name)
						{
							items.Remove(items[i]);
							upgradable.dmg += 5;
							i--;
						}
					
                    }
					items.Add(upgradable);
					Console.WriteLine($"Улучшен предмет {upgradable.name}");
					Console.ReadKey();
                }
				return true;
			}
			return false; // не нашли 3 похожих предмета
			
		}
	}
}
