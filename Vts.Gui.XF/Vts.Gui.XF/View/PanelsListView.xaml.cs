using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vts.Gui.XF.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Vts.Gui.XF.View;
using Vts.Gui.XF.ViewModel;

namespace Vts.Gui.XF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanelsListView : ContentPage
    {
        PanelsListViewModel viewModel;

        public PanelsListView()
        {
            InitializeComponent();

            BindingContext = viewModel = new PanelsListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var panel = args.SelectedItem as PanelsItem;
            if (panel == null)
                return;
            if (panel.Name == "Spectral Panel")
            {
                await Navigation.PushAsync(new SpectralMappingView());
            }

           // Manually deselect item.
           ItemsListView.SelectedItem = null;
        }

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.PanelsList.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}