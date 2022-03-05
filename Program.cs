using System;
namespace quazare {
	class Квазар {
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
					stavka = Ставка(vse);
					vse = Пересчёт(vse, stavka);
					Console.WriteLine($"автомат: {avtomat = Автомат(0, 0)}");
					while (avtomat <= 20) {
						if (avtomat > 14) {
							if (avtomat == 20) {
								Console.WriteLine("ВЫ ПОБЕДИЛИ!");
								stavka = Win(avtomat, stavka);
								break;
							}
							else
							{
								Console.WriteLine("введите 1, 2 или 3\n1: добавить 1-8;\n2: добавить 4-7;\n3: забрать.");
								resh = Convert.ToInt32(Ввод());
								while (resh > 3 || resh < 1) {
									Console.WriteLine("введите 1, 2 или 3!!!");
									resh = Convert.ToInt32(Ввод());
								}
								if (resh == 1 || resh == 2) avtomat = Решение(avtomat, resh);
								else {
									stavka = Win(avtomat, stavka);
									break;
								}
							}
						}
						else {
							Console.WriteLine("введите 1 или 2\n1: добавить 1-8;\n2: добавить 4-7.");
							resh = Convert.ToInt32(Ввод());
							while (resh > 2 || resh < 1) {
								Console.WriteLine("введите 1 или 2!!!");
								resh = Convert.ToInt32(Ввод());
							}
							avtomat = Решение(avtomat, resh);
						}
					}
					if (avtomat > 20) {
						Console.WriteLine("вы проиграли");
						stavka = Win(avtomat, stavka);
						Br();
					}
					vse += stavka;
					//вывод выигрыша
					Console.WriteLine($"ваш выигрыш: {Math.Floor(stavka)}");
					Console.WriteLine($"ваши деньги: {Math.Floor(vse)}");
					if (vse > record) {
						record = vse;
						Console.WriteLine($"\nрекордное количество ваших денег: {Math.Floor(record)}");
					}
					Br();
					//решение о продолжение игры
					if (vse >= 20) {
						Console.WriteLine("продолжить?\n1.да\n2.нет");
						ans = Convert.ToInt32(Ввод());
						Br();
					}
					else if (vse < 20) {
						Console.WriteLine("у вас не осталось денег,\nвы проиграли.");
						ans = 2;
						Br();
					}
				} while (ans == 1);
				//решение о начале игры заново
				if (vse == 0) {
					Console.WriteLine("заново?\n1.да\n2.нет\n");
					x = Convert.ToInt32(Ввод());
					Br();
				}
				if (x != 1) {
					Console.WriteLine($"\nрекордное количество ваших денег: {Math.Floor(record)}");
					Br();
				}
			} while (x == 1);
		}
		static double Ставка(double vse) {
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
			doing = Convert.ToInt32(Ввод());
			//система против дурака
			while (doing > 4 || doing < 1) {
				Console.WriteLine("можно ввести от 1 до 4!!");
				doing = Convert.ToInt32(Ввод());
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
					stavka = Convert.ToDouble(Ввод());
					//система от дурака
					while (stavka < 20 || stavka > 200 || stavka > vse) {
						if (stavka > 200 || stavka < 20) Console.WriteLine("введите от 20 до 200!!");
						if (stavka > vse && stavka <= 200 && stavka >= 20) { 
							Console.WriteLine($"ставка не может быть больше чем количество ваших денег.\nу вас денег: {vse}"); 
						}
						Console.WriteLine("ставка (20-200): ");
						stavka = Convert.ToDouble(Ввод());
					}
					break;
			}
			return stavka;
		}
		static double Пересчёт(double vse, double stavka) {
			vse -= stavka;
			Console.WriteLine($"\nваши деньги: {Math.Floor(vse)}");
			Br();
			return vse;
		}
		static int Автомат(int avtomat, int x) {
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
		static int Решение(int avtomat, int resh) {
			//выбор от решения
			switch (resh) {
				case 1:
					avtomat = Автомат(avtomat, 1);
					Console.WriteLine($"автомат: {avtomat}");
					break;
				case 2:
					avtomat = Автомат(avtomat, 2);
					Console.WriteLine($"автомат: {avtomat}");
					break;
			}
			return avtomat;
		}
		static string Ввод() {
			string? n;
			bool trueno = false;
			n = Console.ReadLine();
			while (trueno) {
				trueno = false;
				for (int i = 0; i < n.Length; i++) {
					if (Char.IsNumber(n, i) == false) {
						Console.WriteLine("ты че какие буквы при ввводе чисел..");
						trueno = true;
						break;
					}
				} 
			}
			while (n == "") {
				Console.WriteLine("поаккуратнее с пустым вводом");
				n = Console.ReadLine();
			} 
			return n;
		}
		//функция для определения выигрыша
		static double Win(int a, double dengi) {
			switch (a) {
				case 15:
					dengi *= 0.25;
					break;
				case 16:
					dengi *= 0.5;
					break;
				case 17:
					dengi = dengi;
					break;
				case 18:
					dengi *= 1.25;
					break;
				case 19:
					dengi *= 1.5;
					break;
				case 20:
					dengi *= 2;
					break;
				default:
					dengi = 0;
					break;
			}
			return dengi;
		}
		//отступ на несколько строк
		static void Br() { 
			Console.Write("\n\n\n");
		}
	}
}