using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HtmlLabelTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            htmlLabel.HtmlText = @"
<span style='font-weight:bold'>aaa</span>
<b> bold text </b> 
bbb 
<i>italic</i>
<br/>
<b> bold <i>italic</i> text </b> 
";
        }
    }
}
