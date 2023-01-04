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
    public class TabItemContainerGenerator : ItemContainerGenerator<TabItem>
    {
        public TabItemContainerGenerator(TabControl owner)
            : base(owner, ContentControl.ContentProperty, ContentControl.ContentTemplateProperty)
        {
            Owner = owner;
        }

        public new TabControl Owner { get; }

        protected override Control CreateContainer(object item)
        {
            var tabItem = (TabItem)base.CreateContainer(item)!;

            tabItem.Bind(TabItem.TabStripPlacementProperty, new OwnerBinding<Dock, TabControl, TabItem>(
                tabItem,
                TabControl.TabStripPlacementProperty));

            if (tabItem.HeaderTemplate == null)
            {
                tabItem.Bind(TabItem.HeaderTemplateProperty, new OwnerBinding<IDataTemplate?, TabControl, TabItem>(
                    tabItem,
                    TabControl.ItemTemplateProperty));
            }

            if (Owner.HeaderDisplayMemberBinding is not null)
            {
                tabItem.Bind(HeaderedContentControl.HeaderProperty, Owner.HeaderDisplayMemberBinding,
                    BindingPriority.Style);
            }

            if (tabItem.Header == null)
            {
                if (item is IHeadered headered)
                {
                    tabItem.Header = headered.Header;
                }
                else
                {
                    if (!(tabItem.DataContext is Control))
                    {
                        tabItem.Header = tabItem.DataContext;
                    }
                }
            }

            if (!(tabItem.Content is Control))
            {
                tabItem.Bind(TabItem.ContentTemplateProperty, new OwnerBinding<IDataTemplate?, TabControl, TabItem>(
                    tabItem,
                    TabControl.ContentTemplateProperty));
            }

            return tabItem;
        }
    }
}
