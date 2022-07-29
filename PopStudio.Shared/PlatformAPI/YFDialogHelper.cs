using PopStudio.Dialogs;
using System.Threading.Tasks;

namespace PopStudio.PlatformAPI
{
    public static class YFDialogHelper
    {
        public static Task<object> OpenDialog<T>(params object[] args)
            where T : IDialogClosable, new()
        {
            T dialog = new T();
            dialog.InitDialog(args);
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            dialog.OnCloseOver += () => tcs.SetResult(dialog.Result);
            MainPage.Singleton.BeginDialog(dialog);
            return tcs.Task;
        }

        public static Task<T2> OpenDialog<T1, T2>(params object[] args)
            where T1 : IDialogClosable, new()
            where T2 : class
        {
            T1 dialog = new T1();
            dialog.InitDialog(args);
            TaskCompletionSource<T2> tcs = new TaskCompletionSource<T2>();
            dialog.OnCloseOver += () => tcs.SetResult(dialog.Result as T2);
            MainPage.Singleton.BeginDialog(dialog);
            return tcs.Task;
        }

        public static Task<object> OpenDialogWithoutCancelButton<T>(params object[] args)
            where T : IDialogClosable, new()
        {
            T dialog = new T();
            dialog.InitDialog(args);
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            dialog.OnCloseOver += () => tcs.SetResult(dialog.Result);
            MainPage.Singleton.BeginDialog(dialog, false);
            return tcs.Task;
        }

        public static Task<T2> OpenDialogWithoutCancelButton<T1, T2>(params object[] args)
            where T1 : IDialogClosable, new()
            where T2 : class
        {
            T1 dialog = new T1();
            dialog.InitDialog(args);
            TaskCompletionSource<T2> tcs = new TaskCompletionSource<T2>();
            dialog.OnCloseOver += () => tcs.SetResult(dialog.Result as T2);
            MainPage.Singleton.BeginDialog(dialog, false);
            return tcs.Task;
        }
    }
}
