using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;

namespace XamlBrewer.Uwp.Controls
{
    /// <summary>
    /// A Custom Slider Control that can be two-way bound to a property of an Enumeration type.
    /// </summary>
    [TemplatePart(Name = SliderPartName, Type = typeof(Slider))]
    public sealed class EnumSlider : Control
    {
        // Name of the Slider in the Control Template.
        private const string SliderPartName = "PART_Slider";

        // The Enum Type to which we are bound.
        private Type _enum;

        // Value Dependency Property.
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(object),
                typeof(EnumSlider),
                new PropertyMetadata(null, OnValueChanged));

        /// <summary>
        /// Constructor.
        /// </summary>
        public EnumSlider()
        {
            this.DefaultStyleKey = typeof(EnumSlider);
        }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Sets up a two-way data binding.
        /// </summary>
        public bool BindTo(object viewModel, string path)
        {
            try
            {
                Binding b = new Binding();
                b.Source = viewModel;
                b.Path = new PropertyPath(path);
                b.Mode = BindingMode.TwoWay;
                b.Converter = new EnumConverter();
                this.SetBinding(EnumSlider.ValueProperty, b);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Called when the (default or custom) style template is applied.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            if (_enum == null)
            {
                return;
            }

            InitializeSlider();

            base.OnApplyTemplate();
        }

        /// <summary>
        /// Configures the internal Slider.
        /// </summary>
        private void InitializeSlider()
        {
            var slider = this.GetTemplateChild(SliderPartName) as Slider;
            if (slider != null)
            {
                slider.ValueChanged += Slider_ValueChanged;
                slider.Maximum = Enum.GetNames(this._enum).Count() - 1;
                slider.ThumbToolTipValueConverter = new DoubleToEnumConverter(_enum);
                slider.Value = (double)(int)this.Value;
            }
        }

        /// <summary>
        /// Called when the internal Slider's Value changed, e.g. by user manipulation.
        /// </summary>
        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (this._enum == null)
            {
                return;
            }

            this.Value = Enum.ToObject(this._enum, (int)(sender as Slider).Value);
        }

        /// <summary>
        /// Called when the Value changed, e.g. through data binding.
        /// </summary>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var _this = d as EnumSlider;

            if (_this != null)
            {
                // Initialize the Enum Type.
                if (e.OldValue == null)
                {
                    if (e.NewValue is Enum)
                    {
                        _this._enum = e.NewValue.GetType();
                        _this.InitializeSlider();
                    }
                }

                var slider = _this.GetTemplateChild(SliderPartName) as Slider;

                if (slider != null)
                {
                    slider.Value = (double)(int)_this.Value;
                }
            }
        }
    }
}
