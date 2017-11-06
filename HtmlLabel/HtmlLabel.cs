using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MWX.XamForms.Controls
{
    public class HtmlLabel : Label
    {
        public static BindableProperty HtmlTextProperty = BindableProperty.Create(nameof(HtmlText), typeof(string), typeof(HtmlLabel), defaultBindingMode: BindingMode.OneWay, propertyChanged: HtmlTextPropertyChanged, defaultValue: "");

        static void HtmlTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as HtmlLabel;
            if (instance != null)
            {
                instance.FormattedText = HtmlHelper.Html2LabelSpans(Convert.ToString(newValue), instance.IgnoreNewLine);
            }
        }

        /// <summary>
        /// HtmlText from Google AdMob
        /// </summary>
        public string HtmlText
        {
            get { return (string)GetValue(HtmlTextProperty); }
            set { SetValue(HtmlTextProperty, value); }
        }

        public static BindableProperty IgnoreNewLineProperty = BindableProperty.Create(nameof(IgnoreNewLine), typeof(bool), typeof(HtmlLabel), defaultBindingMode: BindingMode.OneWay, propertyChanged: IgnoreNewLinePropertyChanged, defaultValue:true);

        static void IgnoreNewLinePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as HtmlLabel;
            if (instance != null)
            {
                instance.FormattedText = HtmlHelper.Html2LabelSpans(instance.HtmlText, Convert.ToBoolean(newValue));
            }
        }

        /// <summary>
        /// IgnoreNewLine from Google AdMob
        /// </summary>
        public bool IgnoreNewLine
        {
            get { return (bool)GetValue(IgnoreNewLineProperty); }
            set { SetValue(IgnoreNewLineProperty, value); }
        }

        public HtmlLabel()
        {
        }
    }
}
