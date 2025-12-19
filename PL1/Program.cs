using System;

class Program
{
  static void Main()
  {
    Console.WriteLine("Выберите номер задания (2–7):");
    int task = ReadInt("Номер задания: ", 2, 7);

    switch (task)
    {
      case 2: Task2_LuckyTicket(); break;
      case 3: Task3_ReduceFraction(); break;
      case 4: Task4_GuessNumber(); break;
      case 5: Task5_CoffeeMachine(); break;
      case 6: Task6_Bacteria(); break;
      case 7: Task7_Mars(); break;
    }
  }

  // ===== Универсальная функция ввода целого числа =====
  static int ReadInt(string message, int? min = null, int? max = null)
  {
    int value;
    while (true)
    {
      Console.Write(message);
      string input = Console.ReadLine();

      if (!int.TryParse(input, out value))
      {
        Console.WriteLine("Ошибка: введите целое число!");
        continue;
      }

      if (min.HasValue && value < min.Value)
      {
        Console.WriteLine($"Ошибка: число должно быть не меньше {min.Value}");
        continue;
      }

      if (max.HasValue && value > max.Value)
      {
        Console.WriteLine($"Ошибка: число должно быть не больше {max.Value}");
        continue;
      }

      return value;
    }
  }

  // ===== Задание 2 =====
  static void Task2_LuckyTicket()
  {
    int ticket = ReadInt("Введите номер билета (6 цифр): ", 0, 999999);

    int a = ticket / 100000;
    int b = ticket / 10000 % 10;
    int c = ticket / 1000 % 10;
    int d = ticket / 100 % 10;
    int e = ticket / 10 % 10;
    int f = ticket % 10;

    bool lucky = (a + b + c) == (d + e + f);
    Console.WriteLine(lucky ? "Счастливый билет!" : "Обычный билет.");
  }

  // ===== Задание 3 =====
  static int GCD(int a, int b)
  {
    while (b != 0)
    {
      int t = b;
      b = a % b;
      a = t;
    }
    return Math.Abs(a);
  }

  static void Task3_ReduceFraction()
  {
    int m = ReadInt("Введите числитель: ");
    int n = ReadInt("Введите знаменатель (не 0): ", 1, int.MaxValue);

    int gcd = GCD(m, n);
    m /= gcd;
    n /= gcd;

    if (n < 0)
    {
      m = -m;
      n = -n;
    }

    Console.WriteLine($"Результат: {m} / {n}");
  }

  // ===== Задание 4 =====
  static void Task4_GuessNumber()
  {
    int left = 0, right = 63;

    for (int i = 0; i < 7; i++)
    {
      int mid = (left + right) / 2;
      int answer = ReadInt($"Ваше число больше {mid}? (1-да, 0-нет): ", 0, 1);

      if (answer == 1)
        left = mid + 1;
      else
        right = mid;
    }

    Console.WriteLine($"Ваше число: {left}");
  }

  // ===== Задание 5 =====
  static void Task5_CoffeeMachine()
  {
    int water = ReadInt("Введите количество воды в мл: ", 0);
    int milk = ReadInt("Введите количество молока в мл: ", 0);

    int americano = 0, latte = 0, money = 0;

    while (water >= 300 || (water >= 30 && milk >= 270))
    {
      int choice = ReadInt("Выберите напиток (1 — американо, 2 — латте): ", 1, 2);

      if (choice == 1)
      {
        if (water >= 300)
        {
          water -= 300;
          americano++;
          money += 150;
          Console.WriteLine("Ваш напиток готов");
        }
        else Console.WriteLine("Не хватает воды");
      }
      else
      {
        if (water >= 30 && milk >= 270)
        {
          water -= 30;
          milk -= 270;
          latte++;
          money += 170;
          Console.WriteLine("Ваш напиток готов");
        }
        else Console.WriteLine("Не хватает молока");
      }
    }

    Console.WriteLine("\n*Отчёт*");
    Console.WriteLine($"Вода: {water} мл");
    Console.WriteLine($"Молоко: {milk} мл");
    Console.WriteLine($"Американо: {americano}");
    Console.WriteLine($"Латте: {latte}");
    Console.WriteLine($"Итого: {money} рублей");
  }

  // ===== Задание 6 =====
  static void Task6_Bacteria()
  {
    int bacteria = ReadInt("Введите количество бактерий: ", 1);
    int drops = ReadInt("Введите количество антибиотика: ", 1);

    int hour = 1;
    int kill = 10 * drops;

    while (bacteria > 0 && kill > 0)
    {
      bacteria *= 2;
      bacteria -= kill;

      if (bacteria < 0) bacteria = 0;

      Console.WriteLine($"После {hour} часа бактерий осталось {bacteria}");
      hour++;
      kill -= drops;
    }
  }

  // ===== Задание 7 =====
  static void Task7_Mars()
  {
    int n = ReadInt("Введите n: ", 1);
    int a = ReadInt("Введите a: ", 1);
    int b = ReadInt("Введите b: ", 1);
    int w = ReadInt("Введите w: ", 1);
    int h = ReadInt("Введите h: ", 1);

    int d = 0;

    while (true)
    {
      int width = a + 2 * (d + 1);
      int height = b + 2 * (d + 1);

      int cols = w / width;
      int rows = h / height;

      if (cols * rows >= n)
        d++;
      else
        break;
    }

    Console.WriteLine($"Ответ d = {d}");
  }
}
