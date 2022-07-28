using System;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dialog_Wait : Page, IDialogClosable
    {
        public bool CanClose { get; set; }

        public object Result { get; set; }

        public Func<Task<bool>> OnClose { get; set; }

        public Action OnCloseOver { get; set; }

        public void InitDialog(params object[] args)
        {
            OnClose += () => Task.FromResult(CanClose = true);
            if (args is not null && args.Length >= 1 && args[0] is Task tsk)
            {
                tsk.GetAwaiter().OnCompleted(() => (this as IDialogClosable)?.Close());
            }
        }

        public Dialog_Wait()
        {
            this.InitializeComponent();
        }
    }
}
