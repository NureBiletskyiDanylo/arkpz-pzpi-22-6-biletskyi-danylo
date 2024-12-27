using System.Diagnostics;

namespace MediStoS.Services;

public class BackupService
{
    private readonly string _connectionString;

    public BackupService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CreateBackup(string backupFilePath)
    {
        var builder = new Npgsql.NpgsqlConnectionStringBuilder(_connectionString);

        string host = builder.Host;
        string port = builder.Port.ToString();
        string dbName = builder.Database;
        string username = builder.Username;
        string password = builder.Password;
        Environment.SetEnvironmentVariable("PGPASSWORD", password);
        string pgDumpPath = @"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe";
        string arguments = $"-U {username} -h {host} -p {port} {dbName} -f \"{backupFilePath}\"";
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = pgDumpPath,
                Arguments = arguments,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        try
        {
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                string error = process.StandardError.ReadToEnd();
                throw new Exception($"Backup failed: {error}");
            }
        }
        finally
        {
            Environment.SetEnvironmentVariable("PGPASSWORD", null);
        }
    }
}
