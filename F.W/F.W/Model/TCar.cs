using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoCarsAllowed.Model
{
    public class TCar
    {
        string mark; // Переменная марки автомобиля
        string bodycar; // Переменная кузова автомобиля
        string country; // Переменная страны изготовителя
        DateTime date; // Объект структуры DateTime выпуска автомобиля
        int price; // Перемнная стоймости автомобиля
        public string Mark // Свойство для возвращения и присваивания переменной марки объекта
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
            }
        }
        public string Bodycar // Свойство для возвращения и присваивания переменной кузова объекта
        {
            get
            {
                return bodycar;
            }
            set
            {
                bodycar = value;
            }
        }
        public string Country // Свойство для возвращения и присваивания переменной страны изготовителя объекта
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public string Date // Свойство для возвращения и присваивания преобразованной в вид хх.хх.хххх даты с использованием строкового класса
        {
            get
            {   
                string redate = "";   // Пустая строковая переменная для преобразования даты к виду хх.хх.хххх
                if ($"{date.Day}".Length < 2)     // Если день не является двузначным, то к нему присваивается ноль
                    redate += $"0{date.Day}.";
                else redate += $"{date.Day}.";    // Если день не входит в диапазон от 1 до 9 дней он вписывается только с разделительной точкой
                if ($"{date.Month}".Length < 2)   // Если месяц не является двузначным, то к нему присваивается ноль
                    redate += $"0{date.Month}.";
                else redate += $"{date.Month}.";  // Если месяц не входит в диапазон от 1 до 9 дней он вписывается только с разделительной точкой
                return $"{redate}{date.Year}";    // Возвращаем полученную строку с присвоенным годом
            }
            set
            {
                int day = Int32.Parse($"{value[0]}" + $"{value[1]}");   // Переменная дня, которой присваивается первые два символа, где 2 символ это точка
                int month = Int32.Parse($"{value[3]}" + $"{value[4]}"); // Переменная месяца, которой присваивается третий и четвёртый и символ, где 5 символ это точка
                string years = "";  // Для года используем строковую переменную и присваиваем ей оставшееся значение новой даты
                for (int i = 6; i < value.Length; i++)
                {
                    years += $"{value[i]}";
                }
                date = new DateTime(Int32.Parse(years), month, day);    // Присваиваем объекту date новую дату с помощью нового объекта структуры DateTime
            }
        }
        public DateTime RealDate // Свойство для возвращения и присваивания переменной структуры DateTime даты объекта
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        // Метод для изменения, создания и исправления даты изготовления в Form2.cs после внесения изменений в поле, а также в конструкторе объекта для проверки даты 
        public void NewDate(int year, int month, int day)
        {
            if (month > 12) // Если новый месяц выходит за допустимые пределы, то новому месяцу присваивается максимальное значение
                month = 12;
            if (month < 1)  // Если новый месяц ниже допустимых значений, то новому месяцу присваивается минимальное значение
                month = 1;
            if (year < 1)   // Если новый год ниже допустимых значений, ему присваивается год по умолчанию
                year = 2000;
            if (year > DateTime.Today.Year)
            {
                year = DateTime.Today.Year;
                month = DateTime.Today.Month;
                day = DateTime.Today.Day;
            }
            if (day > DateTime.DaysInMonth(year, month))    // Для определения максимально доступного диапазона дней в месяце импользуем метод струк. DateTime, DaysInMonth
                day = DateTime.DaysInMonth(year, month);    // Если новое значение выходит за пределы ему присваивается последний день месяца
            if (day < 1)
                day = 1;    // Если новый день ниже допустимых значений, ему присваивается минимальный день в месяце
            DateTime update = new DateTime(year, month, day);   // Создаём новый объект структуры DateTime для замены
            if (update < DateTime.Today)    // Проверка корректности введёной даты по сравнению с текущей
            {   
                date = update;
            }   // Если новая дата изготовления указана как ещё не изготовленная в данное время, то ей выдаётся текущая дата
            else date = DateTime.Today;
            Console.WriteLine(date);
        }
        // Конструктор объекта TCar
        public TCar(string mark, string bodycar, string country, int monthRelease, int dayRelease, int yearRelease, int price)
        {
            // Заполнение значений параметров объекта соответствующих файлу сохранения
            this.mark = mark;
            this.bodycar = bodycar;
            this.country = country;
            NewDate(yearRelease, monthRelease, dayRelease); // Переменная даты корректируется и присваивается в методе
            Price = price; // Использование свойства Price для инверсии отрицательной цены
        }
        // Метод для отображения текущих данных объекта в строковом виде для консоли
        public string ShowCar() 
        {
            string sc = $"Mark: {mark}\n";
            sc += $"Bodycar: {bodycar}\n";
            sc += $"Publisher's country: {country}\n";
            sc += $"Release date: {date.Day}.{date.Month}.{date.Year}\n";
            sc += $"Service time: {ServiceTime()} years\n";
            sc += $"Price: {price}\n";
            return sc;
        }
        public int Price // Свойство для возвращения и присваивания цены с возможной инверсией при необходимости
        {
            get
            {
                return price;
            }
            set
            {   // При соответствии типа цены но её отрицании, цена инвертируется
                while (value < 0)
                {
                    value = -value;
                }
                price = value;
            }
        }
        // Метод для определения срока службы автомобиля
        public int ServiceTime()
        {   // Для сравнения создаём объект текущей даты
            DateTime dateToday = DateTime.Today; 
            int time = dateToday.Year - date.Year;  // Переменная для определения срока службы
            if (dateToday.AddYears(-time) < date)   // Для определения неполного года, пробуем получить дату выпуска от текущей
                time--; // Если год полученной даты выпуска больше изначальной, вычитаем этот год как не полный
            return time;
        }
    }
}
