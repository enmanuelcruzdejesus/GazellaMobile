using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazellaMobile.ViewModels
{
    public class ReportsPageViewModel
    {
        public Task<dynamic[]> Data
        {
            get
            {
                return App.ServiceClient.GetReportModules();

            }
        }




    }
}
