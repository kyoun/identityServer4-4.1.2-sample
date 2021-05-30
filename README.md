# identityServer4-4.1.2-sample

### Grant Types
1. Client Credentials - No user involved. machine to machine, trusted 1st party source, server to server, 
2. Resource Owner Password - User involved, trusted 1st party (client app is trusted by the server that knows client is capable of storing user credential)
3. Authorization Code - google, facebook, etc returns. user involved, web app (server app), 3rd party app. use google or facebook to authenticate a user
4. Implicit - user involved. redirect browser to identity server and user login with user credential. user approves the browser (app) access the resources. (web application, server side web app)
5. Hybrid - Combination of Implicit and Authorization Code. Identity is added to the token.  User involved. (native application, server side web app, desktop and mobile app)


User Involved - 2, 3, 4, 5
Machine to machine trusted connection - 1
Login with Google, Face book account - 3, 5

### To use Implicit grant, IdentityServer4 template need to be installed
This will add user interface for user login and forward to the API.

to install the template, run the command:
-   dotnet new --install IdentityServer4.Templates
   
if above doesn't add the template, try with version number:

-  dotnet new --install IdentityServer4.Templates::4.0.1

This will shows the list of the template installed and there will be template for identity server 4

![image](https://user-images.githubusercontent.com/3983653/120103705-41e85d80-c194-11eb-82b9-b807afcb06b8.png)


after the template is installed, move to the identity server project folder then run

- dotnet new is4ui

![image](https://user-images.githubusercontent.com/3983653/120103719-53ca0080-c194-11eb-97dd-a6069cb8fb63.png)


