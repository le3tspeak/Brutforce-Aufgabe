namespace Brutforce;

public partial class BruteForce : Form
{
    private bool isBruteforceRunning = false;
    private readonly string[] runningTitles = { "Suche Passwort", "Suche Passwort.", "Suche Passwort..", "Suche Passwort..." };

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

    private async void btn1_Click(object sender, EventArgs e)
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

        // Wenn Keine Fehler aufgetreten sind starte das Bruteforce in einem separaten Task
        tbpassword.Enabled = false;
        btn1.Enabled = false;
        isBruteforceRunning = true;
        Text = "Suche Passwort";

        // Ändere die Überschrift in der Top-Bar
        UpdateRunningTitle();

        // Starte den Bruteforce-Prozess in einem separaten Task
        await Task.Run(async () =>
        {
            await Bruteforce(password);
        });

        // Nach Abschluss des Bruteforce-Prozesses den Lauftext entfernen
        Invoke(new Action(() =>
        {
            Text = "BruteForce Tool";
            tbpassword.Enabled = true;
            btn1.Enabled = true;
        }));
    }

    // Lauftext aktualisieren (wird in einem eigenen Task ausgeführt)
    private async void UpdateRunningTitle()
    {
        while (isBruteforceRunning)
        {
            for (var i = 0; i < runningTitles.Length; i++)
            {
                // Zeige den Lauftext für 500ms an und aktualisiere ihn dann
                await Task.Delay(500);
                Invoke(new Action(() =>
                {
                    Text = runningTitles[i];
                }));
            }
        }
    }

    // Bruteforce
    private async Task Bruteforce(string password)
    {
        // Starte den Timer
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        // Variablen initialisieren
        ulong attempts = 0;
        var found = false;
        var chars = Enumerable.Range(32, 95).Select(i => (char)i).ToArray(); // ASCII-Zeichen von 32 bis 126 (inklusive) entsprechen den druckbaren Zeichen

        // Bruteforce-Loop (Parallel.For) 
        await Task.Run(() =>
        {
            // Parallel.For durchläuft alle möglichen Kombinationen
            // Parallel.For bricht ab, sobald das Passwort gefunden wurde
            // Begrenze Parrallel.For auf Max 10 Threads
            ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = 10 };
            Parallel.For(1, int.MaxValue, options, (length, loopState) =>
            {
                if (found)
                {
                    loopState.Break();
                    return;
                }

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
                        loopState.Break();
                        break;
                    }
                }
            });
        });

        // Timer stoppen
        stopwatch.Stop();

        // UI aktualisieren und Nachricht anzeigen
        string message;
        if (found)
        {
            message = $"Das Passwort wurde gefunden! \n" +
                      $"\nPasswort: {password} " +
                      $"\n\nVersuche: {attempts} " +
                      $"\nZeit: {stopwatch.ElapsedMilliseconds}ms ";
        }
        else
        {
            message = $"Das Passwort konnte nicht gefunden werden.";
        }

        // UI aktualisieren und Nachricht anzeigen
        Invoke(new Action(() =>
        {
            isBruteforceRunning = false;
            MessageBox.Show(message, found ? "Passwort gefunden" : "Passwort nicht gefunden", MessageBoxButtons.OK, found ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }));
    }
}
