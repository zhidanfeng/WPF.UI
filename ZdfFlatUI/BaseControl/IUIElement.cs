using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZdfFlatUI.BaseControl
{
    public interface IUIElement : IDisposable
    {
        /// <summary>
        /// 注册事件
        /// </summary>
        void EventsRegistion();

        /// <summary>
        /// 解除事件注册
        /// </summary>
        void EventDeregistration();
    }
}
