using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace MWX.XamForms.Controls
{
    public class HtmlHelper
    {
        public static FormattedString Html2LabelSpans(string html, bool ignoreNewLines = true, Label l = null)
        {
            FormattedString fstring = new FormattedString();
            try
            {
                var xml = $@"<html>{html}</html>";

                XElement root = XElement.Parse(xml);

                ProcessNodes(fstring, root, new StyleContainer(l), ignoreNewLines, l);

            }
            catch (Exception ex)
            {
                fstring.Spans.Add(new Span
                {
                    Text = $"Error: {ex.Message}",
                    ForegroundColor = Color.Red,
                    FontAttributes = FontAttributes.Bold
                });
            }
            return fstring;
        }

        private static void ProcessNodes(FormattedString fstring, XElement xe, StyleContainer cont, bool ignoreNewLines, Label l)
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
                            ProcessNodes(fstring, element, boldCont, ignoreNewLines, l);
                        }
                        else if (element.Name.LocalName.ToLower() == "b")
                        {
                            var boldCont = cont.Clone();
                            boldCont.FontAttributes |= FontAttributes.Bold;
                            ProcessStyles(element, boldCont);
                            ProcessNodes(fstring, element, boldCont, ignoreNewLines, l);
                        }
                        else if (element.Name.LocalName.ToLower() == "i")
                        {
                            var boldCont = cont.Clone();
                            ProcessStyles(element, boldCont);
                            boldCont.FontAttributes |= FontAttributes.Italic;
                            ProcessNodes(fstring, element, boldCont, ignoreNewLines, l);
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
                var parts = styleEntry.ToLower().Split(":".ToCharArray());
                if (parts[0] == "background-color")
                {
                    styleCont.BackgroundColor = ReadHexColor(parts[1]);
                }
                else if (parts[0] == "color")
                {
                    styleCont.ForegroundColor = ReadHexColor(parts[1]);
                }
                else if (parts[0] == "font-weight")
                {
                    if (parts[1] == "bold")
                    {
                        styleCont.FontAttributes |= FontAttributes.Bold;
                    }
                    if (parts[1] == "normal")
                    {
                        styleCont.FontAttributes &= ~FontAttributes.Bold;
                    }
                }
                else if (parts[0] == "font-style")
                {
                    if (parts[1] == "italic")
                    {
                        styleCont.FontAttributes |= FontAttributes.Bold;
                    }
                    if (parts[1] == "normal")
                    {
                        styleCont.FontAttributes &= ~FontAttributes.Bold;
                    }
                }
                else if (parts[0] == "font-family")
                {
                    styleCont.FontFamily = parts[1];
                }
                else if (parts[0] == "font-size")
                {
                    styleCont.FontSize = double.Parse(parts[1].Trim("ptpxem".ToCharArray()));
                }
            }
        }

        private static Color ReadHexColor(string strColor)
        {
            strColor = strColor.Trim('#');
            return Color.FromHex(strColor);
        }

        private class StyleContainer
        {
            public Color BackgroundColor { get; set; }
            //public Font Font { get; set; }
            public Color ForegroundColor { get; set; }
            public FontAttributes FontAttributes { get; set; }
            //public Font Font { get; private set; }
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

            public StyleContainer()
            {

            }

            public StyleContainer(Label l)
            {
                this.BackgroundColor = l.BackgroundColor;
                this.ForegroundColor = l.TextColor;
                this.FontAttributes = l.FontAttributes;
                this.FontFamily = l.FontFamily;
                this.FontSize = l.FontSize;
            }
        }
    }
}
