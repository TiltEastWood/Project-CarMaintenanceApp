# CAR MAINTENENCE APP
#### Video Demo: https://youtu.be/elLlvJ0jpyY
#### Description: 
This is a car maintenance app Blazor project that allows users to keep track of their car's maintenance history. 
Users can add, edit, and delete maintenance records. The app also provides a summary of the car's maintenance history and inspection dates, 
indicating when you should go for an inspection to get approval that the car is roadworthy.

One of the goals of this project was to learn how to use Blazor and Entity Framework Core. It was a great learning experience, 
and I am happy with the result. In this project, Program.cs is the entry point of the application. 
It is responsible for setting up the host and configuring the services that the application will use, such as adding Razor pages, services, 
and the database context. The database context is used to interact with the database. Client and logging are also configured in Program.cs.

The app uses a SQLite database to store the car maintenance records, users, passwords, cars, and pictures. 
The database is created and seeded with some data when the application starts. Pictures are saved both on disk and in the database. 
I did this because I read it's better to use two ways to save pictures. As you can see in this project, 
I created a folder named Services which contains all the services that are used in the project. 
The services are used to interact with the database and perform other operations like adding a car, editing a car, deleting a car, 
adding a maintenance record, editing a maintenance record, deleting a maintenance record, etc.

In the Models folder, I have all the classes such as Car, User, ServiceRecord, Register, and Login. Also in this folder, 
I have the CarMaintenanceContext class that is used to interact with the database and the CarMaintenanceContextFactory class that is used to create 
the database context. In the Pages folder, I have all the pages that are used in the project. 
The pages are used to display the UI and interact with the user. There are pages like CarList, 
which allows displaying cars associated with the logged-in user. It allows the user to click on a car to edit, delete, or view its details. 
CarDetails shows car details and allows the user to add a car service record and view car service records.

When developing the app, I took special care to ensure that user authentication and data security were handled appropriately. 
For this reason, I implemented password hashing to securely store user passwords. 
Password hashing is the process of converting a plain-text password into a secure, irreversible string using a cryptographic algorithm. 
This is crucial because storing passwords as plain text in a database can be a significant security risk. If a malicious actor were to access the database, 
they would have access to users' unencrypted passwords. By hashing the passwords, we ensure that even if the database is compromised, 
the original password cannot be retrieved. In this project, I used a password hasher provided by ASP.NET Core Identity to hash and verify passwords. 
This approach provides an added layer of protection and follows security best practices to safeguard user data.

In addition to password hashing, I also implemented cookie authentication. Cookie authentication is a mechanism used to manage user sessions in web 
applications. When a user logs in, a cookie is created and stored on their device. This cookie contains a secure authentication token that identifies 
the user across requests. The authentication token is sent with each request made by the user, allowing the app to validate the user’s identity and 
ensure they have the proper permissions to perform actions like editing or deleting their car records. The use of cookie authentication ensures that users 
can stay logged in across sessions without needing to repeatedly enter their credentials. The authentication token in the cookie is encrypted and signed to 
prevent tampering, making it a secure way to handle sessions. Without authentication and secure session management, users would have to log in repeatedly or 
the app would be vulnerable to unauthorized access. In this project, I used ASP.NET Core’s cookie authentication middleware, which is configured to store
an auth_token cookie with a 30-day expiration period. The app also redirects users to the login page if they are not authenticated or if their session has 
expired.

In conclusion, this project was a time-consuming and sometimes challenging but rewarding experience. I learned a lot about Blazor, 
Entity Framework Core, and ASP.NET Core Identity. I also gained valuable experience in building a full-stack web application from scratch and 
was able to use my creativity to think of something that would benefit me.