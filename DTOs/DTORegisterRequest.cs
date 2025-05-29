﻿namespace digitalArsv1.DTOs
{
    public class RegisterRequest
    {
        public string Mail { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }

        public string Direccion { get; set; }

        public string Telefono{ get; set; }
        public string Tipo_cliente { get; set; }

    }

    public class LoginDTO
    {
        public string mail { get; set; }
        public string Password { get; set; }
    }

    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string mail { get; set; }
        public string Nombre { get; set; }
        public string tipo_cliente { get; set; }
    }
}