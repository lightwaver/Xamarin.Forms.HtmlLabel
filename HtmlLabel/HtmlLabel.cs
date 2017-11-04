using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HtmlLabel
{
    public class HtmlLabel : Label
    {
        public static BindableProperty HtmlTextProperty = BindableProperty.Create(nameof(HtmlText), typeof(string), typeof(HtmlLabel), propertyChanged: HtmlTextPropertyChanged);

        static void HtmlTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as HtmlLabel;
            if (instance != null)
            {
                instance.FormattedText = HtmlHelper.Html2LabelSpans(Convert.ToString(newValue));    
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

        public HtmlLabel()
        {
        }
    }
}
