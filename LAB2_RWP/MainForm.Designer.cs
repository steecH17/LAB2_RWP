namespace LAB2_RWP
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonWind = new System.Windows.Forms.Button();
            this.buttonRain = new System.Windows.Forms.Button();
            this.buttonGraphOffset = new System.Windows.Forms.Button();
            this.buttonColorGraph = new System.Windows.Forms.Button();
            this.buttonBackgroundStyle = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonRandomFunction = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timerRain = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonWind);
            this.groupBox1.Controls.Add(this.buttonRain);
            this.groupBox1.Controls.Add(this.buttonGraphOffset);
            this.groupBox1.Controls.Add(this.buttonColorGraph);
            this.groupBox1.Controls.Add(this.buttonBackgroundStyle);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.buttonRandomFunction);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(600, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tools";
            // 
            // buttonWind
            // 
            this.buttonWind.Location = new System.Drawing.Point(3, 295);
            this.buttonWind.Name = "buttonWind";
            this.buttonWind.Size = new System.Drawing.Size(50, 52);
            this.buttonWind.TabIndex = 6;
            this.buttonWind.UseVisualStyleBackColor = true;
            this.buttonWind.Click += new System.EventHandler(this.buttonWind_Click);
            // 
            // buttonRain
            // 
            this.buttonRain.Location = new System.Drawing.Point(3, 379);
            this.buttonRain.Name = "buttonRain";
            this.buttonRain.Size = new System.Drawing.Size(50, 49);
            this.buttonRain.TabIndex = 5;
            this.buttonRain.UseVisualStyleBackColor = true;
            this.buttonRain.Click += new System.EventHandler(this.buttonRain_Click);
            // 
            // buttonGraphOffset
            // 
            this.buttonGraphOffset.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonGraphOffset.Location = new System.Drawing.Point(72, 376);
            this.buttonGraphOffset.Name = "buttonGraphOffset";
            this.buttonGraphOffset.Size = new System.Drawing.Size(107, 51);
            this.buttonGraphOffset.TabIndex = 4;
            this.buttonGraphOffset.Text = "Перемещение функции";
            this.buttonGraphOffset.UseVisualStyleBackColor = false;
            this.buttonGraphOffset.Click += new System.EventHandler(this.buttonGraphOffset_Click);
            // 
            // buttonColorGraph
            // 
            this.buttonColorGraph.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonColorGraph.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonColorGraph.Location = new System.Drawing.Point(72, 197);
            this.buttonColorGraph.Name = "buttonColorGraph";
            this.buttonColorGraph.Size = new System.Drawing.Size(107, 54);
            this.buttonColorGraph.TabIndex = 3;
            this.buttonColorGraph.Text = "Цвет функции";
            this.buttonColorGraph.UseVisualStyleBackColor = false;
            this.buttonColorGraph.Click += new System.EventHandler(this.buttonColorGraph_Click);
            // 
            // buttonBackgroundStyle
            // 
            this.buttonBackgroundStyle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonBackgroundStyle.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBackgroundStyle.Location = new System.Drawing.Point(72, 108);
            this.buttonBackgroundStyle.Name = "buttonBackgroundStyle";
            this.buttonBackgroundStyle.Size = new System.Drawing.Size(107, 49);
            this.buttonBackgroundStyle.TabIndex = 2;
            this.buttonBackgroundStyle.Text = "Фон";
            this.buttonBackgroundStyle.UseVisualStyleBackColor = false;
            this.buttonBackgroundStyle.Click += new System.EventHandler(this.buttonBackgroundStyle_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonSave.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSave.Location = new System.Drawing.Point(72, 294);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(107, 52);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonRandomFunction
            // 
            this.buttonRandomFunction.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonRandomFunction.Font = new System.Drawing.Font("Arial Narrow", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRandomFunction.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonRandomFunction.Location = new System.Drawing.Point(72, 17);
            this.buttonRandomFunction.Name = "buttonRandomFunction";
            this.buttonRandomFunction.Size = new System.Drawing.Size(107, 58);
            this.buttonRandomFunction.TabIndex = 0;
            this.buttonRandomFunction.Text = "Рандомная функция";
            this.buttonRandomFunction.UseVisualStyleBackColor = false;
            this.buttonRandomFunction.Click += new System.EventHandler(this.buttonRandomFunction_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 450);
            this.panel1.TabIndex = 1;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // timerRain
            // 
            this.timerRain.Tick += new System.EventHandler(this.timerRain_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Graphics";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBackgroundStyle;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonRandomFunction;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonColorGraph;
        private System.Windows.Forms.Button buttonGraphOffset;
        private System.Windows.Forms.Timer timerRain;
        private System.Windows.Forms.Button buttonRain;
        private System.Windows.Forms.Button buttonWind;
    }
}

