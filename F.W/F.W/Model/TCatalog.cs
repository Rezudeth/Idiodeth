﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoCarsAllowed.Model
{
    public class TCatalog
    {
        string nameCatalog; // Наименование каталога
        int numCar; // Идентификатор общего количество автомобилей
        public TCar[] cars; // Массив объектов автомобилей
        string mark, bodycar, country; int dayRelease, monthRelease, yearRelease, price; // Параметры для конструктора объекта массива TCar[]
        public TCar this[int i] // Для работы с отдельными объектами массива TCar[] используем индексатор
        {
            get // Аксессор для обращения к объекту массива TCar[]
            {
                return cars[i];
            }
            set // Аксессор для создания нового объекта массива TCar[] 
            {
                cars[i] = new TCar(mark, bodycar, country, dayRelease, monthRelease, yearRelease, price);
            }
        }
        public int Number // Свойство для получения доступа к общему количеству автомобилей
        {
            get // Используем только аксессор считывания т.к. изменение количества автомобилей используется в другом свойстве 
            {
                return numCar;
            }
        }
        public string Name // Свойство для получения доступа к общему количеству автомобилей
        {
            get // Аксессор для считывания переменной содержащей имя каталога
            {
                return nameCatalog;
            }
            set // Аксессор для присваивание нового имени каталога во второй форме
            {
                nameCatalog = value;
            }
        }
        public int Length // Свойство для получения доступа к длинне массива cars
        {
            get // Аксессор для считывания длины массива
            {
                return cars.Length;
            }
            set // Аксессор для изменения длины массива
            {
                int oldnum = numCar;
                Array.Resize(ref cars, value); // Использование класса массива и метода для переформирования массива в новый массив с новой длинной от присвоенного числа
                numCar = value; // Так же присваивание числа к переменной количества автомобилей
                for (;oldnum < numCar; oldnum++)
                    cars[oldnum] = new TCar("", "", "", 1, 1, 2000, 0);
            }
        }
        // Метод для преобразования определённого объекта в новый объект (где int i - идентификатор объекта преобразования)
        public void Add(int i, string mark, string bodycar, string country, int monthRelease, int dayRelease, int yearRelease, int price)
        {
                cars[i] = new TCar(mark, bodycar, country, monthRelease, dayRelease, yearRelease, price); // Вызов конструктора TCar[]
        }
        // Свойство для удаления определённого объекта из массива cars
        public int remove
        {
            set // Аксессор присваивания идентификации объекта для удаления из массива (где value это идентификатор)
            { 
                // Использование оператора итераций for для смещения массива, перемещая требуемый объект в конец массива
                for (int i = value-1; i < cars.Length - 1; i++) // Использование идентификатора с расчётом на то что присвоенное число инденксации является натуральным
                    cars[i] = cars[i + 1]; // Смещение удаляемого объекта к концу массива
                Array.Resize(ref cars, cars.Length - 1); // Создание нового массива исключая удаляемый объект
                numCar--; // Условное исключение удалённого объекта из идентификатора количества автомобилей
            }
        }
        // Метод для создания папки save и нового файла list.tхt при их отсутвии в папке проекта
        // Метод является статичным для создания пустых файлов за пределами данного класса и без зависимости к объектам
        public static void CreateBlank(string path, int L) // string path - путь к папке где будет файл, int L - это аргумент языка программы
        {
            // Если файла сохранения уже существует, он удаляется и создаётся новый базовый файл для замены
            // Для проверки мы используем булевой метод класса File который проверяет существование указанного в аргументе пути
            // Используем класс Directory для обозначения пути, метод GetCurrentDirectory возвращает путь к файлу приложение
            // Метод GetParent с аргументом прошлого метода возвращает путь на шаг выше
            // Свойство Parent делает ещё шаг выше, а свойство FullName возвращает результат как строчное значение к данной строке добавляем расположение требуемого файла
            if (File.Exists($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt"))
            {   // Для избежания непредвиденных дополнений в файле удаляем его, если существует
                GC.Collect();   // Поток может очищаться из буфера памяти сразу, собираем все неиспользуемые в памяти потоки и начинаем очистку 
                GC.WaitForPendingFinalizers();  // Ожидаем завершение очищения ненужных потоков в памяти
                File.Delete($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt"); 
            }
            // Создаём новую папку с помощью указанной в аргументе строковой переменной содержащей путь проводника
            Directory.CreateDirectory(path); // Создание новой папки соответствующей указаному пути
            StreamWriter replace = new StreamWriter(File.OpenWrite(path + @"\list.txt")); // Создание объекта StreamWriter для создания и заполнения пустого файла list.tхt
            // В качестве аргумента для конструктора StreamWriter мы отправляем метод занимющий поток и позволяющий создать тот самый файл и редактировать его
            replace.WriteLine($"catalog_name \"\"\nnum_of_cars \"0\"\nlanguage \"{L}\""); // Использование класса TeхtWriter для внесения базовых строк в созданный файл
            replace.Close(); // Освобождение потока с помощью метода Close()
            Console.WriteLine("Blank save file was created"); // Оповещение в консоли о успешном создании файла
        }
        // Метод для нахождения определённой строки и её значения в файле вне зависимости от её положения
        // Первый аргумент это требуемое кодовое имя, второй это тип возвращаемой строки (i - int, s - string, d - date, y - year)
        // Метод является статичным для нахождения строк за пределами данного класса и без зависимости к объектам
        public static string FindValue(string Name, char Type)
        {
            StreamReader sr;
            if (File.Exists($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt")) // Проверить существование файла сохранения
                sr = new StreamReader($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt"); // Занимаем поток файлом сохранения для чтения
            else   // Если файла не существует создаём базовый файл сохранения и после занимаем поток новым файлом 
            {
                CreateBlank($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save", 0);
                sr = new StreamReader($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt");
            }
            bool contain = false; // Булевая переменная для добавления заглушки или значения по умолчанию при отсутсвии требуемой строки
            string line; // Создание пустой строковой переменной которой будет присваиваться значение потока sr
            while ((line = sr.ReadLine()) != null) // Оператор итераций while будет присваивать значение потока sr к line пока строки файла не закончатся
            {   // Если в строке содержится требуемое кодовое имя, то поиск прекращается, а полученная строка используется в дальнейшем
                if (line.Contains(Name))
                {   // Имя содержится в файле, пустое значение не будет присвоено
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == Name[0])
                        {
                            if (char.IsDigit(line[i + Name.Length]))
                            {
                                goto ContinueEnd;
                            }
                        }
                    }
                    contain = true;
                    break;
                }
            ContinueEnd:;
            }
            if (!contain)
            {   // Если же требуемой строки нет или она записана некорректно, то для объекта локального массива выдаётся заглушка
                if (Type == 'i') // Если строка является целочисленным типом, то возвращается числовая заглушка
                    return "0";
                if (Type == 's') // Если строка является строковым типом, то возвращается заглушка
                    return "";
                if (Type == 'd') // Если строка является датой (либо днём, либо месяцем), то возвращается минимальное значение
                    return "1";
                if (Type == 'y') // Если строка является годом, то возвращается год по умолчанию
                    return "2000";
            }
            sr.Close(); // Освобождение потока
            string result = ""; // Создание переменной с пустой строкой для присваивания результата между '"'
            string search = $"{line[0]}"; // Переменная для поиска первой открывающей '"' (содержит первый символ строки line, для определения длины строки и перв. симв.)
            bool resultCorrect = false; // Булевая переменная для проверки закреплённости результата
            // Отсчёт поиска начинается со второго символа строки с кодовым именем для того...
            for (int i = 1; i < line.Length; i++)
            {   // чтобы найти требуемый результат который расположен после первой '"' мы рассматриваем его как предыдущий символ строки,
                // Если предыдущим символом является '"' то поиск результата прекращается и начинается его запись для возвращения
                if (search[i - 1] == '"')
                {   // Для считывания результата мы используем ещё один оператор итераций, для отсчёта первой переменной результата мы берём длину поиска в строке
                    // Ограничением будет последний символ '"' даже если он занимает оставшуюся часть строки
                    for (int j = search.Length; j < line.Length; j++)
                    {   // Т.к. мы для идентификации использовали прошлый символ как '"', то последний символ поиска является первым символом результатом
                        // Для идентификации закрывающего символа '"', мы ищем следующее появление данного символа
                        if (line[j] == '"')
                        {   // Для того чтобы считать результат после множественных '"' мы продолжаем считывание до следующего появления
                            if (line[j - 1] == '"')
                                continue;
                            // Если данный символ является закрывающим то результат не является пустым, а цикл завершается с корректным результатом
                            resultCorrect = true;
                            break;
                        }   // Если символ не является закрывающим символом, цикл продолжает считывание результата
                        result += line[j];
                    }   // Если у результата не было закрывающего символа (или результат имеет вид "") то результатом возвращается пустое значение в зависимости от типа
                    if (!resultCorrect)
                    {
                        if (Type == 'i')
                            return "0";
                        if (Type == 's')
                            return "";
                        if (Type == 'd')
                            return "1";
                        if (Type == 'y')
                            return "2000";
                    }
                    break;
                }   // Пока открывающая '"' не будет найдена, поиск будет считывать каждый символ строки вплоть до конца строки
                search += line[i];
            }   // Для проверки целочисленного результата используем обработку исключений
            if (Type == 'i' | Type == 'd' | Type == 'y')
            {
                try
                {   // Преобразуем строку в целочисленный тип
                    int check = Int32.Parse(result);
                }
                catch
                {   // Если произошла ошибка типа, то значение записано не корректно назначеному типу
                    if (Type == 'i')
                        return "0";
                    if (Type == 'd')
                        return "1";
                    if (Type == 'y')
                        return "2000";
                }
            }
            // Полученый результат возвращается в виде строки
            return result;
        }
        // Метод для замены значения в файле вне зависимости от положения строки (иронично, но метод используется только для замены языка при работе с программой)
        // Первый аргумент это требуемое кодовое имя, второй это новое значение
        // Метод является статичным для замены строки (языка (ну да)) за пределами данного класса и без зависимости к объектам
        public static void ReplaceValue(string Name, int Value)
        {
            // Занимаем поток файлом сохранения для чтения со стандартным кодировщиком
            StreamReader sr = new StreamReader($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt");
            string line = sr.ReadLine(); // Создание строковой переменной с первой строкой которой будет присваиваться значение потока sr
            string neline = ""; // Переменная для замены старого файла сохранения, новым
            string result = ""; // Строка которая подвергается изменению
            bool contain = false; // Булевая переменная для добавления базовой строки при её отсутсвии
            bool found = false; // Булевая переменная для проверки закреплённости значения в файле
            while (line != null) // В отличие от метода чтения оператор итераций while будет проверять значение line, присваивая новые значения после блока
            {   // Если в строке содержится требуемое кодовое имя, то поиск прекращается, а полученная строка используется в дальнейшем
                if (line.Contains(Name))
                {   // В отличие от поиска результата, мы можем взять результат как всю строку
                    contain = true;
                    for (int i = 0; i < line.Length; i++)
                    {   // Если открывающий символ '"' существует, то оператор условия начинает следующий поиск 
                        result += line[i];
                        if (line[i] == '"')
                        {   // Переходим на следующий символ идущий после открывающего символа
                            i++;
                            for (; i < line.Length; i++)
                            {   // Данный поиск убирает старое значение из строки вплоть до следующего закрывающего символа
                                if (line[i] == '"')
                                {   // Если закрывающий символ существует, то строке присваивается новое значение и считывание результата прекращается
                                    found = true;
                                    result += $"{Value}" + '"';
                                    break;
                                }
                            }
                        }   // После присвоения результата, последующий текст вводится без изменения
                    }
                    if (!found)
                    {   // Если закрывающего символа не было или строка записана некорректно, то для присвоения результата создаётся базовая строка правильной формы
                        result = $"{Name} \"{Value}\"";
                    }   // После завершения строки с результатом она добавляется в строку нового файла
                    neline += result;
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        neline += '\n';
                    }
                    continue;
                }   // Если строка не найдена или была найдена и пропущена, то строка для нового файла поплняется неизменными строками старого файла
                neline += line;
                line = sr.ReadLine();   // Передаём строковой переменной line значение текущей строки файла
                if (line != null)   // чтобы не создавать лишних пустых строк каждый раз заменяя значение, используем проверку пустого потока после инструкций
                {
                    neline += '\n'; // Если поток содержит ещё одну строку, то для неё в строковой переменной замены происходит переход на следующую
                }
            }
            if (!contain)   // Если требуемой строки небыло она добавляется в конце переменной замены
            {
                neline += $"\n\n*new* {Name} \"{Value}\""; 
            }
            sr.Close(); // Освобождение потока
            GC.Collect();
            GC.WaitForPendingFinalizers(); 
            File.Delete($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt"); // Удаление старого файла
            // Создание объекта StreamWriter для создания и заполнения пустого файла list.tхt
            Directory.CreateDirectory($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save");
            StreamWriter replace = new StreamWriter(File.OpenWrite($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt"));
            replace.Write(neline); // Использование класса TeхtWriter для внесения старых данных с изменениями в созданный файл
            replace.Close(); // Освобождение потока
        }   
        // Конструктор для создания основного массива программы
        public TCatalog()
        {
            // Если файла сохранения не существует то вызывается метод CreateBlank для создания нового пути и базового файла
            if (!File.Exists($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt"))
                CreateBlank($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save", 0); // При отсутвии файла выдаётся английский язык по умолчанию
            nameCatalog = FindValue("catalog_name", 's'); // Использование метода FindValue для получения значения под "" в файле под кодовым именем catalog_name
            numCar = Int32.Parse(FindValue("num_of_cars", 'i')); // Аналогичное получение значения, но с преобразованием строки в целочисленное значение
            cars = new TCar[numCar]; // Создание локального массива для редактирования в программе  
            for (int i = 0; i < numCar; i++) // Заполнение объектов массива
            {
                // Получение значений параметров с натуральным идентификатором объектов из файла для объектов массива cars
                mark = FindValue($"mark_{i + 1}", 's');
                bodycar = FindValue($"bodycar_{i + 1}", 's');
                country = FindValue($"country_{i + 1}", 's');
                dayRelease = Int32.Parse(FindValue($"day_{i + 1}", 'd'));
                monthRelease = Int32.Parse(FindValue($"month_{i + 1}", 'd'));
                yearRelease = Int32.Parse(FindValue($"year_{i + 1}", 'y'));
                price = Int32.Parse(FindValue($"price_{i + 1}", 'i'));
                // Создание объекта с доступными параметрами в качестве аргументов
                cars[i] = new TCar(mark, bodycar, country, monthRelease, dayRelease, yearRelease, price);
            }
        }   
        // Метод для отображения массива объектов cars с помощью консоли
        public string ShowCatalog()
        {
            string sc = ($"List of the cars in {nameCatalog} \n\n");
            if (numCar == 0) // При отсутвии автомобилей возвращается оповещение об этом
                return $"There's no cars";
            for (int i = 0; i < cars.Length; i++)
            {   // Упорядочивание объектов по убыванию от срока службы
                for (int j = 0; j < cars.Length - 1; j++)
                {   // Если следующий объект больше текущего, они меняются местами, перемещая старший объект к началу массива.
                    if (cars[j + 1].ServiceTime() > cars[j].ServiceTime())
                        (cars[j], cars[j + 1]) = (cars[j + 1], cars[j]);
                }
            }   // Использование метода ShowCar() для получения данных о индивидуальных объектах массива
            for (int i = 0; i < cars.Length; i++) 
                sc += $"{cars[i].ShowCar()}\n";
            sc += $"Average service time of cars is {AvgServiceTime()}\n-->"; // Вывод среднего срока службы с помощью метода AvgServiceTime()
            sc += ShowExpensive(); // Использование метода для отображения объекта с самым высоким параметром price в консольном виде
            return sc;
        }   
        // Метод для вывода объекта целочисленным значением с самым высоким параметром price 
        public int MostExpensive()
        {   // Переменные для самого и идентификатора самого высокого показателя
            int most = 0, mosti = 0;
            for (int i = 0; i < cars.Length; i++)
            {   // Если параметр price больше наивысшего, он сам становится наивысшим
                if (cars[i].Price > most)
                {   // Присваивание новых наивысших значений
                    most = cars[i].Price;
                    mosti = i;
                }
            }
            return mosti + 1;
        }
        // Метод для вывода объекта строковым значением с самым высоким параметром price для консоли
        public string ShowExpensive()
        {
            try
            {   // Переменные для самого и идентификатора самого высокого показателя
                int most = 0, mosti = 0;
                for (int i = 0; i < cars.Length; i++)
                {   // Если параметр price больше наивысшего, он сам становится наивысшим
                    if (cars[i].Price > most)
                    {   // Присваивание новых наивысших значений
                        most = cars[i].Price;
                        mosti = i;
                    }
                }   // Внедрение объекта с наивысшим значением price в строковую переменную
                string se = $"The most expensive is \n\n{cars[mosti].ShowCar()}";
                return se;
            }
            catch (Exception ex)
            {   // Обработчик исключений для пустого файла
                string se = (ex.Message);
                return se;
            }
        }
        // Метод для вывода среднего значения срока службы всех объектов массива cars
        public int AvgServiceTime()
        {
            try
            {   // Переменная суммы сроков службы всех объектов
                int sum = 0;
                for (int i = 0; i < cars.Length; i++)
                {   // Складывание сроков службы каждого объекта
                    sum += cars[i].ServiceTime();
                }   // Возвращение среднего результата суммы всех объектов
                return (sum / cars.Length);
            }
            catch
            {   // Обработчик исключений для пустого файла
                return 0;
            }
        }
    }
}
