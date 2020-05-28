# (C#) Server to Server google play authorization

If you are creating a multiplayer game for a mobile market and you want to sell anything in it than mayby you will face a fact that your inapp purchases should be validated on your server backend side.
If your game monetization is based on inapps and you want keep your game save from market exploits you need to validate each purchase on the server side to keep appropriate items/currencies state on your user-storage side.

After a days of research and going through a couple of tutorials and a tons of google documentation I finally achieve what I'v needed.
To keep if for myself and others I'll describe here how I'v done it.

To call google's api with appropriate authorization you will find a lot of information about OAuth2 authorization with generating refresh_token that will let you generate access_token that should be used in 
http authorization header to make google api calls by a server. In my case it doesn't work. What I'v receive was always 403 with information about not linked application. The problem is that you need to bind authorization data 
that will be used by a server with your google publisher console and however I'v done it the result was the same.

So what worked for me?
1. First of all you have to go to your <a href="https://play.google.com/apps/publish/" target="_blank">`Google Play Dev console`</a>.
2.Go to Settings->API access
3.In Service Accounts you have to create a publisher one
4.Role is not so important right now
5.Furnish New private key (P12) with "notasecret" password

Keep this file secure.

6. In new Service Account click Grant Access and modify permissions however you need.
7. Cog->Change Permissions->Enable global visibility and manage orders permissions.


Now the project part.

Add P12 key to your project in a way you will be able to access it at runtime.
In my case I'm just adding it to the project folder -> Add existing file.
I'v marked key to be copied to output directory to keep it in build.
In AuthorizationConstoleTest.Program you have to fill up all the consts used by GoogleServerAuthorizationProvider in function Main.
You can run end debug this console application to see how authorization goes.
I hope it will be usefull for some of you :)
