using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using static NoCarsAllowed.Form1;
using static NoCarsAllowed.Model.TCatalog;

namespace NoCarsAllowed.View
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
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
        // Метод для установки языка текстовых компонентов
        public void GetLanguage()
        {   // Считывание переменной языка из list.tхt и её использование для установки языка 0 - английский, 1 - русский
            Frm1.L = byte.Parse(Model.TCatalog.FindValue("language", 'i'));
            if (Frm1.L == 0)
            {
                LANGUAGE.Text = "ENG";
                LanguageChange(EXIT, "EXIT", 656, 492);
                LanguageChange(B1, "|", 628, 489);
                if (FromFrm == 1)
                {
                    LanguageChange(MAIN, "START", 343, 492);
                    LanguageChange(B2, "|", 479, 489);
                    LanguageChange(START, "MAIN", 507, 492);
                }
                if (FromFrm == 2)
                {
                    LanguageChange(START, "MAIN", 341, 492);
                    LanguageChange(B2, "|", 462, 489);
                    LanguageChange(MAIN, "START", 491, 492);
                }
                LanguageChange(Words, "Term paper on the subject \"Programming\"" +
                    "\n\nTopic: \"Developing of application with visual interface \"No Cars Allowed\"\"" +
                    "\n\nDeveloped by: student Elistratov D.N., group BIT20-11", 69, 225);
            }
            if (Frm1.L == 1)
            {
                LANGUAGE.Text = "RUS";
                LanguageChange(EXIT, "ВЫХОД", 590, 492);
                LanguageChange(B1, "|", 562, 489);
                if (FromFrm == 1)
                {
                    LanguageChange(MAIN, "ГЛАВНАЯ", 194, 492);
                    LanguageChange(B2, "|", 396, 489);
                    LanguageChange(START, "СТАРТ", 425, 492);
                }
                if (FromFrm == 2)
                {
                    LanguageChange(START, "СТАРТ", 195, 492);
                    LanguageChange(B2, "|", 332, 489);
                    LanguageChange(MAIN, "ГЛАВНАЯ", 361, 492);
                }
                LanguageChange(Words, "Курсовая работа по дисциплине \"Программирование\"" +
                    "\n\nТема: \"Разработка приложения с графическим интерфейсом \"No Cars Allowed\"\"" +
                    "\n\nРазработал: студент группы БИТ20 - 11 Елистратов Д.Н.", 39, 225);
            }
        }
        private void pictureBox1_On_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Frm1.image, new Point(Frm1.x - 785, Frm1.y - 360));  // Для нижнего компонента используется смещение к нижней части изображения  
        }
        private void pictureBox2_On_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Frm1.image, new Point(Frm1.x - 1462, Frm1.y - 360));
        }
        private  void START_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ConsoleInv = 1;
                Frm1.ConsoleMove(); // Повторный вызов движения фона после изменения формы
                Frm1.ChangeForm(Frm1, this);
            }
        }
        private void MAIN_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ConsoleInv = 2;
                Frm2.GetLanguage(); // Установка языка второй формы
                Frm1.ConsoleMove();
                Frm1.ChangeForm(Frm2, this);
            }
        }

        private void EXIT_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                    Frm1.Close();
            }
        }
        private void LANGUAGE_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
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

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Frm1.Close();
            e.Cancel = true;
        }
        private void MAIN_MouseHover(object sender, EventArgs e)
        {
            Blinking(MAIN);
        }

        private void MAIN_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(MAIN);
        }

        private void START_MouseHover(object sender, EventArgs e)
        {
            Blinking(START);
        }

        private void START_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(START);
        }

        private void EXIT_MouseHover(object sender, EventArgs e)
        {
            Blinking(EXIT);
        }

        private void EXIT_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(EXIT);
        }

        private void LANGUAGE_MouseHover(object sender, EventArgs e)
        {
            Blinking(LANGUAGE);
        }

        private void LANGUAGE_MouseLeave(object sender, EventArgs e)
        {
            UnBlinking(LANGUAGE);
        }
    }
}
