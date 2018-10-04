using System;
using Vts.Gui.XF.View.Panels.SubPanels;
using Vts.Gui.XF.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vts.Gui.XF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlotView : ContentPage
    {
        PlotViewModel viewModel;
        public PlotView()
        {
            InitializeComponent();
            BindingContext = viewModel = PanelsListViewModel.Current.PlotVM;
        }

        async void OnSettingsButton_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new PlotViewSettings());
        }
    }
}