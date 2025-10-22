namespace SignalAlgorithmEditor.Forms
{
    partial class AlgorithmTestForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlgorithmTestForm));
            fastColoredTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            buttonDebug = new Button();
            buttonSave = new Button();
            ((System.ComponentModel.ISupportInitialize)fastColoredTextBox).BeginInit();
            SuspendLayout();
            // 
            // fastColoredTextBox
            // 
            fastColoredTextBox.AutoCompleteBracketsList = new char[]
    {
    '(',
    ')',
    '{',
    '}',
    '[',
    ']',
    '"',
    '"',
    '\'',
    '\''
    };
            fastColoredTextBox.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            fastColoredTextBox.AutoScrollMinSize = new Size(0, 18);
            fastColoredTextBox.BackBrush = null;
            fastColoredTextBox.BorderStyle = BorderStyle.FixedSingle;
            fastColoredTextBox.CharHeight = 18;
            fastColoredTextBox.CharWidth = 9;
            fastColoredTextBox.DefaultMarkerSize = 8;
            fastColoredTextBox.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            fastColoredTextBox.Font = new Font("Cascadia Code", 12F);
            fastColoredTextBox.IsReplaceMode = false;
            fastColoredTextBox.Location = new Point(12, 27);
            fastColoredTextBox.Name = "fastColoredTextBox";
            fastColoredTextBox.Paddings = new Padding(0);
            fastColoredTextBox.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            fastColoredTextBox.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("fastColoredTextBox.ServiceColors");
            fastColoredTextBox.ShowFoldingLines = true;
            fastColoredTextBox.ShowLineNumbers = false;
            fastColoredTextBox.Size = new Size(776, 65);
            fastColoredTextBox.TabIndex = 0;
            fastColoredTextBox.Text = "fastColoredTextBox";
            fastColoredTextBox.WordWrap = true;
            fastColoredTextBox.Zoom = 100;
            // 
            // buttonDebug
            // 
            buttonDebug.Location = new Point(12, 98);
            buttonDebug.Name = "buttonDebug";
            buttonDebug.Size = new Size(110, 28);
            buttonDebug.TabIndex = 1;
            buttonDebug.Text = "Отладка";
            buttonDebug.UseVisualStyleBackColor = true;
            buttonDebug.Click += buttonDebug_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(128, 98);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(196, 28);
            buttonSave.TabIndex = 2;
            buttonSave.Text = "Сохранить изменения в БД";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // AlgorithmTestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 145);
            Controls.Add(buttonSave);
            Controls.Add(buttonDebug);
            Controls.Add(fastColoredTextBox);
            Name = "AlgorithmTestForm";
            Text = "AlgorithmTestForm";
            ((System.ComponentModel.ISupportInitialize)fastColoredTextBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox;
        private Button buttonDebug;
        private Button buttonSave;
    }
}