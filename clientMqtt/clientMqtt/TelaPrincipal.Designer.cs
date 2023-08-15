
namespace clientMqtt
{
    partial class TelaPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            conectar_button = new System.Windows.Forms.Button();
            topicoPublicar_textBox = new System.Windows.Forms.TextBox();
            messagem_textBox = new System.Windows.Forms.TextBox();
            send_button = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            topicoInscricao_textBox = new System.Windows.Forms.TextBox();
            messagemInscricao_richTextBox = new System.Windows.Forms.RichTextBox();
            inscrever_button = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // conectar_button
            // 
            conectar_button.Location = new System.Drawing.Point(10, 347);
            conectar_button.Name = "conectar_button";
            conectar_button.Size = new System.Drawing.Size(75, 23);
            conectar_button.TabIndex = 0;
            conectar_button.Text = "iniciar";
            conectar_button.UseVisualStyleBackColor = true;
            conectar_button.Click += button1_Click;
            // 
            // topicoPublicar_textBox
            // 
            topicoPublicar_textBox.Location = new System.Drawing.Point(211, 57);
            topicoPublicar_textBox.Name = "topicoPublicar_textBox";
            topicoPublicar_textBox.Size = new System.Drawing.Size(100, 23);
            topicoPublicar_textBox.TabIndex = 1;
            // 
            // messagem_textBox
            // 
            messagem_textBox.Location = new System.Drawing.Point(211, 98);
            messagem_textBox.Name = "messagem_textBox";
            messagem_textBox.Size = new System.Drawing.Size(100, 23);
            messagem_textBox.TabIndex = 2;
            // 
            // send_button
            // 
            send_button.Location = new System.Drawing.Point(211, 142);
            send_button.Name = "send_button";
            send_button.Size = new System.Drawing.Size(75, 23);
            send_button.TabIndex = 3;
            send_button.Text = "enviar dados";
            send_button.UseVisualStyleBackColor = true;
            send_button.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(144, 60);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 15);
            label1.TabIndex = 4;
            label1.Text = "topico";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(114, 104);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 15);
            label2.TabIndex = 5;
            label2.Text = "mensagerm";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(422, 55);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(41, 15);
            label3.TabIndex = 7;
            label3.Text = "topico";
            // 
            // topicoInscricao_textBox
            // 
            topicoInscricao_textBox.Location = new System.Drawing.Point(489, 52);
            topicoInscricao_textBox.Name = "topicoInscricao_textBox";
            topicoInscricao_textBox.Size = new System.Drawing.Size(100, 23);
            topicoInscricao_textBox.TabIndex = 6;
            // 
            // messagemInscricao_richTextBox
            // 
            messagemInscricao_richTextBox.Location = new System.Drawing.Point(422, 98);
            messagemInscricao_richTextBox.Name = "messagemInscricao_richTextBox";
            messagemInscricao_richTextBox.Size = new System.Drawing.Size(253, 196);
            messagemInscricao_richTextBox.TabIndex = 8;
            messagemInscricao_richTextBox.Text = "";
            // 
            // inscrever_button
            // 
            inscrever_button.Location = new System.Drawing.Point(489, 347);
            inscrever_button.Name = "inscrever_button";
            inscrever_button.Size = new System.Drawing.Size(75, 23);
            inscrever_button.TabIndex = 9;
            inscrever_button.Text = "subscript";
            inscrever_button.UseVisualStyleBackColor = true;
            inscrever_button.Click += button3_Click;
            // 
            // TelaPrincipal
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(inscrever_button);
            Controls.Add(messagemInscricao_richTextBox);
            Controls.Add(label3);
            Controls.Add(topicoInscricao_textBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(send_button);
            Controls.Add(messagem_textBox);
            Controls.Add(topicoPublicar_textBox);
            Controls.Add(conectar_button);
            Name = "TelaPrincipal";
            Text = "Cliente Mqtt";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button conectar_button;
        private System.Windows.Forms.TextBox topicoPublicar_textBox;
        private System.Windows.Forms.TextBox messagem_textBox;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox topicoInscricao_textBox;
        private System.Windows.Forms.RichTextBox messagemInscricao_richTextBox;
        private System.Windows.Forms.Button inscrever_button;
    }
}

