
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

Sudoku.SayHello();
Console.WriteLine("Введите размер ширины и высоты блока:");
int[] inputArray = Console.ReadLine()
    .Split(" ")
    .Select(x => int.Parse(x))
    .ToArray();
int blockWidth = inputArray[0];
int blockHeight = inputArray[1];
int size = blockWidth * blockHeight;

List<string> valuesList = new();
Console.WriteLine($"Введите значения построчно через пробел (по {size} значений):");
int stillBe = size*size;
for(int i = 0; i < size; i++)
{
    Console.WriteLine($"Осталось еще {stillBe} значений ({stillBe/size} строк)");
    valuesList.Add(Console.ReadLine());
    stillBe -= size;
}
string values = string.Join(" ", valuesList);
//pattern
//string values = "a 7 a a 3 a a a 8 a a a 4 a a a 6 a a a 4 a 9 a 5 a 3 a 5 2 a 6 a a a a a a a a a a a a 9 4 a a a a 8 a a 2 a 9 a 2 5 a 7 a a a a 3 9 4 a a a a a a 6 a a a 9 a a";


Sudoku sudoku = new Sudoku(blockWidth, blockHeight, values);
Stopwatch watch = new Stopwatch();
watch.Start();

Console.WriteLine("Решаем...");
sudoku.StartSolving();

watch.Stop();

if (Sudoku.Solve != null)
{
    Sudoku.Solve.Show();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Решено за {Math.Round(watch.ElapsedMilliseconds/1000f, 4)} секунды");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Не получилось решить. Потрачено: {Math.Round(watch.ElapsedMilliseconds / 1000f, 4)}");
    Console.WriteLine(":(");
}
Console.ResetColor();
Console.WriteLine($"Создано {Sudoku.matrixs} матриц");


class Sudoku
{
    public readonly int Size;
    public readonly int CountBlocksInRow;
    public readonly int CountBlocksInColumn;
    public readonly int BlocksWidth;
    public readonly int BlocksHeight;
    public readonly List<int>[][] Matrix;
    public static Sudoku Solve;


    public Sudoku(int blocksWidth, int blocksHeigth, string valuesString)
    {
        Size = blocksHeigth * blocksWidth;
        CountBlocksInColumn = blocksWidth;
        CountBlocksInRow = blocksHeigth;
        BlocksWidth = blocksWidth;
        BlocksHeight = blocksHeigth;

        //инициализация и заполнение матрицы
        string[] values = valuesString.Split(" ");
        Matrix = new List<int>[Size][];
        for (int i = 0; i < Size; i++)
        {
            Matrix[i] = new List<int>[Size];
            for(int j = 0; j < Size; j++)
            {
                Matrix[i][j] = GetVariantsFromString(values[i * Size + j]);
            }
        }
        PlusMatrixsCounter();
    }

    private List<int> GetVariantsFromString(string parameter)
    {
        string[] values = parameter.Split(',');
        List<int> variants = new List<int>();
        for (int i = 0; i < values.Length; i++)
        {
            if (int.TryParse(values[i], out int num))
            {
                variants.Add(num);
            }
            else
            {
                switch (values[i])
                {
                    case "a":
                        variants.AddRange(Enumerable.Range(1, Size));
                        break;
                    case "e":
                        variants.AddRange(Enumerable.Range(1, Size).Where(x => x % 2 == 0));
                        break;
                    case "o":
                        variants.AddRange(Enumerable.Range(1, Size).Where(x => x % 2 != 0));
                        break;
                }
            }
        }
        return variants.Distinct().ToList();
    }

    public static BigInteger matrixs = 0;
    private void PlusMatrixsCounter()
    {
        matrixs++;
        if (matrixs % 10000 == 0)
        {
            Console.WriteLine($"Матриц: {matrixs}");
        }
    }

    public bool StartSolving()
    {
        if (IsSolved())
        {
            Solve ??= this;
            return true;
        }
        if (!IsNoVoids()) return false;

        while (ExcecuteStep())
        {
        }

        //deadlock
        if (IsSolved())
        {
            //Console.WriteLine("Решено");
            Solve ??= this;
            return true;
        }
        else if (!IsNoVoids())
        {
            //Console.WriteLine("Испорчено");
            return false;
        }
        else if (IsFilled())
        {
            //Console.WriteLine("Решено не верно");
            return false;
        }
        else
        {
            int smallestRowIndex = Size;
            int smallestColIndex = Size;
            int smallestLength = Size + 1;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int length = Matrix[i][j].Count;
                    if (length > 1 && length < smallestLength)
                    {
                        //if (length == 0) throw new Exception();
                        smallestRowIndex = i;
                        smallestColIndex = j;
                        smallestLength = length;
                    }
                }
            }
            for (int k = 0; k < Matrix[smallestRowIndex][smallestColIndex].Count; k++)
            {
                Sudoku sudokuClone = new Sudoku(BlocksWidth, BlocksHeight, GetValuesString());
                sudokuClone.Matrix[smallestRowIndex][smallestColIndex] = new List<int> { Matrix[smallestRowIndex][smallestColIndex][k] };
                //sudokuClone.Show();
                bool solved = sudokuClone.StartSolving();
                if (solved)
                {
                    Solve ??= sudokuClone;
                    //sudokuClone.Show();
                    //Console.ReadLine();
                    return true;
                }
            }
    }

        return false;
    }

    public bool ExcecuteStep()
    {
        bool matrixHasBeenChanged = false;

        matrixHasBeenChanged |= MethodRemovingValuesFromVariants();
        if (matrixHasBeenChanged)
            return true;

        //matrixHasBeenChanged |= MethodRemovingVariantsFromVariants();

        return matrixHasBeenChanged;
    }

    public bool MethodRemovingValuesFromVariants()
    {
        //Show();
        bool matrixHasBeenChanged = false;
        for (int i = 0; i < Size; i++)
        {
            //bool rowHasBeenEdit = RemoveWrongVariantsFromRow(i);
            //Console.WriteLine("rowHasBeenEdit: " + rowHasBeenEdit);
            //matrixHasBeenEdit |= rowHasBeenEdit;
            //Console.WriteLine("return:" + matrixHasBeenEdit);

            //if (!IsSolvable()) throw new Exception();
            matrixHasBeenChanged |= RemoveValuesFromVariantsInRow(i);
            //if (!IsSolvable()) throw new Exception();
            matrixHasBeenChanged |= RemoveValuesFromVariantsInColumn(i);
            //if (!IsSolvable()) throw new Exception();
        }
        for (int i = 0; i < CountBlocksInRow; i++)
        {
            for (int j = 0; j < CountBlocksInColumn; j++)
            {
                int rowMinIndex = i * BlocksWidth;
                int rowMaxIndex = rowMinIndex + BlocksWidth - 1;
                int colMinIndex = j * BlocksHeight;
                int colMaxIndex = colMinIndex + BlocksHeight - 1;

                //Console.WriteLine($"Блок {i},{j}");
                matrixHasBeenChanged |= RemoveValuesFromVariantsInBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);
                //Show();
                //if (!IsSolvable()) throw new Exception();
                //matrixHasBeenEdit |= SelectValuesFromVariants(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);
                //if (!IsSolvable()) throw new Exception();
            }
        }
        //Show();

        if (!IsNoVoids())
        {
            //Show();
            //throw new Exception();
            return false;
        }
        return matrixHasBeenChanged;
    }


    public bool MethodRemovingVariantsFromVariants()//TODO: Метод оптимизации который позволит создавать меньше матриц при решении, сейчас не применяется, не тестировался
    {
        bool matrixHasBeenChanged = false;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                //Console.WriteLine("j:" + j);
                if (Matrix[i][j].Count == 1)
                {
                    continue;
                }

                var variantsInRow = GetVariantsFromRow(i, j);
                //Console.Write($"Строка {i} Элемент {j}:({string.Join(",", Matrix[i][j])}) varsInRow: ({string.Join(",", variantsInRow)}) ");
                var result = Matrix[i][j].Except(variantsInRow);
                //Console
                if (result.Count() == 1)
                {
                    Matrix[i][j] = result.ToList();
                    //RemoveValuesFromVariantsInColumn(j);
                    //RemoveValuesFromVariantsInBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);
                    j = -1;
                    //Console.WriteLine("Сброс j = 0");
                    matrixHasBeenChanged = true;
                    continue;
                }

                
            }
        }

        if (matrixHasBeenChanged)
            MethodRemovingValuesFromVariants();

        for (int j = 0; j < Size; j++)
        {
            for (int i = 0; i < Size; i++)
            {
                //Console.Write($"{i},{j}: ({string.Join(",", Matrix[i][j])}) ");
                if (Matrix[i][j].Count == 1)
                {
                    //Console.WriteLine("Пропуск");
                    continue;
                }

                var variantsInCol = GetVariantsFromColumn(j, i);
                //Console.Write($"[{string.Join(",", variantsInCol)}]");
                //Console.Write($"Строка {i} Элемент {j}:({string.Join(",", Matrix)}) varsInRow: ({string.Join(",", variantsInRow)}) ");
                //ShowColumn(j);
                var result = Matrix[i][j].Except(variantsInCol);
                //Console.Write($"->({string.Join(",", result)})");
                //Console
                if (result.Count() == 1)
                {
                    Matrix[i][j] = result.ToList();
                    //Console.WriteLine($"= ({string.Join(",", Matrix[i][j])})");
                    i = -1;
                    matrixHasBeenChanged = true;
                    continue;
                }
                //Console.WriteLine($"= ({string.Join(",", Matrix[i][j])})");

            }
        }

        if (matrixHasBeenChanged)
            MethodRemovingValuesFromVariants();

        return matrixHasBeenChanged;
    }

    #region RemoveValuesFromVarinatsInRow
    private bool RemoveValuesFromVariantsInRow(int rowIndex)
    {
        //Console.Write($"Cтрока {rowIndex}:");
        //ShowRow(rowIndex);

        bool rowHasBeenEdit = false;

        for (int i = 0; i < Size; i++)
        {
            if (Matrix[rowIndex][i].Count == 1)
            {
                //Console.WriteLine($"({Matrix[rowIndex][i][0]})");
                continue;
            }

            //Console.Write($"({string.Join(",", Matrix[rowIndex][i])})");
            //Console.Write("->");
            var correctValues = GetValuesFromRow(rowIndex);
            var vars = new List<int>(Matrix[rowIndex][i]);
            int variantsBeforeEdit = Matrix[rowIndex][i].Count;
            Matrix[rowIndex][i] = Matrix[rowIndex][i].Except(correctValues).ToList();
            //if (!IsSolvable()) throw new Exception();
            bool elementHasBeenChanged = variantsBeforeEdit != Matrix[rowIndex][i].Count;
            rowHasBeenEdit |= elementHasBeenChanged;
            //if (elementHasBeenChanged) Console.WriteLine($"({string.Join(",", vars)})->({string.Join(",", Matrix[rowIndex][i])})[{string.Join(",", correctValues)}]");
            //Console.Write($"({string.Join(",", Matrix[rowIndex][i])})[{string.Join(",", correctValues)}]");
            //Console.WriteLine();
        }
        //ShowRow(rowIndex);
        //Console.WriteLine();
        return rowHasBeenEdit;
    }

    private List<int> GetValuesFromRow(int rowIndex)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < Size; i++)
        {
            if (Matrix[rowIndex][i].Count == 1)
            {
                result.Add(Matrix[rowIndex][i][0]);
            }
        }
        return result;
    }

    private List<int> GetVariantsFromRow(int rowIndex, int exceptColIndex)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < Size; i++)
        {
            if (i != exceptColIndex && Matrix[rowIndex][i].Count > 1)
            {
                result.AddRange(Matrix[rowIndex][i]);
            }
        }
        return result;
    }

    private void ShowRow(int rowIndex)
    {
        for (int i = 0; i < Size; i++)
        {
            Console.Write($"({string.Join(",", Matrix[rowIndex][i])})");
        }
        
        Console.WriteLine();
    }
    #endregion RemoveWrongVarinatsFromRow

    #region RemoveValuesFromVariantsInColumn
    private bool RemoveValuesFromVariantsInColumn(int colIndex)
    {
        //Console.Write($"Столбец {colIndex}:");
        //ShowColumn(colIndex);

        bool colHasBeenEdit = false;
        for (int i = 0; i < Size; i++)
        {
            if (Matrix[i][colIndex].Count == 1)
            {
                //Console.WriteLine($"({Matrix[i][colIndex][0]})");
                continue;
            }

            //Console.Write($"({string.Join(",", Matrix[i][colIndex])})");
            //Console.Write("->");
            var correctValues = GetValuesFromColumn(colIndex);
            var vars = new List<int>(Matrix[i][colIndex]);
            int variantsBeforeEdit = Matrix[i][colIndex].Count;
            Matrix[i][colIndex] = Matrix[i][colIndex].Except(correctValues).ToList();
            //if (!IsSolvable()) throw new Exception();
            bool elementHasBeenChanged = variantsBeforeEdit != Matrix[i][colIndex].Count;
            colHasBeenEdit |= elementHasBeenChanged;
            //Console.Write($"({string.Join(",", Matrix[i][colIndex])})[{string.Join(",", correctValues)}]");
            //Console.WriteLine();
        }
        //ShowColumn(colIndex);
        //Console.WriteLine();
        return colHasBeenEdit;
    }

    private List<int> GetValuesFromColumn(int colIndex)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < Size; i++)
        {
            if (Matrix[i][colIndex].Count == 1)
            {
                result.Add(Matrix[i][colIndex][0]);
            }
        }
        return result;
    }

    private List<int> GetVariantsFromColumn(int colIndex, int exceptRowIndex)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < Size; i++)
        {
            if (i != exceptRowIndex && Matrix[i][colIndex].Count > 1)
            {
                result.AddRange(Matrix[i][colIndex]);
            }
        }
        return result;
    }

    private void ShowColumn(int colIndex)
    {
        for (int i = 0; i < Size; i++)
        {
            Console.Write($"({string.Join(",", Matrix[i][colIndex])})");
        }

        Console.WriteLine();
    }

    #endregion

    #region RemoveValuesFromVariantsInBlock
    private bool RemoveValuesFromVariantsInBlock(int rowMinIndex, int rowMaxIndex, int colMinIndex, int colMaxIndex)
    {
        //Console.WriteLine($"Блок {rowMinIndex / BlocksWidth},{colMinIndex / BlocksHeight}");
        //ShowBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);

        bool blockHasBeenEdit = false;

        for (int i = rowMinIndex; i <= rowMaxIndex; i++)
        {
            for (int j = colMinIndex; j <= colMaxIndex; j++)
            {

                if (Matrix[i][j].Count == 1)
                {
                    //Console.WriteLine($"Пропуск ({string.Join(",", Matrix[i][j])})");
                    continue;
                }

                //Console.WriteLine($"Элемент {i % BlocksWidth},{j % BlocksHeight}: ({string.Join(",", Matrix[i][j])})");
                var correctValues = GetValuesFromBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);
                //Console.WriteLine($"[{string.Join(",", correctValues)}]");
                var vars = new List<int>(Matrix[i][j]);
                int variantsBeforeEdit = Matrix[i][j].Count;
                Matrix[i][j] = Matrix[i][j].Except(correctValues).ToList();
                bool elementHasBeenChanged = variantsBeforeEdit != Matrix[i][j].Count;
                //if (!IsSolvable()) throw new Exception();
                blockHasBeenEdit |= elementHasBeenChanged;
                //Console.WriteLine($"->({string.Join(",", Matrix[i][j])})");
            }
        }

        return blockHasBeenEdit;
    }

    private List<int> GetValuesFromBlock(int rowMinIndex, int rowMaxIndex, int colMinIndex, int colMaxIndex)
    {
        //Console.WriteLine($"GetCorrectsVariantsFromBlock: rMin:{rowMinIndex} rMax:{rowMaxIndex} cMin:{colMinIndex} cMax:{colMaxIndex}");
        //ShowBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);
        List<int> result = new List<int>();
        for (int i = rowMinIndex; i <= rowMaxIndex; i++)
        {
            for (int j = colMinIndex; j <= colMaxIndex; j++)
            {
                if (Matrix[i][j].Count == 1)
                    result.Add(Matrix[i][j][0]);
            }
        }
        return result;
    }

    private List<int> GetVariantsFromBlock(int rowMinIndex, int rowMaxIndex, int colMinIndex, int colMaxIndex)
    {
        //Console.WriteLine($"GetCorrectsVariantsFromBlock: rMin:{rowMinIndex} rMax:{rowMaxIndex} cMin:{colMinIndex} cMax:{colMaxIndex}");
        //ShowBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);
        List<int> result = new List<int>();
        for (int i = rowMinIndex; i <= rowMaxIndex; i++)
        {
            for (int j = colMinIndex; j <= colMaxIndex; j++)
            {
                if (Matrix[i][j].Count > 1)
                    result.AddRange(Matrix[i][j]);
            }
        }
        return result;
    }

    private void ShowBlock(int rowMinIndex, int rowMaxIndex, int colMinIndex, int colMaxIndex)
    {
        for (int i = rowMinIndex; i <= rowMaxIndex; i++)
        {
            for (int j = colMinIndex; j <= colMaxIndex; j++)
            {
                Console.Write($"({string.Join(",", Matrix[i][j])})");
            }
            Console.WriteLine();
        }
    }

    #endregion
  
    public bool IsSolved()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Matrix[i][j].Count > 1 || Matrix[i][j].Count == 0)
                    return false;

            }
            if (GetValuesFromRow(i).Distinct().Count() != Size || GetValuesFromColumn(i).Distinct().Count() != Size)
                return false;
        }

        for (int i = 0; i < CountBlocksInRow; i++)
        {
            for (int j = 0; j < CountBlocksInColumn; j++)
            {
                int rowMinIndex = i * BlocksWidth;
                int rowMaxIndex = rowMinIndex + BlocksWidth - 1;
                int colMinIndex = j * BlocksHeight;
                int colMaxIndex = colMinIndex + BlocksHeight - 1;
                if (GetValuesFromBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex).Distinct().Count() != Size)
                    return false;
            }
        }

        return true;
    }

    public bool IsNoVoids()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Matrix[i][j].Count == 0)
                    return false;
            }
        }
        return true;
    }

    public bool IsFilled()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Matrix[i][j].Count != 1)
                    return false;
            }
        }
        return true;
    }

    public string GetValuesString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                stringBuilder.Append(string.Join(",", Matrix[i][j]) + " ");
            }
        }
        return stringBuilder.ToString().Trim();
    }

    public void Show()
    {
        Console.WriteLine("===========================");
        Console.WriteLine($"Sudoku {Size}x{Size}, Blocks {BlocksWidth}x{BlocksHeight}");
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write($"({string.Join(",", Matrix[i][j])})");
            }
            Console.WriteLine();
        }
        Console.WriteLine("===========================");
    }

    public static void SayHello()
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;

        Console.WriteLine(@" _____ _   _______ _____ _   ___   _ _   _  ___________ _____");
        Console.WriteLine(@"/  ___| | | |  _  \  _  | | / / | | | | | ||  ___| ___ \  _  |");
        Console.WriteLine(@"\ `--.| | | | | | | | | | |/ /| | | | |_| || |__ | |_/ / | | |");
        Console.WriteLine(@" `--. \ | | | | | | | | |    \| | | |  _  ||  __||    /| | | |");
        Console.WriteLine(@"/\__/ / |_| | |/ /\ \_/ / |\  \ |_| | | | || |___| |\ \\ \_/ /");
        Console.WriteLine(@"\____/ \___/|___/  \___/\_| \_/\___/\_| |_/\____/\_| \_|\___/ ");

        Console.ResetColor();
    }
}
