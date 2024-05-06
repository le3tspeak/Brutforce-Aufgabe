namespace Brutforce;

partial class BruteForce
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
        btn1 = new Button();
        tbpassword = new TextBox();
        SuspendLayout();
        // 
        // btn1
        // 
        btn1.Location = new Point(405, 12);
        btn1.Name = "btn1";
        btn1.Size = new Size(75, 23);
        btn1.TabIndex = 0;
        btn1.Text = "Start";
        btn1.UseVisualStyleBackColor = true;
        btn1.Click += btn1_Click;
        // 
        // tbpassword
        // 
        tbpassword.Location = new Point(12, 12);
        tbpassword.Name = "tbpassword";
        tbpassword.PlaceholderText = "Passwort hier eingeben";
        tbpassword.Size = new Size(385, 23);
        tbpassword.TabIndex = 1;
        // 
        // BruteForce
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Black;
        ClientSize = new Size(492, 48);
        Controls.Add(tbpassword);
        Controls.Add(btn1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "BruteForce";
        Text = "BruteForce Tool";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button btn1;
    private TextBox tbpassword;
}
