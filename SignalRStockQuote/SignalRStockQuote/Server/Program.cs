using Microsoft.AspNetCore.ResponseCompression;
using SignalRStockQuote.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add SignalR middleware services.
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
});
builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IQuotePump>(new QuotePump());
builder.Services.AddHostedService<QuoteWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapHub<QuoteHub>("/hubs/quotes");
app.MapFallbackToFile("index.html");

app.Run();
