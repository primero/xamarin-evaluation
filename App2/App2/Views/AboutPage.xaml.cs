using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2.Models;
using App2.Views;
using App2.ViewModels;

namespace App2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
        ItemsViewModel viewModel;
        public AboutPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ItemsViewModel();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(@"				INFO {0}", "Add item");
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }
    }
}