using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;
using System.Data;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/report")]
    [ApiController]
    public class ReportController : Controller
    {

        public IActionResult Get()
        {
            return StiNetCoreViewer.ProcessRequestResult(this);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var requestParams = StiNetCoreViewer.GetRequestParams(this);
            switch (requestParams.Action)
            {
                case StiAction.GetReport:
                    return GetReportResult();

                default:
                    return StiNetCoreViewer.ProcessRequestResult(this);
            }
        }

        [HttpPut]
        public void Put()
        {
        }

        [HttpDelete]
        public void Delete()
        {
        }

        private IActionResult GetReportResult()
        {
            var dataSet = new DataSet();
            dataSet.ReadXml(StiNetCoreHelper.MapPath(this, "Reports/Data/Demo.xml"));

            var report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "Reports/TwoSimpleLists.mrt"));
            report.RegData(dataSet);

            return StiNetCoreViewer.GetReportResult(this, report);
        }
    }
}
