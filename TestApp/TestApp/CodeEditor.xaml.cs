using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace TestApp
{
    public partial class CodeEditor : TabbedPage
    {
        private string path;

        public async Task code()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder projects = await rootFolder.CreateFolderAsync("Projects", CreationCollisionOption.OpenIfExists);
            string project = "Projects\\" + path;
            IFolder projectFolder = await rootFolder.CreateFolderAsync(project, CreationCollisionOption.OpenIfExists);
            IFile file = await projects.CreateFileAsync(path + "\\index.html", CreationCollisionOption.OpenIfExists);
            string code = await file.ReadAllTextAsync();
            editor.Text = code;
            await DisplayAlert("PATH", "Project created in: " + file.Path, "OK");
            web_view.Source = new HtmlWebViewSource
            {
                Html = code
            };
        }

        public async void OnSave(Object o, EventArgs e)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder projects = await rootFolder.CreateFolderAsync("Projects", CreationCollisionOption.OpenIfExists);
            string project = "Projects\\" + path;
            IFolder projectFolder = await rootFolder.CreateFolderAsync(project, CreationCollisionOption.OpenIfExists);
            IFile file = await projects.CreateFileAsync(path + "\\index.html", CreationCollisionOption.OpenIfExists);
            await file.WriteAllTextAsync(editor.Text);
            await DisplayAlert("Success", path + "Saved succesfully.", "OK");
        }

        public void refresh(Object o, EventArgs e)
        {
            web_view.Source = new HtmlWebViewSource
            {
                Html = editor.Text
            };
        }

        public CodeEditor(string path)
        {
            InitializeComponent();
            this.path = path;
            code();
        }

        public void CopyToClipboard(Object o, EventArgs e) {
            if (Plugin.Share.CrossShare.Current.SupportsClipboard == false)
            {
                DisplayAlert("Error", "Error while copying to the clipboard.", "OK");
            }
            Plugin.Share.CrossShare.Current.SetClipboardText(editor.Text);
        }

    }
}
