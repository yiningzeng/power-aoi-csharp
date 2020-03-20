using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace power_aoi.Tools
{
    class Aoi
    {
        // 图像匹配算子
        // img: 待搜索的图像
        // templ: 目标模板
        // pos: 匹配结果（输出），目标模板templ在img内的区域的左上角顶点位置
        // binarize: 是否在预处理中加二值化操作，默认不进行(false)
        // method: 匹配度指标
        // return: 匹配结果得分
        //     method使用默认时，值不大于1；得分越高说明匹配程度越高；反之，接近0可认为基本不匹配
        //     method=1时，值不小于0；得分越低说明匹配程度越高
        [DllImport("aoi.dll", EntryPoint = "marker_match_crop", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double marker_match_crop(IntPtr iplImage, IntPtr patch, ref Point point, ref Rectangle rectangle, bool binarize=false, int method=1, bool debug=false);

        [DllImport("aoi.dll", EntryPoint = "marker_match", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double marker_match(IntPtr iplImage, IntPtr patch, ref Point point);
    }
}
