using System;
using Avalonia.LogicalTree;
using Avalonia.Reactive;

namespace Avalonia.Controls.Generators
{
    internal class OwnerBinding<T, TOwner, TChild> : SingleSubscriberObservableBase<T> where TChild : ILogical where TOwner : AvaloniaObject
    {
        private readonly TChild _child;
        private readonly StyledProperty<T> _ownerProperty;
        private IDisposable? _ownerSubscription;
        private IDisposable? _propertySubscription;

        public OwnerBinding(TChild child, StyledProperty<T> ownerProperty)
        {
            _child = child;
            _ownerProperty = ownerProperty;
        }

        protected override void Subscribed()
        {
            _ownerSubscription = ControlLocator.Track(_child, 0, typeof(TOwner)).Subscribe(OwnerChanged);
        }

        protected override void Unsubscribed()
        {
            _ownerSubscription?.Dispose();
            _ownerSubscription = null;
        }

        private void OwnerChanged(ILogical? c)
        {
            _propertySubscription?.Dispose();
            _propertySubscription = null;

            if (c is TOwner owner)
            {
                _propertySubscription = owner.GetObservable(_ownerProperty)
                    .Subscribe(x => PublishNext(x));
            }
        }
    }
}
