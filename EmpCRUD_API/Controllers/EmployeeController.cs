using AdminEntity.Models;
using AdminEntity.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace EmpCRUD_API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IJWTAuthenticationManager _jwtAuthenticationManager;
        IConfiguration _config;
        string _connection;
        public EmployeeController(IJWTAuthenticationManager jWTAuthenticationManager1, IConfiguration config)
        {
            _jwtAuthenticationManager = jWTAuthenticationManager1;
            _config = config;

            _connection = _config.GetValue<string>(
                "ConnectionStrings:Default");
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<EMPLOYEES> lstEmployees = EMPLOYEES_SERVICE.LoadAll(_connection);
            return Ok(lstEmployees);
        }


        [HttpPost]
        public IActionResult addEmployee(EMPLOYEES EMPLOYEES)
        {
            EMPLOYEES_SERVICE.Insert(EMPLOYEES, _connection);
            return Ok(EMPLOYEES);
        }
        [HttpPut]
        public IActionResult updateEmployee(EMPLOYEES EMPLOYEES)
        {
            EMPLOYEES_SERVICE.Update(EMPLOYEES, _connection);
            return Ok(EMPLOYEES);
        }
        [HttpPost]
        public IActionResult deleteEmployee(EMPLOYEES EMPLOYEES)
        {
            EMPLOYEES_SERVICE.Delete(EMPLOYEES, _connection);
            return Ok(EMPLOYEES);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(Users usersdata)
        {
            var token = _jwtAuthenticationManager.Authenticate(usersdata);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
