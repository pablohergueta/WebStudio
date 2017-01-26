using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;
using System.Net.Http;

namespace TestApp
{
    public partial class NewProyect : ContentPage
    {
        public NewProyect()
        {
            InitializeComponent();
        }

        public async Task<String> DescargarContenido(String direccion)
        {
            using (var client = new HttpClient())
            {

                var url = string.Format(direccion);
                var resp = await client.GetAsync(url);
                if (resp.IsSuccessStatusCode)
                {
                    return resp.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return "Ha habido un problema con la descarga";
                }
            }
        }
        public async void new_project(Object sender, EventArgs e)
        {
            // Get the root directory of the file system for our application.
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            // Create another folder, if one doesn't already exist.
            IFolder projects = await rootFolder.CreateFolderAsync("Projects", CreationCollisionOption.OpenIfExists);
            if (newProject.Text == "")
            {
                await DisplayAlert("Error", "Introduce un nombre valido", "OK");
                return;
            }
            string project = "Projects\\" + newProject.Text;
            IFolder projectFolder = await rootFolder.CreateFolderAsync(project, CreationCollisionOption.OpenIfExists);
            try
            {
                IFile file = await projects.CreateFileAsync(newProject.Text + "\\index.html", CreationCollisionOption.FailIfExists);
                string web = await DescargarContenido("http://test.jolama.es/Hackaton/index.html");
                await file.WriteAllTextAsync(web);
                //Abrir editor de codigo
                await Navigation.PushAsync(new CodeEditor(newProject.Text));
            }
            catch
            {
                await DisplayAlert("Error", "Ya hay un proyecto con ese nombre", "OK");
                return;
            }
        }
    }
}
