using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkMove
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            goto app;

        app:
            {
                using (var form = new MainForm())
                {
                    if (form.ShowDialog() != DialogResult.OK)
                        return;

                    using (var operation = new OperationForm(form.SourcePath, form.DestinationPath))
                    {
                        if (operation.ShowDialog() != DialogResult.OK)
                            return;
                        else
                            goto app;
                    }
                }
            }
        }
    }
}
