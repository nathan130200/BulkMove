#pragma warning disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkMove
{
    public partial class OperationForm : Form
    {
        private string sourcePath;
        private string destinationPath;
        private List<BulkMoveRequest> requests;

        private StreamWriter logFile;
        private Stopwatch gWatch;

        string GetDestinationFileName(string sourceFileName)
        {
            if (sourceFileName.IndexOf(sourcePath) != -1)
            {
                sourceFileName = sourceFileName.Replace(sourcePath, string.Empty);

                if (sourceFileName.StartsWith("/") || sourceFileName.StartsWith("\\"))
                    sourceFileName = sourceFileName.Substring(1);
            }

            // ex: Path.Combine("X:\", "Test.txt") -> "X:\Test.txt"
            return Path.Combine(this.destinationPath, sourceFileName);
        }

        void LogToFile(string message)
        {
            try
            {
                lock (this.logFile)
                {
                    logFile.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
                    logFile.Flush();
                }
            }
            catch { Debug.WriteLine(message); }
        }

        protected OperationForm()
        {
            InitializeComponent();
        }

        Thread ScanThread;
        Thread MoveThread;

        public OperationForm(string sourcePath, string destinationPath) : this()
        {
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;

            if (this.sourcePath.EndsWith("\\") || this.sourcePath.EndsWith("/"))
                this.sourcePath = this.sourcePath.Substring(0, this.sourcePath.Length - 1);

            if (this.destinationPath.EndsWith("\\") || this.destinationPath.EndsWith("/"))
                this.sourcePath = this.destinationPath.Substring(0, this.destinationPath.Length - 1);

            if (!Directory.Exists(this.destinationPath))
                Directory.CreateDirectory(this.destinationPath);

            this.logFile = new StreamWriter(Path.Combine(this.destinationPath, $"BulkMove-{DateTime.Now:dd-MM-yyyy_HH-mm-ss}.log"));
            this.requests = new List<BulkMoveRequest>();
        }

        private void OperationForm_Load(object sender, EventArgs e)
        {
            gWatch = Stopwatch.StartNew();

            ScanThread = new Thread(() => this.StartMoveAsync())
            {
                IsBackground = true,
                Name = "BulkMove-WorkingThread-1"
            };

            ScanThread.Start();
        }

        protected void StartMoveAsync()
        {
            LogToFile("====================== PESQUISANDO ARQUIVOS ======================");
            LogToFile(string.Empty);

            this.ScanFiles(this.sourcePath);

            MoveThread = new Thread(() => this.MoveFiles())
            {
                IsBackground = true,
                Name = "BulkMove-WorkingThread-2"
            };

            MoveThread.Start();
        }

        bool IsEmpty
        {
            get
            {
                lock (this.requests)
                    return this.requests.Count == 0;
            }
        }

        BulkMoveRequest PopFirstItem()
        {
            if (this.IsEmpty)
                return null;

            lock (this.requests)
            {
                var item = this.requests[0];
                this.requests.RemoveAt(0);
                return item;
            }
        }

        void ScanFiles(string path)
        {
            var files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);

            LogToFile($" ################################ {path} ################################");
            LogToFile(string.Empty);

            foreach (var file in files)
            {
                var request = new BulkMoveRequest();
                request.Id = Guid.NewGuid().ToString();
                request.CurrentDirectory = path;
                request.From = file;
                request.To = this.GetDestinationFileName(file);

                lock (this.requests)
                    this.requests.Add(request);

                LogToFile($"\t\t{file}");
            }

            LogToFile(string.Empty);
            LogToFile($"\t-> Arquivos: {files.Length}.");

            lock (this.requests)
                this.lbStatus.Dispatch(x => x.Text = $"Enfileirando {this.requests.Count} arquivo(s) para mover.");

            var directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

            LogToFile($"\t-> Diretórios: {directories.Length}");
            LogToFile(string.Empty);

            foreach (var directory in directories)
            {
                try { this.ScanFiles(directory); }
                catch { /* ignore */ }
            }
        }

        void MoveFiles()
        {
            LogToFile("====================== MOVENDO ARQUIVOS ======================");
            LogToFile(string.Empty);

            Thread.Sleep(1500);

            var total = 0;

            lock (this.requests)
                pbStatus.Dispatch(x => x.Maximum = total = this.requests.Count);

            var moved = 0;
            var failed = 0;
            goto check;

        move:
            {
                var request = PopFirstItem();
                LogToFile($"Movendo arquivo {request.From} -> {request.To}");

                try
                {
                    lbStatus.Dispatch(x => x.Text = $"Movendo Arquivo: {request.To}");

                    var fileinfo = new FileInfo(request.To);

                    if (!fileinfo.Directory.Exists)
                        fileinfo.Directory.Create();

                    var watch = Stopwatch.StartNew();
                    File.Move(request.From, request.To);
                    watch.Stop();
                    LogToFile($"\tMovido Em: {watch.ElapsedMilliseconds}ms");
                    LogToFile(string.Empty);
                    moved++;
                }
                catch (Exception ex)
                {
                    failed++;
                    LogToFile($"\tFalhou: <{ex.GetType().FullName}> {ex.Message}\n");
                    LogToFile(string.Empty);
                }

                pbStatus.Dispatch(x => x.PerformStep());
                this.Dispatch(x => x.Text = $"Bulk Move - Trabalhando @ {moved}/{failed} de {total} arquivo(s)");
            }

        check:
            {
                Thread.Sleep(10);

                var state = this.IsEmpty;

                if (!state)
                    goto move;
                else
                {
                    LogToFile("====================== ARQUIVOS MOVIDOS ======================");
                    LogToFile(String.Empty);
                    LogToFile($"\tArquivos Movidos: {total}");
                    LogToFile($"\tArquivos Movidos Com Falha: {failed}");
                    LogToFile($"\tArquivos Movidos Com Sucesso: {moved}");
                    LogToFile($"\tTempo Total: {gWatch.ElapsedMilliseconds}ms");
                    gWatch.Stop();

                    this.Dispatch(x => x.Text = "Bulk Move - Terminou");

                    lbStatus.Dispatch(x => x.ForeColor = Color.Blue);
                    lbStatus.Dispatch(x => x.Text = $"De {total} arquivo(s) foram concluídos: {moved} com sucesso / {failed} com falha.");
                    completed = true;
                    btnCancel.Dispatch(x => x.Text = "OK");
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (logFile != null)
            {
                logFile.Flush();
                logFile.Dispose();
                logFile = null;
            }
        }

        volatile bool completed = false;

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (!completed)
            {

                try
                {
                    if (ScanThread != null)
                        ScanThread.Abort();
                }
                catch { }

                try
                {
                    if (MoveThread != null)
                        MoveThread.Abort();
                }
                catch { }
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class BulkMoveRequest
    {
        public string Id;
        public string CurrentDirectory;
        public string From;
        public string To;
    }
}