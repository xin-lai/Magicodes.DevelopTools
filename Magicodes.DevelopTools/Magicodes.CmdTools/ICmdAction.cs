using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magicodes.CmdTools
{
    public interface ICmdAction
    {
        void Execute(object agrs);
    }
}
