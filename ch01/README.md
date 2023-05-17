# Working with Azure Blob Storage

The instructions for this are fairly straightforward and are covered in the book.  What follows is the TL/DR version

## Create a storage account

You'll need a storage account.

1. Create a normal storage account

    Keep it simple, no need to have replication or any other fancy features.

## Get the Access Key

After you create the account, get the Connection String

1. Put the connection string into your user.secrets file

## Review the code

The code is highlighted in the text.

1. Run the program

    Review the operations and how to work with Blob Storage

1. Consider writing the code yourself

    Now that you've seen the operations and the interactions, try to replicate the functionality yourself to get the practice of typing the commands so you can see them as preparation for the exam.

## Directory.Build.props

The project is set to read all the versions from the Directory.Build.props file.  Use that to leverage which version you want to run and/or update any NuGet references to new versions that are out by the time you review this code.

The default version at time of publish is DN6.  You can leverage DN7 by changing the extension on the props to `dn6` then the `dn7` extension to `props` and the project will default to version 7 with the latest packages at the time of this publishing.  Later, you can change it to DN8 or DN9/10/11, etc and just update the other libraries as needed as well (check for updates in the Nuget Packages for Solution)

## Conclusion

Use this project to help you learn how to interact with the Azure Blob Storage
