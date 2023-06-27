using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Auth.Entity
{
	public class Empleado
	{
		public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string CodigoEmpleado { get; set; } = null!;
        public string Password { get; set; } = null!;
        [NotMapped]
        public string? Token { get; set; }
    }
}

