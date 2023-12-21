using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Program
    {
        static List<Lot> lots = new List<Lot>();//статический список lots, который представляет собой коллекцию объектов класса Lot. 
        static List<User> users = new List<User>();//создается статический список users, который представляет собой коллекцию объектов класса User. 

        static void Main()
        {
            while (true)//бесконечный цикл когда нужно организовать постоянное выполнение каких-то действий,
            {
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Добавить лот");
                Console.WriteLine("3. Просмотр лотов");
                Console.WriteLine("4. Участие в аукционе");
                Console.WriteLine("5. Выход");

                int choice = GetIntInput("Выберите действие: ");

                switch (choice)
                {
                    case 1:
                        RegisterUser();
                        break;
                    case 2:
                        AddLot();
                        break;
                    case 3:
                        DisplayLots();
                        break;
                    case 4:
                        ParticipateInAuction();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, выберите существующий пункт меню.");
                        break;
                }
                foreach (var lot in lots)
                {
                    if (DateTime.Now >= lot.EndTime)
                    {
                        // Проверяем, завершился ли аукцион для данного лота
                        EndAuction(lot);
                    }
                }
            }
            

        }

        static void RegisterUser()
        {
            string username = GetStringInput("Введите ваше имя: ");//вызывается метод для получение строки имени поль
            users.Add(new User(username)); //создается новый обьект класса и добавляется в список юзер
            Console.WriteLine("Регистрация успешна!");
        }

        static void AddLot()
        {
            string name = GetStringInput("Введите название лота: "); //для получения строки название лота
            string description = GetStringInput("Введите описание лота: ");//для получения строки описание
            double startingPrice = GetDoubleInput("Введите начальную цену лота: ");//для получения вещественного числа
            TimeSpan auctionDuration = GetTimeSpanInput("Введите продолжительность аукциона : ");// продолжительность аукциона
            lots.Add(new Lot(name, description, startingPrice, auctionDuration));//создается новый объект класса лот и добавляется в список лотс
            Console.WriteLine("Лот добавлен успешно!");
        }

        static void DisplayLots()
        {
            Console.WriteLine("Список доступных лотов:");
            foreach (var lot in lots)//цикл перебирает каждый элемент списка лот
            {
                Console.WriteLine($"{lot.ID}. {lot.Name} - {lot.CurrentBid:C} ({lot.StartingPrice:C})");
            }
        }

        static void ParticipateInAuction()//принять участие в аукционе
        {
            DisplayLots();//отображает список доступных лотов
            int lotID = GetIntInput("Выберите лот, в который хотите сделать ставку: ");// Получается от пользователя выбор лота
            Lot selectedLot = lots.FirstOrDefault(lot => lot.ID == lotID); //спользуется для поиска первого элемента в коллекции lots, удовлетворяющего определенному условию

            if (selectedLot == null) //если лот который выбрал поль не найден выводит ошибку
            {
                Console.WriteLine("Лот не найден.");
                return;
            }
            if (DateTime.Now >= selectedLot.EndTime)
            {
                Console.WriteLine("Аукцион для этого лота уже завершился.");
                return;
            }

            string username = GetStringInput("Введите ваше имя: ");//получает от поль строку
            User currentUser = users.FirstOrDefault(user => user.Username == username);//для поиска пользователя с именем, указанным пользователем

            if (currentUser == null)//если пользователь не найден
            {
                Console.WriteLine("Пользователь не найден.");
                return;
            }

            double bidAmount = GetDoubleInput("Введите сумму ставки: "); //Получается от пользователя сумму, которую он хочет поставить в текущем аукционе, с использованием метода GetDoubleInput.

            if (bidAmount > currentUser.Balance)//Проверяется, достаточно ли у пользователя средств на балансе для сделки. 
            {
                Console.WriteLine("Недостаточно средств на балансе.");
                return;
            }

            if (bidAmount <= selectedLot.CurrentBid)//Проверяется, что сумма ставки больше текущей ставки на выбранном лоте
            {
                Console.WriteLine("Ставка должна быть выше текущей ставки.");
                return;
            }

            selectedLot.PlaceBid(username, bidAmount);//установить новую ставку от указанного пользователя.
            Console.WriteLine($"Ставка принята. Текущая ставка: {selectedLot.CurrentBid:C}");
        }
        static int GetIntInput(string prompt) //который используется для получения целочисленного ввода от пользователя с обработкой ошибок
        {
            int result;//будет содержать введенное пользователем значение
            bool success;//использоваться для проверки успешности преобразования введенного значения в целое число.
            do
            {
                Console.Write(prompt);
                success = int.TryParse(Console.ReadLine(), out result);// с помощью метода TryParse пытается преобразовать стоку в число
                if (!success) //если преобразование не удалось то выводит ошибку
                {
                    Console.WriteLine("Пожалуйста, введите корректное число.");
                }
            } while (!success);//будет повторятся пока не будет тру
            return result;//значение возвращается из метода
        }

        static double GetDoubleInput(string prompt)//используется для получения вещественного числового ввода от пользователя с обработкой ошибок.
        {
            double result;//содержит введенное поль вещественное число
            bool success;//проверяет успешность преобразование
            do
            {
                Console.Write(prompt);
                success = double.TryParse(Console.ReadLine(), out result);//преобразует строку в число
                if (!success)//false
                {
                    Console.WriteLine("Пожалуйста, введите корректное число.");
                }
            } while (!success);//цикл будет работать пока поль введет коррект данные
            return result;
        }

        static string GetStringInput(string prompt)//считывает строку от поль и возвращает в метод
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        static TimeSpan GetTimeSpanInput(string prompt)
        {
            TimeSpan result;
            bool success;

            do
            {
                Console.Write(prompt);
                success = TimeSpan.TryParse(Console.ReadLine(), out result);

                if (!success)
                {
                    Console.WriteLine("Пожалуйста, введите корректное значение продолжительности (например, '2:30' для 2 часов и 30 минут).");
                }
            } while (!success);

            return result;
        }
        
        static void EndAuction(Lot lot)
        {
            User winningBidder = users.FirstOrDefault(user => user.Username == lot.WinningBidder);
            winningBidder.Balance -= lot.CurrentBid;
            Console.WriteLine($"Лот '{lot.Name}' приобретен пользователем '{winningBidder.Username}' за {lot.CurrentBid:С}");
        }


    }
}

