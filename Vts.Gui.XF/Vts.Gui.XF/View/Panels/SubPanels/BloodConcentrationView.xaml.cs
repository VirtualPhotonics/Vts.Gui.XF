
using System.Windows.Input;
using Vts.Gui.XF.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vts.Gui.XF.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BloodConcentrationView : ContentView
    {
        private BloodConcentrationViewModel viewModel;
        public BloodConcentrationView()
        {
            InitializeComponent();

            BindingContext = viewModel = new BloodConcentrationViewModel();
        }

        //private void TextBox_KeyDown(object sender, KeyEventArgs e)
        private void TextBox_KeyDown(object sender)
        {
            var tbx = sender as Entry;
            //if (tbx != null && e.Key == Key.Enter)
            //    tbx.GetBindingExpression(Entry.TextProperty).UpdateSource();
        }
    }
}