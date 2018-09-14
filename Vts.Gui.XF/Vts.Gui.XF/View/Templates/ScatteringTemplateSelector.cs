using System;
using System.ComponentModel;
using OxyPlot;
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
            "Vts.SpectralMapping.PowerLawScatterer",
            BindingMode.TwoWay,
            propertyChanged: UpdateScatteringType);


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

        private static void UpdateScatteringType(Xamarin.Forms.BindableObject bindable, object oldValue, object newValue)
        {
            //throw new NotImplementedException();

            var selector = oldValue as ScatteringTemplateSelector;

            var scattType = selector.ScatteringType;
            if (scattType == typeof(MieScatterer).FullName)
            {
                //selector.Content = selector.MieScatteringTemplate.LoadContent() as UIElement;
            }
            else if (scattType == typeof(PowerLawScatterer).FullName)
            {
                //selector.Content = selector.PowerLawScatteringTemplate.LoadContent() as UIElement;
                oldValue = selector.PowerLawScatteringTemplate;
            }
            else if (scattType == typeof(IntralipidScatterer).FullName)
            {
                //selector.Content = selector.IntralipidScatteringTemplate.LoadContent() as UIElement;
            }
        }
    }
}