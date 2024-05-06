using System.Diagnostics;

namespace Brutforce;

public partial class BruteForce : Form
{
    private const ulong MaxAttempts = 1000000000;
    private bool isBruteforceRunning = false;

    public BruteForce()
    {
        InitializeComponent();
        Startup();
    }

    private void Startup()
    {
        ControlBox = true;
        DoubleBuffered = true;
        MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
    }

    private void btn1_Click(object sender, EventArgs e)
    {
        // Lade das PW aus dem Textfeld
        var password = tbpassword.Text;

        // Prüfe ob das PW leer
        if (string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Bitte geben Sie ein Passwort ein!");
            return;
        }
        // Prüfe ob das PW Leerzeichen enthält
        if (password.Contains(" "))
        {
            MessageBox.Show("Das Passwort darf keine Leerzeichen enthalten!");
            return;
        }

        // Prüfe ob das Passwort nur "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" enthält
        if (password.Any(c => !"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".Contains(c)))
        {
            MessageBox.Show("Das Passwort darf nur aus den Zeichen 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789' bestehen!");
            return;
        }

        // Wenn Keine Fehler aufgetreten sind starte das Bruteforce in einem separaten Thread
        tbpassword.Enabled = false;
        btn1.Enabled = false;
        isBruteforceRunning = true;
        Thread bruteforceThread = new Thread(() => Bruteforce(password));
        bruteforceThread.IsBackground = true;
        bruteforceThread.Start();

        // Ändere die Überschrift in der Top-Bar
        Thread titleThread = new Thread(() =>
        {
            string[] titles = { "Running", "Running.", "Running..", "Running..." };
            int index = 0;
            while (isBruteforceRunning)
            {
                this.Invoke(new Action(() => { this.Text = titles[index]; }));
                index = (index + 1) % titles.Length;
                Thread.Sleep(500);
            }
        });
        titleThread.IsBackground = true;
        titleThread.Start();
    }

    private void SetControlProperties(Control control, Action action)
    {
        if (control.InvokeRequired)
        {
            control.Invoke(action);
        }
        else
        {
            action();
        }
    }

    // Bruteforce
    private void Bruteforce(string password)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        ulong attempts = 0;
        var found = false;
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        var length = 1;

        while (!found && attempts < MaxAttempts) // Begrenze die Anzahl der Versuche
        {
            var combinations = Math.Pow(chars.Length, length);
            for (var i = 0; i < combinations; i++)
            {
                var guess = "";
                var j = i;
                for (var k = 0; k < length; k++)
                {
                    guess += chars[j % chars.Length];
                    j /= chars.Length;

                }
                attempts++;
                if (guess == password)
                {
                    found = true;
                    break;
                }
            }
            length++;

            // Aktualisiere die Benutzeroberfläche in einem separaten Thread
            SetControlProperties(this, () =>
            {
                lblAktuellerVersuch.Text = $"Aktueller Versuch: {password}";
                lblVersucheProSekunde.Text = $"Versuche pro Sekunde: {(((double)attempts / stopwatch.ElapsedMilliseconds) * 1000):0.##} Tsd.";
            });
        }

        stopwatch.Stop();

        if (found)
        {
            MessageBox.Show($"Das Passwort wurde gefunden! \n" +
                $"\nPasswort: {password} " +
                $"\n\nVersuche: {attempts} " +
                $"\nZeit: {stopwatch.ElapsedMilliseconds}ms " +
                $"\nVersuche pro Sekunde: {(((double)attempts / stopwatch.ElapsedMilliseconds) * 1000):0.##} Tsd.",
                "Passwort gefunden", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetControlProperties(this, () =>
            {
                tbpassword.Enabled = true;
                btn1.Enabled = true;
            });
        }
        else
        {
            MessageBox.Show($"Das Passwort konnte nicht gefunden werden.");
            SetControlProperties(this, () =>
            {
                tbpassword.Enabled = true;
                btn1.Enabled = true;
            });
        }

        // Setze die Überschrift wieder auf "BruteForce Tool" nach Abschluss des Bruteforce-Prozesses
        isBruteforceRunning = false;
        SetControlProperties(this, () => { this.Text = "BruteForce Tool"; });
    }
}
