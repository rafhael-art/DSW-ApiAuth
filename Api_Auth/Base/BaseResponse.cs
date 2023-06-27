using System;
namespace Api_Auth.Base
{
	public class BaseResponse<T>
	{
		public bool Succes { get; set; }
		public string Mensaje { get; set; }
		public T Data { get; set; }
		public BaseResponse()
		{
			Succes = true;
			Mensaje = "Consulta Exitosa";
		}
	}
}

