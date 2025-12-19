using System;

class MatrixCalculator
{
  static void Main()
  {
    Console.WriteLine("Калькулятор матриц");
    int n = ReadInt("Введите количество строк (n): ");
    int m = ReadInt("Введите количество столбцов (m): ");

    double[,] matrix1 = new double[n, m];
    double[,] matrix2 = new double[n, m];

    int choice = ReadInt("Выберите способ заполнения матриц: 1 - с клавиатуры, 2 - случайные числа: ");

    if (choice == 1)
    {
      Console.WriteLine("Введите элементы первой матрицы:");
      FillMatrixFromInput(matrix1);
      Console.WriteLine("Введите элементы второй матрицы:");
      FillMatrixFromInput(matrix2);
    }
    else if (choice == 2)
    {
      int a = ReadInt("Введите начало диапазона случайных чисел (a): ");
      int b = ReadInt("Введите конец диапазона случайных чисел (b): ");
      if (b < a) { Console.WriteLine("Некорректный диапазон! Меняем местами."); int temp = a; a = b; b = temp; }

      Random rnd = new Random();
      FillMatrixRandom(matrix1, rnd, a, b);
      FillMatrixRandom(matrix2, rnd, a, b);

      Console.WriteLine("Матрица 1:");
      PrintMatrix(matrix1);
      Console.WriteLine("Матрица 2:");
      PrintMatrix(matrix2);
    }
    else
    {
      Console.WriteLine("Неверный выбор. Завершение программы.");
      return;
    }

    // Сложение матриц
    Console.WriteLine("\nСложение матриц:");
    if (matrix1.GetLength(0) == matrix2.GetLength(0) && matrix1.GetLength(1) == matrix2.GetLength(1))
    {
      PrintMatrix(AddMatrices(matrix1, matrix2));
    }
    else Console.WriteLine("Невозможно сложить матрицы — разные размеры!");

    // Умножение матриц
    Console.WriteLine("\nУмножение матриц:");
    if (matrix1.GetLength(1) == matrix2.GetLength(0))
      PrintMatrix(MultiplyMatrices(matrix1, matrix2));
    else Console.WriteLine("Невозможно умножить матрицы — несоответствие размеров!");

    // Транспонирование
    Console.WriteLine("\nТранспонированная матрица 1:");
    PrintMatrix(TransposeMatrix(matrix1));

    Console.WriteLine("\nТранспонированная матрица 2:");
    PrintMatrix(TransposeMatrix(matrix2));

    // Детерминант и обратная матрица
    if (n == m)
    {
      try
      {
        double det1 = Determinant(matrix1);
        double det2 = Determinant(matrix2);

        Console.WriteLine($"\nДетерминант матрицы 1: {det1}");
        Console.WriteLine($"Детерминант матрицы 2: {det2}");

        Console.WriteLine("\nОбратная матрица 1:");
        if (det1 != 0) PrintMatrix(InverseMatrix(matrix1));
        else Console.WriteLine("Обратная матрица не существует (детерминант = 0)");

        Console.WriteLine("\nОбратная матрица 2:");
        if (det2 != 0) PrintMatrix(InverseMatrix(matrix2));
        else Console.WriteLine("Обратная матрица не существует (детерминант = 0)");
      }
      catch (Exception ex)
      {
        Console.WriteLine("Ошибка при вычислении детерминанта или обратной матрицы: " + ex.Message);
      }
    }
    else
    {
      Console.WriteLine("\nНевозможно вычислить детерминант и обратную матрицу — не квадратные матрицы!");
    }

    // Решение системы уравнений
    if (n == m)
    {
      try
      {
        double[,] solution = MultiplyMatrices(InverseMatrix(matrix1), matrix2);
        Console.WriteLine("\nРешение системы уравнений (matrix1 * X = matrix2):");
        PrintMatrix(solution);
      }
      catch
      {
        Console.WriteLine("Система уравнений не имеет решения или неоднозначна.");
      }
    }
  }

  // Безопасное чтение int
  static int ReadInt(string prompt)
  {
    int result;
    while (true)
    {
      Console.Write(prompt);
      if (int.TryParse(Console.ReadLine(), out result)) return result;
      Console.WriteLine("Ошибка! Введите целое число.");
    }
  }

  // Ввод матрицы
  static void FillMatrixFromInput(double[,] matrix)
  {
    for (int i = 0; i < matrix.GetLength(0); i++)
      for (int j = 0; j < matrix.GetLength(1); j++)
      {
        while (true)
        {
          Console.Write($"[{i},{j}]: ");
          if (double.TryParse(Console.ReadLine(), out matrix[i, j])) break;
          Console.WriteLine("Ошибка! Введите число.");
        }
      }
  }

  static void FillMatrixRandom(double[,] matrix, Random rnd, int a, int b)
  {
    for (int i = 0; i < matrix.GetLength(0); i++)
      for (int j = 0; j < matrix.GetLength(1); j++)
        matrix[i, j] = rnd.Next(a, b + 1);
  }

  static void PrintMatrix(double[,] matrix)
  {
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
      for (int j = 0; j < matrix.GetLength(1); j++)
        Console.Write(matrix[i, j] + "\t");
      Console.WriteLine();
    }
  }

  static double[,] AddMatrices(double[,] a, double[,] b)
  {
    int rows = a.GetLength(0), cols = a.GetLength(1);
    double[,] res = new double[rows, cols];
    for (int i = 0; i < rows; i++)
      for (int j = 0; j < cols; j++)
        res[i, j] = a[i, j] + b[i, j];
    return res;
  }

  static double[,] MultiplyMatrices(double[,] a, double[,] b)
  {
    int rows = a.GetLength(0), cols = b.GetLength(1), inner = a.GetLength(1);
    double[,] res = new double[rows, cols];
    for (int i = 0; i < rows; i++)
      for (int j = 0; j < cols; j++)
      {
        double sum = 0;
        for (int k = 0; k < inner; k++) sum += a[i, k] * b[k, j];
        res[i, j] = sum;
      }
    return res;
  }

  static double[,] TransposeMatrix(double[,] matrix)
  {
    int rows = matrix.GetLength(0), cols = matrix.GetLength(1);
    double[,] res = new double[cols, rows];
    for (int i = 0; i < rows; i++)
      for (int j = 0; j < cols; j++)
        res[j, i] = matrix[i, j];
    return res;
  }

  static double Determinant(double[,] matrix)
  {
    int n = matrix.GetLength(0);
    if (n == 1) return matrix[0, 0];
    if (n == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

    double det = 0;
    for (int p = 0; p < n; p++)
    {
      double[,] sub = new double[n - 1, n - 1];
      for (int i = 1; i < n; i++)
      {
        int colIndex = 0;
        for (int j = 0; j < n; j++)
        {
          if (j == p) continue;
          sub[i - 1, colIndex++] = matrix[i, j];
        }
      }
      det += matrix[0, p] * Determinant(sub) * ((p % 2 == 0) ? 1 : -1);
    }
    return det;
  }

  static double[,] InverseMatrix(double[,] matrix)
  {
    int n = matrix.GetLength(0);
    double[,] res = new double[n, n];
    double[,] copy = (double[,])matrix.Clone();

    for (int i = 0; i < n; i++) res[i, i] = 1;

    for (int i = 0; i < n; i++)
    {
      double diag = copy[i, i];
      if (diag == 0) throw new Exception("Детерминант равен 0, обратной матрицы не существует");

      for (int j = 0; j < n; j++)
      {
        copy[i, j] /= diag;
        res[i, j] /= diag;
      }

      for (int k = 0; k < n; k++)
      {
        if (k == i) continue;
        double factor = copy[k, i];
        for (int j = 0; j < n; j++)
        {
          copy[k, j] -= factor * copy[i, j];
          res[k, j] -= factor * res[i, j];
        }
      }
    }
    return res;
  }
}
