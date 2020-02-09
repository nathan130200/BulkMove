using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulkMove
{
    public static class WindowsFormsExtensions
    {
        public static void Dispatch<TControl>(this TControl control, Action callback)
            where TControl : Control
        {
            control.Invoke(callback);
        }

        public static void Dispatch<TControl>(this TControl control, Action<TControl> callback)
            where TControl : Control
        {
            control.Invoke(new Action(() => callback(control)));
        }
    }
}
