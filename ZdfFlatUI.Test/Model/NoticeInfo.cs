using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZdfFlatUI.Test.Model
{
    public class NoticeInfo
    {
        public string Title { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// Type的值只能为Info、Success、Warn、Error
        /// </summary>
        public string Type { get; set; }
    }
}
