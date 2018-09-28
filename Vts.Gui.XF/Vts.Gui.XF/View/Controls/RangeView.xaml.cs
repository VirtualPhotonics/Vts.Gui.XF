using Vts.Gui.XF.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vts.Gui.XF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RangeView : ContentView
    {
        private RangeViewModel viewModel;
        public RangeView()
        {
            InitializeComponent();

            BindingContext = viewModel = new RangeViewModel();
        }

        private void TextBox_KeyDown(object sender)
        {
        //    var tbx = sender as TextBox;
        //    if (tbx != null && e.Key == Key.Enter)
        //        tbx.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}