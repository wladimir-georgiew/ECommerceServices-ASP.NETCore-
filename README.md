# Technologies, Packages and Software :computer:
 - ASP.Net Core
 - Entity Framework Core
 - Bootstrap
 - Stripe.NET
 - Quartz.NET
 - SendGrid
 - AJAX
 - Vanilla JS
 - Toastr Notifications
 - Embed Google Maps
 - Microsoft SQL Server Management Studio
 - Visual Studio 2019
 
# Description :memo:
**A web application system used for creating orders for specific home services.**

## User Functionality
 - ### Create account
 - ### Upload profile picture.<br>
  -If no image is provided there will be automatically set default for users.<br>
  -Extension and size validations.
 - ### Submit Order<br>
  -Only users can create orders (one active order at a time).<br>
  -Order is considered active if the due date is in future time.<br>
  -Order status is being set to completed automatically after the due date has passed current time.<br>
  -Order price is being calculated depending on the service fee amount, 'number of workers' and 'hours booked' amount selected by the user. (5$ per hour for every worker)
 - ### Orders History<br>
  -View active and completed orders.<br>
  -Submit a complaint message for completed orders (the message is sent via email using SendGrid to the support, containing description, order and user data).<br>
  -Pay for the order via credit/debit card.<br>
 - ### Comment on department<br>
  -Each department has its own separate comment section with rating system.<br>
  -Comment once every 24 hours (spam protection).<br>
  -Delete their own comments.<br>
  -Pagination.<br>
  
## Employee Functionality
 ###### Employee account has the same functionality as a user account with some additional features and the restriction of creating orders.<br>

 - ### Order schedule/history<br>
 -View active orders which were assigned to them and are yet to be completed.<br>
 -They are automatically being assigned to an order, depending on their department and active orders schedule.Cannot be assigned to two orders with same or overlapping start hours.<br>
 -View completed orders.<br>
 -Employees salary is being incresed by 1% for every order they have completed. The increase is being calculated and added to the salary at the 1st day of every month at 10:30 and includes older increases(it's stacking).<br>
 
 ## Admin Functionality
  ###### Admin account has the same functionality as a user account with some additional features and the restriction of creating orders.<br>
  
 - ### Delete Comments<br>
  -Delete other user comments.<br>
 - ### Admin Panel<br>
  -Create department.<br>
  -Create service for department.<br>
  -Create employee account.<br>
 - ### Create Department<br>
  -Upload image for the card (if no image is uploaded it has default one).<br>
  -Upload image for the navigation background inside department view (required).<br>
  -Add Description.<br>
 - ### Create Service<br>
  -Upload image for the card (if no image is uploaded it has default one).<br>
  -Add Description.<br>
  -Add fee amount.<br>
  -Specify the department in which the service needs to be added.
 - ### Employees View<br>
  -View with all the employee accounts.<br>
  -Search by name filter.<br>
  -Filters - "All", "Deleted Only", "Available Only".<br>
  -Delete / Restore employee account.<br>
  -Pagination.<br>
 - ### Create Employee<br>
  -Basic account creation requirements with some additional.<br>
  -Upload image for profile picture. If no image is provided there will be automatically set default for workers.<br>
  -Specify the department the employee is going to work in.<br>
  -Specify the initial salary for the employee.<br>
 - ### Stripe Dashboard<br>
  -Stripe.NET Account with dashboard for all the transactions made on the website with various information about them and an option for performing refunds.
  
  
# Database Diagram :chart:
![dbdiagram](https://user-images.githubusercontent.com/61605749/101865298-8d736480-3b7e-11eb-90f2-663d0e59cc0b.png)
