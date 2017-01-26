using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void ClickedNewProject(object sender, EventArgs args)
        {
            Navigation.PushAsync(new NewProyect());
        }

        void ClickedOpenProject(object sender, EventArgs args)
        {
            Navigation.PushAsync(new OpenProject());
        }
    }
}
