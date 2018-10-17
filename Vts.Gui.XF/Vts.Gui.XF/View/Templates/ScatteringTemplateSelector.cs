using OxyPlot;
using Vts.Gui.XF.Extensions;
using Vts.SpectralMapping;
using Xamarin.Forms;

namespace Vts.Gui.XF.View
{
    /// <summary>
    /// CURRENTLY THIS CLASS IS OBSOLETE INSTEAD ISVISIBLE IS BEING USED
    /// </summary>
    public class ScatteringTemplateSelector : DataTemplateSelector
    {
        //public static readonly DependencyProperty ScatteringTypeProperty = DependencyProperty.Register(
        //    "ScatteringType",
        //    typeof(string),
        //    typeof(ScatteringTemplateSelector),
        //    new PropertyMetadata(string.Empty, UpdateScatteringType));
        //public static readonly BindableProperty ScatteringTypeProperty = BindableProperty.Create(
        //    "ScatteringType",
        //    typeof(string),
        //    typeof(ScatteringTemplateSelector),
        //    "Vts.SpectralMapping.PowerLawScatterer"); // last arg: default value

        private string _scatteringType = "Vts.SpectralMapping.PowerLawScatterer";
        public ScatteringTemplateSelector()
        {
            //Loaded += (s, a) => ScatteringType = "Vts.SpectralMapping.PowerLawScatterer";
            //ScatteringType = "Vts.SpectralMapping.PowerLawScatterer";
        }

        public DataTemplate MieScatteringTemplate { get; set; }
        public DataTemplate PowerLawScatteringTemplate { get; set; }
        public DataTemplate IntralipidScatteringTemplate { get; set; }

        public string ScatteringType
        {
            //get { return (string) GetValue(ScatteringTypeProperty); }
            //set { SetValue(ScatteringTypeProperty, value); }
            get { return _scatteringType; }
            set { _scatteringType = value; }
        }
        protected override DataTemplate OnSelectTemplate(object item, Xamarin.Forms.BindableObject container)
        {
            var selector = item as ScatteringTemplateSelector;
            //var scattType = e.NewValue as string;
            var scattType = selector.ScatteringType;
            if (scattType == typeof(MieScatterer).FullName)
            {
                //selector.Content = selector.MieScatteringTemplate.LoadContent() as UIElement;
                return MieScatteringTemplate;
            }
            else if (scattType == typeof(PowerLawScatterer).FullName)
            {
                //selector.Content = selector.PowerLawScatteringTemplate.LoadContent() as UIElement;
                _scatteringType = "Vts.SpectralMapping.PowerLawScatterer";
                return PowerLawScatteringTemplate;
            }
            else if (scattType == typeof(IntralipidScatterer).FullName)
            {
                //selector.Content = selector.IntralipidScatteringTemplate.LoadContent() as UIElement;
                _scatteringType = "Vts.SpectralMapping.Intralipid";
                return IntralipidScatteringTemplate;
            }
            else
            {
                return null;
            }
        }

    }
}
