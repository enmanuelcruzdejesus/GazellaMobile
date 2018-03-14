using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazellaMobile.ViewModels
{
    public class ReportParamViewModel
    {
        int _reportId = 0;
        public ReportParamViewModel(int ReportId)
        {
            _reportId = ReportId;
        }
        public int ReportId
        {
            get { return _reportId; }
        }
        public Task<dynamic[]> Data
        {
            get
            {
                return App.ServiceClient.GetReportParams(_reportId);
            }
        }

    }
}
