using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

            BindingContext = viewModel = PanelsListViewModel.Current.SpectralMappingVM;

        }
        private void SetSectionVisibility(StackLayout section, bool isVisible)
        {
            // found following solution on web:
            // https://forums.xamarin.com/discussion/35461/isvisible-inside-control-inside-stacklayout-inside-scrollview-not-work
            // basically sets height of stackLayout = 0 equiv to isVisible=false, =-1 equiv to isVisible=true 
            section.HeightRequest = isVisible ? -1d : 0;
        }
        async void OnPlotButton_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new PlotView());
        }
        //async void OnUpdateWavelengthButton_Clicked(object sender, EventArgs args)
        //{
        //    await this.viewModel.UpdateWavelengthCommand(sender);
        //}
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