using System;
using System.Threading;
using System.Threading.Tasks;

namespace PopStudio.PlatformAPI
{
    public static class YFThread
    {
        public static void Invoke(Action action)
        {
            if (action != null)
            {
                new Thread(() => action.Invoke())
                {
                    IsBackground = true
                }
                .Start();
            }
        }

        public static void InvokeOnMainThread(Action action)
        {
            if (action != null)
            {
                MainPage.Singleton.DispatcherQueue.TryEnqueue(() => action());
                //Task.Run(action); // Do not use ThreadStatic attribute!
            }
        }
    }
}
