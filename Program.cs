
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

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
//string values = "2 3 4 1 o 4 3 2 a 2 1 3 a 1 2 a";
//string values = "a a a a a a a a a a a a a a a a";
//2x2 with deadlock
//string values = "a a 4 1 a a 3 2 4 2 1 3 3 1 2 4";
//3x3 with deadlock
//string values = "a a 8 a a 9 a 5 a a 4 a a a 1 8 a a a a a a a a 3 a 4 a a 2 7 a a a 1 a a a 4 3 a a a a a a 5 a 6 a a a 3 7 a a a a a a a a a 6 a a 1 a a a 8 a a a 5 a a 2 a 7 a";
//test
string values = "a a a a a a a a a a 1 a a 3 6 a a a a a a 8 a a a 1 2 a a 1 6 a a 9 7 8 a a a a 7 a a a a a a 5 a a a a 2 4 a a a a a 7 a 4 a a 6 9 a 5 a 1 a a 3 a a a a a a a a";

//medium
//string values = "a 8 a 2 5 a a 9 a a 5 a 6 1 3 8 7 2 a a a 9 a 4 a 1 a 5 a 7 a a a a 6 a 9 a a a a a 2 a 1 a a 4 a a a a a a 1 a a 3 7 a 9 a a a a 8 a a a 3 4 a 6 7 a a a a a a a";

//hard <- target
//string values = "a 4 a a a a 3 a a a a a 3 2 a a a a 8 6 a 1 a a 4 a a a a 8 a a a a a a a a 2 a 5 8 9 7 a a a a a a a a 3 a a a a a a a a a 3 a 2 a 4 a a 1 9 a a a 9 8 1 5 6 a a";

//from lection
//string values = "o o o e o e 6 e o e e e o o e o 9 o o o e 5 e o e e 7 o e o e 3 e o o 8 e o e 1 5 o e e o e o o e e o 5 2 9 4 e 5 o 2 o o o e o e 2 e o o o o e o 3 o o e 6 e o e";
//string values = "7 a 5 1 a a 9 a 3 a o a a a a a o a a a 6 a 8 a 5 a a a a a 2 o 5 a a 7 a a 7 a 9 a 8 a a 6 a a 8 o 1 a a a a a 9 a 2 a 3 a a a o a a a a a o a 3 a 1 a a 4 6 a 2";

//string values = "9 a a 1 a a a a a a 7 6 a a a a 4 8 a a a a 4 a 6 a 9 4 6 a a 3 9 8 1 2 7 3 1 a 2 4 5 a 6 a a a a 1 5 a 7 a a 1 9 4 a a 2 a a 6 5 a 2 7 a a a a a a a a 6 a a 8 a";
//3x3 all any
//string values = "a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a a";
//2x2 all any
//string values = "a a a a a a a a a a a a a a a a";
//3x3 without deadlock
//string values = "2 1 8 a 3 6 a a 5 a a 9 a a 1 3 a 2 a 7 a 4 a 2 1 6 a a 5 a 2 9 a 6 a 4 a a a 6 a a a 2 a a 3 a 7 a 5 9 1 8 3 a 1 a 5 a a 7 6 a 4 a a 2 a a 5 1 a 8 5 1 a 4 a a 3";
//string[] values = "a 2 a 3 6 a a a 8 8 7 4 a 2 a 6 a a a 3 a a 8 4 a a 2 2 a a 8 a a 4 1 3 4 1 a 7 a 2 a a 6 a a 6 a 3 a a 3 a a 4 1 a 7 3 a 8 a 7 a 2 5 a a a 6 a a a a 2 a 9 1 5 a";
//string[] values = string.Join(" ", valuesList).Split(" ");
Sudoku sudoku = new Sudoku(blockWidth, blockHeight, values);
sudoku.Show();
Stopwatch watch = new Stopwatch();
watch.Start();

while(sudoku.MethodRemovingValuesFromVariants())
{

}
sudoku.Show();
sudoku.MethodRemovingVariantsFromVariants();
sudoku.Show();
sudoku.MethodRemovingValuesFromVariants();
sudoku.Show();
Console.WriteLine(Sudoku.matrixs);

watch.Stop();
Console.WriteLine("Решаем...");
//Console.WriteLine(sudoku.GetWorstSolutionTime());
//long sum = 0;
//int count = 100000;
//for (int i = 0; i < count; i++)
//{
//    Sudoku sudoku = new Sudoku(blockWidth, blockHeight, values);
//    Stopwatch watch = new Stopwatch();
//    watch.Start();
//    sudoku.StartSolving();
//    watch.Stop();
//    sum += watch.ElapsedMilliseconds;
//}
//Console.WriteLine("Среднее: " + sum / (float)count);
if (Sudoku.Solve != null)
{
    //Console.WriteLine($"Решено за {Math.Round(watch.ElapsedMilliseconds/1000f, 4)} секунды");
    Sudoku.Solve.Show();
}
else
{
    Console.WriteLine(":(");
}

//for (int i = 0; i < 100; i++)
//{
//    //Console.WriteLine("step" + i);
//    //bool a = sudoku.ExcecuteStep();
//    //Console.WriteLine("result: " + a);
//    if (!sudoku.ExcecuteStep())
//    {
//        if (sudoku.IsSolved())
//        {
//            Console.WriteLine($"Решено за {i} шагов");
//        }
//        else Console.WriteLine($"deadlock {i + 1} step");
//        break;
//    }
//}


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
        if (!IsSolvable()) return false;

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
            else if (!IsSolvable())
            {
                //Console.WriteLine("Не решаемо");
                return false;
            }
            else
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (Matrix[i][j].Count > 1)
                        {

                        //Console.WriteLine("Клоны");
                        for (int k = 0; k < Matrix[i][j].Count; k++)
                            {
                                Sudoku sudokuClone = new Sudoku(BlocksWidth, BlocksHeight, GetValuesString());
                                sudokuClone.Matrix[i][j] = new List<int> { Matrix[i][j][k] };
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
                    //Console.WriteLine("Конец клонов");
                    }
                }
            }

        return false;
    }

    public bool ExcecuteStep()
    {
        bool matrixHasBeenChanged = false;

        //matrixHasBeenChanged |= MethodRemovingValuesFromVariants();
        //matrixHasBeenChanged |= MethodRemovingVariantsFromVariants();

        return matrixHasBeenChanged;
    }

    public bool MethodRemovingValuesFromVariants()
    {
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

        if (!IsSolvable()) return false;
        return matrixHasBeenChanged;
    }

    public bool MethodRemovingVariantsFromVariants()
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
                //Console.Write($"Строка {i} Элемент {j}:({string.Join(",", Matrix)}) varsInRow: ({string.Join(",", variantsInRow)}) ");
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
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                //Console.WriteLine("j:" + j);
                if (Matrix[j][i].Count == 1)
                {
                    continue;
                }

                var variantsInCol = GetVariantsFromColumn(j, i);
                var result = Matrix[j][i].Except(variantsInCol);
                if (result.Count() == 1)
                {
                    Matrix[j][i] = result.ToList();
                    j = -1;

                    matrixHasBeenChanged = true;
                    continue;
                }

            }
        }

        return matrixHasBeenChanged;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Matrix[i][j].Count == 1)
                {
                    continue;
                }

                int rowMinIndex = i / BlocksWidth;
                int rowMaxIndex = rowMinIndex + BlocksWidth - 1;
                int colMinIndex = j / BlocksHeight;
                int colMaxIndex = colMinIndex + BlocksHeight - 1;

                var variantsInRow = GetVariantsFromRow(i, j);
                var result = Matrix[i][j].Except(variantsInRow);
                if(result.Count() == 1)
                {
                    Matrix[i][j] = result.ToList();
                    RemoveValuesFromVariantsInColumn(j);
                    RemoveValuesFromVariantsInBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);
                    matrixHasBeenChanged = true;
                    continue;
                }

                continue;

                var variantsInCol = GetVariantsFromColumn(j, i);
                result = Matrix[i][j].Except(variantsInCol);
                if (result.Count() == 1)
                {
                    Matrix[i][j] = result.ToList();
                    RemoveValuesFromVariantsInRow(i);
                    RemoveValuesFromVariantsInBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);
                    matrixHasBeenChanged = true;
                    continue;
                }

                var variantsInBlock = GetVariantsFromBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex);
                result = Matrix[i][j].Except(variantsInBlock);
                if (result.Count() == 1)
                {
                    Matrix[i][j] = result.ToList();
                    RemoveValuesFromVariantsInRow(i);
                    RemoveValuesFromVariantsInColumn(j);
                    matrixHasBeenChanged = true;
                    continue;
                }
            }
        }

        //if (!IsSolvable()) return false;
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
            if (elementHasBeenChanged) Console.WriteLine($"({string.Join(",", vars)})->({string.Join(",", Matrix[rowIndex][i])})[{string.Join(",", correctValues)}]");
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
            if (elementHasBeenChanged) Console.WriteLine($"({string.Join(",", vars)})->({string.Join(",", Matrix[i][colIndex])})[{string.Join(",", correctValues)}]");
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
                if (elementHasBeenChanged) Console.WriteLine($"({string.Join(",", vars)})->({string.Join(",", Matrix[i][j])})[{string.Join(",", correctValues)}]");
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
    
    private bool SelectValuesFromVariants(int rowMinIndex, int rowMaxIndex, int colMinIndex, int colMaxIndex)
    {
        //Console.WriteLine("SVFV");
        bool blockHasBeenChanged = false;
        for (int i = rowMinIndex; i <= rowMaxIndex; i++)
        {
            for (int j = colMinIndex; j <= colMaxIndex; j++)
            {
                if (Matrix[i][j].Count == 1)
                {
                    //Console.WriteLine("Пропуск");
                    continue;
                }
                List<int> elementVariants = new List<int>(Matrix[i][j]);
                //if (!IsSolvable()) throw new Exception();
                //Console.WriteLine($"Элемент {i},{j}: ({string.Join(",", Matrix[i][j])})");

                List<int> rowVariants = new List<int>();
                for (int k = 0; k < Size; k++)
                {
                    if ( k != j) rowVariants.AddRange(Matrix[i][k]);
                }
                //Console.WriteLine($"RowVariants: ({string.Join(",",rowVariants)})");
                //////rowVariants.AddRange(GetCorrectVariantsFromRow(i));
                //////rowVariants.AddRange(GetCorrectVariantsFromColumn(j));
                //////colVariants.AddRange(GetCorrectVariantsFromBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex));
                var result = elementVariants.Except(rowVariants).ToList();
                //Console.WriteLine($"Except: ({string.Join(",", result)})");
                if (result.Count == 1)
                {
                    //Console.WriteLine($" Замена: (" + string.Join(",", elementVariants) + ")->(" + string.Join(",", result) + ")");
                    Matrix[i][j] = new List<int> { result[0] };
                    RemoveValuesFromVariantsInRow(i);
                    //RemoveWrongVariantsFromBlock();
                    //if (!IsSolvable()) throw new Exception();
                    blockHasBeenChanged = true;
                    continue;
                }
                //else Console.WriteLine("Без замены: (" + string.Join(",", elementVariants) + ")");
                //if (!IsSolvable()) throw new Exception();
                //Console.WriteLine($"ColVariants: ({string.Join(",", colVariants)})");
                //////colVariants.AddRange(GetCorrectVariantsFromRow(i));
                //////colVariants.AddRange(GetCorrectVariantsFromColumn(j));
                //////colVariants.AddRange(GetCorrectVariantsFromBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex));
                List<int> colVariants = new List<int>();
                for (int k = 0; k < Size; k++)
                {
                    if (k != i) colVariants.AddRange(Matrix[k][j]);
                }
                result = elementVariants.Except(colVariants).ToList();
                //Console.WriteLine($"Except: ({string.Join(",", result)})");
                if (result.Count == 1)
                {
                    //Console.WriteLine($" Замена: (" + string.Join(",", elementVariants) + ")->(" + string.Join(",", result) + ")");
                    Matrix[i][j] = new List<int> { result[0] };
                    //if (!IsSolvable()) throw new Exception();
                    blockHasBeenChanged = true;
                    continue;
                }
                //else Console.WriteLine("Без замены: (" + string.Join(",", elementVariants) + ")");
                //if (!IsSolvable()) throw new Exception();


                List<int> blockVariants = new List<int>();
                for (int ik = rowMinIndex; ik <= rowMaxIndex; ik++)
                {
                    for (int jk = colMinIndex; jk <= colMaxIndex; jk++)
                    {
                        if ( ik != i && jk != j) blockVariants.AddRange(Matrix[ik][jk]);
                    }
                }
                Console.WriteLine($"BlockVariants: ({string.Join(",", blockVariants)})");
                //////blockVariants.AddRange(GetCorrectVariantsFromRow(i));
                //////blockVariants.AddRange(GetCorrectVariantsFromColumn(j));
                //////blockVariants.AddRange(GetCorrectVariantsFromBlock(rowMinIndex, rowMaxIndex, colMinIndex, colMaxIndex));
                result = elementVariants.Except(blockVariants).ToList();
                Console.WriteLine($"Except: ({string.Join(",", result)})");
                if (result.Count == 1)
                {
                    Console.WriteLine($" Замена: (" + string.Join(",", elementVariants) + ")->(" + string.Join(",",result) + ")" );
                    Matrix[i][j] = new List<int> { result[0] };
                    //if (!IsSolvable()) throw new Exception();
                    blockHasBeenChanged = true;
                    continue;
                }
                else Console.WriteLine("Без замены: (" + string.Join(",", elementVariants) + ")");
                //if (!IsSolvable()) throw new Exception();
            }
        }
        //if (blockHasBeenChanged) Console.WriteLine("Сработала вроде");
        return blockHasBeenChanged;
    }

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

    public bool IsSolvable()
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
}
