﻿using System;
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
                instance.setHtml();
            }
        }

        private void setHtml()
        {
            this.FormattedText = HtmlHelper.Html2LabelSpans(HtmlText, IgnoreNewLine, this);
        }

        /// <summary>
        /// Html-Text that should be displayed in the Label
        /// In case of a parsingerror it shows a red text with the parsing-error.
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
                instance.setHtml();                
            }
        }

        /// <summary>
        /// Configures if Newline Characters should be ignored in the html text (default true)
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
