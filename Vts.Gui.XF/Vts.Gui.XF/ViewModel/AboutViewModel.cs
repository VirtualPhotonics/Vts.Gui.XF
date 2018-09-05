using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Vts.Gui.XF.ViewModel
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            //Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}