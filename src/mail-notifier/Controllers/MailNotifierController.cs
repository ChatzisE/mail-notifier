using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mail_notifier.Services;
using mail_notifier.Models;
using System.Net;

namespace mail_notifier.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailNotifierController : ControllerBase
    {

        private readonly ILogger<MailNotifierController> _logger;
        private SendMailService _service;

        public MailNotifierController(ILogger<MailNotifierController> logger, SendMailService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public ActionResult<string> SendMail([FromBody] MailInfo info)
        {
            try
            {
                var response = _service.CreateAndSendMail(info).Result;
                _logger.LogInformation(response.StatusCode.ToString());
                if (response.StatusCode == HttpStatusCode.Accepted)
                    return Ok("Mail Send succesfully");
                else throw new Exception(response.StatusCode.ToString());
            }
            catch (Exception x)
            {
                _logger.LogError(x.Message);
                throw x;
            }
        }

    }
}
