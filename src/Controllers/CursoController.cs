using Curso.Api.Business.Repositories;
using Curso.Api.Filters;
using Curso.Api.Models;
using Curso.Api.Models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Curso.Api.Controllers
{
    [Route("api/v1/curso")]
    [ApiController]
    [Authorize]
    public class CursoController : Controller
    {
        private readonly ICursoRepository _repository;

        public CursoController(ICursoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Listar os cursos do usuário
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Lista de cursos do usuário retornada com sucesso", type: typeof(CursoViewModelOutput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obriogatórios não preenchidos", type: typeof(ValidaCamposViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", type: typeof(ErroGenericoViewModel))]
        [HttpGet]
        [Route("")]
        public IActionResult Listar()
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var cursos = _repository.ObterPorUsuario(codigoUsuario)
                .Select(c => new CursoViewModelOutput()
                {
                    Nome = c.Nome,
                    Descricao = c.Descricao,
                    Login = c.Usuario.Login
                });

            return Ok(cursos);
        }

        /// <summary>
        /// Registrar um curso
        /// </summary>
        /// <param name="registroViewModelInput"></param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Curso registrado com sucesso", type: typeof(RegistrarCursoViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios não preenchidos", type: typeof(ValidaCamposViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", type: typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistrarCursoViewModelInput registroViewModelInput)
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            _repository.Adicinar(new Business.Entities.Curso()
            {
                Nome = registroViewModelInput.Nome,
                Descricao = registroViewModelInput.Descricao,
                CodigoUsuario = codigoUsuario
            });

            _repository.Commit();

            return Created(string.Empty, registroViewModelInput);
        }

    }
}
