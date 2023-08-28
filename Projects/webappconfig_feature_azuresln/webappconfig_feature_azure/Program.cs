// Needs Nuget package Microsoft.Azure.AppConfiguration.AspNetCore

//https://learn.microsoft.com/en-us/azure/azure-app-configuration/quickstart-aspnet-core-app?tabs=core6x

/* FOR USING CONFIGURATION EXPLORER FOR STORING KEY,VALUE PAIR
 1. Create a resource of type App Configuration in Azure
 2. When created, go to Configuration Explorer and create a key, value
 3. To access this key,value from here, go to Access Keys under Settings 
 4. Click on values and copy Connection String
 5. As an example, we display this value in Index.cshtml
 */

using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);


// FOR USING CONFIGURATION EXPLORER FOR STORING KEY,VALUE PAIR
// For getting key,value from Azure App Config

string appconfigString = "Endpoint=https://appconfig000.azconfig.io;Id=3RzK;Secret=gemvC6RXsKxwv87HndaoAv5gXD6+xRF+0x4+HwZtsR0=";

// Load configuration from Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(appconfigString);

////////////////////////////////////////////////////////////////////////

/* FOR USING FEATURE MANAGER
1. Install first nuget package and then this one Microsoft.FeatureManagement.AspNetCore
2. Add code down below
3. Create a new razor page STaging,cshtml, add code to it
4. Add code to Staging.cshtml too
5. Add code to _Layout.cshtml and Pages/_ViewImports.cshtml
6. When feature is enabled in azure, it wil show here in app
 */

// For Feature Mananagement
//https://learn.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-aspnet-core?tabs=core6x
// Same string as before
string featureConnString = "Endpoint=https://appconfig000.azconfig.io;Id=3RzK;Secret=gemvC6RXsKxwv87HndaoAv5gXD6+xRF+0x4+HwZtsR0=";

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(featureConnString)
           // Load all keys that start with `TestApp:` and have no label
           .Select("webapp:*", LabelFilter.Null)
           // Configure to reload configuration if the registered sentinel key is modified
           .ConfigureRefresh(refreshOptions =>
                refreshOptions.Register("webapp:Settings:Sentinel", refreshAll: true));

    // Load all feature flags with no label
    options.UseFeatureFlags();
});


// Add services to the container.
builder.Services.AddRazorPages();
////////////////////////////////////////////////////////////

// Add Azure App Configuration middleware to the container of services.
builder.Services.AddAzureAppConfiguration();


// Add feature management to the container of services.
builder.Services.AddFeatureManagement();


// Bind configuration "TestApp:Settings" section to the Settings object
//builder.Services.Configure<Settings>(builder.Configuration.GetSection("webapp:Settings"));
/////////////////////////////////////////////////////////////


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
