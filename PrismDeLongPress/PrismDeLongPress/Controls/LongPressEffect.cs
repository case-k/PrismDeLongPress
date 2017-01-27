using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrismDeLongPress.Controls
{
    public static class LongPressEffect
    {
        public static readonly BindableProperty EnabledProperty = BindableProperty.CreateAttached("Enabled", typeof(bool), typeof(LongPressEffect), false, propertyChanged: OnEnabledChanged);

        private static void OnEnabledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view == null)
                return;

            var enabled = (bool)newValue;
            if (enabled)
            {
                view.Effects.Add(new LongPressRoutingEffect());
            }
            else
            {
                var toRemove = view.Effects.FirstOrDefault(e => e is LongPressRoutingEffect);
                if (toRemove != null)
                    view.Effects.Remove(toRemove);
            }
        }

        public static void SetEnabled(BindableObject view, bool value)
        {
            view.SetValue(EnabledProperty, value);
        }

        public static bool GetEnabled(BindableObject view)
        {
            return (bool)view.GetValue(EnabledProperty);
        }



        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(LongPressEffect), default(ICommand));

        public static void SetCommand(BindableObject view, ICommand value)
        {
            view.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(CommandProperty);
        }


        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(LongPressEffect), default(object));

        public static void SetCommandParameter(BindableObject view, object value)
        {
            view.SetValue(CommandParameterProperty, value);
        }

        public static object GetCommandParameter(BindableObject view)
        {
            return (object)view.GetValue(CommandParameterProperty);
        }


        class LongPressRoutingEffect : RoutingEffect
        {
            public LongPressRoutingEffect() : base("YourCompany.LongPressEffect")
            {

            }
        }
    }
}
