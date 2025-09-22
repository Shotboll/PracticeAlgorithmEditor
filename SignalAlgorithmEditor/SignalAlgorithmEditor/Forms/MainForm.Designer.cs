namespace SignalAlgorithmEditor
{
    partial class MainForm
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
            buttonLoadSignals = new Button();
            dataGridView = new DataGridView();
            buttonTestAlgorithm = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // buttonLoadSignals
            // 
            buttonLoadSignals.Location = new Point(12, 12);
            buttonLoadSignals.Name = "buttonLoadSignals";
            buttonLoadSignals.Size = new Size(144, 23);
            buttonLoadSignals.TabIndex = 0;
            buttonLoadSignals.Text = "Загрузить алгоритмы";
            buttonLoadSignals.UseVisualStyleBackColor = true;
            buttonLoadSignals.Click += buttonLoadSignals_Click;
            // 
            // dataGridView
            // 
            dataGridView.BackgroundColor = Color.White;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 41);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(671, 337);
            dataGridView.TabIndex = 1;
            // 
            // buttonTestAlgorithm
            // 
            buttonTestAlgorithm.Location = new Point(162, 12);
            buttonTestAlgorithm.Name = "buttonTestAlgorithm";
            buttonTestAlgorithm.Size = new Size(132, 23);
            buttonTestAlgorithm.TabIndex = 2;
            buttonTestAlgorithm.Text = "Проверить алгоритм";
            buttonTestAlgorithm.UseVisualStyleBackColor = true;
            buttonTestAlgorithm.Click += buttonTestAlgorithm_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(695, 389);
            Controls.Add(buttonTestAlgorithm);
            Controls.Add(dataGridView);
            Controls.Add(buttonLoadSignals);
            Name = "MainForm";
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonLoadSignals;
        private DataGridView dataGridView;
        private Button buttonTestAlgorithm;
    }
}
