using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.CommonLayer
{
    public static class RunCommonLayer
    {
        public static ICommon Common = new DefaultCommon();
    }
}
