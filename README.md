![GitHub Workflow Status](https://img.shields.io/github/workflow/status/mublog/mublog-server/.NET)
![GitHub milestone](https://img.shields.io/github/milestones/progress/mublog/mublog-server/1)

<img src="https://github.com/mublog/mublog-server/blob/master/docs/mu-logo.svg?raw=true" alt="mublog-logo" width="448" />

# Archived Notice
µblog was a school project to create a working prototype of a microblogging website.
No further development is currently planned
<br/>

# µblog Server

The back-end of µblog written with ASP.NET Core.
Find the official front-end [here](https://github.com/mublog/mublog-web)

## Features

Features Supported by the API

### Posts

+ Get Posts Paginated and Get Posts by ID
+ Post, Edit, Delete Posts
+ Get Comments by Post
+ Create, Delete Comment under Post 
+ Like, Unlike Post

### Media

+ Upload Images
+ Get Images

### Users

+ Get User Profile
+ Get Follower, Following of User
+ Follow, Unfollow User

### Accounts

+ Register Account
+ Log into Account
+ Refresh JWT Token
+ Change, Display Name, Email, Profile Description and Password
+ Delete Account

### Other 

API can be inspected and tested with Swagger by going to example.com/swagger.

## Deployment

The recommended way of deploying µblog is via Docker. The docker file is included in the repository.

1. Deploy a PostgreSQL database and run the scripts in repository under `src/Infrastructure/Data/SQL`
2. `git clone https://github.com/mublog/mublog-server.git`
3. `cd mublog-server`
4. `docker build . -t mublog:latest`
5. `docker volume create mublog-data`
6. `docker volume create mublog-www`
7. compile the [front-end](https://github.com/mublog/mublog-web) and put the files into the docker volume `mublog-www`
8. `docker run --name mublog -d --restart=unless-stopped -v mublog-data:/data -v mublog-www:/app/wwwroot mublog`

It is recommended that you run an NGINX reverse proxy for higher security with HTTPS, [read about it here](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx).

## Future

Potential features that might be implemented in the future if the project continues to be developed.

+ server side caching
+ standalone image server
+ an email service for notifications and authentication
+ more options to customise the profile supported by the profile
+ enhance security features in the back-end
+ custom feed for each registered user
+ search options enabled by PostgreSQL full text search
+ enable horizontal scaling
