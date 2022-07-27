using PopStudio.Dialogs;
using System.Threading.Tasks;

namespace PopStudio.PlatformAPI
{
    public static class YFDialogHelper
    {
        public static Task<object> OpenDialog<T>(params object[] args) where T : IDialogClosable, new()
        {
            T dialog = new T();
            dialog.InitDialog(args);
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            dialog.OnCloseOver += () => tcs.SetResult(dialog.Result);
            MainPage.Singleton.BeginDialog(dialog);
            return tcs.Task;
        }
    }
}
