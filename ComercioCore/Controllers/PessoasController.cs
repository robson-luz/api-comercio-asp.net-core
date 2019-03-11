using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComercioCore.Data;
using ComercioCore.Models.Pessoas;
using ComercioCore.ViewModel.Pessoas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComercioCore.Extensions;
using Microsoft.EntityFrameworkCore;
using ComercioCore.Models.Usuarios;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace ComercioCore.Controllers
{
    [Route("Pessoas/[action]")]
    public class PessoasController : Controller
    {
        private ComercioDbContext dbContext;

        public PessoasController(ComercioDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public bool CpfValido(string cpf)
        {
            //111.111.11-11 deve ser invalido também
            if (cpf.ElementAt(0) == cpf.ElementAt(1)
                && cpf.ElementAt(1) == cpf.ElementAt(2)
                && cpf.ElementAt(2) == cpf.ElementAt(4)
                && cpf.ElementAt(4) == cpf.ElementAt(5)
                && cpf.ElementAt(5) == cpf.ElementAt(6)
                && cpf.ElementAt(6) == cpf.ElementAt(8)
                && cpf.ElementAt(8) == cpf.ElementAt(9)
                && cpf.ElementAt(9) == cpf.ElementAt(10)
                && cpf.ElementAt(10) == cpf.ElementAt(12)
                && cpf.ElementAt(12) == cpf.ElementAt(13))
            {
                return false;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private void CriarSenhaHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Cadastrar(CadastrarPessoaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.MensagemErro(ModelState));
            }

            if (dbContext.Pessoa.ComCpf(viewModel.Cpf).Any())
                return BadRequest(this.MensagemErro("Este cpf já foi cadastrado."));
            if (dbContext.Pessoa.ComEmail(viewModel.Email).Any())
                return BadRequest(this.MensagemErro("Este e-mail já foi cadastrado."));
            if (dbContext.Usuario.ComLogin(viewModel.Login).Any())
                return BadRequest(this.MensagemErro("Já existe um usuário com este login."));

            byte[] senhaHash, senhaSalt;
            CriarSenhaHash(viewModel.Senha, out senhaHash, out senhaSalt);

            Usuario usuario = new Usuario()
            {
                Login = viewModel.Login,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt
            };

            dbContext.Add(usuario);

            Pessoa pessoa = new Pessoa()
            {
                IdUsuario = usuario.IdUsuario,
                TipoPessoa = viewModel.TipoPessoa,
                Nome = viewModel.Nome,
                Cpf = viewModel.Cpf,
                Email = viewModel.Email,
                NumeroCelular = viewModel.NumeroCelular
            };

            if (viewModel.Arquivo != null && viewModel.Arquivo.Length > 0)
            {
                if (!viewModel.Arquivo.FileName.Contains(".jpg")
                    && !viewModel.Arquivo.FileName.Contains(".png")
                    && !viewModel.Arquivo.FileName.Contains(".gif"))
                    return BadRequest(this.MensagemErro("A imagem de estar no formato 'jpg', 'png' ou 'gif'"));

                using (var arquivoBytes = new MemoryStream())
                {
                    viewModel.Arquivo.CopyTo(arquivoBytes);

                    pessoa.Foto = arquivoBytes.ToArray();
                }
            }

            dbContext.Add(pessoa);

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Cadastrado com sucesso."));
        }

        //[HttpPost]
        //public IActionResult Atualizar([FromBody] AtualizarPessoaViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(this.MensagemErro(ModelState));
        //    }

        //    Produto produto = dbContext
        //        .Produto
        //        .ComId(viewModel.IdProduto)
        //        .SingleOrDefault();

        //    if (produto == null)
        //        return NotFound(this.MensagemErro("Produto não encontrado."));

        //    Produto produtoBanco = dbContext
        //        .Produto
        //        .ComDescricao(viewModel.Descricao)
        //        .Where(c => c.IdProduto != viewModel.IdProduto)
        //        .SingleOrDefault();

        //    if (produtoBanco != null)
        //    {
        //        return BadRequest(this.MensagemErro("Já existe um produto com esta descrição."));
        //    }

        //    produto.IdCategoria = viewModel.IdCategoria;
        //    produto.Descricao = viewModel.Descricao;
        //    produto.Preco = viewModel.Preco;

        //    dbContext.Update(produto);

        //    dbContext.SaveChanges();

        //    return Ok(this.MensagemSucesso("Produto atualizado com sucesso."));
        //}

        [HttpPost]
        public IActionResult Detalhes([FromBody] IdPessoaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(this.MensagemErro(ModelState));

            Pessoa pessoa = dbContext
                .Pessoa
                .ComId(viewModel.IdPessoa)
                .SingleOrDefault();

            if (pessoa == null)
                return NotFound(this.MensagemErro("Pessoa não encontrada."));

            dynamic dadosPessoa = new
            {
                idPessoa = pessoa.IdPessoa,
                idUsuario = pessoa.IdUsuario,
                tipoPessoa = pessoa.TipoPessoa,
                nome = pessoa.Nome,
                cpf = pessoa.Cpf,
                email = pessoa.Email,
                numeroCelular = pessoa.NumeroCelular,
                arquivo = pessoa.Foto
            };

            return Json(new
            {
                dadosPessoa = dadosPessoa
            });
        }
    }
}
