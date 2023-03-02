##Architecture & Patterns

-Modular Monolith Architecture with single bootstrapper(web api project)
-Clean architecture
-Domain Driven Design => Domain model pattern, Aggregate pattern,Active record pattern,Repository pattern, Value objects,Domain Services,Domain events pattern,Integration Event pattern
-Test Driven Development=> Unit test, Integration test
-CQRS pattern

##Technologies & Libraries

-MediatR =>Used to create Asynchronus messaging communication between modules
-Fluent validation
-xUnit(Unit testing framework)
-Fluent Assertion
-Serilog
-EF core ORM




## Scenario & Requirements

"As a gym owner, I want to be able to track the members of my gym, so that I can manage their membership status and billing."

In order to achieve this, the gym management system should have the following capabilities:

--Allow me to add new members to the system, including their personal information (name, contact details, membership type, etc.)

--Allow me to view and update the information for existing members

--Keep track of the membership expiration dates for each member, and send automatic reminders to renew their membership if necessary

--Generate billing statements for members on a regular basis (e.g. monthly, quarterly, annually)

--Allow members to make payments for their membership fees and other charges (e.g. personal training sessions, equipment rentals) through the system

--Provide a way for me to view reports on the gym's revenue and membership numbers, including trends over time

With these capabilities, the gym management system will help me effectively manage my gym's members and billing process, allowing me to focus on providing a high-quality experience for my customers.


# Context Designs
Initial context design is shown below keep in mind this design might change in the future as the requirements of system might change 

![image](https://user-images.githubusercontent.com/42850688/211002819-0f8c9025-2c36-4738-963e-ea5e082c8b57.png)

## High Level Entity Relation & Initial Context Integrations

below there is initial application design as a whole this diagram might change in following section 

![image](https://user-images.githubusercontent.com/42850688/211003497-598d545c-7c6e-43dc-b883-bda4eb438e99.png)

## Customer Context Design And Requirements 

--Customers will be noticed 1 week before the end of membership

--Customers will be noticed if Membership period is over

--If customer did not process the payment in a given time membership will be deleted 

--When membership period ends customer will be deactivated

--Customers can start their memberships through system or manually with employee

--Customers can have an account in system but they may want to start their membership later through application

--Owner wants to know how many months a customer is member in total

--Customer will be able to see their past invoices 

--If a customer creates a membership with a time period of 1 year it will receive additional %10 discount automatically


<img align="left" src="https://user-images.githubusercontent.com/42850688/211217398-b8d2e1cc-4218-4635-a445-46356ecbb9fc.png">

