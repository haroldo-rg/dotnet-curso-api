using System.ComponentModel.DataAnnotations;

namespace Curso.Api.Models.Cursos
{
    public class RegistrarCursoViewModelInput
    {
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Descricao é obrigatório")]
        public string Descricao { get; set; }
    }
}
