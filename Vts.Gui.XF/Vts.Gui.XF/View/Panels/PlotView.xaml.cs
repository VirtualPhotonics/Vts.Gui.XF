using System;
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

        private void OnSettingsButton_Clicked(object sender, EventArgs args)
        {
            var tabbedPage = Parent as TabbedPage;
            tabbedPage.CurrentPage = tabbedPage.Children[2];
        }
        private void OnKeyButton_Clicked(object sender, EventArgs args)
        {
            viewModel.HideKey = !viewModel.HideKey;
        }
    }
}