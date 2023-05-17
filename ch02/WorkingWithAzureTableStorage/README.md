# Working with Azure Table Storage

In order to make this code work, you will need to complete a couple of basic operations.  First, you will work with Table storage from an Azure Storage account.  After getting that working, you'll use an Azure Cosmos DB for Table account.  To make the code switch, all you need to do is change your connection string.  The SDK code works in either scenario!

## Create an Azure Storage Account and Add a Table

To get started, create an Azure Storage Account.

1. Create an Azure Storage Account named anything you want (or you can use one that is already provisioned)

1. In the storage account, add a new `Table` in the Tables Data Storage section.

    Name the Table: `Universities`

## Get the Connection String

Get the connection string for the Azure Storage account

1. Put the connection string into the usersecrets.json file

1. Run the program

1. Review the code

1. Review the Table in the `Storage Browser` in the Azure Storage Account

## Create a Cosmos DB For Table Account

It is important to understand you will need an account that is set for the Cosmos DB for Table, which is different than a Cosmos DB for NoSQL.  This means you will need to pay for one of the accounts if you have both active on your subscription, or use separate subscriptions in order to have more than one free account.

1. Create the Cosmos DB for Table Account

## Get the Connection String

As noted in the book, the Connection String for the Cosmos DB for Table is under a left-navigation item called "Connection Strings"

1. Get the Connection String, and put it in your `usersecrets.json` file.  

    Either replace the existing one, or comment out the existing one and add the new one with the exact same key and the value of the Cosmos DB for Table connection string.

## Run the program

Once you have the connection string set, there is nothing more you need to do to get the program to work.

1. Run the program

1. Note that everything just works, whether you are going against the Azure Table Storage or the Cosmos DB for Table using the exact same code.

## Directory.Build.props

The project is set to read all the versions from the Directory.Build.props file.  Use that to leverage which version you want to run and/or update any NuGet references to new versions that are out by the time you review this code.

The default version at time of publish is DN6.  You can leverage DN7 by changing the extension on the props to `dn6` then the `dn7` extension to `props` and the project will default to version 7 with the latest packages at the time of this publishing.  Later, you can change it to DN8 or DN9/10/11, etc and just update the other libraries as needed as well (check for updates in the Nuget Packages for Solution)

## Conclusion

Use this project to help you learn how to interact with the Azure Table Storage and Azure Cosmos DB for Table.
