using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api_Auth.Base;
using Api_Auth.Context;
using Api_Auth.Entity;
using Api_Auth.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api_Auth.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext appDbContext;
        private IConfiguration _configuration;

        public ClienteService(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this._configuration = configuration;
        }


        public async Task<BaseResponse<Empleado>> Add(Empleado empleado)
        {
            var response = new BaseResponse<Empleado>();
            await appDbContext.Empleados.AddAsync(empleado);
            await appDbContext.SaveChangesAsync();
            response.Data = empleado;
            return response;
        }

        public async Task<BaseResponse<bool>> Delete(int id )
        {
            var response = new BaseResponse<bool>();
            var usuarioDB = await appDbContext.Empleados.FirstOrDefaultAsync(x => x.Id == id);
            appDbContext.Empleados.Remove(usuarioDB);
            await appDbContext.SaveChangesAsync();
             response.Data = true;
            return response;    
        }

        public async Task<BaseResponse<List<Empleado>>> GetAll()
        {
            var response = new BaseResponse<List<Empleado>>();
            response.Data =  await appDbContext.Empleados.ToListAsync();
            return response;
        }

        public async Task<BaseResponse<Empleado>> GetById(int id)
        {
            var response = new BaseResponse<Empleado>();
             response.Data =  await appDbContext.Empleados.FirstOrDefaultAsync(x => x.Id == id);
            return response;
        }

        public async Task<BaseResponse<string>> Login(string username, string pasword)
        {
            var response = new BaseResponse<string>();
            var usernameFind = await appDbContext.Empleados.FirstOrDefaultAsync(x => x.Nombre == username);
            if (usernameFind is null)
            {
                response.Mensaje = "Usuario No encontrado";
                response.Succes = false;
            }
            else {
                var userFind = await appDbContext.Empleados.FirstOrDefaultAsync(x => x.Nombre == username && x.Password == pasword);
                if (userFind is null)
                {
                    response.Mensaje = "Contraseña Incorrecta";
                    response.Succes = false;
                }
                else
                {
                    response.Data = GenerateToken(userFind);
                }
            }
            return response;

        }

        public async Task<BaseResponse<bool>> Update(Empleado empleado)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            appDbContext.Empleados.Update(empleado);
            await appDbContext.SaveChangesAsync();
            response.Data = true;
            return response;
        }

        public string GenerateToken(Empleado user)
        {
            var securittyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var credencials = new SigningCredentials(securittyKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Nombre),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.Nombre),
                new Claim(JwtRegisteredClaimNames.GivenName,user.Nombre),
                new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,Guid.NewGuid().ToString(),ClaimValueTypes.Integer64),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"])),
                notBefore: DateTime.UtcNow,
                signingCredentials: credencials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

