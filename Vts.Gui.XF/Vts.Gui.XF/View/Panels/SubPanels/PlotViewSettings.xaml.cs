using System;
using Vts.Gui.XF.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vts.Gui.XF.View.Panels.SubPanels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlotViewSettings : ContentPage
	{
	    private PlotViewModel _viewModel;
		public PlotViewSettings ()
		{
			InitializeComponent ();
		    BindingContext = _viewModel = PanelsListViewModel.Current.PlotVM;
		}

	    async void OnCloseButton_Clicked(object sender, EventArgs args)
	    {
	        await Navigation.PopModalAsync();
	    }
    }
}