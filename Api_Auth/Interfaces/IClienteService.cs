using System;
using Api_Auth.Base;
using Api_Auth.Entity;

namespace Api_Auth.Interfaces
{
	public interface IClienteService
    {
		public Task<BaseResponse<Empleado>> Add(Empleado empleado);
        public Task<BaseResponse<bool>> Update(Empleado empleado);
        public Task<BaseResponse<bool>> Delete(int id);
        public Task<BaseResponse<List<Empleado>>> GetAll();
        public Task<BaseResponse<Empleado>> GetById(int id);
        public Task<BaseResponse<string>> Login(string username , string pasword);
        public string GenerateToken(Empleado empleado);
    }
}

