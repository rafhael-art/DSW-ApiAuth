using System;
using System.Security.Claims;
using Api_Auth.Base;
using Api_Auth.Entity;
using Api_Auth.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpleadoController :  ControllerBase
	{
		private readonly IClienteService clienteService;
        public EmpleadoController(IClienteService clienteService)
		{
			this.clienteService = clienteService;
		}

		[HttpPost]
        [AllowAnonymous]
		public async Task<ActionResult<BaseResponse<Empleado>>> Post([FromBody]Empleado empleado) {
			return await clienteService.Add(empleado);
		}

        [HttpPut]
        public async Task<ActionResult<BaseResponse<bool>>> Put([FromBody] Empleado empleado)
        {
            return await clienteService.Update(empleado);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(int id)
        {
            return await clienteService.Delete(id);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<Empleado>>>> GelAll()
        {
            return await clienteService.GetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseResponse<Empleado>>> GetByID([FromRoute] int id)
        {
            return await clienteService.GetById(id);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse<string>>> GetByID([FromBody] Empleado empleado)
        {
            return await clienteService.Login(empleado.CodigoEmpleado,empleado.Password);
        }

        [HttpGet("validarToken")]
        [AllowAnonymous]
        public async Task<ActionResult<BaseResponse<Empleado>>> GetUserByToken()
        {

            var identntity = HttpContext.User.Identity as ClaimsIdentity;
            var userclaims = identntity.Claims;
            var id = userclaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            var usuario = await clienteService.GetById(Convert.ToInt32(id));
            string nuevoToken = clienteService.GenerateToken(usuario.Data);
            BaseResponse<Empleado> response = new BaseResponse<Empleado>();
            response.Data = usuario.Data;
            response.Data.Token = nuevoToken;
            return response;

        }
    }
}

