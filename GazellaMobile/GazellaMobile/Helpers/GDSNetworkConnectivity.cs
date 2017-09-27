using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace GazellaMobile.Helpers
{
    public class GDSNetworkConnectivity
    {
        public static bool isConneted()
        {
            if (CrossConnectivity.Current.IsConnected)
                return true;

            return false;
        }
    }
}
