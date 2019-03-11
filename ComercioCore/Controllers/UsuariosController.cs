using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComercioCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComercioCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ComercioCore.ViewModel.Usuarios;
using ComercioCore.Models.Usuarios;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ComercioCore.Helpers;
using Microsoft.Extensions.Options;
using ComercioCore.Models.Pessoas;

namespace ComercioCore.Controllers
{
    //[Authorize]
    [Route("Usuarios/[action]")]
    public class UsuariosController : Controller
    {
        private ComercioDbContext dbContext;
        private readonly AppSettings appSettings;

        public UsuariosController(ComercioDbContext _dbContext, IOptions<AppSettings> _appSettings)
        {
            dbContext = _dbContext;
            appSettings = _appSettings.Value;
        }

        private void CriarSenhaHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerificarSenhaHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] UsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.MensagemErro(ModelState));
            }

            Usuario usuarioBanco = dbContext
                .Usuario
                .ComLogin(viewModel.Login)
                .SingleOrDefault();

            if (usuarioBanco != null)
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

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Usuário cadastrado com sucesso."));
        }

        [HttpPost]
        public IActionResult Remover([FromBody] IdUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.MensagemErro(ModelState));
            }

            Usuario usuario = dbContext
                .Usuario
                .ComId(viewModel.IdUsuario)
                .SingleOrDefault();

            if (usuario == null)
            {
                return NotFound(this.MensagemErro("Usuário não encontrado."));
            }

            dbContext.Remove(usuario);

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Usuário removido com sucesso."));
        }

        public Usuario PorId(int id)
        {
            return dbContext.Usuario.Find(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Autenticar([FromBody] UsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.MensagemErro(ModelState));
            }

            Usuario usuario = dbContext
                .Usuario
                .ComLogin(viewModel.Login)
                .SingleOrDefault();

            if (usuario == null)
                return BadRequest(this.MensagemErro("Usuário/senha incorretos."));

            if (!VerificarSenhaHash(viewModel.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                return BadRequest(this.MensagemErro("Usuário/senha incorretos."));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.IdUsuario.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            Pessoa pessoa = dbContext
                .Pessoa
                .ComIdUsuario(usuario.IdUsuario)
                .SingleOrDefault();

            var usuarioJson = new
            {
                idUsuario = usuario.IdUsuario,
                idPessoa = pessoa.IdPessoa,
                login = usuario.Login,
                nome = pessoa.Nome,
                arquivo = pessoa.Foto,
                token = tokenString
            };

            return Ok(new
            {
                usuario = usuarioJson
            });
        }

        [Authorize]
        public IActionResult ManterSessao()
        {
            int idUsuario = int.Parse(this.User.Claims.First().Value);

            Usuario usuario = dbContext
                .Usuario
                .ComId(idUsuario)
                .SingleOrDefault();

            if (usuario == null)
                return BadRequest(this.MensagemErro("Sessão expirada."));
          
            Pessoa pessoa = dbContext
                .Pessoa
                .ComIdUsuario(usuario.IdUsuario)
                .SingleOrDefault();

            var usuarioJson = new
            {
                idUsuario = usuario.IdUsuario,
                idPessoa = pessoa.IdPessoa,
                login = usuario.Login,
                nome = pessoa.Nome,
                arquivo = pessoa.Foto
            };

            return Ok(new
            {
                usuario = usuarioJson
            });
        }
    }
}
