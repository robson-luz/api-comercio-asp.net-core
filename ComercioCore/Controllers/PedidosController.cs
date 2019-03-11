using ComercioCore.Data;
using ComercioCore.Extensions;
using ComercioCore.Models.Itens;
using ComercioCore.Models.Pedidos;
using ComercioCore.ViewModel.Pedidos;
using ComercioCore.ViewModel.Pessoas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Controllers
{
    [Route("Pedidos/[action]")]
    public class PedidosController : Controller
    {
        private ComercioDbContext dbContext;

        public PedidosController(ComercioDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpPost]
        public IActionResult CadastrarPedido([FromBody] CadastrarPedidoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(this.MensagemErro(ModelState));


            Pedido pedido = new Pedido()
            {
                IdPessoa = viewModel.IdPessoa,
                DataPedido = DateTime.Now,
                ValorTotal = viewModel.Total
            };

            pedido.Itens = new List<Item>();

            foreach (ItemViewModel item in viewModel.Itens)
            {
                pedido.Itens.Add(new Item()
                {
                    IdProduto = item.IdProduto,
                    Quantidade = item.Quantidade,
                    Subtotal = item.Subtotal
                });
            }

            dbContext.Add(pedido);

            dbContext.SaveChanges();

            return Ok(this.MensagemSucesso("Pedido finalizado com sucesso."));
        }

        [HttpPost]
        public IActionResult DaPessoa([FromBody] IdPessoaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(this.MensagemErro(ModelState));

            ICollection<Pedido> pedidos = dbContext
                .Pedido
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Where(p => p.IdPessoa == viewModel.IdPessoa)
                .OrderBy(p => p.IdPedido)
                .ToList();

            dynamic pedidosJson = new List<dynamic>();
            foreach (Pedido pedido in pedidos)
            {
                dynamic itensJson = new List<dynamic>();
                foreach (Item item in pedido.Itens)
                {
                    itensJson.Add(new
                    {
                        descricao = item.Produto.Descricao,
                        preco = item.Produto.Preco,
                        quantidade = item.Quantidade,
                        subtotal = item.Subtotal
                    });
                }

                pedidosJson.Add(new
                {
                    idPedido = pedido.IdPedido,
                    idPessoa = pedido.IdPessoa,
                    dataPedido = pedido.DataPedido.ToShortDateString(),
                    valorTotal = pedido.ValorTotal,
                    itens = itensJson,
                    visivel= false
                });
            }

            return Json(new
            {
                pedidos = pedidosJson
            });
        }
    }
}
