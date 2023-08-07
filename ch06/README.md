# Working with Azure Functions

This README is to help with the implementation of a couple of functions in this application.

You will need to make sure to do a couple of things in order for this project to work correctly.

## Create an Azure Function App

Create a new Azure Function App at Azure.  For this book and for my current recommendation, I would suggest just doing Windows and .NET6 non-isolated.  If you want to try isolated .NET 6 or isolated .NET 7 there are a couple of hoops to jump through and I'm not going to be able to guarantee this code will work exactly as provided (I would recommend you start from scratch and then just add functionality as you go rather than trying to use these files).

1. With the Azure Function App Created, publish it

    You can choose to right-click and publish.  You can decide if you want to use slots. For practice, outside of the AZ-204 realm and just professional development, I would recommend you try building a slot, deploy with CI/CD to the slot, and then modify the YAML to work efficiently to deploy to the slot.

    Using slots creates a great deal more work.  You need to remember to

    - use different Durable Task hub names (you should consider using an environment variable to avoid pain here)
    - make sure you have all the connection string information set correctly in the slots where you want to do stuff.  For example, is your event on the storage account triggering both test and production, just test, or just production.  This is all up to you, as long as you get it working in one of the places, then you will understand how to make this all work!

1. If you choose to right-click and publish, do not try to bind the integrations in the Kudu console

    One thing I show in the book is how to reset the direction of the bindings for cosmos and the storage account (a very good thing to understand how these bindings work).  This is a futile endeavor and would only hold until the next deployment.  

## Uncomment code and run as you go

There are a number of functions pre-built in this code.  You will need to uncomment as you go in order to follow along with the text.  If a function is missing, just recreate it (that may be the case for something that was done in the book as a demonstration or quick learning effort).  

1. Make sure Function1 works

    To get started, make sure Function1 works

## Event Grid trigger

The second thing covered is the event grid trigger.  To make this work, you have a few tasks to complete.

1. You need a function that has an event grid trigger, with bindings to Storage and to Cosmos DB.

    The Cosmos DB binding can be added later but to set the trigger and event together you will want to have deployed the function with the binding for the Storage Account in place.

1. Uncomment the function `ProcessExcelToCosmos`

    Remove the comments from the `ProcessExcelToCosmos` function and deploy.

1. Create a new Storage Account with a container for uploads.  Alternatively, leverage a storage account in use (not the backing storage for the function app though!).

1. Get the connection string for the storage account.

1. Set the configuration in the function for the `myStorageConnection` in the Portal

    Add the connection string to the environment variables for the function on the slot you are wiring up.  This is covered in  the book if you are uncertain as to what I'm asking you to do here.

1. With the storage account in place, create an Event Subscription on the storage account

    Once again, leverage the book for information on how to do this.

    >**Note**: If you have deployed the Function App correctly, then everything should be fairly easy.  If you don't see your function app or function as expected, you should double-check that your deployment has succeeded with the code uncommented.

## Bind to Comsos Output

1. Create or leverage an existing Cosmos DB account

    - Ensure that it has a database: `SampleDataItems`
    - Ensure that it has a container: `Items` in the database `SampleDataItems`

1. Get the connection string for Cosmos DB

    Put the connection string into the Function App environment variables as `myCosmosConnection`

## Drop a file

With everything set, upload a file and see the results

1. Use the SampleItems.xlsx file in the ParseExcel folder

    Upload the file and monitor the results as shown in the book.

1. Review the data

    When everything is working correctly, the file will be parsed into the Cosmos DB Items container as expected.

## Durable Functions

For the Durable Functions to work, there must be a Task Hub.  The name of the Task Hub lives in the `host.json` file for the project and is pre-set in the sample files.

1. If using slots, make the task hub name dynamic

    To be honest, this part feels very fragile to me.  As the book outlines, you can set the hub name to a variable such as  

    ```text
    %mytaskhub%
    ```  

    And then override it in the configuration settings for the function app.

    - Add the variable to the configuration settings

    - update the host.json file and deploy

## Function Chaining

The function chaining example is simple.  Either uncomment this code or create a new function with an Orchestration trigger.

1. The default code generated is the function chaining pattern.

    The function generation creates the HTTP Start function, which then can be triggered for testing

1. Use the trigger to start it

    The Start function triggers the orchestration

    The orchestration calls "Say Hello" to three different cities

    You can monitor them and see their calls in the portal.

## Function Fan-Out / Fan-In

This pattern is a bit more tricky to work with

1. Uncomment the code or create a new function with an orchestration trigger.

    Change the code as illustrated in the book or review it (it's all included in the sample files if you need a reference). 
    
    Also make sure to include the helper functions for the processing, and the starter function pointing to the orchestrator function.

1. Make sure to put variable in place for the `NumberOfWorkerFunctions` on the Function App in the configuration settings.

1. Need the http trigger to start it

    Ensure you can kick off the process, the trigger calls to the orchestration, which calls `FirstFunction`.  FirstFunction retrieves the number set in the environment to determine how many functions to orchestrate.

1. The orchestrator creates a parallel task list to execute multiple worker functions at once

    This will run the number you put into the environment variable `NumberOfWorkerFunctions` tasks in parallel.  I like `6` because of the output.  Feel free to try it with a few different low numbers.

1. When all functions have run, the total sum of the results is returned. 

    The result is the sum of doubling every number.  If you put in the number 6, you get the answer to life, the universe, and everything (42).

    If you fail to put a value in for the `NumberOfWorkerFunctions` the final function will still run but your sum total will be 0 and no worker functions will execute.  This makes sense when you think about practical use cases (i.e. there were no files to process today).  

1. If you want to take this to the next level, consider a scenario such as:

    - Connect to storage (input binding or via SDK?)
    - Read the number of files/return a list of all blobs in a container
    - Launch the worker function but process each of the files in the worker function (such as parse out data or put some entry into a database).  Consider moving the file to another storage account or container so it won't be processed again in the future. 
    - At the end of the process, just report that you are done.

## Directory.Build.props

The files are included to update this to .NET 7, but I continue to recommend only running this code in .NET 6 for now. 

Most importantly.  If you switch to .NET 7 do not forgot to update your worker runtime to `dotnet-isolated`.

## Conclusion

By now you should be very familiar with Azure Functions, both traditional and durable.
