using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace lab_9
{
    class Program
    {
        enum Pos
        {
            П,
            С,
            А,
            None
        }

        public const string path3 = @"D:\player.txt";
        public const string path5 = @"D:\text.txt";
        public const string path6 = @"D:\com.txt";
        public const int WATCH_TABLE = 1;
        public const int ADD_RAW = 2;
        public const int REMOVE_RAW = 3;
        public const int UPDATE_RAW = 4;
        public const int FIND_RAW = 5;
        public const int SHOW_LOG = 6;
        public const int EXIT = 7;

        public struct HRD
        {
            public string Name; // Фамилия
            public string Postion;//Долнжость
            public int Yers;//Год рождения
            public string Salary;//Оклад

            internal void ShowTable(string name, string Postion, int Yers, string Salary)
            {
                Console.Write("{0,10}", name);
                Console.Write("{0,10}", Postion);
                Console.Write("{0,10}", Yers);
                Console.Write("{0,10}", Salary);
                Console.WriteLine();
            }
        }

        //Списки
        static List<HRD> list = new List<HRD>(50);



        //Список опираций
        enum Operations
        {
            ADD,
            DELETE,
            UPDATE,
            LOOK,
            SEARCH
        };

        //ЛОГИРОВАНИЕ
        struct Logging
        {
            static List<Logging> log = new List<Logging>();
            public DateTime time;
            public Operations action;
            public String data;

            public static Logging Add(DateTime dt, Operations operation, string s)
            {
                log.Add(new Logging(dt, operation, s));
                return log[log.Count - 1];
            }

            public Logging(DateTime Time, Operations Operations, String Date)
            {
                time = Time;
                action = Operations;
                data = Date;
            }

            public static void ShowInfo()
            {

                foreach (Logging l in log)
                {
                    l.PrintLog();
                }
            }
            public void PrintLog()
            {
                Console.Write("{0,10}", time);
                Console.Write("{0,20}  ", action);
                Console.WriteLine("{0,10}", data);
            }



        }
        public class Node<T>
        {
            public Node(T data)
            {
                Data = data;
            }
            public T Data { get; set; }
            public Node<T> Next { get; set; }
        }
        static int[,] sum(int[,] matrix, int[,] matrix2, int a)
        {
            int[,] arrayOutput = new int[a, a];
            for (int i = 0; i < arrayOutput.GetLength(0); i++)
            {
                for (int j = 0; j < arrayOutput.GetLength(1); j++)
                {
                    arrayOutput[i, j] = matrix[i, j] + matrix2[i, j];
                    Console.Write("{0} ", arrayOutput[i, j]);
                }
                Console.WriteLine();
            }
            return arrayOutput;
        }
        static int[,] multi(int[,] matrix, int[,] matrix2, int a)
        {
            int[,] arrayOutput = new int[a, a];
            for (int i = 0; i < arrayOutput.GetLength(0); i++)
            {
                for (int j = 0; j < arrayOutput.GetLength(1); j++)
                {
                    arrayOutput[i, j] = matrix[i, j] * matrix2[i, j];
                    Console.Write("{0} ", arrayOutput[i, j]);
                }
                Console.WriteLine();
            }
            return arrayOutput;
        }
        static int[,] trans(int[,] matrix, int a)
        {
            int[,] arrayOutput = new int[a, a];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    arrayOutput[i, j] = matrix[j, i];
                    Console.Write("{0} ", arrayOutput[i, j]);
                }
                Console.WriteLine();
            }
            return arrayOutput;
        }
        static int[,] trans2(int[,] matrix2, int a)
        {
            int[,] arrayOutput = new int[a, a];
            for (int i = 0; i < matrix2.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    arrayOutput[i, j] = matrix2[j, i];
                    Console.Write("{0} ", arrayOutput[i, j]);
                }
                Console.WriteLine();
            }
            return arrayOutput;
        }
        struct WR
        {
            public string name;
            public Pos position;
            public int year;
            public decimal salary;
            public void showTable()
            {
                Console.Write("{0,10}", name);
                Console.Write("{0,10}", position);
                Console.Write("{0,10}", year);
                Console.Write("{0,10}", salary);
                Console.WriteLine();
            }
        }
        struct Log
        {
            public DateTime time;
            public string operation;
            public void logOutput()
            {
                Console.Write("{0,10}", time);
                Console.Write("{0,20}  ", operation);
            }
        }
        public class DoublyNode<T>
        {
            public DoublyNode(T data)
            {
                Data = data;
            }
            public T Data { get; set; }
            public DoublyNode<T> Previous { get; set; }
            public DoublyNode<T> Next { get; set; }
        }
        public class DoublyLinkedList<T> : IEnumerable<T>
        {
            DoublyNode<T> start;
            DoublyNode<T> end;
            int count;
            public void Add(T data)
            {
                DoublyNode<T> node = new DoublyNode<T>(data);
                if (start == null)
                    start = node;
                else
                {
                    end.Next = node;
                    node.Previous = end;
                }
                end = node;
                count++;
            }
            public bool Remove(T data)
            {
                DoublyNode<T> current = start;
                while (current != null)
                {
                    if (current.Data.Equals(data))
                        break;
                    current = current.Next;
                }
                if (current != null)
                {
                    if (current.Next != null)
                        current.Next.Previous = current.Previous;
                    else
                        end = current.Previous;
                    if (current.Previous != null)
                        current.Previous.Next = current.Next;
                    else
                        start = current.Next;
                    count--;
                    return true;
                }
                return false;
            }
            public void Change(T dataToChange, T newData)
            {
                DoublyNode<T> node = new DoublyNode<T>(dataToChange);
                DoublyNode<T> current = start;
                while (current != null)
                {
                    if (current.Data.Equals(dataToChange))
                        break;
                    current = current.Next;
                }
                current.Data = newData;
            }
            public int Count { get { return count; } }
            public void Clear()
            {
                start = null;
                end = null;
                count = 0;
            }
            public bool Contains(T data)
            {
                DoublyNode<T> current = start;
                while (current != null)
                {
                    if (current.Data.Equals(data))
                        return true;
                    current = current.Next;
                }
                return false;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)this).GetEnumerator();
            }
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                DoublyNode<T> current = start;
                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
            static void Main(string[] args)
            {
            save:
                Console.Write("Введите номер задания: ");
                int number = Convert.ToInt32(Console.ReadLine());
                switch (number)
                {
                    case 1:
                        break;
                    case 2:
                        ex2();
                        goto save;
                    case 3:
                        ex3();
                        goto save;
                    case 4:
                        ex4();
                        goto save;
                    case 5:
                        ex5();
                        goto save;
                    case 6:
                        ex6();
                        goto save;
                    case 7:
                        ex7();
                        goto save;
                    case 8:
                        ex8();
                        goto save;
                }

            }
            private static void ex1()
            {
                Console.WriteLine("Введите номер подзадания (1 или 2) , который хотите увидеть  ");
                int tmp = Convert.ToInt32(Console.ReadLine());
                if (tmp == 1)
                {
                    int choice = 0;
                    do
                    {
                        Console.WriteLine("Выберите пункт");
                        Console.WriteLine("1 - Просмотр таблицы");
                        Console.WriteLine("2 - добавить запись");
                        Console.WriteLine("3 - Удалить запись");
                        Console.WriteLine("4 - обновить запись");
                        Console.WriteLine("5 - поиск записей");
                        Console.WriteLine("6 - просмотреть лог");
                        Console.WriteLine("7 - Выход");
                        choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case WATCH_TABLE:
                                Console.WriteLine("{0,10} {1,10} {2,10} {3,10}", "Фамилия", "Должность", "Год рождения", "Оклад");
                                for (int list_item = 0; list_item < list.Count; list_item++)
                                {
                                    HRD t = list[list_item];
                                    Console.WriteLine("----------------------------------------------------------------------------");
                                    t.ShowTable(t.Name, t.Postion, t.Yers, t.Salary);

                                }
                                Logging.Add(DateTime.Now, Operations.LOOK, "Просмотрена таблица");
                                break;

                            case ADD_RAW:
                                HRD t1;
                                Console.WriteLine("Введите Фамилию");
                                t1.Name = Console.ReadLine();
                                Console.WriteLine("Введите Должность");
                                t1.Postion = Console.ReadLine();

                            Found1:
                                Console.WriteLine("Введите год рождения");
                                try
                                {
                                    int blabla = Convert.ToInt32(Console.ReadLine()); //вводим данные, и конвертируем в целое число  
                                    t1.Yers = blabla;
                                    if ((blabla < 1895) || (blabla > 2030))
                                    {
                                        Console.WriteLine("Error. (Введите повторно)");
                                        goto Found1;
                                    }
                                }
                                catch (FormatException)
                                {
                                    t1.Yers = 000;
                                    Console.WriteLine("Error. (Введите повторно)");
                                    goto Found1;
                                }
                                Pos pro;
                            Found3:
                                Console.WriteLine("Введите оклад");
                                try
                                {
                                    string blabla3 = Console.ReadLine();
                                    t1.Salary = blabla3;
                                    pro = (Pos)Enum.Parse(typeof(Pos), blabla3);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error. (Введите повторно)");
                                    pro = Pos.None;
                                    goto Found3;
                                }

                                list.Add(t1);
                                Console.WriteLine("Строка была добавлена!");
                                Console.WriteLine();
                                Logging.Add(DateTime.Now, Operations.ADD, "Строка добавлена в таблицу!");
                                break;
                            case REMOVE_RAW:
                                Console.WriteLine("Введите номер строки, которую хотите удалить");
                                int number = int.Parse(Console.ReadLine());
                                try
                                {
                                    list.RemoveAt(number - 1);
                                }
                                catch (Exception e) { Console.WriteLine("Строки с таким номером нет!"); }
                                Console.WriteLine();
                                Logging.Add(DateTime.Now, Operations.ADD, "Строка удалена!");
                                break;
                            case UPDATE_RAW:
                                Console.WriteLine("Введите номер строки, которую хотите изменить");
                                int UpdateIndex = int.Parse(Console.ReadLine());
                                try
                                {
                                    HRD t2 = list[UpdateIndex - 1];

                                    //Выводим старые значения

                                    t2.ShowTable(t2.Name, t2.Postion, t2.Yers, t2.Salary);


                                    //Вводим новые значения

                                    Console.WriteLine("Введите новое фамилию");
                                    t2.Name = Console.ReadLine();
                                    Console.WriteLine("Введите новую должность");
                                    t2.Postion = Console.ReadLine();
                                    Console.WriteLine("Введите новый год рождения");
                                Found2:
                                    t2.Yers = int.Parse(Console.ReadLine());
                                    try
                                    {
                                        int blabla2 = Convert.ToInt32(Console.ReadLine()); //вводим данные, и конвертируем в целое число  
                                        t2.Yers = blabla2;
                                        if ((blabla2 < 1895) || (blabla2 > 2030))
                                        {
                                            Console.WriteLine("Error. (Введите повторно)");
                                            goto Found2;
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        t2.Yers = 000;
                                        Console.WriteLine("Error. (Введите повторно)");
                                        goto Found2;
                                    }
                                    Console.WriteLine("Введите новый Тип");
                                    Pos pro2;
                                Found4:
                                    try
                                    {
                                        string blabla4 = Console.ReadLine();
                                        t2.Salary = blabla4;
                                        pro2 = (Pos)Enum.Parse(typeof(Pos), blabla4);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error. (Введите повторно)");
                                        pro2 = Pos.None;
                                        goto Found4;
                                    }
                                    list[UpdateIndex - 1] = t2;

                                }
                                catch (Exception e) { Console.WriteLine("Нет строки с таким номером!"); }
                                Logging.Add(DateTime.Now, Operations.ADD, "Строка обновлена!");
                                break;
                            case FIND_RAW:
                                Console.WriteLine("Введите фамилию");
                                string text = Console.ReadLine();
                                HRD FindRaw;
                                for (int item_list = 0; item_list < list.Count; item_list++)
                                {
                                    FindRaw = list[item_list];
                                    if (FindRaw.Name.ToLower().Equals(text.ToLower()))
                                    {
                                        Console.Write("{0,10}", FindRaw.Name);
                                        Console.Write("{0,10}", FindRaw.Postion);
                                        Console.Write("{0,10}", FindRaw.Yers);
                                        Console.Write("{0,10}", FindRaw.Salary);
                                        Console.WriteLine();
                                    }
                                }
                                Logging.Add(DateTime.Now, Operations.ADD, "Строка найдена!");
                                break;
                            case SHOW_LOG:
                                Logging.Add(DateTime.Now, Operations.ADD, "Логи просмотрены!");
                                Logging.ShowInfo();
                                break;
                            case EXIT:
                                break;
                        }
                    } while (choice != 7);
                }
                if (tmp == 2)
                {
                    int choice = 0;
                    do
                    {
                        var logOfSession = new DoublyLinkedList<Log>();
                        var table = new DoublyLinkedList<WR>();
                        int salary;
                        int year;
                        Console.WriteLine("Выберите пункт");
                        Console.WriteLine("1 - Просмотр таблицы");
                        Console.WriteLine("2 - добавить запись");
                        Console.WriteLine("3 - Удалить запись");
                        Console.WriteLine("4 - обновить запись");
                        Console.WriteLine("5 - поиск записей");
                        Console.WriteLine("6 - просмотреть лог");
                        Console.WriteLine("7 - Выход");
                        choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case WATCH_TABLE:
                                Console.WriteLine("{0,10} {1,10} {2,10} {3,10}", "Фамилия", "Должность", "Год рождения", "Оклад");
                                for (int list_item = 0; list_item < list.Count; list_item++)
                                {
                                    foreach (var item in table)
                                        item.showTable();
                                }
                                Log newLog;
                                newLog.time = DateTime.Now;
                                newLog.operation = "Просмотр таблицы";
                                logOfSession.Add(newLog);
                                break;

                            case ADD_RAW:
                                string name;
                                Pos position = new Pos();
                                Console.WriteLine("Введите Фамилию");
                                name = Console.ReadLine();
                                Console.WriteLine("Введите Должность");
                                string pos = Console.ReadLine();
                                if (pos == "П")
                                {
                                    position = Pos.П;
                                }
                                else if (pos == "С")
                                {
                                    position = Pos.С;
                                }
                                else if (pos == "А")
                                {
                                    position = Pos.А;
                                }
                                else
                                {
                                    Console.WriteLine("УПС!\n-_-");
                                }
                            Found1:
                                Console.WriteLine("Введите год рождения");
                                try
                                {
                                    int blabla = Convert.ToInt32(Console.ReadLine()); //вводим данные, и конвертируем в целое число  
                                    year = blabla;
                                    if ((blabla < 1895) || (blabla > 2030))
                                    {
                                        Console.WriteLine("Error. (Введите повторно)");
                                        goto Found1;
                                    }
                                }
                                catch (FormatException)
                                {
                                    year = 000;
                                    Console.WriteLine("Error. (Введите повторно)");
                                    goto Found1;
                                }
                                Pos pro;
                            Found3:
                                Console.WriteLine("Введите оклад");
                                try
                                {
                                    int blabla3 = Convert.ToInt32(Console.ReadLine());
                                    salary = blabla3;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error. (Введите повторно)");
                                    pro = Pos.None;
                                    goto Found3;
                                }
                                WR newWr;
                                newWr.name = name;
                                newWr.position = position;
                                newWr.salary = salary;
                                newWr.year = year;
                                Console.WriteLine("Строка была добавлена!");
                                Console.WriteLine();

                                newLog.time = DateTime.Now;
                                newLog.operation = "Строка добавлена";
                                logOfSession.Add(newLog); break;
                            case REMOVE_RAW:
                                Console.WriteLine("Введите номер строки, которую хотите удалить");
                                int number = int.Parse(Console.ReadLine());
                                try
                                {
                                    list.RemoveAt(number - 1);
                                }
                                catch (Exception e) { Console.WriteLine("Строки с таким номером нет!"); }
                                Console.WriteLine();
                                newLog.time = DateTime.Now;
                                newLog.operation = "Строка delet";
                                logOfSession.Add(newLog);  break;
                            case UPDATE_RAW:
                                Console.WriteLine("Введите номер строки, которую хотите изменить");
                                int UpdateIndex = int.Parse(Console.ReadLine());
                                try
                                {
                                    HRD t2 = list[UpdateIndex - 1];

                                    //Выводим старые значения

                                    t2.ShowTable(t2.Name, t2.Postion, t2.Yers, t2.Salary);


                                    //Вводим новые значения

                                    Console.WriteLine("Введите новое фамилию");
                                    t2.Name = Console.ReadLine();
                                    Console.WriteLine("Введите новую должность");
                                    t2.Postion = Console.ReadLine();
                                    Console.WriteLine("Введите новый год рождения");
                                Found2:
                                    t2.Yers = int.Parse(Console.ReadLine());
                                    try
                                    {
                                        int blabla2 = Convert.ToInt32(Console.ReadLine()); //вводим данные, и конвертируем в целое число  
                                        t2.Yers = blabla2;
                                        if ((blabla2 < 1895) || (blabla2 > 2030))
                                        {
                                            Console.WriteLine("Error. (Введите повторно)");
                                            goto Found2;
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        t2.Yers = 000;
                                        Console.WriteLine("Error. (Введите повторно)");
                                        goto Found2;
                                    }
                                    Console.WriteLine("Введите новый Тип");
                                    Pos pro2;
                                Found4:
                                    try
                                    {
                                        string blabla4 = Console.ReadLine();
                                        t2.Salary = blabla4;
                                        pro2 = (Pos)Enum.Parse(typeof(Pos), blabla4);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error. (Введите повторно)");
                                        pro2 = Pos.None;
                                        goto Found4;
                                    }
                                    list[UpdateIndex - 1] = t2;

                                }
                                catch (Exception e) { Console.WriteLine("Нет строки с таким номером!"); }
                                newLog.time = DateTime.Now;
                                newLog.operation = "Строка обновлена";
                                logOfSession.Add(newLog); break;                             case FIND_RAW:
                                Console.WriteLine("Введите фамилию");
                                string text = Console.ReadLine();
                                HRD FindRaw;
                                for (int item_list = 0; item_list < list.Count; item_list++)
                                {
                                    FindRaw = list[item_list];
                                    if (FindRaw.Name.ToLower().Equals(text.ToLower()))
                                    {
                                        Console.Write("{0,10}", FindRaw.Name);
                                        Console.Write("{0,10}", FindRaw.Postion);
                                        Console.Write("{0,10}", FindRaw.Yers);
                                        Console.Write("{0,10}", FindRaw.Salary);
                                        Console.WriteLine();
                                    }
                                }
                                newLog.time = DateTime.Now;
                                newLog.operation = "Строка найдена";
                                logOfSession.Add(newLog);                                 newLog.time = DateTime.Now;
                                newLog.operation = "Логи просмотрены";
                                logOfSession.Add(newLog); 
                                break;
                            case EXIT:
                                break;
                        }
                    } while (choice != 7);
                }

            }
            }
            private static void ex2()
            {
                Console.Write("Write example: ");
                string ex = Console.ReadLine();
                string brackets = String.Empty;
                foreach (char ch in ex)
                    if (ch == '(' || ch == ')')
                        brackets += ch;
                Stack<char> stk = new Stack<char>();
                foreach (char ch in brackets)
                    if (ch == '(')
                        stk.Push(ch);
                    else if (ch == ')' && stk.Count != 0)
                        stk.Pop();
                if (stk.Count == 0)
                    Console.WriteLine("Cool");
                else
                    Console.WriteLine("ERROR");
                Console.ReadKey();
            }
            public class CircularLinkedList<T> : IEnumerable<T>
            {
                Node<T> head;
                Node<T> tail;
                int count;
                public void Add(T data)
                {
                    Node<T> node = new Node<T>(data);
                    if (head == null)
                    {
                        head = node;
                        tail = node;
                        tail.Next = head;
                    }
                    else
                    {
                        node.Next = head;
                        tail.Next = node;
                        tail = node;
                    }
                    count++;
                }
                public void Counting(int start, int count)
                {
                    for (int i = 0; i < start + count - 2; i++)
                        head = head.Next;
                    Console.WriteLine("Победитель: " + head.Data);
                }
                IEnumerator IEnumerable.GetEnumerator()
                {
                    return ((IEnumerable)this).GetEnumerator();
                }
                IEnumerator<T> IEnumerable<T>.GetEnumerator()
                {
                    Node<T> current = head;
                    do
                    {
                        if (current != null)
                        {
                            yield return current.Data;
                            current = current.Next;
                        }
                    }
                    while (current != head);
                }
            }
            private static void ex3()
            {
                int select = 0;
                while (select > 2 || select < 1)
                {
                    Console.Write("1 first method, 2 second method");
                    select = int.Parse(Console.ReadLine());
                }
                string people;
                using (StreamReader file = new StreamReader(path3))
                    people = file.ReadToEnd();
                string[] tmp = people.Split(new char[] { ' ' });
                Console.Write("Запишите считалочку: ");
                string[] count = Console.ReadLine().Split(new char[] { ' ', ',', '-', '.', '?', ';', ':', '"' }, StringSplitOptions.RemoveEmptyEntries);
                Console.Write("Выбирите с какого участника начать от 1 до {0}: ", tmp.Length);
                int start = 0;
                while (start > tmp.Length || start < 1)
                    start = int.Parse(Console.ReadLine());
                if (select == 1)
                {
                    CircularLinkedList<string> users = new CircularLinkedList<string>();
                    foreach (string s in tmp)
                        users.Add(s);
                    users.Counting(start, count.Length);
                }
                else if (select == 2)
                    Console.WriteLine("Победитель: " + tmp[(count.Length % tmp.Length) - 2 + start]);
                Console.ReadKey();
            }
            private static void ex4()
            {
                Dictionary<int, int> numbers = new Dictionary<int, int>();
                int N = 50000;
                int limit = Convert.ToInt32(Math.Pow(N, 1.0 / 3.0));
                int count = 0;
                int[] coin = new int[N + 1];
                for (int x = 0; x <= limit; x++)
                    for (int y = 0; y <= limit; y++)
                        for (int z = 0; z <= limit; z++)
                            if (Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(z, 3) <= N && Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(z, 3) > 0)
                                numbers.Add(++count, Convert.ToInt32((Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(z, 3))));
                foreach (var number in numbers)
                    if (number.Value <= N)
                        coin[number.Value]++;
                for (int i = 0; i < coin.Length; i++)
                    if (coin[i] > 2)
                        Console.WriteLine(i);
                Console.ReadKey();
            }
            private static void ex5()
            {
                string[] txt;
                using (StreamReader sr = new StreamReader(path5))
                    txt = sr.ReadToEnd().ToLower().Split(new char[] { ' ', ',', '-', '.', ';', ':', '?' }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, int> rep = new Dictionary<string, int>();
                foreach (string word in txt)
                {
                    if (rep.Keys.Contains<string>(word))
                        rep[word]++;
                    else
                        rep.Add(word, 1);
                }
                int conter = 10;
                foreach (var word in rep.OrderBy(key => key.Key).OrderByDescending(key => key.Value))
                {
                    if (conter > 0)
                        Console.WriteLine(word);
                    conter--;
                }
                Console.ReadKey();
            }


            class BinaryTreeNode
            {
                public matches data;
                public BinaryTreeNode left = null;
                public BinaryTreeNode right = null;
            }
            class BinaryTree
            {
                BinaryTreeNode root = null;

                public BinaryTreeNode AddRoot(matches elem)
                {
                    root = new BinaryTreeNode { data = elem };
                    return root;
                }
                public BinaryTreeNode AddLeft(BinaryTreeNode node, matches elem)
                {
                    var newNode = new BinaryTreeNode { data = elem };
                    node.left = newNode;
                    return newNode;
                }
                public BinaryTreeNode AddRight(BinaryTreeNode node, matches elem)
                {
                    var newNode = new BinaryTreeNode { data = elem };
                    node.right = newNode;
                    return newNode;
                }
                public void PreOrder(BinaryTreeNode node, int level = 0)
                {

                    if (node != null)
                    {
                        PreOrder(node.left, level + 1);
                        for (int i = 0; i < level; i++)
                            Console.Write("     ");
                        Console.WriteLine(node.data.tmp() + "\n");
                        PreOrder(node.right, level + 1);

                    }
                }
                public void PreOrderTraversal()
                {
                    PreOrder(root);
                }
            }
            struct matches
            {
                public string team1, team2;
                public int score1, score2;
                public string tmp()
                {
                    return (team1 + " - " + team2 + " : " + score1 + " - " + score2);
                }
            }
            static matches Nextmatch(matches match1, matches match2)
            {
                string newteam1 = "";
                string newteam2 = "";
                Random rnd = new Random();

                if (match1.score1 >= match1.score2)
                    newteam1 = match1.team1;

                else
                    newteam1 = match1.team2;

                if (match2.score1 >= match2.score2)
                    newteam2 = match2.team1;
                else
                    newteam2 = match2.team2;

                matches newmatch = new matches();
                newmatch.team1 = newteam1;
                newmatch.team2 = newteam2;
                newmatch.score1 = rnd.Next(0, 5);
                newmatch.score2 = rnd.Next(0, 5);
                while (newmatch.score1 == newmatch.score2)
                {
                    newmatch.score1 = rnd.Next(0, 5);
                    newmatch.score2 = rnd.Next(0, 5);
                }
                return newmatch;
            }

            private static void ex6()
            {
                Random rnd = new Random();
                var tree = new BinaryTree();
                string[] teams = { "BEL", "FRA", "BRA", "ENG", "URU", "CRO", "POR", "SPA", "ARG", "COL", "MEX", "SWI", "ITA", "GER", "CHI", "SWE" };
                matches[] match = new matches[teams.Length / 2];
                matches empmat = new matches();
                empmat.team1 = String.Empty;
                empmat.team2 = String.Empty;
                empmat.score1 = 0;
                empmat.score2 = 0;
                BinaryTreeNode[] node = new BinaryTreeNode[15];
                node[0] = tree.AddRoot(empmat);
                node[1] = tree.AddLeft(node[0], empmat);
                node[2] = tree.AddRight(node[0], empmat);
                for (int i = 1, j = 3; i < (node.Length - 1) / 2; i++)
                {
                    node[j] = tree.AddLeft(node[i], empmat);
                    j++;
                    node[j] = tree.AddRight(node[i], empmat);
                    j++;
                }
                for (int i = 0, j = 0; i < match.Length; i++, j += 2)
                {
                    matches matche = new matches();
                    matche.team1 = teams[j];
                    matche.team2 = teams[j + 1];
                    matche.score1 = rnd.Next(0, 5);
                    matche.score2 = rnd.Next(0, 5);
                    while (matche.score1 == matche.score2)
                    {
                        matche.score1 = rnd.Next(0, 5);
                        matche.score2 = rnd.Next(0, 5);
                    }
                    match[i] = matche;
                }
                for (int i = node.Length - 1, j = match.Length - 1; j > -1; i--, j--)
                {
                    node[i].data = match[j];
                }
                for (int i = 6; i > -1; i--)
                {
                    node[i].data = Nextmatch(node[i].left.data, node[i].right.data);
                }
                tree.PreOrderTraversal();
                Console.WriteLine();
                Console.ReadKey();
            }


            private static void ex7()
            {
                Console.WriteLine("Пишите что вам угодно");
                string tmp = Console.ReadLine();
                //string[] words = tmp.Split(new char[] { ' ', ',', '-', '.', ';', ':', '?' }, StringSplitOptions.RemoveEmptyEntries);
                char[] words = tmp.ToCharArray();
                List<char> list = new List<char>();
                List<char> newlist = new List<char>();

                for (int i = 0; i < words.Length; i++)
                {
                    list.Add(words[i]);
                }
                for (int i = 0; i < words.Length; i++)
                {
                    if (!newlist.Contains(words[i]))
                    {
                        if (list.Contains(words[i]))
                            newlist.Add(words[i]);
                    }
                    else
                    {
                        newlist.Remove(words[i]);
                        list.Add(words[i]);
                    }
                }
                foreach (char c in newlist)
                {
                    Console.WriteLine(c);
                }

                Console.ReadKey();
            }

            private static void ex8()
            {
                Random rnd = new Random();
                Console.Write("Введите размерность матрицы: ");
                int a = Convert.ToInt32(Console.ReadLine());
                int[,] matrix = new int[a, a];
                int[,] matrix2 = new int[a, a];
                int[,] array = new int[a, a];
                int[,] mT = new int[a, a];
                int[,] mT2 = new int[a, a];
                CircularLinkedList<int> linkedList = new CircularLinkedList<int>();
                Console.WriteLine("Первая матрица: \n");
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[rnd.Next(0, a - 1), rnd.Next(0, a - 1)] = rnd.Next(0, 2);
                        Console.Write("{0} ", matrix[i, j]);
                        linkedList.Add(matrix[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Вторая матрица: \n");
                for (int i = 0; i < matrix2.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix2.GetLength(1); j++)
                    {
                        matrix2[rnd.Next(0, a - 1), rnd.Next(0, a - 1)] = rnd.Next(0, 2);
                        Console.Write("{0} ", matrix2[i, j]);
                    }
                    Console.WriteLine();
                }
                int[,] arrayOutput = new int[a, a];
                Console.WriteLine("Сумма матриц: \n");
                arrayOutput = sum(matrix, matrix2, a);
                Console.WriteLine("Умножение матриц: \n");
                array = multi(matrix, matrix2, a);
                Console.WriteLine("Транспонирования первой матрицы: \n");
                mT = trans(matrix, a);
                Console.WriteLine("Транспонирования второй матрицы: \n");
                mT2 = trans2(matrix2, a);
                foreach (char c in linkedList)
                {
                    Console.WriteLine(c);
                }
                Console.ReadKey();
            }
        }
}

