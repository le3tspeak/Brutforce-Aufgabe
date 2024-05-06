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
                Invoke(new Action(() => { Text = titles[index]; }));
                index = (index + 1) % titles.Length;
                Thread.Sleep(500);
            }
        });
        titleThread.IsBackground = true;
        titleThread.Start();
    }

    // Bruteforce
    private void Bruteforce(string password)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        ulong attempts = 0;
        var found = false;
        var chars = Enumerable.Range(32, 95).Select(i => (char)i).ToArray(); // ASCII-Zeichen von 32 bis 126 (inklusive) entsprechen den druckbaren Zeichen
        var length = 1;

        while (!found && attempts < MaxAttempts) // Begrenze die Anzahl der Versuche
        {
            var combinations = (ulong)Math.Pow(chars.Length, length);
            for (ulong i = 0; i < combinations; i++)
            {
                var guess = "";
                var j = i;
                for (var k = 0; k < length; k++)
                {
                    guess += chars[(int)(j % (ulong)chars.Length)];
                    j /= (ulong)chars.Length;
                }
                attempts++;
                if (guess == password)
                {
                    found = true;
                    break;
                }
            }
            length++;
        }

        stopwatch.Stop();

        // UI aktualisieren und Nachricht anzeigen
        string message;
        if (found)
        {
            message = $"Das Passwort wurde gefunden! \n" +
                      $"\nPasswort: {password} " +
                      $"\n\nVersuche: {attempts} " +
                      $"\nZeit: {stopwatch.ElapsedMilliseconds}ms " +
                      $"\nVersuche pro Sekunde: {(((double)attempts / stopwatch.ElapsedMilliseconds) * 1000):0.#} Tsd.";
        }
        else
        {
            message = $"Das Passwort konnte nicht gefunden werden.";
        }

        // BeginInvoke verwenden, um die UI im UI-Thread zu aktualisieren
        BeginInvoke((MethodInvoker)delegate
        {
            MessageBox.Show(message, found ? "Passwort gefunden" : "Passwort nicht gefunden", MessageBoxButtons.OK, found ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            tbpassword.Enabled = true;
            btn1.Enabled = true;
            this.Text = "BruteForce Tool";
        });
    }
}


