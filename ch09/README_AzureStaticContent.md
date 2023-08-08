# Azure Static Content

This sample file is just for you to work with if you want to play around with Redis Cache outside of a website.

## Getting started

To make this work, all you need to do is create a Redis Cache instance at Azure (use the Basic plan that is inexpensive and does not provide any replication or Enterprise-level tools)

1. Deploy a Redis Cache instance

    Go get a coffee/tea/water, etc

## Get the connection string

The Redis cache will take about 10-15 minutes to deploy and propagate (at the most, it may be quicker) so go get a coffee, then come back and you can easily complete the work.

1. Get the connection string into your user secrets file

    Use the provided sample to set the cache connection string

## Run the program

Run the program to interact with Cache

1. The default program doesn't do much, but it gets you started

1. Play around with other Azure Redis Commands

    https://learn.microsoft.com/azure/azure-cache-for-redis/cache-dotnet-how-to-use-azure-redis-cache

## Directory.Build.props

The project is set to read all the versions from the Directory.Build.props file.  Use that to leverage which version you want to run and/or update any NuGet references to new versions that are out by the time you review this code.

The default version at time of publish is DN6.  You can leverage DN7 by changing the extension on the props to `dn6` then the `dn7` extension to `props` and the project will default to version 7 with the latest packages at the time of this publishing.  Later, you can change it to DN8 or DN9/10/11, etc and just update the other libraries as needed as well (check for updates in the Nuget Packages for Solution)

## Conclusion

Use this project to help you learn how to interact with the Azure Cache for Redis using the StackExchange.Redis NuGet Package from a console application.
