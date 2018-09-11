using System.ComponentModel;
using System.Windows;
using OxyPlot;
using Vts.Gui.Wpf.View;
using Vts.Gui.XF.ViewModel;
using Vts.SpectralMapping;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vts.Gui.XF.View
{
    public class ScatteringTemplateSelector : ContentView
    {
        public static readonly BindableProperty ScatteringTypeProperty = BindableProperty.Create(
            "ScatteringType",
            typeof(string),
            typeof(ScatteringTemplateSelector),
            null);
            //new PropertyMetadata(string.Empty, UpdateScatteringType));

        public ScatteringTemplateSelector()
        {
            //Loaded += (s, a) => ScatteringType = "Vts.SpectralMapping.PowerLawScatterer";
            ScatteringType = "Vts.SpectralMapping.PowerLawScatterer";
        }

        public DataTemplate MieScatteringTemplate { get; set; }
        public DataTemplate PowerLawScatteringTemplate { get; set; }
        public DataTemplate IntralipidScatteringTemplate { get; set; }

        public string ScatteringType
        {
            get { return (string) GetValue(ScatteringTypeProperty); }
            set { SetValue(ScatteringTypeProperty, value); }
        }

        private void UpdateScatteringType(object obj, PropertyChangedEventArgs e)
        {
            var selector = obj as ScatteringTemplateSelector;

            var scattType = e.PropertyName as string;
            //if (scattType == typeof(MieScatterer).FullName)
            //{
            //    selector.Content = selector.MieScatteringTemplate.LoadContent() as UIElement;
            //}
            //else if (scattType == typeof(PowerLawScatterer).FullName)
            //{
            //    selector.Content = selector.PowerLawScatteringTemplate.SetBinding() as PowerLawScatteringView;
            //}
            //else if (scattType == typeof(IntralipidScatterer).FullName)
            //{
            //    selector.Content = selector.IntralipidScatteringTemplate.LoadContent() as UIElement;
            //}
        }
    }
}