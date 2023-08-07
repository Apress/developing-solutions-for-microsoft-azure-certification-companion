# Simple MVC Web 

## Chapter 4 related

If you completed chapter 4, utilize your existing files.  If not, you can use this set of files.  Note that the following steps should have been completed during chapter 4

### Create a basic database

You'll need to utilize a database if you want to do the authorization stuff later in the book.  For now, you could skip this, but if you deploy a Basic Azure SQL database, it will run about $5/month.

1. Deploy the Basic SQL Database

1. Manually migrate the database or just uncomment the code to enable automatic migrations (in the Program.cs file).

### Create the App Service Plan and App Service

Feel free to create the plan first, or just create the plan while deploying, your choice.  

If you want to utilize slots and scaling (good practice for the exam), you'll need to deploy an S1 or better instance.  If you just want to deploy the application so that you can work with it in the most basic form (no slots, no scaling), use the Free (F1) deployment.

1. Create your App Service (and Plan)

1. Right-Click and Publish or set up CI/CD

    Either just publish it from your machine or set up CI/CD to your GitHub (or other GIT service).

1. If using the database, ensure the migrations (manual by connecting from your machine and running them, or via code in Program.cs)

1. Validate the site is working

### Review the text

Follow the text to do anything else you want to learn about with the site, including:

1. Deployment Slots and Swapping, directing traffic to a slot
1. Scaling the app

### Important

This website and code is used in a couple of chapters (8 and 10).  Those chapters build on this site.  If you want practice or you don't want to keep services alive, feel free to destroy the web app when you are done with the chapter.  Otherwise, leave the site in place until you have completed chapters 8 and 10

- [ ] Configure your application insights (Utilized in Chapter 10, but deployed with the App Service)
- [ ] Most of the code is present, a lot is commented out until you get to later chapters (security for KeyVault, working with Azure App Configuration, Redis Cache, etc)
- [ ] make sure to check the connection string and update the database if using from this point on
- [ ] Review settings in Directory.Build.props for package versions (use Nuget package manager to update)

## Chapter 7

If you skipped chapter 7, you don't need to turn on the identity for this chapter to work.  Therefore, you can leave the code commented out regarding chapter 7 in the Program.cs file (if it's not commented out, just leave it alone, if it's commented out, just leave it alone).

## Chapter 8

Assuming you are done with chapter 4 work and have your own files, you won't need these solution files.  If you are just trying to start here, note that the default files for chapter 4 are used to start this chapter too, because at this point I'm assuming you did not do chapter 4 if you are using my files.

Please note that the default does not:

- Implement Identity (see the comments in Program.cs for chapter 7).  Leave this alone if you have skipped chapter 7 and/or you are not set up for working with Identity at this time.

- Automate migrations (you need a database configured, and if you want to use identity in your app you'll need to implement that)
- require any caching or content delivery (you will implement caching in the next chapter)

### Step 1, Learn about Managed Identities

In order to complete the work for this chapter, you need to work with Managed Identities.  Make sure that you review the first part of the chapter to learn about identities.  If you have questions about some of the directions that follow, make sure you've reviewed the learning because the answer is likely in the book already.

### Step 2, Key Vault

Once you have learned about identities, it's time to create a Key Vault.  You will then connect your deployed App Service to the Key Vault to retrieve the secret information

1. Make sure you have a valid app service deployment

    You should already have (or you need to do it now) an active app service in your subscription with the provided code.  The default page should be showing both a local value that is "shared" and a "secret" value.  You will be moving the secret value to Key vault soon

1. Create the Azure Key Vault

1. Add a value for the secret into KeyVault

    Retrive the URI 
    Leverage the URI to create a reference from the App Service to the Key Vault Secret

    >**Note**: You will see an error at this point, and the page will just show the secret URI instead of the secret value.  This is expected.

1. Create the managed identity for the app service

1. Add an access policy in Key Vault for the Managed Identity for the App Service

    After a few minutes with this correctly implemented, restart your web application

    You will now see the value is retrieved and viewing on the page will show the value from the Key Vault Secret

### Step 3: Azure App Configuration

In this step, you'll move the shared item to the App Configuration, and then you will create a shared secret leveraged from Key Vault through the Azure App Configuration.  In the process, you'll see how to manage the code to connect to both the Azure App Configuration and the Azure Key Vault from your App Service.

>**Note:** this is a high-level overview and does not go in exactly the same order as the book.  I recommend you walk through the pages of the book with this code and use this guide as a summary of the things you've accomplished.

1. Create the Azure App Configuration

1. Add the shared value as a shared key/value pair

1. Create a managed identity for the App Configuration

1. Add the App Configuration managed identity to the access policy for Key Vault

1. Add the secret value to the App Configuration as a KeyVault Reference

1. Add a reference to the app configuration from the app service; remove or rename the original keys.

1. Add the managed identity of the app service as an App Config Data Reader role on the App Configuration.

1. Utilize the code to connect to the App Service and Key Vault from the App Service.

## Directory.Build.props

The project is set to read all the versions from the Directory.Build.props file.  Use that to leverage which version you want to run and/or update any NuGet references to new versions that are out by the time you review this code.

The default version at time of publish is DN6.  You can leverage DN7 by changing the extension on the props to `dn6` then the `dn7` extension to `props` and the project will default to version 7 with the latest packages at the time of this publishing.  Later, you can change it to DN8 or DN9/10/11, etc and just update the other libraries as needed as well (check for updates in the Nuget Packages for Solution)

## Conclusion

Use this project to help you learn how to interact with the Azure App Configuration and Azure Key Vault
