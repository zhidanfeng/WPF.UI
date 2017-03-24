using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ZdfFlatUI.Utils
{
    public class DoubleUtil
    {
        public static double DpiScaleX
        {
            get
            {
                int dx = 0;
                int dy = 0;
                GetDPI(out dx, out dy);
                if (dx != 96)
                {
                    return (double)dx / 96.0;
                }
                return 1.0;
            }
        }

        public static double DpiScaleY
        {
            get
            {
                int dx = 0;
                int dy = 0;
                GetDPI(out dx, out dy);
                if (dy != 96)
                {
                    return (double)dy / 96.0;
                }
                return 1.0;
            }
        }

        public static void GetDPI(out int dpix, out int dpiy)
        {
            dpix = 0;
            dpiy = 0;
            using (System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_DesktopMonitor"))
            {
                using (System.Management.ManagementObjectCollection moc = mc.GetInstances())
                {

                    foreach (System.Management.ManagementObject each in moc)
                    {
                        dpix = int.Parse((each.Properties["PixelsPerXLogicalInch"].Value.ToString()));
                        dpiy = int.Parse((each.Properties["PixelsPerYLogicalInch"].Value.ToString()));
                    }
                }
            }
        }
        public static bool GreaterThan(double value1, double value2)
        {
            return value1 > value2 && !DoubleUtil.AreClose(value1, value2);
        }

        public static bool AreClose(double value1, double value2)
        {
            if (value1 == value2)
            {
                return true;
            }
            double num = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * 2.2204460492503131E-16;
            double num2 = value1 - value2;
            return -num < num2 && num > num2;
        }

        public static bool IsZero(double value)
        {
            return Math.Abs(value) < 2.2204460492503131E-15;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct NanUnion
        {
            [FieldOffset(0)]
            internal double DoubleValue;
            [FieldOffset(0)]
            internal ulong UintValue;
        }

        public static bool IsNaN(double value)
        {
            DoubleUtil.NanUnion nanUnion = default(DoubleUtil.NanUnion);
            nanUnion.DoubleValue = value;
            ulong num = nanUnion.UintValue & 18442240474082181120uL;
            ulong num2 = nanUnion.UintValue & 4503599627370495uL;
            return (num == 9218868437227405312uL || num == 18442240474082181120uL) && num2 != 0uL;
        }
    }
}
