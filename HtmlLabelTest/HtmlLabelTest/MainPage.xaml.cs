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
            htmlInput.Text = @"<span style='font-weight:bold;font-size:32'>this is a HTML-Sample</span><br/>
with
<b> bold text </b> 
and 
<i>italic text</i>
<br/>some newlines <br/>
<b>bold <i>italic text </i></b>
<br/>
<span style='color:#00AA00;font-weight:bold;background-color:#ff0000'>green Text on red Background (except on UWP)</span><br/>
";
        }
    }
}
