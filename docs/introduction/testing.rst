Testing project
===========

When the application is run with docker, 4501x ports will be reserved for services and 4500x ports for infrastructure. Port 45013 is reserved for the application gateway.

Test data will be added to the system automatically. [See](https://github.com/senkadir/reservations/blob/master/src/Reservations.Services.Offices/Initializations/Extensions.cs)

The structure of the test data is shown in the picture below.

![image](https://user-images.githubusercontent.com/10263337/97174625-65cd6600-17a3-11eb-96ac-e3138afe727f.png)

For a quick start, two collection files that can be run with Postman are placed under the docs folder in the main directory. 

	1. reservations.all.endpoints.json
	2. reservations.cycle.json

Number 1 file contains all the endpoints in the system. 

Number 2 file contains the steps required to make an appointment from the system are listed. 

Once the file is run in Postman with Runner, the steps are listed below. Tests tab can be examined to understand how the automated steps work.

	1. Login: Fake user logged in to the system and JWT token returned as the response and "token" variable assigned to the environment.
	2. Get current offices: Pre-defined offices are listed to get id.
	3. Get resources: Getting all resources in the system to get id.
	4. Check available rooms  for reservation: This step returns the appropriate offices in accordance with the location of the logged-in user at the desired time interval from the system. 
	As a result of this query, the system will return to the Amsterdam office by default.
	5. Create new reservation for room: A reservation request is made for a predefined date range for the Amsterdam office. It is also stated that there should be a whiteboard and a blue marker pen in the room. 
	With this step, the booking cycle is completed.
	6. Rooms and Resources: Lists the rooms in the system and the resources in the rooms.
	7. Get room resources: Lists resources that are in a specific room
	8. My reservations: Lists the reservations of the logged in person