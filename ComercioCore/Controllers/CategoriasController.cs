using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComercioCore.Data;
using ComercioCore.Models.Categorias;
using ComercioCore.ViewModel.Categorias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComercioCore.Extensions;

namespace ComercioCore.Controllers
{
    [Route("Categorias/[action]")]
    [Authorize]
    public class CategoriasController : Controller
    {
        private ComercioDbContext dbContext;

        public CategoriasController(ComercioDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CadastrarCategoriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.MensagemErro(ModelState));
            }

            Categoria categoriaBanco = dbContext
                .Categoria
                .ComDescricao(viewModel.Descricao)
                .SingleOrDefault();

            if (categoriaBanco != null)
            {
                return BadRequest(this.MensagemErro("Já existe uma categoria com esta descrição."));
            }

            Categoria categoria = new Categoria()
            {
                Descricao = viewModel.Descricao
            };

            dbContext.Add(categoria);

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Categoria cadastrada com sucesso."));
        }

        [HttpPost]
        public IActionResult Atualizar([FromBody] AtualizarCategoriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.MensagemErro(ModelState));
            }

            Categoria categoria = dbContext
                .Categoria
                .ComId(viewModel.IdCategoria)
                .SingleOrDefault();

            if (categoria == null)
                return NotFound(this.MensagemErro("Categoria não encontrada."));

            Categoria categoriaBanco = dbContext
                .Categoria
                .ComDescricao(viewModel.Descricao)
                .Where(c => c.IdCategoria != viewModel.IdCategoria)
                .SingleOrDefault();

            if (categoriaBanco != null)
            {
                return BadRequest(this.MensagemErro("Já existe uma categoria com esta descrição."));
            }

            categoria.Descricao = viewModel.Descricao;

            dbContext.Update(categoria);

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Categoria atualizada com sucesso."));
        }

        [HttpPost]
        public IActionResult Cadastradas([FromBody] CategoriasCadastradasViewModel viewModel)
        {
            IQueryable<Categoria> categoriasQuery = dbContext
                .Categoria
                .OndeDescricaoContem(viewModel.Descricao)
                .OrderBy(c => c.IdCategoria);

            ICollection<Categoria> categorias = categoriasQuery
                .Skip((viewModel.Paginacao.Inicio - 1) * viewModel.Paginacao.Limite)
                .Take(viewModel.Paginacao.Limite)
                .ToList();

            viewModel.Paginacao.TotalRegistros = categoriasQuery.ToList().Count;

            List<dynamic> categoriasJson = new List<dynamic>();

            foreach (Categoria categoria in categorias)
            {
                categoriasJson.Add(new
                {
                    idCategoria = categoria.IdCategoria,
                    descricao = categoria.Descricao
                });
            }

            return Json(new
            {
                categorias = categoriasJson,
                paginacao = viewModel.Paginacao
            });
        }

        [HttpPost]
        public IActionResult Remover([FromBody] IdCategoriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Categoria categoria = dbContext
                .Categoria
                .ComId(viewModel.IdCategoria)
                .SingleOrDefault();

            if (categoria == null)
            {
                return NotFound(this.MensagemErro("Categoria não encontrada."));
            }

            if (dbContext.Produto.Any(p => p.IdCategoria == categoria.IdCategoria))
            {
                return BadRequest(this.MensagemErro("Não é possível remover esta categoria pois ela está vinculada a um produto."));
            }

            dbContext.Remove(categoria);

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Categoria removida com sucesso."));
        }

        [HttpGet]
        public IActionResult Todas()
        {
            IQueryable<Categoria> categoriasQuery = dbContext
                .Categoria            
                .OrderBy(c => c.Descricao);

            List<dynamic> categoriasJson = new List<dynamic>();

            foreach (Categoria categoria in categoriasQuery)
            {
                categoriasJson.Add(new
                {
                    idCategoria = categoria.IdCategoria,
                    descricao = categoria.Descricao
                });
            }

            return Json(new
            {
                categorias = categoriasJson
            });
        }
    }
}
