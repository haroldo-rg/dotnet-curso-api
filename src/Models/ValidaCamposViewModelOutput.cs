using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Curso.Api.Models
{
    public class ValidaCamposViewModelOutput
    {
        public IEnumerable<string> Erros { get; private set; }

        public ValidaCamposViewModelOutput(IEnumerable<string> erros)
        {
            this.Erros = erros;
        }

        public static ValidaCamposViewModelOutput CreateFromViewModelState(ModelStateDictionary ModelState)
        {
            return new ValidaCamposViewModelOutput(ModelState.SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage));
        }
    }
}
