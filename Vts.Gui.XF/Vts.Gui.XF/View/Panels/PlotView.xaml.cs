using System.Windows;
//using System.Windows.Controls;
using System.Windows.Input;
using Vts.Gui.XF.ViewModel;
//using Vts.Gui.XF.ViewModel.Helpers;
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

            BindingContext = viewModel = new PlotViewModel();
        }


        //private void TextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    var tbx = sender as TextBox;
        //    if (tbx != null && e.Key == Key.Enter)
        //        tbx.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        //}
    }
}