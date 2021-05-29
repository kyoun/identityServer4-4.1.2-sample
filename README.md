# identityServer4-4.1.2-sample

### Grant Types
1. Client Credentials - No user involved. machine to machine, trusted 1st party source, server to server, 
2. Resource Owner Password - User involved, trusted 1st party (client app is trusted by the server that knows client is capable of storing user credential)
3. Authorization Code - google, facebook, etc returns. user involved, web app (server app), 3rd party app. use google or facebook to authenticate a user
4. Implicit - user involved. redirect browser to identity server and user login with user credential. user approves the browser (app) access the resources.
5. Hybrid
