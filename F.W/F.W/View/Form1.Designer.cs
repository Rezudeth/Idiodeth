
namespace NoCarsAllowed
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.START = new System.Windows.Forms.Label();
            this.INFO = new System.Windows.Forms.Label();
            this.EXIT = new System.Windows.Forms.Label();
            this.NEW = new System.Windows.Forms.Label();
            this.LANGUAGE = new System.Windows.Forms.Label();
            this.B1 = new System.Windows.Forms.Label();
            this.B2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // START
            // 
            this.START.AutoSize = true;
            this.START.BackColor = System.Drawing.Color.Black;
            this.START.Font = new System.Drawing.Font("Segoe UI Black", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.START.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.START.Location = new System.Drawing.Point(357, 492);
            this.START.Name = "START";
            this.START.Size = new System.Drawing.Size(147, 51);
            this.START.TabIndex = 0;
            this.START.Text = "START";
            this.START.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.START.MouseDown += new System.Windows.Forms.MouseEventHandler(this.START_MouseDown);
            this.START.MouseLeave += new System.EventHandler(this.START_MouseLeave);
            this.START.MouseHover += new System.EventHandler(this.START_MouseHover);
            this.START.MouseUp += new System.Windows.Forms.MouseEventHandler(this.START_MouseUp);
            // 
            // INFO
            // 
            this.INFO.AutoSize = true;
            this.INFO.BackColor = System.Drawing.Color.Black;
            this.INFO.Font = new System.Drawing.Font("Segoe UI Black", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.INFO.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.INFO.Location = new System.Drawing.Point(522, 492);
            this.INFO.Name = "INFO";
            this.INFO.Size = new System.Drawing.Size(116, 51);
            this.INFO.TabIndex = 1;
            this.INFO.Text = "INFO";
            this.INFO.MouseDown += new System.Windows.Forms.MouseEventHandler(this.INFO_MouseDown);
            this.INFO.MouseLeave += new System.EventHandler(this.INFO_MouseLeave);
            this.INFO.MouseHover += new System.EventHandler(this.INFO_MouseHover);
            this.INFO.MouseUp += new System.Windows.Forms.MouseEventHandler(this.INFO_MouseUp);
            // 
            // EXIT
            // 
            this.EXIT.AutoSize = true;
            this.EXIT.BackColor = System.Drawing.Color.Black;
            this.EXIT.Font = new System.Drawing.Font("Segoe UI Black", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EXIT.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EXIT.Location = new System.Drawing.Point(656, 492);
            this.EXIT.Name = "EXIT";
            this.EXIT.Size = new System.Drawing.Size(107, 51);
            this.EXIT.TabIndex = 2;
            this.EXIT.Text = "EXIT";
            this.EXIT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EXIT_MouseDown);
            this.EXIT.MouseLeave += new System.EventHandler(this.EXIT_MouseLeave);
            this.EXIT.MouseHover += new System.EventHandler(this.EXIT_MouseHover);
            this.EXIT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EXIT_MouseUp);
            // 
            // NEW
            // 
            this.NEW.AutoSize = true;
            this.NEW.BackColor = System.Drawing.Color.Black;
            this.NEW.Font = new System.Drawing.Font("Segoe UI Black", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NEW.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.NEW.Location = new System.Drawing.Point(620, 18);
            this.NEW.Name = "NEW";
            this.NEW.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NEW.Size = new System.Drawing.Size(143, 51);
            this.NEW.TabIndex = 3;
            this.NEW.Text = "CLEAR";
            this.NEW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NEW_MouseDown);
            this.NEW.MouseLeave += new System.EventHandler(this.NEW_MouseLeave);
            this.NEW.MouseHover += new System.EventHandler(this.NEW_MouseHover);
            this.NEW.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NEW_MouseUp);
            // 
            // LANGUAGE
            // 
            this.LANGUAGE.AutoSize = true;
            this.LANGUAGE.BackColor = System.Drawing.Color.Black;
            this.LANGUAGE.Font = new System.Drawing.Font("Segoe UI Black", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LANGUAGE.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LANGUAGE.Location = new System.Drawing.Point(12, 18);
            this.LANGUAGE.Name = "LANGUAGE";
            this.LANGUAGE.Size = new System.Drawing.Size(102, 51);
            this.LANGUAGE.TabIndex = 5;
            this.LANGUAGE.Text = "ENG";
            this.LANGUAGE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LANGUAGE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LANGUAGE_MouseDown);
            this.LANGUAGE.MouseLeave += new System.EventHandler(this.LANGUAGE_MouseLeave);
            this.LANGUAGE.MouseHover += new System.EventHandler(this.LANGUAGE_MouseHover);
            this.LANGUAGE.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LANGUAGE_MouseUp);
            // 
            // B1
            // 
            this.B1.AutoSize = true;
            this.B1.BackColor = System.Drawing.Color.Black;
            this.B1.Font = new System.Drawing.Font("Segoe UI Black", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.B1.Location = new System.Drawing.Point(628, 489);
            this.B1.Name = "B1";
            this.B1.Size = new System.Drawing.Size(37, 51);
            this.B1.TabIndex = 7;
            this.B1.Text = "|";
            // 
            // B2
            // 
            this.B2.AutoSize = true;
            this.B2.BackColor = System.Drawing.Color.Black;
            this.B2.Font = new System.Drawing.Font("Segoe UI Black", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.B2.Location = new System.Drawing.Point(494, 489);
            this.B2.Name = "B2";
            this.B2.Size = new System.Drawing.Size(37, 51);
            this.B2.TabIndex = 8;
            this.B2.Text = "|";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.B2);
            this.panel1.Controls.Add(this.B1);
            this.panel1.Controls.Add(this.LANGUAGE);
            this.panel1.Controls.Add(this.NEW);
            this.panel1.Controls.Add(this.EXIT);
            this.panel1.Controls.Add(this.INFO);
            this.panel1.Controls.Add(this.START);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MaximumSize = new System.Drawing.Size(784, 561);
            this.panel1.MinimumSize = new System.Drawing.Size(784, 561);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 561);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(88, 249);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(598, 79);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(0, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(784, 362);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_On_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "No Cars Allowed™";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label START;
        private System.Windows.Forms.Label INFO;
        private System.Windows.Forms.Label EXIT;
        private System.Windows.Forms.Label NEW;
        private System.Windows.Forms.Label LANGUAGE;
        private System.Windows.Forms.Label B1;
        private System.Windows.Forms.Label B2;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

