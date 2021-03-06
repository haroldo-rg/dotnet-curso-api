using Curso.Api.Business.Entities;
using Curso.Api.Business.Repositories;
using Curso.Api.Filters;
using Curso.Api.Infrastructure.Data;
using Curso.Api.Models;
using Curso.Api.Models.Usuarios;
using Curso.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;

namespace Curso.Api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly CursoDbContext _context;
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IAuthenticationService authenticationService, CursoDbContext context, IUsuarioRepository repository)
        {
            _authenticationService = authenticationService;
            _context = context;
            _repository = repository;
        }

        /// <summary>
        /// Efetuar login
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Usuário autenticado com sucesso", type: typeof(ListarCursosViewModelOutput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios não preenchidos", type: typeof(ValidaCamposViewModelOutput))]
        [SwaggerResponse(statusCode: 404, description: "Usuário ou senha inválidos", type: typeof(ValidaCamposViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", type: typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            if(_repository.ValidarCredenciais(loginViewModelInput.Login, loginViewModelInput.Senha) == false)
                return NotFound(new ValidaCamposViewModelOutput(new string[] { "Usuário ou senha inválidos" }));

            var usuario = _repository.ObterPorLogin(loginViewModelInput.Login);

            var token = _authenticationService.GerarToken(new Usuario() {
                Codigo = usuario.Codigo,
                Login = usuario.Login,
                Email = usuario.Email
            });

            loginViewModelInput.Senha = null;

            return Ok(new ListarCursosViewModelOutput()
            {
                Token = token,
                Usuario = loginViewModelInput
            });
        }

        /// <summary>
        /// Registrar um usuário
        /// </summary>
        /// <param name="registroViewModelInput"></param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Usuário registrado com sucesso", type: typeof(RegistrarUsuarioViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios não preenchidos", type: typeof(ValidaCamposViewModelOutput))]
        [SwaggerResponse(statusCode: 409, description: "Já existe um usuário cadastrado com o login informado", type: typeof(ValidaCamposViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", type: typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistrarUsuarioViewModelInput registroViewModelInput)
        {

            if (_repository.ObterPorLogin(registroViewModelInput.Login) != null)
            {
                return Conflict(new ValidaCamposViewModelOutput(new string[] { "Já existe um usuário cadastrado com o login informado" }));
            }

            _repository.Adicinar(new Usuario()
            {
                Login = registroViewModelInput.Login,
                Email = registroViewModelInput.Email,
                Senha = registroViewModelInput.Senha
            });

            _repository.Commit();

            registroViewModelInput.Senha = null;

            return Created(string.Empty, registroViewModelInput);
        }

    }
}
