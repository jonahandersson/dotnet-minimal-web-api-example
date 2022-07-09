using AdventureWorksMinimalAPIDemo.Components;
using DotNetMinimalAPIDemo.EntityClasses;

namespace DotNetMinimalAPIDemo.RouterClasses
{
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
            Logger.LogInformation("Getting all orders");
            return Results.Ok(GetAll());
        }


        private List<Order> GetAll()
        {
            return new List<Order> {
                new Order {
                    OrderId =  1,
                    OrderDetailsId=   Guid.NewGuid(),
                    OrderDate= DateTime.Now,
                    Status = "New",
                    CustomerId = 1
                    },
                   new Order {
                    OrderId =  2,
                    OrderDetailsId=   Guid.NewGuid(),
                    OrderDate= DateTime.Now,
                    Status = "Processing",
                    CustomerId = 2
                    },
                    new Order {
                    OrderId =  3,
                    OrderDetailsId=   Guid.NewGuid(),
                    OrderDate= DateTime.Now,
                    Status = "Cancelled"                    
                    }
                };
        }

        /// <summary>
        /// GET a single row of data,
        /// </summary>
        /// <returns>An IResult object</returns>
        protected virtual IResult Get(int id)
        {
            // Locate a single row of data
            Order? current = GetAll()
            .Find(c => c.OrderId == id);
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
        protected virtual IResult Post(Order entity)
        {
            // Generate a new ID
            entity.OrderId = GetAll()
            .Max(o => o.OrderId) + 1;
            // TODO: Insert into data store
            // Return the new object created
            return Results.Created(
            $"/{UrlFragment}/{entity.OrderId}", entity);
        }

        /// UPDATE existing data
        /// </summary>
        /// <returns>An IResult object</returns>
        protected virtual IResult Put(int id, Order entity)
        {
            IResult ret;
            // Locate a single row of data
            Order? currentOrder = GetAll()
            .Find(c => c.OrderId == id);
            if (currentOrder != null)
            {
                // TODO: Update the entity
                currentOrder.OrderId = entity.OrderId;
                currentOrder.OrderDate = entity.OrderDate;
                currentOrder.OrderDetailsId = entity.OrderDetailsId;
                currentOrder.Status = entity.Status;
                currentOrder.CustomerId = entity.CustomerId;
                // TODO: Update the data store
                // Return the updated entity
                ret = Results.Ok(currentOrder);
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
            Order? current = GetAll()
            .Find(c => c.OrderId == id);
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
            (Order entity) => Post(entity));

            app.MapPut($"/{UrlFragment}/{{id:int}}",
            (int id, Order entity) => Put(id, entity));

            app.MapDelete($"/{UrlFragment}/{{id:int}}",
            (int id) => Delete(id));
        }
    }

}
