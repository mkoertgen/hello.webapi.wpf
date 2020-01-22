using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace hello.webapi.wpf
{
    public class SalesController : ApiController
    {
        private readonly IHaveSales _sales;

        public SalesController(IHaveSales sales)
        {
            if (sales == null) throw new ArgumentNullException(nameof(sales));
            _sales = sales;
        }

        public IEnumerable<Sale> Get()
        {
            return _sales.Sales.ToModel();
        }

        public IHttpActionResult GetSale(int id)
        {
            var sale = _sales.Sales.FirstOrDefault(p => p.Id == id);
            if (sale == null) return NotFound();
            return Ok(sale.ToModel());
        }

        public IHttpActionResult PostSale(Sale model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _sales.Sales.Add(model.ToViewModel());
            return CreatedAtRoute("DefaultApi", new {id = model.Id}, model);
        }

        public IHttpActionResult PutSale(int id, Sale model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != model.Id) return BadRequest();

            var viewModel = _sales.Sales.SingleOrDefault(s => s.Id == id);
            if (viewModel == null) return NotFound();

            viewModel.FromModel(model);
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteSale(int id)
        {
            var viewModel = _sales.Sales.SingleOrDefault(s => s.Id == id);
            if (viewModel == null) return NotFound();

            _sales.Sales.Remove(viewModel);

            return Ok(viewModel.ToModel());
        }
    }
}