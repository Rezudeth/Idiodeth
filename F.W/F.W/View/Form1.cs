using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NoCarsAllowed.View;
using static NoCarsAllowed.Model.TCatalog;

namespace NoCarsAllowed
{
    public partial class Form1 : Form
    {
        public static bool stopitplz; // Булевая переменная для прекращения работы основного блока метода Blinking() во время перехода к UnBlinking() для всех форм
        public byte L; // Восьмибитная целочисленная переменная для хранения текущего языка программы
        public Int16 x = 0, y = 0;  // Шестнадцатибитные целочисленные переменные для работы со смещением изображения q3_console.png на панели
        public static string clwarD, clwarN, esvwarD, eD, eN, esD, esN;   // Переменные текста MSApplication оповещений
                        // clwarD - Описание оповещения о закрытии, clwarN - Имя оповещения о закрытии, esvwarD - Описание оповещения о сохранении при закрытии  
        public static Form4 Frm4;   // Объект первой формы для работы во всех формах
        public static Form2 Frm2;   // Объект второй формы для работы во всех формах
        public static Form1 Frm1;   // Объект первой формы для работы во всех формах
        public static int ConsoleInv = 1;   // Переменная для нанесения на pictureBox активной формы q3_console.png при переключении форм
        // Растровое изображение q3_console.png которое находится в папке need
        public static float fontSize;   // Исходный размер  нажатой кнопки
        public static bool fontChange = false;  // Переменная для проверки нажатия кнопки 
        public static Label LastName;   // Переменная для проверки соответствующей кнопки при нажатии
        public static int FromFrm;
        public Bitmap image = (Bitmap)Bitmap.FromFile($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\need\q3_console.png");
        public Form1()
        {
            Frm1 = this; // Присвоение первой форме ранее созданного объекта
            Frm2 = new Form2(); // Присвоение второй форме нового объекта формы
            Frm4 = new Form4();
            this.StartPosition = FormStartPosition.CenterScreen;    // Установка позиции окна первой формы по центру экрана
            InitializeComponent();  // Инициализируем компоненты Form1.Designer.cs
            pictureBox1.Controls.Add(pictureBox2);  // Добавляем в коллекцию анимированное изображение-текст к центральному изображению для установления цвета фона
            pictureBox2.Location = new Point(95, 140);  // Устанавливаем позицию анимационного изображения на центр
            pictureBox2.BackColor = Color.Transparent;  // Устанавливаем фон панели с анимрованным изображением как прозрачный 
            this.Show();    // Выводим первую форму
            Frm2.Show();    // Для бесшовного перехода между формами заранее загружаем вторую форму
            Frm2.Location = this.Location;
            Frm2.Hide();    // После загрузки формы сразу же скрываем её пока не потребуется
            Frm4.Show();
            Frm4.Location = this.Location;
            Frm4.Hide();
            GetLanguage();  // Присвоение программе языка в зависимости от установленного в файле
        }
        // Переопределяем свойство CreateParams из пространства имён наследуемого класса Form устанавливая параметры создания для данной формы
        protected override CreateParams CreateParams
        {
            get
            {   // Для дополнения акссесора get создаём новый объект класса CreateParams с базовыми блоками инструкций
                CreateParams c = base.CreateParams;
                c.ExStyle = 0x02000000; // Для активации двойной буферизации формы присваиваем свойству расширения начального состояния константу WS_EX_COMPOSITED
                return c; // Возвращаем расширенный объект и создаём на его основе форму
            }
        }
        // Метод для перемещения изображения q3_console.png
        private void Moving()
        {
            y += 1; // Скорость перемещения по оси ординат за итерацию
            x += 1; // по оси абсцисс
            if (x > 785)    // Если позиция по оси абсцисс изображения выходит за пределы она обнуляется
                x = 0;
            if (y > 360)    // по оси ординат
                y = 0;
        }
        // Метод для переключения активной формы с движением q3_console.png
        public async void ConsoleMove()
        {   // Используем оператор итераций while для определения нового положения изображения и его нанесения пока первая форма активна
            while (ConsoleInv == 1)
            {   
                Frm1.pictureBox1.Invalidate(); // Вызываем событие для нанесения изображения на компонент pictureBox1 расположенный по центру первой формы
                Moving();
                await Task.Delay(40);   // Использование асинхронной искусственной 40 мс'ной задержки для плавного движения изображения по компоненту
            }
            while (ConsoleInv == 2)
            {
                Frm2.pictureBox1.Invalidate();   // снизу второй формы
                Frm2.pictureBox2.Invalidate();   // сверху второй формы
                Moving();
                await Task.Delay(40);
            }
            while (ConsoleInv == 3)
            {
                Frm4.pictureBox1.Invalidate();   // слева четвёртой формы
                Frm4.pictureBox2.Invalidate();   // справа четвёртой формы
                Moving();
                await Task.Delay(40);
            }
        }
        // Событие при необходимости изменения изображения 
        private void pictureBox1_On_Paint(object sender, PaintEventArgs e)
        {
            // Использование аргументов графического события для размещения растрового изображения на заданной позиции
            e.Graphics.DrawImage(image, new Point(x - 785, y - 360));
        }
        // Событие загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {   // Вызов движения изображения после перехода форм
            ConsoleMove();
        }
        // Событие закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {   //  Для создания предупреждения о закрытии приложения присваиваем перечислению результат нажатой кнопки
            DialogResult a = MessageBox.Show(eD, eN, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (a == DialogResult.Yes)
            {   // При подтверждении закрытия, вызывается диалоговое окно для сохранения 
                DialogResult b = MessageBox.Show(esD, esN, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (b == DialogResult.Yes)
                {   // Обращение к статическому методу для сохранения объектов массива в list.tхt
                    Form2.SaveList(Frm2.Catalog, L);
                }
            }
            else e.Cancel = true;   // Отмена события при соответствующем результате перечисления 
        }
        // Метод мигания кнопок при наведении на них курсора, name - Название компонента
        public static async void Blinking(Label name)
        {
            stopitplz = false; // Если курсор находится над текстом, то метод возобновляет доступ к оператору while
            while (stopitplz == false)
            {   // Оператор for для максимального кодируемого числа цветов в 10 шагов за итерацию
                for (int i = 255; i > 0; i -= 10)
                {   // Если событие не активно во время итераций то оператор прерывается
                    if (stopitplz == true) break;
                    if (i < 0) i = 0;   // Если кодируемое число выходит за пределы, оно получает минимальное допустимое значение
                    name.ForeColor = Color.FromArgb(255, i, i); // Установка цвета тексту за итерацию от белого к красному
                    await Task.Delay(1);    // Миллисекунда для удерживания цвета во время накаливания
                }
                await Task.Delay(50);   // Более длительное удерживание красного цвета
                // для минимального кодируемого числа цветов
                for (int i = 0; i < 255; i += 10)
                {   
                    if (stopitplz == true) break;
                    if (i > 255) i = 255;   // Если кодируемое число выходит за пределы, оно получает максимальное допустимое значение
                    name.ForeColor = Color.FromArgb(255, i, i); // Установка цвета тексту за итерацию от красного к белому
                    await Task.Delay(1);
                }
            }
        }
        // Метод прекращения мигания кнопок при отведении от них курсора, name - Название компонента
        public static void UnBlinking(Label name)
        {   // Если курсор не находится над текстом, то асинхронный метод Blinking прекращается
            stopitplz = true;
            name.ForeColor = Color.White;   // Установка тексту изначального белого цвета 
            if (fontChange & LastName == name) // Если шрифт после нажатия изменился и событие соответствует компоненту пока курсор находится вне текста, возвращаем его размер в исходный
            {
                name.Font = new Font(name.Font.Name, fontSize);
                fontChange = false; // Обнуляем переменную
            }
        }
        // Метод для изменения текста и позиции компонента, x - Положение по абсциссе, y - Положение по ординате
        public static void LanguageChange(Label Name, string t, int x, int y)
        {
            Name.Text = t;
            Name.Location = new Point(x, y);
        }
        // Метод для изменения текста, его размера и позиции компонента, size - Новый размер
        public static void LanguageChange(Label Name, string t, int x, int y, int size)
        {
            Name.Text = t;
            Name.Location = new Point(x, y);
            Name.Font = new Font(Name.Font.Name, size);
        }
        // Метод для установки языка текстовых компонентов
        public void GetLanguage()
        {   // Считывание переменной языка из list.tхt и её использование для установки языка 0 - английский, 1 - русский
            L = byte.Parse(Model.TCatalog.FindValue("language", 'i'));
            if (L == 0) 
            {
                LANGUAGE.Text = "ENG";
                LanguageChange(NEW, "CLEAR", 620, 18);
                LanguageChange(EXIT, "EXIT", 656, 492);
                LanguageChange(B1, "|", 628, 489);
                LanguageChange(INFO, "INFO", 522, 492);
                LanguageChange(B2, "|", 494, 489);
                LanguageChange(START, "START", 357, 492);
                esvwarD = "Save all unsaved changes before exit?";
                clwarN = "Are you sure?";
                clwarD = "It'll clear all of the unsaved changes";
                eD = "Do you really want to exit?";
                eN = "Yeah sure, wtv";
                esD = "Save changes done into the list.txt file";
                esN = "Save changes?";
            }
            if (L == 1)
            {
                LANGUAGE.Text = "RUS";
                LanguageChange(NEW, "ОЧИСТИТЬ", 524, 18);
                LanguageChange(EXIT, "ВЫХОД", 590, 492);
                LanguageChange(B1, "|", 562, 489);
                LanguageChange(INFO, "ИНФОРМАЦИЯ", 244, 492);
                LanguageChange(B2, "|", 215, 489);
                LanguageChange(START, "СТАРТ", 77, 492);
                esvwarD = "Сохранить изменения перед выходом?";
                clwarN = "Вы уверены?";
                clwarD = "Данное действие очистит несохранённые данные";
                eD = "Вы действительно хотите выйти?";
                eN = "Ну да конечно, неважно";
                esD = "Сохранить внесённые изменения в файле list.txt?";
                esN = "Сохранить изменения?";
            }
        }
        // Событие при поднятии клавиши мыши для смены языка
        private void LANGUAGE_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {   // Считывание переменной языка из list.tхt
                L = byte.Parse(FindValue("language", 'i'));
                bool Changed = false;   // Переменная для проверки смены языка
                if (L == 0) // Если язык английскай то он заменяется на русский
                {
                    L = 1;
                    Changed = true;
                }
                if (L == 1 & Changed == false)  // Если язык русский и он не заменялся на русский во время метода он заменяется на английский
                {
                    L = 0;
                }
                ReplaceValue("language", L);    // Замена переменной в list.tхt
                GetLanguage();  // установки языка
            }
        }
        // Событие для очистки объектов из массива находясь в первой форме
        private void NEW_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DialogResult a = MessageBox.Show(clwarD, clwarN, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (a == DialogResult.Yes)
                {
                    Frm2.Catalog.Length = 0;    // Установка количества объектов массива во второй форме на ноль
                    Frm2.ResetForm2();  // Обновление второй формы
                }
            }
        }
        public async void ChangeForm(Form frm, Form oldfrm)
        {
            frm.Visible = true;
            frm.BringToFront();
            frm.Location = oldfrm.Location;
            await Task.Delay(10);
            oldfrm.Visible = false;
        }
        // Событие для перехода на вторую форму
        private void START_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ConsoleInv = 2;
                Frm2.GetLanguage(); // Установка языка второй формы
                ChangeForm(Frm2, this);
            }
        }
        private void INFO_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ConsoleInv = 3;
                FromFrm = 1;
                Frm4.GetLanguage(); // Установка языка второй формы
                ChangeForm(Frm4, this);
            }
        }
        // Событие для закрытия формы
        private void EXIT_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Frm1.Close();   // Вызов закрытия корневой формы
            }
        }
        // События наводки и отводки курсора по отношению к тексту с миганием и антимиганием
        private void LANGUAGE_MouseHover(object sender, EventArgs e)
        {
            Blinking(LANGUAGE);
        }
        private void NEW_MouseHover(object sender, EventArgs e)
        {
            Blinking(NEW);
        }
        private void START_MouseHover(object sender, EventArgs e)
        {
            Blinking(START);
        }
        private void INFO_MouseHover(object sender, EventArgs e)
        {
            Blinking(INFO);
        }
        private void EXIT_MouseHover(object sender, EventArgs e)
        {
            Blinking(EXIT);
        }
        private void LANGUAGE_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(LANGUAGE);
        }
        private void NEW_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(NEW);
        }
        private void START_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(START);
        }
        private void INFO_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(INFO);
        }
        private void EXIT_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(EXIT);
        }
    }
}
