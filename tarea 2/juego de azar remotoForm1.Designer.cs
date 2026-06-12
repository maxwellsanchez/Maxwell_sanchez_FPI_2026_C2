namespace juego_de_azar
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textJ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textG = new System.Windows.Forms.TextBox();
            this.apuesta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textL = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(738, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 64);
            this.button1.TabIndex = 0;
            this.button1.Text = "jugar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textJ
            // 
            this.textJ.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textJ.Location = new System.Drawing.Point(756, 287);
            this.textJ.Name = "textJ";
            this.textJ.Size = new System.Drawing.Size(100, 38);
            this.textJ.TabIndex = 1;
            this.textJ.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(621, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Numero de juego -->";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textG
            // 
            this.textG.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textG.ForeColor = System.Drawing.Color.Red;
            this.textG.Location = new System.Drawing.Point(12, 23);
            this.textG.Name = "textG";
            this.textG.Size = new System.Drawing.Size(911, 41);
            this.textG.TabIndex = 3;
            this.textG.TextChanged += new System.EventHandler(this.textG_TextChanged);
            // 
            // apuesta
            // 
            this.apuesta.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apuesta.Location = new System.Drawing.Point(756, 243);
            this.apuesta.Name = "apuesta";
            this.apuesta.Size = new System.Drawing.Size(100, 38);
            this.apuesta.TabIndex = 4;
            this.apuesta.TextChanged += new System.EventHandler(this.apuesta_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(611, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cantidad a apostar -->";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textL
            // 
            this.textL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textL.Location = new System.Drawing.Point(12, 129);
            this.textL.Name = "textL";
            this.textL.Size = new System.Drawing.Size(571, 22);
            this.textL.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 525);
            this.Controls.Add(this.textL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.apuesta);
            this.Controls.Add(this.textG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textJ);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textJ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textG;
        private System.Windows.Forms.TextBox apuesta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textL;
    }
}

