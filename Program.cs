using System;
namespace EXAM
{

    class Program
    {


        static void Main(string[] args)
        {
            Random rand = new Random();
            //Создание врагов, предметов и т.д.
            //Предметы
            item sword = new item("Меч", 5);
            item spear = new item("Копьё", 5);
            item axe = new item("Топор", 10);
            item katana = new item("Катана", 15);
            item imba = new item("Проклятый Меч Губительной Пустоты", 9999999);
            item claws = new item("Когти", 1); //уникальное "оружие" для крыс,
            //так как когти нельзя взять и экипировать (это было бы не логично), я не стал
            //добавлять их в массив всех оружий чтобы игрок случайно не нашёл когти, путешевствуя по подземелью
            item scythe = new item("Коса Смерти", 500); //Уникальное оружие для Смерти, человеку не по силам использовать столь мощное оружие
            item[] all_items = new item[] { sword, spear, axe, katana, imba };

            //Инвентари
            inventory skeleton_inv = new inventory(); skeleton_inv.add(sword); skeleton_inv.equip(sword);
            inventory elf_inv = new inventory(); elf_inv.add(spear); elf_inv.equip(spear);
            inventory player_inv = new inventory(); player_inv.add(sword); player_inv.equip(sword);
            inventory rat_inv = new inventory(); rat_inv.add(claws); rat_inv.equip(claws);
            inventory death_inv = new inventory(); death_inv.add(scythe); death_inv.equip(scythe);

            //Существа
            player player = new player(player_inv);
            enemy skeleton = new enemy("Скелет", 25, 2, skeleton_inv);
            enemy rat = new enemy("Крыса", 15, 0, rat_inv);
            enemy elf = new enemy("Эльф", 50, 5, elf_inv);
            enemy death = new enemy("Смерть", 1000, 100, death_inv);
            enemy[] all_enemies = { skeleton, elf, rat };


            //Начало игры
            Console.WriteLine("Тёмное подземелье\n> Начать");
            Console.ReadKey();
            Console.Clear();
            while (true)
            {
                Console.Clear();
                menu();
            }
            void menu()
            {
                Console.WriteLine("Чем вы хотите заняться?\n" +
                    "1) Путешевствовать по подземелью\n" +
                    "2) Посмотреть статы\n" +
                    "3) Излечиться\n" +
                    "4) Покинуть подземелье\n" +
                    "5) Посмотреть инвентарь\n" +
                    "6) Улучшить предметы");
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            travel();
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            player.print();
                            Console.ReadKey();
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Благодаря крови дракона, текущей в ваших венах, вы регенерируетесь\n" +
                                $"Ваше здоровье теперь равно {player.max_hp}");
                            Console.ReadKey();
                            Console.ForegroundColor = ConsoleColor.White;
                            player.hp = player.max_hp;
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ТРУС! ТЫ НЕ МОЖЕШЬ ПОКИНУТЬ ПОДЗЕМЕЛЬЕ, ПОКА НЕ УБЬЁШЬ БОССА" +
                                "! ТЫ УМРЁШЬ И БУДЕШЬ ГОРЕТЬ В АДУ!!!");
                            Console.WriteLine("\nПеред Вами появляется портал, из которого вылезает демон, который " +
                                "выше вас примерно в 500 раз и одним взглядом убивает вас");
                            System.Environment.Exit(0);
                            break;
                        case 5:
                            Console.Clear();
                            int choice = 1;
                            Console.WriteLine("Используйте стрелочки вверх и вниз для навигации по инвентарю, esc чтобы выйти\nУ вас есть:");
                            player_inv.print(choice);
                            bool esc = false;
                            while (true)
                            {
                                if (esc)
                                    break;
                                if (choice <= 0)
                                    choice = 1;
                                if (choice > player_inv.items.Count)
                                    choice = player_inv.items.Count;

                                switch (Console.ReadKey().Key)
                                {
                                    case ConsoleKey.DownArrow:
                                        choice++; break;
                                    case ConsoleKey.UpArrow:
                                        choice--; break;
                                    case ConsoleKey.Enter:
                                        player_inv.equip(player_inv.items[choice - 1]);
                                        Console.WriteLine($"Вы экипировали {player_inv.items[choice - 1].name}");
                                        Console.ReadKey();
                                        break;
                                    case ConsoleKey.Escape:
                                        esc = true;
                                        break;
                                    default:
                                        Console.WriteLine("\nУправление по инвентарю происходит с помощью стрелок вверх и вниз, нажмите escape чтобы выйти");
                                        Console.ReadKey();
                                        break;
                                }
                                Console.Clear();
                                Console.WriteLine("Используйте стрелочки вверх и вниз для навигации по инвентарю, esc чтобы выйти\nУ вас есть:");
                                player_inv.print(choice);
                            }
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("Вы можете перековать несколько одинаковых предметов в один\n" +
                                "У полученого предмета будет увеличен урон\n" +
                                "За каждый экземпляр предмета урон будет увеличен на 5\n" +
                                $"Например, у вас есть 3 топора, базовый урон топора = {axe.dmg}\n" +
                                $"После улучшения вы получите топор с уроном {axe.dmg + 15}\n" +
                                "Предметы, которые у вас есть:");
                            player_inv.print();
                            if (!player_inv.upgrade(true))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Вам нечего улучшить...");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Вы можете улучшить предмет");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Чтобы улучшить предметы нажмите Enter, чтобы выйти нажмите Esc");
                                while (true)
                                {
                                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                                    {
                                        player_inv.upgrade(false);
                                        Console.WriteLine("Предметы улучшены.");
                                    }
                                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                                        break;
                                    else
                                    {
                                        Console.WriteLine("Нажмите Enter чтобы улучшить предметы, Esc чтобы покинуть меню");
                                        Console.ReadKey();
                                    }
                                }

                            }

                            Console.ReadKey();

                            break;
                        default:
                            Console.WriteLine("Нажмите кнопку в соответствии с вашим выбором");
                            break;

                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Нажмите цифру, соответствующую вашему выбору");
                    Console.ReadKey();
                }
            }
            void travel()
            {
                Console.WriteLine("Вы бродите по подземелью...");
                Console.ReadKey();
                switch (rand.Next(0, 100))
                {
                    case <=49: //Нападение
                        enemy attack = all_enemies[rand.Next(0, all_enemies.Length)];
                        Console.Write($"\nНа вас напал {attack.name}");
                        if (fight(attack))
                            break;
                        else
                            System.Environment.Exit(0);
                        break;
                    case >=51:// Нашёл предмет
                        item item = new item(all_items[rand.Next(0, all_items.Length)]);
                        Console.WriteLine($"\nВы находите предмет {item.name}");
                        player_inv.add(item);
                        Console.ReadKey();
                        break;
                    case 50:
                        Console.WriteLine("\nНа вас напала сама Смерть... Удачи...");
                        Console.ReadKey();
                        if (!fight(death))
                            System.Environment.Exit(0);
                        else 
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nНевероятно, вы одолели саму Смерть!\nВы чувствуете прилив сил");
                            Console.ReadKey();
                            player.lvlup(99999);
                        }
                        break;
                }
            }
            bool fight(enemy enemy) //если возвращает false, то игрок умер, если true, то выжил
            {
                while (true)
                {
                    int input = 0;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{enemy.name} нанёс вам удар своим {enemy.inventory.equipped.name}," +
                        $" нанеся вам {enemy.inventory.equipped.dmg + enemy.skill} урона");
                    player.hp -= enemy.inventory.equipped.dmg + enemy.skill;
                    if (player.hp <= 0)
                    {
                        Console.WriteLine("Вы погибли");
                        Console.ReadKey();
                        return false;
                    }
                    Console.WriteLine($"У вас осталось {player.hp} здоровья");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Что делать дальше?\n" +
                        "1) Бежать\n" +
                        "2) Продолжать сражаться");
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input == 1)
                    {
                        if (rand.Next(0, 100) <= 50)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Вы сбежали!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.ReadKey();
                            return true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Вы пытаетесь бежать, но {enemy.name} догоняет вас и наносит удар");
                            Console.ReadKey();
                        }
                    }
                    else if (input == 2)
                    {
                        Console.WriteLine($"\nВы наносите {enemy.name} удар своим {player.inventory.equipped.name}," +
                        $" нанеся врагу {player.inventory.equipped.dmg + player.skill} урона\n");
                        enemy.hp -= player.inventory.equipped.dmg + player.skill;
                        Console.WriteLine($"У {enemy.name} остаётся {enemy.hp} здоровья");
                        if (enemy.hp <= 0 && enemy.hp >= -100)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Вы победили врага!");
                            Console.ForegroundColor = ConsoleColor.White;
                            player.lvlup(20);
                            Console.ReadKey();
                            return true;
                        }
                        else if (enemy.hp < -100)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"От вашего мощного удара {enemy.name} разлетелся на куски");
                            Console.ForegroundColor = ConsoleColor.White;
                            player.lvlup(20);
                            Console.ReadKey();
                            return true;
                        }
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
