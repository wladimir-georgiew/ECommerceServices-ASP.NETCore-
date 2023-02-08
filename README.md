Deployed to Azure on - https://fastservices.azurewebsites.net/
# Technologies, Packages and Software :computer:
 - ASP.Net Core
 - Entity Framework Core
 - Stripe.NET
 - Quartz.NET
 - XUnit.NET
 - Moq
 - SendGrid
 - AJAX
 - JavaScript
 - Bootstrap
 - Toastr Notifications
 - Google reCAPTCHA v3
 - Embed Google Maps
 - Microsoft SQL Server Management Studio
 - Visual Studio 2019
 
# Description :memo:
**A responsive web application system used for creating orders for specific home services with included employee and admin roles.**

## User Functionality
 - ### Create account
 ![image](https://user-images.githubusercontent.com/61605749/217587095-b614f383-912c-4080-92e9-66040d789f1e.png)
 - ### Upload profile picture.<br>
 ![image](https://user-images.githubusercontent.com/61605749/217587400-9243bb90-899c-46d8-a451-574ab924f42c.png)
  -If no image is provided there will be automatically set default for users.<br>
  -Extension and size validations.
 - ### Submit Order<br>
 ![image](https://user-images.githubusercontent.com/61605749/217587586-8871b941-6039-4714-976a-fccdf22be897.png)
  -Only users can create orders (one active order at a time).<br>
  -Order price is being calculated depending on the service fee amount, 'number of workers' and 'hours booked' amount selected by the user. (5$ per hour for every worker)
 - ### Orders History<br>
![image](https://user-images.githubusercontent.com/61605749/217588107-1e4057f0-cb35-4553-8a8e-b6a486b09368.png)
  -View active and completed orders.<br>
  -Order is considered active if the due date is in future time.<br>
  -Order status is being set to completed automatically after the due date has passed current time.<br>
  -Submit a complaint message for completed orders (the message is sent via email using SendGrid to the support, containing description, order and user data).<br>
  -Pay for the order via credit/debit card.<br>
 - ### Comment on department<br>
 ![image](https://user-images.githubusercontent.com/61605749/217588371-a5c011e5-ffc1-4d95-83fb-2c45b14c0050.png)
  -Each department has its own separate comment section with rating system.<br>
  -Comment once every 24 hours (spam protection).<br>
  -Delete their own comments.<br>
  -Pagination.<br>
  
## Employee Functionality
 ###### Employee account has the same functionality as a user account with some additional features and the restriction of creating orders.<br>

 - ### Order schedule/history<br>
 ![image](https://user-images.githubusercontent.com/61605749/217588620-c3a03b0c-752e-4a8c-83a5-29745d5e7d9e.png)
 -View active orders which were assigned to them and are yet to be completed.<br>
 -They are automatically being assigned to an order, depending on their department and active orders schedule.Cannot be assigned to two orders with same or overlapping start hours.<br>
 -View completed orders.<br>
 -Employees salary is being incresed by 1% of the price of every completed order. The increase is being calculated and added to the salary at the 1st day of every month at 10:30 and includes older increases(it's stacking).<br>
 ![image](https://user-images.githubusercontent.com/61605749/217588819-b65f6c01-d5ac-4334-9bf6-604cd2d2e188.png)
 
 ## Admin Functionality
  ###### Admin account has the same functionality as a user account with some additional features and the restriction of creating orders.<br>
  
 - ### Delete Comments<br>
 ![image](https://user-images.githubusercontent.com/61605749/217588915-231e9c3f-e804-40e3-a6db-10180338e71a.png)
  -Delete other user comments.<br>
 - ### Admin Panel<br>
 ![image](https://user-images.githubusercontent.com/61605749/217589059-49d62ed4-d6ff-4636-b57f-31f357044fec.png)
  -Create department.<br>
  -Create service for department.<br>
  -Create employee account.<br>
 - ### Create Department<br>
 ![image](https://user-images.githubusercontent.com/61605749/217589159-c5e08ebe-99fa-4eb0-a838-fd1376481975.png)
  -Upload image for the card (if no image is uploaded it has default one).<br>
  -Upload image for the navigation background inside department view (required).<br>
  -Add Description.<br>
 - ### Create Service<br>
 ![image](https://user-images.githubusercontent.com/61605749/217589257-119171e7-e7c5-43dc-a28e-adb64bcf277a.png)
  -Upload image for the card (if no image is uploaded it has default one).<br>
  -Add Description.<br>
  -Add fee amount.<br>
  -Specify the department in which the service needs to be added.
 - ### Employees View<br>
 ![image](https://user-images.githubusercontent.com/61605749/217589386-3c665764-f9cb-4991-af43-7e1f01c4fa7c.png)
  -View with all the employee accounts.<br>
  -Search by name filter.<br>
  -Filters - "All", "Deleted Only", "Available Only".<br>
  -Delete / Restore employee account.<br>
  -Pagination.<br>
 - ### Create Employee<br>
 ![image](https://user-images.githubusercontent.com/61605749/217589480-8ab26630-79ae-4cff-8404-77c3698a9000.png)
  -Basic account creation requirements with some additional.<br>
  -Upload image for profile picture. If no image is provided there will be automatically set default for workers.<br>
  -Specify the department the employee is going to work in.<br>
  -Specify the initial salary for the employee.<br>
 - ### Stripe Dashboard<br>
 ![image](https://user-images.githubusercontent.com/61605749/217589544-c951debf-2f4c-4d09-93bc-6ab40ae08994.png)
  -Stripe.NET Account with dashboard for all the transactions made on the website with various information about them and an option for performing refunds.
  
  
# Database Diagram :chart:
![dbdiagram](https://user-images.githubusercontent.com/61605749/101865298-8d736480-3b7e-11eb-90f2-663d0e59cc0b.png)
