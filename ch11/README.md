# Working with Azure API Management (APIM)

This README is to help with the implementation of a couple of functions in this application.

You will need to make sure to do a couple of things in order for this project to work correctly.

## Notes

If you already worked through Chapter 6, you don't need to do anything else with Azure Functions (unless you didn't implement the `GetMovies` code).  If the `GetMovies` and `Function1` code is uncommented and working, you are ready to complete this project.

In the case where you skipped chapter 6, or you just want a fresh start, deploy a new consumption tier Function App and make sure that you have deployed the sample code provided.  For convenience, the sample code in this chapter already has `Function1` and `GetMovies` ready to go, and the rest of the functions are commented out.

## Deploy an APIM

If you want to do anything other than consumption tier on the APIM (i.e. work with the developer portal [discussed in the book] or see any custom networking features [not used in the book]), you must deploy a Developer or better tier APIM.

>**Note:** It can take 45 minutes or so to deploy anything but a consumption tier APIM instance.

Since it can take some time, work through the deployment as shown in the book before going further, and let it deploy while you ensure your Function App is working.

## Create an Azure Function App (if you don't have one or want to start fresh)

Create a new Azure Function App at Azure.  For this book and for my current recommendation, I would suggest just doing Windows and .NET6 non-isolated.  If you want to try isolated .NET 6 or isolated .NET 7 there are a couple of hoops to jump through and I'm not going to be able to guarantee this code will work exactly as provided (I would recommend you start from scratch and then just add functionality as you go rather than trying to use these files).

1. With the Azure Function App Created, publish it

    You can choose to right-click and publish.  You can decide if you want to use slots. For practice, outside of the AZ-204 realm and just professional development, I would recommend you try building a slot, deploy with CI/CD to the slot, and then modify the YAML to work efficiently to deploy to the slot.

    Using slots creates a great deal more work.  You need to remember to

    - use different Durable Task hub names (you should consider using an environment variable to avoid pain here)
    - make sure you have all the connection string information set correctly in the slots where you want to do stuff.  For example, is your event on the storage account triggering both test and production, just test, or just production.  This is all up to you, as long as you get it working in one of the places, then you will understand how to make this all work!

## Ensure the two functions work

There are a number of functions pre-built in this code.  For chapter 11 (APIM), you only need to have `Function1` and `GetMovies` working.  The remaining functions are irrelevant to this chapter.

1. Make sure `Function1` works

    To get started, make sure `Function1` works, either in the Azure Portal or by making a PostMan or cURL request against it.

1. Make sure the `GetMovies` function works.

    As with `Function1`, make sure you can execute the `GetMovies` function.

## Directory.Build.props

The files are included to update this to .NET 7, but I continue to recommend only running this code in .NET 6 for now (until you can at least test a .NET 7 function in the Portal). 

Most importantly.  If you switch to .NET 7 or .NET 6 Isolated do not forgot to update your worker runtime to `dotnet-isolated` or the Function App won't work.

## Conclusion

Utilize this code (or your previous deployment) to work with APIM and learn the important aspects for the AZ-204 Exam.
