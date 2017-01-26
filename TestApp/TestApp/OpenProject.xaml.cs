using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TestApp
{
    public partial class OpenProject : ContentPage
    {
        public OpenProject()
        {
            InitializeComponent();
        }

        public async void open_project(Object o, EventArgs e)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder projects = await rootFolder.CreateFolderAsync("Projects", CreationCollisionOption.OpenIfExists);
            if(ExistenceCheckResult.NotFound != (await projects.CheckExistsAsync(project_name.Text)))
            {
                await DisplayAlert("Error", "This proyect does not exist.", "OK");
                return;
            }
            await Navigation.PushAsync(new CodeEditor(project_name.Text));
        }
    }
}
