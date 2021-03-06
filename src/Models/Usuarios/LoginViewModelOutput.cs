using System.ComponentModel.DataAnnotations;

namespace Curso.Api.Models.Usuarios
{
    public class ListarCursosViewModelOutput
    {
        public string Token { get; set; }

        public LoginViewModelInput Usuario { get; set; }
    }
}
