# GymManagement-DDD-Events-CQRS-Auth
This is an actual application backend that is currently used by a local Gym for a simple application like this it would be worse decision to add complexities like Domain Model Patter, Events,CQRS, Messaging etc. My aim was to have an application that is working properly and also practice my Domain Driven Design skills.

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

![image](https://user-images.githubusercontent.com/42850688/211003497-598d545c-7c6e-43dc-b883-bda4eb438e99.png)

