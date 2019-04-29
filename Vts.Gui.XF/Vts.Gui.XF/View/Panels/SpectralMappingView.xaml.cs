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

        override protected void OnAppearing()
        {
            var tabbedPage = Parent as TabbedPage;
            if (PanelsListViewModel.Current.PlotVM.PlotSeriesCollection.Count > 0 && tabbedPage.Children.Count == 1)
            {
                tabbedPage.Children.Add(new PlotView());
                tabbedPage.Children.Add(new PlotViewSettings());
            }
        }

        private void SetSectionVisibility(StackLayout section, bool isVisible)
        {
            // found following solution on web:
            // https://forums.xamarin.com/discussion/35461/isvisible-inside-control-inside-stacklayout-inside-scrollview-not-work
            // basically sets height of stackLayout = 0 equiv to isVisible=false, =-1 equiv to isVisible=true 
            section.HeightRequest = isVisible ? -1d : 0;
        }
        private void OnPlotButton_Clicked(object sender, EventArgs args)
        {
            var tabbedPage = Parent as TabbedPage;
            if (tabbedPage.Children.Count == 1)
            {
                // if the plot tab has not been created, create it here
                tabbedPage.Children.Add(new PlotView());
                tabbedPage.Children.Add(new PlotViewSettings());
            }
            tabbedPage.CurrentPage = tabbedPage.Children[1];
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