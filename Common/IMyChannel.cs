using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Common
{
    public interface IMyChannel : IMyContract, IClientChannel
    {

    }
}
