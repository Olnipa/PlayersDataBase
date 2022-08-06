namespace PlayersDataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isWorking = true;
            List<Player> players = new List<Player>() { new Player(1, "Admin") };
            DataBase dataBase = new DataBase(players);

            while (isWorking)
            {
                Console.Clear();
                Console.Write("\nДобро пожаловать в меню управления базой данных игроков.\n1 - добавить игрока" +
                    "\n2 - Забанить игрока\n3 - Разбанить игрока\n4 - Удалить игрока\n5 - Показать всех игроков\n0 - Выход" +
                    "\nВведите команду: ");
                string choosenMenu = Console.ReadLine();
                
                switch (choosenMenu)
                {
                    case "1":
                        dataBase.AddPlayer(players);
                        break;
                    case "2":
                        dataBase.BanPlayer(players);
                        break;
                    case "3":
                        dataBase.UnbanPlayer(players);
                        break;
                    case "4":
                        dataBase.DeletePlayer(players);
                        break;
                    case "5":
                        dataBase.ShowPlayers(players);
                        break;
                    case "0":
                        isWorking = false;
                        break;
                }
            }
        }
    }

    class DataBase
    {
        public List<Player> Players { get; private set; }
        public DataBase(List<Player> players)
        {
            Players = players;
        }

        public void ShowPlayers(List<Player> players)
        {
            Console.WriteLine();

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ID - {players[i].ID}. Name - {players[i].Name}. Level - {players[i].Level}. Is banned - {players[i].IsBanned}");
            }

            WriteMessage("\nДля продолжения нажмите любую клавишу...");
        }

        public void AddPlayer(List<Player> players)
        {
            string name = ReadName("\nВведите имя нового игрока: ");
            players.Add(new Player(GenerateID(players), name));
            WriteMessage("Новый игрок создан. Для продолжения нажмите любую клавишу...");
        }

        public void BanPlayer(List<Player> players)
        {
            string playerIDForBan = ReadID("\nВведите ID игрока, которого нужно забанить: ");

            if (FindPlayerID(players, out int index, playerIDForBan))
            {
                players[index].IsBanned = true;
                WriteMessage($"Игрок  с ID {playerIDForBan} забанен. Для продолжения нажмите любую клавишу...");
            }
        }

        public void DeletePlayer(List<Player> players)
        {
            string playerIDForDelete = ReadID("\nВведите ID игрока, которого нужно удалить: ");

            if (FindPlayerID(players, out int index, playerIDForDelete))
            {
                players.RemoveAt(index);
                WriteMessage($"Игрок с ID {playerIDForDelete} удален. Для продолжения нажмите любую клавишу...");
            }
        }

        public void UnbanPlayer(List<Player> players)
        {
            string playerIDForUnBan = ReadID("\nВведите ID игрока, которого нужно разбанить: ");

            if (FindPlayerID(players, out int index, playerIDForUnBan))
            {
                players[index].IsBanned = false;
                WriteMessage($"Игрок  с ID {playerIDForUnBan} успешно разбанен. Для продолжения нажмите любую клавишу...");
            }
        }

        public bool FindPlayerID(List<Player> players, out int i, string id)
        {
            bool idIsFound = false;
            i = 0;

            if (int.TryParse(id, out int value))
            {
                for (i = 0; i < players.Count; i++)
                {
                    if (players[i].ID == value)
                    {
                        idIsFound = true;
                        return idIsFound;
                    }
                }

                if (idIsFound == false)
                {
                    WriteMessage($"Игрока  с ID {value} ненайдено. Для продолжения нажмите любую клавишу...", ConsoleColor.Red);
                }
            }
            else
            {
                WriteMessage("Неверно введен ID. ID Игрока состоит только из цифр.", ConsoleColor.Red);
            }
            
            return idIsFound;
        }

        public void WriteMessage(string text, ConsoleColor color = ConsoleColor.Green)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = defaultColor;
            Console.ReadKey();
        }

        private string ReadID(string text)
        {
            Console.Write(text);
            string value = Console.ReadLine();
            return value;
        }

        private string ReadName(string text)
        {
            string value = "";
            int minValueLenght = 3;

            while (value.Length < minValueLenght)
            {
                Console.Write(text);
                value = Console.ReadLine();

                if (value.Length < minValueLenght)
                {
                    WriteMessage($"Значение должно состоять минимум из {minValueLenght}х символов. " +
                        $"Нажмите любую клавишу для повторного ввода...", ConsoleColor.Red);
                }
            }
            
            return value;
        }

        private int GenerateID(List<Player> players)
        {
            int playerID;

            if (players.Count > 0)
            {
                playerID = players[players.Count - 1].ID + 1;
            }
            else
            {
                playerID = 1;
            }

            return playerID;
        }
    }
     
    class Player
    {
        public int ID {get; private set;}
        public string Name { get; private set; }
        public int Level { get; private set; }
        private bool _isBanned;
        public bool IsBanned
        {
            get
            {
                return _isBanned;
            }
            set
            {
                _isBanned = value;
            }
        }

        public Player(int id, string name, int level = 0, bool isBanned = false)
        {
            ID = id;
            Name = name;
            Level = level;
            _isBanned = isBanned;
        }
    }
}