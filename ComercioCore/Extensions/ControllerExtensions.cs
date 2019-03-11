using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Extensions
{
    public static class ControllerExtensions
    {
        public static JsonResult MensagemSucesso(this Controller controller, string msg)
        {
            return new JsonResult(
                new { info = msg }
            );
        }

        public static JsonResult MensagemErro(this Controller controller, string msg)
        {
            var errorMessage = new List<dynamic>();

            errorMessage.Add(msg);

            return new JsonResult(
                new { errorMessage = errorMessage }
            );
        }

        public static JsonResult MensagemErro(this Controller controller, ModelStateDictionary modelState)
        {
            var errorMessage = new List<dynamic>();

            foreach (dynamic erro in modelState.Values.SelectMany(x => x.Errors).ToList())
            {
                errorMessage.Add(erro.ErrorMessage);
            }

            return new JsonResult(
                new { errorMessage = errorMessage }
            );
        }
    }
}
