# Working with Working with Azure Cosmos DB

For this example, you need to create an Azure Cosmos DB for NoSQL account.  The code will do the rest of the work.

## Create the Cosmos DB Account

First, create the Cosmos DB Account, then put the connection information into user secrets

1. Create the account

1. Get the connection string, the endpoint, and the primary key

    Place these in the usersecrets.json file.  Note that you can connect by composing the client directly against the connection string or by using the endpoint and primary key together.  Both connection compositions are in the code, but the one that utilizes the endpoint and key is used for the default code set up.

## Run the code

The code is complete, so just run it and see it in action

1. Run the code

1. Review the code to see what each step is doing and how you can work against the Cosmos DB for NoSQL account via the SDK

1. Switch to using the connection string only; the code still works just fine.

>**NOTE:** Make sure to notice the difference in iteration between the Query Iteration and the LINQ Iteration

## Directory.Build.props

The project is set to read all the versions from the Directory.Build.props file.  Use that to leverage which version you want to run and/or update any NuGet references to new versions that are out by the time you review this code.

The default version at time of publish is DN6.  You can leverage DN7 by changing the extension on the props to `dn6` then the `dn7` extension to `props` and the project will default to version 7 with the latest packages at the time of this publishing.  Later, you can change it to DN8 or DN9/10/11, etc and just update the other libraries as needed as well (check for updates in the Nuget Packages for Solution)

## Conclusion

Use this project to help you learn how to interact with the Azure Cosmos DB for NoSQL
