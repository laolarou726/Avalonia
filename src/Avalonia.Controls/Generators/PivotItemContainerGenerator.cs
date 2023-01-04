using Avalonia.Controls.Templates;

namespace Avalonia.Controls.Generators
{
    public partial class PivotItemContainerGenerator : ItemContainerGenerator<PivotItem>
    {
        public PivotItemContainerGenerator(Pivot owner)
            : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
        {
            Owner = owner;
        }

        public new Pivot Owner { get; }

        protected override Control CreateContainer(object item)
        {
            var PivotItem = (PivotItem)base.CreateContainer(item)!;

            if (!(PivotItem.Content is Control))
            {
                PivotItem.Bind(PivotItem.ContentTemplateProperty, new OwnerBinding<IDataTemplate?, Pivot, PivotItem>(
                    PivotItem,
                    Pivot.ItemTemplateProperty));
            }

            return PivotItem;
        }
    }
}
