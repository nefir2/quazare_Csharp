using System;
namespace quazare {
	class квазар {
		static void Main(string[] args) {
			//переменные
			double stavka, vse, record;
			int x, ans = 1, avtomat, resh;
			do {
				//стоковые значения для переменных
				x = 0;
				vse = 200;
				record = 0;
				do {
					stavka = ставка(vse);
					vse = пересчёт(vse, stavka);
					Console.WriteLine($"автомат: {avtomat = автомат(0, 0)}");
					while (avtomat <= 20) {
						if (avtomat > 14) {
							if (avtomat == 20) {
								Console.WriteLine("ВЫ ПОБЕДИЛИ!");
								stavka = win(avtomat, stavka);
								break;
							}
							else {
								Console.WriteLine("введите 1, 2 или 3\n1: добавить 1-8;\n2: добавить 4-7;\n3: забрать.");
								resh = Convert.ToInt32(Console.ReadLine());
								while (resh > 3 || resh < 1) {
									Console.WriteLine("введите 1, 2 или 3!!!");
									resh = Convert.ToInt32(Console.ReadLine());
								}
								if (resh == 1 || resh == 2) avtomat = решение(avtomat, resh);
								else {
									stavka = win(avtomat, stavka);
									break;
								}
							}
                        }
						else {
							Console.WriteLine("введите 1 или 2\n1: добавить 1-8;\n2: добавить 4-7.");
							resh = Convert.ToInt32(Console.ReadLine());
							while (resh > 2 || resh < 1) {
								Console.WriteLine("введите 1 или 2!!!");
								resh = Convert.ToInt32(Console.ReadLine());
							}
							avtomat = решение(avtomat, resh);
						}
                    }
					if (avtomat > 20) { 
						Console.WriteLine("вы проиграли"); 
						stavka = win(avtomat, stavka); 
						br(); 
					}
					vse += stavka;
					//вывод выигрыша
					Console.WriteLine($"ваш выигрыш: {Math.Floor(stavka)}");
					Console.WriteLine($"ваши деньги: {Math.Floor(vse)}");
					if (vse > record) {
						record = vse;
						Console.WriteLine($"\nрекордное количество ваших денег: {Math.Floor(record)}");
					}
					br();
					//решение о продолжение игры
					if (vse >= 20) {
						Console.WriteLine("продолжить?\n1.да\n2.нет");
						ans = Convert.ToInt32(Console.ReadLine());
						br();
					} else if (vse < 20){
						Console.WriteLine("у вас не осталось денег,\nвы проиграли.");
						ans = 2;
						br();
					}
				} while (ans == 1);
				//решение о начале игры заново
				if (vse == 0) {
					Console.WriteLine("заново?\n1.да\n2.нет\n");
					x = Convert.ToInt32(Console.ReadLine());
					br();
				} 
				if (x != 1) {
					Console.WriteLine($"\nрекордное количество ваших денег: {Math.Floor(record)}");
					br(); 
				}
			} while (x == 1);
		}
		static double ставка(double vse) {
			//переменные
			int doing; //выбор пользователя
			double stavka = 0; //объявление ставки
			//вывод меню
			Console.WriteLine($"у вас всего денег: {Math.Floor(vse)}");
			Console.WriteLine("введите номер действия со ставкой:\n");
			Console.WriteLine("1.поставить все деньги(если больше 200 то ставится 200);");
			Console.WriteLine("2.поставить половину всех ваших денег(если больше 200 то поставится 100);");
			Console.WriteLine("3.поставить минимум(20);");
			Console.WriteLine("4.ввести свою ставку.");
			//получение выбора действия
			doing = Convert.ToInt32(Console.ReadLine());
			//система против дурака
			while (doing > 4 || doing < 1) {
				Console.WriteLine("можно ввести от 1 до 4!!");
				doing = Convert.ToInt32(Console.ReadLine());
			}
			//распределение по выбору
			switch (doing) {
				case 1: //все деньги
					if (vse > 200) stavka = 200; 
					else stavka = vse;
					break;
				case 2: //половина всех денег
					if (vse > 200) stavka = 100;
					else if (vse < 40) { 
						Console.WriteLine("меньше 20 ставить нельзя. ставка = 20"); 
						stavka = 20; 
					}
					else stavka = vse / 2;
					break;
				case 3: //минимальное количество денег
					stavka = 20;
					break;
				case 4: //ввод своей ставки
					Console.Write("ставка (20-200): ");
					stavka = Convert.ToDouble(Console.ReadLine());
					//система от дурака
					while (stavka < 20 || stavka > 200 || stavka > vse) {
						if (stavka > 200 || stavka < 20) Console.WriteLine("введите от 20 до 200!!");
						if (stavka > vse && stavka <= 200 && stavka >= 20) { 
							Console.WriteLine($"ставка не может быть больше чем количество ваших денег.\nу вас денег: {vse}"); 
						}
						Console.WriteLine("ставка (20-200): ");
						stavka = Convert.ToDouble(Console.ReadLine());
					}
					break;
			}
			return stavka;
		}
		static double пересчёт(double vse, double stavka) {
			vse -= stavka;
			Console.WriteLine($"\nваши деньги: {Math.Floor(vse)}");
			br();
			return vse;
		}
		static int автомат(int avtomat, int x) {
			//случайное число из автомата
			int bottom = 0, top = 0;
			Random rand = new Random();
			//гибкость под задачи программы
            switch (x) {
				case 0:
					bottom = 1;
					top = 12;
					break;
				case 1:
					bottom = 1;
					top = 8;
					break;
				case 2:
					bottom = 4;
					top = 7;
					break;
            }
			avtomat += rand.Next(bottom, top);
			return avtomat;
		}
		static int решение(int avtomat, int resh) {
			//выбор от решения
			switch (resh) {
				case 1:
					avtomat = автомат(avtomat, 1);
					Console.WriteLine($"автомат: {avtomat}");
					break;
				case 2:
					avtomat = автомат(avtomat, 2);
					Console.WriteLine($"автомат: {avtomat}");
					break;
            }
			return avtomat;
		}
		//функция для определения выигрыша
		static double win(int a, double dengi) {
			switch (a) {
				case 15:
					dengi = dengi * 0.25;
					break;
				case 16:
					dengi = dengi * 0.5;
					break;
				case 17:
					dengi = dengi;
					break;
				case 18:
					dengi = dengi * 1.25;
					break;
				case 19:
					dengi = dengi * 1.5;
					break;
				case 20:
					dengi = dengi * 2;
					break;
				default:
					dengi = 0;
					break;
			}
			return dengi;
		}
		//отступ на несколько строк
		static void br() { 
			Console.Write("\n\n\n");
		}
	}
}