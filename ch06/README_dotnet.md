# Working with Azure Functions

This README is to help with the implementation of a couple of functions in this application.

You will need to make sure to do a couple of things in order for this project to work correctly.

## Create an Azure Function App

Create a new Azure Function App at Azure.  This version uses the older version of Azure Functions, not the isolated version.  You can use the isolated version, but you will need to make some changes to the code.  I recommend you start with the non-isolated version and then move to the isolated version once you have a good understanding of how the functions work.  The isolated version is a bit more complex to work with, but it is the future of Azure Functions.  I am also including a version where the isolated code can be explored.

1. With the Azure Function App Created, publish it

    You can choose to right-click and publish.  You can decide if you want to use slots. For this book, I recommend you do not use slots for ease of learning and to avoid spending too much time in this part of the book.
    
    For practice, outside of the AZ-204 realm and just professional development, I would recommend you try building a slot, deploy with CI/CD to the slot, and then modify the YAML to work efficiently to deploy to the slot, then look at what it takes to trigger a swap (or do it manually).

    Using slots creates a great deal more work, and is not trivial to manage for durable functions.  You need to remember to

    - use different Durable Task hub names (you should consider using an environment variable to avoid pain here)
    - make sure you have all the connection string information set correctly in the slots where you want to do stuff.  For example, is your event on the storage account triggering both test and production, just test, or just production.  This is all up to you, as long as you get it working in one of the places, then you will understand how to make this all work!

    >**Note:** you can have multiple triggers on the same blob storage.  As I update this, I have three triggers for three different functions, two of which are blob triggers triggers and one is an event grid trigger (they are all event subscriptions through event grid).  I can drop a single file and process with all three event/blob triggers in different versions of the function (isolated/non-isolated, using bindings/using SDK)

1. If you choose to right-click and publish, do not try to bind the integrations in the Kudu console

    One thing I show in the book is how to reset the direction of the bindings for cosmos and the storage account (a very good thing to understand how these bindings work).  This is a futile endeavor and would only hold until the next deployment. You would never do this in the real world.

## Uncomment code and run as you go

There are a number of functions pre-built in this code.  You will need to uncomment as you go in order to follow along with the text.  If a function is missing, just recreate it (that may be the case for something that was done in the book as a demonstration or quick learning effort).  

1. Make sure Function1 works

    To get started, make sure Function1 works.  This is the default function that is created when you create a new function app.  It is a simple HTTP trigger that returns a string.  You can test it in the portal or in Postman.  If you are using slots, make sure you are testing the correct slot.

    For additional practice, get the `GetTopMovies` function working.

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

    >**Important:** Do not forget to set both variables for the `myCosmosConnection` and `myStorageConnection` in the configuration on the function app.  If you do not do this, the function will not be able to use the bindings for processing from storage and to CosmosDB.

## Drop a file

With everything set, upload a file and see the results

1. Use the `SampleItems.xlsx` file in the ParseExcel folder

    Upload the file and monitor the results as shown in the book.

1. Review the data

    When everything is working correctly, the file will be parsed into the Cosmos DB Items container as expected.

## Durable Functions

For the Durable Functions to work, there must be a Task Hub.  The name of the Task Hub lives in the `host.json` file for the project and is pre-set in the sample files.

1. If using slots, make the task hub name dynamic

    To be honest, this part feels very fragile to me (it would be easy to forget to do this and have some issues with slots interfering with one another).  As the book outlines, you can set the hub name to a variable such as  

    ```text
    %mytaskhub%
    ```  

    Then override the variable in the configuration settings for the function app.

    - >**Important:** Do not forget to add the variable to the configuration settings

    - update the host.json file and deploy

## Function Chaining

The function chaining example is simple.  Either uncomment this code or create a new function with an Orchestration trigger.

1. The default code generated is the function chaining pattern.

    The function generation creates the HTTP Start function, which then can be triggered for testing

1. Use the trigger to start it

    The `ChainFunctionOrchestrator_HttpStart` function triggers the orchestration, which you can trigger in the portal, via PostMAN or via cURL.

    The orchestration calls "Say Hello" to three different cities.  This is the default code generated by the function app.

    You can monitor the start, orchestrator, and SayHello functions to see their calls in the portal live metrics.

## Function Fan-Out / Fan-In

This pattern is a bit more tricky to work with, but is really cool to see in action.

1. Uncomment the code or create a new function with an orchestration trigger.

    Change the code as illustrated in the book or review it (it's all included in the sample files if you need a reference). 
    
    Also make sure to include the helper functions for the processing, and the starter function pointing to the orchestrator function.

1. Make sure to put variable in place for the `NumberOfWorkerFunctions` on the Function App in the configuration settings.

    If you forget to do this, you will get `0` as the number of worker functions and the fanning won't happen, nor will the final function fire.

1. Need the http trigger to start it

    Trigger the function `FanInFanOutOrchestrator_HttpStart` to start processing, which triggers the `FanInFanOutOrchestrator`

    Ensure you can kick off the process, the trigger calls to the orchestration, which calls `FirstFunction`.  FirstFunction retrieves the number set in the environment to determine how many functions to orchestrate.

1. The orchestrator creates a parallel task list to execute multiple worker functions at once

    After getting the value from `FirstFunction`, the orchestrator will run whatever number you put into the environment variable `NumberOfWorkerFunctions` tasks in parallel.  I like `6` because of the output.  Feel free to try it with a few different low numbers.  Each parallel task runs the function `WorkerFunction`

1. When all functions have run, the total sum of the results is returned. 

    After the fan is completed, the `FinalFunction` is executed when fanning back in.

    The result of this effort is the sum of doubling every number.  If you put in the number 6, you get the answer to life, the universe, and everything (42).  As the workers accumulate the sum, the final function is called and the total is passed to it.

    If you fail to put a value in for the `NumberOfWorkerFunctions` the final function will still run but your sum total will be 0 and no worker functions will execute.  This makes sense when you think about practical use cases (i.e. there were no files to process today).  

1. If you want to take this to the next level, consider a scenario such as:

    - Connect to storage (input binding or via SDK?)
    - Read the number of files/return a list of all blobs in a container
    - Launch the worker function but process each of the files in the worker function (such as parse out data or put some entry into a database).  Consider moving the file to another storage account or container so it won't be processed again in the future. 
    - At the end of the process, just report that you are done.

## Directory.Build.props

Reminder that this is only for .NET 6, but you can still use this file to determine all of the package versions.  Just don't try to change this to .NET 7 as there are too many breaking changes.

## Conclusion

By now you should be very familiar with Azure Functions, both traditional and durable using the non-isolated function runtime in .NET 6.
