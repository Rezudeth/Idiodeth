using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static NoCarsAllowed.Form1;

namespace NoCarsAllowed.View
{
    public partial class Form3 : Form
    {
        private Model.TCatalog catal;   // Временный объект TCatalog
        bool md = false;    // Подтверждение переключений перетаскивания формы с помощью панели
        Point ll;   // Изначальная позиция нажатия мыши при перетаскивании формы
        public Form3()
        {
            this.Left = Frm2.Left + 234;    // Используем положение второй формы и корректируем окно по центру
            this.Top = Frm2.Top + 162;
            catal = Frm2.Catalog;   // Создаём временный объект массива объектов для модификации
            InitializeComponent();
            GetLanguage();  // Активируем текущий язык формы
            this.BackgroundImage =  (Bitmap)Bitmap.FromFile($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\need\add_background.png"); // Устанавливаем фон формы
        }
        protected override CreateParams CreateParams   // Присвоение двойной буферизации для параметров создания третьей форме (аналогично первой форме)
        {
            get
            {
                var c = base.CreateParams;
                c.ExStyle = 0x02000000;
                return c;
            }
        }
        // Событие после закрытия формы
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Frm2.EnableForm2(true); // Вторая форма снова становится доступной
        }
        // Присвоение языа форме (аналогично первой форме)
        public void GetLanguage()
        {
            Frm1.L = byte.Parse(Model.TCatalog.FindValue("language", 'i'));
            if (Frm1.L == 0)
            {
                LanguageChange(MARK, "MARK");
                LanguageChange(BODYCAR, "BODYCAR");
                LanguageChange(COUNTRY, "COUNTRY");
                LanguageChange(DATE, "DATE");
                LanguageChange(PRICE, "PRICE");
                LanguageChange(ADD, "ADD", 25, 217, 20);
                LanguageChange(CANCEL, "CANCEL", 168, 217, 20);
                esvwarD = "Save all unsaved changes before exit?";
                clwarN = "Are you sure?";
                clwarD = "It'll clear all of the unsaved changes";
                eD = "Do you really want to exit?";
                eN = "Yeah sure, wtv";
                esD = "Save changes done into the list.txt file";
                esN = "Save changes?";
            }
            if (Frm1.L == 1)
            {
                LanguageChange(MARK, "МАРКА");
                LanguageChange(BODYCAR, "КУЗОВ");
                LanguageChange(COUNTRY, "СТРАНА");
                LanguageChange(DATE, "ДАТА");
                LanguageChange(PRICE, "ЦЕНА");
                LanguageChange(ADD, "ДОБАВИТЬ", 25, 225, 16);
                LanguageChange(CANCEL, "ОТМЕНА", 184, 225, 16);
                esvwarD = "Сохранить изменения перед выходом?";
                clwarN = "Вы уверены?";
                clwarD = "Данное действие очистит несохранённые данные";
                eD = "Вы действительно хотите выйти?";
                eN = "Ну да конечно, неважно";
                esD = "Сохранить внесённые изменения в файле list.txt?";
                esN = "Сохранить изменения?";
            }
        }
        private void ADD_MouseUp(object sender, MouseEventArgs e)
        {
            Pressing(ADD, '-');
            // Присваивание переменным значения по умолчанию чтобы можно было добавить объект оставив все строки пустыми
            int price = 0, day = 01, month = 01, year = 2000;
            if (txtPrice.Text != "")    // Условие для добавления цены в новый объект через третью форму
                price = Int32.Parse($"{txtPrice.Text}");
            // Аналогичные инструкции метода DateChange из второй формы
            if (txtDate.Text.Length == 8)
            { 
                if (((txtDate.Text[1] == '.') | (txtDate.Text[1] == ' ')) & Char.IsDigit(txtDate.Text[0]))
                {
                    day = Int32.Parse($"{txtDate.Text[0]}"); 
                    if (((txtDate.Text[3] == '.') | (txtDate.Text[3] == ' ')) & Char.IsDigit(txtDate.Text[2]) & Char.IsDigit(txtDate.Text[4])
                        & Char.IsDigit(txtDate.Text[5]) & Char.IsDigit(txtDate.Text[6]) & Char.IsDigit(txtDate.Text[7]))  
                    {
                        month = Int32.Parse($"{txtDate.Text[2]}"); 
                        year = Int32.Parse($"{txtDate.Text[4]}{txtDate.Text[5]}{txtDate.Text[6]}{txtDate.Text[7]}"); 
                    }
                }
            }
            if (txtDate.Text.Length == 9) 
            {   
                if (((txtDate.Text[2] == '.') | (txtDate.Text[2] == ' ')) & Char.IsDigit(txtDate.Text[0]) & Char.IsDigit(txtDate.Text[1])) 
                {
                    day = Int32.Parse($"{txtDate.Text[0]}{txtDate.Text[1]}"); 
                    if (((txtDate.Text[4] == '.') | (txtDate.Text[4] == ' ')) & Char.IsDigit(txtDate.Text[3]) & Char.IsDigit(txtDate.Text[5])
                         & Char.IsDigit(txtDate.Text[6]) & Char.IsDigit(txtDate.Text[7]) & Char.IsDigit(txtDate.Text[8]))
                    {
                        month = Int32.Parse($"{txtDate.Text[3]}");
                        year = Int32.Parse($"{txtDate.Text[5]}{txtDate.Text[6]}{txtDate.Text[7]}{txtDate.Text[8]}");
                    }
                }   
                if (((txtDate.Text[1] == '.') | (txtDate.Text[1] == ' ')) & Char.IsDigit(txtDate.Text[0])) 
                {
                    day = Int32.Parse($"{txtDate.Text[0]}");
                    if (((txtDate.Text[4] == '.') | (txtDate.Text[4] == ' ')) & Char.IsDigit(txtDate.Text[2]) & Char.IsDigit(txtDate.Text[3])
                        & Char.IsDigit(txtDate.Text[5]) & Char.IsDigit(txtDate.Text[6]) & Char.IsDigit(txtDate.Text[7]) & Char.IsDigit(txtDate.Text[8]))
                    {
                        month = Int32.Parse($"{txtDate.Text[2]}{txtDate.Text[3]}");
                        year = Int32.Parse($"{txtDate.Text[5]}{txtDate.Text[6]}{txtDate.Text[7]}{txtDate.Text[8]}");
                    }
                }
            }
            if (txtDate.Text.Length >= 10)
            {   
                if (((txtDate.Text[2] == '.') | (txtDate.Text[2] == ' ')) & Char.IsDigit(txtDate.Text[0]) & Char.IsDigit(txtDate.Text[1])) 
                {
                    day = Int32.Parse($"{txtDate.Text[0]}{txtDate.Text[1]}");
                    if (((txtDate.Text[5] == '.') | (txtDate.Text[5] == ' ')) & Char.IsDigit(txtDate.Text[3]) & Char.IsDigit(txtDate.Text[4]) 
                        & Char.IsDigit(txtDate.Text[6]) & Char.IsDigit(txtDate.Text[7]) & Char.IsDigit(txtDate.Text[8]) & Char.IsDigit(txtDate.Text[9]))
                    {
                        month = Int32.Parse($"{txtDate.Text[3]}{txtDate.Text[4]}");
                        year = Int32.Parse($"{txtDate.Text[6]}{txtDate.Text[7]}{txtDate.Text[8]}{txtDate.Text[9]}");
                    }
                }
            }
            catal.Length = catal.Length + 1;    // Расширение объекта массива для нового объекта
            catal.Add(catal.Length - 1, txtMark.Text, txtBodycar.Text, txtCountry.Text, month, day, year, price);   // Дополняем определённым объектом временный объект TCatalog
            Frm2.Catalog = catal;   // Переносим расширенный объект во вторую форму
            Frm2.ResetForm2();  // Обновляем форму для отображения изменений
            this.Close();   // Закрываем форму
        }
        // События для перетаскивания формы за основную панель
        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                md = true;  // Указываем нажатие на панель/форму
                ll = e.Location;    // Задаём текущее положение мыши
            }
        }
        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            if (md) // Пока кнопка мыши зажата 
            {   // Меняем позицию формы на любое отклонение текущей позиции формы от изначального расположения курсора на форме
                this.Location = new Point((this.Location.X - ll.X) + e.X, (this.Location.Y - ll.Y) + e.Y);
            }
        }
        private void Form3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                md = false; // Указываем разжатие панели/формы
            }
        }
        // События для кнопки закрытия формы
        private void CANCEL_MouseUp(object sender, MouseEventArgs e)
        {
            Pressing(CANCEL, '-');
            this.Close();   // Закрываем форму.... и открываем компоненты второй формы
        }
        // События опускания клавиши мыши с имитацией нажатия
        private void ADD_MouseDown(object sender, MouseEventArgs e)
        {
            Pressing(ADD, '+');
        }
        private void CANCEL_MouseDown(object sender, MouseEventArgs e)
        {
            Pressing(CANCEL, '+');
        }
        // События наводки и отводки курсора по отношению к тексту с миганием и антимиганием
        private void ADD_MouseHover(object sender, EventArgs e)
        {
            Blinking(ADD);
        }
        private void CANCEL_MouseHover(object sender, EventArgs e)
        {
            Blinking(CANCEL);
        }
        private void ADD_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(ADD);
        }
        private void CANCEL_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(CANCEL);
        }
    }
}
