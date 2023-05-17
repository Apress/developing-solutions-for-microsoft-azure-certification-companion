# Working with Microsoft Identity

In this chapter, we cover working with Microsoft Identity.  This is the first part of our look at security with authentication and authorization.  In this chapter, we look at getting our solutions wired up to utilize the MSAL SDK and the Microsoft Graph, as well as wiring up our web applications to leverage Microsoft Identity.

## Identity and Access Management

The first part of the chapter helps you set up the identity and access management on the Web application deployed in chapter 4 is locked down

1. Make sure that you have a functional web application as deployed at the end of chapter 4.

    If you need to get this set up quickly, you can just grab the starter files from chapter 4 and deploy them.  Alternatively, Create your own MVC application with Individual Authorization and deploy to Azure.

1. Lock down the application

    Follow the steps in the book to lock down the application so that only authorized users will even be able to display the application for use (so that you have to be logged in to a valid tenant in order for the application to be served to you).

    Remember that this identity is not the same identity/authorization for `within` the website.  This is just to get authentication and authorization to just display the site at all.

1. Utilize a second App Registration to create a secure third-party login using Microsoft Identity

    The next part of the book talks about how to leverage Identity for a third-party login from Microsoft into the web application.  This is the same user identity that logs in to the tenant but now is also utilizing that same identity to incorporate into the web application.

    >**Note:** Make sure to set up everything correctly, including the endpoints for redirect

    Also remember that in order for this to work, you'll have to add a client secret and leverage that from the web application so that the project will work as expected.  You'll need to tweak the web solution to add the managed identity provider and add the secrets into your local usersecrets.json file as well as the configuration in the Azure App Service.

## Service Principals

In addition to Identity, another way that a solution can be authorized is via service principals.   For example, from Azure Dev Ops, service principals allow you to deploy your applications and even provision resources.

1. Follow the training in the book to see how to leverage a service principal from GitHub actions.

    Although GitHub Actions are not likely part of the AZ-204 Exam, seeing how to leverage this service principal is within the scope of what you may be asked to do as an Azure Developer, and is therefore potentially fair game for the exam.

1. Federated Credentials

    You should make sure to leverage this section to ensure you understand how to work with federated credentials.

## Managed Identities

This is another critical concept. They will come into scope in chapter 8.

## Authorization Flows

As a developer, you need to be in command of all of these.

## Working with MSAL via the .NET SDK

To get started, you need an App Registration, and you need those values in your user secrets for the application.

1. Complete the App Registration as outlined in the book

1. Get your Client ID and  Tenant ID

    Put the values into your `usersecrets.json` file (use the user-secrets-example.txt as a guide).

1. Fetch the values into variables in your code

    Get the tenant and client id values into variables.

1. Create the MSAL application

    Use the provided code in the text and sample app to build a PublicClientApplicationBuilder on Azure Public with redirect URI of http://localhost as entered into the App Registration.  

    >**Note**: Note the use of the AzureCloudInstance enumeration and the values available for selection.

1. Set the scopes

    Remember the scopes you ask for are important and you want to get just enough to do what you need without making the user feel like they are giving you too much permission

1. Acquire the token

    Acquire the token to prove you can now make requests as the logged-in user.  That Bearer token could be used to make any requests against the Azure ARM API with the authorization of the logged-in user.

## Working with the Microsoft Graph via the .NET SDK

The process to work with the graph is similar to the MSAL.  Review the book for more detail

1. As above, get tenant and client id

1. Next, create the PublicClientApplicaitonBuilder as before

1. Set the scopes just like the first method.

1. Use a device code authorization flow

    Create the DeviceCodeCredentialOptions then make a DeviceCodeCredential on those options

1. Create a GraphServiceClient

1. Use the GSC to request info about the logged in user.

## Directory.Build.props

The project is set to read all the versions from the Directory.Build.props file.  Use that to leverage which version you want to run and/or update any NuGet references to new versions that are out by the time you review this code.

The default version at time of publish is DN6.  You can leverage DN7 by changing the extension on the props to `dn6` then the `dn7` extension to `props` and the project will default to version 7 with the latest packages at the time of this publishing.  Later, you can change it to DN8 or DN9/10/11, etc and just update the other libraries as needed as well (check for updates in the Nuget Packages for Solution)

## Conclusion

Use this project to help you learn how to interact with the Microsoft Identity Platform and complete the work of chapter 7.
