﻿using System;
namespace quazare
{
    class Квазар
    {
        //глобальные переменные
        //вынесены за функцию "Main" для того чтобы работать с ними из любой другой (в особенности из "Комманды")
        static double stavka, vse, record; //для денег
        static int заново, продолжить, avtomat, решение = 0, exit = 0; //для действий
        static bool inCommand = false;
        static void Main()
        {
            Правила();
            Br(); //перенос строк 
            Console.WriteLine("если ввести \"/правила\" в любой момент программы, то правила появятся снова.");
            Console.WriteLine("также, если ввести \"/процент\", то выведется таблица получаемых денег.");
            Console.Write("об остальных командах можно узнать через /help.");
            Br(); //перенос строк 
            do //цикл "заново" (на случай полного проигрыша)
            {
                //стоковые значения для переменных при повторном начале игры
                заново = 0;
                vse = 200;
                record = 0;
                do //цикл "продолжить" (на случай, если остались деньги)
                {
                    Ставка(); //функция действий со ставкой
                    Пересчёт(); //пересчёт всех денег на основе ставки
                    Console.WriteLine($"автомат: {avtomat = Автомат(0, 0)}"); //первый выброс автомата
                    while (avtomat <= 20) //если выйдет за 20 то автоматический проигрыш ставки
                    {
                        if (avtomat > 14) //если выпало больше 14 то появляется третий вариант выбора
                        {
                            if (avtomat == 20) //автоматический выход на случай победы
                            {
                                Console.WriteLine("ВЫ ПОБЕДИЛИ!");
                                stavka = Win(avtomat, stavka); //пересчёт ставки
                                break; //выход из цикла "меньше равен 20" при победе
                            }
                            else //если автомат не равен 20
                            {
                                do //цикл для случая ввода команды
                                {
                                    if (решение == -26) //получаемый код при вводе команды
                                    {
                                        if (exit == 1) break;
                                        Console.WriteLine($"ваша ставка: {stavka}"); //вывод ставки для более точного определения выбора
                                        Console.WriteLine($"если вы заберёте сейчас, то вы получите {Win(avtomat, stavka)}"); //вывод возможного получаемого выигрыша для максимально точного выбора
                                        Br();
                                        Console.WriteLine($"автомат: {avtomat}"); //вывод автомата если была введена команда
                                    }
                                    Console.WriteLine("введите 1, 2 или 3:");
                                    Console.WriteLine("1: добавить 1-8;");
                                    Console.WriteLine("2: добавить 4-7;");
                                    Console.WriteLine("3: забрать.");
                                    решение = Convert.ToInt32(Ввод()); //ввод действия
                                    while (решение > 3 || решение < 1) //система против дурака
                                    {
                                        if (exit == 1) break;
                                        if (решение == -26) break; //выход из цикла если это была команда
                                        Console.WriteLine("введите 1, 2 или 3!!!");
                                        решение = Convert.ToInt32(Ввод());
                                    }
                                } while (решение == -26);
                                if (решение == 1 || решение == 2) avtomat = Решение(avtomat, решение); //выброс автомата с учётом выбора игрока
                                else //третий выбор
                                {
                                    stavka = Win(avtomat, stavka); //пересчёт ставки
                                    break; //выход из цикла
                                }
                            } //условие, если автомат не равен 20
                        } //условие, если автомат выдал больше 14
                        else //если выпадает 14 или меньше
                        {
                            if (exit == 1) break;
                            do //цикл для случая ввода команды
                            {
                                if (решение == -26)
                                {
                                    Console.WriteLine($"автомат: {avtomat}");
                                }
                                Console.WriteLine("введите 1 или 2");
                                Console.WriteLine("1: добавить 1-8;");
                                Console.WriteLine("2: добавить 4-7.");
                                решение = Convert.ToInt32(Ввод());
                                if (решение == -26)
                                {
                                    if (exit == 1) break;
                                    Console.WriteLine($"ваша ставка: {stavka}");
                                    Br();
                                }
                                while (решение > 2 || решение < 1) //система против дурака
                                {
                                    if (решение == -26) break;
                                    Console.WriteLine("введите 1 или 2!!!");
                                    решение = Convert.ToInt32(Ввод());
                                }
                            } while (решение == -26);
                            avtomat = Решение(avtomat, решение); //выброс автомата с учётом выбора игрока
                        } // условие, если автомат выдал 14 или меньше
                    } //конец цикла игры
                    if (exit == 1) break;
                    if (avtomat > 20) //проверка на проигрыш
                    {
                        Console.WriteLine("вы проиграли");
                        stavka = Win(avtomat, stavka); //пересчёт денег при проигрыше
                        Br();
                    }
                    vse += stavka; //возврат денег на весь счёт
                    //вывод выигрыша
                    Console.WriteLine();
                    Console.WriteLine($"ваш выигрыш: {Math.Floor(stavka)}"); //вывод выигрыша, без остатка
                    Console.WriteLine($"ваши деньги: {Math.Floor(vse)}"); //вывод всех денег, без остатка
                    if (vse > record) //если побито рекордное количество денег, появляется сообщение
                    {
                        record = vse;
                        Console.WriteLine();
                        Console.WriteLine($"рекордное количество ваших денег: {Math.Floor(record)}");
                    }
                    Br(); //перенос строк
                    if (vse >= 20) //решение о продолжении игры, если всех денег хватает
                    {
                        do //цикл на случай если введена команда
                        {
                            Console.WriteLine("продолжить?");
                            Console.WriteLine("1.да");
                            Console.WriteLine("2.нет");
                            продолжить = Convert.ToInt32(Ввод());
                            if (exit == 1) break;
                            Br();
                        } while (продолжить == -26);
                    }
                    else //если все деньги меньше 20
                    {
                        Console.WriteLine("у вас не осталось денег,");
                        Console.WriteLine("вы проиграли.");
                        продолжить = 2; //принудительное окончание игры, из-за проигрыша денег
                        Br();
                    }
                } while (продолжить == 1);
                if (exit == 1) break;
                if (vse < 20) //решение о начале игры заново. обход случая когда игрок захотел выйти из игры, но у него остались деньги
                {
                    do //цикл для команд
                    {
                        Console.WriteLine("заново?");
                        Console.WriteLine("1.да");
                        Console.WriteLine("2.нет");
                        заново = Convert.ToInt32(Ввод());
                        if (exit == 1) break;
                        Br();
                    } while (заново == -26);
                }
                if (заново != 1) //вывод рекорда если игрок захотел выйти
                {
                    Console.WriteLine();
                    Console.WriteLine($"рекордное количество ваших денег: {Math.Floor(record)}");
                    Br();
                }
            } while (заново == 1);
        } //конец игры
        static void Ставка() //вывод выбора ставки
        {
            //переменные
            int действие; //выбор пользователя
            do //цикл для случая ввода команды
            {
                //вывод меню
                Console.WriteLine($"у вас всего денег: {Math.Floor(vse)}"); //вывод всех денег, без остатка
                Console.WriteLine("введите номер действия со ставкой:");
                Console.WriteLine();
                Console.WriteLine("1.поставить все деньги(если больше 200 то ставится 200);");
                Console.WriteLine("2.поставить половину всех ваших денег(если больше 200 то поставится 100);");
                Console.WriteLine("3.поставить минимум(20);");
                Console.WriteLine("4.ввести свою ставку.");
                //получение выбора действия
                действие = Convert.ToInt32(Ввод());
                if (exit == 1) break;
                while (действие > 4 || действие < 1) //система против дурака
                {
                    if (действие == -26) break; //выход из цикла если введена команда
                    Console.WriteLine("можно ввести от 1 до 4!!");
                    действие = Convert.ToInt32(Ввод());
                }
            } while (действие == -26);
            switch (действие) //распределение по выбору
            {
                case 1: //все деньги
                    if (vse > 200) stavka = 200;
                    else stavka = vse;
                    break;
                case 2: //половина всех денег
                    if (vse > 200) stavka = 100;
                    else if (vse < 40)
                    {
                        Console.WriteLine("меньше 20 ставить нельзя. stavka = 20");
                        stavka = 20;
                    }
                    else stavka = vse / 2;
                    break;
                case 3: //минимальное количество денег
                    stavka = 20;
                    break;
                case 4: //ввод своей ставки
                    do //цикл для команд
                    {
                        Console.Write("stavka (20-200): ");
                        stavka = Convert.ToDouble(Ввод());
                        while (stavka < 20 || stavka > 200 || stavka > vse) //система против дурака
                        {
                            if (stavka == -26) break; //выход из цикла если введена команда
                            if (stavka > 200 || stavka < 20) Console.WriteLine("введите от 20 до 200!!"); //если выход за границы возможного ввода
                            if (stavka > vse && stavka <= 200 && stavka >= 20) //если в границах, но больше всех денег, чем есть не счету
                            {
                                Console.WriteLine("stavka не может быть больше чем количество ваших денег.");
                                Console.WriteLine($"у вас денег: {vse}");
                            }
                            Console.Write("stavka (20-200): ");
                            stavka = Convert.ToDouble(Ввод());
                        }
                    } while (stavka == -26);
                    break; //синтаксис свитча
            }
        }
        static void Пересчёт() //простая функция по пересчёту и выводу сообщения о количестве денег
        {
            vse -= stavka;
            Console.WriteLine();
            Console.WriteLine($"ваши деньги: {Math.Floor(vse)}");
            Br();
        }
        static int Автомат(int значение_автомата, int номер_действия) //случайное число из автомата
        { //верхнее значение (top) никогда не выпадет
            int bottom = 0, top = 0; //объявление границ случайного числа
            Random rand = new Random(); //объявление случайного числа
            switch (номер_действия) //выбор границ выброса значений из автомата
            {
                case 0: //первый выброс автомата
                    bottom = 1;
                    top = 11;
                    break;
                case 1: //первое решение игрока
                    bottom = 1;
                    top = 9; //9 не включительно
                    break;
                case 2: //второе решение игрока
                    bottom = 4;
                    top = 8; //8 не включительно
                    break;
            }
            значение_автомата += rand.Next(bottom, top); //единственное сложение значений автомата, иначе всё сломается
            return значение_автомата;
        }
        static int Решение(int значение_автомата, int номер_действия) //выбор действия игроком (свитч отделённый в отдельную функцию)
        {
            switch (номер_действия)
            {
                case 1:
                    значение_автомата = Автомат(значение_автомата, 1);
                    Console.WriteLine($"автомат: {значение_автомата}");
                    break;
                case 2:
                    значение_автомата = Автомат(значение_автомата, 2);
                    Console.WriteLine($"автомат: {значение_автомата}");
                    break;
            }
            return значение_автомата;
        }
        ///*
        static bool Сравнивалка(string изКого, string что) //отдельная функция для поиска совпадений
        {
            //char[] notaString = new char[что.Length];
            bool ans = false;
            int счётчикСовпадений = 0;
            if (изКого.Length > что.Length)
            {
                for (int i = 0; i < изКого.Length - что.Length; i++)
                {
                    for (int j = 0; j < что.Length; j++) if (изКого[j + i] == что[j]) счётчикСовпадений++;
                    if (счётчикСовпадений == что.Length) ans = true; //? true : false были опущены редактором
                    if (ans == true) break;
                    счётчикСовпадений = 0;
                }
            }
            else if (изКого.Length == что.Length)
            {
                for (int i = 0; i < что.Length; i++)
                {
                    if (изКого[i] == что[i]) счётчикСовпадений++;
                }
                if (счётчикСовпадений == что.Length) ans = true;
            }
            return ans;
        }
        //*/
        static double Чит_команда(string команда, int номерЧита)
        {
            double параметр = -1;
            if (Сравнивалка(команда, $"/-26 {номерЧита} ") && команда.Length > 6)
            {
                char[] howMuch = new char[команда.Length - 7];
                for (int i = 7; i < команда.Length; i++)
                {
                    if (Char.IsNumber(команда, i) == true)
                    {
                        howMuch[i - 7] = команда[i];
                    }
                    else
                    {
                        Console.WriteLine("ты как буквы превратишь в цифры");
                        return -1;
                    }
                }
                string chars = new string(howMuch);
                параметр = double.Parse(chars);
            }
            return параметр;
        }
        static void Комманды(string команда) //функция при вводе команд
        {
            double p;
            do
            {
                if (команда == "/percent" || команда == "/procent" || команда == "/процент" || команда == "/p") //вызов функции проценты
                {
                    Br();
                    Проценты();
                    Br();
                }
                else if (команда == "/правила" || команда == "/rules" || команда == "/r") //вызов функции правила
                {
                    Правила();
                    Br();
                }
                else if (команда == "/help" || команда == "/команды" || команда == "/помощь" || команда == "/h") //вывод команд
                {
                    Br();
                    Console.WriteLine("команды:");
                    Console.WriteLine("/help, /помощь или /команды - вывод списка команд;");
                    Console.WriteLine("/процент или /percent - вывод таблицы возвращаемых денег от значения автомата;");
                    Console.WriteLine("/правила или /rules - вывод правил.");
                    Console.WriteLine("есть сокращения команд: первая буква команды на английском");
                    Br();
                }
                else if (команда == "/back" || команда == "/верни" || команда == "/b") Br(); //вывод сообщения о действии, если старое потерялось в истории
                else if (Сравнивалка(команда, "/-26"))
                {
                    Br();
                    Console.WriteLine($"avtomat = {avtomat}");
                    Console.WriteLine($"vse = {vse}");
                    Console.WriteLine($"stavka = {stavka}");
                    Console.WriteLine($"record = {record}");
                    int номер_действия;
                    double сколько = -1;
                    if (команда.Length > 5 && Char.IsNumber(команда[5]) == true)
                    {
                        номер_действия = int.Parse(команда[5].ToString());
                        сколько = Чит_команда(команда, номер_действия);
                    }
                    else
                    {
                        Console.WriteLine("чо исполнять?");
                        номер_действия = Convert.ToInt32(Ввод());
                        if (номер_действия == -2) break;
                    }
                    switch (номер_действия)
                    {
                        case 0:
                            exit = 1;
                            break;
                        case 1:
                            if (сколько != -1) vse = сколько;
                            else
                            {
                                Console.Write("vse = ");
                                p = Convert.ToDouble(Ввод());
                                if (p == -2) break;
                                else vse = p;
                            }
                            break;
                        case 2:
                            if (сколько != -1) avtomat = (int)сколько;
                            else
                            {
                                Console.Write("автомат: ");
                                p = Convert.ToInt32(Ввод());
                                if (p == -2) break;
                                else avtomat = (int)p;
                            }
                            break;
                        case 3:
                            if (сколько != -1) stavka = сколько;
                            else
                            {
                                Console.Write("stavka = ");
                                p = Convert.ToDouble(Ввод());
                                if (p == -2) break;
                                else stavka = p;
                            }
                            break;
                        default:
                            Console.WriteLine("такой команды нет.");
                            break;
                    }
                }
                else //если не совпало ни с одной командой
                {
                    Console.WriteLine("команды нормально пиши");
                    Br();
                }
            } while (false);
        }
        static bool StringHaveNotNumbers(string numbers) //функция для проверки строки на цифры
        {
            bool yes = false;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (Char.IsNumber(numbers, i) == false) //если очередной символ оказывается не цифрой
                {
                    yes = true;
                    break; //достаточно одной буквы чтобы сломать ввод, поэтому даже при одной букве сразу выходим из цикла фор
                }
            }
            return yes;
        }
        static string Ввод() //ввод значений отделённый в функцию, на случай пустого или не численного ввода 
        { //в последствии превращённый в ловителя команд
            string ввод; //вводимое значение
            bool цикл; //логическая переменная для повторного ввода
            do
            {
                цикл = false; //если цикл останется false, то функция вернёт значение без повторов
                ввод = Console.ReadLine();
                if (ввод == "")
                {
                    if (inCommand == true)
                    {
                        ввод = "-2";
                    }
                    else
                    {
                        Console.WriteLine("поаккуратнее с пустым вводом");
                        цикл = true;
                    }
                }
                else //не пустой ввод
                {
                    if (ввод[0] == '/' && inCommand == false) //ожидание получения команды
                    { //команды
                        inCommand = true;
                        Комманды(ввод);
                        inCommand = false;
                        ввод = "-26"; //возврат кода в основную часть программы, для того чтобы снова вывести сообщение о вводе действия
                    }
                    else if (ввод[0] == '/' && inCommand == true)
                    {
                        Console.WriteLine("ну да, из команды вызвать команду, гениально!");
                    }
                    else //не команда
                    {
                        цикл = StringHaveNotNumbers(ввод);
                        if (цикл == true) Console.WriteLine("лишние буквы не пиши");
                    } //не команда
                } //не пустой ввод
            } while (цикл); //пока цикл возваращает true, повторяется действие
            return ввод;
        }
        static void Правила() //функция для вывода правил (используется при запуске программы и вводе команды)
        {
            Br();
            Console.WriteLine("для начала игры вам даётся 200 денег.");
            Console.WriteLine("сначала вы должны сделать ставку, после чего начнётся игра.");
            Console.WriteLine();
            Console.WriteLine("автомат выдаёт случайное число, и вы должны сделать выбор:");
            Console.WriteLine("добавить к этому числу от 1 до 8 или от 4 до 7.");
            Console.WriteLine();
            Console.WriteLine("если в автомате выпадет 20 очков, то вы выиграли.");
            Console.WriteLine("но если выпадает меньше, вы всё равно сможете забрать деньги.");
            Console.WriteLine("если забираете раньше 20 очков, вы получаете меньше денег.");
            Console.WriteLine();
            Console.WriteLine("если вам не повезло, и выпало больше 20,");
            Console.WriteLine("то вы автоматически проигрываете всю ставку.");
            Console.WriteLine();
            Проценты();
            Console.WriteLine("если меньше 15, то забрать нельзя, если больше 20 - вы получаете 0.");
        }
        static void Проценты() //функция вывода таблицы возвращаемых процентов (используется в правилах и при вводе команды)
        {
            Console.WriteLine("при выпадении очков, процент ставки вы получаете: ");
            Console.WriteLine("15    -    25%;");
            Console.WriteLine("16    -    50%;");
            Console.WriteLine("17    -    100%;");
            Console.WriteLine("18    -    125%;");
            Console.WriteLine("19    -    150%;");
            Console.WriteLine("20    -    200%.");
        }
        static double Win(int значение_автомата, double деньги) //функция пересчёта денег относительно значения автомата
        { //=> действие заменяемое на *=
            деньги *= значение_автомата switch //предложенный редактором кода вид свитча
            { //значение автомата: деньги {действие указанное выше строкой} число
                15 => 0.25, //15: деньги *= 0.25
                16 => 0.5, //16: деньги *= 0.5
                17 => 1, //17: деньги *= 1
                18 => 1.25, //18: деньги *= 1.25
                19 => 1.5, //19: деньги *= 1.5
                20 => 2, //20: деньги *= 2
                _ => 0, //default: деньги *= 0
            }; //без точки запятой умрёт
            return деньги;
        }
        static void Br() //отступ на несколько строк
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}