SafeHaven
=========

A Xamarin Forms mobile app to help people take photos of their important documents

Project Goals
-----
In my previous work with financial advisors, I created a product they could give their clients to organize their life documents - basically a polished and comprehensive filing system. I received regular feedback that some of their clients wanted a more tech savvy solution. I designed SafeHaven to fill that niche - you can take photos of your important documents and store them in the cloud. I also added a feature so that users could designate other registered users as "accessors" their account, so that their loved ones could have immediate access in case of emergency.

This was Nashville Software School back-end capstone project, and I wanted to learn some mobile development before graduating, which is not covered at NSS. Xamarin was the obvious choice - I used my existing C#/.Net knowledge while building a cross-platform app.

Technologies
------------
SafeHaven is built in [Xamarin Forms](https://www.xamarin.com/forms) and geared primarily towards iOS at the moment. It stores and retrieves data from a SQLite server attached to an ASP.NET web API. It accesses the native camera using [Xamarin Media](https://github.com/jamesmontemagno/MediaPlugin). I used [ngrok](https://ngrok.com/) for testing - super helpful!

Features
------------

 - Create and update documents
 - Take pictures of documents
 - Give access to other registered users
 - Gain access to other users' documents

Features in Development

 - Full CRUD on documents and images
 - Search functionality to find documents faster
 - Organize documents into folders
 - Deploy it via Azure
 - Develop the Android version (will have to wait until a have a device to test the camera with).
