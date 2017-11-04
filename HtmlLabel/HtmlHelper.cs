using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace HtmlLabel
{
    public class HtmlHelper
    {

        public static FormattedString Html2LabelSpans(string html, bool ignoreNewLines = true)
        {
            FormattedString fstring = new FormattedString();
            var xml = $@"<html>{html}</html>";

            XElement root = XElement.Parse(xml);

            ProcessNodes(fstring, root, new StyleContainer(), ignoreNewLines);

            return fstring;
        }

        private static void ProcessNodes(FormattedString fstring, XElement xe, StyleContainer cont, bool ignoreNewLines)
        {
            foreach (var node in xe.Nodes())
            {
                var element = node as XElement;
                switch (node.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        if (element.Name.LocalName.ToLower() == "span")
                        {
                            var boldCont = cont.Clone();
                            ProcessStyles(element, boldCont);
                            ProcessNodes(fstring, element, boldCont, ignoreNewLines);
                        }
                        else if (element.Name.LocalName.ToLower() == "b")
                        {
                            var boldCont = cont.Clone();
                            boldCont.FontAttributes |= FontAttributes.Bold;
                            ProcessStyles(element, boldCont);
                            ProcessNodes(fstring, element, boldCont, ignoreNewLines);
                        }
                        else if (element.Name.LocalName.ToLower() == "i")
                        {
                            var boldCont = cont.Clone();
                            ProcessStyles(element, boldCont);
                            boldCont.FontAttributes |= FontAttributes.Italic;
                            ProcessNodes(fstring, element, boldCont, ignoreNewLines);
                        }
                        else if (element.Name.LocalName.ToLower() == "br")
                        {
                            fstring.Spans[fstring.Spans.Count - 1].Text += Environment.NewLine;
                        }
                        break;
                    case System.Xml.XmlNodeType.Text:
                        var span = cont.ToSpan();
                        var txt = node.ToString();
                        if (ignoreNewLines)
                            txt = txt.Replace("\n", "").Replace("\r", "");

                        span.Text = txt;
                        fstring.Spans.Add(span);
                        break;
                    default:
                        break;
                }

            }
        }

        private static void ProcessStyles(XElement element, StyleContainer styleCont)
        {
            if (!element.HasAttributes) return;
            var styleStr = element.Attribute(XName.Get("style"))?.Value;
            if (string.IsNullOrEmpty(styleStr)) return;
            var styles = styleStr.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var styleEntry in styles)
            {
                var parts = styleEntry.ToLower().Split("=".ToCharArray());
                if (parts[0] == "background-color")
                {
                    styleCont.BackgroundColor = readHexColor(parts[1]);
                }
                else if (parts[0] == "color")
                {
                    styleCont.ForegroundColor = readHexColor(parts[1]);
                }
                else if (parts[0] == "font-weight")
                {
                    if (parts[1] =="bold")
                    {
                        styleCont.FontAttributes |= FontAttributes.Bold;
                    }
                }
            }
        }

        private static Color readHexColor(string v)
        {
            throw new NotImplementedException();
        }

        private class StyleContainer
        {
            public Color BackgroundColor { get; set; }
            public Font Font { get; set; }
            public Color ForegroundColor { get; set; }
            public FontAttributes FontAttributes { get; set; }
            public string FontFamily { get; set; }
            public double FontSize { get; set; }

            public StyleContainer Clone()
            {
                return new StyleContainer
                {
                    BackgroundColor = this.BackgroundColor,
                    //Font = this.Font,
                    ForegroundColor = this.ForegroundColor,
                    FontAttributes = this.FontAttributes,
                    FontFamily = this.FontFamily,
                    FontSize = this.FontSize
                };
            }

            public Span ToSpan()
            {
                return new Span
                {
                    BackgroundColor = this.BackgroundColor,
                    //Font = this.Font,
                    ForegroundColor = this.ForegroundColor,
                    FontAttributes = this.FontAttributes,
                    FontFamily = this.FontFamily,
                    FontSize = this.FontSize
                };
            }
        }
    }
}
