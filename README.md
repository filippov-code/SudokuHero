# SudokuHero - Решатель судоку 
Консольное приложение для решения судоку  
Программа умеет решать 3 типа судоку:
+ Классическое
+ Чет-Нечет
+ Судоку-X
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/1.png)

## Инструкция
Любое судоку состоит из блоков. При запуске программы нужно указать размер одного такого блока - ширину и высоту через пробел.  
Например нужно решить такое классическое судоку:  
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/problem.png)   
Тогда мы пишем в программу 3 3 (ширина блока = 3, высота блока = 3)  
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/2.png)  
Далее нужно ввести всё судоку. Ввод осуществялется построчно, значения разделяются пробелом.  
Обозначения:
+ a - (any) Любое число
+ e - (even) Четное число
+ o - (odd) Нечетное число
+ 4 - Конкретное число  
Для нашего примера:  
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/3.png)  
Далее программа просит указать опции. Эти опции нужны для решения судоку X(подробнее ниже). Если опций нет, то оставляем строку пустой.    
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/4.png)  
После этого программа начинает решать судоку  
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/5.png)
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/6.png)
## Судоку Чет-Нечет
В этом варианте судоку дается информация о четности или нечетности чисел в ячейках.  
Пользуясь обозначениями описанными выше, вводим судоку обозначая места, в которых должны быть четные/нечетные/любые числа. В данном примере ячейки с кружками означают места для нечетных чисел.  
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/evenodd1.png)
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/evenodd2.png)

## Судоку X
Основная разница при судоку-Х заключается в том, что Вы не можете повторять числа по обеим основным диагоналям решетки.  
Для решения такого судоку нужно указать в опциях букву X. Так же доступны дополнительные опции X0 и X1 для запрета повторений чисел на главной и побочной диагоналях соответственно  
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/x1.png)
![](https://github.com/filippov-code/SudokuHero/blob/master/screenshots/x2.png)  
Опции для решения Судоку-X и обозначения для Судоку Чет-Нечет можно комбинировать и решать головоломку с условиями обоих вариантов.


