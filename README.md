# ACME-Server

This repository provides base libraries to implement an ACME-compliant (RFC 8555) server.
It consists of 4 base nuget packages and one storage implementation.
This is not a runnable product and it needs an implementation for certificate issuance (separately available).

## Known implementations

If you are looking for a prebuild server, this list provides some runnable products for your needs: 

### ACME-ACDS
Made for Entities running Active Directory Certificate Services (ACDS), wanting to issue certificates via ACME.  
*License*: free for non-profit, proprietary for commercial use.
https://github.com/glatzert/ACME-Server-ACDS  

## Want to build your own?

The libraries are distributed via NuGet.org.

### TGIT.ACME.Server.Core

https://www.nuget.org/packages/TGIT.ACME.Server.Core  
Contains nearly everything neccessary to run an acme server on asp.net core.
Reference this, if you want to provide your own deployable server, but do not want to implement API endpoints.

### TGIT.ACME.Protocol.Storage.FileStore  

https://www.nuget.org/packages/TGIT.ACME.Protocol.Storage.FileStore
Contains a storage provider based on files.
Reference this, if you want to provide your own deployable server and do not want to implement the storage layer.


### TGIT.ACME.Model

https://www.nuget.org/packages/TGIT.ACME.Protocol.Model  
Contains model classes for internal use of the implementations as well as http-model classes for use with ACME servers.

### TGIT.ACME.Protocol.Abstractions

https://www.nuget.org/packages/TGIT.ACME.Protocol.Abstractions  
Contains interfaces uses by the core server and protocol implementations.  
Reference this, if you want to create own storage or issuance providers.

### TGIT.ACME.Protocol

https://www.nuget.org/packages/TGIT.ACME.Protocol  
Contains a default implementation for all services (besides storage and issuance) defined in Protocol.Abstractions.
Reference this, if you want to create an own implementation of the http layer, but do not want to create the service implementations.

### Do I need something else?

Yes. To make it a fully runnable server, you need to Implement ICertificateIssuer and register it in your source code.