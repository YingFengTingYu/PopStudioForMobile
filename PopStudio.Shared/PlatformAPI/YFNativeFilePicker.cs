using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace PopStudio.PlatformAPI
{
    public static class YFNativeFilePicker
    {
        public static async Task<StorageFolder> PickFolderAsync()
        {
            FolderPicker folderPicker = new FolderPicker();
#if WinUI
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, WinUI.MainWindow.Handle);
#endif
            PickerLocationId defaultStartFolder;
#if WASM
            defaultStartFolder = PickerLocationId.DocumentsLibrary;
#else
            defaultStartFolder = PickerLocationId.ComputerFolder;
#endif
            folderPicker.SuggestedStartLocation = defaultStartFolder;
            folderPicker.FileTypeFilter.Add("*");
            return await folderPicker.PickSingleFolderAsync();
        }

        public static async Task<StorageFile> PickOpenFileAsync()
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
#if WinUI
            WinRT.Interop.InitializeWithWindow.Initialize(fileOpenPicker, WinUI.MainWindow.Handle);
#endif
            PickerLocationId defaultStartFolder;
#if WASM
            defaultStartFolder = PickerLocationId.DocumentsLibrary;
#else
            defaultStartFolder = PickerLocationId.ComputerFolder;
#endif
            fileOpenPicker.SuggestedStartLocation = defaultStartFolder;
            fileOpenPicker.FileTypeFilter.Add("*");
            return await fileOpenPicker.PickSingleFileAsync();
        }

        public static async Task<StorageFile> PickSaveFileAsync(string name_in = null)
        {
            FileSavePicker fileSavePicker = new FileSavePicker();
#if WinUI
            WinRT.Interop.InitializeWithWindow.Initialize(fileSavePicker, WinUI.MainWindow.Handle);
#endif
            string defaultExtension;
            PickerLocationId defaultStartFolder;
#if WASM
            defaultExtension = ".popstudio";
            defaultStartFolder = PickerLocationId.DocumentsLibrary;
#else
            defaultExtension = ".";
            defaultStartFolder = PickerLocationId.ComputerFolder;
#endif
            fileSavePicker.SuggestedStartLocation = defaultStartFolder;
            string[] lst = new string[1];
            if (name_in is not null)
            {
                int index = name_in.LastIndexOf('.');
                string name;
                if (index >= 0)
                {
                    name = name_in[..index];
                    lst[0] = name_in[index..];
                }
                else
                {
                    name = name_in;
                    lst[0] = defaultExtension;
                }
                fileSavePicker.SuggestedFileName = name;
            }
            else
            {
                fileSavePicker.SuggestedFileName = "new file";
                lst[0] = defaultExtension;
            }
            fileSavePicker.FileTypeChoices.Add("File", lst);
            return await fileSavePicker.PickSaveFileAsync();
        }
    }
}
