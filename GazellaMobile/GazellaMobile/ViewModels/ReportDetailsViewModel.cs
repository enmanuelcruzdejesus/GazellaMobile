using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazellaMobile.ViewModels
{
    public class ReportDetailsViewModel
    {
       int _moduleId = 0;
       public ReportDetailsViewModel(int ModuleId)
       {
            _moduleId = ModuleId;
       }

       public Task<dynamic[]> Data
       {
            get
            {
                return App.ServiceClient.GetReports(_moduleId);
            }
       }


    }
}
