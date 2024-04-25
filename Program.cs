using System.Runtime.CompilerServices;

namespace TextRpg
{
    class Program
    {
        static Character player = new Character("Chad", "전사", 10, 5, 100, 1500); // 초기 플레이어 정보

        static List<Item> inventory = new List<Item>(); // 인벤토리 리스트

        static List<Item> shopItems = new List<Item>()
    {
        new Item("수련자 갑옷", "방어력 +5", 1000),
        new Item("무쇠갑옷", "방어력 +9", 2000),
        new Item("스파르타의 갑옷", "방어력 +15", 3500),
        new Item("낡은 검", "공격력 +2", 600),
        new Item("청동 도끼", "공격력 +5", 1500),
        new Item("스파르타의 창", "공격력 +7", 2500)
    };

        static void Main(string[] args)
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            while (true)
            {
                ShowMainMenu();

                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    ShowStatus();
                }
                else if (input == "2")
                {
                    ShowInventory();
                }
                else if (input == "3")
                {
                    ShowShop();
                }
                else if (input == "4")
                {
                    Stage();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("\n[메인 메뉴]");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 들어가기(미구현)");
        }
        static void Stage()
        {
            char[,] map = new char[20, 20]; // 20x20 크기의 맵 배열
            
            InitializeMap(); // 맵 초기화
            DisplayMap(); // 맵 출력
            Console.WriteLine("1. 마을로 돌아가기(미구현)");
            Console.ReadKey(true);
            string input = Console.ReadLine();

            if (input == "1")
            {
                ShowMainMenu();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            void InitializeMap()
            {
                // 맵을 초기화합니다. 각 셀은 '.'으로 초기화합니다.
                for (int row = 0; row < 20; row++)
                {
                    for (int col = 0; col < 20; col++)
                    {
                        if (row == 0 || row == 19 || col == 0 || col == 19)
                        {
                            map[row, col] = '#';
                        }
                        else
                        {
                            map[row, col] = '.'; // 내부는 '.'으로 초기화합니다.
                        }
                    }
                }
            }
            void DisplayMap()
            {
                Console.Clear(); // 콘솔 화면을 지웁니다.

                // 맵을 출력합니다.
                for (int row = 0; row < 20; row++)
                {
                    for (int col = 0; col < 20; col++)
                    {
                        Console.Write(map[row, col] + " ");
                    }
                    Console.WriteLine(); // 한 행 출력 후 줄바꿈
                }
            }
        }
            
       
    

     

    static void ShowStatus()
    {
        Console.WriteLine("\n[상태 보기]");
        Console.WriteLine($"Lv. {player.Level}");
        Console.WriteLine($"{player.Name} ( {player.Class} )");
        Console.WriteLine($"공격력 : {player.Attack}");
        Console.WriteLine($"방어력 : {player.Defense}");
        Console.WriteLine($"체 력 : {player.Health}");
        Console.WriteLine($"Gold : {player.Gold} G");

        Console.Write("\n나가시려면 아무 키나 누르세요...");
        Console.ReadKey(true);
    }

    static void ShowInventory()
    {
        Console.WriteLine("\n[인벤토리]");

        for (int i = 0; i < inventory.Count; i++)
        {
            string equipped = (inventory[i].Equipped) ? "[E]" : "";
            Console.WriteLine($"{equipped}{i + 1}. {inventory[i].Name} | {inventory[i].Description}");
        }

        Console.Write("\n나가시려면 아무 키나 누르세요...");
        Console.ReadKey(true);
    }

    static void ShowShop()
    {
        Console.WriteLine("\n[상점]");
        Console.WriteLine($"[보유 골드] {player.Gold} G\n");

        for (int i = 0; i < shopItems.Count; i++)
        {
            string status = (shopItems[i].Purchased) ? "구매완료" : $"{shopItems[i].Price} G";
            Console.WriteLine($"{i + 1}. {shopItems[i].Name} | {shopItems[i].Description} | {status}");
        }

        Console.WriteLine("\n원하시는 아이템 번호를 입력하세요 (구매할 아이템) : ");
        string input = Console.ReadLine();

        int index;
        if (int.TryParse(input, out index) && index >= 1 && index <= shopItems.Count)
        {
            BuyItem(index - 1);
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
    }

    static void BuyItem(int index)
    {
        Item item = shopItems[index];

        if (item.Purchased)
        {
            Console.WriteLine("이미 구매한 아이템입니다.");
        }
        else if (player.Gold >= item.Price)
        {
            player.Gold -= item.Price;
            inventory.Add(item);
            item.Purchased = true;
            Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
        }
        else
        {
            Console.WriteLine("Gold가 부족합니다.");
        }

        Console.Write("\n나가시려면 아무 키나 누르세요...");
        Console.ReadKey(true);
    }
}

class Character
{
    public int Level { get; private set; }
    public string Name { get; private set; }
    public string Class { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Health { get; private set; }
    public int Gold { get; set; }

    public Character(string name, string characterClass, int attack, int defense, int health, int gold)
    {
        Level = 1;
        Name = name;
        Class = characterClass;
        Attack = attack;
        Defense = defense;
        Health = health;
        Gold = gold;
    }
}

    class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int Price { get; }
        public bool Equipped { get; set; }
        public bool Purchased { get; set; }

        public Item(string name, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
            Equipped = false;
            Purchased = false;
        }


    }        
}

