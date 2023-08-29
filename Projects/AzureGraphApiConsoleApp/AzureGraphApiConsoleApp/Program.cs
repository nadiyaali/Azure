// See https://aka.ms/new-console-template for more information


/*This app will connect to Azure Active Directory(AAD) and query it through Graph API
1. First register this app in AAD App registration
2. No need to give a callback url
3. Give the app permission by scrolling to API permission, go to MS Graph, choose
    Application Permissions, go to Directory and click on Directory.Read.All
4. Give it access to Group, Group.ReadAll
5. Give it access to User, User.ReadAll
6. Click on "Grant admin consent for Default Directory"
7. Go to certificates and secrets blade, add new secret and create it for 90 days
8. Copy the secret ID and value  otherwise it will be gone
 value = ""

9. Get the Application(client) ID : 
10. Get the Directory(tenant) ID : 
11. Install the nuget Package Microsoft.Graph and Azure.Identity
12. Add in code to create a Graph Client and the access display names of all users
13. Better way to do it is to use Azure Managed Identities which can be of two typed:
System Managed and User Managed. System Managed ID is for one resource only, 
User Managed ID can be assigned to multiple resources. In this way, no secrets, passwords
or keys are required.
 */

using Azure.Identity;
using Microsoft.Graph;

// CODE SAMPLE
//https://learn.microsoft.com/en-us/graph/sdks/choose-authentication-providers?tabs=csharp

// The client credentials flow requires that you request the
// /.default scope, and pre-configure your permissions on the
// app registration in Azure. An administrator must grant consent
// to those permissions beforehand.
var scopes = new[] { "https://graph.microsoft.com/.default" };

// Values from app registration
// Multi-tenant apps can use "common",
// single-tenant apps must use the tenant ID from the Azure portal
var tenantId = "";

// Value from app registration
var clientId = "";

var clientSecret = "";


// using Azure.Identity;
var options = new ClientSecretCredentialOptions
{
    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
};

// https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
var clientSecretCredential = new ClientSecretCredential(
    tenantId, clientId, clientSecret, options);

var graphClient = new GraphServiceClient(clientSecretCredential, scopes);


// CANNOT GET THIS TO LIST ALL USERS AS IN UDEMU COURSE
// GET https://graph.microsoft.com/v1.0/me
//var myuser = await graphClient.Me
//    .GetAsync();
//Console.WriteLine(myuser.DisplayName);

var users = graphClient.Users.ToGetRequestInformation();

Console.WriteLine("All Users");
Console.WriteLine(users);
//foreach (var usr in users)
//{
//    Console.WriteLine(usr.name);
//}



Console.ReadKey();
