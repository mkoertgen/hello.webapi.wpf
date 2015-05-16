using System;
using System.Collections.Generic;
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

        // TODO: get/id

        // put/post id

        // delete ...

    }
}