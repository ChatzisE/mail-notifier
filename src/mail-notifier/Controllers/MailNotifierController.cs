using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mail_notifier.Services;
using mail_notifier.Models;
namespace mail_notifier.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailNotifierController : ControllerBase
    {

        private readonly ILogger<MailNotifierController> _logger;
        private SendMailService _service;

        public MailNotifierController(ILogger<MailNotifierController> logger,SendMailService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public ActionResult<string> SetUpVessel([FromBody]MailInfo args)
        {
            try
            {
                return _service.SetUpVessel(args);
            }
            catch (Exception x)
            {
                _logger.LogError(x.Message);
                throw x;
            }
        }

    }
}
