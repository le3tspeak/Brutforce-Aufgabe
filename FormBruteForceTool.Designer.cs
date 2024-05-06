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
        lblAktuellerVersuch = new Label();
        lblVersucheProSekunde = new Label();
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
        // lblAktuellerVersuch
        // 
        lblAktuellerVersuch.AutoSize = true;
        lblAktuellerVersuch.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblAktuellerVersuch.ForeColor = Color.White;
        lblAktuellerVersuch.Location = new Point(77, 46);
        lblAktuellerVersuch.Name = "lblAktuellerVersuch";
        lblAktuellerVersuch.Size = new Size(116, 17);
        lblAktuellerVersuch.TabIndex = 4;
        lblAktuellerVersuch.Text = "Aktueller Versuch:";
        // 
        // lblVersucheProSekunde
        // 
        lblVersucheProSekunde.AutoSize = true;
        lblVersucheProSekunde.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblVersucheProSekunde.ForeColor = Color.White;
        lblVersucheProSekunde.Location = new Point(47, 71);
        lblVersucheProSekunde.Name = "lblVersucheProSekunde";
        lblVersucheProSekunde.Size = new Size(146, 17);
        lblVersucheProSekunde.TabIndex = 6;
        lblVersucheProSekunde.Text = "Versuche pro Sekunde:";
        // 
        // BruteForce
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Black;
        ClientSize = new Size(492, 103);
        Controls.Add(lblVersucheProSekunde);
        Controls.Add(lblAktuellerVersuch);
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
    private Label lblAktuellerVersuch;
    private Label lblVersucheProSekunde;
}
