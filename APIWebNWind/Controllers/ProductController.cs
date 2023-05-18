using Microsoft.AspNetCore.Mvc;
using APIWebNWind.Data;
using APIWebNWind.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace APIWebNWind.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly NorthwindContext contexto;

        public ProductController(NorthwindContext context)
        {
            contexto = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return contexto.Products.OrderBy(p => p.ProductName);

        }

        /*********************************************************************************************************************************************/



        //DEBE DE LLEVAR EL MISMO NOMBRE DE LA RUTA
        [HttpGet]
        [Route("GetNameAndPrice")]
        /*Este metodo va a regresar una lista en base a las instrucciones sql
         se crea un lista de tipo objeto y damos un nombre y esa lista va a devolver con el metodo get el nombre y el precio*/
        public IEnumerable<object> GetNameAndPrice()
        {
            IEnumerable<object> lista =  //LLENA LAS DOS PROPIEDADES  QUE UNICAMENTE QUEREMOS, EN ESTE CASO EL NOMBRE Y EL PRECIO
                from producto in contexto.Products  //de la tabla producto selecciona los productos
                select new //es de los ultimos que deberia aparecer el select 
                {
                    Name = producto.ProductName, //se trae de vuelta lo que hay en la base de datos y lo almacena en la variable Name
                    Price = producto.UnitPrice,   //se trae de vuelta lo que hay en la base de datos y lo almacena en la variable Price
                    Category = producto.Category.CategoryName   //aacede al objeto sin necesidad un join 

                };

            return lista;
        }

        /*********************************************************************************************************************************************/


        /*Este mismo metodo hace lo mismo que el anteriro  solamente que al darle un tipo
         *especifico de dato a la lista , traera totos los datos que contiene la tabla
         y no devolvera el producto y precio */
        [HttpGet]
        [Route("GetNameAndPrice2")]
        public IEnumerable<Product> GetNameAndPrice2()
        {

            IEnumerable<Product> listaP =  // SI LE DAMOS EXACTAMENTE EL NOMBRE DE LA PROPIEDAD VA A DEVOLVER TODO  EL ARREGLO DE LA LISTA
                from producto in contexto.Products
                select new Product() //Esto es una instancia de tipo productos para inicializar con los datos dentro de las llaves
                {
                    ProductName = producto.ProductName,
                    UnitPrice = producto.UnitPrice,
                };
            return listaP;
        }

        /*********************************************************************************************************************************************/
        //enum funcion de la sintaxis de metodos y sql con 



        /*********************************************************************************************************************************************/
        /* obtener el nombre del producto, categoria
        (nombre), existencia de todos los productos que estan activos con el uso de JOIN
         */

        [HttpGet]
        [Route("GetProductsNodiscontinued")]
        /*Este metodo va a regresar una lista en base a las instrucciones sql
         se crea un lista de tipo objeto y damos un nombre y esa lista va a devolver con el metodo get el nombre y el precio*/
        public IEnumerable<object> GetProductsNodiscontinued()
        {
            var result =
                from c in contexto.Categories
                join p in contexto.Products on c.CategoryId equals p.CategoryId
                where p.Discontinued == false
                select new
                {

                    Nombre = p.ProductName,
                    Categoria = c.CategoryName,
                    Existencia = p.UnitPrice

                };
                  return result;

        }

    
    }
}
