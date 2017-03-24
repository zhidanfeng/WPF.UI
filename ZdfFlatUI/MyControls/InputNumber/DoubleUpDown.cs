using System;
using System.Windows;
using System.Windows.Controls;
using ZdfFlatUI.BaseControl;

namespace ZdfFlatUI
{
    [TemplatePart(Name = "PART_ContentHost", Type = typeof(ScrollViewer))]
    [TemplatePart(Name = "PART_UP", Type = typeof(Button))]
    [TemplatePart(Name = "PART_DOWN", Type = typeof(Button))]
    public class DoubleUpDown : NumericUpDown<double>
    {
        public DoubleUpDown() : base()
        {
            this.Value = 0d;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.UpButtonClick = new UpButtonClickHandler(BtnUp_Click);
            this.DownButtonClick = new DownButtonClickHandler(BtnDown_Click);
            this.ValueChanged = new NumericUpDown<double>.ValueChangedHandler(CurrValueChanged);
        }

        private void BtnUp_Click()
        {
            if (this.Value < this.Maximum) //下一次增加后得到的值若大于最大值，则将其修改为最大值
            {
                double temp = this.Value + this.Increment;
                this.Value = (temp > this.Maximum) ? this.Maximum : temp;
            }
        }

        private void BtnDown_Click()
        {
            if (this.Value > this.Minimum) //下一次减少后得到的值若小于最小值，则将其修改为最小值
            {
                double temp = this.Value - this.Increment;
                this.Value = (temp < this.Minimum) ? this.Minimum : temp;
            }
        }

        /// <summary>
        /// 数值改变，判断其合法性
        /// </summary>
        /// <param name="obj"></param>
        private void CurrValueChanged(object obj)
        {
            if(this.IsReadOnly) //只有允许用户手动输入时才去校验，减少不必要的消耗
            {
                return;
            }

            try
            {
                if(!string.IsNullOrEmpty(Convert.ToString(obj)))
                {
                    double newValue = Convert.ToDouble(obj);
                    IsShowTip = false;
                    this.Value = newValue;

                    //判断用户输入的值，如果大于最大值或者小于最小值，则默认将其修改为最大值或者最小值
                    if (this.Value > this.Maximum)
                    {
                        this.Value = this.Maximum;
                        IsShowTip = true;
                        TipText = string.Format("您输入的数值为{0}，大于最大值{1}", newValue, this.Maximum);
                    }


                    if (this.Value < this.Minimum)
                    {
                        this.Value = this.Minimum;
                        IsShowTip = true;
                        TipText = string.Format("您输入的数值为{0}，小于最小值{1}", newValue, this.Minimum);
                    }
                }
            }
            catch (Exception)
            {
                IsShowTip = true;
                TipText = "输入格式有误，请重新输入";
            }
        }
    }
}
