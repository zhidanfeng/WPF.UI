using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace WPFRichTextEditor.Extensions
{
    public class EditorMethod
    {
        /// <summary>
        /// 判断文本是否加粗
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="pointer"></param>
        /// <returns></returns>
        public static bool IsBold(TextSelection selection, TextPointer pointer)
        {
            FontWeight fontWeightStart = FontWeights.Normal;
            FontWeight fontWeightEnd = FontWeights.Normal;

            try
            {
                //两种情形：1、直接将光标放在文本之间，即没有选中文本；2、鼠标有进行划选，即有选中文本
                if (!string.IsNullOrEmpty(selection.Text))
                {
                    //当鼠标滑动后，有选中的时候，判断之前与之后的文本格式，因为有可能选中的文本中包含了加粗与未加粗的文本，
                    //这个时候加粗按钮应该设置为未选中
                    fontWeightStart = ((System.Windows.Documents.TextElement)selection.Start.Parent).FontWeight;
                    fontWeightEnd = ((System.Windows.Documents.TextElement)selection.End.Parent).FontWeight;
                }
                else
                {
                    //获取光标所在位置的前一个文本
                    var range = new TextRange(pointer.GetPositionAtOffset(-1, LogicalDirection.Backward), pointer);
                    if (!string.IsNullOrEmpty(range.Text))
                    {
                        fontWeightStart = ((System.Windows.Documents.TextElement)range.Start.Parent).FontWeight;
                        fontWeightEnd = ((System.Windows.Documents.TextElement)range.Start.Parent).FontWeight;
                    }
                    else
                    {
                        //MessageBox.Show(range.Text);
                    }
                }
            }
            catch (Exception)
            {

            }

            //如果前面与后面的文本格式中有一个是正常的格式，则加粗按钮不选中，即返回false
            return (fontWeightStart == FontWeights.Normal || fontWeightEnd == FontWeights.Normal) ? false : true;
        }

        /// <summary>
        /// 判断文本是否为斜体
        /// </summary>
        /// <param name="a"></param>
        /// <param name="pointer">光标所在位置</param>
        /// <returns></returns>
        public static bool IsItalic(TextSelection a, TextPointer pointer)
        {
            var start = FontStyles.Normal;
            var end = FontStyles.Normal;

            try
            {
                if (!string.IsNullOrEmpty(a.Text))
                {
                    start = ((System.Windows.Documents.TextElement)a.Start.Parent).FontStyle;
                    end = ((System.Windows.Documents.TextElement)a.End.Parent).FontStyle;
                }
                else
                {
                    var range = new TextRange(pointer.GetPositionAtOffset(-1, LogicalDirection.Backward)
                        , pointer);
                    if (!string.IsNullOrEmpty(range.Text))
                    {
                        start = ((System.Windows.Documents.TextElement)range.Start.Parent).FontStyle;
                        end = ((System.Windows.Documents.TextElement)range.Start.Parent).FontStyle;
                    }
                    else
                    {
                        //MessageBox.Show(range.Text);
                    }
                }
            }
            catch (Exception)
            {

            }

            return (start == FontStyles.Normal || end == FontStyles.Normal) ? false : true;
        }

        public static bool IsUnderline(TextSelection selection, TextPointer pointer)
        {
            bool flag = false;

            try
            {
                if (!string.IsNullOrEmpty(selection.Text.Trim()) && selection.Text.Length > 3)
                {
                    var start = ((System.Windows.Documents.Inline)selection.Start.Parent).TextDecorations;
                    var end = ((System.Windows.Documents.Inline)selection.End.Parent).TextDecorations;
                    if (start.Count > 0 && start.FirstOrDefault(p => p.Location == TextDecorationLocation.Underline) != null
                        && end.Count > 0 && end.FirstOrDefault(p => p.Location == TextDecorationLocation.Underline) != null)
                    {
                        flag = true;
                    }
                }
                else
                {
                    var range = new TextRange(pointer.GetPositionAtOffset(-1, LogicalDirection.Backward), pointer);
                    var a = ((System.Windows.Documents.Inline)range.Start.Parent).TextDecorations;
                    if (!string.IsNullOrEmpty(range.Text.Trim()))
                    {
                        if (a.Count > 0 && a.FirstOrDefault(p => p.Location == TextDecorationLocation.Underline) != null)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        //MessageBox.Show(range.Text);
                    }
                }
            }
            catch (Exception)
            {
                //System.Windows.Documents.EditingCommands.ToggleBold
            }

            return flag;
        }

        public static bool IsStrikethrough(TextSelection selection, TextPointer pointer)
        {
            bool flag = false;

            try
            {
                if (!string.IsNullOrEmpty(selection.Text.Trim()) && selection.Text.Length > 3)
                {
                    var start = ((System.Windows.Documents.Inline)selection.Start.Parent).TextDecorations;
                    var end = ((System.Windows.Documents.Inline)selection.End.Parent).TextDecorations;
                    if (start.Count > 0 && start.FirstOrDefault(p => p.Location == TextDecorationLocation.Strikethrough) != null
                        && end.Count > 0 && end.FirstOrDefault(p => p.Location == TextDecorationLocation.Strikethrough) != null)
                    {
                        flag = true;
                    }
                }
                else
                {
                    var range = new TextRange(pointer.GetPositionAtOffset(-1, LogicalDirection.Backward), pointer);
                    var a = ((System.Windows.Documents.Inline)range.Start.Parent).TextDecorations;
                    if (!string.IsNullOrEmpty(range.Text.Trim()))
                    {
                        if (a.Count > 0 && a.FirstOrDefault(p => p.Location == TextDecorationLocation.Strikethrough) != null)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        //MessageBox.Show(range.Text);
                    }
                }
            }
            catch (Exception)
            {
                //System.Windows.Documents.EditingCommands.ToggleBold
            }

            return flag;
        }

        public static bool IsAlignLeft(TextSelection selection, TextPointer pointer)
        {
            try
            {
                var a = selection.Start.Paragraph.TextAlignment;
                if(a == TextAlignment.Left)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                
            }
            
            return false;
        }

        public static bool IsAlignCenter(TextSelection selection, TextPointer pointer)
        {
            try
            {
                var a = selection.Start.Paragraph.TextAlignment;
                if (a == TextAlignment.Center)
                {
                    return true;
                }
            }
            catch (Exception)
            {

            }

            return false;
        }

        public static bool IsAlignRight(TextSelection selection, TextPointer pointer)
        {
            try
            {
                var a = selection.Start.Paragraph.TextAlignment;
                if (a == TextAlignment.Right)
                {
                    return true;
                }
            }
            catch (Exception)
            {

            }

            return false;
        }

        public static string GetSelectionFontSize(TextSelection selection, TextPointer pointer)
        {
            var fontSize = "12";
            try
            {
                if (!string.IsNullOrEmpty(selection.Text.Trim()))
                {
                    fontSize = ((System.Windows.Documents.TextElement)selection.Start.Parent).FontSize.ToString();
                }
                else
                {
                    var range = new TextRange(pointer.GetPositionAtOffset(-1, LogicalDirection.Backward), pointer);
                    fontSize = ((System.Windows.Documents.TextElement)range.Start.Parent).FontSize.ToString();
                }
            }
            catch (Exception)
            {
                
            }
            return fontSize;
        }

        public static string GetSelectionFontFamily(TextSelection selection, TextPointer pointer)
        {
            var fontFamily = "12";
            try
            {
                if (!string.IsNullOrEmpty(selection.Text.Trim()))
                {
                    fontFamily = ((System.Windows.Documents.TextElement)selection.Start.Parent).FontFamily.ToString();
                }
                else
                {
                    var range = new TextRange(pointer.GetPositionAtOffset(-1, LogicalDirection.Backward), pointer);
                    fontFamily = ((System.Windows.Documents.TextElement)range.Start.Parent).FontFamily.ToString();
                }
            }
            catch (Exception)
            {

            }
            return fontFamily;
        }
    }
}
