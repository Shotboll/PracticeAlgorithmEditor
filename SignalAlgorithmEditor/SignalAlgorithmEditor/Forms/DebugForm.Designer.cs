namespace SignalAlgorithmEditor.Forms
{
    partial class DebugForm
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
            dataGridViewParams = new DataGridView();
            buttonEvaluate = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewParams).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewParams
            // 
            dataGridViewParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewParams.Location = new Point(12, 12);
            dataGridViewParams.Name = "dataGridViewParams";
            dataGridViewParams.RowHeadersVisible = false;
            dataGridViewParams.Size = new Size(357, 426);
            dataGridViewParams.TabIndex = 1;
            // 
            // buttonEvaluate
            // 
            buttonEvaluate.Location = new Point(144, 444);
            buttonEvaluate.Name = "buttonEvaluate";
            buttonEvaluate.Size = new Size(90, 23);
            buttonEvaluate.TabIndex = 2;
            buttonEvaluate.Text = "Вычислить";
            buttonEvaluate.UseVisualStyleBackColor = true;
            buttonEvaluate.Click += buttonEvaluate_Click;
            // 
            // DebugForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(381, 478);
            Controls.Add(buttonEvaluate);
            Controls.Add(dataGridViewParams);
            Name = "DebugForm";
            Text = "Отладка";
            Load += DebugForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewParams).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridViewParams;
        private Button buttonEvaluate;
    }
}