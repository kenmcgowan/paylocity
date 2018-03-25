using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paylocity.Benefits.Registration.Api.Exceptions;
using Paylocity.Benefits.Registration.Api.Extensions;
using Paylocity.Benefits.Registration.Api.Interfaces;
using Paylocity.Benefits.Registration.Api.Models;
using System;
using System.Net;

namespace Paylocity.Benefits.Registration.Api.Controllers
{
    [Produces("application/json")]
    [EnableCors("allow-all-for-demo")]
    public class RegistrationController : Controller
    {
        private ILogger<RegistrationController> _logger;
        private IRegistrationService _registrationService;

        public RegistrationController(ILogger<RegistrationController> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        [HttpPost]
        [Route("benefits/registration/employees")]
        public IActionResult RegisterEmployee([FromBody] Person person)
        {
            try
            {
                var employee = _registrationService.RegisterEmployee(person);
                var url = $"registration/employees/{employee.Id}";
                return this.Created(url, employee);
            }
            catch (ArgumentException)
            {
                return BadRequest("Unable to register the employee; invalid parameters");
            }
            catch (Exception exception)
            {
                return HandleUnanticipatedException(exception);
            }
        }

        [HttpPost]
        [Route("benefits/registration/employees/{employeeId}/dependents")]
        public IActionResult RegisterEmployee(long employeeId, [FromBody] Person person)
        {
            try
            {
                var dependent = _registrationService.RegisterDependent(employeeId, person);
                var url = $"registration/employees/{dependent.EmployeeId}/dependents/{dependent.Id}";
                return Created(url, dependent);
            }
            catch (ArgumentException)
            {
                return BadRequest("Unable to register the dependent; invalid parameters");
            }
            catch (Exception exception)
            {
                return HandleUnanticipatedException(exception);
            }
        }

        [Route("benefits/registration/employees/{employeeId}/payperiods")]
        public IActionResult PreviewPayPeriods(long employeeId)
        {
            try
            {
                var result = new
                {
                    PayPeriods = _registrationService.PreviewPayPeriods(employeeId)
                };
                return Ok(result);
            }
            catch (InvalidRequestException)
            {
                return NotFound("The requested employee does not exist");
            }
            catch (Exception exception)
            {
                return HandleUnanticipatedException(exception);
            }
        }

        private IActionResult HandleUnanticipatedException(Exception exception)
        {
            _logger.LogError(exception.ToStringRecursive());
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
