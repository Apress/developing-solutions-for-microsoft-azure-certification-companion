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

If you skipped chapter 8, you don't need to turn on the sections regarding the use of the Azure App Configuration and Key Vault.  For this reason, the code is once again the same starter files that existed at the start of the book.  

Please note that the default does not:

- Implement Identity (see the comments in Program.cs for chapter 7).  Leave this alone if you have skipped chapter 7 and/or you are not set up for working with Identity at this time.

- Automate migrations (you need a database configured, and if you want to use identity in your app you'll need to implement that)
- Implement any of the secure solutions code for Azure App Configuration and/or Azure Key Vault.

## Notes

Assuming you have been working through the chapters, you already have your code in a good place.  Assuming you want to just do this chapter, you can turn on the sections that are relevant to this project.  You will need to either mock the response of the database or implement a database to get the states data for caching.  If you don't want to implement a database, just create a list of say 10 or so states and return that as mocked data, and then implement code to invalidate the cache, add/edit/delete against your mock data.  

## Chapter 9

To complete this chapter, you will need to configure a CDN and then an Azure Cache for Redis.

### Part 1: CDN

There is no code for this part, so just work through the concepts in the book.  

1. If you want to go deeper, follow the code links from the book to learn about working with code.  

1. Complete the Learn modules.  At the time of this writing they were using deprecated code but will likely be updated by the time you are working through this book.


### Part 2: Cache For Redis

To get started, complete the deployment

### Deploy a Redis Cache

1. Deploy an Azure Cache for Redis (also mentioned in the simple console app)

    You can use the same one from the other app if you did that first.

### Work with the console app

Utilize the console app to see how to interact with the Azure Cache for Redis instance directly.

### Implement the code to leverage caching from the ASP.NET Core Web application

1. Update the secrets to use the connection string

1. Notice the use of sample code from various sources (documented in the code files like RedisConnection.cs).

1. Follow the book for directions on working with the code and what is going on with the code

1. Utilize the StatesCachedController to see the code in action

    Requires a database or mocked States data to cache

## Directory.Build.props

The project is set to read all the versions from the Directory.Build.props file.  Use that to leverage which version you want to run and/or update any NuGet references to new versions that are out by the time you review this code.

The default version at time of publish is DN6.  You can leverage DN7 by changing the extension on the props to `dn6` then the `dn7` extension to `props` and the project will default to version 7 with the latest packages at the time of this publishing.  Later, you can change it to DN8 or DN9/10/11, etc and just update the other libraries as needed as well (check for updates in the Nuget Packages for Solution)

## Conclusion

Use this project to help you learn how to interact with the Azure App Configuration and Azure Key Vault
