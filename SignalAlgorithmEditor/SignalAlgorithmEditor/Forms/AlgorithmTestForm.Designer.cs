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
            fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)fastColoredTextBox1).BeginInit();
            SuspendLayout();
            // 
            // fastColoredTextBox1
            // 
            fastColoredTextBox1.AutoCompleteBracketsList = new char[]
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
            fastColoredTextBox1.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            fastColoredTextBox1.AutoScrollMinSize = new Size(179, 14);
            fastColoredTextBox1.BackBrush = null;
            fastColoredTextBox1.CharHeight = 14;
            fastColoredTextBox1.CharWidth = 8;
            fastColoredTextBox1.DefaultMarkerSize = 8;
            fastColoredTextBox1.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            fastColoredTextBox1.Font = new Font("Courier New", 9.75F);
            fastColoredTextBox1.Hotkeys = resources.GetString("fastColoredTextBox1.Hotkeys");
            fastColoredTextBox1.IsReplaceMode = false;
            fastColoredTextBox1.Location = new Point(159, 86);
            fastColoredTextBox1.Name = "fastColoredTextBox1";
            fastColoredTextBox1.Paddings = new Padding(0);
            fastColoredTextBox1.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            fastColoredTextBox1.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("fastColoredTextBox1.ServiceColors");
            fastColoredTextBox1.Size = new Size(373, 33);
            fastColoredTextBox1.TabIndex = 0;
            fastColoredTextBox1.Text = "fastColoredTextBox1";
            fastColoredTextBox1.Zoom = 100;
            // 
            // AlgorithmTestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(fastColoredTextBox1);
            Name = "AlgorithmTestForm";
            Text = "AlgorithmTestForm";
            ((System.ComponentModel.ISupportInitialize)fastColoredTextBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
    }
}