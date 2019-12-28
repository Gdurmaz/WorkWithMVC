using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.CommonLayer
{
    public class DefaultCommon : ICommon
    {
        public string GetCurrentUserame()
        {
            return "System-Blog";
        }
    }
}
