using Avalonia.Controls.Generators;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.Layout;

namespace Avalonia.Controls.Primitives
{
    public class PivotHeader : SelectingItemsControl
    {
        private static readonly FuncTemplate<Panel> DefaultPanel =
            new FuncTemplate<Panel>(() => new VirtualizingStackPanel { Orientation = Orientation.Horizontal });

        static PivotHeader()
        {
            SelectionModeProperty.OverrideDefaultValue<PivotHeader>(SelectionMode.AlwaysSelected);
            FocusableProperty.OverrideDefaultValue(typeof(PivotHeader), false);
            ItemsPanelProperty.OverrideDefaultValue<PivotHeader>(DefaultPanel);
        }

        protected override IItemContainerGenerator CreateItemContainerGenerator()
        {
            return new PivotHeaderItemContainerGenerator(this);
        }

        /// <inheritdoc/>
        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            base.OnGotFocus(e);

            if (e.NavigationMethod == NavigationMethod.Directional)
            {
                e.Handled = UpdateSelectionFromEventSource(e.Source);
            }
        }

        /// <inheritdoc/>
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (e.Source is Visual source)
            {
                var point = e.GetCurrentPoint(source);

                if (point.Properties.IsLeftButtonPressed)
                {
                    e.Handled = UpdateSelectionFromEventSource(e.Source);
                }
            }
        }
    }
}
