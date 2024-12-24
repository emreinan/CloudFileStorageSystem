using CloudFileStorageMVC;
using CloudFileStorageMVC.Util.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add<ExceptionAndToastFilter>();
}).AddNToastNotifyToastr();
builder.Services.AddClientMvcServices(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseNToastNotify();
app.UseDeveloperExceptionPage();

app.Run();
