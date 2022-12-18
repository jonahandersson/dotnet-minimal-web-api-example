using DotNetMinimalAPIDemo.EntityClasses;
using DotNetMinimalAPIDemo.Components;

namespace DotNetMinimalAPIDemo.RouterClasses
{
    /// <summary>
    /// Order Router inherits from the Router Base class
    /// </summary>
    public class OrderRouter: RouterBase
    {
        public OrderRouter(ILogger<OrderRouter> logger)
        {
            UrlFragment = "order";
            Logger = logger;
        }


        /// <summary>
        /// GET a collection of data
        /// </summary>
        /// <returns>An IResult object</returns>
        protected virtual IResult Get()
        {
            Logger.LogInformation("Getting all products");
            return Results.Ok(GetAll());
        }

        /// <summary>
        /// Get a collection of Product objects
        /// </summary>
        /// <returns>A list of Product objects</returns>
        protected virtual List<Product> GetAll()
        {
            Logger.LogInformation("Getting all Order");
            return new List<OrderItem> {
                new OrderItem {
                       OrderID = 1, OrderDate = DateTime.Now, CustomerId = 2, Quantity = 1, ProductId = 1 }
                                    
                };
        }

        /// <summary>
        /// GET a single row of data
        /// </summary>
        /// <returns>An IResult object</returns>
        protected virtual IResult Get(int id)
        {
            // Locate a single row of data
            Product? current = GetAll()
            .Find(p => p.ProductID == id);
            if (current != null)
            {
                return Results.Ok(current);
            }
            else
            {
                return Results.NotFound();
            }
        }

        /// <summary>
        /// INSERT new data
        /// </summary>
        /// <returns>An IResult object</returns>
        protected virtual IResult Post(Product entity)
        {
            // Generate a new ID
            entity.ProductID = GetAll()
            .Max(p => p.ProductID) + 1;
            // TODO: Insert into data store
            // Return the new object created
            return Results.Created(
            $"/{UrlFragment}/{entity.ProductID}", entity);
        }

        /// UPDATE existing data
        /// </summary>
        /// <returns>An IResult object</returns>
        protected virtual IResult Put(int id, Product entity)
        {
            IResult ret;
            // Locate a single row of data
            Product? current = GetAll()
            .Find(p => p.ProductID == id);
            if (current != null)
            {
                // TODO: Update the entity
                current.Name = entity.Name;
                current.Color = entity.Color;
                current.ListPrice = entity.ListPrice;
                // TODO: Update the data store
                // Return the updated entity
                ret = Results.Ok(current);
            }
            else
            {
                ret = Results.NotFound();
            }

            return ret;
        }

        /// <summary>
        /// DELETE a single row
        /// </summary>
        /// <returns>An IResult object</returns>
        protected virtual IResult Delete(int id)
        {
            IResult ret;
            // Locate a single row of data
            Product? current = GetAll()
            .Find(p => p.ProductID == id);
            if (current != null)
            {
                // TODO: Delete data from the data store
                GetAll().Remove(current);
                // Return NoContent
                ret = Results.NoContent();
            }
            else
            {
                ret = Results.NotFound();
            }
            return ret;
        }

        /// <summary>
        /// The AddRoutes() method calls the app.MapGet() method using the WebApplication app variable passed in from the Program.cs file.
        ///  The first parameter to the MapGet() method is  the route name the user sends the request to, such as http://
        /// localhost:nnnn/product or http://localhost:nnnn/customer
        //</summary>
        /// <param name="app">A WebApplication object</param>
        public override void AddRoutes(WebApplication app)
        {
            app.MapGet($"/{UrlFragment}", () => Get());

            app.MapGet($"/{UrlFragment}/{{id:int}}",
            (int id) => Get(id));

            app.MapPost($"/{UrlFragment}",
            (Product entity) => Post(entity));

            app.MapPut($"/{UrlFragment}/{{id:int}}",
            (int id, Product entity) => Put(id, entity));

            app.MapDelete($"/{UrlFragment}/{{id:int}}",
            (int id) => Delete(id));
        }
    }
}
