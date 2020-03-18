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
        [DllImport("aoi.dll", EntryPoint = "marker_match_crop", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double marker_match_crop(IntPtr iplImage, IntPtr patch, ref Point point, ref Rectangle rectangle);

        [DllImport("aoi.dll", EntryPoint = "marker_match", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern double marker_match(IntPtr iplImage, IntPtr patch, ref Point point);
    }
}
