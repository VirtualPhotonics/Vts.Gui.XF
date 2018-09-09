using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Vts.Gui.XF.View;
using Vts.Gui.XF.ViewModel;

namespace Vts.Gui.XF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpectralMappingView : ContentPage
    {
        SpectralMappingViewModel viewModel;

        public SpectralMappingView()
        {
            InitializeComponent();

            BindingContext = viewModel = new SpectralMappingViewModel();
        }

        async void OnPlotMuaSpectrumButton_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new PlotView());                
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //var panel = args.SelectedItem as Item
            //if (item == null)
                return;

            //await Navigation.PushAsync(new SpectralMappingView());

            //    // Manually deselect item.
            //    //ItemsListView.SelectedItem = null;
        }

        private void Entry_TextChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var tbx = sender as Entry;
            //if (tbx != null && e.Key == Key.Enter)
            //    tbx.GetBindingExpression(Entry.TextProperty).UpdateSource();
        }
    }
}