﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using static System.Environment;
using static NoCarsAllowed.Model.TCatalog;
using static NoCarsAllowed.Form1;
using NoCarsAllowed.Model;

namespace NoCarsAllowed.View
{
    public partial class Form2 : Form
    {
        private Model.TCatalog catalog = new Model.TCatalog();  // Объявление основного класса каталога
        private string clwarD, clwarN, svwarD;  // Переменные текста MSApplication оповещений 
        private bool show = true, listC = false, disabled, s0, s1, s2, s3, s4; // show - переменная для блокирования компонентов при переключении на третью форму
                                                                // s0 - переменная отображения названий строк колонок списков, s1, s2, s3, s4 - переменные для отображения колонок списка
                                                                // dated - переменная для предотвращения двойного изменения даты
        // Метод для заполнения колонок соответствующего списка (i - номер списка, Name - номер списка, Mark, Bodycar, Country, Date, Price - данные объекта для текстовых окон)
        private void Change(int i, DomainUpDown Name, TextBox Mark, TextBox Bodycar, TextBox Country, TextBox Date, TextBox Price)
        {
            Name.Text = $"{i + 1}";
            Mark.Text = $"{catalog[i].Mark}";
            Bodycar.Text = $"{catalog[i].Bodycar}";
            Country.Text = $"{catalog[i].Country}";
            Date.Text = $"{catalog[i].Date}";
            Price.Text = $"{catalog[i].Price}";
        }
        // Метод для обновления содержимого колонок списков и счётчиков в форме
        public void ResetForm2()
        {
            // Очистка списков для заполнения нового числа доступных объектов
            domainRemove.Items.Clear(); // Список удаления объектов  
            domain1.Items.Clear();      // Список первой колонки
            domain2.Items.Clear();      // Список второй колонки
            domain3.Items.Clear();      // Список третьей колонки
            domain4.Items.Clear();      // Список четвёртой колонки
            for (int i = catalog.Length; i > 0; i--)    // Добавление доступных объектов по длине массива в каждый список
            {
                domainRemove.Items.Add(i);
                domain1.Items.Add(i);
                domain2.Items.Add(i);
                domain3.Items.Add(i);
                domain4.Items.Add(i);
            }
            txtName.Text = $"{catalog.Name}";   // Замена текстового поля на соответствующее названием каталога из объекта
            numCars.Text = $"{catalog.Number}"; // Изменение метки количества объектов на соответствущий из объекта
            theAverage.Text = $":  {catalog.AvgServiceTime()}"; // Изменение метки среднего года службы с помощью метода AvgServiceTime из TCatalog
            theMoist.Text = $":  {catalog.MostExpensive()}";    // Изменение метки самого дорогого объекта методом MostExpensive из TCatalog
            if (Int32.Parse(numCars.Text) == 0) //  Условие отображения формы если в массиве нет объектов
            {
                stopitplz = true;   // Блокировка мигания кнопки удаления объекта
                disabled = true;    // Блокировка активных кнопок для работы с объектами при их отсутствии
                domainRemove.Text = "0"; // Отображение отсутствия доступных объектов для удаления в списке 
                MOIST.ForeColor = Color.DimGray;    // Визуальное отображение бездействия меток - Самый дорогой объект
                theMoist.Visible = false;   // Блокировка отображения соответствующего результата
                AVERAGE.ForeColor = Color.DimGray;  // - Средний год службы объектов
                theAverage.Visible = false;
                REMOVE.ForeColor = Color.DimGray;   // - Удаление объектов (Метка кнопка)
                domainRemove.Enabled = false;
                ASCEND.ForeColor = Color.DimGray;   // - Сортировка по убыванию даты изготовления объектов (Метка кнопка)
            }
            else
            {   
                disabled = false;   // Если объект содержит хотя бы один объект кнопки для работы с объектами будут активны
                domainRemove.Text = "1";    // Отображение первого объекта для удаления объектов
                MOIST.ForeColor = Color.White;  // Визуальное отображение работоспособности меток
                theMoist.Visible = true;    // Отображение результата 
                AVERAGE.ForeColor = Color.White;
                theAverage.Visible = true;
                REMOVE.ForeColor = Color.White;
                domainRemove.Enabled = true;
                ASCEND.ForeColor = Color.White;
            }
            if (catalog.Length == 0)    // Условие отображения списков при отсутствии объектов
            {   // Списки не отображаются
                s0 = false;
                s1 = false;
                s2 = false;
                s3 = false;
                s4 = false;
            }
            if (catalog.Length == 1)    // В зависимости от количества объектов (от одного до четырёх) отображаются соответствующие колонки списков
            {   // Метки с наименованием строк и текстовые окна первого списка отображаются
                s0 = true;   
                s1 = true;
                s2 = false;
                s3 = false;
                s4 = false;
                Change(0, domain1, txtMark1, txtBodycar1, txtCountry1, txtDate1, txtPrice1);    // Заполнение текстовых окон первого списка данными объекта
            }
            if (catalog.Length == 2)
            {
                s0 = true;
                s1 = true;
                s2 = true;
                s3 = false;
                s4 = false;
                Change(0, domain1, txtMark1, txtBodycar1, txtCountry1, txtDate1, txtPrice1);
                Change(1, domain2, txtMark2, txtBodycar2, txtCountry2, txtDate2, txtPrice2);
            }
            if (catalog.Length == 3)
            {
                s0 = true;
                s1 = true;
                s2 = true;
                s3 = true;
                s4 = false;
                Change(0, domain1, txtMark1, txtBodycar1, txtCountry1, txtDate1, txtPrice1);
                Change(1, domain2, txtMark2, txtBodycar2, txtCountry2, txtDate2, txtPrice2);
                Change(2, domain3, txtMark3, txtBodycar3, txtCountry3, txtDate3, txtPrice3);
            }
            if (catalog.Length > 3)
            {
                s0 = true;
                s1 = true;
                s2 = true;
                s3 = true;
                s4 = true;
                Change(0, domain1, txtMark1, txtBodycar1, txtCountry1, txtDate1, txtPrice1);
                Change(1, domain2, txtMark2, txtBodycar2, txtCountry2, txtDate2, txtPrice2);
                Change(2, domain3, txtMark3, txtBodycar3, txtCountry3, txtDate3, txtPrice3);
                Change(3, domain4, txtMark4, txtBodycar4, txtCountry4, txtDate4, txtPrice4);
            }   // В зависимости от полученных булевых переменных отображаются компоненты формы 
            MARK.Visible = s0;
            BODYCAR.Visible = s0;
            COUNTRY.Visible = s0;
            DATE.Visible = s0;
            PRICE.Visible = s0;
            txtMark1.Visible = s1;
            txtBodycar1.Visible = s1;
            txtCountry1.Visible = s1;
            txtDate1.Visible = s1;
            txtPrice1.Visible = s1;
            domain1.Visible = s1;
            txtMark2.Visible = s2;
            txtBodycar2.Visible = s2;
            txtCountry2.Visible = s2;
            txtDate2.Visible = s2;
            txtPrice2.Visible = s2;
            domain2.Visible = s2;
            txtMark3.Visible = s3;
            txtBodycar3.Visible = s3;
            txtCountry3.Visible = s3;
            txtDate3.Visible = s3;
            txtPrice3.Visible = s3;
            domain3.Visible = s3;
            txtMark4.Visible = s4;
            txtBodycar4.Visible = s4;
            txtCountry4.Visible = s4;
            txtDate4.Visible = s4;
            txtPrice4.Visible = s4;
            domain4.Visible = s4;
        }
        public Form2()
        {
            InitializeComponent();
            ResetForm2();   // Заполнение второй формы после создания
        }
        public Model.TCatalog Catalog   // Свойство для передачи объекта каталога другой форме
        {
            get
            {
                return catalog;
            }
            set
            {
                catalog = value;    // Для дополнения объекта catalog в третьей форме добавляем аксессор set
            }
        }
        protected override CreateParams CreateParams    // Присвоение двойной буферизации для параметров создания второй форме (аналогично первой форме)
        {
            get
            {
                var c = base.CreateParams;
                c.ExStyle = 0x02000000;
                return c;
            }
        }
        // Событие для обоих компонентов pictureBox при необходимости изменения изображения 
        private void pictureBox1_On_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Frm1.image, new Point(Frm1.x - 785, Frm1.y - 705));  // Для нижнего компонента используется смещение к нижней части изображения  
        }
        private void pictureBox2_On_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Frm1.image, new Point(Frm1.x - 785, Frm1.y - 360));
        }
        // Событие при закрытии второй формы
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Frm1.Close(); // Зарытие корневой формы
            e.Cancel = true;    // Если первая форма не была закрыта, событие отменяется
        }
        // Присвоение языа форме (аналогично первой форме)
        public void GetLanguage()
        {
            Frm1.L = byte.Parse(Model.TCatalog.FindValue("language", 'i'));
            if (Frm1.L == 0)
            {
                ListC.Text = "List";
                LANGUAGE.Text = "ENG";
                LanguageChange(NEW, "CLEAR", 620, 18, 28);
                LanguageChange(B3, "|", 590, 15, 28);
                LanguageChange(SAVE, "SAVE", 479, 18, 28);
                LanguageChange(B4, "|", 451, 15, 28);
                LanguageChange(OPEN, "OPEN", 334, 18, 28);
                LanguageChange(EXIT, "EXIT", 656, 492);
                LanguageChange(B1, "|", 628, 489);
                LanguageChange(INFO, "INFO", 522, 492);
                LanguageChange(B2, "|", 494, 489);
                LanguageChange(MAIN, "MAIN", 372, 492);
                MARK.Text = "MARK";
                BODYCAR.Text = "BODYCAR";
                COUNTRY.Text = "COUNTRY";
                DATE.Text = "DATE";
                PRICE.Text = "PRICE";
                LanguageChange(ADD, "ADD", 74, 131);
                LanguageChange(REMOVE, "REMOVE", 30, 159);
                LanguageChange(ASCEND, "DATE ASCEND", 27, 187, 15);
                LanguageChange(RESET, "RESET", 67, 215);
                LanguageChange(CATALOGNAME, "CATALOG NAME", 199, 131, 15);
                txtName.Location = new Point(382, 135);
                NUMOFCARS.Text = "NUMBER OF CARS";
                numCars.Location = new Point(405, 156);
                LanguageChange(MOIST, "MOIST EXPENSIVE", 199, 178, 12);
                theMoist.Location = new Point(393, 177);
                LanguageChange(AVERAGE, "AVERAGE SERVICE TIME", 200, 199, 12);
                theAverage.Location = new Point(393, 198);
                clwarN = "Are you sure?";
                svwarD = "It'll overwrite the save file and it's parameters cannot be reset to the primordial ones.";
                clwarD = "It'll clear all of the unsaved changes.";
            }
            if (Frm1.L == 1)
            {;
                ListC.Text = "Лист";
                LANGUAGE.Text = "RUS";
                LanguageChange(NEW, "ОЧИСТИТЬ", 582, 23, 21);
                LanguageChange(B3, "|", 559, 20, 21);
                LanguageChange(SAVE, "СОХРАНИТЬ", 364, 23, 21);
                LanguageChange(B4, "|", 341, 20, 21);
                LanguageChange(OPEN, "ОТКРЫТЬ", 187, 23, 21);
                LanguageChange(EXIT, "ВЫХОД", 590, 492);
                LanguageChange(B1, "|", 562, 489);
                LanguageChange(INFO, "ИНФОРМАЦИЯ", 244, 492);
                LanguageChange(B2, "|", 215, 489);
                LanguageChange(MAIN, "ГЛАВНАЯ", 12, 492);
                MARK.Text = "МАРКА";
                BODYCAR.Text = "КУЗОВ";
                COUNTRY.Text = "СТРАНА";
                DATE.Text = "ДАТА";
                PRICE.Text = "ЦЕНА";
                LanguageChange(ADD, "ДОБАВИТЬ", 31, 131);
                LanguageChange(REMOVE, "УДАЛИТЬ", 13, 159, 15);
                LanguageChange(ASCEND, "СОРТ. ПО ВОЗРАСТ.", 5, 190, 13);
                LanguageChange(RESET, "СБРОС", 60, 215);
                CATALOGNAME.Text = "НАЗВАНИЕ КАТАЛОГА";
                txtName.Location = new Point(448, 135);
                NUMOFCARS.Text = "КОЛИЧЕСТВО АВТОМОБИЛЕЙ :";
                numCars.Location = new Point(463, 156);
                MOIST.Text = "САМЫЙ ДОРОГОСТОЯЩИЙ";
                theMoist.Location = new Point(450, 177);
                AVERAGE.Text = "СРЕДНИЙ СРОК СЛУЖБЫ";
                theAverage.Location = new Point(450, 198);
                clwarN = "Вы уверены?";
                svwarD = "Данное действие перезапишит файл сохранения и его параметры нельзя будет сбросить до исходных.";
                clwarD = "Данное действие очистит все несохраненные изменения.";
            }
        }
        // Метод для преобразования даты к необходимому виду, Dmn - список объекта catalog, Txt - используемое окно с датой
        private void DateChange(DomainUpDown Dmn, TextBox Txt)
        {
            int day, month, year;
            if (Txt.Text.Length == 8)   // Условие даты из 8 символов
            {   // Для даты вида 1.1.1111 или 1 1 1111 
                if (((Txt.Text[1] == '.') | (Txt.Text[1] == ' ')) & Char.IsDigit(Txt.Text[0])) // Первый символ день, второй разделитель, точка или пробел
                {
                    day = Int32.Parse($"{Txt.Text[0]}");    // Присваивание дня с одним символом
                    if (((Txt.Text[3] == '.') | (Txt.Text[3] == ' ')) & Char.IsDigit(Txt.Text[2]) & Char.IsDigit(Txt.Text[4])
                        & Char.IsDigit(Txt.Text[5]) & Char.IsDigit(Txt.Text[6]) & Char.IsDigit(Txt.Text[7]))  // Третий символ месяц, четвёртый разделитель и последующие символы год
                    {
                        month = Int32.Parse($"{Txt.Text[2]}");  // Присваивание месяца с одним символом
                        year = Int32.Parse($"{Txt.Text[4]}{Txt.Text[5]}{Txt.Text[6]}{Txt.Text[7]}"); // Последующие сиволы являются присваиваются году
                        catalog[Int32.Parse(Dmn.Text) - 1].NewDate(year, month, day);  // Устанавливаем новую дату соответствующему объекту массива catalog
                        Txt.Text = $"{catalog[Int32.Parse(Dmn.Text) - 1].Date}";    // Отображение в текстовом окне новой преобразованной даты
                        Txt.SelectionStart = Txt.Text.Length;   // Перенос курсора в конец строки
                        Txt.SelectionLength = 0;    // Обнуление выделения
                    }
                }
            }
            if (Txt.Text.Length == 9)   // Условие даты из 9 символов
            {   // Для даты вида 11.1.1111 или 11 1 1111 
                if (((Txt.Text[2] == '.') | (Txt.Text[2] == ' ')) & Char.IsDigit(Txt.Text[0]) & Char.IsDigit(Txt.Text[1])) // Первый и второй символ день, третий разделитель
                {
                    day = Int32.Parse($"{Txt.Text[0]}{Txt.Text[1]}"); // Присваивание дня с двумя символом
                    if (((Txt.Text[4] == '.') | (Txt.Text[4] == ' ')) & Char.IsDigit(Txt.Text[3]) & Char.IsDigit(Txt.Text[5])
                         & Char.IsDigit(Txt.Text[6]) & Char.IsDigit(Txt.Text[7]) & Char.IsDigit(Txt.Text[8])) // четвёртый символ месяц, пятый разделитель
                    {
                        month = Int32.Parse($"{Txt.Text[3]}");
                        year = Int32.Parse($"{Txt.Text[5]}{Txt.Text[6]}{Txt.Text[7]}{Txt.Text[8]}");
                        catalog[Int32.Parse(Dmn.Text) - 1].NewDate(year, month, day);
                        Txt.Text = $"{catalog[Int32.Parse(Dmn.Text) - 1].Date}";
                        Txt.SelectionStart = Txt.Text.Length;
                        Txt.SelectionLength = 0;
                    }
                }   // Для даты вида 1.11.1111 или 1 11 1111 
                if (((Txt.Text[1] == '.') | (Txt.Text[1] == ' ')) & Char.IsDigit(Txt.Text[0])) // Первый символ день, второй разделитель
                {
                    day = Int32.Parse($"{Txt.Text[0]}");
                    if (((Txt.Text[4] == '.') | (Txt.Text[4] == ' ')) & Char.IsDigit(Txt.Text[2]) & Char.IsDigit(Txt.Text[3]) // Третий и четвёртый символ месяц, пятый разделитель
                        & Char.IsDigit(Txt.Text[5]) & Char.IsDigit(Txt.Text[6]) & Char.IsDigit(Txt.Text[7]) & Char.IsDigit(Txt.Text[8]))   
                    {
                        month = Int32.Parse($"{Txt.Text[2]}{Txt.Text[3]}");
                        year = Int32.Parse($"{Txt.Text[5]}{Txt.Text[6]}{Txt.Text[7]}{Txt.Text[8]}");
                        catalog[Int32.Parse(Dmn.Text) - 1].NewDate(year, month, day);
                        Txt.Text = $"{catalog[Int32.Parse(Dmn.Text) - 1].Date}";
                        Txt.SelectionStart = Txt.Text.Length;
                        Txt.SelectionLength = 0;
                    }
                }
            }
            if (Txt.Text.Length >= 10)
            {   // Для даты вида 11.11.1111 или 11 11 1111 
                    if (((Txt.Text[2] == '.') | (Txt.Text[2] == ' ')) & Char.IsDigit(Txt.Text[0]) & Char.IsDigit(Txt.Text[1]))  // Первый и второй символ день, третий разделитель
                {
                    day = Int32.Parse($"{Txt.Text[0]}{Txt.Text[1]}");
                    if (((Txt.Text[5] == '.') | (Txt.Text[5] == ' ')) & Char.IsDigit(Txt.Text[3]) & Char.IsDigit(Txt.Text[4])   // четвёртый и пятый символ месяц, шестой разделитель
                        & Char.IsDigit(Txt.Text[6]) & Char.IsDigit(Txt.Text[7]) & Char.IsDigit(Txt.Text[8]) & Char.IsDigit(Txt.Text[9]))
                    {
                        month = Int32.Parse($"{Txt.Text[3]}{Txt.Text[4]}");
                        year = Int32.Parse($"{Txt.Text[6]}{Txt.Text[7]}{Txt.Text[8]}{Txt.Text[9]}");
                        catalog[Int32.Parse(Dmn.Text) - 1].NewDate(year, month, day);
                        Txt.Text = $"{catalog[Int32.Parse(Dmn.Text) - 1].Date}";
                        Txt.SelectionStart = Txt.Text.Length;
                        Txt.SelectionLength = 0;
                    }
                }
            }
            theAverage.Text = $":  {catalog.AvgServiceTime()}"; // Обновляем среднее время службы после изменении даты
        }
        // Метод для проверки цены, Dmn - список объекта catalog, Txt - используемое окно с ценой
        private void PriceChange(DomainUpDown Dmn, TextBox Txt)
        {
            string price = "";  // Переменная новой цены объекта
            for (int i = 0; i < Txt.Text.Length; i++)   // Добавление всех числел в окне
            {
                if (Char.IsDigit(Txt.Text[i]))
                {
                    price += Txt.Text[i];
                }
            }
            if (price != "") // Если цена введена она передаётся объету 
            {
                catalog[Int32.Parse(Dmn.Text) - 1].Price = Int32.Parse(price);
            }
            theMoist.Text = $":  {catalog.MostExpensive()}";    // Обновляем самый дорогостоящий объект
        }
        // Метод для дизфункции активных кнопок, пока форма не активна, disenable - переменная для переключения формы
        public void EnableForm2(bool disenable)
        { 
            show = disenable;
            txtName.Enabled = show;
            domain1.Enabled = show;
            txtMark1.Enabled = show;
            txtBodycar1.Enabled = show;
            txtCountry1.Enabled = show;
            txtDate1.Enabled = show;
            txtPrice1.Enabled = show;
            domain2.Enabled = show;
            txtMark2.Enabled = show;
            txtBodycar2.Enabled = show;
            txtCountry2.Enabled = show;
            txtDate2.Enabled = show;
            txtPrice2.Enabled = show;
            domain3.Enabled = show;
            txtMark3.Enabled = show;
            txtBodycar3.Enabled = show;
            txtCountry3.Enabled = show;
            txtDate3.Enabled = show;
            txtPrice3.Enabled = show;
            domain4.Enabled = show;
            txtMark4.Enabled = show;
            txtBodycar4.Enabled = show;
            txtCountry4.Enabled = show;
            txtDate4.Enabled = show;
            txtPrice4.Enabled = show;
            domainRemove.Enabled = show;
        }
        // Метод для сохранения текущего объекта в list.txt
        public static void SaveList(Model.TCatalog catalog, int its)
        {   // Создаём новую папку под файл если она отсутствует
            Directory.CreateDirectory($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save");
            if (File.Exists($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt"))
            {
                GC.Collect(); 
                GC.WaitForPendingFinalizers();
                File.Delete($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt");
            }
            StreamWriter save = new StreamWriter(File.OpenWrite($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName }\save\list.txt"));    // Создаём файл и открываем его для записи
            save.WriteLine($"catalog_name \"{catalog.Name}\""); // Вписываем в него базовые строки
            save.WriteLine($"num_of_cars \"{catalog.Number}\"");
            save.Write($"language \"{its}\"");
            if (catalog.Length > 0)
                save.WriteLine();
            for (int i = 0; i < catalog.Length; i++)    // Добавляем текущие объекты в файл
            {
                save.WriteLine();
                save.WriteLine($"mark_{i + 1} \"{catalog[i].Mark}\""); // Сохраняем параметры в виде name_i "value"
                save.WriteLine($"bodycar_{i + 1} \"{catalog[i].Bodycar}\"");
                save.WriteLine($"country_{i + 1} \"{catalog[i].Country}\"");
                save.WriteLine($"day_{i + 1} \"{catalog[i].RealDate.Day}\"");
                save.WriteLine($"month_{i + 1} \"{catalog[i].RealDate.Month}\"");
                save.WriteLine($"year_{i + 1} \"{catalog[i].RealDate.Year}\"");
                if (i == catalog.Length - 1)    // чтобы не создавать лишнюю пустую строку, вписываем последнюю строку не создавая новую 
                    save.Write($"price_{i + 1} \"{catalog[i].Price}\"");
                else
                    save.WriteLine($"price_{i + 1} \"{catalog[i].Price}\"");
            }
            save.Close();   // Освобождаем поток
        }
        // Метод для сортировки объектов по возрастанию даты изготовления
        private void Ascend()
        {   // Аналогично методу из TCatalog
            for (int i = 0; i < catalog.Length; i++)
            {
                for (int j = 0; j < catalog.Length - 1; j++)
                {
                    if (catalog[j].RealDate > catalog[j + 1].RealDate)  // Используем свойство RealDate чтобы сравнивать структуры DateTime вместо классов String
                    {   // Смещаем все параметры объекта с поздней датой на позицию вверх, меняя местами их значения
                        (catalog[j + 1].Mark, catalog[j].Mark) = (catalog[j].Mark, catalog[j + 1].Mark);
                        (catalog[j + 1].Bodycar, catalog[j].Bodycar) = (catalog[j].Bodycar, catalog[j + 1].Bodycar);
                        (catalog[j + 1].Country, catalog[j].Country) = (catalog[j].Country, catalog[j + 1].Country);
                        (catalog[j + 1].RealDate, catalog[j].RealDate) = (catalog[j].RealDate, catalog[j + 1].RealDate);
                        (catalog[j + 1].Price, catalog[j].Price) = (catalog[j].Price, catalog[j + 1].Price);
                    }
                }
            }
            ResetForm2();   // Обновляем форму чтобы отобразить объект после изменений
        }
        // Событие при поднятии клавиши мыши для смены языка (аналогично первой форме)
        private void LANGUAGE_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true | listC == true)   // Если активна третья форма, активные метки второй формы блокируются
                {
                    Frm1.L = byte.Parse(FindValue("language", 'i'));
                    bool Changed = false;
                    if (Frm1.L == 0)
                    {
                        Frm1.L = 1;
                        Changed = true;
                    }
                    if (Frm1.L == 1 & Changed == false)
                    {
                        Frm1.L = 0;
                    }
                    ReplaceValue("language", Frm1.L);
                    GetLanguage();
                    Frm1.GetLanguage(); // Для перевода оповещений о закрытии всех форм, обновляем язык первой формы в которой они находятся
                }
            }
        }
        // Событие при поднятии клавиши мыши для открытия файла сохранения
        private void OPEN_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true | listC == true)
                {
                    Process.Start($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\save\list.txt"); // Создаём процесс программы по умолчанию для открытия текстового файла list.txt
                }
            }
        }
        // Событие при поднятии клавиши мыши для сохранения файла list.txt
        private void SAVE_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true | listC == true)
                {   //  Для создания предупреждения о сохранении приложения присваиваем перечислению результат нажатой кнопки
                    DialogResult a = MessageBox.Show(svwarD, clwarN, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (a == DialogResult.Yes)
                    {   // При подтверждении сохранения вызываем метод для сохранения объектов массива в list.tхt 
                        if (listC == true)
                            ListLoad();
                        SaveList(catalog, Frm1.L);
                    }
                    ResetForm2();   // Загружаем новый объект в форму
                }
            }
        }
        // Событие для очистки объектов из массива находясь во второй форме (аналогично первой форме)
        private void NEW_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true | listC == true)
                {
                    DialogResult a = MessageBox.Show(clwarD, clwarN, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (a == DialogResult.Yes)
                    {
                        catalog.Length = 0;
                    }
                    ResetForm2();
                }
            }
        }
        // Событие для перехода на вторую форму (аналогично первой форме)
        private  void MAIN_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true | listC == true)
                {
                    MAIN.Font = new Font(MAIN.Font.Name, 28);   // Возвращаем исходное состояние метки, т.к. метод Pressing не успеет отразится на форме
                    ConsoleInv = 1;
                    Frm1.ConsoleMove(); // Повторный вызов движения фона после перехода на низшую форму
                    Frm1.ChangeForm(Frm1, this);
                }
            }
        }
        // Событие для закрытия формы (аналогично первой форме)
        private void EXIT_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true | listC == true)
                {
                    Frm1.Close();
                }
            }
        }
        // Событие для добавления нового объекта в массив объектов
        private void ADD_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true)
                {
                    Form3 f3 = new Form3(); // Открытие третьей формы для добавления нового объекта
                    f3.Show(this);  // Устанавливаем её как дочернюю форму данной формы
                    EnableForm2(false); // Блокируем вторую форму
                    ADD.Font = new Font(ADD.Font.Name, 15); // Возвращаем метку к исходному размеру, т.к. событие ADD_MouseDown не случится
                    stopitplz = true;   // Останавливаем мигание
                    ADD.ForeColor = Color.White;    // Возвращаем кнопке нейтральный цвет, т.к. событие ADD_MouseLeave не произойдёт
                }
            }
        }
        // Событие для удаление объекта из массива
        private void REMOVE_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true & disabled == false)
                {
                    catalog.remove = Int32.Parse(domainRemove.Text);    // Использование свойства remove для удаления присвоенного списка 
                    ResetForm2();   // Обновление формы чтобы отобразить новый массив
                }
            }
        }
        // Событие для сортировки по возрастанию
        private void ASCEND_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true & disabled == false)
                {
                    Ascend();   // Использование соответствующего метода
                }
            }
        }
        // Событие для присвоения форме данных файла сохранения
        private void RESET_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true)
                {
                    {
                        catalog = new Model.TCatalog(); // Создание нового массива с данными текущего файла
                        ResetForm2();   // Обновляем форму для демонстрации нового массива
                    }
                }
            }
        }
        private  void INFO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (show == true | listC == true)
                {
                    INFO.Font = new Font(INFO.Font.Name, 28);
                    ConsoleInv = 3;
                    FromFrm = 2;
                    Frm4.GetLanguage(); // Установка языка второй формы
                    Frm1.ChangeForm(Frm4, this);
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ListC.Checked == true)
            {
                EnableForm2(false);
                listC = true;
                textBox1.Visible = true;
                string list = $"catalog_name \t\"{catalog.Name}\"{NewLine}num_of_cars \t\"{catalog.Number}\"";
                for (int i = 0; i < catalog.Length; i++)
                {
                    list += $"{NewLine}{NewLine}mark_{i + 1} \t\t\"{catalog[i].Mark}\"{NewLine}bodycar_{i + 1} \t\"{catalog[i].Bodycar}\"{NewLine}country_{i + 1} \t\"{catalog[i].Country}\"" +
                        $"{NewLine}day_{i + 1}       \t\"{catalog[i].RealDate.Day}\"{NewLine}month_{i + 1}   \t\"{catalog[i].RealDate.Month}\"{NewLine}year_{i + 1}      \t\"{catalog[i].RealDate.Year}\"" +
                        $"{NewLine}price_{i + 1}     \t\"{catalog[i].Price}\"";
                }
                textBox1.Text = list;
                } else
            {
                EnableForm2(true);
                ListLoad();
                listC = false;
                textBox1.Visible = false;
            }
        }
        int comingLength = 0, wtf = 0;
        private void ListLoad()
        {
            int day, month, year;
            catalog.Name = FindValueString("catalog_name", 's');
            catalog.Length = int.Parse(FindValueString("num_of_cars", 'i'));
            for (int i = 0; i < catalog.Length; i++) // Заполнение объектов массива
            {
                // Получение значений параметров с натуральным идентификатором объектов из файла для объектов массива cars
                catalog[i].Mark = FindValueString($"mark_{i + 1}", 's');
                catalog[i].Bodycar = FindValueString($"bodycar_{i + 1}", 's');
                catalog[i].Country = FindValueString($"country_{i + 1}", 's');
                day = Int32.Parse(FindValueString($"day_{i + 1}", 'd'));
                month = Int32.Parse(FindValueString($"month_{i + 1}", 'd'));
                year = Int32.Parse(FindValueString($"year_{i + 1}", 'y'));
                catalog[i].NewDate(year, month, day);
                catalog[i].Price = Int32.Parse(FindValueString($"price_{i + 1}", 'i'));
                Console.WriteLine($"{comingLength}/{textBox1.Text.Length}\t|\tObject[{i+1}] loaded\t|\tNumber of iterations - {wtf}");
                wtf = 0;
            }
            comingLength = 0;
            ResetForm2();
        }
        public string FindValueString(string name, char Type)
        {
            bool contain = false, resultCorrect = false, unbeginned = true;
            string result = "";
            int i = comingLength, j = 0, length = textBox1.Text.Length, first_i;
            if ((textBox1.Text).Contains(name))
            {
                Start:
                for (; i < length; i++)
                {
                    wtf++;
                    if (textBox1.Text[i] == name[0])
                    {
                        for (; j < name.Length; i++, j++)
                        {
                            wtf++;
                            if (textBox1.Text[i] == name[j])
                            {
                                contain = true;
                            } else
                            {
                                contain = false;
                                j = 0;
                                break;
                            }
                        }
                        if (contain & i != length)
                        {
                            if (char.IsDigit(textBox1.Text[i]))
                            {
                                j = 0;
                                contain = false;
                            }
                        }
                        if (contain)
                        {
                            first_i = i - name.Length;
                            for (; i < length; i++)
                            {
                                wtf++;
                                if (textBox1.Text[i] == NewLine[0])
                                {
                                    while (textBox1.Text[first_i] != NewLine[0] | textBox1.Text[first_i] != textBox1.Text[0])
                                    {
                                        first_i--;
                                        if (textBox1.Text[first_i] == '"')
                                        {
                                            contain = true;
                                            while (textBox1.Text[first_i] != NewLine[0] | textBox1.Text[first_i] != textBox1.Text[0])
                                            {
                                                first_i--;
                                                if (first_i >= 0)
                                                {
                                                    if (textBox1.Text[first_i] == '"')
                                                    {
                                                        if (textBox1.Text[first_i + 1] == '"')
                                                            continue;
                                                        string newResult = "";
                                                        for (int i_verse = result.Length - 1; 0 <= i_verse; i_verse--)
                                                        {
                                                            newResult += result[i_verse];
                                                        }
                                                        result = newResult;
                                                        resultCorrect = true;
                                                        goto MethodEnding;
                                                    }
                                                }
                                                else goto MethodEnding;
                                                result += textBox1.Text[first_i];
                                            }
                                        }
                                        contain = false;
                                    }
                                }
                                if (textBox1.Text[i - 1] == '"')
                                {   // Для считывания результата мы используем ещё один оператор итераций, для отсчёта первой переменной результата мы берём длину поиска в строке
                                    // Ограничением будет последний символ '"' даже если он занимает оставшуюся часть строки
                                    for (; i < length; i++)
                                    {   // Т.к. мы для идентификации использовали прошлый символ как '"', то последний символ поиска является первым символом результатом
                                        // Для идентификации закрывающего символа '"', мы ищем следующее появление данного символа
                                        wtf++;
                                        if (textBox1.Text[i] == '"')
                                        {   // Для того чтобы считать результат после множественных '"' мы продолжаем считывание до следующего появления
                                            if (textBox1.Text[i - 1] == '"')
                                                continue;
                                            // Если данный символ является закрывающим то результат не является пустым, а цикл завершается с корректным результатом
                                            resultCorrect = true;
                                            break;
                                        }   // Если символ не является закрывающим символом, цикл продолжает считывание результата
                                        if (textBox1.Text[i] == NewLine[0])
                                        {
                                            resultCorrect = true;
                                            break;
                                        }
                                        result += textBox1.Text[i];
                                    }   // Если у результата не было закрывающего символа (или результат имеет вид "") то результатом возвращается пустое значение в зависимости от типа
                                    comingLength = i;
                                    goto MethodEnding;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                if (!contain & unbeginned)
                {
                    Console.WriteLine($"There's no {name}");
                    unbeginned = false;
                    i = 0;
                    length = comingLength;
                    goto Start;
                }
            }   // Если у результата не было закрывающего символа (или результат имеет вид "") то результатом возвращается пустое значение в зависимости от типа
            MethodEnding:
            if (Type == 'i' | Type == 'd' | Type == 'y')
            {
                try
                {   // Преобразуем строку в целочисленный тип
                    int check = Int32.Parse(result);
                }
                catch
                {
                    resultCorrect = false;
                }
            }
            if (!contain | result == null | !resultCorrect)
            {
                if (Type == 'i')
                    return "0";
                if (Type == 'd')
                    return "1";
                if (Type == 'y')
                    return "2000";
                if (Type == 's')
                    return "";
            }
            return result;
        }
        // События изменения текстовых окон
        private void domain1_SelectedItemChanged(object sender, EventArgs e)
        {
            Change(Int32.Parse(domain1.Text) - 1, domain1, txtMark1, txtBodycar1, txtCountry1, txtDate1, txtPrice1);
        }
        private void domain2_SelectedItemChanged(object sender, EventArgs e)
        {
            Change(Int32.Parse(domain2.Text) - 1, domain2, txtMark2, txtBodycar2, txtCountry2, txtDate2, txtPrice2);
        }
        private void domain3_SelectedItemChanged(object sender, EventArgs e)
        {
            Change(Int32.Parse(domain3.Text) - 1, domain3, txtMark3, txtBodycar3, txtCountry3, txtDate3, txtPrice3);
        }
        private void domain4_SelectedItemChanged(object sender, EventArgs e)
        {
            Change(Int32.Parse(domain4.Text) - 1, domain4, txtMark4, txtBodycar4, txtCountry4, txtDate4, txtPrice4);
        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            catalog.Name = txtName.Text;
        }
        private void txtMark1_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain1.Text) - 1].Mark = txtMark1.Text;
        }
        private void txtBodycar1_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain1.Text) - 1].Bodycar = txtBodycar1.Text;
        }
        private void txtCountry1_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain1.Text) - 1].Country = txtCountry1.Text;
        }
        private void txtDate1_TextChanged(object sender, EventArgs e)
        {
            DateChange(domain1, txtDate1);
        }
        private void txtPrice1_TextChanged(object sender, EventArgs e)
        {
            PriceChange(domain1, txtPrice1);
        }
        private void txtMark2_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain2.Text) - 1].Mark = txtMark2.Text;
        }
        private void txtBodycar2_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain2.Text) - 1].Bodycar = txtBodycar2.Text;
        }
        private void txtCountry2_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain2.Text) - 1].Country = txtCountry2.Text;
        }
        private void txtDate2_TextChanged(object sender, EventArgs e)
        {
            DateChange(domain2, txtDate2);
        }
        private void txtPrice2_TextChanged(object sender, EventArgs e)
        {
            PriceChange(domain2, txtPrice2);
        }
        private void txtMark3_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain3.Text) - 1].Mark = txtMark3.Text;
        }
        private void txtBodycar3_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain3.Text) - 1].Bodycar = txtBodycar3.Text;
        }
        private void txtCountry3_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain3.Text) - 1].Country = txtCountry3.Text;
        }
        private void txtDate3_TextChanged(object sender, EventArgs e)
        {
            DateChange(domain3, txtDate3);
        }
        private void txtPrice3_TextChanged(object sender, EventArgs e)
        {
            PriceChange(domain3, txtPrice3);
        }
        private void txtMark4_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain4.Text) - 1].Mark = txtMark4.Text;
        }
        private void txtBodycar4_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain4.Text) - 1].Bodycar = txtBodycar4.Text;
        }
        private void txtCountry4_TextChanged(object sender, EventArgs e)
        {
            catalog[Int32.Parse(domain4.Text) - 1].Country = txtCountry4.Text;
        }
        private void txtDate4_TextChanged(object sender, EventArgs e)
        {
            DateChange(domain4, txtDate4);
        }
        private void txtPrice4_TextChanged(object sender, EventArgs e)
        {
            PriceChange(domain4, txtPrice4);
        }
        // События наводки и отводки курсора по отношению к тексту с миганием и антимиганием
        private void LANGUAGE_MouseHover(object sender, EventArgs e)
        {
            if (show == true | listC == true)
                Blinking(LANGUAGE);
        }
        private void OPEN_MouseHover(object sender, EventArgs e)
        {
            if (show == true | listC == true)
                Blinking(OPEN);
        }
        private void SAVE_MouseHover(object sender, EventArgs e)
        {
            if (show == true | listC == true)
                Blinking(SAVE);
        }
        private void NEW_MouseHover(object sender, EventArgs e)
        {
            if (show == true | listC == true)
                Blinking(NEW);
        }
        private void MAIN_MouseHover(object sender, EventArgs e)
        {
            if (show == true | listC == true)
                Blinking(MAIN);
        }
        private void EXIT_MouseHover(object sender, EventArgs e)
        {
            if (show == true | listC == true)
                Blinking(EXIT);
        }
        private void ADD_MouseHover(object sender, EventArgs e)
        {
            if (show == true)
                Blinking(ADD);
        }
        private void REMOVE_MouseHover(object sender, EventArgs e)
        {
            if (show == true & disabled == false)
                Blinking(REMOVE);
        }
        private void ASCEND_MouseHover(object sender, EventArgs e)
        {
            if (show == true & disabled == false)
                Blinking(ASCEND);
        }
        private void RESET_MouseHover(object sender, EventArgs e)
        {
            if (show == true)
                Blinking(RESET);
        }
        private void INFO_MouseHover(object sender, EventArgs e)
        {
            if (show == true | listC == true)
                Blinking(INFO);
        }
        private void LANGUAGE_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(LANGUAGE);
        }
        private void OPEN_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(OPEN);
        }
        private void SAVE_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(SAVE);
        }
        private void NEW_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(NEW);
        }
        private void MAIN_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(MAIN);
        }
        private void EXIT_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(EXIT);
        }
        private void ADD_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(ADD);
        }
        private void REMOVE_MouseLeave(object sender, EventArgs e)
        {
            if (disabled == false)
                UnBlinking(REMOVE);
        }
        private void ASCEND_MouseLeave(object sender, EventArgs e)
        {
            if (disabled == false)
                UnBlinking(ASCEND);
        }
        private void RESET_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(RESET);
        }
        private void INFO_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(INFO);
        }
    }
}