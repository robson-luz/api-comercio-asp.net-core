using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComercioCore.Data;
using ComercioCore.Models.Produtos;
using ComercioCore.ViewModel.Produtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComercioCore.Extensions;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ComercioCore.Controllers
{
    [Route("Produtos/[action]")]
    public class ProdutosController : Controller
    {
        private ComercioDbContext dbContext;

        public ProdutosController(ComercioDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarProdutoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.MensagemErro(ModelState));
            }

            Produto produtoBanco = dbContext
                .Produto
                .ComDescricao(viewModel.Descricao)
                .SingleOrDefault();

            if (produtoBanco != null)
            {
                return BadRequest(this.MensagemErro("Já existe um produto com esta descrição."));
            }

            Produto produto = new Produto()
            {
                IdCategoria = viewModel.IdCategoria,
                Descricao = viewModel.Descricao,
                Preco = viewModel.Preco
            };

            if (viewModel.Arquivo != null && viewModel.Arquivo.Length > 0)
            {
                if (!viewModel.Arquivo.FileName.Contains(".jpg")
                    && !viewModel.Arquivo.FileName.Contains(".png")
                    && !viewModel.Arquivo.FileName.Contains(".gif"))
                {
                    return BadRequest(this.MensagemErro("A imagem de estar no formato 'jpg', 'png' ou 'gif'"));
                }

                var diretorio = "C:\\ArquivosComercio";

                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                var path = Path.Combine(
                diretorio,
                viewModel.Arquivo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    viewModel.Arquivo.CopyTo(stream);
                }

                produto.DiretorioImagem = path;
            }

            dbContext.Add(produto);

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Produto cadastrado com sucesso."));
        }

        [HttpPost]
        public IActionResult Atualizar(AtualizarProdutoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.MensagemErro(ModelState));
            }

            Produto produto = dbContext
                .Produto
                .ComId(viewModel.IdProduto)
                .SingleOrDefault();

            if (produto == null)
                return NotFound(this.MensagemErro("Produto não encontrado."));

            Produto produtoBanco = dbContext
                .Produto
                .ComDescricao(viewModel.Descricao)
                .Where(c => c.IdProduto != viewModel.IdProduto)
                .SingleOrDefault();

            if (produtoBanco != null)
            {
                return BadRequest(this.MensagemErro("Já existe um produto com esta descrição."));
            }

            produto.IdCategoria = viewModel.IdCategoria;
            produto.Descricao = viewModel.Descricao;
            produto.Preco = viewModel.Preco;

            if (viewModel.Arquivo != null && viewModel.Arquivo.Length > 0)
            {
                if (!viewModel.Arquivo.FileName.Contains(".jpg")
                    && !viewModel.Arquivo.FileName.Contains(".png")
                    && !viewModel.Arquivo.FileName.Contains(".gif"))
                {
                    return BadRequest(this.MensagemErro("A imagem de estar no formato 'jpg', 'png' ou 'gif'"));
                }

                var diretorio = "C:\\ArquivosComercio";

                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                var path = Path.Combine(
                diretorio,
                viewModel.Arquivo.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    viewModel.Arquivo.CopyTo(stream);
                }

                produto.DiretorioImagem = path;
            }

            dbContext.Update(produto);

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Produto atualizado com sucesso."));
        }

        [HttpPost]
        public IActionResult Cadastrados([FromBody] ProdutosCadastradosViewModel viewModel)
        {
            IQueryable<Produto> produtosQuery = dbContext
                .Produto
                .Include(p => p.Categoria)
                .OndeDescricaoContem(viewModel.Descricao)
                .ComIdCategoria(viewModel.IdCategoria)
                .OrderBy(c => c.IdProduto);

            ICollection<Produto> produtos = produtosQuery
                .Skip((viewModel.Paginacao.Inicio - 1) * viewModel.Paginacao.Limite)
                .Take(viewModel.Paginacao.Limite)
                .ToList();

            viewModel.Paginacao.TotalRegistros = produtosQuery.ToList().Count;

            List<dynamic> produtosJson = new List<dynamic>();

            foreach (Produto produto in produtos)
            {
                dynamic memory = null;

                if (!String.IsNullOrEmpty(produto.DiretorioImagem))
                {
                    var path = produto.DiretorioImagem;

                    memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        stream.CopyTo(memory);
                    }
                    memory.Position = 0;
                }

                produtosJson.Add(new
                {
                    idProduto = produto.IdProduto,
                    idCategoria = produto.IdCategoria,
                    categoria = produto.Categoria.Descricao,
                    preco = produto.Preco,
                    descricao = produto.Descricao,
                    arquivo = memory != null ? memory.GetBuffer() : null
                });
            }

            return Json(new
            {
                produtos = produtosJson,
                paginacao = viewModel.Paginacao
            });
        }

        [HttpPost]
        public IActionResult Remover([FromBody] IdProdutoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.MensagemErro(ModelState));
            }

            Produto produto = dbContext
                .Produto
                .ComId(viewModel.IdProduto)
                .SingleOrDefault();

            if (produto == null)
            {
                return NotFound(this.MensagemErro("Produto não encontrado."));
            }

            dbContext.Remove(produto);

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Produto removido com sucesso."));
        }

    }
}
