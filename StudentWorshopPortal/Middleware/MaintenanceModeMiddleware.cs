namespace StudentWorshopPortal.Middleware
{
    public class MaintenanceModeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public MaintenanceModeMiddleware(
            RequestDelegate next,
            IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool maintenanceMode =
                _configuration.GetValue<bool>("ApplicationSettings:MaintenanceMode");

            bool isStaticFile =
                context.Request.Path.StartsWithSegments("/css") ||
                context.Request.Path.StartsWithSegments("/js") ||
                context.Request.Path.StartsWithSegments("/lib") ||
                context.Request.Path.StartsWithSegments("/images");

            bool isMaintenancePage =
                context.Request.Path.StartsWithSegments("/Home/Maintenance");

            if (maintenanceMode && !isStaticFile && !isMaintenancePage)
            {
                context.Response.Redirect("/Home/Maintenance");
                return;
            }

            await _next(context);
        }
    }
}