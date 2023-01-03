using Avalonia.Styling;

namespace Avalonia.Controls
{
    /// <summary>
    /// Decorator control that isolates controls subtree with locally defined <see cref="ThemeVariant"/> property.
    /// </summary>
    public class ThemeVariantScope : Decorator
    {
        /// <summary>
        /// Gets or sets the UI theme variant that is used by the control (and its child elements) for resource determination.
        /// The UI theme you specify with ThemeVariant can override the app-level ThemeVariant.
        /// </summary>
        /// <remarks>
        /// To reset local value and inherit parent theme, set ThemeVariant.Default value or Null.
        /// </remarks>
        public ThemeVariant? RequestedThemeVariant
        {
            get => GetValue(RequestedThemeVariantProperty);
            set => SetValue(RequestedThemeVariantProperty, value);
        }
    }
}
