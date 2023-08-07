# Simple MVC Web 

## Create a basic database

You'll need to utilize a database if you want to do the authorization stuff later in the book.  For now, you could skip this, but if you deploy a Basic Azure SQL database, it will run about $5/month.

1. Deploy the Basic SQL Database

1. Manually migrate the database or just uncomment the code to enable automatic migrations (in the Program.cs file).

## Create the App Service Plan and App Service

Feel free to create the plan first, or just create the plan while deploying, your choice.  

If you want to utilize slots and scaling (good practice for the exam), you'll need to deploy an S1 or better instance.  If you just want to deploy the application so that you can work with it in the most basic form (no slots, no scaling), use the Free (F1) deployment.

1. Create your App Service (and Plan)

1. Right-Click and Publish or set up CI/CD

    Either just publish it from your machine or set up CI/CD to your GitHub (or other GIT service).

1. If using the database, ensure the migrations (manual by connecting from your machine and running them, or via code in Program.cs)

1. Validate the site is working

## Review the text

Follow the text to do anything else you want to learn about with the site, including:

1. Deployment Slots and Swapping, directing traffic to a slot
1. Scaling the app

## Important

This website and code is used in a couple of additional chapters (7, 8, 9, and 10).  Those chapters build on this site.  If you want practice or you don't want to keep services alive, feel free to destroy the web app when you are done with the chapter.  Otherwise, leave the site in place until you have completed the additional work.

- [ ] Configure your application insights (Utilized in Chapter 10, but should be deployed with the App Service)
- [ ] Most of the code is present, a lot is commented out until you get to later chapters (security for KeyVault, working with Azure App Configuration, Redis Cache, etc)
- [ ] make sure to check the connection string and update the database if/when using from this point on
- [ ] Review settings in Directory.Build.props for package versions (use Nuget package manager to update)

## Directory.Build.props

The project is set to read all the versions from the Directory.Build.props file.  Use that to leverage which version you want to run and/or update any NuGet references to new versions that are out by the time you review this code.

The default version at time of publish is DN6.  You can leverage DN7 by changing the extension on the props to `dn6` then the `dn7` extension to `props` and the project will default to version 7 with the latest packages at the time of this publishing.  Later, you can change it to DN8 or DN9/10/11, etc and just update the other libraries as needed as well (check for updates in the Nuget Packages for Solution)

## Conclusion

Use this project to help you learn how to interact with the Azure App Services and Azure App Service Plans
