using System;
using System.Threading.Tasks;

namespace PopStudio.Dialogs
{
    public interface IDialogClosable
    {
        public Func<Task<bool>> OnClose { get; set; }

        public Action OnCloseOver { get; set; }

        public object Result { get; set; }

        public bool CanClose { get; set; }

        public async Task<bool> Close()
        {
            CanClose = true;
            await OnClose?.Invoke();
            if (CanClose)
            {
                MainPage.Singleton.EndDialog();
                OnCloseOver?.Invoke();
            }
            return CanClose;
        }

        public void InitDialog(params object[] args);
    }
}
