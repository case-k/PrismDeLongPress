using PrismDeLongPress.Controls;
using PrismDeLongPress.iOS.Effects;
using System.Windows.Input;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(LongPressPlatformEffect), "LongPressEffect")]

namespace PrismDeLongPress.iOS.Effects
{
    class LongPressPlatformEffect : PlatformEffect
    {
        private ICommand _command;
        private object _commandParameter;
        private UIView _view;
        private UILongPressGestureRecognizer _longPressGesture;

        protected override void OnAttached()
        {
            _view = Control ?? Container;

            UpdateCommand();
            UpdateCommandParameter();
        }

        protected override void OnDetached()
        {
            if (_longPressGesture != null)
            {
                _view.RemoveGestureRecognizer(_longPressGesture);
                _longPressGesture.Dispose();
            }
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(e);

            if (e.PropertyName == LongPressEffect.CommandProperty.PropertyName)
            {
                UpdateCommand();
            }
            else if (e.PropertyName == LongPressEffect.CommandParameterProperty.PropertyName)
            {
                UpdateCommandParameter();
            }
        }

        void UpdateCommand()
        {
            _command = LongPressEffect.GetCommand(Element);
            if (_longPressGesture != null)
            {
                _view.RemoveGestureRecognizer(_longPressGesture);
                _longPressGesture.Dispose();
            }

            if (_command == null)
            {
                return;
            }

            _longPressGesture = new UILongPressGestureRecognizer( (obj) => {
                if (obj.State == UIGestureRecognizerState.Began)
                {
                    _command?.Execute(_commandParameter ?? Element);
                }
            });

            _view.AddGestureRecognizer(_longPressGesture);
        }

        void UpdateCommandParameter()
        {
            _commandParameter = LongPressEffect.GetCommandParameter(Element);
        }
    }
}
