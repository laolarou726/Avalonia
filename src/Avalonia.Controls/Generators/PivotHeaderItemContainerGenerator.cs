using System;
using System.Collections.Generic;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.LogicalTree;
using Avalonia.Reactive;
using Avalonia.VisualTree;

namespace Avalonia.Controls.Generators
{    
    public class PivotHeaderItemContainerGenerator : ItemContainerGenerator<PivotHeaderItem>
    {
        public PivotHeaderItemContainerGenerator(PivotHeader owner)
            : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
        {
            Owner = owner;
        }

        public new PivotHeader Owner { get; }

        protected override Control CreateContainer(object item)
        {
            var PivotHeaderItem = (PivotHeaderItem)base.CreateContainer(item)!;

            if (PivotHeaderItem.ContentTemplate == null)
            {
                PivotHeaderItem.Bind(PivotHeaderItem.ContentTemplateProperty, new OwnerBinding<IDataTemplate?, PivotHeader, PivotHeaderItem>(
                    PivotHeaderItem,
                    PivotHeader.ItemTemplateProperty));
            }

            if (Owner.DisplayMemberBinding is not null)
            {
                PivotHeaderItem.Bind(HeaderedContentControl.HeaderProperty, Owner.DisplayMemberBinding,
                    BindingPriority.Style);
            }

            if (PivotHeaderItem.Content == null)
            {
                if (item is IHeadered headered)
                {
                    PivotHeaderItem.Content = headered.Header;
                }
                else
                {
                    if (!(PivotHeaderItem.DataContext is Control))
                    {
                        PivotHeaderItem.Content = PivotHeaderItem.DataContext;
                    }
                }
            }

            return PivotHeaderItem;
        }
    }
}
