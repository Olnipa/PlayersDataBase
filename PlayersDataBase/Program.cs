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
                        dataBase.AddPlayer();
                        break;
                    case "2":
                        dataBase.BanPlayer();
                        break;
                    case "3":
                        dataBase.UnbanPlayer();
                        break;
                    case "4":
                        dataBase.DeletePlayer();
                        break;
                    case "5":
                        dataBase.ShowPlayers();
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
        private List<Player> _players;
        
        public DataBase(List<Player> players)
        {
            _players = players;
        }

        public void ShowPlayers()
        {
            Console.WriteLine();

            for (int i = 0; i < _players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ID - {_players[i].ID}. Name - {_players[i].Name}. Level - {_players[i].Level}. Is banned - {_players[i].IsBanned}");
            }

            WriteMessage("\nДля продолжения нажмите любую клавишу...");
        }

        public void AddPlayer()
        {
            string name = ReadName("\nВведите имя нового игрока: ");
            _players.Add(new Player(GenerateID(_players), name));
            WriteMessage("Новый игрок создан. Для продолжения нажмите любую клавишу...");
        }

        public void BanPlayer()
        {
            string playerIDForBan = ReadID("\nВведите ID игрока, которого нужно забанить: ");

            if (FindPlayerID(out int index, playerIDForBan))
            {
                _players[index].Ban();
                WriteMessage($"Игрок  с ID {playerIDForBan} забанен. Для продолжения нажмите любую клавишу...");
            }
        }

        public void DeletePlayer()
        {
            string playerIDForDelete = ReadID("\nВведите ID игрока, которого нужно удалить: ");

            if (FindPlayerID(out int index, playerIDForDelete))
            {
                _players.RemoveAt(index);
                WriteMessage($"Игрок с ID {playerIDForDelete} удален. Для продолжения нажмите любую клавишу...");
            }
        }

        public void UnbanPlayer()
        {
            string playerIDForUnBan = ReadID("\nВведите ID игрока, которого нужно разбанить: ");

            if (FindPlayerID(out int index, playerIDForUnBan))
            {
                _players[index].Unban();
                WriteMessage($"Игрок  с ID {playerIDForUnBan} успешно разбанен. Для продолжения нажмите любую клавишу...");
            }
        }

        private bool FindPlayerID(out int index, string id)
        {
            bool idIsFound = false;
            index = 0;

            if (int.TryParse(id, out int value))
            {
                for (index = 0; index < _players.Count; index++)
                {
                    if (_players[index].ID == value)
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

        private void WriteMessage(string text, ConsoleColor color = ConsoleColor.Green)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = defaultColor;
            Console.ReadKey();
        }

        public string ReadID(string text)
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
        public bool IsBanned { get; private set; }
        public int ID {get; private set;}
        public string Name { get; private set; }
        public int Level { get; private set; }

        public Player(int id, string name, int level = 0, bool isBanned = false)
        {
            ID = id;
            Name = name;
            Level = level;
            IsBanned = isBanned;
        }

        public void Unban()
        {
            IsBanned = false;
        }

        public void Ban()
        {
            IsBanned = true;
        }
    }
}