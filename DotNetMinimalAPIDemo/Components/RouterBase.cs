namespace AdventureWorksMinimalAPIDemo.Components
{
    public class RouterBase
    {
        public string UrlFragment;
        protected ILogger Logger;
        public virtual void AddRoutes(
        WebApplication app)
        {
        }
    }
}
