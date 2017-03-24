using System.Windows.Input;

namespace WPFRichTextEditor.Command
{
    public static class HtmlEditingCommands
    {
        #region 文本编辑命令

        /// <summary>
        /// 撤销命令
        /// </summary>
        public static RoutedUICommand Undo { get { return _undo; } }

        /// <summary>
        /// 重做命令
        /// </summary>
        public static RoutedUICommand Redo { get { return _redo; } }

        /// <summary>
        /// 剪切命令
        /// </summary>
        public static RoutedUICommand Cut { get { return _cut; } }

        /// <summary>
        /// 复制命令
        /// </summary>
        public static RoutedUICommand Copy { get { return _copy; } }

        /// <summary>
        /// 粘贴命令
        /// </summary>
        public static RoutedUICommand Paste { get { return _paste; } }

        /// <summary>
        /// 删除命令
        /// </summary>
        public static RoutedUICommand Delete { get { return _delete; } }

        /// <summary>
        /// 全选命令
        /// </summary>
        public static RoutedUICommand SelectAll { get { return _selectAll; } }

        #endregion

        #region 文本样式命令

        /// <summary>
        /// 设置粗体命令
        /// </summary>
        public static RoutedUICommand Bold { get { return _bold; } }

        /// <summary>
        /// 设置斜体命令
        /// </summary>
        public static RoutedUICommand Italic { get { return _italic; } }

        /// <summary>
        /// 设置下划线命令
        /// </summary>
        public static RoutedUICommand Underline { get { return _underline; } }

        /// <summary>
        /// 设置下标命令
        /// </summary>
        public static RoutedUICommand Subscript { get { return _subscript; } }

        /// <summary>
        /// 设置上标命令
        /// </summary>
        public static RoutedUICommand Superscript { get { return _superscript; } }

        /// <summary>
        /// 清除样式命令
        /// </summary>
        public static RoutedUICommand ClearStyle { get { return _clearStyle; } }
 
        #endregion

        #region 文本格式命令

        /// <summary>
        /// 增加缩进命令
        /// </summary>
        public static RoutedUICommand Indent { get { return _indent; } }

        /// <summary>
        /// 减少缩进命令
        /// </summary>
        public static RoutedUICommand Outdent { get { return _outdent; } }

        /// <summary>
        /// 无序列表命令
        /// </summary>
        public static RoutedUICommand BubbledList { get { return _bubbledList; } }

        /// <summary>
        /// 有序列表命令
        /// </summary>
        public static RoutedUICommand NumericList { get { return _numericList; } }

        /// <summary>
        /// 左对齐命令
        /// </summary>
        public static RoutedUICommand JustifyLeft { get { return _justifyLeft; } }

        /// <summary>
        /// 右对齐命令
        /// </summary>
        public static RoutedUICommand JustifyRight { get { return _justifyRight; } }

        /// <summary>
        /// 中间对齐命令
        /// </summary>
        public static RoutedUICommand JustifyCenter { get { return _justifyCenter; } }

        /// <summary>
        /// 两端对齐命令
        /// </summary>
        public static RoutedUICommand JustifyFull { get { return _justifyFull; } }
 
        #endregion

        #region 插入对象命令

        /// <summary>
        /// 插入超链接命令
        /// </summary>
        public static RoutedUICommand InsertHyperlink { get { return _insertHyperlink; } }

        /// <summary>
        /// 插入图像命令
        /// </summary>
        public static RoutedUICommand InsertImage { get { return _insertImage; } }

        /// <summary>
        /// 插入表格命令
        /// </summary>
        public static RoutedUICommand InsertTable { get { return _insertTable; } }

        /// <summary>
        /// 插入代码段命令
        /// </summary>
        public static RoutedUICommand InsertCodeBlock { get { return _insertCodeBlock; } }

        /// <summary>
        /// 插入换行符命令
        /// </summary>
        public static RoutedUICommand InsertLineBreak { get { return _insertLineBreak; } }

        /// <summary>
        /// 插入段落命令
        /// </summary>
        public static RoutedUICommand InsertParagraph { get { return _insertParagraph; } } 

        #endregion

        #region 非公开字段

        static RoutedUICommand _undo = new RoutedUICommand();
        static RoutedUICommand _redo = new RoutedUICommand();
        static RoutedUICommand _cut = new RoutedUICommand();
        static RoutedUICommand _copy = new RoutedUICommand();
        static RoutedUICommand _paste = new RoutedUICommand();
        static RoutedUICommand _delete = new RoutedUICommand();
        static RoutedUICommand _selectAll = new RoutedUICommand();

        static RoutedUICommand _bold = new RoutedUICommand();
        static RoutedUICommand _italic = new RoutedUICommand();
        static RoutedUICommand _underline = new RoutedUICommand();
        static RoutedUICommand _subscript = new RoutedUICommand();
        static RoutedUICommand _superscript = new RoutedUICommand();
        static RoutedUICommand _clearStyle = new RoutedUICommand();

        static RoutedUICommand _indent = new RoutedUICommand();
        static RoutedUICommand _outdent = new RoutedUICommand();
        static RoutedUICommand _bubbledList = new RoutedUICommand();
        static RoutedUICommand _numericList = new RoutedUICommand();
        static RoutedUICommand _justifyLeft = new RoutedUICommand();
        static RoutedUICommand _justifyRight = new RoutedUICommand();
        static RoutedUICommand _justifyCenter = new RoutedUICommand();
        static RoutedUICommand _justifyFull = new RoutedUICommand();

        static RoutedUICommand _insertHyperlink = new RoutedUICommand();
        static RoutedUICommand _insertImage = new RoutedUICommand();
        static RoutedUICommand _insertTable = new RoutedUICommand();
        static RoutedUICommand _insertCodeBlock = new RoutedUICommand();
        static RoutedUICommand _insertLineBreak = new RoutedUICommand();
        static RoutedUICommand _insertParagraph = new RoutedUICommand(); 

        #endregion
     }
}
