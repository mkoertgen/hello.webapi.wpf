using System.Collections.Generic;
using System.Web.Http;

namespace hello.webapi.wpf
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<Sale> Get()
        {
            return new List<Sale> { new Sale(){ SaleId = "4645", BuyerName = "John Smith"},
                new Sale(){ SaleId = "23455", BuyerName = "Mark Johnson"} };
        }
    }
}