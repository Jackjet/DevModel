using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Infrastructure
{
    public static class StringExtentions
    {
        public static string FormatMe(this string me, params object[] args) {
            return string.Format(me, args);
        }
    }
}
