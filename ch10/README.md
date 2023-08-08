# Simple MVC Web 

As with other chapters, this code is progressive through chapter 4, 7, 8, and 9, prior to this chapter.

That being said, you can use these starter files for just chapter 10 if you are skipping directly to this chapter.  You can get by without a database for this chapter as well, unless you would like to see it in the application map.  Chapter 7's identity stuff is also cool to see in the Application map.

If you've been working along with the book, you don't need these files; just use your existing project and leverage the remaining pieces of information (your code is likely already working for Application Insights).

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

## Chapter 9

You don't need to worry about Azure Cache for Redis.  If you skipped that chapter, just don't use the StatesCachedController as that part won't work.

## Learning about Azure Monitor, Metrics, and Logs

Make sure to review the first part of the text which discusses Azure Monitor and what the differences are between metrics, logs, and traces.

Also learn about the components of Azure Monitor, specifically how to visualize information and where you can send the data for further visualization (i.e. Alerts to webhooks and external APIs/Dashboards)

## Application Insights

The code for Application Insights should already be instrumented, and, unless you turned it off, the app service you deployed likely already has Application Insights logging data.

1. Review the Program.cs file to see the application insights added and the connection string is leveraged:

    ```cs
    builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);
    ```  

    Note this means you need to place the app insights connection string into your user secrets if you haven't already

1. Review the `TelemetryClient`

    The `HomeController` already has the telemetry client injected

1. Leverage the `TelemetryClient`

    In the `HomeController`, look for calls to `TrackTrace`, `TrackEvent`, and `TrackException`.

## Create Availability Tests

Take some time to create an availability test (Standard and/or Ping) and see how this works to probe the health of your website.

## Review the Application Map

If you have Identity and the database set up, there is much more to see here.  You can see how many calls happen and what the latency is, as well as any potential bottlenecks.  You can also drill into the specific piece of the map to get more details.

## Use Kusto Queries

Learn how to create a couple of basic Kusto queries

1. Leverage the Query in an alert

1. Create a query and pin it to a shared dashboard

## Visualization Tools

Make sure to review the various ways you can `see` the data, whether its live on a dashboard or in Power BI or Grafana, or if it's static like in a workbook.


## Directory.Build.props

The project is set to read all the versions from the Directory.Build.props file.  Use that to leverage which version you want to run and/or update any NuGet references to new versions that are out by the time you review this code.

The default version at time of publish is DN6.  You can leverage DN7 by changing the extension on the props to `dn6` then the `dn7` extension to `props` and the project will default to version 7 with the latest packages at the time of this publishing.  Later, you can change it to DN8 or DN9/10/11, etc and just update the other libraries as needed as well (check for updates in the Nuget Packages for Solution)

## Conclusion

Use this project to help you learn how to interact with the Azure Application Insights
