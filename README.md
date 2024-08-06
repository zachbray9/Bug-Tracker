# Bug Tracker
 An intuitive Project Management web app made using React, Typescript, MobX, Chakra UI, and .Net 6 that allows development teams to collaboratively manage projects. Users can create and assign tickets, keep track of ticket status and priority, and chat in real time making the development process streamlined and organized.

## Youtube Demo
 [![Bug Tracker Demo](https://img.youtube.com/vi/nnRKnhd6yys/0.jpg)](https://www.youtube.com/watch?v=nnRKnhd6yys)
   
## Tech Stack
### Frontend
* React.js | Typescript | Chakra Ui | MobX
### Backend
* C# | .Net 6 | Entity Framework | Sql Server
### Deployment
* Microsoft Azure | Github Actions

## What I Learned
### Npm
* Used the npm cli to download packages build the appliation
### React
* Created reusable components
* Navigation using React Router
* Made Api calls using custom Axios agent and interceptors
### Styling
* Learned about Chakra Ui's diverse component library 
* Created custom theme to implement dark mode and globally adjust component styles to whatever I want
* Used Chakra's built in breakpoints to make components responsive for different screen sizes
### Central State Management
* Used MobX to create stores for different parts of the application (commonStore, userStore, projectStore, ticketStore, ect..)
* Used MobX actions to load content and make api calls throughout the application.
### Authentication
* Implemented JWT Bearer authentication for my api to ensure only authorized users could call my endpoints.
* Used Auth and Refresh Tokens to allow users to call the api without having to reauthenticate every time.
### Entity Framework
* Learned about relational databases and how to create entity relationships.
* Used the Entity Framework Tools CLI to add migrations and update my database.
* Learned about DbContext and how to use it to access Entities in my database.
* Used Microsoft's built in IdentityUser class for its built in properties and access to the UserManager.
### Real Time Chat Using Web Sockets
* Implemented SignalR to create a chathub and allow users to chat with each other in real time using web sockets
### Microsoft Azure
* Used an Azure SQL Server Database for my production database.
* Created an Azure Storage Account to store and fetch user profile pictures.
* Deployed the app using an Azure App Service and used it's environment variables to store my app secrets such as connection strings.
### Continuous Integration
* Implemented a CI/CD pipeline that automatically deploys my appliation when I push changes to my main branch.
* Created a development branch where I create features and use Pull Requests to merge commits into the main branch and update the production application.
